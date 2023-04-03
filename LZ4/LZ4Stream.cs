using System;
using System.IO;
using System.IO.Compression;

namespace LZ4
{
	// Token: 0x02000611 RID: 1553
	public class LZ4Stream : Stream
	{
		// Token: 0x06002FF2 RID: 12274 RVA: 0x000EAA70 File Offset: 0x000E8C70
		[Obsolete("This constructor is obsolete")]
		public LZ4Stream(Stream innerStream, LZ4StreamMode compressionMode, bool highCompression, int blockSize = 1048576, bool interactiveRead = false)
		{
			this._innerStream = innerStream;
			this._compressionMode = compressionMode;
			this._highCompression = highCompression;
			this._interactiveRead = interactiveRead;
			this._isolateInnerStream = false;
			this._blockSize = Math.Max(16, blockSize);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000EAAAC File Offset: 0x000E8CAC
		public LZ4Stream(Stream innerStream, LZ4StreamMode compressionMode, LZ4StreamFlags compressionFlags = LZ4StreamFlags.None, int blockSize = 1048576)
		{
			this._innerStream = innerStream;
			this._compressionMode = compressionMode;
			this._highCompression = ((compressionFlags & LZ4StreamFlags.HighCompression) > LZ4StreamFlags.None);
			this._interactiveRead = ((compressionFlags & LZ4StreamFlags.InteractiveRead) > LZ4StreamFlags.None);
			this._isolateInnerStream = ((compressionFlags & LZ4StreamFlags.IsolateInnerStream) > LZ4StreamFlags.None);
			this._blockSize = Math.Max(16, blockSize);
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000EAB00 File Offset: 0x000E8D00
		private static NotSupportedException NotSupported(string operationName)
		{
			return new NotSupportedException(string.Format("Operation '{0}' is not supported", operationName));
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000EAB12 File Offset: 0x000E8D12
		private static EndOfStreamException EndOfStream()
		{
			return new EndOfStreamException("Unexpected end of stream");
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000EAB20 File Offset: 0x000E8D20
		private bool TryReadVarInt(out ulong result)
		{
			byte[] array = new byte[1];
			int num = 0;
			result = 0UL;
			while (this._innerStream.Read(array, 0, 1) != 0)
			{
				byte b = array[0];
				result += (ulong)((ulong)((long)(b & 127)) << num);
				num += 7;
				if ((b & 128) == 0 || num >= 64)
				{
					return true;
				}
			}
			if (num == 0)
			{
				return false;
			}
			throw LZ4Stream.EndOfStream();
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000EAB7C File Offset: 0x000E8D7C
		private ulong ReadVarInt()
		{
			ulong result;
			if (!this.TryReadVarInt(out result))
			{
				throw LZ4Stream.EndOfStream();
			}
			return result;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000EAB9C File Offset: 0x000E8D9C
		private int ReadBlock(byte[] buffer, int offset, int length)
		{
			int num = 0;
			while (length > 0)
			{
				int num2 = this._innerStream.Read(buffer, offset, length);
				if (num2 == 0)
				{
					break;
				}
				offset += num2;
				length -= num2;
				num += num2;
			}
			return num;
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000EABD4 File Offset: 0x000E8DD4
		private void WriteVarInt(ulong value)
		{
			byte[] array = new byte[1];
			do
			{
				byte b = (byte)(value & 127UL);
				value >>= 7;
				array[0] = (b | ((value == 0UL) ? 0 : 128));
				this._innerStream.Write(array, 0, 1);
			}
			while (value != 0UL);
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000EAC18 File Offset: 0x000E8E18
		private void FlushCurrentChunk()
		{
			if (this._bufferOffset <= 0)
			{
				return;
			}
			byte[] array = new byte[this._bufferOffset];
			int num = this._highCompression ? LZ4Codec.EncodeHC(this._buffer, 0, this._bufferOffset, array, 0, this._bufferOffset) : LZ4Codec.Encode(this._buffer, 0, this._bufferOffset, array, 0, this._bufferOffset);
			if (num <= 0 || num >= this._bufferOffset)
			{
				array = this._buffer;
				num = this._bufferOffset;
			}
			bool flag = num < this._bufferOffset;
			LZ4Stream.ChunkFlags chunkFlags = LZ4Stream.ChunkFlags.None;
			if (flag)
			{
				chunkFlags |= LZ4Stream.ChunkFlags.Compressed;
			}
			if (this._highCompression)
			{
				chunkFlags |= LZ4Stream.ChunkFlags.HighCompression;
			}
			this.WriteVarInt((ulong)((long)chunkFlags));
			this.WriteVarInt((ulong)((long)this._bufferOffset));
			if (flag)
			{
				this.WriteVarInt((ulong)((long)num));
			}
			this._innerStream.Write(array, 0, num);
			this._bufferOffset = 0;
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000EACE8 File Offset: 0x000E8EE8
		private bool AcquireNextChunk()
		{
			ulong num;
			while (this.TryReadVarInt(out num))
			{
				LZ4Stream.ChunkFlags chunkFlags = (LZ4Stream.ChunkFlags)num;
				bool flag = (chunkFlags & LZ4Stream.ChunkFlags.Compressed) > LZ4Stream.ChunkFlags.None;
				int num2 = (int)this.ReadVarInt();
				int num3 = flag ? ((int)this.ReadVarInt()) : num2;
				if (num3 > num2)
				{
					throw LZ4Stream.EndOfStream();
				}
				byte[] array = new byte[num3];
				if (this.ReadBlock(array, 0, num3) != num3)
				{
					throw LZ4Stream.EndOfStream();
				}
				if (!flag)
				{
					this._buffer = array;
					this._bufferLength = num3;
				}
				else
				{
					if (this._buffer == null || this._buffer.Length < num2)
					{
						this._buffer = new byte[num2];
					}
					if (chunkFlags >> 2 != LZ4Stream.ChunkFlags.None)
					{
						throw new NotSupportedException("Chunks with multiple passes are not supported.");
					}
					LZ4Codec.Decode(array, 0, num3, this._buffer, 0, num2, true);
					this._bufferLength = num2;
				}
				this._bufferOffset = 0;
				if (this._bufferLength != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002FFC RID: 12284 RVA: 0x000EADB4 File Offset: 0x000E8FB4
		public override bool CanRead
		{
			get
			{
				return this._compressionMode == LZ4StreamMode.Decompress;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002FFD RID: 12285 RVA: 0x000EADBF File Offset: 0x000E8FBF
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06002FFE RID: 12286 RVA: 0x000EADC2 File Offset: 0x000E8FC2
		public override bool CanWrite
		{
			get
			{
				return this._compressionMode == LZ4StreamMode.Compress;
			}
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000EADCD File Offset: 0x000E8FCD
		public override void Flush()
		{
			if (this._bufferOffset > 0 && this.CanWrite)
			{
				this.FlushCurrentChunk();
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06003000 RID: 12288 RVA: 0x000EADE6 File Offset: 0x000E8FE6
		public override long Length
		{
			get
			{
				return -1L;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06003001 RID: 12289 RVA: 0x000EADEA File Offset: 0x000E8FEA
		// (set) Token: 0x06003002 RID: 12290 RVA: 0x000EADEE File Offset: 0x000E8FEE
		public override long Position
		{
			get
			{
				return -1L;
			}
			set
			{
				throw LZ4Stream.NotSupported("SetPosition");
			}
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000EADFC File Offset: 0x000E8FFC
		public override int ReadByte()
		{
			if (!this.CanRead)
			{
				throw LZ4Stream.NotSupported("Read");
			}
			if (this._bufferOffset >= this._bufferLength && !this.AcquireNextChunk())
			{
				return -1;
			}
			byte[] buffer = this._buffer;
			int bufferOffset = this._bufferOffset;
			this._bufferOffset = bufferOffset + 1;
			return buffer[bufferOffset];
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000EAE4C File Offset: 0x000E904C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw LZ4Stream.NotSupported("Read");
			}
			int num = 0;
			while (count > 0)
			{
				int num2 = Math.Min(count, this._bufferLength - this._bufferOffset);
				if (num2 > 0)
				{
					Buffer.BlockCopy(this._buffer, this._bufferOffset, buffer, offset, num2);
					this._bufferOffset += num2;
					num += num2;
					if (this._interactiveRead)
					{
						break;
					}
					offset += num2;
					count -= num2;
				}
				else if (!this.AcquireNextChunk())
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000EAECF File Offset: 0x000E90CF
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw LZ4Stream.NotSupported("Seek");
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x000EAEDB File Offset: 0x000E90DB
		public override void SetLength(long value)
		{
			throw LZ4Stream.NotSupported("SetLength");
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x000EAEE8 File Offset: 0x000E90E8
		public override void WriteByte(byte value)
		{
			if (!this.CanWrite)
			{
				throw LZ4Stream.NotSupported("Write");
			}
			if (this._buffer == null)
			{
				this._buffer = new byte[this._blockSize];
				this._bufferLength = this._blockSize;
				this._bufferOffset = 0;
			}
			if (this._bufferOffset >= this._bufferLength)
			{
				this.FlushCurrentChunk();
			}
			byte[] buffer = this._buffer;
			int bufferOffset = this._bufferOffset;
			this._bufferOffset = bufferOffset + 1;
			buffer[bufferOffset] = value;
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x000EAF64 File Offset: 0x000E9164
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw LZ4Stream.NotSupported("Write");
			}
			if (this._buffer == null)
			{
				this._buffer = new byte[this._blockSize];
				this._bufferLength = this._blockSize;
				this._bufferOffset = 0;
			}
			while (count > 0)
			{
				int num = Math.Min(count, this._bufferLength - this._bufferOffset);
				if (num > 0)
				{
					Buffer.BlockCopy(buffer, offset, this._buffer, this._bufferOffset, num);
					offset += num;
					count -= num;
					this._bufferOffset += num;
				}
				else
				{
					this.FlushCurrentChunk();
				}
			}
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x000EB002 File Offset: 0x000E9202
		protected override void Dispose(bool disposing)
		{
			this.Flush();
			if (!this._isolateInnerStream)
			{
				this._innerStream.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x000EB024 File Offset: 0x000E9224
		[Obsolete("This constructor is obsolete")]
		public LZ4Stream(Stream innerStream, CompressionMode compressionMode, bool highCompression, int blockSize = 1048576, bool interactiveRead = false) : this(innerStream, LZ4Stream.ToLZ4StreamMode(compressionMode), LZ4Stream.CombineLZ4Flags(highCompression, interactiveRead), blockSize)
		{
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000EB03D File Offset: 0x000E923D
		public LZ4Stream(Stream innerStream, CompressionMode compressionMode, LZ4StreamFlags compressionFlags = LZ4StreamFlags.None, int blockSize = 1048576) : this(innerStream, LZ4Stream.ToLZ4StreamMode(compressionMode), compressionFlags, blockSize)
		{
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x000EB04F File Offset: 0x000E924F
		private static LZ4StreamMode ToLZ4StreamMode(CompressionMode compressionMode)
		{
			if (compressionMode == CompressionMode.Decompress)
			{
				return LZ4StreamMode.Decompress;
			}
			if (compressionMode == CompressionMode.Compress)
			{
				return LZ4StreamMode.Compress;
			}
			throw new ArgumentException(string.Format("Unhandled compression mode: {0}", compressionMode));
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000EB074 File Offset: 0x000E9274
		private static LZ4StreamFlags CombineLZ4Flags(bool highCompression, bool interactiveRead)
		{
			LZ4StreamFlags lz4StreamFlags = LZ4StreamFlags.None;
			if (highCompression)
			{
				lz4StreamFlags |= LZ4StreamFlags.HighCompression;
			}
			if (interactiveRead)
			{
				lz4StreamFlags |= LZ4StreamFlags.InteractiveRead;
			}
			return lz4StreamFlags;
		}

		// Token: 0x04002F83 RID: 12163
		private readonly Stream _innerStream;

		// Token: 0x04002F84 RID: 12164
		private readonly LZ4StreamMode _compressionMode;

		// Token: 0x04002F85 RID: 12165
		private readonly bool _highCompression;

		// Token: 0x04002F86 RID: 12166
		private readonly bool _interactiveRead;

		// Token: 0x04002F87 RID: 12167
		private readonly bool _isolateInnerStream;

		// Token: 0x04002F88 RID: 12168
		private readonly int _blockSize;

		// Token: 0x04002F89 RID: 12169
		private byte[] _buffer;

		// Token: 0x04002F8A RID: 12170
		private int _bufferLength;

		// Token: 0x04002F8B RID: 12171
		private int _bufferOffset;

		// Token: 0x020012DA RID: 4826
		[Flags]
		public enum ChunkFlags
		{
			// Token: 0x0400972E RID: 38702
			None = 0,
			// Token: 0x0400972F RID: 38703
			Compressed = 1,
			// Token: 0x04009730 RID: 38704
			HighCompression = 2,
			// Token: 0x04009731 RID: 38705
			Passes = 28
		}
	}
}

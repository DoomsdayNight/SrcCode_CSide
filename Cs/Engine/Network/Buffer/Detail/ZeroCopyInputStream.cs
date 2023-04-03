using System;
using System.IO;

namespace Cs.Engine.Network.Buffer.Detail
{
	// Token: 0x020010B4 RID: 4276
	internal sealed class ZeroCopyInputStream : Stream
	{
		// Token: 0x06009C8A RID: 40074 RVA: 0x00335F78 File Offset: 0x00334178
		public ZeroCopyInputStream(ZeroCopyBuffer buffer)
		{
			this.tailBuffers = buffer.GetView();
			this.totalSize = buffer.CalcTotalSize();
			this.Length = (long)this.totalSize;
		}

		// Token: 0x06009C8B RID: 40075 RVA: 0x00335FA8 File Offset: 0x003341A8
		public ZeroCopyInputStream(ZeroCopyBuffer buffer, int fixedOffset)
		{
			this.tailBuffers = buffer.GetView();
			this.totalSize = buffer.CalcTotalSize();
			if (fixedOffset < 0 || fixedOffset > this.totalSize)
			{
				throw new Exception(string.Format("[ZeroCopyInputStream] invalid offset. fixed:{0} length:{1}", fixedOffset, this.Length));
			}
			this.fixedOffset = fixedOffset;
			this.Length = (long)(this.totalSize - this.fixedOffset);
			this.ResetOffset(this.fixedOffset);
		}

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x06009C8C RID: 40076 RVA: 0x00336028 File Offset: 0x00334228
		public override bool CanRead
		{
			get
			{
				return this.IsReadable();
			}
		}

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x06009C8D RID: 40077 RVA: 0x00336030 File Offset: 0x00334230
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x06009C8E RID: 40078 RVA: 0x00336033 File Offset: 0x00334233
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x06009C8F RID: 40079 RVA: 0x00336036 File Offset: 0x00334236
		public override long Length { get; }

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x06009C90 RID: 40080 RVA: 0x0033603E File Offset: 0x0033423E
		// (set) Token: 0x06009C91 RID: 40081 RVA: 0x0033604E File Offset: 0x0033424E
		public override long Position
		{
			get
			{
				return (long)(this.CalcCurrentFullOffset() - this.fixedOffset);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06009C92 RID: 40082 RVA: 0x00336055 File Offset: 0x00334255
		public override void Flush()
		{
		}

		// Token: 0x06009C93 RID: 40083 RVA: 0x00336058 File Offset: 0x00334258
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = 0;
			while (this.CanRead)
			{
				TailBuffer tailBuffer = this.tailBuffers[this.index];
				if (this.offset > tailBuffer.Offset)
				{
					throw new Exception(string.Format("memory offset error. this.offset:{0} buffer offset:{1}", this.offset, tailBuffer.Offset));
				}
				if (this.offset == tailBuffer.Offset)
				{
					this.index++;
					this.offset = 0;
				}
				else
				{
					int num2 = Math.Min(count - num, tailBuffer.Offset - this.offset);
					Buffer.BlockCopy(tailBuffer.Data, this.offset, buffer, offset + num, num2);
					this.offset += num2;
					num += num2;
					if (num > count)
					{
						throw new Exception(string.Format("memory offset error. transferred:{0} count:{1}", num, count));
					}
					if (num == count)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06009C94 RID: 40084 RVA: 0x00336144 File Offset: 0x00334344
		public override long Seek(long offset, SeekOrigin origin)
		{
			int num = 0;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = this.fixedOffset + (int)offset;
				break;
			case SeekOrigin.Current:
				num = this.CalcCurrentFullOffset() + (int)offset;
				break;
			case SeekOrigin.End:
				num = (int)(this.Length - offset);
				break;
			}
			this.ResetOffset(num);
			return (long)num;
		}

		// Token: 0x06009C95 RID: 40085 RVA: 0x00336192 File Offset: 0x00334392
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009C96 RID: 40086 RVA: 0x00336199 File Offset: 0x00334399
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009C97 RID: 40087 RVA: 0x003361A0 File Offset: 0x003343A0
		private void ResetOffset(int offset)
		{
			if (offset > this.totalSize)
			{
				throw new Exception(string.Format("[ZeroCopyInputStream] invalid offset:{0} length:{1}", offset, this.totalSize));
			}
			this.index = 0;
			this.offset = offset;
			for (int i = 0; i < this.tailBuffers.Length; i++)
			{
				TailBuffer tailBuffer = this.tailBuffers[i];
				if (this.offset < tailBuffer.Offset)
				{
					break;
				}
				this.index++;
				this.offset -= tailBuffer.Offset;
			}
			if (this.index >= this.tailBuffers.Length && this.offset > 0)
			{
				throw new Exception(string.Format("index:{0} buffers:{1} offset:{2}", this.index, this.tailBuffers.Length, this.offset));
			}
		}

		// Token: 0x06009C98 RID: 40088 RVA: 0x0033627C File Offset: 0x0033447C
		private bool IsReadable()
		{
			if (this.tailBuffers.Length == 0)
			{
				return false;
			}
			if (this.index < this.tailBuffers.Length)
			{
				return this.index < this.tailBuffers.Length || this.offset < this.tailBuffers[this.index].Offset;
			}
			if (this.offset == 0)
			{
				return false;
			}
			throw new Exception(string.Format("invalid index:{0} #buffer:{1} offset:{2}", this.index, this.tailBuffers.Length, this.offset));
		}

		// Token: 0x06009C99 RID: 40089 RVA: 0x00336310 File Offset: 0x00334510
		private int CalcCurrentFullOffset()
		{
			int num = this.offset;
			for (int i = 0; i < this.index; i++)
			{
				num += this.tailBuffers[i].Offset;
			}
			return num;
		}

		// Token: 0x0400906B RID: 36971
		private readonly TailBuffer[] tailBuffers;

		// Token: 0x0400906C RID: 36972
		private readonly int totalSize;

		// Token: 0x0400906D RID: 36973
		private readonly int fixedOffset;

		// Token: 0x0400906E RID: 36974
		private int index;

		// Token: 0x0400906F RID: 36975
		private int offset;
	}
}

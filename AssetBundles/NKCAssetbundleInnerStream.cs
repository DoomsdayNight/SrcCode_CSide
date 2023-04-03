using System;
using System.IO;
using Cs.Engine.Network.Buffer.Detail;

namespace AssetBundles
{
	// Token: 0x0200005A RID: 90
	public class NKCAssetbundleInnerStream : Stream
	{
		// Token: 0x060002BA RID: 698 RVA: 0x0000C0E8 File Offset: 0x0000A2E8
		public static string GetJarRelativePath(string path)
		{
			return path.Substring(path.LastIndexOf("!/assets/", StringComparison.OrdinalIgnoreCase) + "!/assets/".Length);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000C108 File Offset: 0x0000A308
		public NKCAssetbundleInnerStream(string path)
		{
			if (path.Contains("jar:"))
			{
				path = NKCAssetbundleInnerStream.GetJarRelativePath(path);
			}
			this.betterStream = BetterStreamingAssets.OpenRead(path);
			this.decryptSize = Math.Min(this.Length, 212L);
			this.betterStream.Read(this.decryptedArray, 0, (int)this.decryptSize);
			Crypto.Encrypt(this.decryptedArray, (int)this.decryptSize, AssetBundleManager.GetMaskList(path, false));
			this.Seek(0L, SeekOrigin.Begin);
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000C1A2 File Offset: 0x0000A3A2
		public override bool CanRead
		{
			get
			{
				return this.betterStream.CanRead;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000C1AF File Offset: 0x0000A3AF
		public override bool CanSeek
		{
			get
			{
				return this.betterStream.CanSeek;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000C1BC File Offset: 0x0000A3BC
		public override bool CanWrite
		{
			get
			{
				return this.betterStream.CanWrite;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000C1C9 File Offset: 0x0000A3C9
		public override long Length
		{
			get
			{
				return this.betterStream.Length;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000C1D6 File Offset: 0x0000A3D6
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000C1E3 File Offset: 0x0000A3E3
		public override long Position
		{
			get
			{
				return this.betterStream.Position;
			}
			set
			{
				this.betterStream.Position = value;
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000C1F1 File Offset: 0x0000A3F1
		public override void Flush()
		{
			this.betterStream.Flush();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000C200 File Offset: 0x0000A400
		public override int Read(byte[] buffer, int offset, int count)
		{
			long position = this.Position;
			int result = this.betterStream.Read(buffer, offset, count);
			if (position < this.decryptSize)
			{
				if (position + (long)count < this.decryptSize)
				{
					Array.Copy(this.decryptedArray, position, buffer, (long)offset, (long)count);
					return result;
				}
				int num = (int)(this.decryptSize - position);
				Array.Copy(this.decryptedArray, position, buffer, (long)offset, (long)num);
			}
			return result;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000C264 File Offset: 0x0000A464
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.betterStream.Seek(offset, origin);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000C273 File Offset: 0x0000A473
		public override void SetLength(long value)
		{
			this.betterStream.SetLength(value);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000C281 File Offset: 0x0000A481
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.betterStream.Write(buffer, offset, count);
		}

		// Token: 0x040001ED RID: 493
		private Stream betterStream;

		// Token: 0x040001EE RID: 494
		private byte[] decryptedArray = new byte[212L];

		// Token: 0x040001EF RID: 495
		private long decryptSize;
	}
}

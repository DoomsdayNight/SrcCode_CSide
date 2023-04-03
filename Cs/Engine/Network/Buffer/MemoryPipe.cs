using System;
using System.IO;
using Cs.Engine.Network.Buffer.Detail;

namespace Cs.Engine.Network.Buffer
{
	// Token: 0x020010AE RID: 4270
	internal sealed class MemoryPipe : IDisposable
	{
		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x06009C4A RID: 40010 RVA: 0x003353E0 File Offset: 0x003335E0
		public long Length
		{
			get
			{
				return (long)this.buffer.CalcTotalSize() - this.offset;
			}
		}

		// Token: 0x06009C4B RID: 40011 RVA: 0x003353F5 File Offset: 0x003335F5
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			this.buffer.Hold().Dispose();
		}

		// Token: 0x06009C4C RID: 40012 RVA: 0x00335417 File Offset: 0x00333617
		public Stream GetReadStream()
		{
			return new ZeroCopyInputStream(this.buffer, (int)this.offset);
		}

		// Token: 0x06009C4D RID: 40013 RVA: 0x0033542B File Offset: 0x0033362B
		public void Write(byte[] data, int offset, int count)
		{
			this.buffer.Write(data, offset, count);
		}

		// Token: 0x06009C4E RID: 40014 RVA: 0x0033543C File Offset: 0x0033363C
		public void Adavnce(long size)
		{
			long num = (long)this.buffer.CalcTotalSize();
			if (size > num)
			{
				throw new ArgumentException(string.Format("[MemoryPipe] invalid advance size:{0} totalSize:{1}", size, num));
			}
			this.offset += size;
			while (this.buffer.SegmentCount > 1)
			{
				TailBuffer tailBuffer = this.buffer.Peek();
				if (this.offset < (long)tailBuffer.Offset)
				{
					return;
				}
				this.offset -= (long)tailBuffer.Offset;
				this.buffer.PopHeadSegment();
			}
		}

		// Token: 0x0400905D RID: 36957
		private readonly ZeroCopyBuffer buffer = new ZeroCopyBuffer();

		// Token: 0x0400905E RID: 36958
		private long offset;

		// Token: 0x0400905F RID: 36959
		private bool disposed;
	}
}

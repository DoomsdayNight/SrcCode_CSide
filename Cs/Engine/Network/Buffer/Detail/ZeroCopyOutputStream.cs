using System;
using System.IO;

namespace Cs.Engine.Network.Buffer.Detail
{
	// Token: 0x020010B5 RID: 4277
	internal sealed class ZeroCopyOutputStream : Stream
	{
		// Token: 0x06009C9A RID: 40090 RVA: 0x00336346 File Offset: 0x00334546
		public ZeroCopyOutputStream(ZeroCopyBuffer buffer)
		{
			this.buffer = buffer;
		}

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x06009C9B RID: 40091 RVA: 0x00336355 File Offset: 0x00334555
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x06009C9C RID: 40092 RVA: 0x00336358 File Offset: 0x00334558
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x06009C9D RID: 40093 RVA: 0x0033635B File Offset: 0x0033455B
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x06009C9E RID: 40094 RVA: 0x0033635E File Offset: 0x0033455E
		public override long Length
		{
			get
			{
				return (long)this.buffer.CalcTotalSize();
			}
		}

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x06009C9F RID: 40095 RVA: 0x0033636C File Offset: 0x0033456C
		// (set) Token: 0x06009CA0 RID: 40096 RVA: 0x00336373 File Offset: 0x00334573
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06009CA1 RID: 40097 RVA: 0x0033637A File Offset: 0x0033457A
		public override void Flush()
		{
		}

		// Token: 0x06009CA2 RID: 40098 RVA: 0x0033637C File Offset: 0x0033457C
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009CA3 RID: 40099 RVA: 0x00336383 File Offset: 0x00334583
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009CA4 RID: 40100 RVA: 0x0033638A File Offset: 0x0033458A
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009CA5 RID: 40101 RVA: 0x00336391 File Offset: 0x00334591
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.buffer.Write(buffer, offset, count);
		}

		// Token: 0x04009071 RID: 36977
		private readonly ZeroCopyBuffer buffer;
	}
}

using System;
using System.IO;

namespace Cs.Engine.Network.Buffer.Detail
{
	// Token: 0x020010B2 RID: 4274
	internal sealed class SendStream : Stream
	{
		// Token: 0x06009C74 RID: 40052 RVA: 0x00335E20 File Offset: 0x00334020
		public SendStream(SendBuffer sendBuffer)
		{
			this.buffer = sendBuffer;
		}

		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x06009C75 RID: 40053 RVA: 0x00335E2F File Offset: 0x0033402F
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x06009C76 RID: 40054 RVA: 0x00335E32 File Offset: 0x00334032
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06009C77 RID: 40055 RVA: 0x00335E35 File Offset: 0x00334035
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x06009C78 RID: 40056 RVA: 0x00335E38 File Offset: 0x00334038
		public override long Length
		{
			get
			{
				return (long)this.buffer.CalcTotalSize();
			}
		}

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x06009C79 RID: 40057 RVA: 0x00335E46 File Offset: 0x00334046
		// (set) Token: 0x06009C7A RID: 40058 RVA: 0x00335E4D File Offset: 0x0033404D
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

		// Token: 0x06009C7B RID: 40059 RVA: 0x00335E54 File Offset: 0x00334054
		public override void Flush()
		{
		}

		// Token: 0x06009C7C RID: 40060 RVA: 0x00335E56 File Offset: 0x00334056
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009C7D RID: 40061 RVA: 0x00335E5D File Offset: 0x0033405D
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009C7E RID: 40062 RVA: 0x00335E64 File Offset: 0x00334064
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009C7F RID: 40063 RVA: 0x00335E6B File Offset: 0x0033406B
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.buffer.Write(buffer, offset, count);
		}

		// Token: 0x04009066 RID: 36966
		private readonly SendBuffer buffer;
	}
}

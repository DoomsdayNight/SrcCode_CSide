using System;
using Cs.Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000DE7 RID: 3559
	public sealed class NKMOfficePostState : ISerializable
	{
		// Token: 0x060096C2 RID: 38594 RVA: 0x0032BED5 File Offset: 0x0032A0D5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.broadcastExecution);
			stream.PutOrGet(ref this.sendCount);
			stream.PutOrGet(ref this.recvCount);
			stream.PutOrGet(ref this.nextResetDate);
		}

		// Token: 0x040088B7 RID: 34999
		public bool broadcastExecution;

		// Token: 0x040088B8 RID: 35000
		public int sendCount;

		// Token: 0x040088B9 RID: 35001
		public int recvCount;

		// Token: 0x040088BA RID: 35002
		public DateTime nextResetDate;
	}
}

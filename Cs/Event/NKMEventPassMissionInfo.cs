using System;
using Cs.Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F71 RID: 3953
	public sealed class NKMEventPassMissionInfo : ISerializable
	{
		// Token: 0x060099BE RID: 39358 RVA: 0x0033075A File Offset: 0x0032E95A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.missionId);
			stream.PutOrGet(ref this.slotIndex);
			stream.PutOrGet(ref this.retryCount);
		}

		// Token: 0x04008CD4 RID: 36052
		public int missionId;

		// Token: 0x04008CD5 RID: 36053
		public int slotIndex;

		// Token: 0x04008CD6 RID: 36054
		public int retryCount;
	}
}

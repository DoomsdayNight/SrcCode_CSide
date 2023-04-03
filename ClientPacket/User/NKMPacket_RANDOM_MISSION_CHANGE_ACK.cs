using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CC5 RID: 3269
	[PacketId(ClientPacketId.kNKMPacket_RANDOM_MISSION_CHANGE_ACK)]
	public sealed class NKMPacket_RANDOM_MISSION_CHANGE_ACK : ISerializable
	{
		// Token: 0x06009487 RID: 38023 RVA: 0x00328D91 File Offset: 0x00326F91
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.beforeGroupId);
			stream.PutOrGet<NKMMissionData>(ref this.afterMissionData);
			stream.PutOrGet(ref this.remainRefreshCount);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItemData);
		}

		// Token: 0x040085F9 RID: 34297
		public NKM_ERROR_CODE errorCode;

		// Token: 0x040085FA RID: 34298
		public int beforeGroupId;

		// Token: 0x040085FB RID: 34299
		public NKMMissionData afterMissionData;

		// Token: 0x040085FC RID: 34300
		public int remainRefreshCount;

		// Token: 0x040085FD RID: 34301
		public NKMItemMiscData costItemData;
	}
}

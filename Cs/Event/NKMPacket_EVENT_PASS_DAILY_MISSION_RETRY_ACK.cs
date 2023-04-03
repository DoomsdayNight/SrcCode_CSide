using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F83 RID: 3971
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_DAILY_MISSION_RETRY_ACK)]
	public sealed class NKMPacket_EVENT_PASS_DAILY_MISSION_RETRY_ACK : ISerializable
	{
		// Token: 0x060099E2 RID: 39394 RVA: 0x00330A29 File Offset: 0x0032EC29
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMEventPassMissionInfo>(ref this.missionInfo);
			stream.PutOrGet<NKMItemMiscData>(ref this.costItems);
		}

		// Token: 0x04008D00 RID: 36096
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D01 RID: 36097
		public NKMEventPassMissionInfo missionInfo = new NKMEventPassMissionInfo();

		// Token: 0x04008D02 RID: 36098
		public List<NKMItemMiscData> costItems = new List<NKMItemMiscData>();
	}
}

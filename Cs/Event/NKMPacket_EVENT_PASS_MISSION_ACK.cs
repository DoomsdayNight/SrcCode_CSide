using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F7E RID: 3966
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_MISSION_ACK)]
	public sealed class NKMPacket_EVENT_PASS_MISSION_ACK : ISerializable
	{
		// Token: 0x060099D8 RID: 39384 RVA: 0x00330973 File Offset: 0x0032EB73
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.isFinalMissionCompleted);
			stream.PutOrGetEnum<EventPassMissionType>(ref this.missionType);
			stream.PutOrGet<NKMEventPassMissionInfo>(ref this.missionInfoList);
			stream.PutOrGet(ref this.nextResetDate);
		}

		// Token: 0x04008CF5 RID: 36085
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008CF6 RID: 36086
		public bool isFinalMissionCompleted;

		// Token: 0x04008CF7 RID: 36087
		public EventPassMissionType missionType;

		// Token: 0x04008CF8 RID: 36088
		public List<NKMEventPassMissionInfo> missionInfoList;

		// Token: 0x04008CF9 RID: 36089
		public DateTime nextResetDate;
	}
}

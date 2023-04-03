using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F81 RID: 3969
	[PacketId(ClientPacketId.kNKMPacket_EVENT_PASS_FINAL_MISSION_COMPLETE_ACK)]
	public sealed class NKMPacket_EVENT_PASS_FINAL_MISSION_COMPLETE_ACK : ISerializable
	{
		// Token: 0x060099DE RID: 39390 RVA: 0x003309E5 File Offset: 0x0032EBE5
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.totalExp);
			stream.PutOrGetEnum<EventPassMissionType>(ref this.missionType);
		}

		// Token: 0x04008CFC RID: 36092
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008CFD RID: 36093
		public int totalExp;

		// Token: 0x04008CFE RID: 36094
		public EventPassMissionType missionType;
	}
}

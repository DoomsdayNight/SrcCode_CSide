using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000F97 RID: 3991
	[PacketId(ClientPacketId.kNKMPacket_KAKAO_MISSION_REFRESH_STATE_ACK)]
	public sealed class NKMPacket_KAKAO_MISSION_REFRESH_STATE_ACK : ISerializable
	{
		// Token: 0x06009A06 RID: 39430 RVA: 0x00330D48 File Offset: 0x0032EF48
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<KakaoMissionData>(ref this.missionData);
		}

		// Token: 0x04008D34 RID: 36148
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D35 RID: 36149
		public KakaoMissionData missionData = new KakaoMissionData();
	}
}

using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F4F RID: 3919
	[PacketId(ClientPacketId.kNKMPacket_GAME_DEV_RESPAWN_ACK)]
	public sealed class NKMPacket_GAME_DEV_RESPAWN_ACK : ISerializable
	{
		// Token: 0x0600997E RID: 39294 RVA: 0x0033017C File Offset: 0x0032E37C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMUnitData>(ref this.unitData);
			stream.PutOrGet<NKMDynamicRespawnUnitData>(ref this.dynamicRespawnUnitDataTeamA);
			stream.PutOrGet<NKMDynamicRespawnUnitData>(ref this.dynamicRespawnUnitDataTeamB);
			stream.PutOrGetEnum<NKM_TEAM_TYPE>(ref this.teamType);
		}

		// Token: 0x04008C83 RID: 35971
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008C84 RID: 35972
		public NKMUnitData unitData;

		// Token: 0x04008C85 RID: 35973
		public List<NKMDynamicRespawnUnitData> dynamicRespawnUnitDataTeamA = new List<NKMDynamicRespawnUnitData>();

		// Token: 0x04008C86 RID: 35974
		public List<NKMDynamicRespawnUnitData> dynamicRespawnUnitDataTeamB = new List<NKMDynamicRespawnUnitData>();

		// Token: 0x04008C87 RID: 35975
		public NKM_TEAM_TYPE teamType;
	}
}

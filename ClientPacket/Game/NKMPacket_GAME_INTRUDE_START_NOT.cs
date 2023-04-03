using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F36 RID: 3894
	[PacketId(ClientPacketId.kNKMPacket_GAME_INTRUDE_START_NOT)]
	public sealed class NKMPacket_GAME_INTRUDE_START_NOT : ISerializable
	{
		// Token: 0x0600994C RID: 39244 RVA: 0x0032FC30 File Offset: 0x0032DE30
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.gameTime);
			stream.PutOrGet(ref this.absoluteGameTime);
			stream.PutOrGet<NKMGameSyncDataPack>(ref this.gameSyncDataPack);
			stream.PutOrGet<NKMGameTeamDeckData>(ref this.gameTeamDeckDataA);
			stream.PutOrGet<NKMGameTeamDeckData>(ref this.gameTeamDeckDataB);
			stream.PutOrGet(ref this.usedRespawnCost);
			stream.PutOrGet(ref this.respawnCount);
			stream.PutOrGet(ref this.mainShipAStateCoolTimeMap);
			stream.PutOrGet(ref this.mainShipBStateCoolTimeMap);
		}

		// Token: 0x04008C32 RID: 35890
		public float gameTime;

		// Token: 0x04008C33 RID: 35891
		public float absoluteGameTime;

		// Token: 0x04008C34 RID: 35892
		public NKMGameSyncDataPack gameSyncDataPack;

		// Token: 0x04008C35 RID: 35893
		public NKMGameTeamDeckData gameTeamDeckDataA;

		// Token: 0x04008C36 RID: 35894
		public NKMGameTeamDeckData gameTeamDeckDataB;

		// Token: 0x04008C37 RID: 35895
		public float usedRespawnCost;

		// Token: 0x04008C38 RID: 35896
		public float respawnCount;

		// Token: 0x04008C39 RID: 35897
		public Dictionary<int, float> mainShipAStateCoolTimeMap = new Dictionary<int, float>();

		// Token: 0x04008C3A RID: 35898
		public Dictionary<int, float> mainShipBStateCoolTimeMap = new Dictionary<int, float>();
	}
}

using System;
using Cs.Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C92 RID: 3218
	public sealed class WarfareGameSyncData : ISerializable
	{
		// Token: 0x06009421 RID: 37921 RVA: 0x00328144 File Offset: 0x00326344
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_WARFARE_GAME_STATE>(ref this.warfareGameState);
			stream.PutOrGet(ref this.isTurnA);
			stream.PutOrGet(ref this.isWinTeamA);
			stream.PutOrGet(ref this.turnCount);
			stream.PutOrGet(ref this.firstAttackCount);
			stream.PutOrGet(ref this.battleAllyUid);
			stream.PutOrGet(ref this.battleMonsterUid);
			stream.PutOrGet(ref this.holdCount);
			stream.PutOrGet(ref this.containerCount);
			stream.PutOrGet(ref this.alliesKillCount);
			stream.PutOrGet(ref this.enemiesKillCount);
			stream.PutOrGet(ref this.targetKillCount);
			stream.PutOrGet(ref this.assistCount);
			stream.PutOrGet(ref this.supplyUseCount);
		}

		// Token: 0x04008541 RID: 34113
		public NKM_WARFARE_GAME_STATE warfareGameState;

		// Token: 0x04008542 RID: 34114
		public bool isTurnA;

		// Token: 0x04008543 RID: 34115
		public bool isWinTeamA;

		// Token: 0x04008544 RID: 34116
		public int turnCount;

		// Token: 0x04008545 RID: 34117
		public int firstAttackCount;

		// Token: 0x04008546 RID: 34118
		public int battleAllyUid;

		// Token: 0x04008547 RID: 34119
		public int battleMonsterUid;

		// Token: 0x04008548 RID: 34120
		public int holdCount;

		// Token: 0x04008549 RID: 34121
		public short containerCount;

		// Token: 0x0400854A RID: 34122
		public byte alliesKillCount;

		// Token: 0x0400854B RID: 34123
		public byte enemiesKillCount;

		// Token: 0x0400854C RID: 34124
		public byte targetKillCount;

		// Token: 0x0400854D RID: 34125
		public byte assistCount;

		// Token: 0x0400854E RID: 34126
		public byte supplyUseCount;
	}
}

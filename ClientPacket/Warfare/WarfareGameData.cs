using System;
using System.Collections.Generic;
using ClientPacket.Community;
using Cs.Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C96 RID: 3222
	public sealed class WarfareGameData : ISerializable
	{
		// Token: 0x06009429 RID: 37929 RVA: 0x00328394 File Offset: 0x00326594
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_WARFARE_GAME_STATE>(ref this.warfareGameState);
			stream.PutOrGet(ref this.warfareTempletID);
			stream.PutOrGet<WarfareTileData>(ref this.warfareTileDataList);
			stream.PutOrGet<WarfareTeamData>(ref this.warfareTeamDataA);
			stream.PutOrGet<WarfareTeamData>(ref this.warfareTeamDataB);
			stream.PutOrGet(ref this.isTurnA);
			stream.PutOrGet(ref this.turnCount);
			stream.PutOrGet(ref this.firstAttackCount);
			stream.PutOrGet(ref this.battleAllyUid);
			stream.PutOrGet(ref this.battleMonsterUid);
			stream.PutOrGet(ref this.isWinTeamA);
			stream.PutOrGet(ref this.expireTimeTick);
			stream.PutOrGet(ref this.holdCount);
			stream.PutOrGet(ref this.containerCount);
			stream.PutOrGet(ref this.flagshipDeckIndex);
			stream.PutOrGet(ref this.alliesKillCount);
			stream.PutOrGet(ref this.enemiesKillCount);
			stream.PutOrGet(ref this.targetKillCount);
			stream.PutOrGet(ref this.assistCount);
			stream.PutOrGet(ref this.supplyUseCount);
			stream.PutOrGet<WarfareSupporterListData>(ref this.supportUnitData);
			stream.PutOrGet(ref this.rewardMultiply);
		}

		// Token: 0x04008564 RID: 34148
		public NKM_WARFARE_GAME_STATE warfareGameState;

		// Token: 0x04008565 RID: 34149
		public int warfareTempletID;

		// Token: 0x04008566 RID: 34150
		public List<WarfareTileData> warfareTileDataList = new List<WarfareTileData>();

		// Token: 0x04008567 RID: 34151
		public WarfareTeamData warfareTeamDataA = new WarfareTeamData();

		// Token: 0x04008568 RID: 34152
		public WarfareTeamData warfareTeamDataB = new WarfareTeamData();

		// Token: 0x04008569 RID: 34153
		public bool isTurnA;

		// Token: 0x0400856A RID: 34154
		public int turnCount;

		// Token: 0x0400856B RID: 34155
		public int firstAttackCount;

		// Token: 0x0400856C RID: 34156
		public int battleAllyUid;

		// Token: 0x0400856D RID: 34157
		public int battleMonsterUid;

		// Token: 0x0400856E RID: 34158
		public bool isWinTeamA;

		// Token: 0x0400856F RID: 34159
		public long expireTimeTick;

		// Token: 0x04008570 RID: 34160
		public int holdCount;

		// Token: 0x04008571 RID: 34161
		public short containerCount;

		// Token: 0x04008572 RID: 34162
		public byte flagshipDeckIndex;

		// Token: 0x04008573 RID: 34163
		public byte alliesKillCount;

		// Token: 0x04008574 RID: 34164
		public byte enemiesKillCount;

		// Token: 0x04008575 RID: 34165
		public byte targetKillCount;

		// Token: 0x04008576 RID: 34166
		public byte assistCount;

		// Token: 0x04008577 RID: 34167
		public byte supplyUseCount;

		// Token: 0x04008578 RID: 34168
		public WarfareSupporterListData supportUnitData = new WarfareSupporterListData();

		// Token: 0x04008579 RID: 34169
		public int rewardMultiply;
	}
}

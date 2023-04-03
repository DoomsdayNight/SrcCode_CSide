using System;
using Cs.Protocol;
using NKM;
using NKM.Templet;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C93 RID: 3219
	public sealed class WarfareUnitData : ISerializable
	{
		// Token: 0x06009423 RID: 37923 RVA: 0x00328204 File Offset: 0x00326404
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.warfareGameUnitUID);
			stream.PutOrGetEnum<WarfareUnitData.Type>(ref this.unitType);
			stream.PutOrGetEnum<NKM_WARFARE_ENEMY_ACTION_TYPE>(ref this.warfareEnemyActionType);
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet(ref this.friendCode);
			stream.PutOrGet(ref this.dungeonID);
			stream.PutOrGet(ref this.hp);
			stream.PutOrGet(ref this.hpMax);
			stream.PutOrGet(ref this.isTurnEnd);
			stream.PutOrGet(ref this.supply);
			stream.PutOrGet(ref this.tileIndex);
			stream.PutOrGet(ref this.isTarget);
			stream.PutOrGet(ref this.isSummonee);
		}

		// Token: 0x0400854F RID: 34127
		public int warfareGameUnitUID;

		// Token: 0x04008550 RID: 34128
		public WarfareUnitData.Type unitType;

		// Token: 0x04008551 RID: 34129
		public NKM_WARFARE_ENEMY_ACTION_TYPE warfareEnemyActionType;

		// Token: 0x04008552 RID: 34130
		public NKMDeckIndex deckIndex;

		// Token: 0x04008553 RID: 34131
		public long friendCode;

		// Token: 0x04008554 RID: 34132
		public int dungeonID;

		// Token: 0x04008555 RID: 34133
		public float hp;

		// Token: 0x04008556 RID: 34134
		public float hpMax;

		// Token: 0x04008557 RID: 34135
		public bool isTurnEnd;

		// Token: 0x04008558 RID: 34136
		public int supply;

		// Token: 0x04008559 RID: 34137
		public short tileIndex;

		// Token: 0x0400855A RID: 34138
		public bool isTarget;

		// Token: 0x0400855B RID: 34139
		public bool isSummonee;

		// Token: 0x02001A26 RID: 6694
		public enum Type : byte
		{
			// Token: 0x0400ADCD RID: 44493
			User,
			// Token: 0x0400ADCE RID: 44494
			Dungeon
		}
	}
}

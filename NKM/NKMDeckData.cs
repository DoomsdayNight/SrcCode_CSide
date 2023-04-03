using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020003D9 RID: 985
	public sealed class NKMDeckData : ISerializable
	{
		// Token: 0x060019EA RID: 6634 RVA: 0x0006F444 File Offset: 0x0006D644
		public NKMDeckData()
		{
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x0006F46C File Offset: 0x0006D66C
		public NKMDeckData(NKM_DECK_TYPE deckType)
		{
			int i = (deckType == NKM_DECK_TYPE.NDT_RAID) ? 16 : 8;
			while (i > this.m_listDeckUnitUID.Count)
			{
				this.m_listDeckUnitUID.Add(0L);
			}
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x0006F4C4 File Offset: 0x0006D6C4
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_DeckName);
			stream.PutOrGet(ref this.m_ShipUID);
			stream.PutOrGet(ref this.m_OperatorUID);
			stream.PutOrGet(ref this.m_listDeckUnitUID);
			stream.PutOrGet(ref this.m_LeaderIndex);
			stream.PutOrGetEnum<NKM_DECK_STATE>(ref this.m_DeckState);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x0006F51C File Offset: 0x0006D71C
		public bool CheckHasDuplicateUnit(NKMArmyData armyData)
		{
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < this.m_listDeckUnitUID.Count; i++)
			{
				long num = this.m_listDeckUnitUID[i];
				if (num != 0L)
				{
					NKMUnitData unitFromUID = armyData.GetUnitFromUID(num);
					if (NKMUnitManager.CheckContainsBaseUnit(hashSet, unitFromUID.m_UnitID))
					{
						return true;
					}
					if (!hashSet.Add(unitFromUID.m_UnitID))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0006F57E File Offset: 0x0006D77E
		public long GetLeaderUnitUID()
		{
			if (this.m_LeaderIndex < 0 || !this.IsValidSlotIndex((byte)this.m_LeaderIndex))
			{
				return 0L;
			}
			return this.m_listDeckUnitUID[(int)this.m_LeaderIndex];
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0006F5AC File Offset: 0x0006D7AC
		public bool IsLeaderUnit(long unitUID)
		{
			return this.GetLeaderUnitUID() == unitUID;
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0006F5BC File Offset: 0x0006D7BC
		public bool SetUnitUID(byte slotIndex, long unitUID)
		{
			if (!this.IsValidSlotIndex(slotIndex))
			{
				return false;
			}
			int num = this.m_listDeckUnitUID.FindIndex((long x) => x == unitUID);
			if (num != -1)
			{
				this.m_listDeckUnitUID[num] = 0L;
			}
			this.m_listDeckUnitUID[(int)slotIndex] = unitUID;
			return true;
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x0006F61E File Offset: 0x0006D81E
		public bool SetOperatorUID(long operatorUID)
		{
			this.m_OperatorUID = operatorUID;
			return true;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x0006F628 File Offset: 0x0006D828
		public NKM_DECK_STATE GetState()
		{
			return this.m_DeckState;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0006F630 File Offset: 0x0006D830
		public void SetState(NKM_DECK_STATE state)
		{
			this.m_DeckState = state;
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0006F63C File Offset: 0x0006D83C
		public NKM_ERROR_CODE IsValidState()
		{
			switch (this.m_DeckState)
			{
			case NKM_DECK_STATE.DECK_STATE_NORMAL:
				return NKM_ERROR_CODE.NEC_OK;
			case NKM_DECK_STATE.DECK_STATE_WORLDMAP_MISSION:
				return NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_DOING;
			case NKM_DECK_STATE.DECK_STATE_WARFARE:
				return NKM_ERROR_CODE.NEC_FAIL_WARFARE_DOING;
			case NKM_DECK_STATE.DECK_STATE_DIVE:
				return NKM_ERROR_CODE.NEC_FAIL_DIVE_DOING;
			default:
				return NKM_ERROR_CODE.NEC_FAIL_DECK_STATE_INVALID;
			}
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x0006F684 File Offset: 0x0006D884
		public bool IsValidGame(NKM_GAME_TYPE gameType)
		{
			if (NKMGame.IsPVP(gameType))
			{
				return this.m_DeckState == NKM_DECK_STATE.DECK_STATE_NORMAL;
			}
			switch (gameType)
			{
			case NKM_GAME_TYPE.NGT_INVALID:
			case NKM_GAME_TYPE.NGT_DEV:
			case NKM_GAME_TYPE.NGT_PRACTICE:
			case NKM_GAME_TYPE.NGT_DUNGEON:
			case NKM_GAME_TYPE.NGT_PVP_RANK:
			case NKM_GAME_TYPE.NGT_TUTORIAL:
			case NKM_GAME_TYPE.NGT_RAID:
			case NKM_GAME_TYPE.NGT_ASYNC_PVP:
			case NKM_GAME_TYPE.NGT_RAID_SOLO:
			case NKM_GAME_TYPE.NGT_SHADOW_PALACE:
			case NKM_GAME_TYPE.NGT_PHASE:
			case NKM_GAME_TYPE.NGT_GUILD_DUNGEON_ARENA:
			case NKM_GAME_TYPE.NGT_GUILD_DUNGEON_BOSS:
			case NKM_GAME_TYPE.NGT_PVP_PRIVATE:
			case NKM_GAME_TYPE.NGT_PVP_LEAGUE:
			case NKM_GAME_TYPE.NGT_TRIM:
				return this.m_DeckState == NKM_DECK_STATE.DECK_STATE_NORMAL;
			case NKM_GAME_TYPE.NGT_WARFARE:
				return this.m_DeckState == NKM_DECK_STATE.DECK_STATE_WARFARE;
			case NKM_GAME_TYPE.NGT_DIVE:
				return this.m_DeckState == NKM_DECK_STATE.DECK_STATE_DIVE;
			case NKM_GAME_TYPE.NGT_CUTSCENE:
			case NKM_GAME_TYPE.NGT_WORLDMAP:
				return false;
			}
			return false;
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0006F72C File Offset: 0x0006D92C
		private bool IsValidSlotIndex(byte slotIndex)
		{
			return (int)slotIndex < this.m_listDeckUnitUID.Count;
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0006F73C File Offset: 0x0006D93C
		public IEnumerable<NKMUnitData> GetUnits(NKMArmyData armyData)
		{
			foreach (long num in this.m_listDeckUnitUID)
			{
				if (num != 0L)
				{
					yield return armyData.GetUnitFromUID(num);
				}
			}
			List<long>.Enumerator enumerator = default(List<long>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0006F754 File Offset: 0x0006D954
		public bool HasUnitUid(long unitUid, out int index)
		{
			for (int i = 0; i < this.m_listDeckUnitUID.Count; i++)
			{
				if (this.m_listDeckUnitUID[i] == unitUid)
				{
					index = i;
					return true;
				}
			}
			index = -1;
			return false;
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0006F78F File Offset: 0x0006D98F
		public bool CheckAllSlotFilled()
		{
			if (this.m_ShipUID != 0L)
			{
				return this.m_listDeckUnitUID.TrueForAll((long e) => e != 0L);
			}
			return false;
		}

		// Token: 0x04001304 RID: 4868
		public const int InvalidIndex = -1;

		// Token: 0x04001305 RID: 4869
		public string m_DeckName = string.Empty;

		// Token: 0x04001306 RID: 4870
		public long m_ShipUID;

		// Token: 0x04001307 RID: 4871
		public long m_OperatorUID;

		// Token: 0x04001308 RID: 4872
		public List<long> m_listDeckUnitUID = new List<long>();

		// Token: 0x04001309 RID: 4873
		public sbyte m_LeaderIndex = -1;

		// Token: 0x0400130A RID: 4874
		public NKM_DECK_STATE m_DeckState;

		// Token: 0x0400130B RID: 4875
		public int power;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ClientPacket.Common;
using Cs.Core.Util;
using Cs.Logging;
using Cs.Protocol;
using NKC;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x0200039B RID: 923
	[DataContract]
	public class NKMArmyData : Cs.Protocol.ISerializable
	{
		// Token: 0x060017A7 RID: 6055 RVA: 0x0005F72C File Offset: 0x0005D92C
		public NKMArmyData()
		{
			for (int i = 0; i < this.deckSets.Length; i++)
			{
				this.deckSets[i] = new NKMDeckSet((NKM_DECK_TYPE)i);
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x0005F7EF File Offset: 0x0005D9EF
		public NKMUserData Owner
		{
			get
			{
				return this.owner;
			}
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0005F7F7 File Offset: 0x0005D9F7
		public List<NKMDeckSet> GetDeckList()
		{
			return this.deckSets.ToList<NKMDeckSet>();
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x0005F804 File Offset: 0x0005DA04
		public IEnumerable<NKMDeckSet> DeckSets
		{
			get
			{
				return this.deckSets;
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0005F80C File Offset: 0x0005DA0C
		public void UpdateData(NKMUnitData UnitData)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(UnitData);
			if (unitTempletBase != null)
			{
				switch (unitTempletBase.m_NKM_UNIT_TYPE)
				{
				default:
					if (unitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
					{
						this.UpdateUnitData(UnitData);
						return;
					}
					this.UpdateTrophyData(UnitData);
					return;
				case NKM_UNIT_TYPE.NUT_SHIP:
					this.UpdateShipData(UnitData);
					break;
				case NKM_UNIT_TYPE.NUT_OPERATOR:
					break;
				}
			}
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0005F85E File Offset: 0x0005DA5E
		public void SetOwner(NKMUserData owner)
		{
			this.owner = owner;
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0005F868 File Offset: 0x0005DA68
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_MaxUnitCount);
			stream.PutOrGet(ref this.m_MaxShipCount);
			stream.PutOrGet(ref this.m_MaxOperatorCount);
			stream.PutOrGet(ref this.m_MaxTrophyCount);
			stream.PutOrGet<NKMDeckSet>(ref this.deckSets);
			stream.PutOrGet<NKMUnitData>(ref this.m_dicMyShip);
			stream.PutOrGet<NKMUnitData>(ref this.m_dicMyUnit);
			stream.PutOrGet<NKMOperator>(ref this.m_dicMyOperator);
			stream.PutOrGet<NKMUnitData>(ref this.m_dicMyTrophy);
			stream.PutOrGet(ref this.m_illustrateUnit);
			stream.PutOrGet<NKMTeamCollectionData>(ref this.m_dicTeamCollectionData);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0005F8F9 File Offset: 0x0005DAF9
		public NKMDeckData GetDeckData(NKMDeckIndex deckIndex)
		{
			return this.GetDeckData(deckIndex.m_eDeckType, (int)deckIndex.m_iIndex);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0005F910 File Offset: 0x0005DB10
		public NKMDeckData GetDeckData(NKM_DECK_TYPE type, int index)
		{
			IReadOnlyList<NKMDeckData> deckList = this.GetDeckList(type);
			if (index >= 0 && index < deckList.Count)
			{
				return deckList[index];
			}
			return null;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0005F93B File Offset: 0x0005DB3B
		public bool SetPvpDefenceDeck(NKMDeckData deckData)
		{
			if (deckData == null || !deckData.CheckAllSlotFilled())
			{
				return false;
			}
			this.deckSets[6].SetDeck(0, deckData);
			return true;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0005F95A File Offset: 0x0005DB5A
		public void SetTrimDeck(byte index, NKMDeckData deckData)
		{
			this.deckSets[7].SetDeck((int)index, deckData);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0005F96C File Offset: 0x0005DB6C
		public NKMDeckData GetDeckDataByUnitUID(NKM_DECK_TYPE deckType, long unitUID)
		{
			NKMDeckData result;
			this.GetDeckSet(deckType).FindDeckByUnitUid(unitUID, out result);
			return result;
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0005F98C File Offset: 0x0005DB8C
		public NKMDeckData GetDeckDataByUnitUID(long unitUID)
		{
			if (unitUID == 0L)
			{
				return null;
			}
			NKMDeckSet[] array = this.deckSets;
			for (int i = 0; i < array.Length; i++)
			{
				NKMDeckData result;
				if (array[i].FindDeckByUnitUid(unitUID, out result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0005F9C4 File Offset: 0x0005DBC4
		public NKMDeckData GetDeckDataByShipUID(long shipUID)
		{
			if (shipUID == 0L)
			{
				return null;
			}
			NKMDeckSet[] array = this.deckSets;
			for (int i = 0; i < array.Length; i++)
			{
				NKMDeckData result;
				if (array[i].FindDeckByShipUid(shipUID, out result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0005F9FB File Offset: 0x0005DBFB
		public int GetDeckCount(NKM_DECK_TYPE eType)
		{
			return this.GetDeckSet(eType).Count;
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0005FA09 File Offset: 0x0005DC09
		public NKMUnitData GetDeckShip(NKMDeckIndex deckIndex)
		{
			return this.GetDeckShip(deckIndex.m_eDeckType, (int)deckIndex.m_iIndex);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0005FA20 File Offset: 0x0005DC20
		public NKMUnitData GetDeckShip(NKM_DECK_TYPE eType, int deckIndex)
		{
			NKMDeckData deckData = this.GetDeckData(eType, deckIndex);
			if (deckData == null)
			{
				return null;
			}
			if (this.m_dicMyShip.ContainsKey(deckData.m_ShipUID))
			{
				return this.m_dicMyShip[deckData.m_ShipUID];
			}
			return null;
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0005FA61 File Offset: 0x0005DC61
		public NKMOperator GetDeckOperator(NKMDeckIndex deckIndex)
		{
			return this.GetDeckOperator(deckIndex.m_eDeckType, (int)deckIndex.m_iIndex);
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0005FA78 File Offset: 0x0005DC78
		public NKMOperator GetDeckOperator(NKM_DECK_TYPE eType, int deckIndex)
		{
			NKMDeckData deckData = this.GetDeckData(eType, deckIndex);
			if (deckData == null)
			{
				return null;
			}
			if (this.m_dicMyOperator.ContainsKey(deckData.m_OperatorUID))
			{
				return this.m_dicMyOperator[deckData.m_OperatorUID];
			}
			return null;
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0005FABC File Offset: 0x0005DCBC
		public NKMUnitData GetUnitFromUID(long unitUid)
		{
			NKMUnitData result;
			this.m_dicMyUnit.TryGetValue(unitUid, out result);
			return result;
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0005FADC File Offset: 0x0005DCDC
		public NKMUnitData GetShipFromUID(long shipUid)
		{
			NKMUnitData result;
			this.m_dicMyShip.TryGetValue(shipUid, out result);
			return result;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0005FAFC File Offset: 0x0005DCFC
		public NKMUnitData GetTrophyFromUID(long trophy)
		{
			NKMUnitData result;
			this.m_dicMyTrophy.TryGetValue(trophy, out result);
			return result;
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0005FB1C File Offset: 0x0005DD1C
		public NKMOperator GetOperatorFromUId(long operatorUid)
		{
			NKMOperator result;
			this.m_dicMyOperator.TryGetValue(operatorUid, out result);
			return result;
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0005FB3C File Offset: 0x0005DD3C
		public List<NKMUnitData> GetTrophyListByUnitID(int unitID)
		{
			List<NKMUnitData> list = new List<NKMUnitData>();
			foreach (NKMUnitData nkmunitData in this.m_dicMyTrophy.Values)
			{
				if (nkmunitData.m_UnitID == unitID)
				{
					list.Add(nkmunitData);
				}
			}
			return list;
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0005FBA4 File Offset: 0x0005DDA4
		public List<int> GetAmryIndexListByCategory(EPISODE_CATEGORY episodeCategory, int limitCount)
		{
			switch (episodeCategory)
			{
			case EPISODE_CATEGORY.EC_MAINSTREAM:
			case EPISODE_CATEGORY.EC_FIELD:
				return this.GetDeckIndexList(NKM_DECK_TYPE.NDT_NORMAL, NKM_DECK_STATE.DECK_STATE_WARFARE, limitCount);
			case EPISODE_CATEGORY.EC_DAILY:
				return this.GetDeckIndexList(NKM_DECK_TYPE.NDT_DAILY, NKM_DECK_STATE.DECK_STATE_NORMAL, limitCount);
			}
			return null;
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0005FBD8 File Offset: 0x0005DDD8
		public List<int> GetDeckIndexList(NKM_DECK_TYPE deckType, NKM_DECK_STATE deckState, int limit)
		{
			IReadOnlyList<NKMDeckData> deckList = this.GetDeckList(deckType);
			List<int> list = new List<int>();
			int num = 0;
			while (num < deckList.Count && list.Count < limit)
			{
				if (deckList[num].GetState() == deckState)
				{
					list.Add(num);
				}
				num++;
			}
			return list;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0005FC24 File Offset: 0x0005DE24
		public static Predicate<NKMUnitData> MakeSearchPredicate(int unitID, NKMArmyData.UNIT_SEARCH_OPTION searchType, int searchValue)
		{
			switch (searchType)
			{
			case NKMArmyData.UNIT_SEARCH_OPTION.None:
				return (NKMUnitData unit) => unit.m_UnitID == unitID;
			case NKMArmyData.UNIT_SEARCH_OPTION.Level:
				return (NKMUnitData unit) => unit.m_UnitID == unitID && unit.m_UnitLevel >= searchValue;
			case NKMArmyData.UNIT_SEARCH_OPTION.LimitLevel:
				return (NKMUnitData unit) => unit.m_UnitID == unitID && (int)unit.m_LimitBreakLevel >= searchValue;
			case NKMArmyData.UNIT_SEARCH_OPTION.Devotion:
				return (NKMUnitData unit) => unit.m_UnitID == unitID && unit.IsPermanentContract;
			case NKMArmyData.UNIT_SEARCH_OPTION.StarGrade:
				return (NKMUnitData unit) => unit.m_UnitID == unitID && unit.GetStarGrade() >= searchValue;
			default:
				return (NKMUnitData unit) => false;
			}
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0005FCC4 File Offset: 0x0005DEC4
		private static Func<NKMUnitData, long> MakeUnitStatusSelector(NKMArmyData.UNIT_SEARCH_OPTION searchType)
		{
			switch (searchType)
			{
			case NKMArmyData.UNIT_SEARCH_OPTION.None:
				return (NKMUnitData unit) => unit.m_UnitUID;
			case NKMArmyData.UNIT_SEARCH_OPTION.Level:
				return (NKMUnitData unit) => (long)unit.m_UnitLevel;
			case NKMArmyData.UNIT_SEARCH_OPTION.LimitLevel:
				return (NKMUnitData unit) => (long)unit.m_LimitBreakLevel;
			case NKMArmyData.UNIT_SEARCH_OPTION.Devotion:
				return (NKMUnitData unit) => unit.IsPermanentContract ? 1L : 0L;
			case NKMArmyData.UNIT_SEARCH_OPTION.StarGrade:
				return (NKMUnitData unit) => (long)unit.GetStarGrade();
			default:
				return (NKMUnitData unit) => 0L;
			}
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0005FDB0 File Offset: 0x0005DFB0
		public long SearchMaxUnitStatusByID(NKM_UNIT_TYPE unitType, int unitID, NKMArmyData.UNIT_SEARCH_OPTION searchType)
		{
			IEnumerable<NKMUnitData> enumerable = null;
			if (unitType != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				if (unitType == NKM_UNIT_TYPE.NUT_SHIP)
				{
					enumerable = this.m_dicMyShip.Values;
				}
			}
			else
			{
				enumerable = this.m_dicMyUnit.Values;
			}
			if (enumerable == null)
			{
				return 0L;
			}
			IEnumerable<NKMUnitData> source = from unit in enumerable
			where unit.m_UnitID == unitID
			select unit;
			if (source.Count<NKMUnitData>() <= 0)
			{
				return 0L;
			}
			Func<NKMUnitData, long> selector = NKMArmyData.MakeUnitStatusSelector(searchType);
			return source.Max((NKMUnitData unit) => selector(unit));
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0005FE34 File Offset: 0x0005E034
		public NKMUnitData GetUnitOrShipFromUID(long UID)
		{
			if (this.m_dicMyUnit.ContainsKey(UID))
			{
				return this.m_dicMyUnit[UID];
			}
			if (this.m_dicMyTrophy.ContainsKey(UID))
			{
				return this.m_dicMyTrophy[UID];
			}
			if (this.m_dicMyShip.ContainsKey(UID))
			{
				return this.m_dicMyShip[UID];
			}
			return null;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0005FE93 File Offset: 0x0005E093
		public NKMUnitData GetUnitOrTrophyFromUID(long UID)
		{
			if (this.m_dicMyUnit.ContainsKey(UID))
			{
				return this.m_dicMyUnit[UID];
			}
			if (this.m_dicMyTrophy.ContainsKey(UID))
			{
				return this.m_dicMyTrophy[UID];
			}
			return null;
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0005FECC File Offset: 0x0005E0CC
		public NKMUnitData GetDeckUnitByIndex(NKMDeckIndex deckIndex, int slotIndex)
		{
			return this.GetDeckUnitByIndex(deckIndex.m_eDeckType, (int)deckIndex.m_iIndex, slotIndex);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0005FEE4 File Offset: 0x0005E0E4
		public NKMUnitData GetDeckUnitByIndex(NKM_DECK_TYPE eType, int deckIndex, int slotIndex)
		{
			NKMDeckData deckData = this.GetDeckData(eType, deckIndex);
			return this.GetUnitData(deckData, slotIndex);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0005FF04 File Offset: 0x0005E104
		private NKMUnitData GetUnitData(NKMDeckData cNKMDeckData, int slotIndex)
		{
			if (cNKMDeckData == null)
			{
				return null;
			}
			if (slotIndex >= 0 && slotIndex < cNKMDeckData.m_listDeckUnitUID.Count)
			{
				long key = cNKMDeckData.m_listDeckUnitUID[slotIndex];
				if (this.m_dicMyUnit.ContainsKey(key))
				{
					return this.m_dicMyUnit[key];
				}
			}
			return null;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0005FF51 File Offset: 0x0005E151
		public NKMOperator GetDeckOperatorByIndex(NKMDeckIndex deckIndex)
		{
			return this.GetDeckOperatorByIndex(deckIndex.m_eDeckType, (int)deckIndex.m_iIndex);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0005FF68 File Offset: 0x0005E168
		public NKMOperator GetDeckOperatorByIndex(NKM_DECK_TYPE eType, int deckIndex)
		{
			NKMDeckData deckData = this.GetDeckData(eType, deckIndex);
			if (deckData == null)
			{
				return null;
			}
			if (!this.m_dicMyOperator.ContainsKey(deckData.m_OperatorUID))
			{
				return null;
			}
			return this.m_dicMyOperator[deckData.m_OperatorUID];
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0005FFAC File Offset: 0x0005E1AC
		public NKMUnitData GetDeckLeaderUnitData(NKM_DECK_TYPE deckType, byte deckIndex)
		{
			NKMDeckData deckData = this.GetDeckData(deckType, (int)deckIndex);
			if (deckData != null)
			{
				return this.GetUnitData(deckData, (int)deckData.m_LeaderIndex);
			}
			return null;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0005FFD4 File Offset: 0x0005E1D4
		public NKMUnitData GetDeckLeaderUnitData(NKMDeckIndex deckIndex)
		{
			NKMDeckData deckData = this.GetDeckData(deckIndex);
			if (deckData != null)
			{
				return this.GetUnitData(deckData, (int)deckData.m_LeaderIndex);
			}
			return null;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0005FFFC File Offset: 0x0005E1FC
		public IReadOnlyList<NKMDeckData> GetDeckList(NKM_DECK_TYPE eType)
		{
			int num = (int)eType;
			if (num >= this.deckSets.Length)
			{
				num = 0;
			}
			return this.deckSets[num].Values;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00060028 File Offset: 0x0005E228
		public NKMDeckSet GetDeckSet(NKM_DECK_TYPE eType)
		{
			int num = (int)eType;
			if (num >= this.deckSets.Length)
			{
				num = 0;
			}
			return this.deckSets[num];
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0006004C File Offset: 0x0005E24C
		public NKMDeckIndex GetShipDeckIndex(NKM_DECK_TYPE eType, long shipUID)
		{
			NKMDeckIndex result;
			this.GetDeckSet(eType).FindDeckIndexByShipUid(shipUID, out result);
			return result;
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0006006C File Offset: 0x0005E26C
		public NKMDeckIndex GetOperatorDeckIndex(NKM_DECK_TYPE eType, long operatorUid)
		{
			NKMDeckIndex result;
			this.GetDeckSet(eType).FindDeckIndexByOperatorUid(operatorUid, out result);
			return result;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0006008A File Offset: 0x0005E28A
		public bool GetUnitDeckPosition(NKM_DECK_TYPE eType, long unitUID, out NKMDeckIndex unitDeckIndex, out sbyte unitSlotIndex)
		{
			return this.GetDeckSet(eType).FindDeckIndexByUnitUid(unitUID, out unitDeckIndex, out unitSlotIndex);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0006009C File Offset: 0x0005E29C
		public void AddDeck(NKM_DECK_TYPE eType, NKMDeckData newDeck)
		{
			this.GetDeckSet(eType).AddDeck(newDeck);
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x000600AB File Offset: 0x0005E2AB
		public int GetCurrentUnitCount()
		{
			return this.m_dicMyUnit.Count;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x000600B8 File Offset: 0x0005E2B8
		public int GetCurrentShipCount()
		{
			return this.m_dicMyShip.Count;
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000600C5 File Offset: 0x0005E2C5
		public int GetCurrentOperatorCount()
		{
			return this.m_dicMyOperator.Count;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000600D2 File Offset: 0x0005E2D2
		public int GetCurrentTrophyCount()
		{
			return this.m_dicMyTrophy.Count;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x000600DF File Offset: 0x0005E2DF
		public bool CanGetMoreUnit(int addCount)
		{
			return this.GetCurrentUnitCount() + addCount <= this.m_MaxUnitCount;
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x000600F4 File Offset: 0x0005E2F4
		public bool CanGetMoreShip(int addCount)
		{
			return this.GetCurrentShipCount() + addCount <= this.m_MaxShipCount;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00060109 File Offset: 0x0005E309
		public bool CanGetMoreOperator(int addCount)
		{
			return this.GetCurrentOperatorCount() + addCount <= this.m_MaxOperatorCount;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0006011E File Offset: 0x0005E31E
		public bool CanGetMoreTrophy(int addCount)
		{
			return this.GetCurrentTrophyCount() + addCount <= this.m_MaxTrophyCount;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00060133 File Offset: 0x0005E333
		public bool IsValidDeckIndex(NKMDeckIndex deckIndex)
		{
			return this.IsValidDeckIndex(deckIndex.m_eDeckType, (int)deckIndex.m_iIndex);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00060147 File Offset: 0x0005E347
		public bool IsValidDeckIndex(NKM_DECK_TYPE eType, int deckIndex)
		{
			return eType != NKM_DECK_TYPE.NDT_NONE && 0 <= deckIndex && deckIndex < (int)this.GetUnlockedDeckCount(eType);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0006015E File Offset: 0x0005E35E
		public byte GetUnlockedDeckCount(NKM_DECK_TYPE eType)
		{
			if (eType == NKM_DECK_TYPE.NDT_TRIM)
			{
				return this.GetMaxDeckCount(NKM_DECK_TYPE.NDT_TRIM);
			}
			return (byte)this.GetDeckList(eType).Count;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0006017C File Offset: 0x0005E37C
		public void UnlockDeck(NKM_DECK_TYPE eType, int newDeckSize)
		{
			if (eType == NKM_DECK_TYPE.NDT_NONE)
			{
				Log.Error("Trying unlock nonexistent deck", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMArmyData.cs", 571);
				return;
			}
			if (newDeckSize > (int)this.GetMaxDeckCount(eType))
			{
				Log.Error("Trying unlock beyond max size", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMArmyData.cs", 577);
				newDeckSize = (int)this.GetMaxDeckCount(eType);
			}
			while (this.GetDeckCount(eType) < newDeckSize)
			{
				NKMDeckData newDeck = new NKMDeckData(eType);
				this.AddDeck(eType, newDeck);
			}
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x000601E4 File Offset: 0x0005E3E4
		public void RemoveUnit(IEnumerable<long> lstUnitUID)
		{
			if (lstUnitUID == null)
			{
				return;
			}
			foreach (long num in lstUnitUID)
			{
				if (this.m_dicMyUnit.ContainsKey(num))
				{
					this.RemoveUnit(num);
				}
				else if (this.m_dicMyTrophy.ContainsKey(num))
				{
					this.RemoveTrophy(num);
				}
			}
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00060258 File Offset: 0x0005E458
		public void RemoveShip(IEnumerable<long> lstShipUID)
		{
			if (lstShipUID == null)
			{
				return;
			}
			foreach (long shipUID in lstShipUID)
			{
				this.RemoveShip(shipUID);
			}
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x000602A4 File Offset: 0x0005E4A4
		public void RemoveOperator(IEnumerable<long> lstOperatorUID)
		{
			if (lstOperatorUID == null)
			{
				return;
			}
			foreach (long operatorUid in lstOperatorUID)
			{
				this.RemoveOperator(operatorUid);
			}
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x000602F0 File Offset: 0x0005E4F0
		public void RemoveOperator(long operatorUid)
		{
			this.m_dicMyOperator.Remove(operatorUid);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00060300 File Offset: 0x0005E500
		public bool IsUnitInAnyDeck(long unitUID)
		{
			NKMDeckSet[] array = this.deckSets;
			for (int i = 0; i < array.Length; i++)
			{
				NKMDeckData nkmdeckData;
				if (array[i].FindDeckByUnitUid(unitUID, out nkmdeckData))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00060334 File Offset: 0x0005E534
		public bool IsOperatorAnyDeck(long operatorUId)
		{
			NKMDeckSet[] array = this.deckSets;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].FindDeckByOperatporUid(operatorUId))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00060364 File Offset: 0x0005E564
		public bool IsShipInAnyDeck(long shipUid)
		{
			NKMDeckSet[] array = this.deckSets;
			for (int i = 0; i < array.Length; i++)
			{
				using (IEnumerator<NKMDeckData> enumerator = array[i].Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.m_ShipUID == shipUid)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000603D0 File Offset: 0x0005E5D0
		public int GetArmyAvarageOperationPower(NKMDeckIndex index, bool bPVP = false, Dictionary<int, NKMBanData> dicNKMBanData = null, Dictionary<int, NKMUnitUpData> dicNKMUpData = null)
		{
			NKMDeckData deckData = this.GetDeckData(index);
			if (deckData == null)
			{
				return 0;
			}
			return this.GetArmyAvarageOperationPower(deckData, bPVP, dicNKMBanData, dicNKMUpData);
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x000603F8 File Offset: 0x0005E5F8
		public int GetArmyAvarageOperationPower(NKMDeckData deckData, bool bPVP = false, Dictionary<int, NKMBanData> dicNKMBanData = null, Dictionary<int, NKMUnitUpData> dicNKMUpData = null)
		{
			if (deckData == null)
			{
				return 0;
			}
			NKMUnitData shipFromUID = this.GetShipFromUID(deckData.m_ShipUID);
			IEnumerable<NKMUnitData> units = deckData.GetUnits(this);
			int operatorPower = 0;
			if (deckData.m_OperatorUID != 0L)
			{
				NKMOperator operatorFromUId = this.GetOperatorFromUId(deckData.m_OperatorUID);
				if (operatorFromUId != null)
				{
					operatorPower = operatorFromUId.CalculateOperatorOperationPower();
				}
			}
			return NKMOperationPower.Calculate(shipFromUID, units, deckData.GetLeaderUnitUID(), this.owner.m_InventoryData, bPVP, dicNKMBanData, dicNKMUpData, operatorPower);
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0006045C File Offset: 0x0005E65C
		public int GetArmyAvarageOperationPower(NKMEventDeckData deckData, bool bPVP = false, Dictionary<int, NKMBanData> dicNKMBanData = null, Dictionary<int, NKMUnitUpData> dicNKMUpData = null)
		{
			if (deckData == null)
			{
				return 0;
			}
			NKMUnitData shipFromUID = this.GetShipFromUID(deckData.m_ShipUID);
			IEnumerable<NKMUnitData> units = deckData.GetUnits(this);
			int operatorPower = 0;
			if (deckData.m_OperatorUID != 0L)
			{
				NKMOperator operatorFromUId = this.GetOperatorFromUId(deckData.m_OperatorUID);
				if (operatorFromUId != null)
				{
					operatorPower = operatorFromUId.CalculateOperatorOperationPower();
				}
			}
			return NKMOperationPower.Calculate(shipFromUID, units, deckData.m_dicUnit[deckData.m_LeaderIndex], this.owner.m_InventoryData, bPVP, dicNKMBanData, dicNKMUpData, operatorPower);
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x000604CC File Offset: 0x0005E6CC
		public bool CanSetLeader(NKMDeckIndex deckIndex)
		{
			if (!this.IsValidDeckIndex(deckIndex))
			{
				return false;
			}
			NKMDeckData deckData = this.GetDeckData(deckIndex);
			return deckData != null && deckData.IsValidState() == NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x000604FC File Offset: 0x0005E6FC
		public NKM_ERROR_CODE CanModifyOrPlayDeck(NKMDeckIndex deckIndex)
		{
			if (!this.IsValidDeckIndex(deckIndex))
			{
				return NKM_ERROR_CODE.NEC_FAIL_DECK_DATA_INVALID;
			}
			NKMDeckData deckData = this.GetDeckData(deckIndex);
			if (deckData == null)
			{
				Log.Error(string.Format("Invalid DeckIndex. userUid:{0}, deckType:{1}, deckIndex:{2}", this.owner.m_UserUID, deckIndex.m_eDeckType, deckIndex.m_iIndex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMArmyData.cs", 778);
				return NKM_ERROR_CODE.NEC_FAIL_DECK_DATA_INVALID;
			}
			return deckData.IsValidState();
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00060568 File Offset: 0x0005E768
		public void AddTeamCollectionData(NKMTeamCollectionData data)
		{
			if (!this.m_dicTeamCollectionData.ContainsKey(data.TeamID))
			{
				this.m_dicTeamCollectionData.Add(data.TeamID, data);
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00060590 File Offset: 0x0005E790
		public NKMTeamCollectionData GetTeamCollectionData(int teamID)
		{
			NKMTeamCollectionData result;
			this.m_dicTeamCollectionData.TryGetValue(teamID, out result);
			return result;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x000605B0 File Offset: 0x0005E7B0
		public float CalculateDeckAvgSummonCost(NKMDeckIndex deckIndex, Dictionary<int, NKMBanData> dicNKMBanData = null, Dictionary<int, NKMUnitUpData> dicNKMUpData = null)
		{
			NKMDeckData deckData = this.GetDeckData(deckIndex);
			int num = 0;
			int num2 = 0;
			if (deckData == null)
			{
				return 0f;
			}
			for (int i = 0; i < 8; i++)
			{
				long unitUid = deckData.m_listDeckUnitUID[i];
				NKMUnitData unitFromUID = this.GetUnitFromUID(unitUid);
				if (unitFromUID != null)
				{
					num++;
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitFromUID.m_UnitID);
					if (unitStatTemplet == null)
					{
						Log.Error(string.Format("Cannot found UnitStatTemplet. UserUid:{0}, UnitId:{1}", this.owner.m_UserUID, unitFromUID.m_UnitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMArmyData.cs", 821);
					}
					else
					{
						num2 += unitStatTemplet.GetRespawnCost(i == (int)deckData.m_LeaderIndex, dicNKMBanData, dicNKMUpData);
					}
				}
			}
			if (num == 0)
			{
				return 0f;
			}
			return (float)num2 / (float)num;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00060670 File Offset: 0x0005E870
		public void ResetDeckStateOf(NKM_DECK_STATE targetState)
		{
			foreach (NKMDeckData nkmdeckData in this.deckSets[1].Values)
			{
				if (nkmdeckData != null && nkmdeckData.m_DeckState == targetState)
				{
					nkmdeckData.m_DeckState = NKM_DECK_STATE.DECK_STATE_NORMAL;
				}
			}
			foreach (NKMDeckData nkmdeckData2 in this.deckSets[8].Values)
			{
				if (nkmdeckData2 != null && nkmdeckData2.m_DeckState == targetState)
				{
					nkmdeckData2.m_DeckState = NKM_DECK_STATE.DECK_STATE_NORMAL;
				}
			}
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00060720 File Offset: 0x0005E920
		public int GetUnitCountByLevel(int level)
		{
			return (from e in this.m_dicMyUnit.Values
			where e.m_UnitLevel >= level
			select e).Count<NKMUnitData>();
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0006075C File Offset: 0x0005E95C
		public int GetShipCountByLevel(int level)
		{
			return (from e in this.m_dicMyShip.Values
			where e.m_UnitLevel >= level
			select e).Count<NKMUnitData>();
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00060797 File Offset: 0x0005E997
		public int GetUnitPermanentCount()
		{
			return this.m_dicMyUnit.Values.Count((NKMUnitData e) => e.IsPermanentContract);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x000607C8 File Offset: 0x0005E9C8
		public byte GetMaxDeckCount(NKM_DECK_TYPE type)
		{
			switch (type)
			{
			case NKM_DECK_TYPE.NDT_NORMAL:
				return (byte)NKMCommonConst.Deck.MaxNormalDeckCount;
			case NKM_DECK_TYPE.NDT_PVP:
				return (byte)NKMCommonConst.Deck.MaxPvpDeckCount;
			case NKM_DECK_TYPE.NDT_DAILY:
				return (byte)NKMCommonConst.Deck.MaxDailyDeckCount;
			case NKM_DECK_TYPE.NDT_RAID:
				return (byte)NKMCommonConst.Deck.MaxRaidDeckCount;
			case NKM_DECK_TYPE.NDT_FRIEND:
				return (byte)NKMCommonConst.Deck.MaxFriendDeckCount;
			case NKM_DECK_TYPE.NDT_PVP_DEFENCE:
				return (byte)NKMCommonConst.Deck.MaxPvpDefenceDeckCount;
			case NKM_DECK_TYPE.NDT_TRIM:
				return (byte)NKMCommonConst.Deck.MaxTrimingDeckCount;
			case NKM_DECK_TYPE.NDT_DIVE:
				return (byte)NKMCommonConst.Deck.MaxDiveDeckCount;
			default:
				return 0;
			}
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00060860 File Offset: 0x0005EA60
		public bool IsFirstGetUnit(int unitID)
		{
			return !this.m_illustrateUnit.Contains(unitID);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00060874 File Offset: 0x0005EA74
		public void AddNewUnit(NKMUnitData newUnit)
		{
			if (newUnit == null)
			{
				Log.Error("Trying to add null unit", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMArmyDataEx.cs", 43);
				return;
			}
			if (this.TryCollectUnit(newUnit.m_UnitID))
			{
				NKCContentManager.SetUnlockedContent(STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_COLLECTION_RARITY_COUNT, -1);
				NKCContentManager.SetUnlockedContent(STAGE_UNLOCK_REQ_TYPE.SURT_UNIT_GET, newUnit.m_UnitID);
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(newUnit);
			if (unitTempletBase == null)
			{
				Log.Error(string.Format("unit has no templetbase! id : {0}", newUnit.m_UnitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMArmyDataEx.cs", 57);
				return;
			}
			if (unitTempletBase.IsTrophy)
			{
				if (this.m_dicMyTrophy.ContainsKey(newUnit.m_UnitUID))
				{
					Log.Error("Trying to add duplicated trophy", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMArmyDataEx.cs", 69);
					return;
				}
				this.m_dicMyTrophy.Add(newUnit.m_UnitUID, newUnit);
			}
			else
			{
				if (this.m_dicMyUnit.ContainsKey(newUnit.m_UnitUID))
				{
					Log.Error("Trying to add duplicated unit", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMArmyDataEx.cs", 81);
					return;
				}
				this.m_dicMyUnit.Add(newUnit.m_UnitUID, newUnit);
			}
			NKMArmyData.OnUnitUpdate onUnitUpdate = this.dOnUnitUpdate;
			if (onUnitUpdate == null)
			{
				return;
			}
			onUnitUpdate(NKMUserData.eChangeNotifyType.Add, NKM_UNIT_TYPE.NUT_NORMAL, newUnit.m_UnitUID, newUnit);
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00060978 File Offset: 0x0005EB78
		public bool SearchUnitByID(NKM_UNIT_TYPE unitType, int unitID, NKMArmyData.UNIT_SEARCH_OPTION searchType, int searchValue)
		{
			IEnumerable<NKMUnitData> enumerable = null;
			if (unitType != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				if (unitType == NKM_UNIT_TYPE.NUT_SHIP)
				{
					enumerable = this.m_dicMyShip.Values;
				}
			}
			else
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
				if (unitTempletBase != null && unitTempletBase.IsTrophy)
				{
					enumerable = this.m_dicMyTrophy.Values;
				}
				else
				{
					enumerable = this.m_dicMyUnit.Values;
				}
			}
			if (enumerable == null)
			{
				return false;
			}
			if (searchType == NKMArmyData.UNIT_SEARCH_OPTION.Level && unitType == NKM_UNIT_TYPE.NUT_NORMAL && this.IsHasRearmUnit(unitID))
			{
				return true;
			}
			Predicate<NKMUnitData> predicate = NKMArmyData.MakeSearchPredicate(unitID, searchType, searchValue);
			return enumerable.Any((NKMUnitData unit) => predicate(unit));
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00060A0C File Offset: 0x0005EC0C
		private bool IsHasRearmUnit(int unitID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
			if (unitTempletBase != null)
			{
				foreach (NKMUnitRearmamentTemplet nkmunitRearmamentTemplet in NKMTempletContainer<NKMUnitRearmamentTemplet>.Values)
				{
					if (nkmunitRearmamentTemplet.EnableByTag && nkmunitRearmamentTemplet.FromUnitTemplet.m_UnitID == unitID && this.IsCollectedUnit(nkmunitRearmamentTemplet.ToUnitTemplet.m_UnitID))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00060A8C File Offset: 0x0005EC8C
		public void AddNewShip(NKMUnitData newShip)
		{
			if (newShip == null)
			{
				Log.Error("Trying to add null ship", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMArmyDataEx.cs", 149);
				return;
			}
			this.TryCollectUnit(newShip.m_UnitID);
			if (this.m_dicMyShip.ContainsKey(newShip.m_UnitUID))
			{
				Log.Error("Trying to add duplicated ship", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMArmyDataEx.cs", 161);
				return;
			}
			this.m_dicMyShip.Add(newShip.m_UnitUID, newShip);
			NKMArmyData.OnUnitUpdate onUnitUpdate = this.dOnUnitUpdate;
			if (onUnitUpdate == null)
			{
				return;
			}
			onUnitUpdate(NKMUserData.eChangeNotifyType.Add, NKM_UNIT_TYPE.NUT_SHIP, newShip.m_UnitUID, newShip);
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00060B14 File Offset: 0x0005ED14
		public void AddNewOperator(NKMOperator newOperator)
		{
			if (newOperator == null)
			{
				Log.Error("Trying to add null operator", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMArmyDataEx.cs", 171);
				return;
			}
			this.TryCollectUnit(newOperator.id);
			if (this.m_dicMyOperator.ContainsKey(newOperator.uid))
			{
				Log.Error("Trying to add duplicated operator", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMArmyDataEx.cs", 183);
				return;
			}
			this.m_dicMyOperator.Add(newOperator.uid, newOperator);
			NKMArmyData.OnOperatorUpdate onOperatorUpdate = this.dOnOperatorUpdate;
			if (onOperatorUpdate == null)
			{
				return;
			}
			onOperatorUpdate(NKMUserData.eChangeNotifyType.Add, newOperator.uid, newOperator);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00060B9C File Offset: 0x0005ED9C
		public int GetUnitCollectCount(IEnumerable<int> unitIDList)
		{
			int num = 0;
			foreach (int unitID in unitIDList)
			{
				if (this.IsCollectedUnit(unitID))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00060BF0 File Offset: 0x0005EDF0
		public bool IsCollectedUnit(int unitID)
		{
			return this.m_illustrateUnit.Contains(unitID);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00060C00 File Offset: 0x0005EE00
		public bool TryCollectUnit(int unitID)
		{
			if (this.IsCollectedUnit(unitID))
			{
				return false;
			}
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitID);
			if (nkmunitTempletBase != null && nkmunitTempletBase.IsRearmUnit)
			{
				this.m_illustrateUnit.Add(nkmunitTempletBase.m_BaseUnitID);
			}
			this.m_illustrateUnit.Add(unitID);
			return true;
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x00060C4C File Offset: 0x0005EE4C
		public void UpdateUnitData(List<NKMUnitData> lstUnitData)
		{
			foreach (NKMUnitData unitData in lstUnitData)
			{
				this.UpdateUnitData(unitData);
			}
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00060C9C File Offset: 0x0005EE9C
		public void UpdateUnitData(NKMUnitData UnitData)
		{
			if (UnitData == null)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(UnitData);
			if (unitTempletBase != null && unitTempletBase.IsTrophy)
			{
				if (!this.m_dicMyTrophy.ContainsKey(UnitData.m_UnitUID))
				{
					Log.Error("Tried to update nonexist unit", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMArmyDataEx.cs", 245);
					return;
				}
				this.m_dicMyTrophy[UnitData.m_UnitUID] = UnitData;
			}
			else
			{
				if (!this.m_dicMyUnit.ContainsKey(UnitData.m_UnitUID))
				{
					Log.Error("Tried to update nonexist unit", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMArmyDataEx.cs", 254);
					return;
				}
				this.m_dicMyUnit[UnitData.m_UnitUID] = UnitData;
			}
			NKMArmyData.OnUnitUpdate onUnitUpdate = this.dOnUnitUpdate;
			if (onUnitUpdate == null)
			{
				return;
			}
			onUnitUpdate(NKMUserData.eChangeNotifyType.Update, NKM_UNIT_TYPE.NUT_NORMAL, UnitData.m_UnitUID, UnitData);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x00060D50 File Offset: 0x0005EF50
		public void UpdateShipData(NKMUnitData ShipData)
		{
			if (ShipData == null)
			{
				return;
			}
			if (!this.m_dicMyShip.ContainsKey(ShipData.m_UnitUID))
			{
				Log.Error("Tried to update nonexist ship", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMArmyDataEx.cs", 270);
				return;
			}
			this.m_dicMyShip[ShipData.m_UnitUID] = ShipData;
			NKMArmyData.OnUnitUpdate onUnitUpdate = this.dOnUnitUpdate;
			if (onUnitUpdate == null)
			{
				return;
			}
			onUnitUpdate(NKMUserData.eChangeNotifyType.Update, NKM_UNIT_TYPE.NUT_SHIP, ShipData.m_UnitUID, ShipData);
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00060DB4 File Offset: 0x0005EFB4
		public bool UpdateOperatorData(NKMOperator OperatorData)
		{
			bool flag = false;
			if (OperatorData == null)
			{
				return false;
			}
			if (!this.m_dicMyOperator.ContainsKey(OperatorData.uid))
			{
				Log.Error("Tried to update nonexist operator", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMArmyDataEx.cs", 285);
				return false;
			}
			if (this.m_dicMyOperator[OperatorData.uid].level < OperatorData.level)
			{
				flag = true;
			}
			flag = ((((flag || this.m_dicMyOperator[OperatorData.uid].mainSkill.level >= OperatorData.mainSkill.level) && this.m_dicMyOperator[OperatorData.uid].subSkill.id != OperatorData.subSkill.id) || this.m_dicMyOperator[OperatorData.uid].subSkill.level >= OperatorData.subSkill.level) && this.m_dicMyOperator[OperatorData.uid].subSkill.exp < OperatorData.subSkill.exp);
			this.m_dicMyOperator[OperatorData.uid] = OperatorData;
			NKMArmyData.OnOperatorUpdate onOperatorUpdate = this.dOnOperatorUpdate;
			if (onOperatorUpdate != null)
			{
				onOperatorUpdate(NKMUserData.eChangeNotifyType.Update, OperatorData.uid, OperatorData);
			}
			return flag;
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00060F00 File Offset: 0x0005F100
		public void UpdateTrophyData(NKMUnitData trophyData)
		{
			if (trophyData == null)
			{
				return;
			}
			if (!this.m_dicMyTrophy.ContainsKey(trophyData.m_UnitUID))
			{
				Log.Error("Tried to update nonexist trophy", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMArmyDataEx.cs", 311);
				return;
			}
			this.m_dicMyTrophy[trophyData.m_UnitUID] = trophyData;
			NKMArmyData.OnUnitUpdate onUnitUpdate = this.dOnUnitUpdate;
			if (onUnitUpdate == null)
			{
				return;
			}
			onUnitUpdate(NKMUserData.eChangeNotifyType.Update, NKM_UNIT_TYPE.NUT_NORMAL, trophyData.m_UnitUID, trophyData);
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00060F64 File Offset: 0x0005F164
		public void RemoveUnitOrShip(long unitUid)
		{
			if (this.m_dicMyUnit.ContainsKey(unitUid))
			{
				this.RemoveUnit(unitUid);
				return;
			}
			if (this.m_dicMyShip.ContainsKey(unitUid))
			{
				this.RemoveShip(unitUid);
				return;
			}
			if (this.m_dicMyTrophy.ContainsKey(unitUid))
			{
				this.RemoveTrophy(unitUid);
				return;
			}
			Log.Error(string.Format("{0} 에 해당하는 유닛/함선이 없음", unitUid), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKMEx/NKMArmyDataEx.cs", 329);
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00060FD2 File Offset: 0x0005F1D2
		public void RemoveUnit(long unitUID)
		{
			this.m_dicMyUnit.Remove(unitUID);
			this.m_dicMyTrophy.Remove(unitUID);
			NKMArmyData.OnUnitUpdate onUnitUpdate = this.dOnUnitUpdate;
			if (onUnitUpdate == null)
			{
				return;
			}
			onUnitUpdate(NKMUserData.eChangeNotifyType.Remove, NKM_UNIT_TYPE.NUT_NORMAL, unitUID, null);
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00061004 File Offset: 0x0005F204
		public void RemoveUnitList(IEnumerable<long> lstUnitUID)
		{
			foreach (long key in lstUnitUID)
			{
				this.m_dicMyUnit.Remove(key);
				this.m_dicMyTrophy.Remove(key);
			}
			NKMArmyData.OnUnitUpdate onUnitUpdate = this.dOnUnitUpdate;
			if (onUnitUpdate == null)
			{
				return;
			}
			onUnitUpdate(NKMUserData.eChangeNotifyType.Remove, NKM_UNIT_TYPE.NUT_NORMAL, 0L, null);
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00061074 File Offset: 0x0005F274
		public void RemoveShip(long shipUID)
		{
			this.m_dicMyShip.Remove(shipUID);
			if (NKCScenManager.CurrentUserData().GetShipCandidateData().shipUid == shipUID)
			{
				NKCPacketSender.Send_NKMPacket_SHIP_SLOT_OPTION_CANCEL_REQ();
			}
			NKMArmyData.OnUnitUpdate onUnitUpdate = this.dOnUnitUpdate;
			if (onUnitUpdate == null)
			{
				return;
			}
			onUnitUpdate(NKMUserData.eChangeNotifyType.Remove, NKM_UNIT_TYPE.NUT_SHIP, shipUID, null);
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x000610AE File Offset: 0x0005F2AE
		public void RemoveOperatorEx(long operatorUID)
		{
			this.RemoveOperator(operatorUID);
			NKMArmyData.OnOperatorUpdate onOperatorUpdate = this.dOnOperatorUpdate;
			if (onOperatorUpdate == null)
			{
				return;
			}
			onOperatorUpdate(NKMUserData.eChangeNotifyType.Remove, operatorUID, null);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000610CA File Offset: 0x0005F2CA
		public void RemoveTrophy(long unitUID)
		{
			this.m_dicMyTrophy.Remove(unitUID);
			NKMArmyData.OnUnitUpdate onUnitUpdate = this.dOnUnitUpdate;
			if (onUnitUpdate == null)
			{
				return;
			}
			onUnitUpdate(NKMUserData.eChangeNotifyType.Remove, NKM_UNIT_TYPE.NUT_NORMAL, unitUID, null);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x000610ED File Offset: 0x0005F2ED
		public void DeckUpdated(NKMDeckIndex deckIndex, NKMDeckData deckData)
		{
			if (deckData != null)
			{
				NKMArmyData.OnDeckUpdate onDeckUpdate = this.dOnDeckUpdate;
				if (onDeckUpdate == null)
				{
					return;
				}
				onDeckUpdate(deckIndex, deckData);
			}
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00061104 File Offset: 0x0005F304
		public void RemoveUnitInDeckByUnitUid(long unitUid)
		{
			NKMDeckSet[] array = this.deckSets;
			for (int i = 0; i < array.Length; i++)
			{
				foreach (NKMDeckData nkmdeckData in array[i].Values)
				{
					int index;
					if (nkmdeckData.HasUnitUid(unitUid, out index))
					{
						nkmdeckData.m_listDeckUnitUID[index] = 0L;
					}
				}
			}
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0006117C File Offset: 0x0005F37C
		public void GetDeckList(NKM_DECK_TYPE eType, int slotIndex, ref List<long> unitList)
		{
			NKMDeckData deckData = this.GetDeckData(eType, slotIndex);
			if (deckData != null)
			{
				for (int i = 0; i < deckData.m_listDeckUnitUID.Count; i++)
				{
					if (deckData.m_listDeckUnitUID[i] != 0L)
					{
						unitList.Add(deckData.m_listDeckUnitUID[i]);
					}
				}
			}
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x000611CC File Offset: 0x0005F3CC
		public void SetDeckUnitByIndex(NKMDeckIndex deckIndex, byte slotIndex, long unitUID)
		{
			this.SetDeckUnitByIndex(deckIndex.m_eDeckType, (int)deckIndex.m_iIndex, slotIndex, unitUID);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x000611E2 File Offset: 0x0005F3E2
		public void SetDeckUnitByIndex(NKM_DECK_TYPE eType, int deckIndex, byte slotIndex, long unitUID)
		{
			if (this.m_dicMyUnit.ContainsKey(unitUID) || unitUID == 0L)
			{
				this.GetDeckData(eType, deckIndex).SetUnitUID(slotIndex, unitUID);
			}
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00061208 File Offset: 0x0005F408
		public void SetDeckOperatorByIndex(NKM_DECK_TYPE eType, int deckIndex, long unitUID)
		{
			if (this.m_dicMyOperator.ContainsKey(unitUID) || unitUID == 0L)
			{
				NKMDeckData deckData = this.GetDeckData(eType, deckIndex);
				if (deckData != null)
				{
					deckData.SetOperatorUID(unitUID);
				}
			}
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0006123A File Offset: 0x0005F43A
		public void SetDeckLeader(NKMDeckIndex deckIndex, sbyte leaderSlotIndex)
		{
			if (deckIndex.m_eDeckType == NKM_DECK_TYPE.NDT_NONE)
			{
				return;
			}
			this.GetDeckData(deckIndex.m_eDeckType, (int)deckIndex.m_iIndex).m_LeaderIndex = leaderSlotIndex;
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00061260 File Offset: 0x0005F460
		public Dictionary<NKMDeckIndex, NKMDeckData> GetAllDecks()
		{
			Dictionary<NKMDeckIndex, NKMDeckData> dictionary = new Dictionary<NKMDeckIndex, NKMDeckData>();
			foreach (object obj in Enum.GetValues(typeof(NKM_DECK_TYPE)))
			{
				NKM_DECK_TYPE nkm_DECK_TYPE = (NKM_DECK_TYPE)obj;
				int deckCount = this.GetDeckCount(nkm_DECK_TYPE);
				for (int i = 0; i < deckCount; i++)
				{
					NKMDeckIndex nkmdeckIndex = new NKMDeckIndex(nkm_DECK_TYPE, i);
					dictionary.Add(nkmdeckIndex, this.GetDeckData(nkmdeckIndex));
				}
			}
			return dictionary;
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x000612F8 File Offset: 0x0005F4F8
		public int GetAvailableDeckIndex(NKM_DECK_TYPE eType)
		{
			return this.GetDeckSet(eType).GetAvailableDeckIndex(this);
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00061308 File Offset: 0x0005F508
		public NKMDeckIndex GetDeckIndexByUnitUID(NKM_DECK_TYPE deckType, long unitUID)
		{
			if (unitUID == 0L)
			{
				return NKMDeckIndex.None;
			}
			NKMDeckIndex result;
			this.GetDeckSet(deckType).FindDeckIndexByUnitUid(unitUID, out result);
			return result;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0006132F File Offset: 0x0005F52F
		public bool IsHaveUnitFromUID(long unitUID)
		{
			return this.m_dicMyUnit.ContainsKey(unitUID);
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0006133D File Offset: 0x0005F53D
		public bool IsHaveShipFromUID(long shipUID)
		{
			return this.m_dicMyShip.ContainsKey(shipUID);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x0006134B File Offset: 0x0005F54B
		public bool IsHaveOperatorFromUID(long operatorUID)
		{
			return this.m_dicMyOperator.ContainsKey(operatorUID);
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0006135C File Offset: 0x0005F55C
		public int GetSameKindShipCountFromID(int shipID)
		{
			int num = 0;
			foreach (NKMUnitData nkmunitData in this.m_dicMyShip.Values)
			{
				if (NKMShipManager.IsSameKindShip(shipID, nkmunitData.m_UnitID))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x000613C4 File Offset: 0x0005F5C4
		public static bool IsAllowedSameUnitInMultipleDeck(NKM_DECK_TYPE eType)
		{
			return eType != NKM_DECK_TYPE.NDT_NORMAL;
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x000613D0 File Offset: 0x0005F5D0
		public List<byte> GetValidDeckIndexByOperationPowerDecendSort(NKM_DECK_TYPE eNKM_DECK_TYPE)
		{
			int deckCount = this.GetDeckCount(eNKM_DECK_TYPE);
			List<byte> list = new List<byte>();
			List<NKMArmyData.DeckIndexWithAvgOperationPower> list2 = new List<NKMArmyData.DeckIndexWithAvgOperationPower>();
			byte b = 0;
			while ((int)b < deckCount)
			{
				NKMDeckIndex nkmdeckIndex = new NKMDeckIndex(eNKM_DECK_TYPE, (int)b);
				if (NKMMain.IsValidDeck(this, nkmdeckIndex) == NKM_ERROR_CODE.NEC_OK)
				{
					list2.Add(new NKMArmyData.DeckIndexWithAvgOperationPower
					{
						m_Index = b,
						m_AvgOperationPower = this.GetArmyAvarageOperationPower(nkmdeckIndex, false, null, null)
					});
				}
				b += 1;
			}
			list2.Sort();
			for (int i = 0; i < list2.Count; i++)
			{
				list.Add(list2[i].m_Index);
			}
			return list;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x0006146C File Offset: 0x0005F66C
		public bool HaveUnit(int unitID, bool bIncludeRearm)
		{
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitID);
			if (nkmunitTempletBase != null && nkmunitTempletBase.IsTrophy)
			{
				using (Dictionary<long, NKMUnitData>.ValueCollection.Enumerator enumerator = this.m_dicMyTrophy.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.m_UnitID == unitID)
						{
							return true;
						}
					}
					return false;
				}
			}
			foreach (NKMUnitData nkmunitData in this.m_dicMyUnit.Values)
			{
				if (nkmunitData.m_UnitID == unitID)
				{
					return true;
				}
				if (bIncludeRearm && nkmunitTempletBase.IsSameBaseUnit(nkmunitData.m_UnitID))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00061540 File Offset: 0x0005F740
		public int GetUnitCountByID(int unitID)
		{
			int num = 0;
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitID);
			if (nkmunitTempletBase != null && nkmunitTempletBase.IsTrophy)
			{
				using (Dictionary<long, NKMUnitData>.ValueCollection.Enumerator enumerator = this.m_dicMyTrophy.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.m_UnitID == unitID)
						{
							num++;
						}
					}
					return num;
				}
			}
			using (Dictionary<long, NKMUnitData>.ValueCollection.Enumerator enumerator = this.m_dicMyUnit.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.m_UnitID == unitID)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x000615FC File Offset: 0x0005F7FC
		public int GetOperatorCountByID(int unitID)
		{
			int num = 0;
			using (Dictionary<long, NKMOperator>.ValueCollection.Enumerator enumerator = this.m_dicMyOperator.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.id == unitID)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0006165C File Offset: 0x0005F85C
		public List<NKMUnitData> GetUnitListByUnitID(int unitID)
		{
			List<NKMUnitData> list = new List<NKMUnitData>();
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitID);
			if (nkmunitTempletBase != null && nkmunitTempletBase.IsTrophy)
			{
				using (Dictionary<long, NKMUnitData>.ValueCollection.Enumerator enumerator = this.m_dicMyTrophy.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKMUnitData nkmunitData = enumerator.Current;
						if (nkmunitData.m_UnitID == unitID)
						{
							list.Add(nkmunitData);
						}
					}
					return list;
				}
			}
			foreach (NKMUnitData nkmunitData2 in this.m_dicMyUnit.Values)
			{
				if (nkmunitData2.m_UnitID == unitID)
				{
					list.Add(nkmunitData2);
				}
			}
			return list;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x0006172C File Offset: 0x0005F92C
		public int GetUnitTypeCount()
		{
			HashSet<int> hashSet = new HashSet<int>();
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair in this.m_dicMyUnit)
			{
				NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(keyValuePair.Value.m_UnitID);
				if (nkmunitTempletBase != null && NKMUnitManager.CanUnitUsedInDeck(nkmunitTempletBase) && this.CanBeDraftPickCandidate(nkmunitTempletBase) && !hashSet.Contains(keyValuePair.Value.m_UnitID))
				{
					hashSet.Add(keyValuePair.Value.m_UnitID);
				}
			}
			return hashSet.Count;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x000617D4 File Offset: 0x0005F9D4
		private bool CanBeDraftPickCandidate(NKMUnitTempletBase templetBase)
		{
			return NKCCollectionManager.GetUnitTemplet(templetBase.m_UnitID) != null && (templetBase.IsUnitStyleType() && templetBase.CollectionEnableByTag && templetBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL) && !templetBase.m_bMonster;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x0006180C File Offset: 0x0005FA0C
		public int GetShipTypeCount()
		{
			HashSet<int> hashSet = new HashSet<int>();
			foreach (KeyValuePair<long, NKMUnitData> keyValuePair in this.m_dicMyShip)
			{
				if (!hashSet.Contains(keyValuePair.Value.m_UnitID))
				{
					hashSet.Add(keyValuePair.Value.m_UnitID);
				}
			}
			return hashSet.Count;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0006188C File Offset: 0x0005FA8C
		public void InitUnitDelete()
		{
			this.listUnitDelete.Clear();
			this.listUnitDeleteReward.Clear();
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x000618A4 File Offset: 0x0005FAA4
		public void SetUnitDeleteList(List<long> list)
		{
			this.listUnitDelete.AddRange(list);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x000618B4 File Offset: 0x0005FAB4
		public List<long> GetUnitDeleteList()
		{
			List<long> list = new List<long>();
			if (this.listUnitDelete.Count > 100)
			{
				list = this.listUnitDelete.GetRange(0, 100);
				this.listUnitDelete.RemoveRange(0, 100);
			}
			else
			{
				list.AddRange(this.listUnitDelete);
				this.listUnitDelete.Clear();
			}
			return list;
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x0006190D File Offset: 0x0005FB0D
		public bool IsEmptyUnitDeleteList
		{
			get
			{
				return this.listUnitDelete.Count == 0;
			}
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0006191D File Offset: 0x0005FB1D
		public void AddUnitDeleteRewardList(List<NKMItemMiscData> list)
		{
			this.listUnitDeleteReward.AddRange(list);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0006192B File Offset: 0x0005FB2B
		public List<NKMItemMiscData> GetUnitDeleteReward()
		{
			return this.listUnitDeleteReward;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00061934 File Offset: 0x0005FB34
		public static bool IsAllUnitsEquipedAllGears(NKMDeckIndex _targetDeck)
		{
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
			if (armyData == null || inventoryData == null)
			{
				return false;
			}
			for (int i = 0; i < 8; i++)
			{
				NKMUnitData deckUnitByIndex = armyData.GetDeckUnitByIndex(_targetDeck, i);
				if (deckUnitByIndex != null)
				{
					if (inventoryData.GetItemEquip(deckUnitByIndex.GetEquipItemWeaponUid()) == null)
					{
						return false;
					}
					if (inventoryData.GetItemEquip(deckUnitByIndex.GetEquipItemDefenceUid()) == null)
					{
						return false;
					}
					if (inventoryData.GetItemEquip(deckUnitByIndex.GetEquipItemAccessoryUid()) == null)
					{
						return false;
					}
					if (deckUnitByIndex.IsUnlockAccessory2() && inventoryData.GetItemEquip(deckUnitByIndex.GetEquipItemAccessory2Uid()) == null)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04000FFD RID: 4093
		[DataMember]
		private NKMDeckSet[] deckSets = new NKMDeckSet[EnumUtil<NKM_DECK_TYPE>.Count];

		// Token: 0x04000FFE RID: 4094
		[DataMember]
		private Dictionary<int, NKMTeamCollectionData> m_dicTeamCollectionData = new Dictionary<int, NKMTeamCollectionData>();

		// Token: 0x04000FFF RID: 4095
		private NKMUserData owner;

		// Token: 0x04001000 RID: 4096
		public int m_MaxUnitCount = 200;

		// Token: 0x04001001 RID: 4097
		public int m_MaxShipCount = 10;

		// Token: 0x04001002 RID: 4098
		public int m_MaxOperatorCount = 10;

		// Token: 0x04001003 RID: 4099
		public int m_MaxTrophyCount = 2000;

		// Token: 0x04001004 RID: 4100
		[DataMember]
		public Dictionary<long, NKMUnitData> m_dicMyShip = new Dictionary<long, NKMUnitData>();

		// Token: 0x04001005 RID: 4101
		[DataMember]
		public Dictionary<long, NKMUnitData> m_dicMyUnit = new Dictionary<long, NKMUnitData>();

		// Token: 0x04001006 RID: 4102
		[DataMember]
		public Dictionary<long, NKMOperator> m_dicMyOperator = new Dictionary<long, NKMOperator>();

		// Token: 0x04001007 RID: 4103
		[DataMember]
		public Dictionary<long, NKMUnitData> m_dicMyTrophy = new Dictionary<long, NKMUnitData>();

		// Token: 0x04001008 RID: 4104
		[DataMember]
		public HashSet<int> m_illustrateUnit = new HashSet<int>();

		// Token: 0x04001009 RID: 4105
		public NKMArmyData.OnUnitUpdate dOnUnitUpdate;

		// Token: 0x0400100A RID: 4106
		public NKMArmyData.OnDeckUpdate dOnDeckUpdate;

		// Token: 0x0400100B RID: 4107
		public NKMArmyData.OnOperatorUpdate dOnOperatorUpdate;

		// Token: 0x0400100C RID: 4108
		private List<long> listUnitDelete = new List<long>();

		// Token: 0x0400100D RID: 4109
		private List<NKMItemMiscData> listUnitDeleteReward = new List<NKMItemMiscData>();

		// Token: 0x0400100E RID: 4110
		private const int UNIT_DELETE_COUNT = 100;

		// Token: 0x0200119B RID: 4507
		public enum UNIT_SEARCH_OPTION
		{
			// Token: 0x040092B7 RID: 37559
			None,
			// Token: 0x040092B8 RID: 37560
			Level,
			// Token: 0x040092B9 RID: 37561
			LimitLevel,
			// Token: 0x040092BA RID: 37562
			Devotion,
			// Token: 0x040092BB RID: 37563
			StarGrade
		}

		// Token: 0x0200119C RID: 4508
		// (Invoke) Token: 0x0600A027 RID: 40999
		public delegate void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData);

		// Token: 0x0200119D RID: 4509
		// (Invoke) Token: 0x0600A02B RID: 41003
		public delegate void OnDeckUpdate(NKMDeckIndex deckIndex, NKMDeckData deckData);

		// Token: 0x0200119E RID: 4510
		// (Invoke) Token: 0x0600A02F RID: 41007
		public delegate void OnOperatorUpdate(NKMUserData.eChangeNotifyType eEventType, long uid, NKMOperator operatorData);

		// Token: 0x0200119F RID: 4511
		private class DeckIndexWithAvgOperationPower : IComparable<NKMArmyData.DeckIndexWithAvgOperationPower>
		{
			// Token: 0x0600A032 RID: 41010 RVA: 0x0033DB8F File Offset: 0x0033BD8F
			public int CompareTo(NKMArmyData.DeckIndexWithAvgOperationPower other)
			{
				if (this.m_AvgOperationPower > other.m_AvgOperationPower)
				{
					return -1;
				}
				if (other.m_AvgOperationPower > this.m_AvgOperationPower)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x040092BC RID: 37564
			public byte m_Index;

			// Token: 0x040092BD RID: 37565
			public int m_AvgOperationPower;
		}
	}
}

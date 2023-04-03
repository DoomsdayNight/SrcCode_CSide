using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Templet;

namespace NKM
{
	// Token: 0x020003DF RID: 991
	public class NKMDeckCondition
	{
		// Token: 0x06001A2F RID: 6703 RVA: 0x00070529 File Offset: 0x0006E729
		public static bool IsMultiCountDeckCondition(NKMDeckCondition.DECK_CONDITION condition)
		{
			return condition == NKMDeckCondition.DECK_CONDITION.UNIT_GRADE_COUNT;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00070533 File Offset: 0x0006E733
		public void AddAllDeckCondition(NKMDeckCondition.SingleCondition allCondition)
		{
			if (this.m_dicAllDeckCondition == null)
			{
				this.m_dicAllDeckCondition = new Dictionary<NKMDeckCondition.DECK_CONDITION, NKMDeckCondition.SingleCondition>();
			}
			this.m_dicAllDeckCondition.Add(allCondition.eCondition, allCondition);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0007055C File Offset: 0x0006E75C
		public NKMDeckCondition.SingleCondition GetAllDeckCondition(NKMDeckCondition.DECK_CONDITION condition)
		{
			if (this.m_dicAllDeckCondition == null)
			{
				return null;
			}
			NKMDeckCondition.SingleCondition result;
			if (this.m_dicAllDeckCondition.TryGetValue(condition, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00070588 File Offset: 0x0006E788
		public NKMDeckCondition.GameCondition GetGameCondition(NKMDeckCondition.GAME_CONDITION type)
		{
			NKMDeckCondition.GameCondition result;
			if (this.m_dicGameCondition.TryGetValue(type, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x000705A8 File Offset: 0x0006E7A8
		public NKM_ERROR_CODE CheckDeckCondition(NKMArmyData armyData, NKMDeckData deckData)
		{
			new HashSet<int>();
			long shipUID = deckData.m_ShipUID;
			NKMUnitData shipFromUID = armyData.GetShipFromUID(shipUID);
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CheckUnitCondition(shipFromUID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				return nkm_ERROR_CODE;
			}
			new Dictionary<NKMDeckCondition.DECK_CONDITION, int>();
			for (int i = 0; i < deckData.m_listDeckUnitUID.Count; i++)
			{
				long unitUid = deckData.m_listDeckUnitUID[i];
				NKMUnitData unitFromUID = armyData.GetUnitFromUID(unitUid);
				NKM_ERROR_CODE nkm_ERROR_CODE2 = this.CheckUnitCondition(unitFromUID);
				if (nkm_ERROR_CODE2 != NKM_ERROR_CODE.NEC_OK)
				{
					return nkm_ERROR_CODE2;
				}
			}
			if (this.m_dicAllDeckCondition != null)
			{
				foreach (NKMDeckCondition.SingleCondition singleCondition in this.m_dicAllDeckCondition.Values)
				{
					int num = 0;
					for (int j = 0; j < deckData.m_listDeckUnitUID.Count; j++)
					{
						long unitUid2 = deckData.m_listDeckUnitUID[j];
						NKMUnitData unitFromUID2 = armyData.GetUnitFromUID(unitUid2);
						if (unitFromUID2 != null)
						{
							num += singleCondition.GetAllDeckConditionValue(unitFromUID2);
						}
					}
					if (!singleCondition.IsValueOk(num))
					{
						return singleCondition.GetFailErrorCode();
					}
				}
				return NKM_ERROR_CODE.NEC_OK;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x000706D0 File Offset: 0x0006E8D0
		public NKM_ERROR_CODE CheckUnitCondition(NKMUnitData unitData)
		{
			if (this.m_lstUnitCondition == null)
			{
				return NKM_ERROR_CODE.NEC_OK;
			}
			if (unitData == null)
			{
				return NKM_ERROR_CODE.NEC_OK;
			}
			if (NKMUnitTempletBase.Find(unitData.m_UnitID) == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DECK_UNIT_INVALID;
			}
			foreach (NKMDeckCondition.SingleCondition singleCondition in this.m_lstUnitCondition)
			{
				NKM_ERROR_CODE nkm_ERROR_CODE = singleCondition.IsValueOk(unitData);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					return nkm_ERROR_CODE;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0007074C File Offset: 0x0006E94C
		public NKM_ERROR_CODE CheckEventDeckCondition(NKMArmyData armyData, NKMDungeonEventDeckTemplet eventDeckTemplet, NKMEventDeckData eventDeckData, bool bOperatorEnabled)
		{
			Dictionary<int, long> dicUnit = eventDeckData.m_dicUnit;
			new HashSet<int>();
			long shipUID = eventDeckData.m_ShipUID;
			NKMUnitData shipFromUID = armyData.GetShipFromUID(shipUID);
			NKM_ERROR_CODE nkm_ERROR_CODE = this.CheckEventUnitCondition(shipFromUID, eventDeckTemplet.ShipSlot);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				return nkm_ERROR_CODE;
			}
			if (bOperatorEnabled)
			{
				NKMOperator operatorFromUId = armyData.GetOperatorFromUId(eventDeckData.m_OperatorUID);
				NKM_ERROR_CODE nkm_ERROR_CODE2 = this.CheckEventOperatorCondition(operatorFromUId, eventDeckTemplet.OperatorSlot);
				if (nkm_ERROR_CODE2 != NKM_ERROR_CODE.NEC_OK)
				{
					return nkm_ERROR_CODE2;
				}
			}
			for (int i = 0; i < 8; i++)
			{
				NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot = eventDeckTemplet.GetUnitSlot(i);
				long unitUid;
				if (!dicUnit.TryGetValue(i, out unitUid))
				{
					unitUid = 0L;
				}
				NKMUnitData unitFromUID = armyData.GetUnitFromUID(unitUid);
				NKM_ERROR_CODE nkm_ERROR_CODE3 = this.CheckEventUnitCondition(unitFromUID, unitSlot);
				if (nkm_ERROR_CODE3 != NKM_ERROR_CODE.NEC_OK)
				{
					return nkm_ERROR_CODE3;
				}
			}
			if (this.m_dicAllDeckCondition != null)
			{
				foreach (NKMDeckCondition.SingleCondition singleCondition in this.m_dicAllDeckCondition.Values)
				{
					int num = 0;
					for (int j = 0; j < 8; j++)
					{
						NKMDungeonEventDeckTemplet.EventDeckSlot unitSlot2 = eventDeckTemplet.GetUnitSlot(j);
						long unitUid2;
						if (!dicUnit.TryGetValue(j, out unitUid2))
						{
							unitUid2 = 0L;
						}
						NKMUnitData unitFromUID2 = armyData.GetUnitFromUID(unitUid2);
						switch (unitSlot2.m_eType)
						{
						case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE:
						case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_COUNTER:
						case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_SOLDIER:
						case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE_MECHANIC:
							if (unitFromUID2 != null)
							{
								num += singleCondition.GetAllDeckConditionValue(unitFromUID2);
							}
							break;
						case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED:
						case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_GUEST:
						case NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_NPC:
						{
							NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitSlot2.m_ID);
							num += singleCondition.GetAllDeckConditionValue(unitTempletBase);
							break;
						}
						}
					}
					if (!singleCondition.IsValueOk(num))
					{
						return singleCondition.GetFailErrorCode();
					}
				}
				return NKM_ERROR_CODE.NEC_OK;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00070908 File Offset: 0x0006EB08
		public NKM_ERROR_CODE CheckEventUnitCondition(NKMUnitData unitData, NKMDungeonEventDeckTemplet.EventDeckSlot eventDeckSlotData)
		{
			if (unitData == null)
			{
				return NKM_ERROR_CODE.NEC_OK;
			}
			List<NKMDeckCondition.SingleCondition> list = new List<NKMDeckCondition.SingleCondition>(this.AllConditionEnumerator());
			if (NKMUnitTempletBase.Find(unitData.m_UnitID) == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DECK_UNIT_INVALID;
			}
			foreach (NKMDeckCondition.SingleCondition singleCondition in list)
			{
				if (!this.SlotConditionCheck(singleCondition.eCondition, eventDeckSlotData.m_eType))
				{
					NKM_ERROR_CODE nkm_ERROR_CODE = singleCondition.IsValueOk(unitData);
					if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
					{
						return nkm_ERROR_CODE;
					}
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00070998 File Offset: 0x0006EB98
		public NKM_ERROR_CODE CheckEventOperatorCondition(NKMOperator operatorData, NKMDungeonEventDeckTemplet.EventDeckSlot eventDeckSlotData)
		{
			if (operatorData == null)
			{
				return NKM_ERROR_CODE.NEC_OK;
			}
			List<NKMDeckCondition.SingleCondition> list = new List<NKMDeckCondition.SingleCondition>(this.AllConditionEnumerator());
			if (NKMUnitManager.GetUnitTempletBase(operatorData) == null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DECK_UNIT_INVALID;
			}
			foreach (NKMDeckCondition.SingleCondition singleCondition in list)
			{
				if (singleCondition.IsConditionApplyToOperator() && !this.SlotConditionCheck(singleCondition.eCondition, eventDeckSlotData.m_eType))
				{
					NKM_ERROR_CODE nkm_ERROR_CODE = singleCondition.IsValueOk(operatorData);
					if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
					{
						return nkm_ERROR_CODE;
					}
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00070A2C File Offset: 0x0006EC2C
		private bool SlotConditionCheck(NKMDeckCondition.DECK_CONDITION eCondition, NKMDungeonEventDeckTemplet.SLOT_TYPE slotType)
		{
			switch (eCondition)
			{
			case NKMDeckCondition.DECK_CONDITION.UNIT_STYLE:
			case NKMDeckCondition.DECK_CONDITION.SHIP_STYLE:
				if (slotType > NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FREE && slotType - NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED <= 5)
				{
					return true;
				}
				break;
			case NKMDeckCondition.DECK_CONDITION.UNIT_GRADE:
			case NKMDeckCondition.DECK_CONDITION.UNIT_COST:
			case NKMDeckCondition.DECK_CONDITION.UNIT_ROLE:
			case NKMDeckCondition.DECK_CONDITION.UNIT_ID_NOT:
			case NKMDeckCondition.DECK_CONDITION.AWAKEN_COUNT:
			case NKMDeckCondition.DECK_CONDITION.UNIT_GROUND_COUNT:
			case NKMDeckCondition.DECK_CONDITION.UNIT_AIR_COUNT:
			case NKMDeckCondition.DECK_CONDITION.UNIT_GRADE_COUNT:
				if (slotType - NKMDungeonEventDeckTemplet.SLOT_TYPE.ST_FIXED <= 2)
				{
					return true;
				}
				break;
			case NKMDeckCondition.DECK_CONDITION.UNIT_COST_TOTAL:
				return true;
			}
			return false;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00070A8C File Offset: 0x0006EC8C
		public static bool ParseDeckCondition(string strCondition, out NKMDeckCondition deckCondition)
		{
			deckCondition = null;
			if (string.IsNullOrEmpty(strCondition))
			{
				return true;
			}
			char[] separator = new char[]
			{
				',',
				' ',
				'\t',
				'\n'
			};
			Queue<string> queue = new Queue<string>(strCondition.Split(separator, StringSplitOptions.RemoveEmptyEntries));
			deckCondition = new NKMDeckCondition();
			while (queue.Count > 0)
			{
				string data = queue.Dequeue();
				NKMDeckCondition.DECK_CONDITION condition;
				if (data.TryParse(out condition, true))
				{
					if (!NKMDeckCondition.ParseSingleCondition(condition, ref deckCondition, ref queue))
					{
						return false;
					}
				}
				else
				{
					NKMDeckCondition.GAME_CONDITION game_CONDITION;
					if (!data.TryParse(out game_CONDITION, true))
					{
						Log.Error("Parse Error - Unexpected Token : condition token parse failed", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 1035);
						return false;
					}
					NKMDeckCondition.GameCondition value;
					if (!NKMDeckCondition.ParseGameCondition(game_CONDITION, ref queue, out value))
					{
						return false;
					}
					deckCondition.m_dicGameCondition.Add(game_CONDITION, value);
				}
			}
			deckCondition.m_lstUnitCondition.Sort();
			return true;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00070B48 File Offset: 0x0006ED48
		private static bool ParseSingleCondition(NKMDeckCondition.DECK_CONDITION condition, ref NKMDeckCondition deckCondition, ref Queue<string> qTokens)
		{
			bool result;
			try
			{
				switch (condition)
				{
				case NKMDeckCondition.DECK_CONDITION.UNIT_COST_TOTAL:
				{
					NKMDeckCondition.SingleCondition singleCondition = new NKMDeckCondition.SingleCondition();
					singleCondition.eCondition = condition;
					bool flag = NKMDeckCondition.ProcessDeckConditionParse(qTokens, ref singleCondition, NKMDeckCondition.MORE_LESS_TYPE.ALL);
					deckCondition.AddAllDeckCondition(singleCondition);
					result = flag;
					break;
				}
				case NKMDeckCondition.DECK_CONDITION.AWAKEN_COUNT:
				{
					NKMDeckCondition.SingleCondition singleCondition2 = new NKMDeckCondition.SingleCondition();
					singleCondition2.eCondition = condition;
					bool flag2 = NKMDeckCondition.ProcessDeckConditionParse(qTokens, ref singleCondition2, NKMDeckCondition.MORE_LESS_TYPE.NO_NOT);
					deckCondition.AddAllDeckCondition(singleCondition2);
					result = flag2;
					break;
				}
				case NKMDeckCondition.DECK_CONDITION.UNIT_GROUND_COUNT:
				{
					NKMDeckCondition.SingleCondition singleCondition3 = new NKMDeckCondition.SingleCondition();
					singleCondition3.eCondition = condition;
					bool flag3 = NKMDeckCondition.ProcessDeckConditionParse(qTokens, ref singleCondition3, NKMDeckCondition.MORE_LESS_TYPE.ALL);
					deckCondition.AddAllDeckCondition(singleCondition3);
					result = flag3;
					break;
				}
				case NKMDeckCondition.DECK_CONDITION.UNIT_AIR_COUNT:
				{
					NKMDeckCondition.SingleCondition singleCondition4 = new NKMDeckCondition.SingleCondition();
					singleCondition4.eCondition = condition;
					bool flag4 = NKMDeckCondition.ProcessDeckConditionParse(qTokens, ref singleCondition4, NKMDeckCondition.MORE_LESS_TYPE.ALL);
					deckCondition.AddAllDeckCondition(singleCondition4);
					result = flag4;
					break;
				}
				case NKMDeckCondition.DECK_CONDITION.UNIT_GRADE_COUNT:
				{
					NKMDeckCondition.SingleCondition singleCondition5 = new NKMDeckCondition.SingleCondition();
					singleCondition5.eCondition = condition;
					bool flag5 = NKMDeckCondition.ProcessMultiCountDeckConditionParse<NKM_UNIT_GRADE>(qTokens, ref singleCondition5, NKMDeckCondition.MORE_LESS_TYPE.ALL);
					deckCondition.AddAllDeckCondition(singleCondition5);
					result = flag5;
					break;
				}
				default:
				{
					NKMDeckCondition.SingleCondition singleCondition6 = new NKMDeckCondition.SingleCondition();
					singleCondition6.eCondition = condition;
					switch (condition)
					{
					case NKMDeckCondition.DECK_CONDITION.UNIT_STYLE:
					case NKMDeckCondition.DECK_CONDITION.SHIP_STYLE:
						if (!NKMDeckCondition.ProcessDeckConditionParse<NKM_UNIT_STYLE_TYPE>(qTokens, ref singleCondition6, NKMDeckCondition.MORE_LESS_TYPE.EQUAL_OR_NOT))
						{
							return false;
						}
						break;
					case NKMDeckCondition.DECK_CONDITION.UNIT_GRADE:
						if (!NKMDeckCondition.ProcessDeckConditionParse<NKM_UNIT_GRADE>(qTokens, ref singleCondition6, NKMDeckCondition.MORE_LESS_TYPE.ALL))
						{
							return false;
						}
						break;
					case NKMDeckCondition.DECK_CONDITION.UNIT_COST:
					case NKMDeckCondition.DECK_CONDITION.UNIT_LEVEL:
					case NKMDeckCondition.DECK_CONDITION.SHIP_LEVEL:
						if (!NKMDeckCondition.ProcessDeckConditionParse(qTokens, ref singleCondition6, NKMDeckCondition.MORE_LESS_TYPE.ALL))
						{
							return false;
						}
						break;
					case NKMDeckCondition.DECK_CONDITION.UNIT_ROLE:
						if (!NKMDeckCondition.ProcessDeckConditionParse<NKM_UNIT_ROLE_TYPE>(qTokens, ref singleCondition6, NKMDeckCondition.MORE_LESS_TYPE.EQUAL_OR_NOT))
						{
							return false;
						}
						break;
					case NKMDeckCondition.DECK_CONDITION.UNIT_ID_NOT:
						if (!NKMDeckCondition.ProcessDeckConditionParse(qTokens, ref singleCondition6, NKMDeckCondition.MORE_LESS_TYPE.NO_USE))
						{
							return false;
						}
						singleCondition6.eMoreLess = NKMDeckCondition.MORE_LESS.NOT;
						break;
					}
					deckCondition.m_lstUnitCondition.Add(singleCondition6);
					result = true;
					break;
				}
				}
			}
			catch (InvalidOperationException)
			{
				Log.Error("Parse Error : deck condition detail token parse failed! token count not match.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 1144);
				result = false;
			}
			return result;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x00070D10 File Offset: 0x0006EF10
		private static bool ParseGameCondition(NKMDeckCondition.GAME_CONDITION condition, ref Queue<string> qTokens, out NKMDeckCondition.GameCondition gameCondition)
		{
			gameCondition = new NKMDeckCondition.GameCondition();
			gameCondition.eCondition = condition;
			int value;
			if (!int.TryParse(qTokens.Dequeue(), out value))
			{
				Log.Error("unexpected Token after " + gameCondition.eCondition.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 1163);
				return false;
			}
			gameCondition.Value = value;
			return true;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00070D78 File Offset: 0x0006EF78
		private static bool ProcessDeckConditionParse<T>(Queue<string> qTokens, ref NKMDeckCondition.SingleCondition deckCondition, NKMDeckCondition.MORE_LESS_TYPE mlType) where T : Enum
		{
			if (mlType != NKMDeckCondition.MORE_LESS_TYPE.NO_USE)
			{
				NKMDeckCondition.MORE_LESS more_LESS;
				if (!qTokens.Dequeue().TryParse(out more_LESS, false))
				{
					Log.Error("unexpected Token after " + deckCondition.eCondition.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 1188);
					return false;
				}
				switch (mlType)
				{
				case NKMDeckCondition.MORE_LESS_TYPE.EQUAL_OR_NOT:
				case NKMDeckCondition.MORE_LESS_TYPE.BOOL:
					if (more_LESS != NKMDeckCondition.MORE_LESS.EQUAL && more_LESS != NKMDeckCondition.MORE_LESS.NOT)
					{
						Log.Error("Equal More or Less is not permitted for " + deckCondition.eCondition.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 1199);
						return false;
					}
					break;
				case NKMDeckCondition.MORE_LESS_TYPE.NO_NOT:
					if (more_LESS == NKMDeckCondition.MORE_LESS.NOT)
					{
						Log.Error("Equal More or Less is not permitted for " + deckCondition.eCondition.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 1209);
						return false;
					}
					break;
				}
				deckCondition.eMoreLess = more_LESS;
			}
			else
			{
				deckCondition.eMoreLess = NKMDeckCondition.MORE_LESS.EQUAL;
			}
			if (mlType == NKMDeckCondition.MORE_LESS_TYPE.BOOL)
			{
				return true;
			}
			T t;
			if (!qTokens.Dequeue().TryParse(out t, false))
			{
				Log.Error("unexpected Token after " + deckCondition.eCondition.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 1232);
				return false;
			}
			deckCondition.Value = Convert.ToInt32(t);
			if (deckCondition.eMoreLess != NKMDeckCondition.MORE_LESS.EQUAL)
			{
				if (deckCondition.eMoreLess != NKMDeckCondition.MORE_LESS.NOT)
				{
					return true;
				}
			}
			T t2;
			while (qTokens.Count > 0 && qTokens.Peek().TryParse(out t2, true))
			{
				if (deckCondition.lstValue == null)
				{
					deckCondition.lstValue = new List<int>();
					deckCondition.lstValue.Add(deckCondition.Value);
				}
				deckCondition.lstValue.Add(Convert.ToInt32(t2));
				qTokens.Dequeue();
			}
			return true;
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x00070F20 File Offset: 0x0006F120
		private static bool ProcessDeckConditionParse(Queue<string> qTokens, ref NKMDeckCondition.SingleCondition deckCondition, NKMDeckCondition.MORE_LESS_TYPE mlType)
		{
			if (mlType != NKMDeckCondition.MORE_LESS_TYPE.NO_USE)
			{
				NKMDeckCondition.MORE_LESS more_LESS;
				if (!qTokens.Dequeue().TryParse(out more_LESS, false))
				{
					Log.Error("unexpected Token after " + deckCondition.eCondition.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 1271);
					return false;
				}
				if ((mlType == NKMDeckCondition.MORE_LESS_TYPE.EQUAL_OR_NOT || mlType == NKMDeckCondition.MORE_LESS_TYPE.BOOL) && more_LESS != NKMDeckCondition.MORE_LESS.EQUAL && more_LESS != NKMDeckCondition.MORE_LESS.NOT)
				{
					Log.Error("Equal More or Less is not permitted for " + deckCondition.eCondition.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 1279);
					return false;
				}
				deckCondition.eMoreLess = more_LESS;
			}
			else
			{
				deckCondition.eMoreLess = NKMDeckCondition.MORE_LESS.EQUAL;
			}
			if (mlType == NKMDeckCondition.MORE_LESS_TYPE.BOOL)
			{
				return true;
			}
			int value;
			if (!int.TryParse(qTokens.Dequeue(), out value))
			{
				Log.Error("unexpected Token after " + deckCondition.eCondition.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 1297);
				return false;
			}
			deckCondition.Value = value;
			if (deckCondition.eMoreLess != NKMDeckCondition.MORE_LESS.EQUAL)
			{
				if (deckCondition.eMoreLess != NKMDeckCondition.MORE_LESS.NOT)
				{
					return true;
				}
			}
			int item;
			while (qTokens.Count > 0 && int.TryParse(qTokens.Peek(), out item))
			{
				if (deckCondition.lstValue == null)
				{
					deckCondition.lstValue = new List<int>();
					deckCondition.lstValue.Add(deckCondition.Value);
				}
				deckCondition.lstValue.Add(item);
				qTokens.Dequeue();
			}
			return true;
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00071074 File Offset: 0x0006F274
		private static bool ProcessMultiCountDeckConditionParse<T>(Queue<string> qTokens, ref NKMDeckCondition.SingleCondition deckCondition, NKMDeckCondition.MORE_LESS_TYPE mlType) where T : Enum
		{
			if (mlType == NKMDeckCondition.MORE_LESS_TYPE.NO_USE)
			{
				Log.Error("NO_USE type cannot used for condition " + deckCondition.eCondition.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 1332);
				return false;
			}
			deckCondition.lstValue = new List<int>();
			while (qTokens.Count > 0)
			{
				string data = qTokens.Dequeue();
				NKMDeckCondition.MORE_LESS eMoreLess;
				if (data.TryParse(out eMoreLess, true))
				{
					deckCondition.eMoreLess = eMoreLess;
					break;
				}
				T t;
				if (!data.TryParse(out t, false))
				{
					return false;
				}
				deckCondition.lstValue.Add(Convert.ToInt32(t));
			}
			int value;
			if (!int.TryParse(qTokens.Dequeue(), out value))
			{
				Log.Error("unexpected Token after " + deckCondition.eCondition.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 1362);
				return false;
			}
			deckCondition.Value = value;
			return true;
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x0007114D File Offset: 0x0006F34D
		public int ConditionCount
		{
			get
			{
				return ((this.m_dicAllDeckCondition != null) ? this.m_dicAllDeckCondition.Count : 0) + this.m_lstUnitCondition.Count;
			}
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00071171 File Offset: 0x0006F371
		public IEnumerable<NKMDeckCondition.SingleCondition> AllConditionEnumerator()
		{
			if (this.m_lstUnitCondition != null)
			{
				foreach (NKMDeckCondition.SingleCondition singleCondition in this.m_lstUnitCondition)
				{
					yield return singleCondition;
				}
				List<NKMDeckCondition.SingleCondition>.Enumerator enumerator = default(List<NKMDeckCondition.SingleCondition>.Enumerator);
			}
			if (this.m_dicAllDeckCondition != null)
			{
				foreach (NKMDeckCondition.SingleCondition singleCondition2 in this.m_dicAllDeckCondition.Values)
				{
					yield return singleCondition2;
				}
				Dictionary<NKMDeckCondition.DECK_CONDITION, NKMDeckCondition.SingleCondition>.ValueCollection.Enumerator enumerator2 = default(Dictionary<NKMDeckCondition.DECK_CONDITION, NKMDeckCondition.SingleCondition>.ValueCollection.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x04001321 RID: 4897
		public Dictionary<NKMDeckCondition.DECK_CONDITION, NKMDeckCondition.SingleCondition> m_dicAllDeckCondition;

		// Token: 0x04001322 RID: 4898
		public List<NKMDeckCondition.SingleCondition> m_lstUnitCondition = new List<NKMDeckCondition.SingleCondition>();

		// Token: 0x04001323 RID: 4899
		public Dictionary<NKMDeckCondition.GAME_CONDITION, NKMDeckCondition.GameCondition> m_dicGameCondition = new Dictionary<NKMDeckCondition.GAME_CONDITION, NKMDeckCondition.GameCondition>();

		// Token: 0x020011D5 RID: 4565
		public enum DECK_CONDITION
		{
			// Token: 0x0400937B RID: 37755
			UNIT_STYLE,
			// Token: 0x0400937C RID: 37756
			UNIT_GRADE,
			// Token: 0x0400937D RID: 37757
			UNIT_COST,
			// Token: 0x0400937E RID: 37758
			UNIT_ROLE,
			// Token: 0x0400937F RID: 37759
			UNIT_LEVEL,
			// Token: 0x04009380 RID: 37760
			SHIP_STYLE,
			// Token: 0x04009381 RID: 37761
			SHIP_LEVEL,
			// Token: 0x04009382 RID: 37762
			UNIT_ID_NOT,
			// Token: 0x04009383 RID: 37763
			UNIT_COST_TOTAL,
			// Token: 0x04009384 RID: 37764
			AWAKEN_COUNT,
			// Token: 0x04009385 RID: 37765
			UNIT_GROUND_COUNT,
			// Token: 0x04009386 RID: 37766
			UNIT_AIR_COUNT,
			// Token: 0x04009387 RID: 37767
			UNIT_GRADE_COUNT
		}

		// Token: 0x020011D6 RID: 4566
		public enum GAME_CONDITION
		{
			// Token: 0x04009389 RID: 37769
			LEVEL_CAP,
			// Token: 0x0400938A RID: 37770
			MODIFY_START_COST
		}

		// Token: 0x020011D7 RID: 4567
		public enum MORE_LESS
		{
			// Token: 0x0400938C RID: 37772
			EQUAL,
			// Token: 0x0400938D RID: 37773
			NOT,
			// Token: 0x0400938E RID: 37774
			MORE,
			// Token: 0x0400938F RID: 37775
			LESS
		}

		// Token: 0x020011D8 RID: 4568
		public class SingleCondition : IComparable<NKMDeckCondition.SingleCondition>
		{
			// Token: 0x0600A0A8 RID: 41128 RVA: 0x0033E548 File Offset: 0x0033C748
			public NKM_ERROR_CODE IsValueOk(NKMUnitData unitData)
			{
				if (unitData == null)
				{
					return NKM_ERROR_CODE.NEC_OK;
				}
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
				switch (this.eCondition)
				{
				case NKMDeckCondition.DECK_CONDITION.UNIT_STYLE:
				case NKMDeckCondition.DECK_CONDITION.UNIT_GRADE:
				case NKMDeckCondition.DECK_CONDITION.UNIT_COST:
				case NKMDeckCondition.DECK_CONDITION.UNIT_ROLE:
				case NKMDeckCondition.DECK_CONDITION.UNIT_LEVEL:
				case NKMDeckCondition.DECK_CONDITION.AWAKEN_COUNT:
				case NKMDeckCondition.DECK_CONDITION.UNIT_GROUND_COUNT:
				case NKMDeckCondition.DECK_CONDITION.UNIT_AIR_COUNT:
				case NKMDeckCondition.DECK_CONDITION.UNIT_GRADE_COUNT:
					if (unitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_NORMAL)
					{
						return NKM_ERROR_CODE.NEC_OK;
					}
					goto IL_AC;
				case NKMDeckCondition.DECK_CONDITION.SHIP_STYLE:
				case NKMDeckCondition.DECK_CONDITION.SHIP_LEVEL:
					if (unitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
					{
						return NKM_ERROR_CODE.NEC_OK;
					}
					goto IL_AC;
				case NKMDeckCondition.DECK_CONDITION.UNIT_ID_NOT:
				{
					NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(this.Value);
					if (unitTempletBase2 == null)
					{
						Log.Error(string.Format("Target Unit {0} not exist!", this.Value), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 293);
						return NKM_ERROR_CODE.NEC_OK;
					}
					if (unitTempletBase2.m_NKM_UNIT_TYPE != unitTempletBase.m_NKM_UNIT_TYPE)
					{
						return NKM_ERROR_CODE.NEC_OK;
					}
					goto IL_AC;
				}
				}
				return NKM_ERROR_CODE.NEC_OK;
				IL_AC:
				switch (this.eCondition)
				{
				case NKMDeckCondition.DECK_CONDITION.UNIT_STYLE:
					if (!this.IsValueOk((int)unitTempletBase.m_NKM_UNIT_STYLE_TYPE))
					{
						return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_UNIT_STYLE;
					}
					break;
				case NKMDeckCondition.DECK_CONDITION.UNIT_GRADE:
					if (!this.IsValueOk((int)unitTempletBase.m_NKM_UNIT_GRADE))
					{
						return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_UNIT_RARITY;
					}
					break;
				case NKMDeckCondition.DECK_CONDITION.UNIT_COST:
				{
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitData.m_UnitID);
					if (!this.IsValueOk(unitStatTemplet.GetRespawnCost(false, null, null)))
					{
						return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_UNIT_COST;
					}
					break;
				}
				case NKMDeckCondition.DECK_CONDITION.UNIT_ROLE:
					if (!this.IsValueOk((int)unitTempletBase.m_NKM_UNIT_ROLE_TYPE))
					{
						return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_UNIT_ROLE;
					}
					break;
				case NKMDeckCondition.DECK_CONDITION.UNIT_LEVEL:
					if (!this.IsValueOk(unitData.m_UnitLevel))
					{
						return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_UNIT_LEVEL;
					}
					break;
				case NKMDeckCondition.DECK_CONDITION.SHIP_STYLE:
					if (!this.IsValueOk((int)unitTempletBase.m_NKM_UNIT_STYLE_TYPE))
					{
						return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_SHIP_STYLE;
					}
					break;
				case NKMDeckCondition.DECK_CONDITION.SHIP_LEVEL:
					if (!this.IsValueOk(unitData.m_UnitLevel))
					{
						return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_SHIP_LEVEL;
					}
					break;
				case NKMDeckCondition.DECK_CONDITION.UNIT_ID_NOT:
					if (!this.IsValueOk(unitData.m_UnitID))
					{
						return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_UNIT_ID;
					}
					break;
				default:
					return NKM_ERROR_CODE.NEC_OK;
				case NKMDeckCondition.DECK_CONDITION.AWAKEN_COUNT:
					if (this.IsProhibited() && unitTempletBase.m_bAwaken)
					{
						return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_AWAKEN_COUNT;
					}
					break;
				case NKMDeckCondition.DECK_CONDITION.UNIT_GROUND_COUNT:
					if (this.IsProhibited() && !unitTempletBase.m_bAirUnit)
					{
						return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_UNIT_MOVE_TYPE;
					}
					break;
				case NKMDeckCondition.DECK_CONDITION.UNIT_AIR_COUNT:
					if (this.IsProhibited() && unitTempletBase.m_bAirUnit)
					{
						return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_UNIT_MOVE_TYPE;
					}
					break;
				case NKMDeckCondition.DECK_CONDITION.UNIT_GRADE_COUNT:
					if (this.IsProhibited() && !this.IsValueOk((int)unitTempletBase.m_NKM_UNIT_GRADE))
					{
						return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_UNIT_MOVE_TYPE;
					}
					break;
				}
				return NKM_ERROR_CODE.NEC_OK;
			}

			// Token: 0x0600A0A9 RID: 41129 RVA: 0x0033E764 File Offset: 0x0033C964
			public NKM_ERROR_CODE IsValueOk(NKMOperator operatorData)
			{
				if (!this.IsConditionApplyToOperator())
				{
					return NKM_ERROR_CODE.NEC_OK;
				}
				if (operatorData == null)
				{
					return NKM_ERROR_CODE.NEC_OK;
				}
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorData);
				if (this.eCondition != NKMDeckCondition.DECK_CONDITION.UNIT_ID_NOT)
				{
					return NKM_ERROR_CODE.NEC_OK;
				}
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(this.Value);
				if (unitTempletBase2 == null)
				{
					Log.Error(string.Format("Target Unit {0} not exist!", this.Value), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 419);
					return NKM_ERROR_CODE.NEC_OK;
				}
				if (unitTempletBase2.m_NKM_UNIT_TYPE != unitTempletBase.m_NKM_UNIT_TYPE)
				{
					return NKM_ERROR_CODE.NEC_OK;
				}
				if (!this.IsValueOk(operatorData.id))
				{
					return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_UNIT_ID;
				}
				return NKM_ERROR_CODE.NEC_OK;
			}

			// Token: 0x17001795 RID: 6037
			// (get) Token: 0x0600A0AA RID: 41130 RVA: 0x0033E7EC File Offset: 0x0033C9EC
			public bool IsAllDeckCondition
			{
				get
				{
					NKMDeckCondition.DECK_CONDITION deck_CONDITION = this.eCondition;
					return deck_CONDITION - NKMDeckCondition.DECK_CONDITION.UNIT_COST_TOTAL <= 4;
				}
			}

			// Token: 0x0600A0AB RID: 41131 RVA: 0x0033E80C File Offset: 0x0033CA0C
			public int GetAllDeckConditionValue(NKMUnitTempletBase unitTempletBase)
			{
				if (unitTempletBase == null)
				{
					return 0;
				}
				switch (this.eCondition)
				{
				case NKMDeckCondition.DECK_CONDITION.UNIT_COST_TOTAL:
					return NKMUnitManager.GetUnitStatTemplet(unitTempletBase.m_UnitID).GetRespawnCost(false, null, null);
				case NKMDeckCondition.DECK_CONDITION.AWAKEN_COUNT:
					if (!unitTempletBase.m_bAwaken)
					{
						return 0;
					}
					return 1;
				case NKMDeckCondition.DECK_CONDITION.UNIT_GROUND_COUNT:
					if (!unitTempletBase.m_bAirUnit)
					{
						return 1;
					}
					return 0;
				case NKMDeckCondition.DECK_CONDITION.UNIT_AIR_COUNT:
					if (!unitTempletBase.m_bAirUnit)
					{
						return 0;
					}
					return 1;
				case NKMDeckCondition.DECK_CONDITION.UNIT_GRADE_COUNT:
					if (!this.lstValue.Contains((int)unitTempletBase.m_NKM_UNIT_GRADE))
					{
						return 0;
					}
					return 1;
				default:
					return 0;
				}
			}

			// Token: 0x0600A0AC RID: 41132 RVA: 0x0033E894 File Offset: 0x0033CA94
			public int GetAllDeckConditionValue(NKMUnitData unitData)
			{
				if (unitData == null)
				{
					return 0;
				}
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
				return this.GetAllDeckConditionValue(unitTempletBase);
			}

			// Token: 0x0600A0AD RID: 41133 RVA: 0x0033E8B4 File Offset: 0x0033CAB4
			public bool CanAddThisUnit(NKMUnitData unitData, int currentValue)
			{
				int allDeckConditionValue = this.GetAllDeckConditionValue(unitData);
				int num = currentValue + allDeckConditionValue;
				switch (this.eMoreLess)
				{
				case NKMDeckCondition.MORE_LESS.NOT:
					return num != this.Value;
				case NKMDeckCondition.MORE_LESS.MORE:
					return true;
				}
				return num <= this.Value;
			}

			// Token: 0x0600A0AE RID: 41134 RVA: 0x0033E908 File Offset: 0x0033CB08
			public bool IsValueOk(bool currentValue)
			{
				NKMDeckCondition.MORE_LESS more_LESS = this.eMoreLess;
				if (more_LESS == NKMDeckCondition.MORE_LESS.EQUAL)
				{
					return currentValue;
				}
				if (more_LESS != NKMDeckCondition.MORE_LESS.NOT)
				{
					Log.Error("bool Value with non EQUAL-NOT condition??", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMDungeonEventDeck.cs", 542);
					return false;
				}
				return !currentValue;
			}

			// Token: 0x0600A0AF RID: 41135 RVA: 0x0033E944 File Offset: 0x0033CB44
			public bool IsValueOk(int currentValue)
			{
				bool flag = NKMDeckCondition.IsMultiCountDeckCondition(this.eCondition);
				switch (this.eMoreLess)
				{
				default:
					if (!flag && this.lstValue != null)
					{
						for (int i = 0; i < this.lstValue.Count; i++)
						{
							if (this.lstValue[i] == currentValue)
							{
								return true;
							}
						}
						return false;
					}
					return currentValue == this.Value;
				case NKMDeckCondition.MORE_LESS.NOT:
					if (!flag && this.lstValue != null)
					{
						for (int j = 0; j < this.lstValue.Count; j++)
						{
							if (this.lstValue[j] == currentValue)
							{
								return false;
							}
						}
						return true;
					}
					return currentValue != this.Value;
				case NKMDeckCondition.MORE_LESS.MORE:
					return currentValue >= this.Value;
				case NKMDeckCondition.MORE_LESS.LESS:
					return currentValue <= this.Value;
				}
			}

			// Token: 0x0600A0B0 RID: 41136 RVA: 0x0033EA14 File Offset: 0x0033CC14
			public int CompareTo(NKMDeckCondition.SingleCondition other)
			{
				int num = this.eCondition.CompareTo(other.eCondition);
				if (num != 0)
				{
					return num;
				}
				int num2 = this.eMoreLess.CompareTo(other.eMoreLess);
				if (num2 != 0)
				{
					return num2;
				}
				return this.Value.CompareTo(other.Value);
			}

			// Token: 0x0600A0B1 RID: 41137 RVA: 0x0033EA76 File Offset: 0x0033CC76
			public bool IsProhibited()
			{
				if (this.IsAllDeckCondition)
				{
					if (this.eMoreLess == NKMDeckCondition.MORE_LESS.EQUAL || this.eMoreLess == NKMDeckCondition.MORE_LESS.LESS)
					{
						return this.Value == 0;
					}
				}
				else if (this.eMoreLess == NKMDeckCondition.MORE_LESS.NOT)
				{
					return true;
				}
				return false;
			}

			// Token: 0x0600A0B2 RID: 41138 RVA: 0x0033EAA7 File Offset: 0x0033CCA7
			public bool IsConditionApplyToOperator()
			{
				return this.eCondition == NKMDeckCondition.DECK_CONDITION.UNIT_ID_NOT;
			}

			// Token: 0x0600A0B3 RID: 41139 RVA: 0x0033EAB8 File Offset: 0x0033CCB8
			public NKM_ERROR_CODE GetFailErrorCode()
			{
				switch (this.eCondition)
				{
				case NKMDeckCondition.DECK_CONDITION.UNIT_COST_TOTAL:
					return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_UNIT_COST_TOTAL;
				case NKMDeckCondition.DECK_CONDITION.AWAKEN_COUNT:
					return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_AWAKEN_COUNT;
				case NKMDeckCondition.DECK_CONDITION.UNIT_GROUND_COUNT:
				case NKMDeckCondition.DECK_CONDITION.UNIT_AIR_COUNT:
					return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_UNIT_MOVE_TYPE;
				case NKMDeckCondition.DECK_CONDITION.UNIT_GRADE_COUNT:
					return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION_UNIT_RARITY;
				default:
					return NKM_ERROR_CODE.NEC_FAIL_EVENTDECK_CONDITION;
				}
			}

			// Token: 0x04009390 RID: 37776
			public NKMDeckCondition.DECK_CONDITION eCondition;

			// Token: 0x04009391 RID: 37777
			public NKMDeckCondition.MORE_LESS eMoreLess;

			// Token: 0x04009392 RID: 37778
			public int Value;

			// Token: 0x04009393 RID: 37779
			public List<int> lstValue;
		}

		// Token: 0x020011D9 RID: 4569
		public class GameCondition
		{
			// Token: 0x0600A0B5 RID: 41141 RVA: 0x0033EB0F File Offset: 0x0033CD0F
			public bool IsPenalty()
			{
				return this.Value < 0;
			}

			// Token: 0x04009394 RID: 37780
			public NKMDeckCondition.GAME_CONDITION eCondition;

			// Token: 0x04009395 RID: 37781
			public int Value;
		}

		// Token: 0x020011DA RID: 4570
		private enum MORE_LESS_TYPE
		{
			// Token: 0x04009397 RID: 37783
			NO_USE,
			// Token: 0x04009398 RID: 37784
			EQUAL_OR_NOT,
			// Token: 0x04009399 RID: 37785
			NO_NOT,
			// Token: 0x0400939A RID: 37786
			BOOL,
			// Token: 0x0400939B RID: 37787
			ALL
		}
	}
}

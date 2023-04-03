using System;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.User;
using ClientPacket.WorldMap;
using Cs.Logging;
using Cs.Math;
using NKM.Templet;
using NKM.Templet.Base;

namespace NKM
{
	// Token: 0x020004B1 RID: 1201
	public static class NKMUnitManager
	{
		// Token: 0x06002156 RID: 8534 RVA: 0x000AA218 File Offset: 0x000A8418
		public static bool LoadFromLUA(string[] fileNames, string unitTempletFolderName, bool bFullLoad = false)
		{
			Dictionary<string, NKMUnitStatTemplet> dicNKMUnitStatTempletByStrID = NKMUnitManager.m_dicNKMUnitStatTempletByStrID;
			if (dicNKMUnitStatTempletByStrID != null)
			{
				dicNKMUnitStatTempletByStrID.Clear();
			}
			Dictionary<int, NKMUnitStatTemplet> dicNKMUnitStatTempletByID = NKMUnitManager.m_dicNKMUnitStatTempletByID;
			if (dicNKMUnitStatTempletByID != null)
			{
				dicNKMUnitStatTempletByID.Clear();
			}
			NKMUnitManager.m_dicNKMUnitTempletStrID.Clear();
			NKMUnitManager.m_dicNKMUnitTempletID.Clear();
			bool flag = true;
			NKMUnitManager.m_UnitTempletFolderName = unitTempletFolderName;
			return flag & NKMUnitManager.LoadFromLUA_LUA_UNIT_STAT_TEMPLET(fileNames, false) & NKMUnitManager.LoadFromLUA_LUA_UNIT_TEMPLET(bFullLoad, false);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x000AA270 File Offset: 0x000A8470
		private static bool LoadFromLUA_LUA_UNIT_STAT_TEMPLET(string[] fileNames, bool bReload = false)
		{
			if (bReload)
			{
				Dictionary<int, NKMUnitStatTemplet> dicNKMUnitStatTempletByID = NKMUnitManager.m_dicNKMUnitStatTempletByID;
				if (dicNKMUnitStatTempletByID != null)
				{
					dicNKMUnitStatTempletByID.Clear();
				}
				Dictionary<string, NKMUnitStatTemplet> dicNKMUnitStatTempletByStrID = NKMUnitManager.m_dicNKMUnitStatTempletByStrID;
				if (dicNKMUnitStatTempletByStrID != null)
				{
					dicNKMUnitStatTempletByStrID.Clear();
				}
			}
			NKMUnitManager.m_dicNKMUnitStatTempletByID = NKMTempletLoader.LoadDictionary<NKMUnitStatTemplet>("AB_SCRIPT_UNIT_DATA", fileNames, "m_dicNKMUnitStatByID", new Func<NKMLua, NKMUnitStatTemplet>(NKMUnitStatTemplet.LoadFromLUA));
			NKMUnitManager.m_dicNKMUnitStatTempletByStrID = NKMUnitManager.m_dicNKMUnitStatTempletByID.ToDictionary((KeyValuePair<int, NKMUnitStatTemplet> e) => e.Value.m_UnitStrID, (KeyValuePair<int, NKMUnitStatTemplet> e) => e.Value);
			using (NKMLua nkmlua = new NKMLua())
			{
				if (!nkmlua.LoadCommonPath("AB_SCRIPT_UNIT_DATA", fileNames[0], true))
				{
					return false;
				}
				if (!nkmlua.OpenTable("m_dicNKMUnitStatByID"))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x000AA35C File Offset: 0x000A855C
		private static bool LoadFromLUA_LUA_UNIT_TEMPLET(bool bFullLoad = false, bool bReload = false)
		{
			if (bFullLoad)
			{
				using (IEnumerator<NKMUnitTempletBase> enumerator = NKMUnitTempletBase.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKMUnitTempletBase nkmunitTempletBase = enumerator.Current;
						if (nkmunitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SYSTEM && !NKMUnitManager.LoadFromLUA_LUA_UNIT_TEMPLET(nkmunitTempletBase, bReload))
						{
							Log.ErrorAndExit(string.Format("[UnitTemplet] 유닛 상세 정보가 존재하지 않음 m_UnitID : {0}, m_UnitTempletFileName : {1}", nkmunitTempletBase.m_UnitID, nkmunitTempletBase.m_UnitTempletFileName), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitManager.cs", 1477);
						}
					}
					return true;
				}
			}
			foreach (KeyValuePair<string, NKMUnitTemplet> keyValuePair in NKMUnitManager.m_dicNKMUnitTempletStrID)
			{
				NKMUnitTemplet value = keyValuePair.Value;
				if (value.m_UnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SYSTEM && !NKMUnitManager.LoadFromLUA_LUA_UNIT_TEMPLET(value.m_UnitTempletBase, bReload))
				{
					Log.ErrorAndExit(string.Format("[UnitTemplet] 유닛 상세 정보가 존재하지 않음 m_UnitID : {0}, m_UnitTempletFileName : {1}", value.m_UnitTempletBase.m_UnitID, value.m_UnitTempletBase.m_UnitTempletFileName), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitManager.cs", 1492);
				}
			}
			return true;
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x000AA45C File Offset: 0x000A865C
		private static bool LoadFromLUA_LUA_UNIT_TEMPLET(NKMUnitTempletBase cNKMUnitTempletBase, bool bReload = false)
		{
			if (cNKMUnitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				return true;
			}
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT_UNIT_DATA_UNIT_TEMPLET", NKMUnitManager.m_UnitTempletFolderName + cNKMUnitTempletBase.m_UnitTempletFileName, true))
			{
				if (!nkmlua.OpenTable("NKMUnitTemplet"))
				{
					return false;
				}
				NKMUnitTemplet nkmunitTemplet = null;
				NKMUnitTemplet nkmunitTemplet2 = new NKMUnitTemplet();
				string text = "";
				nkmlua.GetData("BASE_UNIT_STR_ID", ref text);
				if (text.Length <= 1)
				{
					NKM_UNIT_TYPE nkm_UNIT_TYPE = cNKMUnitTempletBase.m_NKM_UNIT_TYPE;
					if (nkm_UNIT_TYPE != NKM_UNIT_TYPE.NUT_NORMAL)
					{
						if (nkm_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP)
						{
							text = "NKM_UNIT_BASE_SHIP";
						}
					}
					else
					{
						text = "NKM_UNIT_BASE_NORMAL";
					}
				}
				if (text.Length > 1)
				{
					nkmunitTemplet = NKMUnitManager.GetUnitTemplet(text);
					if (nkmunitTemplet != null)
					{
						nkmunitTemplet2.DeepCopyFromSource(nkmunitTemplet);
					}
					else
					{
						Log.Error(cNKMUnitTempletBase.m_UnitStrID + ": BASE_UNIT_STR_ID(" + text + ") baseTemplet null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitManager.cs", 1538);
					}
				}
				if (nkmunitTemplet2.LoadFromLUA(nkmlua, cNKMUnitTempletBase))
				{
					nkmunitTemplet2.SetCoolTimeLink();
					if (!NKMUnitManager.m_dicNKMUnitTempletID.ContainsKey(nkmunitTemplet2.m_UnitTempletBase.m_UnitID))
					{
						NKMUnitManager.m_dicNKMUnitTempletID.Add(nkmunitTemplet2.m_UnitTempletBase.m_UnitID, nkmunitTemplet2);
						if (!NKMUnitManager.m_dicNKMUnitTempletStrID.ContainsKey(nkmunitTemplet2.m_UnitTempletBase.m_UnitStrID))
						{
							NKMUnitManager.m_dicNKMUnitTempletStrID.Add(nkmunitTemplet2.m_UnitTempletBase.m_UnitStrID, nkmunitTemplet2);
						}
						else if (bReload)
						{
							NKMUnitManager.m_dicNKMUnitTempletStrID[nkmunitTemplet2.m_UnitTempletBase.m_UnitStrID].DeepCopyFromSource(nkmunitTemplet2);
						}
						else
						{
							Log.Error("m_dicNKMUnitTempletID Duplicate Key: " + nkmunitTemplet2.m_UnitTempletBase.m_UnitStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitManager.cs", 1561);
						}
					}
					else if (bReload)
					{
						NKMUnitManager.m_dicNKMUnitTempletID[nkmunitTemplet2.m_UnitTempletBase.m_UnitID].DeepCopyFromSource(nkmunitTemplet2);
					}
					else if (nkmunitTemplet != null && nkmunitTemplet.m_UnitTempletBase.m_UnitID != nkmunitTemplet2.m_UnitTempletBase.m_UnitID)
					{
						Log.Error("m_dicNKMUnitTempletID Duplicate Key: " + nkmunitTemplet2.m_UnitTempletBase.m_UnitStrID, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitManager.cs", 1576);
					}
				}
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
			return true;
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x000AA664 File Offset: 0x000A8864
		public static int GetUnitID(string unitStrID)
		{
			NKMUnitTempletBase nkmunitTempletBase = NKMTempletContainer<NKMUnitTempletBase>.Find(unitStrID);
			if (nkmunitTempletBase == null)
			{
				return 0;
			}
			return nkmunitTempletBase.m_UnitID;
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x000AA684 File Offset: 0x000A8884
		public static string GetUnitStrID(int unitID)
		{
			NKMUnitTempletBase nkmunitTempletBase = NKMTempletContainer<NKMUnitTempletBase>.Find(unitID);
			if (nkmunitTempletBase == null)
			{
				return null;
			}
			return nkmunitTempletBase.m_UnitStrID;
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x000AA6A4 File Offset: 0x000A88A4
		public static NKMUnitTemplet GetUnitTemplet(int unitID)
		{
			if (!NKMUnitManager.m_dicNKMUnitTempletID.ContainsKey(unitID))
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
				if (unitTempletBase != null)
				{
					NKMUnitManager.LoadFromLUA_LUA_UNIT_TEMPLET(unitTempletBase, false);
				}
			}
			if (NKMUnitManager.m_dicNKMUnitTempletID.ContainsKey(unitID))
			{
				return NKMUnitManager.m_dicNKMUnitTempletID[unitID];
			}
			return null;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x000AA6EC File Offset: 0x000A88EC
		public static NKMUnitTemplet GetUnitTemplet(string unitStrID)
		{
			if (!NKMUnitManager.m_dicNKMUnitTempletStrID.ContainsKey(unitStrID))
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitStrID);
				if (unitTempletBase != null)
				{
					NKMUnitManager.LoadFromLUA_LUA_UNIT_TEMPLET(unitTempletBase, false);
				}
			}
			if (NKMUnitManager.m_dicNKMUnitTempletStrID.ContainsKey(unitStrID))
			{
				return NKMUnitManager.m_dicNKMUnitTempletStrID[unitStrID];
			}
			return null;
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x000AA734 File Offset: 0x000A8934
		public static List<NKMUnitTempletBase> GetListTeamUPUnitTempletBase(string TeamUp)
		{
			List<NKMUnitTempletBase> list = new List<NKMUnitTempletBase>();
			foreach (NKMUnitTempletBase nkmunitTempletBase in NKMTempletContainer<NKMUnitTempletBase>.Values)
			{
				if (string.Equals(nkmunitTempletBase.TeamUp, TeamUp))
				{
					list.Add(nkmunitTempletBase);
				}
			}
			return list;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000AA798 File Offset: 0x000A8998
		public static NKMUnitTempletBase GetUnitTempletBase(NKMUnitData UnitData)
		{
			if (UnitData == null)
			{
				return null;
			}
			return NKMUnitManager.GetUnitTempletBase(UnitData.m_UnitID);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x000AA7AA File Offset: 0x000A89AA
		public static NKMUnitTempletBase GetUnitTempletBase(NKMOperator operatorData)
		{
			if (operatorData == null)
			{
				return null;
			}
			return NKMUnitManager.GetUnitTempletBase(operatorData.id);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000AA7BC File Offset: 0x000A89BC
		public static NKMUnitTempletBase GetUnitTempletBase(int unitID)
		{
			return NKMTempletContainer<NKMUnitTempletBase>.Find(unitID);
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x000AA7C4 File Offset: 0x000A89C4
		public static NKMUnitTempletBase GetUnitTempletBase(string unitStrID)
		{
			return NKMTempletContainer<NKMUnitTempletBase>.Find(unitStrID);
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x000AA7CC File Offset: 0x000A89CC
		public static NKMUnitStatTemplet GetUnitStatTemplet(int unitID)
		{
			NKMUnitStatTemplet result;
			NKMUnitManager.m_dicNKMUnitStatTempletByID.TryGetValue(unitID, out result);
			return result;
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000AA7E8 File Offset: 0x000A89E8
		public static NKMUnitStatTemplet GetUnitStatTemplet(string unitStrID)
		{
			if (NKMUnitManager.m_dicNKMUnitStatTempletByStrID.ContainsKey(unitStrID))
			{
				return NKMUnitManager.m_dicNKMUnitStatTempletByStrID[unitStrID];
			}
			return null;
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x000AA804 File Offset: 0x000A8A04
		public static int GetUnitTempletCount()
		{
			return NKMUnitManager.m_dicNKMUnitTempletStrID.Count;
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x000AA810 File Offset: 0x000A8A10
		public static NKM_ERROR_CODE IsUnitBusy(NKMUserData userdata, NKMUnitData unitData, bool ignoreWorldmapState = false)
		{
			NKMDeckData deckDataByUnitUID = userdata.m_ArmyData.GetDeckDataByUnitUID(unitData.m_UnitUID);
			if (deckDataByUnitUID != null)
			{
				NKM_ERROR_CODE nkm_ERROR_CODE = deckDataByUnitUID.IsValidState();
				if (ignoreWorldmapState && nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_DOING)
				{
					return NKM_ERROR_CODE.NEC_OK;
				}
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					return nkm_ERROR_CODE;
				}
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x000AA84C File Offset: 0x000A8A4C
		public static NKM_ERROR_CODE GetCanDeleteUnit(NKMUnitData cUnitData, NKMUserData cUserData)
		{
			NKMArmyData armyData = cUserData.m_ArmyData;
			if (NKMMain.excludeUnitID.Contains(cUnitData.m_UnitID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_DELETE_EXCLUDE_UNIT;
			}
			if (!NKMOpenTagManager.IsOpened("TAG_DELETE_YOO_MI_NA") && cUnitData.m_UnitID == 1001)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DELETE_EXCLUDE_UNIT;
			}
			if (!NKMOpenTagManager.IsOpened("TAG_DELETE_TEAM_FENRIR") && cUnitData.m_UnitID == 1002)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DELETE_EXCLUDE_UNIT;
			}
			if (!NKMOpenTagManager.IsOpened("TAG_DELETE_JOO_SHI_YOON") && cUnitData.m_UnitID == 1003)
			{
				return NKM_ERROR_CODE.NEC_FAIL_DELETE_EXCLUDE_UNIT;
			}
			if (cUnitData.m_bLock)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_LOCKED;
			}
			if (cUserData.backGroundInfo.unitInfoList.Find((NKMBackgroundUnitInfo e) => e.unitUid == cUnitData.m_UnitUID) != null)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_LOBBYUNIT;
			}
			if (armyData.IsUnitInAnyDeck(cUnitData.m_UnitUID))
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_IN_DECK;
			}
			if (cUnitData.GetEquipItemAccessoryUid() != 0L || cUnitData.GetEquipItemDefenceUid() != 0L || cUnitData.GetEquipItemWeaponUid() != 0L || cUnitData.GetEquipItemAccessory2Uid() != 0L)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_EQUIP_ITEM;
			}
			using (Dictionary<int, NKMWorldMapCityData>.ValueCollection.Enumerator enumerator = cUserData.m_WorldmapData.worldMapCityDataMap.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.leaderUnitUID == cUnitData.m_UnitUID)
					{
						return NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_WORLDMAP_LEADER;
					}
				}
			}
			if (cUnitData.OfficeRoomId > 0)
			{
				return NKM_ERROR_CODE.NEC_FAIL_OFFICE_UNIT_DELETE_IN_ROOM;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000AA9FC File Offset: 0x000A8BFC
		public static bool CanUnitUsedInDeck(NKMUnitData unitData)
		{
			return NKMUnitManager.CanUnitUsedInDeck(NKMUnitManager.GetUnitTempletBase(unitData));
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000AAA09 File Offset: 0x000A8C09
		public static bool CanUnitUsedInDeck(int unitID)
		{
			return NKMUnitManager.CanUnitUsedInDeck(NKMUnitManager.GetUnitTempletBase(unitID));
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000AAA16 File Offset: 0x000A8C16
		public static bool CanUnitUsedInDeck(NKMUnitTempletBase unitTempletBase)
		{
			return unitTempletBase != null && unitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_TRAINER;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000AAA29 File Offset: 0x000A8C29
		public static bool IsShipType(NKM_UNIT_STYLE_TYPE type)
		{
			return type - NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT <= 5;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x000AAA34 File Offset: 0x000A8C34
		public static bool CheckContainsBaseUnit(IEnumerable<int> lstUnitID, int unitID)
		{
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitID);
			if (nkmunitTempletBase == null)
			{
				return lstUnitID.Contains(unitID);
			}
			foreach (int targetUnit in lstUnitID)
			{
				if (nkmunitTempletBase.IsSameBaseUnit(targetUnit))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x000AAA98 File Offset: 0x000A8C98
		public static void CheckValidation()
		{
			int num = 0;
			foreach (NKMUnitTemplet nkmunitTemplet in NKMUnitManager.m_dicNKMUnitTempletStrID.Values)
			{
				if (nkmunitTemplet.m_UnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SYSTEM && nkmunitTemplet.m_UnitTempletBase.m_NKM_UNIT_STYLE_TYPE != NKM_UNIT_STYLE_TYPE.NUST_TRAINER)
				{
					foreach (NKMUnitState nkmunitState in nkmunitTemplet.m_dicNKMUnitState.Values)
					{
						if (!string.IsNullOrEmpty(nkmunitState.m_AnimName) && NKMAnimDataManager.GetAnimTimeMax(nkmunitTemplet.m_UnitTempletBase.m_SpriteBundleName, nkmunitTemplet.m_UnitTempletBase.m_SpriteName, nkmunitState.m_AnimName).IsNearlyZero(1E-05f))
						{
							num++;
							Log.Error(string.Concat(new string[]
							{
								"[UnitTemplet] 유닛 상태에 따른 애니메이션이 존재하지 않음 UnitStrID : ",
								nkmunitTemplet.m_UnitTempletBase.m_UnitStrID,
								", StateName : ",
								nkmunitState.m_StateName,
								", AniName : ",
								nkmunitState.m_AnimName
							}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitManager.cs", 1851);
						}
						foreach (NKMEventDamageEffect nkmeventDamageEffect in nkmunitState.m_listNKMEventDamageEffect)
						{
							if (NKMDETempletManager.GetDETemplet(nkmeventDamageEffect.m_DEName) == null)
							{
								num++;
								Log.Error(string.Concat(new string[]
								{
									"[UnitTemplet] 유닛 상태에 따른 데미지 이펙트가 존재하지 않음 UnitStrID : ",
									nkmunitTemplet.m_UnitTempletBase.m_UnitStrID,
									", StateName : ",
									nkmunitState.m_StateName,
									", DEName : ",
									nkmeventDamageEffect.m_DEName
								}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitManager.cs", 1859);
							}
							if (nkmeventDamageEffect.m_DENamePVP.Length > 1 && NKMDETempletManager.GetDETemplet(nkmeventDamageEffect.m_DENamePVP) == null)
							{
								num++;
								Log.Error(string.Concat(new string[]
								{
									"[UnitTemplet] 유닛 상태에 따른 데미지 이펙트(m_DENamePVP)가 존재하지 않음 UnitStrID : ",
									nkmunitTemplet.m_UnitTempletBase.m_UnitStrID,
									", StateName : ",
									nkmunitState.m_StateName,
									", DEName : ",
									nkmeventDamageEffect.m_DENamePVP
								}), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitManager.cs", 1867);
							}
						}
					}
				}
			}
			if (num > 0)
			{
				Log.ErrorAndExit(string.Format("[UnitTemplet] 유닛 스크립트 정합성 체크에 실패 했습니다. invalidCount : {0}", num), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMUnitManager.cs", 1876);
			}
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000AAD58 File Offset: 0x000A8F58
		public static bool ReloadFromLUA()
		{
			string[] fileNames = new string[]
			{
				"LUA_UNIT_TEMPLET_BASE",
				"LUA_UNIT_TEMPLET_BASE2"
			};
			NKMTempletContainer<NKMUnitTempletBase>.Load("AB_SCRIPT_UNIT_DATA", fileNames, "m_dicNKMUnitTempletBaseByStrID", new Func<NKMLua, NKMUnitTempletBase>(NKMUnitTempletBase.LoadFromLUA), (NKMUnitTempletBase e) => e.m_UnitStrID);
			fileNames = new string[]
			{
				"LUA_UNIT_STAT_TEMPLET",
				"LUA_UNIT_STAT_TEMPLET2"
			};
			NKMUnitManager.LoadFromLUA_LUA_UNIT_STAT_TEMPLET(fileNames, true);
			NKMUnitManager.LoadFromLUA_LUA_UNIT_TEMPLET(false, true);
			NKMTempletContainer<NKMUnitTempletBase>.Join();
			return true;
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000AADE4 File Offset: 0x000A8FE4
		public static NKMUnitTempletBase GetUnitTempletBaseByShipGroupID(int groupID)
		{
			if (groupID == 0)
			{
				return null;
			}
			return NKMTempletContainer<NKMUnitTempletBase>.Find((NKMUnitTempletBase e) => e.m_ShipGroupID == groupID);
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000AAE1C File Offset: 0x000A901C
		public static NKM_ERROR_CODE GetCanDeleteOperator(NKMOperator operatorData, NKMUserData cUserData)
		{
			NKMArmyData armyData = cUserData.m_ArmyData;
			if (operatorData.bLock)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_LOCKED;
			}
			if (cUserData.GetBackgroundUnitIndex(operatorData.uid) >= 0)
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_LOBBYUNIT;
			}
			if (armyData.IsOperatorAnyDeck(operatorData.uid))
			{
				return NKM_ERROR_CODE.NEC_FAIL_UNIT_IN_DECK;
			}
			return NKM_ERROR_CODE.NEC_OK;
		}

		// Token: 0x0400223E RID: 8766
		public const int SHIP_REAL_MAX_STAR_COUNT = 6;

		// Token: 0x0400223F RID: 8767
		public static Dictionary<string, NKMUnitStatTemplet> m_dicNKMUnitStatTempletByStrID = null;

		// Token: 0x04002240 RID: 8768
		public static Dictionary<int, NKMUnitStatTemplet> m_dicNKMUnitStatTempletByID = null;

		// Token: 0x04002241 RID: 8769
		public static Dictionary<string, NKMUnitTemplet> m_dicNKMUnitTempletStrID = new Dictionary<string, NKMUnitTemplet>();

		// Token: 0x04002242 RID: 8770
		public static Dictionary<int, NKMUnitTemplet> m_dicNKMUnitTempletID = new Dictionary<int, NKMUnitTemplet>();

		// Token: 0x04002243 RID: 8771
		private static string m_UnitTempletFolderName = "";
	}
}

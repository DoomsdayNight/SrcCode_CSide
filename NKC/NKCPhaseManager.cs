using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Mode;
using Cs.Logging;
using NKC.UI;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x020006A1 RID: 1697
	public static class NKCPhaseManager
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x060037D9 RID: 14297 RVA: 0x00120109 File Offset: 0x0011E309
		// (set) Token: 0x060037DA RID: 14298 RVA: 0x00120110 File Offset: 0x0011E310
		public static PhaseModeState PhaseModeState { get; private set; }

		// Token: 0x060037DB RID: 14299 RVA: 0x00120118 File Offset: 0x0011E318
		public static void SetPhaseModeState(PhaseModeState state)
		{
			if (state == null || state.stageId == 0)
			{
				NKCPhaseManager.LastStageID = ((NKCPhaseManager.PhaseModeState != null) ? NKCPhaseManager.PhaseModeState.stageId : 0);
			}
			NKCPhaseManager.PhaseModeState = state;
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x00120144 File Offset: 0x0011E344
		public static void Reset()
		{
			NKCPhaseManager.PhaseModeState = null;
			NKCPhaseManager.LastStageID = 0;
			NKCPhaseManager.m_dicPhaseStageClearData.Clear();
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x0012015C File Offset: 0x0011E35C
		public static int GetLastStageID()
		{
			if (NKCPhaseManager.PhaseModeState == null || NKCPhaseManager.PhaseModeState.stageId == 0)
			{
				return NKCPhaseManager.LastStageID;
			}
			return NKCPhaseManager.PhaseModeState.stageId;
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x00120181 File Offset: 0x0011E381
		public static NKMStageTempletV2 GetStageTemplet()
		{
			if (NKCPhaseManager.PhaseModeState == null)
			{
				return null;
			}
			return NKMStageTempletV2.Find(NKCPhaseManager.PhaseModeState.stageId);
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x0012019C File Offset: 0x0011E39C
		public static NKMPhaseTemplet GetPhaseTemplet()
		{
			NKMStageTempletV2 stageTemplet = NKCPhaseManager.GetStageTemplet();
			if (stageTemplet == null)
			{
				return null;
			}
			return stageTemplet.PhaseTemplet;
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x001201BC File Offset: 0x0011E3BC
		public static NKMPhaseOrderTemplet GetPhaseOrderTemplet()
		{
			if (NKCPhaseManager.PhaseModeState == null)
			{
				return null;
			}
			NKMPhaseTemplet phaseTemplet = NKCPhaseManager.GetPhaseTemplet();
			if (phaseTemplet == null)
			{
				return null;
			}
			return phaseTemplet.GetPhase(NKCPhaseManager.PhaseModeState.phaseIndex);
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x001201F0 File Offset: 0x0011E3F0
		public static void SetPhaseClearDataList(List<NKMPhaseClearData> list)
		{
			NKCPhaseManager.m_dicPhaseStageClearData.Clear();
			foreach (NKMPhaseClearData nkmphaseClearData in list)
			{
				NKCPhaseManager.m_dicPhaseStageClearData.Add(nkmphaseClearData.stageId, nkmphaseClearData);
			}
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x00120254 File Offset: 0x0011E454
		public static void UpdateClearData(NKMPhaseClearData _NKMPhaseClearData)
		{
			if (_NKMPhaseClearData == null)
			{
				return;
			}
			if (!NKCPhaseManager.m_dicPhaseStageClearData.ContainsKey(_NKMPhaseClearData.stageId))
			{
				NKCPhaseManager.m_sHsFirstClearStage.Add(_NKMPhaseClearData.stageId);
			}
			NKCPhaseManager.m_dicPhaseStageClearData[_NKMPhaseClearData.stageId] = _NKMPhaseClearData;
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x0012028E File Offset: 0x0011E48E
		public static bool WasPhaseStageFirstClear(int stageID)
		{
			if (NKCPhaseManager.m_sHsFirstClearStage.Contains(stageID))
			{
				NKCPhaseManager.m_sHsFirstClearStage.Remove(stageID);
				return true;
			}
			return false;
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x001202AC File Offset: 0x0011E4AC
		public static NKMPhaseClearData GetPhaseClearData(NKMStageTempletV2 templet)
		{
			if (templet == null)
			{
				return null;
			}
			NKMPhaseClearData result;
			if (NKCPhaseManager.m_dicPhaseStageClearData.TryGetValue(templet.Key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x001202D8 File Offset: 0x0011E4D8
		public static NKMPhaseClearData GetPhaseClearData(NKMPhaseTemplet templet)
		{
			if (templet == null)
			{
				return null;
			}
			if (templet.StageTemplet == null)
			{
				return null;
			}
			NKMPhaseClearData result;
			if (NKCPhaseManager.m_dicPhaseStageClearData.TryGetValue(templet.StageTemplet.Key, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x00120310 File Offset: 0x0011E510
		public static bool CheckPhaseClear(int phaseID)
		{
			NKMPhaseTemplet nkmphaseTemplet = NKMPhaseTemplet.Find(phaseID);
			return nkmphaseTemplet != null && NKCPhaseManager.CheckPhaseStageClear(nkmphaseTemplet);
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x0012032F File Offset: 0x0011E52F
		public static bool CheckPhaseClear(NKMPhaseTemplet phaseTemplet)
		{
			return phaseTemplet != null && NKCPhaseManager.CheckPhaseStageClear(phaseTemplet);
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x0012033C File Offset: 0x0011E53C
		public static bool CheckPhaseStageClear(int stageID)
		{
			return NKCPhaseManager.m_dicPhaseStageClearData.ContainsKey(stageID);
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x00120349 File Offset: 0x0011E549
		public static bool CheckPhaseStageClear(NKMStageTempletV2 templet)
		{
			return templet != null && NKCPhaseManager.CheckPhaseStageClear(templet.Key);
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x0012035B File Offset: 0x0011E55B
		public static bool CheckPhaseStageClear(NKMPhaseTemplet templet)
		{
			return templet != null && templet.StageTemplet != null && NKCPhaseManager.CheckPhaseStageClear(templet.StageTemplet.Key);
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x0012037C File Offset: 0x0011E57C
		public static bool IsPhaseOnGoing()
		{
			return NKCPhaseManager.PhaseModeState != null && NKCPhaseManager.PhaseModeState.stageId != 0;
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x00120394 File Offset: 0x0011E594
		public static bool IsCurrentPhaseDungeon(int dungeonID)
		{
			NKMPhaseTemplet phaseTemplet = NKCPhaseManager.GetPhaseTemplet();
			if (phaseTemplet == null)
			{
				return false;
			}
			using (IEnumerator<NKMPhaseOrderTemplet> enumerator = phaseTemplet.PhaseList.List.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Dungeon.m_DungeonID == dungeonID)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x00120400 File Offset: 0x0011E600
		public static bool CheckOneTimeReward(NKMStageTempletV2 stageTemplet, int index)
		{
			if (index < 0)
			{
				return false;
			}
			NKMPhaseClearData phaseClearData = NKCPhaseManager.GetPhaseClearData(stageTemplet);
			return phaseClearData != null && phaseClearData.onetimeRewardResults != null && phaseClearData.onetimeRewardResults.Count >= index && phaseClearData.onetimeRewardResults[index];
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x00120444 File Offset: 0x0011E644
		public static bool ShouldPlayNextPhase()
		{
			if (NKCPhaseManager.PhaseModeState == null)
			{
				return false;
			}
			if (NKCPhaseManager.PhaseModeState.stageId == 0)
			{
				return false;
			}
			NKMStageTempletV2 nkmstageTempletV = NKMStageTempletV2.Find(NKCPhaseManager.PhaseModeState.stageId);
			if (nkmstageTempletV == null)
			{
				Log.Error(string.Format("NKMStageTemplet not found! Stage ID : {0}", NKCPhaseManager.PhaseModeState.stageId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCPhaseManager.cs", 238);
				return false;
			}
			if (nkmstageTempletV.PhaseTemplet == null)
			{
				Log.Error(string.Format("NKMPhaseTemplet not found! Stage ID : {0}", NKCPhaseManager.PhaseModeState.stageId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCPhaseManager.cs", 244);
				return false;
			}
			if (nkmstageTempletV.PhaseTemplet.GetPhase(NKCPhaseManager.PhaseModeState.phaseIndex) == null)
			{
				Log.Error(string.Format("NKMPhaseOrderTemplet not found! Phase ID : {0}, index : {1}", nkmstageTempletV.PhaseTemplet.Id, NKCPhaseManager.PhaseModeState.phaseIndex), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCPhaseManager.cs", 251);
				return false;
			}
			if (NKMDungeonManager.GetDungeonTempletBase(NKCPhaseManager.PhaseModeState.dungeonId) == null)
			{
				Log.Error(string.Format("Dungeon templet not found! ID : {0}", NKCPhaseManager.PhaseModeState.dungeonId), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCManager/NKCPhaseManager.cs", 258);
				return false;
			}
			return true;
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x00120564 File Offset: 0x0011E764
		public static bool PlayNextPhase()
		{
			if (!NKCPhaseManager.ShouldPlayNextPhase())
			{
				return false;
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(NKCPhaseManager.PhaseModeState.dungeonId);
			NKCUICutScenPlayer.CutScenCallBack cutScenCallBack = delegate()
			{
				NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(0, NKCPhaseManager.PhaseModeState.stageId, 0, NKCPhaseManager.PhaseModeState.dungeonId, 0, false, 1, 0);
			};
			NKMStageTempletV2 stageTemplet = NKCPhaseManager.GetStageTemplet();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			bool flag = false;
			bool flag2 = true;
			if (NKCScenManager.CurrentUserData() != null)
			{
				flag2 = NKCScenManager.CurrentUserData().m_UserOption.m_bPlayCutscene;
			}
			bool isOnGoing = NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing();
			if (!nkmuserData.CheckStageCleared(stageTemplet) || (flag2 && !isOnGoing))
			{
				flag = true;
			}
			NKCCutScenTemplet cutScenTemple = NKCCutScenManager.GetCutScenTemple(dungeonTempletBase.m_CutScenStrIDBefore);
			if (flag && cutScenTemple != null)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_CUTSCEN_DUNGEON().SetReservedOneCutscenType(cutScenTemple.m_CutScenStrID, cutScenCallBack, 0);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_CUTSCENE_DUNGEON, true);
			}
			else
			{
				cutScenCallBack();
			}
			return true;
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x00120634 File Offset: 0x0011E834
		public static bool IsFirstStage(int dungeonID)
		{
			NKMPhaseTemplet phaseTemplet = NKCPhaseManager.GetPhaseTemplet();
			if (phaseTemplet == null)
			{
				return false;
			}
			NKMPhaseOrderTemplet phase = phaseTemplet.GetPhase(0);
			return phase != null && dungeonID == phase.Dungeon.m_DungeonID;
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x00120668 File Offset: 0x0011E868
		public static bool IsLastStage(int dungeonID)
		{
			NKMPhaseTemplet phaseTemplet = NKCPhaseManager.GetPhaseTemplet();
			if (phaseTemplet == null)
			{
				return true;
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonID);
			return phaseTemplet.IsLastPhase(dungeonTempletBase);
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x00120690 File Offset: 0x0011E890
		public static void SaveTempUnitData(NKMGame nkmGame, NKMGameRecord gameRecord)
		{
			if (nkmGame == null || gameRecord == null)
			{
				return;
			}
			foreach (KeyValuePair<short, NKMGameRecordUnitData> keyValuePair in gameRecord.UnitRecordList)
			{
				short key = keyValuePair.Key;
				NKMGameRecordUnitData value = keyValuePair.Value;
				NKMUnit unit = nkmGame.GetUnit(keyValuePair.Key, true, true);
				if (unit != null)
				{
					NKMUnitData unitData = unit.GetUnitData();
					if (unitData != null && !NKCPhaseManager.s_dicTempUnitData.ContainsKey(new ValueTuple<NKM_TEAM_TYPE, int, bool, bool>(value.teamType, value.unitId, value.isSummonee, value.isAssistUnit)))
					{
						NKCPhaseManager.s_dicTempUnitData.Add(new ValueTuple<NKM_TEAM_TYPE, int, bool, bool>(value.teamType, value.unitId, value.isSummonee, value.isAssistUnit), unitData);
					}
				}
			}
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x0012076C File Offset: 0x0011E96C
		public static NKMUnitData GetTempUnitData(NKMGameRecordUnitData gameRecordUnitData)
		{
			NKMUnitData result;
			if (NKCPhaseManager.s_dicTempUnitData.TryGetValue(new ValueTuple<NKM_TEAM_TYPE, int, bool, bool>(gameRecordUnitData.teamType, gameRecordUnitData.unitId, gameRecordUnitData.isSummonee, gameRecordUnitData.isAssistUnit), out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x001207A7 File Offset: 0x0011E9A7
		public static void ClearTempUnitData()
		{
			NKCPhaseManager.s_dicTempUnitData.Clear();
		}

		// Token: 0x04003471 RID: 13425
		private static int LastStageID = 0;

		// Token: 0x04003473 RID: 13427
		private static Dictionary<int, NKMPhaseClearData> m_dicPhaseStageClearData = new Dictionary<int, NKMPhaseClearData>();

		// Token: 0x04003474 RID: 13428
		private static HashSet<int> m_sHsFirstClearStage = new HashSet<int>();

		// Token: 0x04003475 RID: 13429
		private static Dictionary<ValueTuple<NKM_TEAM_TYPE, int, bool, bool>, NKMUnitData> s_dicTempUnitData = new Dictionary<ValueTuple<NKM_TEAM_TYPE, int, bool, bool>, NKMUnitData>();
	}
}

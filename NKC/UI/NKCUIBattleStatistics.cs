using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Game;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000975 RID: 2421
	public class NKCUIBattleStatistics : NKCUIBase
	{
		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x06006206 RID: 25094 RVA: 0x001EAFC8 File Offset: 0x001E91C8
		public static NKCUIBattleStatistics Instance
		{
			get
			{
				if (NKCUIBattleStatistics.m_Instance == null)
				{
					NKCUIBattleStatistics.m_Instance = NKCUIManager.OpenNewInstance<NKCUIBattleStatistics>("AB_UI_NKM_UI_RESULT", "NKM_UI_RESULT_BATTLE_STATISTICS", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIBattleStatistics.CleanupInstance)).GetInstance<NKCUIBattleStatistics>();
					NKCUIBattleStatistics.m_Instance.Init();
				}
				return NKCUIBattleStatistics.m_Instance;
			}
		}

		// Token: 0x06006207 RID: 25095 RVA: 0x001EB01C File Offset: 0x001E921C
		private static void CleanupInstance()
		{
			NKCUIBattleStatistics.m_Instance = null;
		}

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06006208 RID: 25096 RVA: 0x001EB024 File Offset: 0x001E9224
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIBattleStatistics.m_Instance != null && NKCUIBattleStatistics.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006209 RID: 25097 RVA: 0x001EB03F File Offset: 0x001E923F
		public static void CheckInstanceAndClose()
		{
			if (NKCUIBattleStatistics.m_Instance != null)
			{
				if (NKCUIBattleStatistics.m_Instance.IsOpen)
				{
					NKCUIBattleStatistics.m_Instance.Close();
				}
				NKCUIBattleStatistics.m_Instance.Clear();
			}
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x0600620A RID: 25098 RVA: 0x001EB06E File Offset: 0x001E926E
		public override string MenuName
		{
			get
			{
				return "전투 통계";
			}
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x0600620B RID: 25099 RVA: 0x001EB075 File Offset: 0x001E9275
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x0600620C RID: 25100 RVA: 0x001EB078 File Offset: 0x001E9278
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x0600620D RID: 25101 RVA: 0x001EB07C File Offset: 0x001E927C
		public void Init()
		{
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_tgDps.OnValueChanged.RemoveAllListeners();
			this.m_tgDps.OnValueChanged.AddListener(new UnityAction<bool>(this.ChangeToggleDPS));
			NKCUIBattleStatisticsSlot newInstance = NKCUIBattleStatisticsSlot.GetNewInstance(this.m_contents, true);
			this.m_slotList.Add(newInstance);
			for (int i = 0; i < this.TEMP_SLOT_COUNT; i++)
			{
				newInstance = NKCUIBattleStatisticsSlot.GetNewInstance(this.m_contents, false);
				this.m_slotList.Add(newInstance);
			}
		}

		// Token: 0x0600620E RID: 25102 RVA: 0x001EB124 File Offset: 0x001E9324
		public void Open(NKCUIBattleStatistics.BattleData battleData, NKCUIBattleStatistics.OnClose onClose)
		{
			if (battleData == null)
			{
				return;
			}
			this.m_battleData = battleData;
			this.m_battleData.teamA.RemoveAll((NKCUIBattleStatistics.UnitBattleData x) => x == null);
			this.m_battleData.teamB.RemoveAll((NKCUIBattleStatistics.UnitBattleData x) => x == null);
			this.m_tgDps.Select(false, true, false);
			this.Sort(false);
			this.SetData(this.m_battleData, false);
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)battleData.playTime);
			this.m_txtPlayTime.text = NKCUtilString.GetTimeSpanStringMS(timeSpan);
			if (onClose != null)
			{
				this.dOnClose = onClose;
			}
			base.UIOpened(true);
		}

		// Token: 0x0600620F RID: 25103 RVA: 0x001EB1EE File Offset: 0x001E93EE
		private void ChangeToggleDPS(bool bDps)
		{
			this.Sort(bDps);
			this.SetData(this.m_battleData, bDps);
		}

		// Token: 0x06006210 RID: 25104 RVA: 0x001EB204 File Offset: 0x001E9404
		private void SetData(NKCUIBattleStatistics.BattleData battleData, bool isDps = false)
		{
			int num = Mathf.Max(battleData.teamA.Count + 1, battleData.teamB.Count + 1);
			Debug.LogWarning(string.Format("NeedSlot : {0}", num));
			for (int i = this.m_slotList.Count; i < num; i++)
			{
				NKCUIBattleStatisticsSlot newInstance = NKCUIBattleStatisticsSlot.GetNewInstance(this.m_contents, false);
				this.m_slotList.Add(newInstance);
			}
			float maxValue = isDps ? battleData.maxDps : battleData.maxValue;
			NKCUtil.SetGameobjectActive(this.m_slotList[0], true);
			this.m_slotList[0].SetDataA(battleData.mainShipA, maxValue, isDps);
			this.m_slotList[0].SetDataB(battleData.mainShipB, maxValue, isDps);
			int num2 = 0;
			for (int j = 1; j < this.m_slotList.Count; j++)
			{
				if (j >= num)
				{
					NKCUtil.SetGameobjectActive(this.m_slotList[j], false);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_slotList[j], true);
					if (num2 < battleData.teamA.Count)
					{
						this.m_slotList[j].SetEnableShowBan(this.m_battleData.m_bBanGame && !this.m_battleData.m_bHideUpBanUITeamA);
						this.m_slotList[j].SetEnableShowUpUnit(this.m_battleData.m_bUpUnitGame && !this.m_battleData.m_bHideUpBanUITeamA);
						this.m_slotList[j].SetDataA(battleData.teamA[num2], maxValue, isDps);
					}
					else
					{
						this.m_slotList[j].SetDataA(null, 0f, false);
					}
					if (num2 < battleData.teamB.Count)
					{
						this.m_slotList[j].SetEnableShowBan(this.m_battleData.m_bBanGame);
						this.m_slotList[j].SetEnableShowUpUnit(this.m_battleData.m_bUpUnitGame);
						this.m_slotList[j].SetDataB(battleData.teamB[num2], maxValue, isDps);
					}
					else
					{
						this.m_slotList[j].SetDataB(null, 0f, false);
					}
					num2++;
				}
			}
		}

		// Token: 0x06006211 RID: 25105 RVA: 0x001EB452 File Offset: 0x001E9652
		public override void CloseInternal()
		{
			NKCUIBattleStatistics.OnClose onClose = this.dOnClose;
			if (onClose != null)
			{
				onClose();
			}
			base.gameObject.SetActive(false);
			this.m_battleData = null;
		}

		// Token: 0x06006212 RID: 25106 RVA: 0x001EB478 File Offset: 0x001E9678
		private void Clear()
		{
			for (int i = 0; i < this.m_slotList.Count; i++)
			{
				this.m_slotList[i].CloseInstance();
			}
			this.m_slotList.Clear();
		}

		// Token: 0x06006213 RID: 25107 RVA: 0x001EB4B7 File Offset: 0x001E96B7
		public static NKCUIBattleStatistics.BattleData MakeBattleData(NKMGame game, NKMPacket_GAME_END_NOT sPacket)
		{
			return NKCUIBattleStatistics.MakeBattleData(game, sPacket.gameRecord, sPacket.totalPlayTime, NKM_GAME_TYPE.NGT_INVALID);
		}

		// Token: 0x06006214 RID: 25108 RVA: 0x001EB4CC File Offset: 0x001E96CC
		public static NKCUIBattleStatistics.BattleData MakeBattleData(NKMGame game, NKMGameRecord gameRecord, NKM_GAME_TYPE gameType = NKM_GAME_TYPE.NGT_INVALID)
		{
			return NKCUIBattleStatistics.MakeBattleData(game, gameRecord, game.GetGameRuntimeData().GetGamePlayTime(), gameType);
		}

		// Token: 0x06006215 RID: 25109 RVA: 0x001EB4E4 File Offset: 0x001E96E4
		public static NKCUIBattleStatistics.BattleData MakeBattleData(NKMGame game, NKMGameRecord gameRecord, float totalGameTime, NKM_GAME_TYPE gameType = NKM_GAME_TYPE.NGT_INVALID)
		{
			if (game == null || gameRecord == null)
			{
				return null;
			}
			NKMGameData gameData = game.GetGameData();
			NKCUIBattleStatistics.BattleData battleData = new NKCUIBattleStatistics.BattleData();
			battleData.m_bBanGame = gameData.IsBanGame();
			battleData.m_bUpUnitGame = gameData.IsUpUnitGame();
			battleData.m_bHideUpBanUITeamA = (gameType == NKM_GAME_TYPE.NGT_PVP_STRATEGY);
			battleData.playTime = totalGameTime;
			NKM_TEAM_TYPE nkm_TEAM_TYPE = NKM_TEAM_TYPE.NTT_INVALID;
			if (NKCReplayMgr.IsPlayingReplay())
			{
				nkm_TEAM_TYPE = game.GetGameData().GetTeamType(NKCScenManager.CurrentUserData().m_UserUID);
				if (nkm_TEAM_TYPE == NKM_TEAM_TYPE.NTT_INVALID)
				{
					nkm_TEAM_TYPE = NKM_TEAM_TYPE.NTT_A1;
				}
			}
			else
			{
				nkm_TEAM_TYPE = game.GetGameData().GetTeamType(NKCScenManager.CurrentUserData().m_UserUID);
			}
			NKMGameTeamData myTeamData = null;
			NKMGameTeamData enemyTeamData = null;
			if (game.IsATeam(nkm_TEAM_TYPE))
			{
				myTeamData = gameData.m_NKMGameTeamDataA;
				enemyTeamData = gameData.m_NKMGameTeamDataB;
			}
			else
			{
				myTeamData = gameData.m_NKMGameTeamDataB;
				enemyTeamData = gameData.m_NKMGameTeamDataA;
			}
			foreach (KeyValuePair<short, NKMGameRecordUnitData> keyValuePair in gameRecord.UnitRecordList)
			{
				short key = keyValuePair.Key;
				NKMGameRecordUnitData value = keyValuePair.Value;
				NKCUIBattleStatistics.UnitBattleData unitBattleData = null;
				bool flag = game.IsSameTeam(nkm_TEAM_TYPE, value.teamType);
				NKMGameTeamData nkmgameTeamData = flag ? myTeamData : enemyTeamData;
				int checkUnitID = value.unitId;
				bool bSummon = value.isSummonee;
				bool bAssist = value.isAssistUnit;
				bool bLeader = value.isLeader;
				string changeName = value.changeUnitName;
				if (flag)
				{
					unitBattleData = battleData.teamA.Find((NKCUIBattleStatistics.UnitBattleData v) => v.UnitID == checkUnitID && v.bAssist == bAssist && v.bSummon == bSummon && v.bLeader == bLeader && string.Equals(v.recordData.changeUnitName, changeName));
				}
				else
				{
					unitBattleData = battleData.teamB.Find((NKCUIBattleStatistics.UnitBattleData v) => v.UnitID == checkUnitID && v.bAssist == bAssist && v.bSummon == bSummon && v.bLeader == bLeader && string.Equals(v.recordData.changeUnitName, changeName));
				}
				if (unitBattleData == null)
				{
					unitBattleData = new NKCUIBattleStatistics.UnitBattleData();
					unitBattleData.UnitID = checkUnitID;
					unitBattleData.bSummon = bSummon;
					unitBattleData.bAssist = bAssist;
					unitBattleData.bLeader = bLeader;
					unitBattleData.recordData = new NKMGameRecordUnitData();
					unitBattleData.recordData.unitId = value.unitId;
					unitBattleData.recordData.unitLevel = value.unitLevel;
					unitBattleData.recordData.isSummonee = value.isSummonee;
					unitBattleData.recordData.isAssistUnit = value.isAssistUnit;
					unitBattleData.recordData.isLeader = value.isLeader;
					unitBattleData.recordData.teamType = value.teamType;
					unitBattleData.recordData.changeUnitName = value.changeUnitName;
					if (flag)
					{
						battleData.teamA.Add(unitBattleData);
					}
					else
					{
						battleData.teamB.Add(unitBattleData);
					}
				}
				if (unitBattleData.unitData == null)
				{
					NKMUnitData nkmunitData = null;
					NKMUnit unit = game.GetUnit(key, true, true);
					if (unit != null)
					{
						nkmunitData = unit.GetUnitData();
					}
					else
					{
						List<NKMUnit> list = new List<NKMUnit>();
						game.GetUnitByUnitID(list, checkUnitID, true, true);
						foreach (NKMUnit nkmunit in list)
						{
							if (nkmunit.GetTeam() == value.teamType && nkmgameTeamData.IsAssistUnit(nkmunit.GetUnitData().m_UnitUID) == value.isAssistUnit && nkmunit.GetUnitDataGame().m_MasterGameUnitUID != 0 == value.isSummonee)
							{
								nkmunitData = nkmunit.GetUnitData();
								break;
							}
						}
					}
					if (nkmunitData == null)
					{
						nkmunitData = NKCPhaseManager.GetTempUnitData(value);
					}
					if (nkmunitData == null)
					{
						nkmunitData = NKMDungeonManager.MakeUnitDataFromID(checkUnitID, -1L, value.unitLevel, -1, 0, 0);
					}
					if (checkUnitID == nkmunitData.m_UnitID)
					{
						unitBattleData.unitData = nkmunitData;
					}
				}
				unitBattleData.recordData.recordGiveDamage += value.recordGiveDamage;
				unitBattleData.recordData.recordTakeDamage += value.recordTakeDamage;
				unitBattleData.recordData.recordHeal += value.recordHeal;
				unitBattleData.recordData.recordSummonCount += value.recordSummonCount;
				unitBattleData.recordData.recordDieCount += value.recordDieCount;
				unitBattleData.recordData.recordKillCount += value.recordKillCount;
				unitBattleData.recordData.playtime += value.playtime;
				battleData.maxValue = Mathf.Max(new float[]
				{
					battleData.maxValue,
					value.recordGiveDamage,
					value.recordTakeDamage,
					value.recordHeal
				});
				if (unitBattleData.recordData.playtime > 0)
				{
					battleData.maxDps = Mathf.Max(new float[]
					{
						battleData.maxDps,
						unitBattleData.recordData.recordGiveDamage / (float)unitBattleData.recordData.playtime,
						unitBattleData.recordData.recordTakeDamage / (float)unitBattleData.recordData.playtime
					});
				}
			}
			foreach (NKCUIBattleStatistics.UnitBattleData unitBattleData2 in battleData.teamA)
			{
				NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitBattleData2.unitData.m_UnitID);
				if (unitStatTemplet != null && unitStatTemplet.m_RespawnCount > 1)
				{
					unitBattleData2.recordData.playtime /= unitStatTemplet.m_RespawnCount;
					unitBattleData2.recordData.recordSummonCount /= unitStatTemplet.m_RespawnCount;
				}
				if (unitBattleData2.recordData.playtime > 0)
				{
					float num = (float)unitBattleData2.recordData.playtime;
					battleData.maxDps = Mathf.Max(new float[]
					{
						battleData.maxDps,
						unitBattleData2.recordData.recordGiveDamage / num,
						unitBattleData2.recordData.recordTakeDamage / num,
						unitBattleData2.recordData.recordHeal / num
					});
				}
			}
			foreach (NKCUIBattleStatistics.UnitBattleData unitBattleData3 in battleData.teamB)
			{
				if (!unitBattleData3.bSummon)
				{
					NKMUnitStatTemplet unitStatTemplet2 = NKMUnitManager.GetUnitStatTemplet(unitBattleData3.unitData.m_UnitID);
					if (unitStatTemplet2 != null && unitStatTemplet2.m_RespawnCount > 1)
					{
						unitBattleData3.recordData.playtime /= unitStatTemplet2.m_RespawnCount;
						unitBattleData3.recordData.recordSummonCount /= unitStatTemplet2.m_RespawnCount;
					}
				}
				if (unitBattleData3.recordData.playtime > 0)
				{
					float num2 = (float)unitBattleData3.recordData.playtime;
					battleData.maxDps = Mathf.Max(new float[]
					{
						battleData.maxDps,
						unitBattleData3.recordData.recordGiveDamage / num2,
						unitBattleData3.recordData.recordTakeDamage / num2,
						unitBattleData3.recordData.recordHeal / num2
					});
				}
			}
			NKCUIBattleStatistics.UnitBattleData unitBattleData4 = battleData.teamA.Find((NKCUIBattleStatistics.UnitBattleData v) => v.unitData.m_UnitUID == myTeamData.m_MainShip.m_UnitUID);
			if (unitBattleData4 == null)
			{
				Debug.LogError("A팀 main Ship 찾을 수 없음");
			}
			battleData.teamA.Remove(unitBattleData4);
			battleData.mainShipA = unitBattleData4;
			NKCUIBattleStatistics.UnitBattleData unitBattleData5;
			if (gameData.m_NKMGameTeamDataB.m_MainShip != null)
			{
				unitBattleData5 = battleData.teamB.Find((NKCUIBattleStatistics.UnitBattleData v) => v.unitData.m_UnitUID == enemyTeamData.m_MainShip.m_UnitUID);
				if (unitBattleData5 == null)
				{
					Debug.LogError("B팀 main Ship 찾을 수 없음");
				}
				battleData.teamB.Remove(unitBattleData5);
			}
			else
			{
				unitBattleData5 = battleData.teamB.Find((NKCUIBattleStatistics.UnitBattleData v) => v.bLeader);
				battleData.teamB.Remove(battleData.mainShipB);
			}
			battleData.mainShipB = unitBattleData5;
			return battleData;
		}

		// Token: 0x06006216 RID: 25110 RVA: 0x001EBD2C File Offset: 0x001E9F2C
		private void Sort(bool bDps)
		{
			if (bDps)
			{
				this.m_battleData.teamA.Sort(new Comparison<NKCUIBattleStatistics.UnitBattleData>(this.SortDps));
				this.m_battleData.teamB.Sort(new Comparison<NKCUIBattleStatistics.UnitBattleData>(this.SortDps));
				return;
			}
			this.m_battleData.teamA.Sort(new Comparison<NKCUIBattleStatistics.UnitBattleData>(this.SortDamage));
			this.m_battleData.teamB.Sort(new Comparison<NKCUIBattleStatistics.UnitBattleData>(this.SortDamage));
		}

		// Token: 0x06006217 RID: 25111 RVA: 0x001EBDB0 File Offset: 0x001E9FB0
		private int SortDamage(NKCUIBattleStatistics.UnitBattleData a, NKCUIBattleStatistics.UnitBattleData b)
		{
			if (a == null && b == null)
			{
				Debug.LogError("recordData null!");
				return 0;
			}
			if (a == null)
			{
				Debug.LogError("recordData null!");
				return 1;
			}
			if (b == null)
			{
				Debug.LogError("recordData null!");
				return -1;
			}
			if (a.recordData == null && b.recordData == null)
			{
				Debug.LogError(string.Format("recordData for {0}, {1} null?", a.UnitID, b.UnitID));
				return 0;
			}
			if (a.recordData == null)
			{
				Debug.LogError(string.Format("recordData for {0} null?", a.UnitID));
				return 1;
			}
			if (b.recordData == null)
			{
				Debug.LogError(string.Format("recordData for {0} null?", b.UnitID));
				return -1;
			}
			if (a.recordData.recordGiveDamage != b.recordData.recordGiveDamage)
			{
				return b.recordData.recordGiveDamage.CompareTo(a.recordData.recordGiveDamage);
			}
			return this.SortCommon(a, b);
		}

		// Token: 0x06006218 RID: 25112 RVA: 0x001EBEA8 File Offset: 0x001EA0A8
		private int SortDps(NKCUIBattleStatistics.UnitBattleData a, NKCUIBattleStatistics.UnitBattleData b)
		{
			if (a == null && b == null)
			{
				Debug.LogError("recordData null!");
				return 0;
			}
			if (a == null)
			{
				Debug.LogError("recordData null!");
				return 1;
			}
			if (b == null)
			{
				Debug.LogError("recordData null!");
				return -1;
			}
			NKMGameRecordUnitData recordData = a.recordData;
			NKMGameRecordUnitData recordData2 = b.recordData;
			if (recordData == null && recordData2 == null)
			{
				Debug.LogError(string.Format("recordData for {0}, {1} null?", a.UnitID, b.UnitID));
				return 0;
			}
			if (recordData == null)
			{
				Debug.LogError(string.Format("recordData for {0} null?", a.UnitID));
				return 1;
			}
			if (recordData2 == null)
			{
				Debug.LogError(string.Format("recordData for {0} null?", b.UnitID));
				return -1;
			}
			if (recordData.playtime <= 0)
			{
				return 1;
			}
			if (recordData2.playtime <= 0)
			{
				return -1;
			}
			float num = recordData.recordGiveDamage / (float)recordData.playtime;
			float num2 = recordData2.recordGiveDamage / (float)recordData2.playtime;
			if (num != num2)
			{
				return num2.CompareTo(num);
			}
			return this.SortCommon(a, b);
		}

		// Token: 0x06006219 RID: 25113 RVA: 0x001EBFA8 File Offset: 0x001EA1A8
		private int SortCommon(NKCUIBattleStatistics.UnitBattleData a, NKCUIBattleStatistics.UnitBattleData b)
		{
			if (a.recordData.isLeader != b.recordData.isLeader)
			{
				return a.recordData.isLeader.CompareTo(b.recordData.isLeader);
			}
			if (a.recordData.isAssistUnit != b.recordData.isAssistUnit)
			{
				return a.recordData.isAssistUnit.CompareTo(b.recordData.isAssistUnit);
			}
			if (a.recordData.isSummonee != b.recordData.isSummonee)
			{
				return a.recordData.isSummonee.CompareTo(b.recordData.isSummonee);
			}
			return a.UnitID.CompareTo(b.UnitID);
		}

		// Token: 0x04004E02 RID: 19970
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_RESULT";

		// Token: 0x04004E03 RID: 19971
		public const string UI_ASSET_NAME = "NKM_UI_RESULT_BATTLE_STATISTICS";

		// Token: 0x04004E04 RID: 19972
		private static NKCUIBattleStatistics m_Instance;

		// Token: 0x04004E05 RID: 19973
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04004E06 RID: 19974
		public Transform m_contents;

		// Token: 0x04004E07 RID: 19975
		public NKCUIComToggle m_tgDps;

		// Token: 0x04004E08 RID: 19976
		public Text m_subTitleText;

		// Token: 0x04004E09 RID: 19977
		public Text m_txtPlayTime;

		// Token: 0x04004E0A RID: 19978
		private List<NKCUIBattleStatisticsSlot> m_slotList = new List<NKCUIBattleStatisticsSlot>();

		// Token: 0x04004E0B RID: 19979
		private NKCUIBattleStatistics.BattleData m_battleData;

		// Token: 0x04004E0C RID: 19980
		private int TEMP_SLOT_COUNT = 10;

		// Token: 0x04004E0D RID: 19981
		private NKCUIBattleStatistics.OnClose dOnClose;

		// Token: 0x0200161B RID: 5659
		public class UnitBattleData
		{
			// Token: 0x0400A33D RID: 41789
			public int UnitID;

			// Token: 0x0400A33E RID: 41790
			public NKMUnitData unitData;

			// Token: 0x0400A33F RID: 41791
			public NKMGameRecordUnitData recordData;

			// Token: 0x0400A340 RID: 41792
			public bool bSummon;

			// Token: 0x0400A341 RID: 41793
			public bool bLeader;

			// Token: 0x0400A342 RID: 41794
			public bool bAssist;
		}

		// Token: 0x0200161C RID: 5660
		public class BattleData
		{
			// Token: 0x0400A343 RID: 41795
			public NKCUIBattleStatistics.UnitBattleData mainShipA;

			// Token: 0x0400A344 RID: 41796
			public NKCUIBattleStatistics.UnitBattleData mainShipB;

			// Token: 0x0400A345 RID: 41797
			public List<NKCUIBattleStatistics.UnitBattleData> teamA = new List<NKCUIBattleStatistics.UnitBattleData>();

			// Token: 0x0400A346 RID: 41798
			public List<NKCUIBattleStatistics.UnitBattleData> teamB = new List<NKCUIBattleStatistics.UnitBattleData>();

			// Token: 0x0400A347 RID: 41799
			public float maxValue;

			// Token: 0x0400A348 RID: 41800
			public float maxDps;

			// Token: 0x0400A349 RID: 41801
			public float playTime;

			// Token: 0x0400A34A RID: 41802
			public bool m_bBanGame;

			// Token: 0x0400A34B RID: 41803
			public bool m_bUpUnitGame;

			// Token: 0x0400A34C RID: 41804
			public bool m_bHideUpBanUITeamA;
		}

		// Token: 0x0200161D RID: 5661
		// (Invoke) Token: 0x0600AF3A RID: 44858
		public delegate void OnClose();
	}
}

using System;
using System.Collections.Generic;
using ClientPacket.Raid;
using ClientPacket.WorldMap;
using Cs.Math;
using DG.Tweening;
using NKC.UI.Guide;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009D3 RID: 2515
	public class NKCUIRaidRightSide : NKCUIInstantiatable
	{
		// Token: 0x06006BA3 RID: 27555 RVA: 0x00231421 File Offset: 0x0022F621
		public NKMRaidTemplet GetRaidTemplet()
		{
			return NKMRaidTemplet.Find(this.m_RaidID);
		}

		// Token: 0x06006BA4 RID: 27556 RVA: 0x00231430 File Offset: 0x0022F630
		public static NKCUIRaidRightSide OpenInstance(Transform trParent, NKCUIRaidRightSide.onClickAttackBtn _onClickAttackBtn = null)
		{
			NKCUIRaidRightSide nkcuiraidRightSide = NKCUIInstantiatable.OpenInstance<NKCUIRaidRightSide>("AB_UI_NKM_UI_WORLD_MAP_RAID", "NKM_UI_WORLD_MAP_RAID_RIGHT", trParent);
			if (nkcuiraidRightSide != null)
			{
				nkcuiraidRightSide.Init(_onClickAttackBtn);
			}
			return nkcuiraidRightSide;
		}

		// Token: 0x06006BA5 RID: 27557 RVA: 0x0023145F File Offset: 0x0022F65F
		public void CloseInstance()
		{
			this.m_lbAttackCost.DOKill(false);
			base.CloseInstance("AB_UI_NKM_UI_WORLD_MAP_RAID", "NKM_UI_WORLD_MAP_RAID_RIGHT");
		}

		// Token: 0x06006BA6 RID: 27558 RVA: 0x00231480 File Offset: 0x0022F680
		public void Init(NKCUIRaidRightSide.onClickAttackBtn _onClickAttackBtn = null)
		{
			this.m_dOnClickAttackBtn = _onClickAttackBtn;
			this.m_csbtnReady.PointerClick.RemoveAllListeners();
			this.m_csbtnReady.PointerClick.AddListener(new UnityAction(this.OnClickReadyBtn));
			this.m_csbtnClear.PointerClick.RemoveAllListeners();
			this.m_csbtnClear.PointerClick.AddListener(new UnityAction(this.OnClickAttackBtn));
			this.m_csbtnExit.PointerClick.RemoveAllListeners();
			this.m_csbtnExit.PointerClick.AddListener(new UnityAction(this.OnClickExitBtn));
			this.m_lsrReward.dOnGetObject += this.GetSlot;
			this.m_lsrReward.dOnReturnObject += this.ReturnSlot;
			this.m_lsrReward.dOnProvideData += this.ProvideSlotData;
			this.m_tglEquip1.OnValueChanged.RemoveAllListeners();
			this.m_tglEquip1.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedEquips));
			this.m_tglEquip2.OnValueChanged.RemoveAllListeners();
			this.m_tglEquip2.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedEquips));
			this.m_tglEquip3.OnValueChanged.RemoveAllListeners();
			this.m_tglEquip3.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedEquips));
			this.m_tglEquip4.OnValueChanged.RemoveAllListeners();
			this.m_tglEquip4.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangedEquips));
			this.m_csbtnInfo.PointerClick.RemoveAllListeners();
			this.m_csbtnInfo.PointerClick.AddListener(new UnityAction(this.OnClickInfoBtn));
			if (this.m_btnGuide != null)
			{
				this.m_btnGuide.PointerClick.RemoveAllListeners();
				this.m_btnGuide.PointerClick.AddListener(new UnityAction(this.OnClickGuide));
			}
		}

		// Token: 0x06006BA7 RID: 27559 RVA: 0x00231673 File Offset: 0x0022F873
		private void OnChangedEquips(bool bChanged)
		{
			this.UpdateAttackCostUI();
		}

		// Token: 0x06006BA8 RID: 27560 RVA: 0x0023167C File Offset: 0x0022F87C
		private void OnClickUnitRewardSlot(bool bIsFinder)
		{
			List<int> listNRGI = this.m_listNRGI;
			HashSet<int> rewardIDs = NKCUtil.GetRewardIDs(listNRGI, NKM_REWARD_TYPE.RT_UNIT);
			HashSet<int> rewardIDs2 = NKCUtil.GetRewardIDs(listNRGI, NKM_REWARD_TYPE.RT_OPERATOR);
			List<NKMUnitTempletBase> list = new List<NKMUnitTempletBase>();
			foreach (int unitID in rewardIDs)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
				if (unitTempletBase != null)
				{
					list.Add(unitTempletBase);
				}
			}
			foreach (int unitID2 in rewardIDs2)
			{
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(unitID2);
				if (unitTempletBase2 != null)
				{
					list.Add(unitTempletBase2);
				}
			}
			list.Sort(new CompTemplet.CompNUTB());
			List<int> list2 = new List<int>();
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] != null)
				{
					list2.Add(list[i].m_UnitID);
				}
			}
			NKCUISlotListViewer.Instance.OpenRewardList(list2, NKM_REWARD_TYPE.RT_UNIT, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
		}

		// Token: 0x06006BA9 RID: 27561 RVA: 0x00231794 File Offset: 0x0022F994
		private void OnClickEquipRewardSlot(bool bIsFinder)
		{
			HashSet<int> rewardIDs = NKCUtil.GetRewardIDs(this.m_listNRGI, NKM_REWARD_TYPE.RT_EQUIP);
			List<NKMEquipTemplet> list = new List<NKMEquipTemplet>();
			foreach (int equipID in rewardIDs)
			{
				NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(equipID);
				if (equipTemplet != null)
				{
					list.Add(equipTemplet);
				}
			}
			list.Sort(new CompTemplet.CompNET());
			List<int> list2 = new List<int>();
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] != null)
				{
					list2.Add(list[i].m_ItemEquipID);
				}
			}
			NKCUISlotListViewer.Instance.OpenRewardList(list2, NKM_REWARD_TYPE.RT_EQUIP, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
		}

		// Token: 0x06006BAA RID: 27562 RVA: 0x00231854 File Offset: 0x0022FA54
		private void OnClickMoldRewardSlot(bool bIsFinder)
		{
			HashSet<int> rewardIDs = NKCUtil.GetRewardIDs(this.m_listNRGI, NKM_REWARD_TYPE.RT_MOLD);
			List<NKMItemMoldTemplet> list = new List<NKMItemMoldTemplet>();
			foreach (int moldID in rewardIDs)
			{
				NKMItemMoldTemplet itemMoldTempletByID = NKMItemManager.GetItemMoldTempletByID(moldID);
				if (itemMoldTempletByID != null)
				{
					list.Add(itemMoldTempletByID);
				}
			}
			list.Sort(new CompTemplet.CompNMT());
			List<int> list2 = new List<int>();
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] != null)
				{
					list2.Add(list[i].m_MoldID);
				}
			}
			NKCUISlotListViewer.Instance.OpenRewardList(list2, NKM_REWARD_TYPE.RT_MOLD, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
		}

		// Token: 0x06006BAB RID: 27563 RVA: 0x00231914 File Offset: 0x0022FB14
		private void OnClickMiscRewardSlot(bool bIsFinder)
		{
			HashSet<int> rewardIDs = NKCUtil.GetRewardIDs(this.m_listNRGI, NKM_REWARD_TYPE.RT_MISC);
			List<NKMItemMiscTemplet> list = new List<NKMItemMiscTemplet>();
			foreach (int itemMiscID in rewardIDs)
			{
				NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemMiscID);
				if (itemMiscTempletByID != null)
				{
					list.Add(itemMiscTempletByID);
				}
			}
			list.Sort(new CompTemplet.CompNIMT());
			List<int> list2 = new List<int>();
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i] != null)
				{
					list2.Add(list[i].m_ItemMiscID);
				}
			}
			NKCUISlotListViewer.Instance.OpenRewardList(list2, NKM_REWARD_TYPE.RT_MISC, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
		}

		// Token: 0x06006BAC RID: 27564 RVA: 0x002319D4 File Offset: 0x0022FBD4
		private RectTransform GetSlot(int index)
		{
			NKCUISlot nkcuislot = UnityEngine.Object.Instantiate<NKCUISlot>(this.m_pfbUISlot);
			nkcuislot.Init();
			return nkcuislot.GetComponent<RectTransform>();
		}

		// Token: 0x06006BAD RID: 27565 RVA: 0x002319EC File Offset: 0x0022FBEC
		private void ReturnSlot(Transform tr)
		{
			tr.SetParent(base.transform);
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06006BAE RID: 27566 RVA: 0x00231A08 File Offset: 0x0022FC08
		private void ProvideSlotData(Transform tr, int idx)
		{
			NKCUISlot component = tr.GetComponent<NKCUISlot>();
			long userUID = NKCScenManager.CurrentUserData().m_UserUID;
			bool flag = NKCUtil.CheckExistRewardType(this.m_listNRGI, NKM_REWARD_TYPE.RT_UNIT) || NKCUtil.CheckExistRewardType(this.m_listNRGI, NKM_REWARD_TYPE.RT_OPERATOR);
			bool flag2 = NKCUtil.CheckExistRewardType(this.m_listNRGI, NKM_REWARD_TYPE.RT_EQUIP);
			bool flag3 = NKCUtil.CheckExistRewardType(this.m_listNRGI, NKM_REWARD_TYPE.RT_MOLD);
			bool flag4 = NKCUtil.CheckExistRewardType(this.m_listNRGI, NKM_REWARD_TYPE.RT_MISC);
			int num = 0;
			if (flag)
			{
				if (num == idx)
				{
					int maxGradeInRewardGroups = NKCUtil.GetMaxGradeInRewardGroups(this.m_listNRGI, NKM_REWARD_TYPE.RT_UNIT);
					int maxGradeInRewardGroups2 = NKCUtil.GetMaxGradeInRewardGroups(this.m_listNRGI, NKM_REWARD_TYPE.RT_OPERATOR);
					this.SetSlotDataByRandomItem(component, 901, delegate(NKCUISlot.SlotData slotData, bool bLocked)
					{
						this.OnClickUnitRewardSlot(true);
					}, Mathf.Max(maxGradeInRewardGroups, maxGradeInRewardGroups2));
					return;
				}
				num++;
			}
			if (flag2)
			{
				if (num == idx)
				{
					int maxGradeInRewardGroups3 = NKCUtil.GetMaxGradeInRewardGroups(this.m_listNRGI, NKM_REWARD_TYPE.RT_EQUIP);
					this.SetSlotDataByRandomItem(component, 902, delegate(NKCUISlot.SlotData slotData, bool bLocked)
					{
						this.OnClickEquipRewardSlot(true);
					}, maxGradeInRewardGroups3);
					return;
				}
				num++;
			}
			if (flag3)
			{
				if (num == idx)
				{
					int maxGradeInRewardGroups4 = NKCUtil.GetMaxGradeInRewardGroups(this.m_listNRGI, NKM_REWARD_TYPE.RT_MOLD);
					this.SetSlotDataByRandomItem(component, 904, delegate(NKCUISlot.SlotData slotData, bool bLocked)
					{
						this.OnClickMoldRewardSlot(true);
					}, maxGradeInRewardGroups4);
					return;
				}
				num++;
			}
			if (flag4)
			{
				if (num == idx)
				{
					int maxGradeInRewardGroups5 = NKCUtil.GetMaxGradeInRewardGroups(this.m_listNRGI, NKM_REWARD_TYPE.RT_MISC);
					this.SetSlotDataByRandomItem(component, 903, delegate(NKCUISlot.SlotData slotData, bool bLocked)
					{
						this.OnClickMiscRewardSlot(true);
					}, maxGradeInRewardGroups5);
					return;
				}
				num++;
			}
		}

		// Token: 0x06006BAF RID: 27567 RVA: 0x00231B60 File Offset: 0x0022FD60
		private void SetSlotDataByRandomItem(NKCUISlot cSlot, int miscItemID, NKCUISlot.OnClick onClick, int maxGrade)
		{
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(new NKMItemMiscData(miscItemID, 1L, 0L, 0), 0);
			cSlot.SetData(data, false, false, true, onClick);
			cSlot.SetBackGround(maxGrade);
		}

		// Token: 0x06006BB0 RID: 27568 RVA: 0x00231B92 File Offset: 0x0022FD92
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x06006BB1 RID: 27569 RVA: 0x00231BA0 File Offset: 0x0022FDA0
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006BB2 RID: 27570 RVA: 0x00231BB0 File Offset: 0x0022FDB0
		private int GetFinalBuffCost(NKMWorldMapCityData cityData, int orgCost)
		{
			if (cityData == null)
			{
				return orgCost;
			}
			int num = (int)cityData.CalcBuildStat(NKM_CITY_BUILDING_STAT.CBS_RAID_DEFENCE_COST_REDUCE_RATE, (float)orgCost);
			num = Math.Min(orgCost, num);
			return orgCost - num;
		}

		// Token: 0x06006BB3 RID: 27571 RVA: 0x00231BE0 File Offset: 0x0022FDE0
		private void SetEquipUI(NKMRaidTemplet cNKMRaidTemplet, NKMRaidDetailData cNKMRaidDetailData)
		{
			if (cNKMRaidDetailData == null)
			{
				return;
			}
			int num = 1;
			NKMWorldMapCityData cityData = NKCScenManager.CurrentUserData().m_WorldmapData.GetCityData(cNKMRaidDetailData.cityID);
			if (cityData != null)
			{
				num = (int)cityData.CalcBuildStat(NKM_CITY_BUILDING_STAT.CBS_RAID_DEFENCE_LEVEL, 1f);
				num = Math.Min(5, num);
			}
			this.m_tglEquip1.Select(false, true, false);
			this.m_tglEquip2.Select(false, true, false);
			this.m_tglEquip3.Select(false, true, false);
			this.m_tglEquip4.Select(false, true, false);
			NKMRaidBuffTemplet nkmraidBuffTemplet = NKMRaidBuffTemplet.Find(1, num);
			if (nkmraidBuffTemplet != null)
			{
				this.m_lbEquip1Cost.text = this.GetFinalBuffCost(cityData, nkmraidBuffTemplet.RaidBuffCost).ToString();
			}
			else
			{
				this.m_lbEquip1Cost.text = "???";
			}
			nkmraidBuffTemplet = NKMRaidBuffTemplet.Find(2, num);
			if (nkmraidBuffTemplet != null)
			{
				this.m_lbEquip2Cost.text = this.GetFinalBuffCost(cityData, nkmraidBuffTemplet.RaidBuffCost).ToString();
			}
			else
			{
				this.m_lbEquip2Cost.text = "???";
			}
			nkmraidBuffTemplet = NKMRaidBuffTemplet.Find(3, num);
			if (nkmraidBuffTemplet != null)
			{
				this.m_lbEquip3Cost.text = this.GetFinalBuffCost(cityData, nkmraidBuffTemplet.RaidBuffCost).ToString();
			}
			else
			{
				this.m_lbEquip3Cost.text = "???";
			}
			nkmraidBuffTemplet = NKMRaidBuffTemplet.Find(4, num);
			if (nkmraidBuffTemplet != null)
			{
				this.m_lbEquip4Cost.text = this.GetFinalBuffCost(cityData, nkmraidBuffTemplet.RaidBuffCost).ToString();
			}
			else
			{
				this.m_lbEquip4Cost.text = "???";
			}
			string text = string.Format(NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, num);
			this.m_lbEquip1LV.text = text;
			this.m_lbEquip2LV.text = text;
			this.m_lbEquip3LV.text = text;
			this.m_lbEquip4LV.text = text;
			this.m_lbEquip1LVOff.text = text;
			this.m_lbEquip2LVOff.text = text;
			this.m_lbEquip3LVOff.text = text;
			this.m_lbEquip4LVOff.text = text;
		}

		// Token: 0x06006BB4 RID: 27572 RVA: 0x00231DC8 File Offset: 0x0022FFC8
		public void SetUI(long raidUID, NKCUIRaidRightSide.NKC_RAID_SUB_MENU_TYPE eNKC_RAID_SUB_MENU_TYPE = NKCUIRaidRightSide.NKC_RAID_SUB_MENU_TYPE.NRSMT_REMAIN_TIME, NKCUIRaidRightSide.NKC_RAID_SUB_BUTTON_TYPE eNKC_RAID_SUB_BUTTON_TYPE = NKCUIRaidRightSide.NKC_RAID_SUB_BUTTON_TYPE.NRSBT_READY)
		{
			this.m_RaidUID = raidUID;
			this.m_bLockExitBtn = false;
			this.m_cNKMRaidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_RaidUID);
			if (this.m_cNKMRaidDetailData == null)
			{
				return;
			}
			NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(this.m_cNKMRaidDetailData.stageID);
			if (nkmraidTemplet == null)
			{
				return;
			}
			this.m_RaidID = nkmraidTemplet.Key;
			if (this.m_bFirstOpen)
			{
				this.m_bFirstOpen = false;
				NKCUtil.SetGameobjectActive(base.gameObject, true);
				this.m_lsrReward.PrepareCells(0);
			}
			NKCUtil.SetRaidEventPoint(this.m_imgEventPointColor, nkmraidTemplet);
			this.m_lbLevel.text = nkmraidTemplet.RaidLevel.ToString();
			this.m_lbName.text = nkmraidTemplet.DungeonTempletBase.GetDungeonName();
			NKCUtil.SetGameobjectActive(this.m_objAttendLimit, nkmraidTemplet.AttendLimit > 0);
			if (nkmraidTemplet.AttendLimit > 0)
			{
				NKCUtil.SetLabelText(this.m_lbAttendLimit, string.Format("{0}/{1}", this.m_cNKMRaidDetailData.raidJoinDataList.Count, nkmraidTemplet.AttendLimit));
			}
			NKCUtil.SetGameobjectActive(this.m_objTeamOnlyData, nkmraidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_RAID);
			NKCUtil.SetGameobjectActive(this.m_objBossDesc, nkmraidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID);
			if (nkmraidTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_SOLO_RAID)
			{
				NKMDungeonTemplet dungeonTemplet = NKMDungeonManager.GetDungeonTemplet(nkmraidTemplet.DungeonTempletBase.m_DungeonID);
				if (dungeonTemplet != null)
				{
					NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(dungeonTemplet.m_BossUnitStrID);
					if (unitTempletBase != null)
					{
						this.m_lbBossDesc.text = unitTempletBase.GetUnitDesc();
					}
				}
			}
			float num = 0f;
			if (this.m_cNKMRaidDetailData.maxHP.IsNearlyZero(1E-05f))
			{
				this.m_imgBossHP.fillAmount = 0f;
			}
			else
			{
				num = this.m_cNKMRaidDetailData.curHP / this.m_cNKMRaidDetailData.maxHP;
				this.m_imgBossHP.fillAmount = num;
			}
			this.m_lbRemainHP.text = string.Format("{0} ({1:0.##}%)", ((int)this.m_cNKMRaidDetailData.curHP).ToString("N0"), num * 100f);
			NKMRaidJoinData nkmraidJoinData = this.m_cNKMRaidDetailData.FindJoinData(NKCScenManager.CurrentUserData().m_UserUID);
			float num2 = 0f;
			if (nkmraidJoinData != null)
			{
				num2 = nkmraidJoinData.damage;
			}
			float num3;
			if (this.m_cNKMRaidDetailData.maxHP.IsNearlyZero(1E-05f))
			{
				num3 = 0f;
			}
			else
			{
				num3 = num2 / this.m_cNKMRaidDetailData.maxHP;
			}
			this.m_lbMyAccumDmg.text = string.Format("{0} ({1:0.##}%)", ((int)num2).ToString("N0"), num3 * 100f);
			NKCUtil.SetGameobjectActive(this.m_btnGuide, !string.IsNullOrEmpty(nkmraidTemplet.GuideShortCut));
			NKCUtil.SetGameobjectActive(this.m_objWinPoint_Entry, false);
			NKCUtil.SetGameobjectActive(this.m_objLosePoint_Entry, false);
			NKCUtil.SetLabelText(this.m_lbWinPoint, nkmraidTemplet.RewardRaidPoint_Victory.ToString("N0"));
			NKCUtil.SetLabelText(this.m_lbLosePoint, nkmraidTemplet.RewardRaidPoint_Fail.ToString("N0"));
			NKMRaidSeasonTemplet nowSeasonTemplet = NKCRaidSeasonManager.GetNowSeasonTemplet();
			bool flag = nowSeasonTemplet != null && nowSeasonTemplet.RaidSeasonId == this.m_cNKMRaidDetailData.seasonID;
			NKCUtil.SetGameobjectActive(this.m_objRaidPoint, flag && eNKC_RAID_SUB_MENU_TYPE == NKCUIRaidRightSide.NKC_RAID_SUB_MENU_TYPE.NRSMT_REMAIN_TIME);
			NKCUtil.SetGameobjectActive(this.m_objReward, true);
			NKCUtil.SetGameobjectActive(this.m_lsrReward.content, true);
			NKCUtil.SetGameobjectActive(this.m_objRemainTime, eNKC_RAID_SUB_MENU_TYPE == NKCUIRaidRightSide.NKC_RAID_SUB_MENU_TYPE.NRSMT_REMAIN_TIME);
			NKCUtil.SetGameobjectActive(this.m_objSupportEquip, eNKC_RAID_SUB_MENU_TYPE == NKCUIRaidRightSide.NKC_RAID_SUB_MENU_TYPE.NRSMT_SUPPORT_EQUIP);
			if (eNKC_RAID_SUB_MENU_TYPE == NKCUIRaidRightSide.NKC_RAID_SUB_MENU_TYPE.NRSMT_REMAIN_TIME)
			{
				bool flag2 = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().CheckCompletableRaid(this.m_RaidUID);
				NKCUtil.SetGameobjectActive(this.m_objRemainTime_Play, !flag2);
				NKCUtil.SetGameobjectActive(this.m_objRemainTime_Clear, flag2);
				if (!flag2)
				{
					this.UpdateRemainTime();
				}
			}
			else if (eNKC_RAID_SUB_MENU_TYPE == NKCUIRaidRightSide.NKC_RAID_SUB_MENU_TYPE.NRSMT_SUPPORT_EQUIP)
			{
				this.SetEquipUI(nkmraidTemplet, this.m_cNKMRaidDetailData);
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnReady.gameObject, eNKC_RAID_SUB_BUTTON_TYPE == NKCUIRaidRightSide.NKC_RAID_SUB_BUTTON_TYPE.NRSBT_READY);
			NKCUtil.SetGameobjectActive(this.m_csbtnClear.gameObject, eNKC_RAID_SUB_BUTTON_TYPE == NKCUIRaidRightSide.NKC_RAID_SUB_BUTTON_TYPE.NRSBT_ATTACK);
			NKCUtil.SetGameobjectActive(this.m_csbtnExit.gameObject, eNKC_RAID_SUB_BUTTON_TYPE == NKCUIRaidRightSide.NKC_RAID_SUB_BUTTON_TYPE.NRSBT_EXIT);
			NKCUtil.SetGameobjectActive(this.m_objRemainCount, eNKC_RAID_SUB_BUTTON_TYPE == NKCUIRaidRightSide.NKC_RAID_SUB_BUTTON_TYPE.NRSBT_ATTACK);
			int num4 = (int)((nkmraidJoinData != null) ? nkmraidJoinData.tryCount : 0);
			this.m_lbRemainCount.text = string.Format(NKCUtilString.GET_STRING_RAID_REMAIN_COUNT_ONE_PARAM, nkmraidTemplet.RaidTryCount - num4);
			this.m_listNRGI = new List<int>();
			int num5 = 0;
			bool bVictory;
			bool bFail;
			if (this.m_cNKMRaidDetailData.curHP > 0f)
			{
				if (NKCSynchronizedTime.IsFinished(this.m_cNKMRaidDetailData.expireDate))
				{
					bVictory = false;
					bFail = true;
				}
				else
				{
					bVictory = true;
					bFail = true;
				}
			}
			else
			{
				bVictory = true;
				bFail = false;
			}
			List<int> listNRGI = this.GetListNRGI(bVictory, bFail);
			this.m_listNRGI.AddRange(listNRGI);
			bool flag3 = NKCUtil.CheckExistRewardType(listNRGI, NKM_REWARD_TYPE.RT_UNIT) || NKCUtil.CheckExistRewardType(listNRGI, NKM_REWARD_TYPE.RT_OPERATOR);
			bool flag4 = NKCUtil.CheckExistRewardType(listNRGI, NKM_REWARD_TYPE.RT_EQUIP);
			bool flag5 = NKCUtil.CheckExistRewardType(listNRGI, NKM_REWARD_TYPE.RT_MOLD);
			bool flag6 = NKCUtil.CheckExistRewardType(listNRGI, NKM_REWARD_TYPE.RT_MISC);
			if (flag3)
			{
				num5++;
			}
			if (flag4)
			{
				num5++;
			}
			if (flag5)
			{
				num5++;
			}
			if (flag6)
			{
				num5++;
			}
			this.m_lsrReward.TotalCount = num5;
			this.m_lsrReward.SetIndexPosition(0);
			Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon((nkmraidTemplet != null) ? nkmraidTemplet.StageReqItemID : 3);
			NKCUtil.SetImageSprite(this.m_imgAttackCost, orLoadMiscItemSmallIcon, false);
			NKCUtil.SetImageSprite(this.m_imgEquip1Cost, orLoadMiscItemSmallIcon, false);
			NKCUtil.SetImageSprite(this.m_imgEquip2Cost, orLoadMiscItemSmallIcon, false);
			NKCUtil.SetImageSprite(this.m_imgEquip3Cost, orLoadMiscItemSmallIcon, false);
			NKCUtil.SetImageSprite(this.m_imgEquip4Cost, orLoadMiscItemSmallIcon, false);
			this.UpdateAttackCostUI();
		}

		// Token: 0x06006BB5 RID: 27573 RVA: 0x0023236C File Offset: 0x0023056C
		private void UpdateBtns()
		{
			this.m_cNKMRaidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_RaidUID);
			if (this.m_cNKMRaidDetailData == null)
			{
				return;
			}
			if (this.m_cNKMRaidDetailData.curHP <= 0f || NKCSynchronizedTime.IsFinished(this.m_cNKMRaidDetailData.expireDate))
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnReady.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnClear.gameObject, false);
				NKCUtil.SetGameobjectActive(this.m_csbtnExit.gameObject, true);
				NKCUtil.SetGameobjectActive(this.m_objRemainCount, false);
			}
		}

		// Token: 0x06006BB6 RID: 27574 RVA: 0x00232400 File Offset: 0x00230600
		public int GetCostByCurrSetting()
		{
			this.m_cNKMRaidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_RaidUID);
			if (this.m_cNKMRaidDetailData == null)
			{
				return 0;
			}
			NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(this.m_cNKMRaidDetailData.stageID);
			if (nkmraidTemplet == null)
			{
				return 0;
			}
			int num = nkmraidTemplet.StageReqItemCount;
			if (this.m_tglEquip1.m_bChecked)
			{
				int num2 = 0;
				if (int.TryParse(this.m_lbEquip1Cost.text, out num2))
				{
					num += num2;
				}
			}
			if (this.m_tglEquip2.m_bChecked)
			{
				int num3 = 0;
				if (int.TryParse(this.m_lbEquip2Cost.text, out num3))
				{
					num += num3;
				}
			}
			if (this.m_tglEquip3.m_bChecked)
			{
				int num4 = 0;
				if (int.TryParse(this.m_lbEquip3Cost.text, out num4))
				{
					num += num4;
				}
			}
			if (this.m_tglEquip4.m_bChecked)
			{
				int num5 = 0;
				if (int.TryParse(this.m_lbEquip4Cost.text, out num5))
				{
					num += num5;
				}
			}
			return num;
		}

		// Token: 0x06006BB7 RID: 27575 RVA: 0x002324F0 File Offset: 0x002306F0
		private void UpdateAttackCostUI()
		{
			if (!this.m_csbtnClear.gameObject.activeSelf)
			{
				return;
			}
			int costByCurrSetting = this.GetCostByCurrSetting();
			this.m_lbAttackCost.DOText(costByCurrSetting.ToString(), 0.4f, true, ScrambleMode.Numerals, null);
		}

		// Token: 0x06006BB8 RID: 27576 RVA: 0x00232534 File Offset: 0x00230734
		private void UpdateRemainTime()
		{
			if (!this.m_objRemainTime_Play.activeSelf)
			{
				return;
			}
			this.m_cNKMRaidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_RaidUID);
			if (this.m_cNKMRaidDetailData == null)
			{
				return;
			}
			if (NKCSynchronizedTime.IsFinished(this.m_cNKMRaidDetailData.expireDate))
			{
				NKCUtil.SetLabelText(this.m_lbRemainTime, string.Format(NKCUtilString.GET_STRING_WORLD_MAP_RAID_REMAIN_TIME, NKCUtilString.GetTimeSpanString(TimeSpan.Zero)));
				return;
			}
			DateTime d = new DateTime(this.m_cNKMRaidDetailData.expireDate);
			DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
			TimeSpan timeSpan = d - serverUTCTime;
			NKCUtil.SetLabelText(this.m_lbRemainTime, string.Format(NKCUtilString.GET_STRING_WORLD_MAP_RAID_REMAIN_TIME, NKCUtilString.GetTimeSpanString(timeSpan)));
		}

		// Token: 0x06006BB9 RID: 27577 RVA: 0x002325E6 File Offset: 0x002307E6
		private void Update()
		{
			if (this.m_fNextUpdateTime + 1f < Time.time)
			{
				this.UpdateRemainTime();
				this.UpdateBtns();
				this.m_fNextUpdateTime = Time.time;
			}
		}

		// Token: 0x06006BBA RID: 27578 RVA: 0x00232614 File Offset: 0x00230814
		private void OnClickReadyBtn()
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCUtil.CheckCommonStartCond(NKCScenManager.CurrentUserData());
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCUtil.OnExpandInventoryPopup(nkm_ERROR_CODE);
				return;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID_READY().SetRaidUID(this.m_RaidUID);
			NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID_READY().SetGuildRaid(false);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_RAID_READY, true);
		}

		// Token: 0x06006BBB RID: 27579 RVA: 0x00232668 File Offset: 0x00230868
		private void OnClickAttackBtn()
		{
			if (this.m_dOnClickAttackBtn != null)
			{
				List<int> list = new List<int>();
				if (this.m_tglEquip1.m_bChecked)
				{
					list.Add(1);
				}
				if (this.m_tglEquip2.m_bChecked)
				{
					list.Add(2);
				}
				if (this.m_tglEquip3.m_bChecked)
				{
					list.Add(3);
				}
				if (this.m_tglEquip4.m_bChecked)
				{
					list.Add(4);
				}
				int reqItemID = 0;
				NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(this.m_RaidID);
				if (nkmraidTemplet != null)
				{
					reqItemID = nkmraidTemplet.StageReqItemID;
				}
				this.m_dOnClickAttackBtn(this.m_RaidUID, list, reqItemID, this.GetCostByCurrSetting(), false);
			}
		}

		// Token: 0x06006BBC RID: 27580 RVA: 0x00232708 File Offset: 0x00230908
		private void OnClickExitBtn()
		{
			if (!this.m_bLockExitBtn)
			{
				this.m_bLockExitBtn = true;
				NKCPacketSender.Send_NKMPacket_RAID_RESULT_ACCEPT_REQ(this.m_RaidUID);
			}
		}

		// Token: 0x06006BBD RID: 27581 RVA: 0x00232724 File Offset: 0x00230924
		private void OnClickGuide()
		{
			this.m_cNKMRaidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_RaidUID);
			if (this.m_cNKMRaidDetailData == null)
			{
				return;
			}
			NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(this.m_cNKMRaidDetailData.stageID);
			if (nkmraidTemplet == null)
			{
				return;
			}
			if (string.IsNullOrEmpty(nkmraidTemplet.GuideShortCut))
			{
				return;
			}
			NKCUIPopupTutorialImagePanel.Instance.Open(nkmraidTemplet.GuideShortCut, null);
		}

		// Token: 0x06006BBE RID: 27582 RVA: 0x0023278C File Offset: 0x0023098C
		private void OnClickInfoBtn()
		{
			this.m_cNKMRaidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_RaidUID);
			if (this.m_cNKMRaidDetailData == null)
			{
				return;
			}
			NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(this.m_cNKMRaidDetailData.stageID);
			if (nkmraidTemplet == null)
			{
				return;
			}
			NKCPopupEnemyList.Instance.Open(nkmraidTemplet.DungeonTempletBase);
		}

		// Token: 0x06006BBF RID: 27583 RVA: 0x002327E4 File Offset: 0x002309E4
		public List<int> GetListNRGI(bool bVictory, bool bFail)
		{
			List<int> list = new List<int>();
			if (this.m_cNKMRaidDetailData == null)
			{
				return list;
			}
			NKMRaidJoinData nkmraidJoinData = this.m_cNKMRaidDetailData.FindJoinData(NKCScenManager.CurrentUserData().m_UserUID);
			if (nkmraidJoinData != null && nkmraidJoinData.tryAssist)
			{
				return list;
			}
			NKMRaidTemplet nkmraidTemplet = NKMRaidTemplet.Find(this.m_cNKMRaidDetailData.stageID);
			if (nkmraidTemplet == null)
			{
				return list;
			}
			if (bVictory && !list.Contains(nkmraidTemplet.RewardRaidGroupID_Victory))
			{
				list.Add(nkmraidTemplet.RewardRaidGroupID_Victory);
			}
			if (bFail && !list.Contains(nkmraidTemplet.RewardRaidGroupID_Fail))
			{
				list.Add(nkmraidTemplet.RewardRaidGroupID_Fail);
			}
			return list;
		}

		// Token: 0x04005745 RID: 22341
		private NKCUIRaidRightSide.onClickAttackBtn m_dOnClickAttackBtn;

		// Token: 0x04005746 RID: 22342
		[Header("기본 정보")]
		public Text m_lbLevel;

		// Token: 0x04005747 RID: 22343
		public Text m_lbName;

		// Token: 0x04005748 RID: 22344
		public NKCUIComStateButton m_btnGuide;

		// Token: 0x04005749 RID: 22345
		public NKCUIComStateButton m_csbtnInfo;

		// Token: 0x0400574A RID: 22346
		public Image m_imgBossHP;

		// Token: 0x0400574B RID: 22347
		public Text m_lbRemainHP;

		// Token: 0x0400574C RID: 22348
		public Text m_lbMyAccumDmg;

		// Token: 0x0400574D RID: 22349
		public GameObject m_objTeamOnlyData;

		// Token: 0x0400574E RID: 22350
		public GameObject m_objBossDesc;

		// Token: 0x0400574F RID: 22351
		public Text m_lbBossDesc;

		// Token: 0x04005750 RID: 22352
		public GameObject m_objAttendLimit;

		// Token: 0x04005751 RID: 22353
		public Text m_lbAttendLimit;

		// Token: 0x04005752 RID: 22354
		public Image m_imgEventPointColor;

		// Token: 0x04005753 RID: 22355
		[Header("점수")]
		public GameObject m_objRaidPoint;

		// Token: 0x04005754 RID: 22356
		public Text m_lbWinPoint;

		// Token: 0x04005755 RID: 22357
		public GameObject m_objWinPoint_Entry;

		// Token: 0x04005756 RID: 22358
		public Text m_lbWinPoint_Entry;

		// Token: 0x04005757 RID: 22359
		public Text m_lbLosePoint;

		// Token: 0x04005758 RID: 22360
		public GameObject m_objLosePoint_Entry;

		// Token: 0x04005759 RID: 22361
		public Text m_lbLosePoint_Entry;

		// Token: 0x0400575A RID: 22362
		[Header("보상")]
		public GameObject m_objReward;

		// Token: 0x0400575B RID: 22363
		public LoopScrollRect m_lsrReward;

		// Token: 0x0400575C RID: 22364
		public NKCUISlot m_pfbUISlot;

		// Token: 0x0400575D RID: 22365
		[Header("남은 시간")]
		public GameObject m_objRemainTime;

		// Token: 0x0400575E RID: 22366
		public Text m_lbRemainTime;

		// Token: 0x0400575F RID: 22367
		public GameObject m_objRemainTime_Play;

		// Token: 0x04005760 RID: 22368
		public GameObject m_objRemainTime_Clear;

		// Token: 0x04005761 RID: 22369
		[Header("지원 장비")]
		public GameObject m_objSupportEquip;

		// Token: 0x04005762 RID: 22370
		public NKCUIComToggle m_tglEquip1;

		// Token: 0x04005763 RID: 22371
		public NKCUIComToggle m_tglEquip2;

		// Token: 0x04005764 RID: 22372
		public NKCUIComToggle m_tglEquip3;

		// Token: 0x04005765 RID: 22373
		public NKCUIComToggle m_tglEquip4;

		// Token: 0x04005766 RID: 22374
		public Text m_lbEquip1LV;

		// Token: 0x04005767 RID: 22375
		public Text m_lbEquip2LV;

		// Token: 0x04005768 RID: 22376
		public Text m_lbEquip3LV;

		// Token: 0x04005769 RID: 22377
		public Text m_lbEquip4LV;

		// Token: 0x0400576A RID: 22378
		public Text m_lbEquip1LVOff;

		// Token: 0x0400576B RID: 22379
		public Text m_lbEquip2LVOff;

		// Token: 0x0400576C RID: 22380
		public Text m_lbEquip3LVOff;

		// Token: 0x0400576D RID: 22381
		public Text m_lbEquip4LVOff;

		// Token: 0x0400576E RID: 22382
		public Text m_lbEquip1Cost;

		// Token: 0x0400576F RID: 22383
		public Text m_lbEquip2Cost;

		// Token: 0x04005770 RID: 22384
		public Text m_lbEquip3Cost;

		// Token: 0x04005771 RID: 22385
		public Text m_lbEquip4Cost;

		// Token: 0x04005772 RID: 22386
		public Image m_imgEquip1Cost;

		// Token: 0x04005773 RID: 22387
		public Image m_imgEquip2Cost;

		// Token: 0x04005774 RID: 22388
		public Image m_imgEquip3Cost;

		// Token: 0x04005775 RID: 22389
		public Image m_imgEquip4Cost;

		// Token: 0x04005776 RID: 22390
		[Header("남은 횟수")]
		public GameObject m_objRemainCount;

		// Token: 0x04005777 RID: 22391
		public Text m_lbRemainCount;

		// Token: 0x04005778 RID: 22392
		[Header("맨 아래 버튼 모음")]
		public NKCUIComStateButton m_csbtnReady;

		// Token: 0x04005779 RID: 22393
		public NKCUIComStateButton m_csbtnClear;

		// Token: 0x0400577A RID: 22394
		public Image m_imgAttackCost;

		// Token: 0x0400577B RID: 22395
		public Text m_lbAttackCost;

		// Token: 0x0400577C RID: 22396
		public NKCUIComStateButton m_csbtnExit;

		// Token: 0x0400577D RID: 22397
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_WORLD_MAP_RAID";

		// Token: 0x0400577E RID: 22398
		private const string UI_ASSET_NAME = "NKM_UI_WORLD_MAP_RAID_RIGHT";

		// Token: 0x0400577F RID: 22399
		private long m_RaidUID;

		// Token: 0x04005780 RID: 22400
		private int m_RaidID;

		// Token: 0x04005781 RID: 22401
		private List<int> m_listNRGI = new List<int>();

		// Token: 0x04005782 RID: 22402
		private float m_fNextUpdateTime;

		// Token: 0x04005783 RID: 22403
		private NKMRaidDetailData m_cNKMRaidDetailData;

		// Token: 0x04005784 RID: 22404
		private bool m_bLockExitBtn;

		// Token: 0x04005785 RID: 22405
		private bool m_bFirstOpen = true;

		// Token: 0x020016DA RID: 5850
		// (Invoke) Token: 0x0600B187 RID: 45447
		public delegate void onClickAttackBtn(long raidUID, List<int> _buffs, int reqItemID, int reqItemCount, bool bIsTryAssist);

		// Token: 0x020016DB RID: 5851
		public enum NKC_RAID_SUB_MENU_TYPE
		{
			// Token: 0x0400A556 RID: 42326
			NRSMT_REMAIN_TIME,
			// Token: 0x0400A557 RID: 42327
			NRSMT_SUPPORT_EQUIP
		}

		// Token: 0x020016DC RID: 5852
		public enum NKC_RAID_SUB_BUTTON_TYPE
		{
			// Token: 0x0400A559 RID: 42329
			NRSBT_READY,
			// Token: 0x0400A55A RID: 42330
			NRSBT_ATTACK,
			// Token: 0x0400A55B RID: 42331
			NRSBT_EXIT
		}
	}
}

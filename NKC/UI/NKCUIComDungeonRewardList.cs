using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000932 RID: 2354
	public class NKCUIComDungeonRewardList : MonoBehaviour
	{
		// Token: 0x06005E03 RID: 24067 RVA: 0x001D1060 File Offset: 0x001CF260
		public void InitUI()
		{
			this.m_rectListContent = this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content.GetComponent<RectTransform>();
			this.m_ContentOrgPosX = this.m_rectListContent.anchoredPosition.x;
			this.m_listRewardData.Clear();
			this.m_listRewardData.Add(new FirstClearData(this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content.transform));
			this.m_listRewardData.Add(new FirstAllClearData(this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content.transform));
			for (int i = 0; i < 3; i++)
			{
				this.m_listRewardData.Add(new OneTimeReward(this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content.transform, i));
			}
			this.m_listRewardData.Add(new MainReward(this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content.transform));
			int eventDropGroupCount = NKMStageTempletV2.EventDropGroupCount;
			for (int j = 0; j < eventDropGroupCount; j++)
			{
				this.m_listRewardData.Add(new EventDropReward(this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content.transform, j));
			}
			this.m_listRewardData.Add(new RandomItemReward(this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content.transform, NKM_REWARD_TYPE.RT_UNIT, 901, new NKCUISlot.OnClick(this.OnClickUnitRewardSlot)));
			this.m_listRewardData.Add(new RandomItemReward(this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content.transform, NKM_REWARD_TYPE.RT_EQUIP, 902, new NKCUISlot.OnClick(this.OnClickEquipRewardSlot)));
			this.m_listRewardData.Add(new RandomItemReward(this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content.transform, NKM_REWARD_TYPE.RT_MOLD, 904, new NKCUISlot.OnClick(this.OnClickMoldRewardSlot)));
			this.m_listRewardData.Add(new RandomItemReward(this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content.transform, NKM_REWARD_TYPE.RT_MISC, 903, new NKCUISlot.OnClick(this.OnClickMiscRewardSlot)));
			this.m_listRewardData.Add(new MiscItemReward(this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content.transform, 501));
			this.m_listRewardData.Add(new MiscItemReward(this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content.transform, 1));
			this.m_listRewardData.Add(new MiscItemReward(this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content.transform, 2));
			this.m_listRewardData.Add(new MiscItemReward(this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content.transform, 3));
		}

		// Token: 0x06005E04 RID: 24068 RVA: 0x001D1270 File Offset: 0x001CF470
		public void ShowRewardListUpdate()
		{
			this.m_fElapsedTime += Time.deltaTime;
			if (this.m_fElapsedTime > 0.08f && this.m_listSlotToShow.Count > 0)
			{
				this.m_fElapsedTime = 0f;
				this.m_listSlotToShow[0].SetActive(true);
				this.m_listSlotToShow[0].PlaySmallToOrgSize();
				this.m_listSlotToShow.RemoveAt(0);
			}
		}

		// Token: 0x06005E05 RID: 24069 RVA: 0x001D12E4 File Offset: 0x001CF4E4
		public bool CreateRewardSlotDataList(NKMUserData cNKMUserData, NKMStageTempletV2 stageTemplet, string strDungeonID)
		{
			if (cNKMUserData == null || stageTemplet == null)
			{
				return false;
			}
			this.m_strDungeonID = strDungeonID;
			this.m_fElapsedTime = 0f;
			List<int> listNRGI = this.GetListNRGI();
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content, true);
			this.ResetRewardSlotList();
			int count = this.m_listRewardData.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_listRewardData[i].CreateSlotData(stageTemplet, cNKMUserData, listNRGI, this.m_listSlotToShow);
			}
			Vector2 anchoredPosition = this.m_rectListContent.anchoredPosition;
			anchoredPosition.x = this.m_ContentOrgPosX;
			this.m_rectListContent.anchoredPosition = anchoredPosition;
			Vector2 sizeDelta = this.m_rectListContent.sizeDelta;
			sizeDelta.x = (float)(this.m_listSlotToShow.Count * 150);
			this.m_rectListContent.sizeDelta = sizeDelta;
			return this.HasAnyReward(stageTemplet, listNRGI);
		}

		// Token: 0x06005E06 RID: 24070 RVA: 0x001D13BC File Offset: 0x001CF5BC
		private List<int> GetListNRGI()
		{
			List<int> list = new List<int>();
			NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(this.m_strDungeonID);
			if (nkmstageTempletV == null)
			{
				return list;
			}
			switch (nkmstageTempletV.m_STAGE_TYPE)
			{
			case STAGE_TYPE.ST_WARFARE:
			{
				NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(this.m_strDungeonID);
				if (nkmwarfareTemplet != null)
				{
					for (int i = 0; i < nkmwarfareTemplet.RewardList.Count; i++)
					{
						list.Add(nkmwarfareTemplet.RewardList[i].m_RewardGroupID);
					}
					NKMWarfareMapTemplet mapTemplet = nkmwarfareTemplet.MapTemplet;
					if (mapTemplet != null)
					{
						foreach (string dungeonStrID in mapTemplet.GetDungeonStrIDList())
						{
							NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(dungeonStrID);
							if (dungeonTempletBase != null)
							{
								for (int j = 0; j < dungeonTempletBase.m_listDungeonReward.Count; j++)
								{
									list.Add(dungeonTempletBase.m_listDungeonReward[j].m_RewardGroupID);
								}
							}
						}
						if (nkmwarfareTemplet.ContainerRewardTemplet != null)
						{
							list.Add(nkmwarfareTemplet.ContainerRewardTemplet.GroupId);
						}
					}
				}
				break;
			}
			case STAGE_TYPE.ST_DUNGEON:
			{
				NKMDungeonTempletBase dungeonTempletBase2 = NKMDungeonManager.GetDungeonTempletBase(this.m_strDungeonID);
				if (dungeonTempletBase2 != null)
				{
					for (int k = 0; k < dungeonTempletBase2.m_listDungeonReward.Count; k++)
					{
						list.Add(dungeonTempletBase2.m_listDungeonReward[k].m_RewardGroupID);
					}
					if (dungeonTempletBase2.m_RewardUnitExp1Tier > 0)
					{
						list.Add(1031);
					}
					if (dungeonTempletBase2.m_RewardUnitExp2Tier > 0)
					{
						list.Add(1032);
					}
					if (dungeonTempletBase2.m_RewardUnitExp3Tier > 0)
					{
						list.Add(1033);
					}
				}
				break;
			}
			case STAGE_TYPE.ST_PHASE:
			{
				NKMPhaseTemplet nkmphaseTemplet = NKMPhaseTemplet.Find(this.m_strDungeonID);
				if (nkmphaseTemplet != null)
				{
					for (int l = 0; l < nkmphaseTemplet.Rewards.Count; l++)
					{
						list.Add(nkmphaseTemplet.Rewards[l].m_RewardGroupID);
					}
					if (nkmphaseTemplet.PhaseList != null)
					{
						foreach (NKMPhaseOrderTemplet nkmphaseOrderTemplet in nkmphaseTemplet.PhaseList.List)
						{
							NKMDungeonTempletBase dungeon = nkmphaseOrderTemplet.Dungeon;
							if (dungeon != null)
							{
								for (int m = 0; m < dungeon.m_listDungeonReward.Count; m++)
								{
									list.Add(dungeon.m_listDungeonReward[m].m_RewardGroupID);
								}
							}
							if (dungeon.m_RewardUnitExp1Tier > 0 && !list.Contains(1031))
							{
								list.Add(1031);
							}
							if (dungeon.m_RewardUnitExp2Tier > 0 && !list.Contains(1032))
							{
								list.Add(1032);
							}
							if (dungeon.m_RewardUnitExp3Tier > 0 && !list.Contains(1033))
							{
								list.Add(1033);
							}
						}
					}
				}
				break;
			}
			}
			return list;
		}

		// Token: 0x06005E07 RID: 24071 RVA: 0x001D16C0 File Offset: 0x001CF8C0
		public static List<int> GetUnitRewardIdList(HashSet<int> hsUnits, HashSet<int> hsOperators)
		{
			List<NKMUnitTempletBase> list = new List<NKMUnitTempletBase>();
			foreach (int unitID in hsUnits)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitID);
				if (unitTempletBase != null)
				{
					list.Add(unitTempletBase);
				}
			}
			foreach (int unitID2 in hsOperators)
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
			return list2;
		}

		// Token: 0x06005E08 RID: 24072 RVA: 0x001D17A8 File Offset: 0x001CF9A8
		public static List<int> GetEquipRewardIdList(HashSet<int> hsEquips)
		{
			List<NKMEquipTemplet> list = new List<NKMEquipTemplet>();
			foreach (int equipID in hsEquips)
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
			return list2;
		}

		// Token: 0x06005E09 RID: 24073 RVA: 0x001D1848 File Offset: 0x001CFA48
		public static List<int> GetMoldRewardIdList(HashSet<int> hsMolds)
		{
			List<NKMItemMoldTemplet> list = new List<NKMItemMoldTemplet>();
			foreach (int moldID in hsMolds)
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
			return list2;
		}

		// Token: 0x06005E0A RID: 24074 RVA: 0x001D18E8 File Offset: 0x001CFAE8
		public static List<int> GetMiscRewardIdList(HashSet<int> hsMisc)
		{
			List<NKMItemMiscTemplet> list = new List<NKMItemMiscTemplet>();
			foreach (int itemMiscID in hsMisc)
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
			return list2;
		}

		// Token: 0x06005E0B RID: 24075 RVA: 0x001D1988 File Offset: 0x001CFB88
		private void OnClickUnitRewardSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			List<int> listNRGI = this.GetListNRGI();
			HashSet<int> rewardIDs = NKCUtil.GetRewardIDs(listNRGI, NKM_REWARD_TYPE.RT_UNIT);
			HashSet<int> rewardIDs2 = NKCUtil.GetRewardIDs(listNRGI, NKM_REWARD_TYPE.RT_OPERATOR);
			List<int> unitRewardIdList = NKCUIComDungeonRewardList.GetUnitRewardIdList(rewardIDs, rewardIDs2);
			NKCUISlotListViewer.Instance.OpenRewardList(unitRewardIdList, NKM_REWARD_TYPE.RT_UNIT, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
		}

		// Token: 0x06005E0C RID: 24076 RVA: 0x001D19CC File Offset: 0x001CFBCC
		private void OnClickEquipRewardSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			List<int> equipRewardIdList = NKCUIComDungeonRewardList.GetEquipRewardIdList(NKCUtil.GetRewardIDs(this.GetListNRGI(), NKM_REWARD_TYPE.RT_EQUIP));
			NKCUISlotListViewer.Instance.OpenRewardList(equipRewardIdList, NKM_REWARD_TYPE.RT_EQUIP, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
		}

		// Token: 0x06005E0D RID: 24077 RVA: 0x001D1A04 File Offset: 0x001CFC04
		private void OnClickMoldRewardSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			List<int> moldRewardIdList = NKCUIComDungeonRewardList.GetMoldRewardIdList(NKCUtil.GetRewardIDs(this.GetListNRGI(), NKM_REWARD_TYPE.RT_MOLD));
			NKCUISlotListViewer.Instance.OpenRewardList(moldRewardIdList, NKM_REWARD_TYPE.RT_MOLD, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
		}

		// Token: 0x06005E0E RID: 24078 RVA: 0x001D1A3C File Offset: 0x001CFC3C
		private void OnClickMiscRewardSlot(NKCUISlot.SlotData slotData, bool bLocked)
		{
			List<int> miscRewardIdList = NKCUIComDungeonRewardList.GetMiscRewardIdList(NKCUtil.GetRewardIDs(this.GetListNRGI(), NKM_REWARD_TYPE.RT_MISC));
			NKCUISlotListViewer.Instance.OpenRewardList(miscRewardIdList, NKM_REWARD_TYPE.RT_MISC, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_TITLE, NKCUtilString.GET_STRING_REWARD_LIST_POPUP_DESC);
		}

		// Token: 0x06005E0F RID: 24079 RVA: 0x001D1A74 File Offset: 0x001CFC74
		private void ResetRewardSlotList()
		{
			int count = this.m_listRewardData.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_listRewardData[i].SetSlotActive(false);
			}
			this.m_listSlotToShow.Clear();
		}

		// Token: 0x06005E10 RID: 24080 RVA: 0x001D1AB8 File Offset: 0x001CFCB8
		private bool HasAnyReward(NKMStageTempletV2 stageTemplet, List<int> lstID)
		{
			return lstID.Count > 0 || (stageTemplet.GetFirstRewardData() != null && stageTemplet.GetFirstRewardData().Type != NKM_REWARD_TYPE.RT_NONE) || stageTemplet.OnetimeRewards.Count > 0 || (stageTemplet.MainRewardData != null && stageTemplet.MainRewardData.rewardType != NKM_REWARD_TYPE.RT_NONE);
		}

		// Token: 0x06005E11 RID: 24081 RVA: 0x001D1B10 File Offset: 0x001CFD10
		private void OnDestroy()
		{
			this.m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content = null;
			this.m_rectListContent = null;
			for (int i = 0; i < this.m_listRewardData.Count; i++)
			{
				this.m_listRewardData[i].Release();
			}
			this.m_listRewardData.Clear();
			this.m_listRewardData = null;
			this.m_listSlotToShow.Clear();
			this.m_listSlotToShow = null;
			this.m_strDungeonID = null;
		}

		// Token: 0x04004A39 RID: 19001
		public GameObject m_NKM_UI_OPERATION_POPUP_DROP_ITEM_Content;

		// Token: 0x04004A3A RID: 19002
		private const int ITEM_SLOT_WIDTH_DIST = 150;

		// Token: 0x04004A3B RID: 19003
		private RectTransform m_rectListContent;

		// Token: 0x04004A3C RID: 19004
		private float m_ContentOrgPosX;

		// Token: 0x04004A3D RID: 19005
		private List<StageRewardData> m_listRewardData = new List<StageRewardData>();

		// Token: 0x04004A3E RID: 19006
		private List<NKCUISlot> m_listSlotToShow = new List<NKCUISlot>();

		// Token: 0x04004A3F RID: 19007
		private string m_strDungeonID;

		// Token: 0x04004A40 RID: 19008
		private float m_fElapsedTime;
	}
}

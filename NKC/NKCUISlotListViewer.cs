using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009ED RID: 2541
	public class NKCUISlotListViewer : NKCUIBase
	{
		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06006DC6 RID: 28102 RVA: 0x0023FF28 File Offset: 0x0023E128
		public static NKCUISlotListViewer Instance
		{
			get
			{
				if (NKCUISlotListViewer.m_Instance == null)
				{
					NKCUISlotListViewer.m_Instance = NKCUIManager.OpenNewInstance<NKCUISlotListViewer>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_ITEM_LIST", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUISlotListViewer.CleanupInstance)).GetInstance<NKCUISlotListViewer>();
					NKCUISlotListViewer.m_Instance.Init();
				}
				return NKCUISlotListViewer.m_Instance;
			}
		}

		// Token: 0x06006DC7 RID: 28103 RVA: 0x0023FF77 File Offset: 0x0023E177
		private static void CleanupInstance()
		{
			NKCUISlotListViewer.m_Instance = null;
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06006DC8 RID: 28104 RVA: 0x0023FF7F File Offset: 0x0023E17F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUISlotListViewer.m_Instance != null && NKCUISlotListViewer.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006DC9 RID: 28105 RVA: 0x0023FF9A File Offset: 0x0023E19A
		public static NKCUISlotListViewer GetNewInstance()
		{
			return NKCUIManager.OpenNewInstance<NKCUISlotListViewer>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_ITEM_LIST", NKCUIManager.eUIBaseRect.UIFrontPopup, null).GetInstance<NKCUISlotListViewer>();
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06006DCA RID: 28106 RVA: 0x0023FFB2 File Offset: 0x0023E1B2
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06006DCB RID: 28107 RVA: 0x0023FFB5 File Offset: 0x0023E1B5
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_SLOT_VIEWR;
			}
		}

		// Token: 0x06006DCC RID: 28108 RVA: 0x0023FFBC File Offset: 0x0023E1BC
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06006DCD RID: 28109 RVA: 0x0023FFCA File Offset: 0x0023E1CA
		private void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnOK, new UnityAction(base.Close));
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm);
			NKCUtil.SetScrollHotKey(this.m_srContents, null);
		}

		// Token: 0x06006DCE RID: 28110 RVA: 0x0023FFFC File Offset: 0x0023E1FC
		private void SetSlotCount(int count)
		{
			while (this.m_lstSlot.Count < count)
			{
				NKCUISlot nkcuislot = UnityEngine.Object.Instantiate<NKCUISlot>(this.m_pfbSlot);
				nkcuislot.Init();
				nkcuislot.transform.SetParent(this.m_trSlotRoot, false);
				this.m_lstSlot.Add(nkcuislot);
			}
		}

		// Token: 0x06006DCF RID: 28111 RVA: 0x0024004C File Offset: 0x0023E24C
		private void SetDeckViewUnitSlotCount(int count)
		{
			while (this.m_lstDeckViewUnitSlot.Count < count)
			{
				NKCDeckViewUnitSlot newInstance = NKCDeckViewUnitSlot.GetNewInstance(this.m_trSlotRoot);
				if (newInstance != null)
				{
					newInstance.Init(this.m_lstDeckViewUnitSlot.Count, false);
					newInstance.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
					this.m_lstDeckViewUnitSlot.Add(newInstance);
				}
			}
		}

		// Token: 0x06006DD0 RID: 28112 RVA: 0x002400C0 File Offset: 0x0023E2C0
		private void TurnOffSlotList()
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstSlot[i], false);
			}
		}

		// Token: 0x06006DD1 RID: 28113 RVA: 0x002400F8 File Offset: 0x0023E2F8
		private void TurnOffDeckViewUnitSlotList()
		{
			for (int i = 0; i < this.m_lstDeckViewUnitSlot.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstDeckViewUnitSlot[i], false);
			}
		}

		// Token: 0x06006DD2 RID: 28114 RVA: 0x00240130 File Offset: 0x0023E330
		public void OpenDeckViewEnemySlotList(string stageBattleStrID, string title, string desc)
		{
			NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(stageBattleStrID);
			if (nkmstageTempletV != null)
			{
				this.OpenDeckViewEnemySlotList(nkmstageTempletV, title, desc);
				return;
			}
			NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(stageBattleStrID);
			if (dungeonTempletBase != null)
			{
				this.OpenDeckViewEnemySlotList(dungeonTempletBase, title, desc);
				return;
			}
		}

		// Token: 0x06006DD3 RID: 28115 RVA: 0x00240168 File Offset: 0x0023E368
		public void OpenDeckViewEnemySlotList(NKMStageTempletV2 stageTemplet, string title, string desc)
		{
			if (stageTemplet == null)
			{
				return;
			}
			Dictionary<string, NKCEnemyData> enemyUnits = NKMDungeonManager.GetEnemyUnits(stageTemplet);
			this.OpenDeckViewEnemySlotList(enemyUnits, title, desc);
		}

		// Token: 0x06006DD4 RID: 28116 RVA: 0x0024018C File Offset: 0x0023E38C
		public void OpenDeckViewEnemySlotList(NKMDungeonTempletBase cNKMDungeonTempletBase, string title, string desc)
		{
			if (cNKMDungeonTempletBase == null)
			{
				return;
			}
			Dictionary<string, NKCEnemyData> enemyUnits = NKMDungeonManager.GetEnemyUnits(cNKMDungeonTempletBase);
			this.OpenDeckViewEnemySlotList(enemyUnits, title, desc);
		}

		// Token: 0x06006DD5 RID: 28117 RVA: 0x002401B0 File Offset: 0x0023E3B0
		public void OpenDeckViewEnemySlotList(Dictionary<string, NKCEnemyData> dicEnemyUnitStrIDs, string title, string desc)
		{
			this.m_NKM_UI_POPUP_ITEM_LIST_TEXT.text = desc;
			this.m_NKM_UI_POPUP_ITEM_LIST_TOP_TEXT.text = title;
			this.m_grid.spacing = new Vector2(this.m_grid.spacing.x, 0f);
			if (dicEnemyUnitStrIDs == null || dicEnemyUnitStrIDs.Count <= 0)
			{
				return;
			}
			this.TurnOffSlotList();
			this.SetDeckViewUnitSlotCount(dicEnemyUnitStrIDs.Count);
			List<NKCEnemyData> list = new List<NKCEnemyData>(dicEnemyUnitStrIDs.Values);
			list.Sort(new NKCEnemyData.CompNED());
			int i;
			for (i = 0; i < list.Count; i++)
			{
				NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.m_lstDeckViewUnitSlot[i];
				nkcdeckViewUnitSlot.SetEnemyData(NKMUnitManager.GetUnitTempletBase(list[i].m_UnitStrID), list[i]);
				NKCUtil.SetGameobjectActive(nkcdeckViewUnitSlot.gameObject, true);
			}
			while (i < this.m_lstDeckViewUnitSlot.Count)
			{
				NKCUtil.SetGameobjectActive(this.m_lstDeckViewUnitSlot[i], false);
				i++;
			}
			NKCUtil.SetGameobjectActive(this.m_objPercentCutInfo, false);
			this.OpenCommon();
		}

		// Token: 0x06006DD6 RID: 28118 RVA: 0x002402B0 File Offset: 0x0023E4B0
		private void OpenCommon()
		{
			this.m_srContents.normalizedPosition = new Vector2(0f, 1f);
			base.UIOpened(true);
			if (this.m_NKCUIOpenAnimator == null)
			{
				this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			}
			this.m_NKCUIOpenAnimator.PlayOpenAni();
		}

		// Token: 0x06006DD7 RID: 28119 RVA: 0x00240302 File Offset: 0x0023E502
		private void Update()
		{
			if (base.IsOpen && this.m_NKCUIOpenAnimator != null)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06006DD8 RID: 28120 RVA: 0x0024031F File Offset: 0x0023E51F
		private void SetSlotClickAction(ref NKCUISlot slot)
		{
			if (slot == null)
			{
				return;
			}
			slot.SetOnClickAction(new NKCUISlot.SlotClickType[]
			{
				NKCUISlot.SlotClickType.RatioList,
				NKCUISlot.SlotClickType.MoldList,
				NKCUISlot.SlotClickType.ChoiceList,
				NKCUISlot.SlotClickType.Tooltip
			});
		}

		// Token: 0x06006DD9 RID: 28121 RVA: 0x00240344 File Offset: 0x0023E544
		public void OpenRewardList(List<int> listID, NKM_REWARD_TYPE type, string title, string desc)
		{
			this.m_NKM_UI_POPUP_ITEM_LIST_TEXT.text = desc;
			this.m_NKM_UI_POPUP_ITEM_LIST_TOP_TEXT.text = title;
			this.m_grid.spacing = new Vector2(this.m_grid.spacing.x, 67f);
			if (listID == null || listID.Count <= 0)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.TurnOffDeckViewUnitSlotList();
			this.SetSlotCount(listID.Count);
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				NKCUISlot nkcuislot = this.m_lstSlot[i];
				if (i < listID.Count)
				{
					NKCUtil.SetGameobjectActive(nkcuislot, true);
					NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(type, listID[i], 1, 0);
					nkcuislot.SetData(data, true, false, true, null);
					this.SetSlotClickAction(ref nkcuislot);
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuislot, false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objPercentCutInfo, false);
			this.OpenCommon();
		}

		// Token: 0x06006DDA RID: 28122 RVA: 0x0024042C File Offset: 0x0023E62C
		public void OpenRewardList(Dictionary<NKM_REWARD_TYPE, List<int>> dicRewardIdList, string title, string desc)
		{
			this.m_NKM_UI_POPUP_ITEM_LIST_TEXT.text = desc;
			this.m_NKM_UI_POPUP_ITEM_LIST_TOP_TEXT.text = title;
			this.m_grid.spacing = new Vector2(this.m_grid.spacing.x, 67f);
			if (dicRewardIdList == null)
			{
				return;
			}
			this.TurnOffDeckViewUnitSlotList();
			int num = 0;
			foreach (KeyValuePair<NKM_REWARD_TYPE, List<int>> keyValuePair in dicRewardIdList)
			{
				if (keyValuePair.Value != null)
				{
					num += keyValuePair.Value.Count;
				}
			}
			this.SetSlotCount(num);
			int num2 = 0;
			foreach (KeyValuePair<NKM_REWARD_TYPE, List<int>> keyValuePair2 in dicRewardIdList)
			{
				if (keyValuePair2.Value != null)
				{
					int count = keyValuePair2.Value.Count;
					int num3 = 0;
					while (num3 < count && this.m_lstSlot.Count > num2)
					{
						NKCUISlot nkcuislot = this.m_lstSlot[num2];
						NKCUtil.SetGameobjectActive(nkcuislot, true);
						NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(keyValuePair2.Key, keyValuePair2.Value[num3], 1, 0);
						nkcuislot.SetData(data, true, false, true, null);
						this.SetSlotClickAction(ref nkcuislot);
						num2++;
						num3++;
					}
				}
			}
			for (int i = num2; i < this.m_lstSlot.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstSlot[i], false);
			}
			NKCUtil.SetGameobjectActive(this.m_objPercentCutInfo, false);
			this.OpenCommon();
		}

		// Token: 0x06006DDB RID: 28123 RVA: 0x002405DC File Offset: 0x0023E7DC
		public void OpenMissionRewardList(List<MissionReward> listReward)
		{
			this.m_NKM_UI_POPUP_ITEM_LIST_TEXT.text = NKCUtilString.GET_STRING_SLOT_VIEWR_DESC;
			this.m_NKM_UI_POPUP_ITEM_LIST_TOP_TEXT.text = NKCUtilString.GET_STRING_SLOT_VIEWR;
			this.m_grid.spacing = new Vector2(this.m_grid.spacing.x, 67f);
			this.TurnOffDeckViewUnitSlotList();
			this.SetSlotCount(listReward.Count);
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				NKCUISlot nkcuislot = this.m_lstSlot[i];
				if (i < listReward.Count)
				{
					NKCUtil.SetGameobjectActive(nkcuislot, true);
					NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(listReward[i].reward_type, listReward[i].reward_id, listReward[i].reward_value, 0);
					nkcuislot.SetData(data, true, true, true, null);
					this.SetSlotClickAction(ref nkcuislot);
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuislot, false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objPercentCutInfo, false);
			this.OpenCommon();
		}

		// Token: 0x06006DDC RID: 28124 RVA: 0x002406CC File Offset: 0x0023E8CC
		public void OpenPackageInfo(int BoxItemID)
		{
			this.m_NKM_UI_POPUP_ITEM_LIST_TEXT.text = NKCUtilString.GET_STRING_SLOT_VIEWR_DESC;
			this.m_NKM_UI_POPUP_ITEM_LIST_TOP_TEXT.text = NKCUtilString.GET_STRING_SLOT_VIEWR;
			this.m_grid.spacing = new Vector2(this.m_grid.spacing.x, 67f);
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(BoxItemID);
			if (itemMiscTempletByID == null)
			{
				Debug.LogError("itemTemplet null! ID : " + BoxItemID.ToString());
				return;
			}
			if (itemMiscTempletByID.m_RewardGroupID == 0)
			{
				Debug.LogError("no rewardgroup! ID : " + BoxItemID.ToString());
				return;
			}
			List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(itemMiscTempletByID.m_RewardGroupID);
			if (randomBoxItemTempletList == null)
			{
				Debug.LogError("rewardgroup null! ID : " + itemMiscTempletByID.m_RewardGroupID.ToString());
				return;
			}
			this.TurnOffDeckViewUnitSlotList();
			this.SetSlotCount(randomBoxItemTempletList.Count);
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				NKCUISlot nkcuislot = this.m_lstSlot[i];
				if (i < randomBoxItemTempletList.Count)
				{
					NKCUtil.SetGameobjectActive(nkcuislot, true);
					NKMRandomBoxItemTemplet nkmrandomBoxItemTemplet = randomBoxItemTempletList[i];
					NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(nkmrandomBoxItemTemplet.m_reward_type, nkmrandomBoxItemTemplet.m_RewardID, nkmrandomBoxItemTemplet.TotalQuantity_Max, 0);
					nkcuislot.SetData(data, true, NKCUISlot.WillShowCount(data), true, null);
					this.SetSlotClickAction(ref nkcuislot);
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuislot, false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objPercentCutInfo, false);
			this.OpenCommon();
		}

		// Token: 0x06006DDD RID: 28125 RVA: 0x00240828 File Offset: 0x0023EA28
		public void OpenItemBoxRatio(int BoxItemID)
		{
			this.m_NKM_UI_POPUP_ITEM_LIST_TEXT.text = NKCStringTable.GetString("SI_DP_SLOT_RATIO_VIEWER_DESC", false);
			this.m_NKM_UI_POPUP_ITEM_LIST_TOP_TEXT.text = NKCUtilString.GET_STRING_SLOT_VIEWR;
			this.m_grid.spacing = new Vector2(this.m_grid.spacing.x, 67f);
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(BoxItemID);
			if (itemMiscTempletByID == null)
			{
				Debug.LogError("itemTemplet null! ID : " + BoxItemID.ToString());
				return;
			}
			if (!itemMiscTempletByID.IsRatioOpened())
			{
				Debug.LogError("Ratio probihited! : ID : " + BoxItemID.ToString());
				return;
			}
			if (itemMiscTempletByID.m_RewardGroupID == 0)
			{
				Debug.LogError("no rewardgroup! ID : " + BoxItemID.ToString());
				return;
			}
			List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(itemMiscTempletByID.m_RewardGroupID);
			if (randomBoxItemTempletList == null)
			{
				Debug.LogError("rewardgroup null! ID : " + itemMiscTempletByID.m_RewardGroupID.ToString());
				return;
			}
			int totalRatio = 0;
			randomBoxItemTempletList.ForEach(delegate(NKMRandomBoxItemTemplet x)
			{
				totalRatio += x.m_Ratio;
			});
			this.TurnOffDeckViewUnitSlotList();
			this.SetSlotCount(randomBoxItemTempletList.Count);
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				NKCUISlot nkcuislot = this.m_lstSlot[i];
				if (i < randomBoxItemTempletList.Count)
				{
					NKCUtil.SetGameobjectActive(nkcuislot, true);
					NKMRandomBoxItemTemplet nkmrandomBoxItemTemplet = randomBoxItemTempletList[i];
					NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(nkmrandomBoxItemTemplet.m_reward_type, nkmrandomBoxItemTemplet.m_RewardID, nkmrandomBoxItemTemplet.TotalQuantity_Max, 0);
					nkcuislot.SetData(data, true, NKCUISlot.WillShowCount(data), true, null);
					float probability = (float)nkmrandomBoxItemTemplet.m_Ratio / (float)totalRatio;
					nkcuislot.AddProbabilityToName(probability);
					nkcuislot.SetCountRange((long)nkmrandomBoxItemTemplet.TotalQuantity_Min, (long)nkmrandomBoxItemTemplet.TotalQuantity_Max);
					this.SetSlotClickAction(ref nkcuislot);
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuislot, false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objPercentCutInfo, true);
			this.OpenCommon();
		}

		// Token: 0x06006DDE RID: 28126 RVA: 0x00240A04 File Offset: 0x0023EC04
		public void OpenChoiceInfo(int ChoiceItemID)
		{
			this.m_NKM_UI_POPUP_ITEM_LIST_TEXT.text = NKCStringTable.GetString("SI_DP_SLOT_CHOICE_VIEWER_DESC", false);
			this.m_NKM_UI_POPUP_ITEM_LIST_TOP_TEXT.text = NKCUtilString.GET_STRING_SLOT_VIEWR;
			this.m_grid.spacing = new Vector2(this.m_grid.spacing.x, 67f);
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(ChoiceItemID);
			if (itemMiscTempletByID == null)
			{
				Debug.LogError("itemTemplet null! ID : " + ChoiceItemID.ToString());
				return;
			}
			if (itemMiscTempletByID.m_RewardGroupID == 0)
			{
				Debug.LogError("no rewardgroup! ID : " + ChoiceItemID.ToString());
				return;
			}
			List<NKMRandomBoxItemTemplet> randomBoxItemTempletList = NKCRandomBoxManager.GetRandomBoxItemTempletList(itemMiscTempletByID.m_RewardGroupID);
			if (randomBoxItemTempletList == null)
			{
				Debug.LogError("rewardgroup null! ID : " + itemMiscTempletByID.m_RewardGroupID.ToString());
				return;
			}
			this.TurnOffDeckViewUnitSlotList();
			this.SetSlotCount(randomBoxItemTempletList.Count);
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				NKCUISlot nkcuislot = this.m_lstSlot[i];
				if (i < randomBoxItemTempletList.Count)
				{
					NKCUtil.SetGameobjectActive(nkcuislot, true);
					NKMRandomBoxItemTemplet nkmrandomBoxItemTemplet = randomBoxItemTempletList[i];
					NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(nkmrandomBoxItemTemplet.m_reward_type, nkmrandomBoxItemTemplet.m_RewardID, nkmrandomBoxItemTemplet.TotalQuantity_Max, 0);
					nkcuislot.SetData(data, true, NKCUISlot.WillShowCount(data), true, null);
					this.SetSlotClickAction(ref nkcuislot);
				}
				else
				{
					NKCUtil.SetGameobjectActive(nkcuislot, false);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objPercentCutInfo, false);
			this.OpenCommon();
		}

		// Token: 0x06006DDF RID: 28127 RVA: 0x00240B68 File Offset: 0x0023ED68
		private void OnDestroy()
		{
			for (int i = 0; i < this.m_lstDeckViewUnitSlot.Count; i++)
			{
				this.m_lstDeckViewUnitSlot[i].CloseInstance();
			}
			this.m_lstDeckViewUnitSlot.Clear();
			NKCUISlotListViewer.m_Instance = null;
		}

		// Token: 0x04005959 RID: 22873
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x0400595A RID: 22874
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_ITEM_LIST";

		// Token: 0x0400595B RID: 22875
		private const float ENEMY_VIEWER_SPACING_Y = 0f;

		// Token: 0x0400595C RID: 22876
		private const float COMMON_VIEWER_SPACING_Y = 67f;

		// Token: 0x0400595D RID: 22877
		private static NKCUISlotListViewer m_Instance;

		// Token: 0x0400595E RID: 22878
		public NKCUISlot m_pfbSlot;

		// Token: 0x0400595F RID: 22879
		public ScrollRect m_srContents;

		// Token: 0x04005960 RID: 22880
		public Transform m_trSlotRoot;

		// Token: 0x04005961 RID: 22881
		public Text m_NKM_UI_POPUP_ITEM_LIST_TEXT;

		// Token: 0x04005962 RID: 22882
		public Text m_NKM_UI_POPUP_ITEM_LIST_TOP_TEXT;

		// Token: 0x04005963 RID: 22883
		public GameObject m_objPercentCutInfo;

		// Token: 0x04005964 RID: 22884
		public NKCUIComButton m_csbtnOK;

		// Token: 0x04005965 RID: 22885
		public GridLayoutGroup m_grid;

		// Token: 0x04005966 RID: 22886
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04005967 RID: 22887
		private List<NKCUISlot> m_lstSlot = new List<NKCUISlot>();

		// Token: 0x04005968 RID: 22888
		private List<NKCDeckViewUnitSlot> m_lstDeckViewUnitSlot = new List<NKCDeckViewUnitSlot>();
	}
}

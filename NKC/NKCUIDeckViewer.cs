using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ClientPacket.Common;
using ClientPacket.Community;
using ClientPacket.Raid;
using ClientPacket.User;
using ClientPacket.WorldMap;
using Cs.Logging;
using Cs.Protocol;
using NKC.PacketHandler;
using NKC.UI.Guide;
using NKC.UI.Guild;
using NKC.UI.Trim;
using NKM;
using NKM.Guild;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000980 RID: 2432
	public class NKCUIDeckViewer : NKCUIBase
	{
		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06006309 RID: 25353 RVA: 0x001F26CC File Offset: 0x001F08CC
		public static NKCUIDeckViewer Instance
		{
			get
			{
				if (NKCUIDeckViewer.m_Instance == null)
				{
					NKCUIDeckViewer.m_Instance = NKCUIManager.OpenNewInstance<NKCUIDeckViewer>("ab_ui_nkm_ui_deck_view", "NKM_UI_DECK_VIEW", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIDeckViewer.CleanupInstance)).GetInstance<NKCUIDeckViewer>();
					NKCUIDeckViewer.m_Instance.InitUI();
				}
				return NKCUIDeckViewer.m_Instance;
			}
		}

		// Token: 0x0600630A RID: 25354 RVA: 0x001F271B File Offset: 0x001F091B
		private static void CleanupInstance()
		{
			NKCUIDeckViewer.m_Instance = null;
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x0600630B RID: 25355 RVA: 0x001F2723 File Offset: 0x001F0923
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIDeckViewer.m_Instance != null && NKCUIDeckViewer.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600630C RID: 25356 RVA: 0x001F273E File Offset: 0x001F093E
		public static void CheckInstanceAndClose()
		{
			if (NKCUIDeckViewer.m_Instance != null && NKCUIDeckViewer.m_Instance.IsOpen)
			{
				NKCUIDeckViewer.m_Instance.Close();
			}
		}

		// Token: 0x0600630D RID: 25357 RVA: 0x001F2763 File Offset: 0x001F0963
		public static bool CheckInstance()
		{
			return NKCUIDeckViewer.m_Instance != null;
		}

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x0600630E RID: 25358 RVA: 0x001F2770 File Offset: 0x001F0970
		public override string GuideTempletID
		{
			get
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_TEAM)
				{
					return "ARTICLE_SYSTEM_TEAM_SETTING";
				}
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_WORLDMAP)
				{
					return "ARTICLE_WORLDMAP_MISSION";
				}
				return "";
			}
		}

		// Token: 0x0600630F RID: 25359 RVA: 0x001F279E File Offset: 0x001F099E
		public bool IsPVPMode(NKCUIDeckViewer.DeckViewerMode eDeckViewerMode)
		{
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_ASYNC_NEW_MODE))
			{
				return NKCUIDeckViewer.IsPVPSyncMode(eDeckViewerMode) || this.IsAsyncPvP();
			}
			return NKCUIDeckViewer.IsPVPSyncMode(eDeckViewerMode);
		}

		// Token: 0x06006310 RID: 25360 RVA: 0x001F27C0 File Offset: 0x001F09C0
		public static bool IsPVPSyncMode(NKCUIDeckViewer.DeckViewerMode eDeckViewerMode)
		{
			return eDeckViewerMode == NKCUIDeckViewer.DeckViewerMode.PvPBattleFindTarget || eDeckViewerMode == NKCUIDeckViewer.DeckViewerMode.PrivatePvPReady;
		}

		// Token: 0x06006311 RID: 25361 RVA: 0x001F27CE File Offset: 0x001F09CE
		public static bool IsDungeonAtkReadyScen(NKCUIDeckViewer.DeckViewerMode eDeckViewerMode)
		{
			return eDeckViewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareDungeonBattle || eDeckViewerMode - NKCUIDeckViewer.DeckViewerMode.PrepareDungeonBattle_Daily <= 2;
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06006312 RID: 25362 RVA: 0x001F27DE File Offset: 0x001F09DE
		public override string MenuName
		{
			get
			{
				return this.m_ViewerOptions.MenuName;
			}
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x06006313 RID: 25363 RVA: 0x001F27EB File Offset: 0x001F09EB
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06006314 RID: 25364 RVA: 0x001F27EE File Offset: 0x001F09EE
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.ON_PLAY_GAME;
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06006315 RID: 25365 RVA: 0x001F27F1 File Offset: 0x001F09F1
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				if (this.m_NKCDeckViewUnitSelectList.IsOpen)
				{
					return NKCUIUpsideMenu.eMode.LeftsideOnly;
				}
				if (this.m_NKCDeckViewSupportList.IsOpen)
				{
					return NKCUIUpsideMenu.eMode.LeftsideOnly;
				}
				if (this.m_ViewerOptions.bUpsideMenuHomeButton)
				{
					return NKCUIUpsideMenu.eMode.Normal;
				}
				return base.eUpsideMenuMode;
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06006316 RID: 25366 RVA: 0x001F2826 File Offset: 0x001F0A26
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				if (this.m_ViewerOptions.upsideMenuShowResourceList != null)
				{
					return this.m_ViewerOptions.upsideMenuShowResourceList;
				}
				return base.UpsideMenuShowResourceList;
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06006317 RID: 25367 RVA: 0x001F2847 File Offset: 0x001F0A47
		private NKMArmyData NKMArmyData
		{
			get
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData == null)
				{
					return null;
				}
				return nkmuserData.m_ArmyData;
			}
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x001F2859 File Offset: 0x001F0A59
		public NKCUIDeckViewer.DeckViewerMode GetDeckViewerMode()
		{
			return this.m_ViewerOptions.eDeckviewerMode;
		}

		// Token: 0x06006319 RID: 25369 RVA: 0x001F2866 File Offset: 0x001F0A66
		public bool GetUnitViewEnable()
		{
			return this.m_bUnitViewEnable;
		}

		// Token: 0x0600631A RID: 25370 RVA: 0x001F286E File Offset: 0x001F0A6E
		public NKMDeckIndex GetSelectDeckIndex()
		{
			return this.m_SelectDeckIndex;
		}

		// Token: 0x0600631B RID: 25371 RVA: 0x001F2876 File Offset: 0x001F0A76
		public int GetSelectUnitSlotIndex()
		{
			return this.m_SelectUnitSlotIndex;
		}

		// Token: 0x0600631C RID: 25372 RVA: 0x001F287E File Offset: 0x001F0A7E
		public NKCDeckViewUnit GetCurrDeckViewUnit()
		{
			if (this.IsUnitSlotExtention())
			{
				return this.GetDeckViewUnit24();
			}
			return this.m_NKCDeckViewUnit;
		}

		// Token: 0x0600631D RID: 25373 RVA: 0x001F2898 File Offset: 0x001F0A98
		public void InitUI()
		{
			this.m_NKCDeckViewList.Init(new NKCDeckViewList.OnSelectDeck(this.SelectDeck), new NKCDeckViewList.OnSelectDeck(this.DeckUnlockRequestPopup), new NKCDeckViewList.OnChangedMuiltiSelectedDeckCount(this.OnChangedMultiSelectedCount), new NKCDeckViewList.OnSupportList(this.OpenSupList));
			this.m_NKCDeckViewShip.Init(new NKCDeckViewShip.OnShipClicked(this.DeckViewShipClick));
			this.m_NKCDeckViewUnit.Init(new NKCDeckViewUnit.OnClickUnit(this.OnUnitClicked), new NKCDeckViewUnit.OnDragUnitEnd(this.OnUnitDragEnd));
			this.m_NKCDeckViewOperator.Init();
			this.m_NKCDeckViewSide.Init(new NKCDeckViewSideUnitIllust.OnUnitInfoClick(this.DeckViewUnitInfoClick), new NKCDeckViewSideUnitIllust.OnUnitChangeClick(this.OpenDeckSelectList), new NKCDeckViewSideUnitIllust.OnLeaderChange(this.OnLeaderChange), new NKCDeckViewSide.OnConfirm(this.OnSideMenuButtonConfirm), new NKCDeckViewSide.OnClickCloseBtn(this.OnClickCloseBtnOfDeckViewSide), new NKCDeckViewSide.CheckMultiply(this.CheckOperationMultiply));
			this.m_NKCDeckViewUnitSelectList.Init(this, new NKCDeckViewUnitSelectList.OnDeckUnitChangeClicked(this.OnDeckUnitChangeClicked), new NKCDeckViewUnitSelectList.OnClose(this.OnUnitSelectListClose), new UnityAction(this.ClearDeck), new UnityAction(this.AutoCompleteDeck));
			this.m_NKCDeckViewUnitSelectList.transform.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup));
			this.m_NKCDeckViewSupportList.Init(this, new NKCDeckViewSupportList.OnSelectSlot(this.UpdateSupporterUI), new NKCDeckViewSupportList.OnConfirmBtn(this.OnConfirmSuppoter));
			this.m_NKCDeckViewSupportList.transform.SetParent(NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup));
			this.m_NKM_DECK_VIEW_ARMY_NKCUIComSafeArea = base.transform.Find("NKM_DECK_VIEW_ARMY").GetComponent<NKCUIComSafeArea>();
			if (null != this.m_opereaterEmpty)
			{
				this.m_opereaterEmpty.PointerClick.RemoveAllListeners();
				this.m_opereaterEmpty.PointerClick.AddListener(new UnityAction(this.OnClickOperatorEmptySlot));
			}
			if (null != this.m_NKM_UI_OPERATOR_DECK_SLOT)
			{
				this.m_NKM_UI_OPERATOR_DECK_SLOT.Init(new NKCUIOperatorDeckSlot.OnSelectOperator(this.OnSelectOperator));
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_DECK_VIEW_OPERATOR.gameObject, !NKCOperatorUtil.IsHide());
			NKCUtil.SetButtonClickDelegate(this.m_csbtnEnemyList, new UnityAction(this.OnBtnEnemyList));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnDeckTypeGuide, new UnityAction(this.OnBtnDeckTypeGuide));
			if (this.m_ifDeckName != null)
			{
				this.m_ifDeckName.onValidateInput = new InputField.OnValidateInput(NKCFilterManager.FilterEmojiInput);
				this.m_ifDeckName.onValueChanged.RemoveAllListeners();
				this.m_ifDeckName.onValueChanged.AddListener(new UnityAction<string>(this.OnDeckNameValueChanged));
				this.m_ifDeckName.onEndEdit.RemoveAllListeners();
				this.m_ifDeckName.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEditDeckName));
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnDeckName, new UnityAction(this.OnChangeDeckName));
			base.gameObject.SetActive(false);
		}

		// Token: 0x0600631E RID: 25374 RVA: 0x001F2B64 File Offset: 0x001F0D64
		public void CloseResource()
		{
		}

		// Token: 0x0600631F RID: 25375 RVA: 0x001F2B66 File Offset: 0x001F0D66
		public override void OnScreenResolutionChanged()
		{
			base.OnScreenResolutionChanged();
			base.StartCoroutine(this.DelayedSelectCurrentDeck());
		}

		// Token: 0x06006320 RID: 25376 RVA: 0x001F2B7B File Offset: 0x001F0D7B
		private IEnumerator DelayedSelectCurrentDeck()
		{
			yield return null;
			this.SelectCurrentDeck();
			yield break;
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x001F2B8A File Offset: 0x001F0D8A
		private void OnChangedMultiSelectedCount(int count)
		{
			this.m_NKCDeckViewSide.SetMultiSelectedCount(count, this.m_ViewerOptions.maxMultiSelectCount);
		}

		// Token: 0x06006322 RID: 25378 RVA: 0x001F2BA3 File Offset: 0x001F0DA3
		public void Load(NKMArmyData cNKMArmyData)
		{
		}

		// Token: 0x06006323 RID: 25379 RVA: 0x001F2BA5 File Offset: 0x001F0DA5
		private void LoadIcon(NKMUnitTempletBase cNKMUnitTempletBase)
		{
			NKCResourceUtility.PreloadUnitResource(NKCResourceUtility.eUnitResourceType.FACE_CARD, cNKMUnitTempletBase, true);
			if (cNKMUnitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
			{
				NKCResourceUtility.PreloadUnitResource(NKCResourceUtility.eUnitResourceType.INVEN_ICON, cNKMUnitTempletBase, true);
			}
		}

		// Token: 0x06006324 RID: 25380 RVA: 0x001F2BC0 File Offset: 0x001F0DC0
		private void LoadSkillIcon(NKMUnitTempletBase cNKMUnitTempletBase)
		{
			if (cNKMUnitTempletBase != null)
			{
				for (int i = 0; i < cNKMUnitTempletBase.GetSkillCount(); i++)
				{
					NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(cNKMUnitTempletBase, i);
					if (shipSkillTempletByIndex != null)
					{
						NKCResourceUtility.LoadAssetResourceTemp<Sprite>("AB_UI_SHIP_SKILL_ICON", shipSkillTempletByIndex.m_ShipSkillIcon, true);
					}
					else
					{
						Debug.LogError(string.Format("ERROR - {0}", cNKMUnitTempletBase.GetSkillStrID(i)));
					}
				}
				return;
			}
			Debug.Log("NKCUIDeckViewer::LoadSkillIcon - cNKMUnitTempletBase is Null");
		}

		// Token: 0x06006325 RID: 25381 RVA: 0x001F2C20 File Offset: 0x001F0E20
		private void LoadSpineIllust(NKMUnitTempletBase cNKMUnitTempletBase)
		{
			NKCResourceUtility.PreloadUnitResource(NKCResourceUtility.eUnitResourceType.SPINE_ILLUST, cNKMUnitTempletBase, true);
		}

		// Token: 0x06006326 RID: 25382 RVA: 0x001F2C2A File Offset: 0x001F0E2A
		public void LoadComplete()
		{
		}

		// Token: 0x06006327 RID: 25383 RVA: 0x001F2C2C File Offset: 0x001F0E2C
		public void Init()
		{
			this.m_SelectDeckIndex = this.m_ViewerOptions.DeckIndex;
			this.EnableUnitView(true, false);
			this.m_SelectUnitSlotIndex = -1;
			if (null != this.m_csbtn_NKM_DECK_VIEW_HELP)
			{
				this.m_csbtn_NKM_DECK_VIEW_HELP.PointerClick.RemoveAllListeners();
				this.m_csbtn_NKM_DECK_VIEW_HELP.PointerClick.AddListener(new UnityAction(this.OpenPopupUnitInfo));
			}
		}

		// Token: 0x06006328 RID: 25384 RVA: 0x001F2C93 File Offset: 0x001F0E93
		public void OpenPopupUnitInfo()
		{
			NKCPopupUnitRoleInfo.Instance.OpenDefaultPopup();
		}

		// Token: 0x06006329 RID: 25385 RVA: 0x001F2C9F File Offset: 0x001F0E9F
		private bool IsMultiSelect()
		{
			return this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.DeckMultiSelect || this.m_ViewerOptions.dOnDeckSideButtonConfirmForMulti != null;
		}

		// Token: 0x0600632A RID: 25386 RVA: 0x001F2CC4 File Offset: 0x001F0EC4
		private NKCDeckViewUnit GetDeckViewUnit24()
		{
			if (this.m_NKCDeckViewUnit_24 == null)
			{
				this.m_NKCDeckViewUnit_24 = NKCDeckViewUnit.OpenInstance("AB_UI_NKM_UI_DECK_VIEW", "NKM_UI_DECK_VIEW_UNIT_24", this.m_objDeckViewArmy.transform, new NKCDeckViewUnit.OnClickUnit(this.OnUnitClicked), new NKCDeckViewUnit.OnDragUnitEnd(this.OnUnitDragEnd));
			}
			return this.m_NKCDeckViewUnit_24;
		}

		// Token: 0x0600632B RID: 25387 RVA: 0x001F2D20 File Offset: 0x001F0F20
		private void SetDeckViewUnitUI()
		{
			if (this.IsUnitSlotExtention())
			{
				NKCUtil.SetGameobjectActive(this.GetDeckViewUnit24(), true);
				NKCDeckViewUnit deckViewUnit = this.GetDeckViewUnit24();
				if (deckViewUnit != null)
				{
					deckViewUnit.Open(this.NKMArmyData, this.m_SelectDeckIndex, this.m_ViewerOptions);
				}
				this.m_NKCDeckViewUnit.Close();
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.GetDeckViewUnit24(), false);
				this.m_NKCDeckViewUnit.Open(this.NKMArmyData, this.m_SelectDeckIndex, this.m_ViewerOptions);
			}
			NKCDeckViewUnit currDeckViewUnit = this.GetCurrDeckViewUnit();
			if (currDeckViewUnit != null)
			{
				currDeckViewUnit.Enable();
			}
			this.m_NKCDeckViewOperator.Enable();
		}

		// Token: 0x0600632C RID: 25388 RVA: 0x001F2DB8 File Offset: 0x001F0FB8
		private void SetRightSideView(bool bInit = true)
		{
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareRaid)
			{
				NKCUtil.SetGameobjectActive(this.m_NKCUIGuildCoopRaidRightSide, false);
				if (this.m_NKCUIRaidRightSide == null)
				{
					this.m_NKCUIRaidRightSide = NKCUIRaidRightSide.OpenInstance(this.m_objDeckViewSideRaidParent.transform, new NKCUIRaidRightSide.onClickAttackBtn(this.OnClickRaidAttck));
				}
				NKMRaidDetailData nkmraidDetailData = NKCScenManager.GetScenManager().GetNKCRaidDataMgr().Find(this.m_ViewerOptions.raidUID);
				if (nkmraidDetailData == null)
				{
					Debug.LogError("raidData is null");
					return;
				}
				NKCUIRaidRightSide.NKC_RAID_SUB_BUTTON_TYPE eNKC_RAID_SUB_BUTTON_TYPE = NKCUIRaidRightSide.NKC_RAID_SUB_BUTTON_TYPE.NRSBT_ATTACK;
				if (nkmraidDetailData.curHP == 0f || NKCSynchronizedTime.IsFinished(nkmraidDetailData.expireDate))
				{
					eNKC_RAID_SUB_BUTTON_TYPE = NKCUIRaidRightSide.NKC_RAID_SUB_BUTTON_TYPE.NRSBT_EXIT;
				}
				NKCUtil.SetGameobjectActive(this.m_NKCUIRaidRightSide, true);
				NKCUIRaidRightSide nkcuiraidRightSide = this.m_NKCUIRaidRightSide;
				if (nkcuiraidRightSide != null)
				{
					nkcuiraidRightSide.SetUI(this.m_ViewerOptions.raidUID, NKCUIRaidRightSide.NKC_RAID_SUB_MENU_TYPE.NRSMT_SUPPORT_EQUIP, eNKC_RAID_SUB_BUTTON_TYPE);
				}
				this.m_NKCDeckViewSide.Open(this.m_ViewerOptions, bInit, this.CheckUseCost());
				this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetShipOnlyMode(false);
			}
			else if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.GuildCoopBoss)
			{
				NKCUtil.SetGameobjectActive(this.m_NKCUIRaidRightSide, false);
				if (this.m_NKCUIGuildCoopRaidRightSide == null)
				{
					this.m_NKCUIGuildCoopRaidRightSide = NKCUIGuildCoopRaidRightSide.OpenInstance(this.m_objDeckViewSideRaidParent.transform, new NKCUIGuildCoopRaidRightSide.onClickAttackBtn(this.OnClickRaidAttck));
				}
				NKCUtil.SetGameobjectActive(this.m_NKCUIGuildCoopRaidRightSide, true);
				GuildRaidTemplet cGuildRaidTemplet = NKCGuildCoopManager.m_cGuildRaidTemplet;
				NKCUIGuildCoopRaidRightSide nkcuiguildCoopRaidRightSide = this.m_NKCUIGuildCoopRaidRightSide;
				if (nkcuiguildCoopRaidRightSide != null)
				{
					nkcuiguildCoopRaidRightSide.SetUI(cGuildRaidTemplet.GetSeasonRaidGrouop(), cGuildRaidTemplet.GetStageId());
				}
				this.m_NKCDeckViewSide.Open(this.m_ViewerOptions, bInit, this.CheckUseCost());
				this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetShipOnlyMode(false);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKCUIRaidRightSide, false);
				NKCUtil.SetGameobjectActive(this.m_NKCUIGuildCoopRaidRightSide, false);
				this.m_NKCDeckViewSide.Open(this.m_ViewerOptions, bInit, this.CheckUseCost());
			}
			this.m_NKCDeckViewSide.SetUnitData(null, false);
			this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().ResetObj();
			this.UpdateEnterLimitCount();
		}

		// Token: 0x0600632D RID: 25389 RVA: 0x001F2FB0 File Offset: 0x001F11B0
		private bool CheckUseCost()
		{
			NKCUIDeckViewer.DeckViewerMode eDeckviewerMode = this.m_ViewerOptions.eDeckviewerMode;
			if (eDeckviewerMode - NKCUIDeckViewer.DeckViewerMode.WarfareBatch <= 1)
			{
				return this.m_ViewerOptions.CostItemID == 2;
			}
			return this.m_ViewerOptions.CostItemID > 0;
		}

		// Token: 0x0600632E RID: 25390 RVA: 0x001F2FEC File Offset: 0x001F11EC
		private bool IsUnitSlotExtention()
		{
			return this.m_ViewerOptions.bSlot24Extend;
		}

		// Token: 0x0600632F RID: 25391 RVA: 0x001F3000 File Offset: 0x001F1200
		public void Open(NKCUIDeckViewer.DeckViewerOption options, bool bInit = true)
		{
			this.m_ViewerOptions = options;
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
				this.m_NKM_DECK_VIEW_ARMY_NKCUIComSafeArea.SetSafeAreaBase();
			}
			if (bInit)
			{
				this.Init();
			}
			this.CloseDeckSelectList(false);
			if (this.m_ViewerOptions.bUseAsyncDeckSetting)
			{
				this.m_AsyncOriginalDeckData.DeepCopyFrom(this.NKMArmyData.GetDeckData(this.m_SelectDeckIndex));
			}
			this.SetDeckViewUnitUI();
			NKCUtil.SetGameobjectActive(this.m_objDeckViewSquadTitle, this.m_ViewerOptions.eDeckviewerMode != NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck);
			NKCUtil.SetGameobjectActive(this.m_objDescOrder, this.IsAsyncPvP());
			this.m_NKCDeckViewList.Open(this.IsMultiSelect(), this.NKMArmyData, this.m_SelectDeckIndex.m_eDeckType, (int)this.m_SelectDeckIndex.m_iIndex, this.m_ViewerOptions);
			this.UpdateDeckToggleUI();
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				long shipUId = NKCLocalDeckDataManager.GetShipUId((int)this.m_SelectDeckIndex.m_iIndex);
				NKMUnitData shipFromUID = this.NKMArmyData.GetShipFromUID(shipUId);
				this.m_NKCDeckViewShip.Open(shipFromUID, NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode));
			}
			else
			{
				this.m_NKCDeckViewShip.Open(this.NKMArmyData.GetDeckShip(this.m_SelectDeckIndex), NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode));
			}
			if (this.IsUnitSlotExtention())
			{
				this.m_NKCDeckViewShip.transform.localPosition = this.m_vShipRaidAnchoredPos;
				this.m_NKM_DECK_VIEW_OPERATOR.anchoredPosition = this.m_vOperatorRaidAnchoredPos;
			}
			else
			{
				this.m_NKCDeckViewShip.transform.localPosition = this.m_vShipNormalAnchoredPos;
				this.m_NKM_DECK_VIEW_OPERATOR.anchoredPosition = this.m_vOperatorNormalAnchoredPos;
			}
			this.UpdateOperator(this.NKMArmyData.GetDeckOperator(this.m_SelectDeckIndex));
			this.m_NKCDeckViewShip.Disable();
			this.SetRightSideView(true);
			if (this.IsMultiSelect())
			{
				this.OnChangedMultiSelectedCount(this.m_NKCDeckViewList.GetMultiSelectedCount());
			}
			if (this.m_rtDeckOperationPowerRoot != null)
			{
				if (!this.IsUnitSlotExtention())
				{
					this.m_rtDeckOperationPowerRoot.anchoredPosition = this.m_vDeckPowerNormalAnchoredPos;
				}
				else
				{
					this.m_rtDeckOperationPowerRoot.anchoredPosition = this.m_vDeckPowerRaidAnchoredPos;
				}
			}
			this.UpdateDeckOperationPower();
			if (!bInit && this.m_bUnitViewEnable)
			{
				this.SelectDeckViewUnit(this.m_SelectUnitSlotIndex, true, false);
			}
			this.SelectDeck(this.m_ViewerOptions.DeckIndex);
			if (this.m_ViewerOptions.SelectLeaderUnitOnOpen)
			{
				if (this.m_SelectUnitSlotIndex == -1 || bInit)
				{
					NKMDeckData deckData = this.NKMArmyData.GetDeckData(this.m_SelectDeckIndex);
					if (deckData != null)
					{
						this.SelectDeckViewUnit((int)deckData.m_LeaderIndex, false, false);
					}
					NKCDeckViewUnit currDeckViewUnit = this.GetCurrDeckViewUnit();
					if (currDeckViewUnit != null)
					{
						currDeckViewUnit.SelectDeckViewUnit(this.m_SelectUnitSlotIndex, false);
					}
				}
				else
				{
					this.SelectDeckViewUnit(this.m_SelectUnitSlotIndex, false, false);
					NKCDeckViewUnit currDeckViewUnit2 = this.GetCurrDeckViewUnit();
					if (currDeckViewUnit2 != null)
					{
						currDeckViewUnit2.SelectDeckViewUnit(this.m_SelectUnitSlotIndex, false);
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_DECK_VIEW_BG, this.m_ViewerOptions.bEnableDefaultBackground);
			if (this.m_ViewerOptions.bUpsideMenuHomeButton)
			{
				NKCUIFadeInOut.FadeIn(0.1f, null, false);
			}
			this.SetBotUI();
			base.UIOpened(true);
			if (this.m_ViewerOptions.bOpenAlphaAni)
			{
				this.m_CanvasGroup.alpha = 0f;
			}
			else
			{
				this.m_CanvasGroup.alpha = 1f;
			}
			this.CheckTutorial();
		}

		// Token: 0x06006330 RID: 25392 RVA: 0x001F3350 File Offset: 0x001F1550
		public void UpdateEnterLimitUI()
		{
			this.m_NKCDeckViewSide.Open(this.m_ViewerOptions, false, this.CheckUseCost());
			this.UpdateEnterLimitCount();
		}

		// Token: 0x06006331 RID: 25393 RVA: 0x001F3370 File Offset: 0x001F1570
		public void UpdateEnterLimitCount()
		{
			bool bValue = false;
			if (!string.IsNullOrEmpty(this.m_ViewerOptions.StageBattleStrID))
			{
				NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(this.m_ViewerOptions.StageBattleStrID);
				if (nkmstageTempletV != null && nkmstageTempletV.EnterLimit > 0)
				{
					bValue = true;
					int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(nkmstageTempletV.Key, false, false, false);
					string msg;
					switch (nkmstageTempletV.EnterLimitCond)
					{
					case SHOP_RESET_TYPE.DAY:
						msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_DAY_02, nkmstageTempletV.EnterLimit - statePlayCnt, nkmstageTempletV.EnterLimit);
						break;
					case SHOP_RESET_TYPE.WEEK:
					case SHOP_RESET_TYPE.WEEK_SUN:
					case SHOP_RESET_TYPE.WEEK_MON:
					case SHOP_RESET_TYPE.WEEK_TUE:
					case SHOP_RESET_TYPE.WEEK_WED:
					case SHOP_RESET_TYPE.WEEK_THU:
					case SHOP_RESET_TYPE.WEEK_FRI:
					case SHOP_RESET_TYPE.WEEK_SAT:
						msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_WEEK_02, nkmstageTempletV.EnterLimit - statePlayCnt, nkmstageTempletV.EnterLimit);
						break;
					case SHOP_RESET_TYPE.MONTH:
						msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_MONTH_02, nkmstageTempletV.EnterLimit - statePlayCnt, nkmstageTempletV.EnterLimit);
						break;
					default:
						msg = string.Format(NKCUtilString.GET_STRING_POPUP_DUNGEON_ENTER_LIMIT_DESC_DAY_02, nkmstageTempletV.EnterLimit - statePlayCnt, nkmstageTempletV.EnterLimit);
						break;
					}
					NKCUtil.SetLabelText(this.m_EnterLimit_TEXT, msg);
					if (nkmstageTempletV.EnterLimit - statePlayCnt <= 0)
					{
						NKCUtil.SetLabelTextColor(this.m_EnterLimit_TEXT, Color.red);
					}
					else
					{
						NKCUtil.SetLabelTextColor(this.m_EnterLimit_TEXT, Color.white);
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_DECK_VIEW_SIDE_UNIT_OPERATION_EnterLimit, bValue);
		}

		// Token: 0x06006332 RID: 25394 RVA: 0x001F34E8 File Offset: 0x001F16E8
		private void SetBotUI()
		{
			NKCUtil.SetGameobjectActive(this.m_objBanNotice, NKCUIDeckViewer.IsPVPSyncMode(this.m_ViewerOptions.eDeckviewerMode));
			if (NKCUIDeckViewer.IsDungeonAtkReadyScen(this.m_ViewerOptions.eDeckviewerMode))
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_DECK_VIEW_OPERATION_TITLE, true);
				NKC_SCEN_DUNGEON_ATK_READY scen_DUNGEON_ATK_READY = NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY();
				NKMStageTempletV2 stageTemplet = scen_DUNGEON_ATK_READY.GetStageTemplet();
				NKMDungeonTempletBase dungeonTempletBase = scen_DUNGEON_ATK_READY.GetDungeonTempletBase();
				if (stageTemplet != null)
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnEnemyList, true);
					NKMEpisodeTempletV2 nkmepisodeTempletV = NKMEpisodeTempletV2.Find(scen_DUNGEON_ATK_READY.GetEpisodeID(), scen_DUNGEON_ATK_READY.GetEpisodeDifficulty());
					if (nkmepisodeTempletV != null)
					{
						this.m_lbOperationEpisode.text = nkmepisodeTempletV.GetEpisodeTitle();
						if (nkmepisodeTempletV.m_EPCategory == EPISODE_CATEGORY.EC_DAILY)
						{
							this.m_lbOperationTitle.text = stageTemplet.GetDungeonName() + " " + NKCUtilString.GetDailyDungeonLVDesc(scen_DUNGEON_ATK_READY.GetStageUIIndex());
						}
						else
						{
							this.m_lbOperationTitle.text = string.Concat(new string[]
							{
								scen_DUNGEON_ATK_READY.GetActID().ToString(),
								"-",
								scen_DUNGEON_ATK_READY.GetStageUIIndex().ToString(),
								" ",
								stageTemplet.GetDungeonName()
							});
						}
					}
					if (stageTemplet.m_BuffType.Equals(RewardTuningType.None))
					{
						NKCUtil.SetGameobjectActive(this.m_OPERATION_TITLE_BONUS, false);
						return;
					}
					NKCUtil.SetGameobjectActive(this.m_OPERATION_TITLE_BONUS, true);
					NKCUtil.SetImageSprite(this.m_BONUS_ICON, NKCUtil.GetBounsTypeIcon(stageTemplet.m_BuffType, false), false);
					return;
				}
				else if (dungeonTempletBase != null)
				{
					NKCUtil.SetGameobjectActive(this.m_csbtnEnemyList, true);
					NKMEpisodeTempletV2 nkmepisodeTempletV2 = NKMEpisodeTempletV2.Find(scen_DUNGEON_ATK_READY.GetEpisodeID(), scen_DUNGEON_ATK_READY.GetEpisodeDifficulty());
					if (nkmepisodeTempletV2 != null)
					{
						this.m_lbOperationEpisode.text = nkmepisodeTempletV2.GetEpisodeTitle();
						if (nkmepisodeTempletV2.m_EPCategory == EPISODE_CATEGORY.EC_DAILY)
						{
							this.m_lbOperationTitle.text = dungeonTempletBase.GetDungeonName() + " " + NKCUtilString.GetDailyDungeonLVDesc(scen_DUNGEON_ATK_READY.GetStageUIIndex());
						}
						else
						{
							this.m_lbOperationTitle.text = string.Concat(new string[]
							{
								scen_DUNGEON_ATK_READY.GetActID().ToString(),
								"-",
								scen_DUNGEON_ATK_READY.GetStageUIIndex().ToString(),
								" ",
								dungeonTempletBase.GetDungeonName()
							});
						}
					}
					if (dungeonTempletBase.StageTemplet != null)
					{
						if (dungeonTempletBase.StageTemplet.m_BuffType.Equals(RewardTuningType.None))
						{
							NKCUtil.SetGameobjectActive(this.m_OPERATION_TITLE_BONUS, false);
							return;
						}
						NKCUtil.SetGameobjectActive(this.m_OPERATION_TITLE_BONUS, true);
						NKCUtil.SetImageSprite(this.m_BONUS_ICON, NKCUtil.GetBounsTypeIcon(dungeonTempletBase.StageTemplet.m_BuffType, false), false);
						return;
					}
				}
			}
			else
			{
				NKCUIDeckViewer.DeckViewerMode eDeckviewerMode = this.m_ViewerOptions.eDeckviewerMode;
				if (eDeckviewerMode - NKCUIDeckViewer.DeckViewerMode.WarfareBatch <= 1)
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_DECK_VIEW_OPERATION_TITLE, true);
					NKC_SCEN_WARFARE_GAME nkc_SCEN_WARFARE_GAME = NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME();
					NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(nkc_SCEN_WARFARE_GAME.GetWarfareStrID());
					NKCUtil.SetGameobjectActive(this.m_csbtnEnemyList, nkmwarfareTemplet != null);
					if (nkmwarfareTemplet == null)
					{
						return;
					}
					NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(nkc_SCEN_WARFARE_GAME.GetWarfareStrID());
					if (nkmstageTempletV != null)
					{
						NKMEpisodeTempletV2 episodeTemplet = nkmstageTempletV.EpisodeTemplet;
						if (episodeTemplet != null)
						{
							this.m_lbOperationEpisode.text = episodeTemplet.GetEpisodeTitle();
						}
						this.m_lbOperationTitle.text = string.Concat(new string[]
						{
							nkmstageTempletV.ActId.ToString(),
							"-",
							nkmstageTempletV.m_StageUINum.ToString(),
							" ",
							nkmwarfareTemplet.GetWarfareName()
						});
					}
					if (nkmstageTempletV != null)
					{
						if (nkmstageTempletV.m_BuffType.Equals(RewardTuningType.None))
						{
							NKCUtil.SetGameobjectActive(this.m_OPERATION_TITLE_BONUS, false);
							return;
						}
						NKCUtil.SetGameobjectActive(this.m_OPERATION_TITLE_BONUS, true);
						NKCUtil.SetImageSprite(this.m_BONUS_ICON, NKCUtil.GetBounsTypeIcon(nkmstageTempletV.m_BuffType, false), false);
						return;
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_NKM_DECK_VIEW_OPERATION_TITLE, false);
					NKCUtil.SetGameobjectActive(this.m_csbtnEnemyList, false);
				}
			}
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x001F38B8 File Offset: 0x001F1AB8
		private void UpdateDeckOperationPower()
		{
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				bool bPVP = this.IsPVPMode(this.m_ViewerOptions.eDeckviewerMode);
				bool bPossibleShowBan = NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode);
				bool bPossibleShowUp = NKCUtil.CheckPossibleShowUpUnit(this.m_ViewerOptions.eDeckviewerMode);
				int operationPower = NKCLocalDeckDataManager.GetOperationPower((int)this.m_SelectDeckIndex.m_iIndex, bPVP, bPossibleShowBan, bPossibleShowUp);
				this.m_lbDeckOperationPower.text = operationPower.ToString();
				NKCUtil.SetLabelText(this.m_lbAdditionalInfoTitle, NKCUtilString.GET_STRING_DECK_AVG_SUMMON_COST);
				NKCUtil.SetLabelText(this.m_lbAdditionalInfoNumber, string.Format("{0:0.00}", this.CalculateLocalDeckAvgSummonCost((int)this.m_SelectDeckIndex.m_iIndex)));
				return;
			}
			if (!this.IsSupportMenu)
			{
				bool bPVP2 = this.IsPVPMode(this.m_ViewerOptions.eDeckviewerMode);
				bool flag = NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode);
				bool flag2 = NKCUtil.CheckPossibleShowUpUnit(this.m_ViewerOptions.eDeckviewerMode);
				int armyAvarageOperationPower = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetArmyAvarageOperationPower(this.m_SelectDeckIndex, bPVP2, flag ? NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL) : null, flag2 ? NKCBanManager.m_dicNKMUpData : null);
				this.m_lbDeckOperationPower.text = armyAvarageOperationPower.ToString();
				NKCUtil.SetLabelText(this.m_lbAdditionalInfoTitle, NKCUtilString.GET_STRING_DECK_AVG_SUMMON_COST);
				NKCUtil.SetLabelText(this.m_lbAdditionalInfoNumber, string.Format("{0:0.00}", this.CalculateDeckAvgSummonCost(this.m_SelectDeckIndex)));
				return;
			}
			WarfareSupporterListData selectedData = this.m_NKCDeckViewSupportList.GetSelectedData();
			if (selectedData != null)
			{
				this.m_lbDeckOperationPower.text = selectedData.deckData.CalculateOperationPower().ToString();
				NKCUtil.SetLabelText(this.m_lbAdditionalInfoTitle, NKCUtilString.GET_STRING_DECK_AVG_SUMMON_COST);
				NKCUtil.SetLabelText(this.m_lbAdditionalInfoNumber, string.Format("{0:0.00}", selectedData.deckData.CalculateSummonCost()));
			}
		}

		// Token: 0x06006334 RID: 25396 RVA: 0x001F3A98 File Offset: 0x001F1C98
		private float CalculateDeckAvgSummonCost(NKMDeckIndex deckIndex)
		{
			NKMArmyData armyData = NKCScenManager.CurrentUserData().m_ArmyData;
			if (armyData == null)
			{
				return 0f;
			}
			bool flag = NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode);
			bool flag2 = NKCUtil.CheckPossibleShowUpUnit(this.m_ViewerOptions.eDeckviewerMode);
			return armyData.CalculateDeckAvgSummonCost(deckIndex, flag ? NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL) : null, flag2 ? NKCBanManager.m_dicNKMUpData : null);
		}

		// Token: 0x06006335 RID: 25397 RVA: 0x001F3AFC File Offset: 0x001F1CFC
		public float CalculateLocalDeckAvgSummonCost(int deckIndex)
		{
			int num = 0;
			int num2 = 0;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return 0f;
			}
			bool flag = NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode);
			bool flag2 = NKCUtil.CheckPossibleShowUpUnit(this.m_ViewerOptions.eDeckviewerMode);
			Dictionary<int, NKMBanData> dicNKMBanData = flag ? NKCBanManager.GetBanData(NKCBanManager.BAN_DATA_TYPE.FINAL) : null;
			Dictionary<int, NKMUnitUpData> dicNKMUpData = flag2 ? NKCBanManager.m_dicNKMUpData : null;
			int localLeaderIndex = NKCLocalDeckDataManager.GetLocalLeaderIndex(deckIndex);
			List<long> localUnitDeckData = NKCLocalDeckDataManager.GetLocalUnitDeckData(deckIndex);
			int count = localUnitDeckData.Count;
			for (int i = 0; i < count; i++)
			{
				long unitUid = localUnitDeckData[i];
				NKMUnitData unitFromUID = nkmuserData.m_ArmyData.GetUnitFromUID(unitUid);
				if (unitFromUID != null)
				{
					num++;
					NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitFromUID.m_UnitID);
					if (unitStatTemplet == null)
					{
						Log.Error(string.Format("Cannot found UnitStatTemplet. UserUid:{0}, UnitId:{1}", nkmuserData.m_UserUID, unitFromUID.m_UnitID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIDeckViewer.cs", 1128);
					}
					else
					{
						num2 += unitStatTemplet.GetRespawnCost(i == localLeaderIndex, dicNKMBanData, dicNKMUpData);
					}
				}
			}
			if (num == 0)
			{
				return 0f;
			}
			return (float)num2 / (float)num;
		}

		// Token: 0x06006336 RID: 25398 RVA: 0x001F3C0F File Offset: 0x001F1E0F
		public void SetUnitSlotData(NKMDeckIndex deckIndex, int unitSlotIndex, bool bEffect)
		{
			if (deckIndex == this.m_SelectDeckIndex)
			{
				NKCDeckViewUnit currDeckViewUnit = this.GetCurrDeckViewUnit();
				if (currDeckViewUnit == null)
				{
					return;
				}
				currDeckViewUnit.SetUnitSlotData(this.NKMArmyData, this.m_SelectDeckIndex, unitSlotIndex, bEffect, this.m_ViewerOptions);
			}
		}

		// Token: 0x06006337 RID: 25399 RVA: 0x001F3C44 File Offset: 0x001F1E44
		public override void Hide()
		{
			NKCDeckViewUnit currDeckViewUnit = this.GetCurrDeckViewUnit();
			if (currDeckViewUnit != null)
			{
				currDeckViewUnit.CancelAllDrag();
			}
			this.m_objSlotUnlockEffect.transform.SetParent(base.transform);
			NKCUtil.SetGameobjectActive(this.m_objSlotUnlockEffect, false);
			if (this.m_NKCDeckViewUnitSelectList.IsOpen)
			{
				this.m_bUnitListActivated = true;
				NKCUtil.SetGameobjectActive(this.m_NKCDeckViewUnitSelectList, false);
			}
			else
			{
				this.m_bUnitListActivated = false;
			}
			Action dOnHide = this.m_ViewerOptions.dOnHide;
			if (dOnHide != null)
			{
				dOnHide();
			}
			base.Hide();
		}

		// Token: 0x06006338 RID: 25400 RVA: 0x001F3CCC File Offset: 0x001F1ECC
		public override void UnHide()
		{
			base.UnHide();
			if (this.m_bUnitListActivated)
			{
				NKCUtil.SetGameobjectActive(this.m_NKCDeckViewUnitSelectList, true);
			}
			this.m_bUnitListActivated = false;
			if (this.NKMArmyData.GetDeckUnitByIndex(this.m_SelectDeckIndex, this.m_SelectUnitSlotIndex) == null && !this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().hasUnitData() && this.m_ViewerOptions.eDeckviewerMode != NKCUIDeckViewer.DeckViewerMode.PrepareRaid && this.m_ViewerOptions.eDeckviewerMode != NKCUIDeckViewer.DeckViewerMode.GuildCoopBoss)
			{
				this.PlayLoadingAnim("BASE");
			}
			else
			{
				this.PlayLoadingAnim("CLOSE");
			}
			if (this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().hasUnitData())
			{
				if (this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().IsMatchedSideIllustToUnitType(NKM_UNIT_TYPE.NUT_SHIP))
				{
					this.EnableUnitView(false, true);
				}
				else
				{
					this.EnableUnitView(true, true);
				}
			}
			Action dOnUnhide = this.m_ViewerOptions.dOnUnhide;
			if (dOnUnhide != null)
			{
				dOnUnhide();
			}
			NKCDeckViewUnit currDeckViewUnit = this.GetCurrDeckViewUnit();
			if (currDeckViewUnit == null)
			{
				return;
			}
			currDeckViewUnit.SlotResetPos(true);
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x001F3DBC File Offset: 0x001F1FBC
		public override void OnCloseInstance()
		{
			if (this.m_NKCDeckViewUnitSelectList != null)
			{
				UnityEngine.Object.Destroy(this.m_NKCDeckViewUnitSelectList.gameObject);
			}
			if (this.m_NKCDeckViewSupportList != null)
			{
				UnityEngine.Object.Destroy(this.m_NKCDeckViewSupportList.gameObject);
			}
			base.OnCloseInstance();
		}

		// Token: 0x0600633A RID: 25402 RVA: 0x001F3E0C File Offset: 0x001F200C
		public override void CloseInternal()
		{
			if (this.m_ViewerOptions.bUseAsyncDeckSetting)
			{
				NKMDeckData deckData = this.NKMArmyData.GetDeckData(this.m_SelectDeckIndex);
				if (deckData != null)
				{
					deckData.DeepCopyFrom(this.m_AsyncOriginalDeckData);
				}
			}
			this.m_NKCDeckViewList.Close();
			this.m_NKCDeckViewShip.Close();
			this.m_NKCDeckViewUnit.Close();
			this.m_NKCDeckViewOperator.Close();
			this.m_NKCDeckViewSide.Close();
			this.m_NKCDeckViewSupportList.Close();
			this.m_NKCDeckViewSupportList.Clear();
			NKCUtil.SetGameobjectActive(this.GetDeckViewUnit24(), false);
			NKCUtil.SetGameobjectActive(this.m_NKCUIRaidRightSide, false);
			this.m_objSlotUnlockEffect.transform.SetParent(base.transform);
			NKCUtil.SetGameobjectActive(this.m_objSlotUnlockEffect, false);
			this.m_NKCDeckViewUnitSelectList.Cleanup();
			this.m_NKCDeckViewUnitSelectList.Close(false);
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600633B RID: 25403 RVA: 0x001F3F00 File Offset: 0x001F2100
		private void OnDestroy()
		{
			if (this.m_NKCDeckViewUnit_24 != null)
			{
				this.m_NKCDeckViewUnit_24.CloseResource("AB_UI_NKM_UI_DECK_VIEW", "NKM_UI_DECK_VIEW_UNIT_24");
				this.m_NKCDeckViewUnit_24 = null;
			}
			if (this.m_NKCUIRaidRightSide != null)
			{
				this.m_NKCUIRaidRightSide.CloseInstance();
				this.m_NKCUIRaidRightSide = null;
			}
			if (this.m_NKCUIGuildCoopRaidRightSide != null)
			{
				this.m_NKCUIGuildCoopRaidRightSide.CloseInstance();
				this.m_NKCUIGuildCoopRaidRightSide = null;
			}
			NKCUIDeckViewer.m_Instance = null;
		}

		// Token: 0x0600633C RID: 25404 RVA: 0x001F3F7D File Offset: 0x001F217D
		public override void OnBackButton()
		{
			if (this.m_NKCDeckViewUnitSelectList.IsOpen)
			{
				this.m_NKCDeckViewUnitSelectList.Close(true);
				return;
			}
			if (this.m_ViewerOptions.dOnBackButton != null)
			{
				this.m_ViewerOptions.dOnBackButton();
				return;
			}
			base.OnBackButton();
		}

		// Token: 0x0600633D RID: 25405 RVA: 0x001F3FC0 File Offset: 0x001F21C0
		public void Update()
		{
			if (this.GetUnitViewEnable())
			{
				this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().Open(this.m_ViewerOptions.eDeckviewerMode, false);
			}
			if (base.IsOpen && this.m_CanvasGroup.alpha < 1f)
			{
				this.m_CanvasGroup.alpha += Time.deltaTime * 2.5f;
				if (this.m_CanvasGroup.alpha >= 1f)
				{
					this.m_CanvasGroup.alpha = 1f;
				}
			}
		}

		// Token: 0x0600633E RID: 25406 RVA: 0x001F404A File Offset: 0x001F224A
		private bool IsAsyncPvP()
		{
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_ASYNC_NEW_MODE))
			{
				return this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.AsyncPvpDefenseDeck;
			}
			return this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.AsyncPvPBattleStart || this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.AsyncPvpDefenseDeck;
		}

		// Token: 0x0600633F RID: 25407 RVA: 0x001F4084 File Offset: 0x001F2284
		public void DeckUnlockRequestPopup(NKMDeckIndex index)
		{
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_UNLOCK, NKCUtilString.GET_STRING_DECK_SLOT_UNLOCK, 101, 600, delegate()
			{
				NKCPacketSender.Send_NKMPacket_DECK_UNLOCK_REQ(index.m_eDeckType);
			}, null, true);
		}

		// Token: 0x06006340 RID: 25408 RVA: 0x001F40C7 File Offset: 0x001F22C7
		public void SelectCurrentDeck()
		{
			this.SelectDeck(this.m_SelectDeckIndex);
		}

		// Token: 0x06006341 RID: 25409 RVA: 0x001F40D8 File Offset: 0x001F22D8
		public void SelectDeck(NKMDeckIndex index)
		{
			if (this.m_SelectDeckIndex != index)
			{
				NKCUIDeckViewer.DeckViewerOption.OnChangeDeckIndex dOnChangeDeckIndex = this.m_ViewerOptions.dOnChangeDeckIndex;
				if (dOnChangeDeckIndex != null)
				{
					dOnChangeDeckIndex(index);
				}
			}
			this.m_SelectDeckIndex = index;
			this.m_NKCDeckViewSide.ChangeDeckIndex(this.m_SelectDeckIndex);
			this.m_NKCDeckViewList.SetDeckListButton(this.IsMultiSelect(), this.NKMArmyData, this.m_ViewerOptions, (int)this.m_SelectDeckIndex.m_iIndex, true);
			this.CloseSupList();
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				long shipUId = NKCLocalDeckDataManager.GetShipUId((int)this.m_SelectDeckIndex.m_iIndex);
				NKMUnitData shipFromUID = this.NKMArmyData.GetShipFromUID(shipUId);
				this.m_NKCDeckViewShip.SetShipSlotData(shipFromUID, NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode));
			}
			else
			{
				this.m_NKCDeckViewShip.SetShipSlotData(this.NKMArmyData.GetDeckShip(this.m_SelectDeckIndex), NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode));
			}
			NKCDeckViewUnit currDeckViewUnit = this.GetCurrDeckViewUnit();
			if (currDeckViewUnit != null)
			{
				currDeckViewUnit.SetDeckListButton(this.NKMArmyData, this.m_SelectDeckIndex, this.m_ViewerOptions);
			}
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				long operatorUId = NKCLocalDeckDataManager.GetOperatorUId((int)this.m_SelectDeckIndex.m_iIndex);
				NKMOperator operatorFromUId = this.NKMArmyData.GetOperatorFromUId(operatorUId);
				this.UpdateOperator(operatorFromUId);
			}
			else
			{
				this.UpdateOperator(this.NKMArmyData.GetDeckOperator(this.m_SelectDeckIndex));
			}
			this.EnableUnitView(true, false);
			this.m_NKCDeckViewShip.SetSelectEffect(false);
			this.m_NKM_UI_OPERATOR_DECK_SLOT.SetSelectEffect(false);
			this.SetTitleActive(false);
			NKCUtil.SetLabelText(this.m_lbDeckNamePlaceholder, NKCUIDeckViewer.GetDeckDefaultName(index));
			NKCUtil.SetLabelText(this.m_lbSquadNumber, NKCUtilString.GET_STRING_SQUAD_TWO_PARAM, new object[]
			{
				(int)(index.m_iIndex + 1),
				NKCUtilString.GetRankNumber((int)(index.m_iIndex + 1), true)
			});
			foreach (NKCUIDeckViewer.DeckTypeIconObject deckTypeIconObject in this.m_lstDecktypeIcon)
			{
				NKCUtil.SetGameobjectActive(deckTypeIconObject.objSquadIcon, index.m_eDeckType == deckTypeIconObject.eDeckType);
			}
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				this.m_bDeckInMission = false;
				this.m_bDeckInWarfareBatch = false;
				this.m_bDeckInDiveBatch = false;
				this.SetDeckNameInput(false, "");
			}
			else
			{
				NKMDeckData deckData = this.NKMArmyData.GetDeckData(this.m_SelectDeckIndex);
				if (deckData != null)
				{
					this.m_bDeckInMission = (deckData.GetState() == NKM_DECK_STATE.DECK_STATE_WORLDMAP_MISSION);
					this.m_bDeckInWarfareBatch = (deckData.GetState() == NKM_DECK_STATE.DECK_STATE_WARFARE);
					this.m_bDeckInDiveBatch = (deckData.GetState() == NKM_DECK_STATE.DECK_STATE_DIVE);
					NKM_DECK_TYPE eDeckType = this.m_SelectDeckIndex.m_eDeckType;
					bool bEnable = eDeckType - NKM_DECK_TYPE.NDT_NORMAL <= 3 || eDeckType == NKM_DECK_TYPE.NDT_DIVE;
					this.SetDeckNameInput(bEnable, NKCUIDeckViewer.GetDeckName(this.m_SelectDeckIndex));
				}
				else
				{
					this.m_bDeckInMission = false;
					this.m_bDeckInWarfareBatch = false;
					this.m_bDeckInDiveBatch = false;
					this.SetDeckNameInput(false, "");
				}
			}
			this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetUnitData(null, false, this.IsUnitChangePossible(), false);
			this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetOperatorData(null, this.IsUnitChangePossible(), false);
			this.SelectDeckViewUnit(this.m_SelectUnitSlotIndex, true, false);
			this.UpdateDeckOperationPower();
			this.UpdateDeckReadyState();
			this.UpdateDeckToggleUI();
			if ((this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareRaid || this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.GuildCoopBoss) && !this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().hasUnitData() && !this.m_NKCDeckViewUnitSelectList.IsOpen)
			{
				this.OnClickCloseBtnOfDeckViewSide();
			}
		}

		// Token: 0x06006342 RID: 25410 RVA: 0x001F446C File Offset: 0x001F266C
		private void SetTitleActive(bool isSupporter)
		{
			NKCUtil.SetGameobjectActive(this.m_ifDeckName, !isSupporter);
			NKCUtil.SetGameobjectActive(this.m_csbtnDeckName, !isSupporter);
			NKCUtil.SetGameobjectActive(this.m_lbSquadNumber, false);
			NKCUtil.SetGameobjectActive(this.m_lbSupporterName, isSupporter);
			NKCUtil.SetGameobjectActive(this.m_csbtn_NKM_DECK_VIEW_HELP, !isSupporter);
		}

		// Token: 0x06006343 RID: 25411 RVA: 0x001F44BE File Offset: 0x001F26BE
		public void SetDeckScroll(int deckIndex)
		{
			this.m_NKCDeckViewList.SetScrollPosition(deckIndex);
		}

		// Token: 0x06006344 RID: 25412 RVA: 0x001F44CC File Offset: 0x001F26CC
		public void DeckViewListClick(NKMDeckIndex index)
		{
			this.m_NKCDeckViewList.DeckViewListClick(index);
		}

		// Token: 0x06006345 RID: 25413 RVA: 0x001F44DC File Offset: 0x001F26DC
		private void UpdateDeckReadyState()
		{
			if (this.m_ViewerOptions.dCheckSideMenuButton != null)
			{
				NKM_ERROR_CODE nkm_ERROR_CODE = this.m_ViewerOptions.dCheckSideMenuButton(this.m_SelectDeckIndex);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					this.m_NKCDeckViewSide.SetEnableButtons(nkm_ERROR_CODE);
					return;
				}
			}
			NKCUIDeckViewer.DeckViewerMode eDeckviewerMode = this.m_ViewerOptions.eDeckviewerMode;
			switch (eDeckviewerMode)
			{
			case NKCUIDeckViewer.DeckViewerMode.PvPBattleFindTarget:
			{
				NKM_ERROR_CODE enableButtons = NKMMain.IsValidDeck(this.NKMArmyData, this.m_SelectDeckIndex.m_eDeckType, this.m_SelectDeckIndex.m_iIndex, NKM_GAME_TYPE.NGT_PVP_RANK);
				this.m_NKCDeckViewSide.SetEnableButtons(enableButtons);
				return;
			}
			case NKCUIDeckViewer.DeckViewerMode.AsyncPvPBattleStart:
			case NKCUIDeckViewer.DeckViewerMode.AsyncPvpDefenseDeck:
			{
				NKM_ERROR_CODE enableButtons2 = NKMMain.IsValidDeck(this.NKMArmyData, this.m_SelectDeckIndex.m_eDeckType, this.m_SelectDeckIndex.m_iIndex, NKM_GAME_TYPE.NGT_ASYNC_PVP);
				this.m_NKCDeckViewSide.SetEnableButtons(enableButtons2);
				return;
			}
			case NKCUIDeckViewer.DeckViewerMode.PrepareDungeonBattle:
			case NKCUIDeckViewer.DeckViewerMode.DeckSelect:
			case NKCUIDeckViewer.DeckViewerMode.PrepareDungeonBattle_Daily:
			case NKCUIDeckViewer.DeckViewerMode.PrepareDungeonBattleWithoutCost:
			case NKCUIDeckViewer.DeckViewerMode.PrepareDungeonBattle_CC:
				goto IL_22B;
			case NKCUIDeckViewer.DeckViewerMode.WorldMapMissionDeckSelect:
			{
				NKM_ERROR_CODE nkm_ERROR_CODE2 = NKMWorldMapManager.IsValidDeckForWorldMapMission(NKCScenManager.CurrentUserData(), this.m_SelectDeckIndex, this.m_ViewerOptions.WorldMapMissionCityID);
				bool bDeckReady = nkm_ERROR_CODE2 == NKM_ERROR_CODE.NEC_OK;
				this.m_NKCDeckViewSide.SetEnableButtons(nkm_ERROR_CODE2);
				NKMWorldMapMissionTemplet missionTemplet = NKMWorldMapManager.GetMissionTemplet(this.m_ViewerOptions.WorldMapMissionID);
				NKMWorldMapCityData cityData = NKCScenManager.CurrentUserData().m_WorldmapData.GetCityData(this.m_ViewerOptions.WorldMapMissionCityID);
				int missionSuccessRate = NKMWorldMapManager.GetMissionSuccessRate(missionTemplet, this.NKMArmyData, cityData);
				NKCCompanyBuff.IncreaseMissioRateInWorldMap(this.NKMArmyData.Owner.m_companyBuffDataList, ref missionSuccessRate);
				this.m_NKCDeckViewSide.SetSuccessRate(missionSuccessRate, bDeckReady);
				return;
			}
			case NKCUIDeckViewer.DeckViewerMode.WarfareBatch:
			{
				NKM_ERROR_CODE enableButtons3 = NKMMain.IsValidDeck(this.NKMArmyData, this.m_SelectDeckIndex);
				this.m_NKCDeckViewSide.SetEnableButtons(enableButtons3);
				return;
			}
			case NKCUIDeckViewer.DeckViewerMode.WarfareBatch_Assault:
			{
				NKM_ERROR_CODE nkm_ERROR_CODE3 = NKMMain.IsValidDeck(this.NKMArmyData, this.m_SelectDeckIndex);
				if (nkm_ERROR_CODE3 == NKM_ERROR_CODE.NEC_OK && !NKCWarfareManager.CheckAssaultShip(NKCScenManager.CurrentUserData(), this.m_SelectDeckIndex))
				{
					nkm_ERROR_CODE3 = NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_ASSAULT_POSITION;
				}
				this.m_NKCDeckViewSide.SetEnableButtons(nkm_ERROR_CODE3);
				return;
			}
			case NKCUIDeckViewer.DeckViewerMode.WarfareRecovery:
			case NKCUIDeckViewer.DeckViewerMode.DeckMultiSelect:
				break;
			case NKCUIDeckViewer.DeckViewerMode.MainDeckSelect:
			{
				NKM_ERROR_CODE enableButtons4 = NKM_ERROR_CODE.NEC_FAIL_DECK_DATA_INVALID;
				NKMDeckData deckData = this.NKMArmyData.GetDeckData(this.m_SelectDeckIndex);
				if (deckData != null)
				{
					enableButtons4 = NKMMain.IsValidDeckCommon(this.NKMArmyData, deckData, this.m_SelectDeckIndex, NKM_GAME_TYPE.NGT_INVALID);
				}
				this.m_NKCDeckViewSide.SetEnableButtons(enableButtons4);
				return;
			}
			default:
				if (eDeckviewerMode != NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
				{
					goto IL_22B;
				}
				break;
			}
			this.m_NKCDeckViewSide.SetEnableButtons(NKM_ERROR_CODE.NEC_OK);
			return;
			IL_22B:
			NKM_ERROR_CODE enableButtons5 = NKMMain.IsValidDeck(this.NKMArmyData, this.m_SelectDeckIndex);
			this.m_NKCDeckViewSide.SetEnableButtons(enableButtons5);
		}

		// Token: 0x06006346 RID: 25414 RVA: 0x001F4734 File Offset: 0x001F2934
		public void SetShipSlotData()
		{
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				long shipUId = NKCLocalDeckDataManager.GetShipUId((int)this.m_SelectDeckIndex.m_iIndex);
				this.m_NKCDeckViewShip.SetShipSlotData(this.NKMArmyData.GetShipFromUID(shipUId), NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode));
				return;
			}
			this.m_NKCDeckViewShip.SetShipSlotData(this.NKMArmyData.GetDeckShip(this.m_SelectDeckIndex), NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode));
		}

		// Token: 0x06006347 RID: 25415 RVA: 0x001F47B5 File Offset: 0x001F29B5
		public void SetLeader(NKMDeckIndex deckIndex, int leaderIndex, bool bEffect)
		{
			NKCDeckViewUnit currDeckViewUnit = this.GetCurrDeckViewUnit();
			if (currDeckViewUnit != null)
			{
				currDeckViewUnit.SetLeader(leaderIndex, bEffect);
			}
			if (leaderIndex == this.m_SelectUnitSlotIndex)
			{
				this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetLeader(true);
			}
		}

		// Token: 0x06006348 RID: 25416 RVA: 0x001F47E4 File Offset: 0x001F29E4
		public void SelectDeckViewUnit(int index, bool bForce = false, bool bOpenUnitSelectListIfEmpty = false)
		{
			this.m_SelectUnitSlotIndex = index;
			this.m_NKCDeckViewShip.SetSelectEffect(false);
			this.m_NKM_UI_OPERATOR_DECK_SLOT.SetSelectEffect(false);
			NKCDeckViewUnit currDeckViewUnit = this.GetCurrDeckViewUnit();
			if (currDeckViewUnit != null)
			{
				currDeckViewUnit.SelectDeckViewUnit(index, bForce);
			}
			if (index >= 0)
			{
				NKMUnitData nkmunitData = null;
				this.m_DeckUnitList.Clear();
				bool bLeader;
				if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
				{
					NKCLocalDeckDataManager.GetLocalUnitDeckData((int)this.m_SelectDeckIndex.m_iIndex).ForEach(delegate(long e)
					{
						this.m_DeckUnitList.Add(e);
					});
					if (this.m_DeckUnitList.Count > index)
					{
						nkmunitData = this.NKMArmyData.GetUnitFromUID(this.m_DeckUnitList[index]);
					}
					bLeader = (index == NKCLocalDeckDataManager.GetLocalLeaderIndex((int)this.m_SelectDeckIndex.m_iIndex));
				}
				else
				{
					this.NKMArmyData.GetDeckList(this.m_SelectDeckIndex.m_eDeckType, (int)this.m_SelectDeckIndex.m_iIndex, ref this.m_DeckUnitList);
					nkmunitData = this.NKMArmyData.GetDeckUnitByIndex(this.m_SelectDeckIndex, this.m_SelectUnitSlotIndex);
					NKMDeckData deckData = this.NKMArmyData.GetDeckData(this.m_SelectDeckIndex);
					sbyte? b = (deckData != null) ? new sbyte?(deckData.m_LeaderIndex) : null;
					int? num = (b != null) ? new int?((int)b.GetValueOrDefault()) : null;
					int selectUnitSlotIndex = this.m_SelectUnitSlotIndex;
					bLeader = (num.GetValueOrDefault() == selectUnitSlotIndex & num != null);
				}
				if (bOpenUnitSelectListIfEmpty && nkmunitData == null)
				{
					this.OpenDeckSelectList(NKM_UNIT_TYPE.NUT_NORMAL, 0L);
				}
				else
				{
					this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_NORMAL, (nkmunitData != null) ? nkmunitData.m_UnitUID : 0L);
				}
				if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareRaid)
				{
					if (this.m_NKCUIRaidRightSide != null)
					{
						this.m_NKCUIRaidRightSide.Close();
					}
					this.m_NKCDeckViewSide.Open(this.m_ViewerOptions, false, false);
				}
				else if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.GuildCoopBoss)
				{
					if (this.m_NKCUIGuildCoopRaidRightSide != null)
					{
						this.m_NKCUIGuildCoopRaidRightSide.Close();
					}
					this.m_NKCDeckViewSide.Open(this.m_ViewerOptions, false, false);
				}
				this.m_NKCDeckViewSide.SetUnitData(nkmunitData, false);
				this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetOperatorData(null, this.IsUnitChangePossible(), false);
				this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetUnitData(nkmunitData, bLeader, this.IsUnitChangePossible(), bForce);
			}
			else if (this.m_NKCDeckViewUnitSelectList.IsOpen)
			{
				NKM_UNIT_TYPE eCurrentSelectListType = this.m_eCurrentSelectListType;
				if (eCurrentSelectListType != NKM_UNIT_TYPE.NUT_SHIP)
				{
					if (eCurrentSelectListType == NKM_UNIT_TYPE.NUT_OPERATOR)
					{
						if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
						{
							long operatorUId = NKCLocalDeckDataManager.GetOperatorUId((int)this.m_SelectDeckIndex.m_iIndex);
							NKMOperator operatorFromUId = this.NKMArmyData.GetOperatorFromUId(operatorUId);
							this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_OPERATOR, (operatorFromUId != null) ? operatorFromUId.uid : 0L);
						}
						else
						{
							NKMOperator deckOperatorByIndex = this.NKMArmyData.GetDeckOperatorByIndex(this.m_SelectDeckIndex);
							this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_OPERATOR, (deckOperatorByIndex != null) ? deckOperatorByIndex.uid : 0L);
						}
					}
				}
				else
				{
					this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_SHIP, (this.m_NKCDeckViewShip.GetUnitData() != null) ? this.m_NKCDeckViewShip.GetUnitData().m_UnitUID : 0L);
				}
			}
			this.EnableUnitView(true, false);
		}

		// Token: 0x06006349 RID: 25417 RVA: 0x001F4AFC File Offset: 0x001F2CFC
		public void SetDeckViewShipUnit(long shipUID)
		{
			NKMUnitData shipFromUID = this.NKMArmyData.GetShipFromUID(shipUID);
			this.m_NKCDeckViewSide.SetUnitData(shipFromUID, false);
			this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetUnitData(shipFromUID, false, this.IsUnitChangePossible(), false);
			this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().PlayLoadingAnim("CLOSE");
			if (shipFromUID == null)
			{
				this.EnableUnitView(true, false);
				return;
			}
			this.EnableUnitView(false, false);
		}

		// Token: 0x0600634A RID: 25418 RVA: 0x001F4B68 File Offset: 0x001F2D68
		public void DeckViewShipClick()
		{
			if (this.IsSupportMenu)
			{
				return;
			}
			this.EnableUnitView(false, false);
			this.m_NKM_UI_OPERATOR_DECK_SLOT.SetSelectEffect(false);
			if (this.m_NKCDeckViewShip.GetUnitData() != null)
			{
				this.m_NKCDeckViewSide.SetUnitData(this.m_NKCDeckViewShip.GetUnitData(), false);
				this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetUnitData(this.m_NKCDeckViewShip.GetUnitData(), false, this.IsUnitChangePossible(), this.m_eCurrentSelectListType == NKM_UNIT_TYPE.NUT_OPERATOR);
				this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_SHIP, this.m_NKCDeckViewShip.GetUnitData().m_UnitUID);
				this.m_NKCDeckViewShip.SetSelectEffect(false);
			}
			else
			{
				this.m_NKCDeckViewShip.SetSelectEffect(true);
				this.OpenDeckSelectList(NKM_UNIT_TYPE.NUT_SHIP, 0L);
			}
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareRaid)
			{
				if (this.m_NKCUIRaidRightSide != null)
				{
					this.m_NKCUIRaidRightSide.Close();
				}
			}
			else if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.GuildCoopBoss && this.m_NKCUIGuildCoopRaidRightSide != null)
			{
				this.m_NKCUIGuildCoopRaidRightSide.Close();
			}
			this.m_SelectUnitSlotIndex = -1;
			NKCDeckViewUnit currDeckViewUnit = this.GetCurrDeckViewUnit();
			if (currDeckViewUnit == null)
			{
				return;
			}
			currDeckViewUnit.SelectDeckViewUnit(-1, false);
		}

		// Token: 0x0600634B RID: 25419 RVA: 0x001F4C88 File Offset: 0x001F2E88
		public void DeckViewUnitInfoClick(NKMUnitData UnitData)
		{
			if (UnitData != null)
			{
				NKM_UNIT_TYPE nkm_UNIT_TYPE = NKMUnitManager.GetUnitTempletBase(UnitData.m_UnitID).m_NKM_UNIT_TYPE;
				if (nkm_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL)
				{
					NKCUIUnitInfo.Instance.Open(UnitData, new NKCUIUnitInfo.OnRemoveFromDeck(this.OnRemoveUnit), new NKCUIUnitInfo.OpenOption(this.m_DeckUnitList, this.m_SelectUnitSlotIndex), NKC_SCEN_UNIT_LIST.eUIOpenReserve.Nothing);
					return;
				}
				if (nkm_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
				{
					return;
				}
				NKCUIShipInfo.Instance.Open(UnitData, this.m_SelectDeckIndex, null, NKC_SCEN_UNIT_LIST.eUIOpenReserve.Nothing);
			}
		}

		// Token: 0x0600634C RID: 25420 RVA: 0x001F4CF0 File Offset: 0x001F2EF0
		public void EnableUnitView(bool bEnable, bool bForce = false)
		{
			if (this.m_bUnitViewEnable == bEnable && !bForce)
			{
				return;
			}
			bool flag;
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				flag = (NKCLocalDeckDataManager.GetShipUId((int)this.m_SelectDeckIndex.m_iIndex) <= 0L);
			}
			else
			{
				flag = (this.NKMArmyData.GetDeckShip(this.m_SelectDeckIndex) == null);
			}
			if (bEnable || flag)
			{
				this.PlayLoadingAnim("CHANGE");
				this.m_NKCDeckViewOperator.Enable();
				NKCDeckViewUnit currDeckViewUnit = this.GetCurrDeckViewUnit();
				if (currDeckViewUnit != null)
				{
					currDeckViewUnit.Enable();
				}
				this.m_NKCDeckViewShip.Disable();
				this.m_bUnitViewEnable = true;
				return;
			}
			this.PlayLoadingAnim("CHANGE");
			this.m_NKCDeckViewOperator.Disable();
			NKCDeckViewUnit currDeckViewUnit2 = this.GetCurrDeckViewUnit();
			if (currDeckViewUnit2 != null)
			{
				currDeckViewUnit2.Disable();
			}
			this.m_NKCDeckViewShip.Enable(false);
			this.m_bUnitViewEnable = false;
		}

		// Token: 0x0600634D RID: 25421 RVA: 0x001F4DC3 File Offset: 0x001F2FC3
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (itemData != null)
			{
				NKCDeckViewSide nkcdeckViewSide = this.m_NKCDeckViewSide;
				if (nkcdeckViewSide == null)
				{
					return;
				}
				nkcdeckViewSide.UpdateCostUI(itemData);
			}
		}

		// Token: 0x0600634E RID: 25422 RVA: 0x001F4DDC File Offset: 0x001F2FDC
		public override void OnEquipChange(NKMUserData.eChangeNotifyType eType, long equipUID, NKMEquipItemData equipItem)
		{
			if (eType == NKMUserData.eChangeNotifyType.Update)
			{
				NKMUnitData unitData = this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().GetUnitData();
				if (unitData == null)
				{
					return;
				}
				if (unitData.GetEquipItemWeaponUid() == equipUID || unitData.GetEquipItemDefenceUid() == equipUID || unitData.GetEquipItemAccessoryUid() == equipUID || unitData.GetEquipItemAccessory2Uid() == equipUID)
				{
					this.SelectCurrentDeck();
				}
			}
		}

		// Token: 0x0600634F RID: 25423 RVA: 0x001F4E2C File Offset: 0x001F302C
		public void PlayLoadingAnim(string name)
		{
			this.m_NKCDeckViewSide.PlayLoadingAnim(name);
		}

		// Token: 0x06006350 RID: 25424 RVA: 0x001F4E3C File Offset: 0x001F303C
		public void OnUnitDragEnd(int oldIndex, int newIndex)
		{
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				long localUnitData = NKCLocalDeckDataManager.GetLocalUnitData((int)this.m_SelectDeckIndex.m_iIndex, oldIndex);
				bool unitFromUID = this.NKMArmyData.GetUnitFromUID(localUnitData) != null;
				long localUnitData2 = NKCLocalDeckDataManager.GetLocalUnitData((int)this.m_SelectDeckIndex.m_iIndex, newIndex);
				NKMUnitData unitFromUID2 = this.NKMArmyData.GetUnitFromUID(localUnitData2);
				if (!unitFromUID && unitFromUID2 == null)
				{
					this.SetUnitSlotData(this.m_SelectDeckIndex, oldIndex, true);
					this.SetUnitSlotData(this.m_SelectDeckIndex, newIndex, true);
					this.SelectDeckViewUnit(newIndex, false, false);
					return;
				}
				NKCLocalDeckDataManager.SwapSlotData((int)this.m_SelectDeckIndex.m_iIndex, oldIndex, newIndex);
				this.SetUnitSlotData(this.m_SelectDeckIndex, oldIndex, true);
				this.SetUnitSlotData(this.m_SelectDeckIndex, newIndex, true);
				this.SelectDeckViewUnit(newIndex, false, false);
				NKCSoundManager.PlaySound("FX_UI_DECK_UNIIT_SELECT", 1f, 0f, 0f, false, 0f, false, 0f);
				int localLeaderIndex = NKCLocalDeckDataManager.GetLocalLeaderIndex((int)this.m_SelectDeckIndex.m_iIndex);
				if (localLeaderIndex != -1)
				{
					this.SetLeader(this.m_SelectDeckIndex, localLeaderIndex, false);
				}
				return;
			}
			else
			{
				bool deckUnitByIndex = this.NKMArmyData.GetDeckUnitByIndex(this.m_SelectDeckIndex, oldIndex) != null;
				NKMUnitData deckUnitByIndex2 = this.NKMArmyData.GetDeckUnitByIndex(this.m_SelectDeckIndex, newIndex);
				if (!deckUnitByIndex && deckUnitByIndex2 == null)
				{
					this.SetUnitSlotData(this.m_SelectDeckIndex, oldIndex, true);
					this.SetUnitSlotData(this.m_SelectDeckIndex, newIndex, true);
					this.SelectDeckViewUnit(newIndex, false, false);
					return;
				}
				this.Send_NKMPacket_DECK_UNIT_SWAP_REQ(this.m_SelectDeckIndex, oldIndex, newIndex);
				return;
			}
		}

		// Token: 0x06006351 RID: 25425 RVA: 0x001F4FA5 File Offset: 0x001F31A5
		public void OnUnitClicked(int index)
		{
			this.SelectDeckViewUnit(index, this.m_eCurrentSelectListType == NKM_UNIT_TYPE.NUT_OPERATOR, true);
		}

		// Token: 0x06006352 RID: 25426 RVA: 0x001F4FB8 File Offset: 0x001F31B8
		private NKCDeckViewSideUnitIllust.eUnitChangePossible IsUnitChangePossible()
		{
			if (this.m_bDeckInMission)
			{
				return NKCDeckViewSideUnitIllust.eUnitChangePossible.WORLDMAP_MISSION;
			}
			if (this.m_bDeckInWarfareBatch)
			{
				return NKCDeckViewSideUnitIllust.eUnitChangePossible.WARFARE;
			}
			if (this.m_bDeckInDiveBatch)
			{
				return NKCDeckViewSideUnitIllust.eUnitChangePossible.DIVE;
			}
			return NKCDeckViewSideUnitIllust.eUnitChangePossible.OK;
		}

		// Token: 0x06006353 RID: 25427 RVA: 0x001F4FDC File Offset: 0x001F31DC
		public void OnSideMenuButtonConfirm()
		{
			if (this.m_ViewerOptions.dOnSideMenuButtonConfirm != null)
			{
				this.m_ViewerOptions.dOnSideMenuButtonConfirm(this.m_SelectDeckIndex);
				return;
			}
			if (this.m_ViewerOptions.dOnDeckSideButtonConfirmForMulti != null)
			{
				List<NKMDeckIndex> multiSelectedDeckIndexList = this.m_NKCDeckViewList.GetMultiSelectedDeckIndexList();
				this.m_ViewerOptions.dOnDeckSideButtonConfirmForMulti(multiSelectedDeckIndexList);
				return;
			}
			if (this.m_ViewerOptions.dOnDeckSideButtonConfirmForAsync != null)
			{
				this.m_ViewerOptions.dOnDeckSideButtonConfirmForAsync(this.m_SelectDeckIndex, this.m_AsyncOriginalDeckData);
			}
		}

		// Token: 0x06006354 RID: 25428 RVA: 0x001F5064 File Offset: 0x001F3264
		private bool CheckOperationMultiply(bool bMsg)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.OPERATION_MULTIPLY, 0, 0))
			{
				return false;
			}
			if (!string.IsNullOrEmpty(this.m_ViewerOptions.StageBattleStrID))
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(this.m_ViewerOptions.StageBattleStrID);
				if (nkmstageTempletV != null)
				{
					if (!nkmuserData.IsSuperUser())
					{
						if (nkmuserData.CheckDungeonClear(nkmstageTempletV.m_StageBattleStrID))
						{
							NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(nkmstageTempletV.m_StageBattleStrID);
							if (dungeonTempletBase == null)
							{
								return false;
							}
							NKMDungeonClearData dungeonClearData = nkmuserData.GetDungeonClearData(dungeonTempletBase.m_DungeonStrID);
							if (nkmstageTempletV.EpisodeCategory != EPISODE_CATEGORY.EC_DAILY && (dungeonClearData == null || !dungeonClearData.missionResult1 || !dungeonClearData.missionResult2))
							{
								if (bMsg)
								{
									NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_MULTIPLY_OPERATION_MEDAL_COND, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
								}
								return false;
							}
							if (dungeonTempletBase.m_RewardMultiplyMax <= 1)
							{
								return false;
							}
						}
						else if (nkmuserData.CheckWarfareClear(nkmstageTempletV.m_StageBattleStrID))
						{
							NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(nkmstageTempletV.m_StageBattleStrID);
							if (nkmwarfareTemplet == null)
							{
								return false;
							}
							NKMWarfareClearData warfareClearData = nkmuserData.GetWarfareClearData(nkmwarfareTemplet.m_WarfareID);
							if (warfareClearData == null || !warfareClearData.m_mission_result_1 || !warfareClearData.m_mission_result_2)
							{
								if (bMsg)
								{
									NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_MULTIPLY_OPERATION_MEDAL_COND, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
								}
								return false;
							}
							if (nkmwarfareTemplet.m_RewardMultiplyMax <= 1)
							{
								return false;
							}
						}
						else
						{
							if (nkmstageTempletV.EpisodeCategory == EPISODE_CATEGORY.EC_DAILY)
							{
								if (bMsg)
								{
									NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_CONTENTS_UNLOCK_CLEAR_STAGE, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
								}
								return false;
							}
							if (bMsg)
							{
								NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_MULTIPLY_OPERATION_MEDAL_COND, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
							}
							return false;
						}
					}
					if (nkmstageTempletV.EnterLimit > 0)
					{
						int statePlayCnt = nkmuserData.GetStatePlayCnt(nkmstageTempletV.Key, false, false, false);
						if (this.m_ViewerOptions.bUsableOperationSkip && nkmstageTempletV.EnterLimit - statePlayCnt <= 0)
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06006355 RID: 25429 RVA: 0x001F5202 File Offset: 0x001F3402
		public int GetCurrMultiplyRewardCount()
		{
			return this.m_NKCDeckViewSide.GetCurrMultiplyRewardCount();
		}

		// Token: 0x06006356 RID: 25430 RVA: 0x001F520F File Offset: 0x001F340F
		public bool GetOperationSkipState()
		{
			return this.m_NKCDeckViewSide.OperationSkip;
		}

		// Token: 0x06006357 RID: 25431 RVA: 0x001F521C File Offset: 0x001F341C
		public void CloseOperationSkip()
		{
			if (this.m_NKCDeckViewSide.OperationSkip)
			{
				this.m_NKCDeckViewSide.m_tglSkip.Select(false, false, false);
			}
		}

		// Token: 0x06006358 RID: 25432 RVA: 0x001F5240 File Offset: 0x001F3440
		private void OnClickCloseBtnOfDeckViewSide()
		{
			this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().ResetObj();
			if (this.m_NKCUIRaidRightSide != null)
			{
				this.m_NKCUIRaidRightSide.Open();
			}
			else if (this.m_NKCUIGuildCoopRaidRightSide != null)
			{
				this.m_NKCUIGuildCoopRaidRightSide.Open();
			}
			this.SelectDeckViewUnit(-1, false, false);
		}

		// Token: 0x06006359 RID: 25433 RVA: 0x001F529C File Offset: 0x001F349C
		private void OnClickRaidAttck(long raidUID, List<int> _Buffs, int reqItemID, int reqItemCount, bool bIsTryAssist)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			if (!nkmuserData.CheckPrice(reqItemCount, reqItemID))
			{
				NKCShopManager.OpenItemLackPopup(reqItemID, reqItemCount);
				return;
			}
			if (this.m_ViewerOptions.eDeckviewerMode != NKCUIDeckViewer.DeckViewerMode.GuildCoopBoss)
			{
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_RAID_READY)
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID_READY().SetLastDeckIndex(this.m_SelectDeckIndex);
				}
				NKCPacketSender.Send_NKMPacket_RAID_GAME_LOAD_REQ(raidUID, this.m_SelectDeckIndex.m_iIndex, _Buffs, bIsTryAssist);
				return;
			}
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(NKCGuildCoopManager.CanStartBoss(), true, null, -2147483648))
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_RAID_READY)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_RAID_READY().SetLastDeckIndex(this.m_SelectDeckIndex);
			}
			NKCPacketSender.Send_NKMPacket_GUILD_DUNGEON_BOSS_GAME_LOAD_REQ(NKCGuildCoopManager.m_cGuildRaidTemplet.GetStageId(), this.m_SelectDeckIndex.m_iIndex);
		}

		// Token: 0x0600635A RID: 25434 RVA: 0x001F5360 File Offset: 0x001F3560
		public void SetEnableUnlockEffect(NKCDeckListButton btn)
		{
			if (this.m_objSlotUnlockEffect != null && btn != null)
			{
				Transform transform = btn.transform;
				this.m_objSlotUnlockEffect.transform.SetParent(transform);
				this.m_objSlotUnlockEffect.transform.localPosition = Vector3.zero;
				this.m_objSlotUnlockEffect.transform.localScale = Vector3.one;
				NKCUtil.SetGameobjectActive(this.m_objSlotUnlockEffect, false);
				NKCUtil.SetGameobjectActive(this.m_objSlotUnlockEffect, true);
				NKCSoundManager.PlaySound("FX_UI_DECK_SLOT_OPEN", 1f, 0f, 0f, false, 0f, false, 0f);
			}
		}

		// Token: 0x0600635B RID: 25435 RVA: 0x001F540C File Offset: 0x001F360C
		private bool ContainEmptySlotCost(NKM_ERROR_CODE errorCode)
		{
			if (errorCode != NKM_ERROR_CODE.NEC_OK)
			{
				return false;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.DECKVIEW, 0, 0))
			{
				return false;
			}
			if (this.m_ViewerOptions.CostItemID != 2)
			{
				return false;
			}
			NKMDeckData deckData = this.NKMArmyData.GetDeckData(this.m_SelectDeckIndex);
			return deckData != null && deckData.m_listDeckUnitUID.Contains(0L);
		}

		// Token: 0x0600635C RID: 25436 RVA: 0x001F545E File Offset: 0x001F365E
		private void CloseDeckSelectList(bool bAnimate)
		{
			this.m_eCurrentSelectListType = NKM_UNIT_TYPE.NUT_INVALID;
			this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetShipOnlyMode(false);
			this.m_NKCDeckViewUnitSelectList.Close(bAnimate);
			base.UpdateUpsideMenu();
		}

		// Token: 0x0600635D RID: 25437 RVA: 0x001F548C File Offset: 0x001F368C
		private void OpenDeckSelectList(NKM_UNIT_TYPE eUnitType, long selectedUnitUID)
		{
			this.m_eCurrentSelectListType = eUnitType;
			if (this.m_NKCDeckViewUnitSelectList.IsOpen)
			{
				this.UpdateDeckSelectList(eUnitType, selectedUnitUID);
			}
			else
			{
				this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetShipOnlyMode(true);
				if (eUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
				{
					if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
					{
						this.m_NKCDeckViewUnitSelectList.Open(true, eUnitType, this.MakeLocalOperatorSortOptions(selectedUnitUID), this.m_ViewerOptions);
					}
					else
					{
						this.m_NKCDeckViewUnitSelectList.Open(true, eUnitType, this.MakeOperatorSortOptions(selectedUnitUID), this.m_ViewerOptions);
					}
				}
				else if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
				{
					this.m_NKCDeckViewUnitSelectList.Open(true, eUnitType, this.MakeLocalSortOptions(eUnitType, selectedUnitUID), this.m_ViewerOptions);
				}
				else
				{
					this.m_NKCDeckViewUnitSelectList.Open(true, eUnitType, this.MakeSortOptions(selectedUnitUID), this.m_ViewerOptions);
				}
			}
			base.UpdateUpsideMenu();
		}

		// Token: 0x0600635E RID: 25438 RVA: 0x001F5564 File Offset: 0x001F3764
		private void UpdateDeckSelectList(NKM_UNIT_TYPE eUnitType, long selectedUnitUID)
		{
			this.m_eCurrentSelectListType = eUnitType;
			if (this.m_eCurrentSelectListType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
				{
					this.m_NKCDeckViewUnitSelectList.UpdateLoopScrollList(eUnitType, this.MakeLocalOperatorSortOptions(selectedUnitUID));
					return;
				}
				this.m_NKCDeckViewUnitSelectList.UpdateLoopScrollList(eUnitType, this.MakeOperatorSortOptions(selectedUnitUID));
				return;
			}
			else
			{
				if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
				{
					this.m_NKCDeckViewUnitSelectList.UpdateLoopScrollList(eUnitType, this.MakeLocalSortOptions(eUnitType, selectedUnitUID));
					return;
				}
				this.m_NKCDeckViewUnitSelectList.UpdateLoopScrollList(eUnitType, this.MakeSortOptions(selectedUnitUID));
				return;
			}
		}

		// Token: 0x0600635F RID: 25439 RVA: 0x001F55F0 File Offset: 0x001F37F0
		private NKCUnitSortSystem.UnitListOptions MakeSortOptions(long selectedUnitUID)
		{
			NKCUnitSortSystem.UnitListOptions result = new NKCUnitSortSystem.UnitListOptions
			{
				eDeckType = this.m_SelectDeckIndex.m_eDeckType,
				setExcludeUnitID = null,
				setOnlyIncludeUnitID = null,
				setDuplicateUnitID = null,
				setExcludeUnitUID = null,
				bExcludeLockedUnit = false,
				bExcludeDeckedUnit = false,
				bIgnoreCityState = true,
				bIgnoreWorldMapLeader = true,
				setFilterOption = this.m_NKCDeckViewUnitSelectList.SortOptions.setFilterOption,
				lstSortOption = this.m_NKCDeckViewUnitSelectList.SortOptions.lstSortOption,
				bDescending = this.m_NKCDeckViewUnitSelectList.SortOptions.bDescending,
				bIncludeUndeckableUnit = false,
				bHideDeckedUnit = (this.m_SelectDeckIndex.m_eDeckType == NKM_DECK_TYPE.NDT_NORMAL),
				bPushBackUnselectable = true
			};
			result.MakeDuplicateUnitSet(this.m_SelectDeckIndex, selectedUnitUID, this.NKMArmyData);
			return result;
		}

		// Token: 0x06006360 RID: 25440 RVA: 0x001F56D8 File Offset: 0x001F38D8
		private NKCOperatorSortSystem.OperatorListOptions MakeOperatorSortOptions(long selectedUnitUID)
		{
			NKCOperatorSortSystem.OperatorListOptions result = new NKCOperatorSortSystem.OperatorListOptions
			{
				eDeckType = this.m_SelectDeckIndex.m_eDeckType,
				setExcludeOperatorID = null,
				setOnlyIncludeOperatorID = null,
				setDuplicateOperatorID = null,
				setExcludeOperatorUID = null,
				setFilterOption = this.m_NKCDeckViewUnitSelectList.SortOperatorOptions.setFilterOption,
				lstSortOption = this.m_NKCDeckViewUnitSelectList.SortOperatorOptions.lstSortOption
			};
			result.SetBuildOption(true, new BUILD_OPTIONS[]
			{
				BUILD_OPTIONS.PUSHBACK_UNSELECTABLE
			});
			result.MakeDuplicateSetFromAllDeck(this.m_SelectDeckIndex, selectedUnitUID, this.NKMArmyData);
			return result;
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x001F5778 File Offset: 0x001F3978
		private NKCUnitSortSystem.UnitListOptions MakeLocalSortOptions(NKM_UNIT_TYPE unitType, long selectedUnitUID)
		{
			NKCUnitSortSystem.UnitListOptions unitListOptions = new NKCUnitSortSystem.UnitListOptions
			{
				eDeckType = this.m_SelectDeckIndex.m_eDeckType,
				setExcludeUnitID = null,
				setOnlyIncludeUnitID = null,
				setDuplicateUnitID = null,
				setExcludeUnitUID = null,
				bExcludeLockedUnit = false,
				bExcludeDeckedUnit = false,
				bIgnoreCityState = true,
				bIgnoreWorldMapLeader = true,
				setFilterOption = this.m_NKCDeckViewUnitSelectList.SortOptions.setFilterOption,
				lstSortOption = this.m_NKCDeckViewUnitSelectList.SortOptions.lstSortOption,
				bDescending = this.m_NKCDeckViewUnitSelectList.SortOptions.bDescending,
				bIncludeUndeckableUnit = false,
				bHideDeckedUnit = (this.m_SelectDeckIndex.m_eDeckType == NKM_DECK_TYPE.NDT_NORMAL),
				bPushBackUnselectable = true
			};
			if (unitType != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				if (unitType != NKM_UNIT_TYPE.NUT_SHIP)
				{
					return unitListOptions;
				}
			}
			else
			{
				unitListOptions.setExcludeUnitUID = new HashSet<long>();
				unitListOptions.setDuplicateUnitID = new HashSet<int>();
				using (Dictionary<int, NKMEventDeckData>.Enumerator enumerator = NKCLocalDeckDataManager.GetAllLocalDeckData().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<int, NKMEventDeckData> keyValuePair = enumerator.Current;
						foreach (KeyValuePair<int, long> keyValuePair2 in keyValuePair.Value.m_dicUnit)
						{
							long value = keyValuePair2.Value;
							if (value != 0L && selectedUnitUID != value)
							{
								NKMUnitData unitFromUID = this.NKMArmyData.GetUnitFromUID(value);
								if (unitFromUID != null)
								{
									unitListOptions.setDuplicateUnitID.Add(unitFromUID.m_UnitID);
								}
							}
						}
					}
					return unitListOptions;
				}
			}
			unitListOptions.setExcludeUnitUID = new HashSet<long>();
			unitListOptions.setDuplicateUnitID = new HashSet<int>();
			foreach (KeyValuePair<int, NKMEventDeckData> keyValuePair3 in NKCLocalDeckDataManager.GetAllLocalDeckData())
			{
				long shipUID = keyValuePair3.Value.m_ShipUID;
				if (shipUID != 0L && selectedUnitUID != shipUID)
				{
					NKMUnitData shipFromUID = this.NKMArmyData.GetShipFromUID(shipUID);
					if (shipFromUID != null)
					{
						NKMUnitTempletBase shipTempletBase = NKMUnitManager.GetUnitTempletBase(shipFromUID.m_UnitID);
						if (shipTempletBase != null)
						{
							foreach (NKMUnitTempletBase nkmunitTempletBase in from e in NKMUnitTempletBase.Values
							where e.m_ShipGroupID == shipTempletBase.m_ShipGroupID
							select e)
							{
								unitListOptions.setDuplicateUnitID.Add(nkmunitTempletBase.m_UnitID);
							}
						}
					}
				}
			}
			return unitListOptions;
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x001F5A3C File Offset: 0x001F3C3C
		private NKCOperatorSortSystem.OperatorListOptions MakeLocalOperatorSortOptions(long selectedUnitUID)
		{
			NKCOperatorSortSystem.OperatorListOptions operatorListOptions = new NKCOperatorSortSystem.OperatorListOptions
			{
				eDeckType = this.m_SelectDeckIndex.m_eDeckType,
				setExcludeOperatorID = null,
				setOnlyIncludeOperatorID = null,
				setDuplicateOperatorID = null,
				setExcludeOperatorUID = null,
				setFilterOption = this.m_NKCDeckViewUnitSelectList.SortOperatorOptions.setFilterOption,
				lstSortOption = this.m_NKCDeckViewUnitSelectList.SortOperatorOptions.lstSortOption
			};
			Dictionary<int, NKMEventDeckData> allLocalDeckData = NKCLocalDeckDataManager.GetAllLocalDeckData();
			if (allLocalDeckData.Count > 0)
			{
				operatorListOptions.setExcludeOperatorUID = new HashSet<long>();
				operatorListOptions.setDuplicateOperatorID = new HashSet<int>();
				foreach (KeyValuePair<int, NKMEventDeckData> keyValuePair in allLocalDeckData)
				{
					long operatorUID = keyValuePair.Value.m_OperatorUID;
					if (operatorUID != 0L && selectedUnitUID != operatorUID)
					{
						NKMOperator operatorFromUId = this.NKMArmyData.GetOperatorFromUId(operatorUID);
						if (operatorFromUId != null)
						{
							operatorListOptions.setDuplicateOperatorID.Add(operatorFromUId.id);
						}
					}
				}
			}
			return operatorListOptions;
		}

		// Token: 0x06006363 RID: 25443 RVA: 0x001F5B58 File Offset: 0x001F3D58
		public void OnDeckUnitChangeClicked(NKMDeckIndex unitDeckIndex, long unitUID, NKM_UNIT_TYPE eType)
		{
			if (!NKMArmyData.IsAllowedSameUnitInMultipleDeck(this.m_SelectDeckIndex.m_eDeckType) && this.m_ViewerOptions.eDeckviewerMode != NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				NKMDeckData deckData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckData(unitDeckIndex);
				if (deckData != null)
				{
					if (deckData.GetState() == NKM_DECK_STATE.DECK_STATE_WARFARE)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_DECK_BATCH_FAIL_STATE_WARFARE, null, "");
						return;
					}
					if (deckData.GetState() == NKM_DECK_STATE.DECK_STATE_DIVE)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_DECK_BATCH_FAIL_STATE_DIVE, null, "");
						return;
					}
				}
			}
			switch (eType)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
				if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
				{
					long localUnitData = NKCLocalDeckDataManager.GetLocalUnitData((int)this.m_SelectDeckIndex.m_iIndex, this.m_SelectUnitSlotIndex);
					if (localUnitData > 0L && unitUID > 0L)
					{
						NKMUnitData unitFromUID = this.NKMArmyData.GetUnitFromUID(localUnitData);
						NKMDeckIndex selectDeckIndex = this.m_SelectDeckIndex;
						NKMUnitData unitFromUID2 = this.NKMArmyData.GetUnitFromUID(unitUID);
						NKCUIUnitSelectListChangePopup.Instance.Open(unitFromUID, selectDeckIndex, unitFromUID2, unitDeckIndex, delegate
						{
							this.RegisterUnitToLocalUnitDeck(unitUID, true);
						}, NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode), NKCUtil.CheckPossibleShowUpUnit(this.m_ViewerOptions.eDeckviewerMode));
					}
					else
					{
						this.RegisterUnitToLocalUnitDeck(unitUID, true);
					}
				}
				else
				{
					NKMDeckData deckData2 = this.NKMArmyData.GetDeckData(this.m_SelectDeckIndex);
					if (deckData2 == null)
					{
						Debug.LogError("현재 덱 정보를 찾지 못함!");
						this.Send_NKMPacket_DECK_UNIT_SET_REQ(this.m_SelectDeckIndex, this.m_SelectUnitSlotIndex, unitUID);
						return;
					}
					long num = deckData2.m_listDeckUnitUID[this.m_SelectUnitSlotIndex];
					if (num != 0L && unitUID != 0L)
					{
						NKMUnitData unitFromUID3 = this.NKMArmyData.GetUnitFromUID(num);
						NKMDeckIndex selectDeckIndex2 = this.m_SelectDeckIndex;
						NKMUnitData unitFromUID4 = this.NKMArmyData.GetUnitFromUID(unitUID);
						NKCUIUnitSelectListChangePopup.Instance.Open(unitFromUID3, selectDeckIndex2, unitFromUID4, unitDeckIndex, delegate
						{
							this.Send_NKMPacket_DECK_UNIT_SET_REQ(this.m_SelectDeckIndex, this.m_SelectUnitSlotIndex, unitUID);
						}, NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode), NKCUtil.CheckPossibleShowUpUnit(this.m_ViewerOptions.eDeckviewerMode));
					}
					else
					{
						this.Send_NKMPacket_DECK_UNIT_SET_REQ(this.m_SelectDeckIndex, this.m_SelectUnitSlotIndex, unitUID);
					}
				}
				break;
			case NKM_UNIT_TYPE.NUT_SHIP:
				if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
				{
					NKCLocalDeckDataManager.SetLocalShipUId((int)this.m_SelectDeckIndex.m_iIndex, unitUID, true);
					this.SetShipSlotData();
					long shipUId = NKCLocalDeckDataManager.GetShipUId((int)this.m_SelectDeckIndex.m_iIndex);
					this.SetDeckViewShipUnit(shipUId);
					this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_SHIP, shipUId);
					this.UpdateDeckReadyState();
					this.m_NKCDeckViewList.UpdateDeckState();
					this.UpdateDeckOperationPower();
				}
				else
				{
					this.Send_NKMPacket_DECK_SHIP_SET_REQ(this.m_SelectDeckIndex, unitUID);
				}
				break;
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
				{
					long operatorUId = NKCLocalDeckDataManager.GetOperatorUId((int)this.m_SelectDeckIndex.m_iIndex);
					if (operatorUId != 0L && unitUID != 0L)
					{
						NKCUIOperatorPopupChange.Instance.Open(this.m_SelectDeckIndex, operatorUId, unitUID, delegate
						{
							this.RegisterOperatorToLocalOperatorDeck(unitUID, true);
						});
					}
					else
					{
						this.RegisterOperatorToLocalOperatorDeck(unitUID, true);
					}
				}
				else
				{
					NKMDeckData deckData3 = this.NKMArmyData.GetDeckData(this.m_SelectDeckIndex);
					if (deckData3 == null)
					{
						Debug.LogError("현재 덱 정보를 찾지 못함!");
						this.Send_NKMPacket_DECK_UNIT_SET_REQ(this.m_SelectDeckIndex, this.m_SelectUnitSlotIndex, unitUID);
						return;
					}
					if (deckData3.m_OperatorUID != 0L && unitUID != 0L && NKCOperatorUtil.IsMyOperator(deckData3.m_OperatorUID) && NKCOperatorUtil.IsMyOperator(unitUID))
					{
						NKCUIOperatorPopupChange.Instance.Open(this.m_SelectDeckIndex, deckData3.m_OperatorUID, unitUID, delegate
						{
							this.Send_NKMPacket_DECK_OPERATOR_SET_REQ(this.m_SelectDeckIndex, unitUID);
						});
					}
					else
					{
						this.Send_NKMPacket_DECK_OPERATOR_SET_REQ(this.m_SelectDeckIndex, unitUID);
					}
				}
				break;
			}
			NKCUIDeckViewer.DeckViewerOption.OnChangeDeckUnit dOnChangeDeckUnit = this.m_ViewerOptions.dOnChangeDeckUnit;
			if (dOnChangeDeckUnit == null)
			{
				return;
			}
			dOnChangeDeckUnit(this.m_SelectDeckIndex, unitUID);
		}

		// Token: 0x06006364 RID: 25444 RVA: 0x001F5F4C File Offset: 0x001F414C
		private void OnUnitSelectListClose(NKM_UNIT_TYPE eType)
		{
			this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetShipOnlyMode(false);
			this.m_NKCDeckViewShip.SetSelectEffect(false);
			if (eType != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				if (eType == NKM_UNIT_TYPE.NUT_SHIP)
				{
					if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
					{
						long shipUId = NKCLocalDeckDataManager.GetShipUId((int)this.m_SelectDeckIndex.m_iIndex);
						this.SetDeckViewShipUnit(shipUId);
					}
					else
					{
						NKMUnitData deckShip = this.NKMArmyData.GetDeckShip(this.m_SelectDeckIndex);
						this.SetDeckViewShipUnit((deckShip != null) ? deckShip.m_UnitUID : 0L);
					}
				}
			}
			else
			{
				this.SelectDeckViewUnit(this.m_SelectUnitSlotIndex, false, false);
			}
			base.UpdateUpsideMenu();
			if ((this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareRaid || this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.GuildCoopBoss) && !this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().hasUnitData())
			{
				this.OnClickCloseBtnOfDeckViewSide();
			}
		}

		// Token: 0x06006365 RID: 25445 RVA: 0x001F601A File Offset: 0x001F421A
		public void UpdateUnitCount()
		{
			this.m_NKCDeckViewUnitSelectList.UpdateUnitCount();
		}

		// Token: 0x06006366 RID: 25446 RVA: 0x001F6028 File Offset: 0x001F4228
		private void RegisterUnitToLocalUnitDeck(long unitUId, bool prohibitSameUnitId)
		{
			NKCLocalDeckDataManager.SetLocalUnitUId((int)this.m_SelectDeckIndex.m_iIndex, this.m_SelectUnitSlotIndex, unitUId, prohibitSameUnitId);
			int num = NKCLocalDeckDataManager.GetLocalLeaderIndex((int)this.m_SelectDeckIndex.m_iIndex);
			num = NKCLocalDeckDataManager.SetLeaderIndex((int)this.m_SelectDeckIndex.m_iIndex, (num < 0) ? this.m_SelectUnitSlotIndex : num);
			int count = this.GetCurrDeckViewUnit().m_listNKCDeckViewUnitSlot.Count;
			for (int i = 0; i < count; i++)
			{
				this.SetUnitSlotData(this.m_SelectDeckIndex, i, i == this.m_SelectUnitSlotIndex);
			}
			if (unitUId > 0L && NKCLocalDeckDataManager.IsNextLocalSlotEmpty((int)this.m_SelectDeckIndex.m_iIndex, this.m_SelectUnitSlotIndex))
			{
				this.SelectDeckViewUnit(this.m_SelectUnitSlotIndex + 1, false, false);
				this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_NORMAL, 0L);
			}
			else
			{
				this.SelectDeckViewUnit(this.m_SelectUnitSlotIndex, false, false);
				this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_NORMAL, unitUId);
			}
			this.SetLeader(this.m_SelectDeckIndex, num, false);
			this.UpdateDeckReadyState();
			this.m_NKCDeckViewList.UpdateDeckState();
			this.UpdateDeckOperationPower();
			if (unitUId != 0L)
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				NKMUnitData nkmunitData = (nkmuserData != null) ? nkmuserData.m_ArmyData.GetUnitFromUID(unitUId) : null;
				if (nkmunitData != null)
				{
					NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_SQUAD_ENTER, nkmunitData, false, false);
				}
			}
		}

		// Token: 0x06006367 RID: 25447 RVA: 0x001F614C File Offset: 0x001F434C
		private void RegisterOperatorToLocalOperatorDeck(long operatorUId, bool prohibitSameUnitId)
		{
			NKCLocalDeckDataManager.SetLocalOperatorUId((int)this.m_SelectDeckIndex.m_iIndex, operatorUId, prohibitSameUnitId);
			this.OnSelectOperator(operatorUId);
			this.UpdateOperator(NKCOperatorUtil.GetOperatorData(operatorUId));
			this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_OPERATOR, operatorUId);
			if (operatorUId == 0L)
			{
				this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetUnitData(null, false, this.IsUnitChangePossible(), false);
				return;
			}
			NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_SQUAD_ENTER, NKCOperatorUtil.GetOperatorData(operatorUId), false, true);
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06006368 RID: 25448 RVA: 0x001F61B3 File Offset: 0x001F43B3
		private bool IsSupportMenu
		{
			get
			{
				return this.m_NKCDeckViewSupportList.IsOpen;
			}
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x001F61C0 File Offset: 0x001F43C0
		private void OpenSupList()
		{
			if (this.m_ViewerOptions.lstSupporter == null)
			{
				return;
			}
			this.CloseDeckSelectList(false);
			this.m_SelectDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE, -1);
			this.m_SelectUnitSlotIndex = -1;
			this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetUnitData(null, false, this.IsUnitChangePossible(), false);
			this.SelectDeckViewUnit(this.m_SelectUnitSlotIndex, false, false);
			this.m_NKCDeckViewList.SetDeckListButton(this.IsMultiSelect(), this.NKMArmyData, this.m_ViewerOptions, (int)this.m_SelectDeckIndex.m_iIndex, true);
			this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetShipOnlyMode(false);
			if (this.m_NKCDeckViewUnitSelectList.IsOpen)
			{
				this.m_eCurrentSelectListType = NKM_UNIT_TYPE.NUT_INVALID;
				this.m_NKCDeckViewUnitSelectList.Close(false);
			}
			this.m_NKCDeckViewSupportList.Open(this.m_ViewerOptions.lstSupporter, this.m_ViewerOptions.dIsValidSupport);
			this.UpdateSupporterUI();
			base.UpdateUpsideMenu();
		}

		// Token: 0x0600636A RID: 25450 RVA: 0x001F62A4 File Offset: 0x001F44A4
		private void CloseSupList()
		{
			if (!this.IsSupportMenu)
			{
				return;
			}
			this.m_NKCDeckViewSupportList.Close();
			this.SetUIBySupport(true);
			base.UpdateUpsideMenu();
		}

		// Token: 0x0600636B RID: 25451 RVA: 0x001F62C8 File Offset: 0x001F44C8
		private void UpdateSupporterUI()
		{
			WarfareSupporterListData selectedData = this.m_NKCDeckViewSupportList.GetSelectedData();
			this.SetUIBySupport(selectedData != null);
			this.m_NKCDeckViewSupportList.UpdateSelectUI();
			if (selectedData == null)
			{
				return;
			}
			this.SetTitleActive(true);
			NKCUtil.SetLabelText(this.m_lbSupporterName, selectedData.commonProfile.nickname);
			NKMUnitData nkmunitData = new NKMUnitData();
			nkmunitData.FillDataFromDummy(selectedData.deckData.Ship);
			nkmunitData.m_UnitID = selectedData.deckData.GetShipUnitId();
			this.m_NKCDeckViewShip.Open(nkmunitData, NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode));
			this.m_NKCDeckViewUnit.OpenDummy(selectedData.deckData.List, (int)selectedData.deckData.LeaderIndex);
			this.UpdateDeckOperationPower();
		}

		// Token: 0x0600636C RID: 25452 RVA: 0x001F6382 File Offset: 0x001F4582
		private void OnConfirmSuppoter(long selectedCode)
		{
			NKCUIDeckViewer.DeckViewerOption.OnSelectSupporter dOnSelectSupporter = this.m_ViewerOptions.dOnSelectSupporter;
			if (dOnSelectSupporter == null)
			{
				return;
			}
			dOnSelectSupporter(selectedCode);
		}

		// Token: 0x0600636D RID: 25453 RVA: 0x001F639A File Offset: 0x001F459A
		private void SetUIBySupport(bool bShow)
		{
			NKCUtil.SetGameobjectActive(this.m_NKCDeckViewUnit, bShow);
			NKCUtil.SetGameobjectActive(this.m_NKCDeckViewShip, bShow);
			NKCUtil.SetGameobjectActive(this.m_objNKCDeckViewTitle, bShow);
			NKCUtil.SetGameobjectActive(this.m_rtDeckOperationPowerRoot, bShow);
			NKCUtil.SetGameobjectActive(this.m_NKM_DECK_VIEW_OPERATOR, false);
		}

		// Token: 0x0600636E RID: 25454 RVA: 0x001F63D8 File Offset: 0x001F45D8
		private void UpdateOperator(NKMOperator operatorData = null)
		{
			if (NKCOperatorUtil.IsHide())
			{
				return;
			}
			if (!NKCOperatorUtil.IsActive())
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_DECK_VIEW_OPERATOR, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_DECK_VIEW_OPERATOR, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_OPERATOR_DECK_SLOT, operatorData != null);
			NKCUtil.SetGameobjectActive(this.m_EMPTY, operatorData == null);
			bool flag = NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode);
			this.m_NKM_UI_OPERATOR_DECK_SLOT.SetData(operatorData, flag);
			NKCUtil.SetGameobjectActive(this.m_OperatorSkillInfo, operatorData != null);
			NKCUtil.SetGameobjectActive(this.m_OperatorSkillCombo, operatorData != null);
			if (operatorData != null)
			{
				this.m_OperatorMainSkill.SetData(operatorData.mainSkill.id, (int)operatorData.mainSkill.level, NKCBanManager.IsBanOperator(operatorData.id, NKCBanManager.BAN_DATA_TYPE.FINAL) && flag);
				this.m_OperatorSubSkill.SetData(operatorData.subSkill.id, (int)operatorData.subSkill.level, NKCBanManager.IsBanOperator(operatorData.id, NKCBanManager.BAN_DATA_TYPE.FINAL) && flag);
				this.m_OperatorSkillCombo.SetData(operatorData.id);
			}
			this.UpdateDeckOperationPower();
		}

		// Token: 0x0600636F RID: 25455 RVA: 0x001F64E0 File Offset: 0x001F46E0
		private void OnSelectOperator(long operatorUID)
		{
			this.m_SelectUnitSlotIndex = -1;
			this.m_NKCDeckViewShip.SetSelectEffect(false);
			this.m_NKM_UI_OPERATOR_DECK_SLOT.SetSelectEffect(true);
			NKCDeckViewUnit currDeckViewUnit = this.GetCurrDeckViewUnit();
			if (currDeckViewUnit != null)
			{
				currDeckViewUnit.SelectDeckViewUnit(this.m_SelectUnitSlotIndex, false);
			}
			if (operatorUID != 0L && !NKCOperatorUtil.IsMyOperator(operatorUID))
			{
				return;
			}
			NKMOperator operatorData = NKCOperatorUtil.GetOperatorData(operatorUID);
			if (operatorData == null)
			{
				this.OpenDeckSelectList(NKM_UNIT_TYPE.NUT_OPERATOR, 0L);
			}
			else
			{
				this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_OPERATOR, (operatorData != null) ? operatorData.uid : 0L);
			}
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareRaid)
			{
				if (this.m_NKCUIRaidRightSide != null)
				{
					this.m_NKCUIRaidRightSide.Close();
				}
				this.m_NKCDeckViewSide.Open(this.m_ViewerOptions, false, false);
			}
			else if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.GuildCoopBoss)
			{
				if (this.m_NKCUIGuildCoopRaidRightSide != null)
				{
					this.m_NKCUIGuildCoopRaidRightSide.Close();
				}
				this.m_NKCDeckViewSide.Open(this.m_ViewerOptions, false, false);
			}
			this.m_NKCDeckViewSide.SetOperatorData(operatorData, true);
			this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetOperatorData(operatorData, this.IsUnitChangePossible(), true);
			this.EnableUnitView(true, false);
		}

		// Token: 0x06006370 RID: 25456 RVA: 0x001F6600 File Offset: 0x001F4800
		private void ClearDeck()
		{
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				List<bool> list = NKCLocalDeckDataManager.ClearDeck((int)this.m_SelectDeckIndex.m_iIndex);
				this.SetShipSlotData();
				this.UpdateOperator(null);
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					this.SetUnitSlotData(this.m_SelectDeckIndex, i, list[i]);
				}
				this.SelectDeckViewUnit(this.m_SelectUnitSlotIndex, true, false);
				this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_NORMAL, 0L);
				NKCLocalDeckDataManager.SetLeaderIndex((int)this.m_SelectDeckIndex.m_iIndex, -1);
				this.SetLeader(this.m_SelectDeckIndex, -1, false);
				this.UpdateDeckReadyState();
				this.m_NKCDeckViewList.UpdateDeckState();
				this.UpdateDeckOperationPower();
				NKCUIDeckViewer.DeckViewerOption.OnChangeDeckUnit dOnChangeDeckUnit = this.m_ViewerOptions.dOnChangeDeckUnit;
				if (dOnChangeDeckUnit == null)
				{
					return;
				}
				dOnChangeDeckUnit(this.m_SelectDeckIndex, 0L);
				return;
			}
			else
			{
				NKMDeckData deckData = this.NKMArmyData.GetDeckData(this.m_SelectDeckIndex);
				if (deckData == null)
				{
					return;
				}
				List<long> list2 = new List<long>();
				for (int j = 0; j < deckData.m_listDeckUnitUID.Count; j++)
				{
					list2.Add(0L);
				}
				this.Send_Packet_DECK_UNIT_AUTO_SET_REQ(this.m_SelectDeckIndex, list2, 0L, 0L);
				NKCUIDeckViewer.DeckViewerOption.OnChangeDeckUnit dOnChangeDeckUnit2 = this.m_ViewerOptions.dOnChangeDeckUnit;
				if (dOnChangeDeckUnit2 == null)
				{
					return;
				}
				dOnChangeDeckUnit2(this.m_SelectDeckIndex, 0L);
				return;
			}
		}

		// Token: 0x06006371 RID: 25457 RVA: 0x001F6744 File Offset: 0x001F4944
		private void AutoCompleteDeck()
		{
			NKMUserData userData = NKCScenManager.CurrentUserData();
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				List<long> localUnitDeckData = NKCLocalDeckDataManager.GetLocalUnitDeckData((int)this.m_SelectDeckIndex.m_iIndex);
				List<long> unitList = NKCUnitSortSystem.MakeLocalAutoCompleteDeck(userData, this.m_SelectDeckIndex, localUnitDeckData, true);
				List<bool> list = NKCLocalDeckDataManager.SetLocalAutoDeckUnitUId((int)this.m_SelectDeckIndex.m_iIndex, unitList);
				long shipUId = NKCLocalDeckDataManager.GetShipUId((int)this.m_SelectDeckIndex.m_iIndex);
				long shipUId2 = NKCUnitSortSystem.LocalAutoSelectShip(userData, this.m_SelectDeckIndex, shipUId, true);
				NKCLocalDeckDataManager.SetLocalAutoDeckShipUId((int)this.m_SelectDeckIndex.m_iIndex, shipUId2);
				this.SetShipSlotData();
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					this.SetUnitSlotData(this.m_SelectDeckIndex, i, list[i]);
				}
				this.SelectDeckViewUnit(this.m_SelectUnitSlotIndex, true, false);
				this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_NORMAL, 0L);
				int num = NKCLocalDeckDataManager.GetLocalLeaderIndex((int)this.m_SelectDeckIndex.m_iIndex);
				num = NKCLocalDeckDataManager.SetLeaderIndex((int)this.m_SelectDeckIndex.m_iIndex, (num < 0) ? 0 : num);
				this.SetLeader(this.m_SelectDeckIndex, num, false);
				this.UpdateDeckReadyState();
				this.m_NKCDeckViewList.UpdateDeckState();
				this.UpdateDeckOperationPower();
				return;
			}
			NKMDeckData deckData = this.NKMArmyData.GetDeckData(this.m_SelectDeckIndex);
			if (deckData == null)
			{
				return;
			}
			List<long> unitUIDList = NKCUnitSortSystem.MakeAutoCompleteDeck(userData, this.m_SelectDeckIndex, deckData);
			long shipUID = 0L;
			if (deckData.m_ShipUID == 0L)
			{
				NKCUnitSortSystem.UnitListOptions options = new NKCUnitSortSystem.UnitListOptions
				{
					eDeckType = this.m_SelectDeckIndex.m_eDeckType,
					setExcludeUnitID = null,
					setOnlyIncludeUnitID = null,
					setDuplicateUnitID = null,
					setExcludeUnitUID = null,
					bExcludeLockedUnit = false,
					bExcludeDeckedUnit = false,
					setFilterOption = null,
					lstSortOption = new List<NKCUnitSortSystem.eSortOption>
					{
						NKCUnitSortSystem.eSortOption.Power_High,
						NKCUnitSortSystem.eSortOption.UID_First
					},
					bDescending = true,
					bHideDeckedUnit = !NKMArmyData.IsAllowedSameUnitInMultipleDeck(this.m_SelectDeckIndex.m_eDeckType),
					bPushBackUnselectable = true,
					bIncludeUndeckableUnit = false
				};
				options.MakeDuplicateUnitSet(this.m_SelectDeckIndex, 0L, this.NKMArmyData);
				NKMUnitData nkmunitData = new NKCShipSort(userData, options).AutoSelect(null, null);
				if (nkmunitData != null)
				{
					shipUID = nkmunitData.m_UnitUID;
				}
			}
			else
			{
				shipUID = deckData.m_ShipUID;
			}
			this.Send_Packet_DECK_UNIT_AUTO_SET_REQ(this.m_SelectDeckIndex, unitUIDList, shipUID, deckData.m_OperatorUID);
		}

		// Token: 0x06006372 RID: 25458 RVA: 0x001F699C File Offset: 0x001F4B9C
		public void OnRecv(NKMPacket_DECK_UNLOCK_ACK cPacket)
		{
			NKMDeckIndex nkmdeckIndex = new NKMDeckIndex(cPacket.deckType, (int)(cPacket.unlockedDeckSize - 1));
			this.SelectDeck(nkmdeckIndex);
			this.SetEnableUnlockEffect(this.m_NKCDeckViewList.GetDeckListButton((int)nkmdeckIndex.m_iIndex));
			this.UpdateDeckReadyState();
			this.m_NKCDeckViewList.UpdateDeckState();
			this.UpdateDeckOperationPower();
		}

		// Token: 0x06006373 RID: 25459 RVA: 0x001F69F4 File Offset: 0x001F4BF4
		public void OnRecv(NKMPacket_DECK_UNIT_SET_ACK cPacket, bool bWasEmptySlot)
		{
			this.SetUnitSlotData(cPacket.deckIndex, (int)cPacket.slotIndex, true);
			this.SetUnitSlotData(cPacket.oldDeckIndex, (int)cPacket.oldSlotIndex, false);
			int num = -1;
			if (bWasEmptySlot)
			{
				num = this.FindNextEmptySlot((int)cPacket.slotIndex);
			}
			if (num >= 0)
			{
				this.SelectDeckViewUnit(num, true, false);
				this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_NORMAL, 0L);
			}
			else
			{
				this.SelectDeckViewUnit((int)cPacket.slotIndex, true, false);
				this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_NORMAL, cPacket.slotUnitUID);
			}
			this.SetLeader(cPacket.deckIndex, (int)cPacket.leaderSlotIndex, false);
			this.UpdateDeckReadyState();
			this.m_NKCDeckViewList.UpdateDeckState();
			this.UpdateDeckOperationPower();
			if (cPacket.slotUnitUID != 0L)
			{
				NKMUnitData deckUnitByIndex = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckUnitByIndex(cPacket.deckIndex, (int)cPacket.slotIndex);
				if (deckUnitByIndex != null)
				{
					NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_SQUAD_ENTER, deckUnitByIndex, false, false);
				}
			}
		}

		// Token: 0x06006374 RID: 25460 RVA: 0x001F6AD0 File Offset: 0x001F4CD0
		private int FindNextEmptySlot(int currentIndex)
		{
			NKMDeckData deckData = this.NKMArmyData.GetDeckData(this.m_SelectDeckIndex);
			if (deckData == null)
			{
				return -1;
			}
			for (int i = 1; i < deckData.m_listDeckUnitUID.Count; i++)
			{
				int num = currentIndex + i;
				if (num >= deckData.m_listDeckUnitUID.Count)
				{
					num -= deckData.m_listDeckUnitUID.Count;
				}
				if (deckData.m_listDeckUnitUID[num] == 0L)
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x06006375 RID: 25461 RVA: 0x001F6B3B File Offset: 0x001F4D3B
		public void OnRecv(NKMPacket_DECK_SHIP_SET_ACK cPacket)
		{
			this.SetShipSlotData();
			this.SetDeckViewShipUnit(cPacket.shipUID);
			this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_SHIP, cPacket.shipUID);
			this.UpdateDeckReadyState();
			this.m_NKCDeckViewList.UpdateDeckState();
			this.UpdateDeckOperationPower();
		}

		// Token: 0x06006376 RID: 25462 RVA: 0x001F6B74 File Offset: 0x001F4D74
		public void OnRecv(NKMPacket_DECK_UNIT_SWAP_ACK cPacket_DECK_UNIT_SWAP_ACK)
		{
			this.SetUnitSlotData(cPacket_DECK_UNIT_SWAP_ACK.deckIndex, (int)cPacket_DECK_UNIT_SWAP_ACK.slotIndexFrom, true);
			this.SetUnitSlotData(cPacket_DECK_UNIT_SWAP_ACK.deckIndex, (int)cPacket_DECK_UNIT_SWAP_ACK.slotIndexTo, true);
			this.SelectDeckViewUnit((int)cPacket_DECK_UNIT_SWAP_ACK.slotIndexTo, false, false);
			NKCSoundManager.PlaySound("FX_UI_DECK_UNIIT_SELECT", 1f, 0f, 0f, false, 0f, false, 0f);
			if (cPacket_DECK_UNIT_SWAP_ACK.leaderSlotIndex != -1)
			{
				this.SetLeader(cPacket_DECK_UNIT_SWAP_ACK.deckIndex, (int)cPacket_DECK_UNIT_SWAP_ACK.leaderSlotIndex, false);
			}
		}

		// Token: 0x06006377 RID: 25463 RVA: 0x001F6BF8 File Offset: 0x001F4DF8
		public void OnRecv(NKMPacket_DECK_UNIT_SET_LEADER_ACK cPacket_DECK_UNIT_SET_LEADER_ACK)
		{
			this.SetLeader(cPacket_DECK_UNIT_SET_LEADER_ACK.deckIndex, (int)cPacket_DECK_UNIT_SET_LEADER_ACK.leaderSlotIndex, true);
			NKMUnitData deckUnitByIndex = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckUnitByIndex(cPacket_DECK_UNIT_SET_LEADER_ACK.deckIndex, (int)cPacket_DECK_UNIT_SET_LEADER_ACK.leaderSlotIndex);
			if (deckUnitByIndex != null)
			{
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_SQUAD_LEADER, deckUnitByIndex, false, false);
			}
		}

		// Token: 0x06006378 RID: 25464 RVA: 0x001F6C48 File Offset: 0x001F4E48
		public void OnRecv(NKMPacket_DECK_UNIT_AUTO_SET_ACK sPacket, HashSet<int> setChangedIndex)
		{
			this.SetShipSlotData();
			int num = sPacket.deckData.m_listDeckUnitUID.Count;
			for (int i = 0; i < sPacket.deckData.m_listDeckUnitUID.Count; i++)
			{
				bool flag = setChangedIndex != null && setChangedIndex.Contains(i);
				this.SetUnitSlotData(sPacket.deckIndex, i, flag);
				if (flag && num > i)
				{
					num = i;
				}
			}
			this.SelectDeckViewUnit(this.m_SelectUnitSlotIndex, true, false);
			long selectedUnitUID = (this.m_SelectUnitSlotIndex < sPacket.deckData.m_listDeckUnitUID.Count) ? sPacket.deckData.m_listDeckUnitUID[this.m_SelectUnitSlotIndex] : 0L;
			this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_NORMAL, selectedUnitUID);
			this.UpdateOperator(NKCOperatorUtil.GetOperatorData(sPacket.deckData.m_OperatorUID));
			this.SetLeader(sPacket.deckIndex, (int)sPacket.deckData.m_LeaderIndex, false);
			this.UpdateDeckReadyState();
			this.m_NKCDeckViewList.UpdateDeckState();
			this.UpdateDeckOperationPower();
			if (setChangedIndex.Count > 0)
			{
				NKCUIDeckViewer.DeckViewerOption.OnChangeDeckUnit dOnChangeDeckUnit = this.m_ViewerOptions.dOnChangeDeckUnit;
				if (dOnChangeDeckUnit != null)
				{
					dOnChangeDeckUnit(sPacket.deckIndex, 0L);
				}
				NKMUnitData deckUnitByIndex = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetDeckUnitByIndex(sPacket.deckIndex, num);
				if (deckUnitByIndex != null)
				{
					NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_SQUAD_ENTER, deckUnitByIndex, false, false);
				}
			}
		}

		// Token: 0x06006379 RID: 25465 RVA: 0x001F6D90 File Offset: 0x001F4F90
		public void OnRecv(NKMPacket_DECK_OPERATOR_SET_ACK sPacket)
		{
			if (this.m_SelectDeckIndex == sPacket.deckIndex)
			{
				this.OnSelectOperator(sPacket.operatorUid);
				this.UpdateOperator(NKCOperatorUtil.GetOperatorData(sPacket.operatorUid));
				this.UpdateDeckSelectList(NKM_UNIT_TYPE.NUT_OPERATOR, sPacket.operatorUid);
				if (sPacket.operatorUid == 0L)
				{
					this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().SetUnitData(null, false, this.IsUnitChangePossible(), false);
					return;
				}
				NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_SQUAD_ENTER, NKCOperatorUtil.GetOperatorData(sPacket.operatorUid), false, true);
			}
		}

		// Token: 0x0600637A RID: 25466 RVA: 0x001F6E11 File Offset: 0x001F5011
		private void Send_NKMPacket_DECK_UNIT_SWAP_REQ(NKMDeckIndex deckIndex, int slotFrom, int slotTo)
		{
			if (this.m_ViewerOptions.bUseAsyncDeckSetting)
			{
				this.AsyncDeckUnitSwap(deckIndex, slotFrom, slotTo);
				return;
			}
			NKCPacketSender.Send_NKMPacket_DECK_UNIT_SWAP_REQ(deckIndex, slotFrom, slotTo);
		}

		// Token: 0x0600637B RID: 25467 RVA: 0x001F6E32 File Offset: 0x001F5032
		private void Send_NKMPacket_DECK_UNIT_SET_REQ(NKMDeckIndex deckIndex, int slotIndex, long unitUID)
		{
			if (this.m_ViewerOptions.bUseAsyncDeckSetting)
			{
				this.AsyncDeckUnitSet(deckIndex, slotIndex, unitUID);
				return;
			}
			NKCPacketSender.Send_NKMPacket_DECK_UNIT_SET_REQ(deckIndex, slotIndex, unitUID);
		}

		// Token: 0x0600637C RID: 25468 RVA: 0x001F6E53 File Offset: 0x001F5053
		private void Send_NKMPacket_DECK_SHIP_SET_REQ(NKMDeckIndex deckIndex, long shipUID)
		{
			if (this.m_ViewerOptions.bUseAsyncDeckSetting)
			{
				this.AsyncDeckShipSet(deckIndex, shipUID);
				return;
			}
			NKCPacketSender.Send_NKMPacket_DECK_SHIP_SET_REQ(deckIndex, shipUID);
		}

		// Token: 0x0600637D RID: 25469 RVA: 0x001F6E72 File Offset: 0x001F5072
		private void Send_Packet_DECK_UNIT_SET_LEADER_REQ(NKMDeckIndex deckIndex, sbyte leaderIndex)
		{
			if (this.m_ViewerOptions.bUseAsyncDeckSetting)
			{
				this.AsyncDeckUnitSetLeader(deckIndex, leaderIndex);
				return;
			}
			NKCPacketSender.Send_Packet_DECK_UNIT_SET_LEADER_REQ(deckIndex, leaderIndex);
		}

		// Token: 0x0600637E RID: 25470 RVA: 0x001F6E91 File Offset: 0x001F5091
		public void Send_Packet_DECK_UNIT_AUTO_SET_REQ(NKMDeckIndex deckIndex, List<long> unitUIDList, long shipUID, long operatorUID)
		{
			if (this.m_ViewerOptions.bUseAsyncDeckSetting)
			{
				this.AsyncDeckUnitAutoSet(deckIndex, unitUIDList, shipUID, operatorUID);
				return;
			}
			NKCPacketSender.Send_Packet_DECK_UNIT_AUTO_SET_REQ(deckIndex, unitUIDList, shipUID, operatorUID);
		}

		// Token: 0x0600637F RID: 25471 RVA: 0x001F6EB6 File Offset: 0x001F50B6
		public void Send_NKMPacket_DECK_OPERATOR_SET_REQ(NKMDeckIndex deckIndex, long operatorUID)
		{
			if (this.m_ViewerOptions.bUseAsyncDeckSetting)
			{
				this.AsyncDeckOperatorSet(deckIndex, operatorUID);
				return;
			}
			NKCPacketSender.Send_NKMPacket_DECK_OPERATOR_SET_REQ(deckIndex, operatorUID);
		}

		// Token: 0x06006380 RID: 25472 RVA: 0x001F6ED8 File Offset: 0x001F50D8
		private void AsyncDeckUnitSwap(NKMDeckIndex deckIndex, int slotFrom, int slotTo)
		{
			NKMDeckData deckData = this.NKMArmyData.GetDeckData(deckIndex);
			if (deckData == null)
			{
				Debug.LogError(string.Format("deckData가 없음.. 말이됨? - {0}, {1}", deckIndex.m_eDeckType.ToString(), deckIndex.m_iIndex));
				return;
			}
			if ((int)deckData.m_LeaderIndex == slotFrom)
			{
				deckData.m_LeaderIndex = (sbyte)slotTo;
			}
			else if ((int)deckData.m_LeaderIndex == slotTo)
			{
				deckData.m_LeaderIndex = (sbyte)slotFrom;
			}
			long num = 0L;
			NKMUnitData deckUnitByIndex = this.NKMArmyData.GetDeckUnitByIndex(deckIndex, slotFrom);
			if (deckUnitByIndex != null)
			{
				num = deckUnitByIndex.m_UnitUID;
			}
			long num2 = 0L;
			NKMUnitData deckUnitByIndex2 = this.NKMArmyData.GetDeckUnitByIndex(deckIndex, slotTo);
			if (deckUnitByIndex2 != null)
			{
				num2 = deckUnitByIndex2.m_UnitUID;
			}
			deckData.m_listDeckUnitUID[slotFrom] = num2;
			deckData.m_listDeckUnitUID[slotTo] = num;
			this.OnRecv(new NKMPacket_DECK_UNIT_SWAP_ACK
			{
				errorCode = NKM_ERROR_CODE.NEC_OK,
				deckIndex = deckIndex,
				leaderSlotIndex = deckData.m_LeaderIndex,
				slotIndexFrom = (byte)slotFrom,
				slotIndexTo = (byte)slotTo,
				slotUnitUIDFrom = num2,
				slotUnitUIDTo = num
			});
		}

		// Token: 0x06006381 RID: 25473 RVA: 0x001F6FE8 File Offset: 0x001F51E8
		private void AsyncDeckUnitSet(NKMDeckIndex deckIndex, int slotIndex, long unitUID)
		{
			long num = 0L;
			NKMUnitData deckUnitByIndex = this.NKMArmyData.GetDeckUnitByIndex(deckIndex, slotIndex);
			if (deckUnitByIndex != null)
			{
				num = deckUnitByIndex.m_UnitUID;
			}
			this.NKMArmyData.SetDeckUnitByIndex(deckIndex, (byte)slotIndex, unitUID);
			NKMDeckData deckData = this.NKMArmyData.GetDeckData(deckIndex);
			if (deckData == null)
			{
				Debug.LogError(string.Format("deckData가 없음.. 말이됨? - {0}, {1}", deckIndex.m_eDeckType.ToString(), deckIndex.m_iIndex));
				return;
			}
			if ((int)deckData.m_LeaderIndex == slotIndex && unitUID == 0L)
			{
				int num2 = deckData.m_listDeckUnitUID.FindIndex((long v) => v > 0L);
				deckData.m_LeaderIndex = (sbyte)num2;
			}
			else if (deckData.m_LeaderIndex == -1)
			{
				deckData.m_LeaderIndex = (sbyte)slotIndex;
			}
			this.OnRecv(new NKMPacket_DECK_UNIT_SET_ACK
			{
				errorCode = NKM_ERROR_CODE.NEC_OK,
				deckIndex = deckIndex,
				slotIndex = (byte)slotIndex,
				slotUnitUID = unitUID,
				oldDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE, 0),
				leaderSlotIndex = deckData.m_LeaderIndex
			}, deckUnitByIndex == null);
			if (NKCUIUnitSelectList.IsInstanceOpen)
			{
				if (num != 0L)
				{
					NKCUIUnitSelectList.Instance.ChangeUnitDeckIndex(num, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE));
				}
				NKCUIUnitSelectList.Instance.ChangeUnitDeckIndex(unitUID, deckIndex);
			}
		}

		// Token: 0x06006382 RID: 25474 RVA: 0x001F7120 File Offset: 0x001F5320
		private void AsyncDeckShipSet(NKMDeckIndex deckIndex, long shipUID)
		{
			long num = 0L;
			NKMUnitData deckShip = this.NKMArmyData.GetDeckShip(deckIndex);
			if (deckShip != null)
			{
				num = deckShip.m_UnitUID;
			}
			NKMDeckData deckData = this.NKMArmyData.GetDeckData(deckIndex);
			if (deckData == null)
			{
				Debug.LogError(string.Format("deckData가 없음.. 말이됨? - {0}, {1}", deckIndex.m_eDeckType.ToString(), deckIndex.m_iIndex));
				return;
			}
			deckData.m_ShipUID = shipUID;
			this.OnRecv(new NKMPacket_DECK_SHIP_SET_ACK
			{
				errorCode = NKM_ERROR_CODE.NEC_OK,
				deckIndex = deckIndex,
				shipUID = shipUID,
				oldDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE, 0)
			});
			if (NKCUIUnitSelectList.IsInstanceOpen)
			{
				if (num != 0L)
				{
					NKCUIUnitSelectList.Instance.ChangeUnitDeckIndex(num, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE));
				}
				NKCUIUnitSelectList.Instance.ChangeUnitDeckIndex(shipUID, deckIndex);
			}
		}

		// Token: 0x06006383 RID: 25475 RVA: 0x001F71E4 File Offset: 0x001F53E4
		private void AsyncDeckUnitSetLeader(NKMDeckIndex deckIndex, sbyte leaderIndex)
		{
			NKMDeckData deckData = this.NKMArmyData.GetDeckData(deckIndex);
			if (deckData == null)
			{
				Debug.LogError(string.Format("deckData가 없음.. 말이됨? - {0}, {1}", deckIndex.m_eDeckType.ToString(), deckIndex.m_iIndex));
				return;
			}
			deckData.m_LeaderIndex = leaderIndex;
			this.OnRecv(new NKMPacket_DECK_UNIT_SET_LEADER_ACK
			{
				errorCode = NKM_ERROR_CODE.NEC_OK,
				deckIndex = deckIndex,
				leaderSlotIndex = leaderIndex
			});
		}

		// Token: 0x06006384 RID: 25476 RVA: 0x001F7258 File Offset: 0x001F5458
		private void AsyncDeckUnitAutoSet(NKMDeckIndex deckIndex, List<long> unitUIDList, long shipUID, long operatorUID)
		{
			if (unitUIDList == null)
			{
				return;
			}
			NKMDeckData deckData = this.NKMArmyData.GetDeckData(deckIndex);
			if (deckData == null)
			{
				Debug.LogError(string.Format("deckData가 없음.. 말이됨? - {0}, {1}", deckIndex.m_eDeckType.ToString(), deckIndex.m_iIndex));
				return;
			}
			int num = -1;
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < deckData.m_listDeckUnitUID.Count; i++)
			{
				if (i < unitUIDList.Count)
				{
					if (unitUIDList[i] != deckData.m_listDeckUnitUID[i])
					{
						hashSet.Add(i);
						deckData.m_listDeckUnitUID[i] = unitUIDList[i];
					}
					if (unitUIDList[i] > 0L && num == -1)
					{
						num = i;
					}
				}
			}
			deckData.m_ShipUID = shipUID;
			deckData.m_OperatorUID = operatorUID;
			if (num == -1)
			{
				deckData.m_LeaderIndex = -1;
			}
			else if (deckData.m_LeaderIndex == -1)
			{
				deckData.m_LeaderIndex = 0;
			}
			this.OnRecv(new NKMPacket_DECK_UNIT_AUTO_SET_ACK
			{
				errorCode = NKM_ERROR_CODE.NEC_OK,
				deckIndex = deckIndex,
				deckData = deckData
			}, hashSet);
		}

		// Token: 0x06006385 RID: 25477 RVA: 0x001F736C File Offset: 0x001F556C
		private void AsyncDeckOperatorSet(NKMDeckIndex deckIndex, long unitUID)
		{
			long num = 0L;
			NKMOperator deckOperator = this.NKMArmyData.GetDeckOperator(deckIndex);
			if (deckOperator != null)
			{
				num = deckOperator.uid;
			}
			this.NKMArmyData.SetDeckOperatorByIndex(deckIndex.m_eDeckType, (int)deckIndex.m_iIndex, unitUID);
			if (this.NKMArmyData.GetDeckData(deckIndex) == null)
			{
				Debug.LogError(string.Format("deckData가 없음.. 말이됨? - {0}, {1}", deckIndex.m_eDeckType.ToString(), deckIndex.m_iIndex));
				return;
			}
			this.OnRecv(new NKMPacket_DECK_OPERATOR_SET_ACK
			{
				errorCode = NKM_ERROR_CODE.NEC_OK,
				deckIndex = deckIndex,
				operatorUid = unitUID,
				deckIndex = deckIndex
			});
			if (NKCUIUnitSelectList.IsInstanceOpen)
			{
				if (num != 0L)
				{
					NKCUIUnitSelectList.Instance.ChangeUnitDeckIndex(num, new NKMDeckIndex(NKM_DECK_TYPE.NDT_NONE));
				}
				NKCUIUnitSelectList.Instance.ChangeUnitDeckIndex(unitUID, deckIndex);
			}
		}

		// Token: 0x06006386 RID: 25478 RVA: 0x001F7436 File Offset: 0x001F5636
		public void OnRemoveUnit(NKMUnitData UnitData)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_MATCH_READY)
			{
				NKCUIUnitSelectList.CheckInstanceAndClose();
			}
			this.Send_NKMPacket_DECK_UNIT_SET_REQ(this.m_SelectDeckIndex, this.m_SelectUnitSlotIndex, 0L);
		}

		// Token: 0x06006387 RID: 25479 RVA: 0x001F7460 File Offset: 0x001F5660
		public void OnLeaderChange(NKMUnitData UnitData)
		{
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				int leaderIndex = NKCLocalDeckDataManager.SetLeaderIndex((int)this.m_SelectDeckIndex.m_iIndex, this.m_SelectUnitSlotIndex);
				this.SetLeader(this.m_SelectDeckIndex, leaderIndex, true);
				long localUnitData = NKCLocalDeckDataManager.GetLocalUnitData((int)this.m_SelectDeckIndex.m_iIndex, this.m_SelectUnitSlotIndex);
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				NKMUnitData nkmunitData = (nkmuserData != null) ? nkmuserData.m_ArmyData.GetUnitFromUID(localUnitData) : null;
				if (nkmunitData != null)
				{
					NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_SQUAD_LEADER, nkmunitData, false, false);
				}
				return;
			}
			this.Send_Packet_DECK_UNIT_SET_LEADER_REQ(this.m_SelectDeckIndex, (sbyte)this.m_SelectUnitSlotIndex);
		}

		// Token: 0x06006388 RID: 25480 RVA: 0x001F74F4 File Offset: 0x001F56F4
		private void OnBtnEnemyList()
		{
			if (NKCUIDeckViewer.IsDungeonAtkReadyScen(this.m_ViewerOptions.eDeckviewerMode))
			{
				NKC_SCEN_DUNGEON_ATK_READY scen_DUNGEON_ATK_READY = NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY();
				NKMStageTempletV2 stageTemplet = scen_DUNGEON_ATK_READY.GetStageTemplet();
				NKMDungeonTempletBase dungeonTempletBase = scen_DUNGEON_ATK_READY.GetDungeonTempletBase();
				if (stageTemplet != null)
				{
					NKCPopupEnemyList.Instance.Open(stageTemplet);
					return;
				}
				if (dungeonTempletBase != null)
				{
					NKCPopupEnemyList.Instance.Open(dungeonTempletBase);
					return;
				}
			}
			else
			{
				NKCUIDeckViewer.DeckViewerMode eDeckviewerMode = this.m_ViewerOptions.eDeckviewerMode;
				if (eDeckviewerMode - NKCUIDeckViewer.DeckViewerMode.WarfareBatch <= 1)
				{
					NKMWarfareTemplet cNKMWarfareTemplet = NKMWarfareTemplet.Find(NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareStrID());
					NKCPopupEnemyList.Instance.Open(cNKMWarfareTemplet);
				}
			}
		}

		// Token: 0x06006389 RID: 25481 RVA: 0x001F757C File Offset: 0x001F577C
		public override void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData)
		{
			NKMUnitData unitData2;
			if (eUnitType != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				if (eUnitType != NKM_UNIT_TYPE.NUT_SHIP)
				{
					unitData2 = null;
				}
				else
				{
					unitData2 = this.NKMArmyData.GetShipFromUID(uid);
				}
			}
			else
			{
				unitData2 = this.NKMArmyData.GetUnitFromUID(uid);
			}
			switch (eEventType)
			{
			case NKMUserData.eChangeNotifyType.Add:
			case NKMUserData.eChangeNotifyType.Remove:
				this.m_NKCDeckViewUnitSelectList.InvalidateSortData(eUnitType);
				break;
			case NKMUserData.eChangeNotifyType.Update:
			{
				this.m_ViewerOptions.DeckIndex = this.m_SelectDeckIndex;
				NKCDeckViewUnit currDeckViewUnit = this.GetCurrDeckViewUnit();
				if (currDeckViewUnit != null)
				{
					currDeckViewUnit.UpdateUnit(unitData2, this.m_ViewerOptions);
				}
				this.m_NKCDeckViewSide.UpdateUnitData(unitData);
				this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().UpdateUnit(unitData2);
				this.m_NKCDeckViewUnitSelectList.UpdateSlot(uid, unitData);
				if (eUnitType == NKM_UNIT_TYPE.NUT_SHIP)
				{
					this.m_NKCDeckViewShip.UpdateShipSlotData(unitData, NKCUtil.CheckPossibleShowBan(this.m_ViewerOptions.eDeckviewerMode));
				}
				break;
			}
			}
			this.UpdateDeckOperationPower();
		}

		// Token: 0x0600638A RID: 25482 RVA: 0x001F7654 File Offset: 0x001F5854
		public override void OnOperatorUpdate(NKMUserData.eChangeNotifyType eEventType, long uid, NKMOperator operatorData)
		{
			switch (eEventType)
			{
			case NKMUserData.eChangeNotifyType.Add:
			case NKMUserData.eChangeNotifyType.Remove:
				this.m_NKCDeckViewUnitSelectList.InvalidateSortData(NKM_UNIT_TYPE.NUT_OPERATOR);
				break;
			case NKMUserData.eChangeNotifyType.Update:
				this.UpdateOperator(operatorData);
				this.m_NKCDeckViewSide.SetOperatorData(operatorData, true);
				this.m_NKCDeckViewSide.GetDeckViewSideUnitIllust().UpdateOperator(operatorData);
				this.m_NKM_UI_OPERATOR_DECK_SLOT.UpdateData(operatorData);
				this.m_NKCDeckViewUnitSelectList.UpdateSlot(uid, operatorData);
				break;
			}
			this.UpdateDeckOperationPower();
		}

		// Token: 0x0600638B RID: 25483 RVA: 0x001F76C7 File Offset: 0x001F58C7
		public override void OnDeckUpdate(NKMDeckIndex deckIndex, NKMDeckData deckData)
		{
			this.m_NKCDeckViewList.UpdateDeckState();
			this.SelectCurrentDeck();
		}

		// Token: 0x0600638C RID: 25484 RVA: 0x001F76DA File Offset: 0x001F58DA
		private void OnBtnDeckTypeGuide()
		{
			NKCUIPopUpGuide.Instance.Open("ARTICLE_SYSTEM_TEAM_SETTING", 0);
		}

		// Token: 0x0600638D RID: 25485 RVA: 0x001F76EC File Offset: 0x001F58EC
		private void UpdateDeckToggleUI()
		{
			NKCUtil.SetGameobjectActive(this.m_objDeckTypeTrim, false);
			if (this.m_ViewerOptions.eDeckviewerMode == NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck)
			{
				NKCUtil.SetGameobjectActive(this.m_objDeckType, false);
				if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_TRIM)
				{
					NKCUtil.SetGameobjectActive(this.m_objDeckTypeTrim, true);
					if (NKCUIPopupTrimDungeon.IsInstanceOpen)
					{
						NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(NKCUIPopupTrimDungeon.Instance.TrimId);
						NKMTrimPointTemplet nkmtrimPointTemplet = NKMTrimPointTemplet.Find(NKCUIPopupTrimDungeon.Instance.SelectedGroup, NKCUIPopupTrimDungeon.Instance.SelectedLevel);
						string msg = (nkmtrimTemplet != null) ? NKCStringTable.GetString(nkmtrimTemplet.TirmGroupName, false) : " - ";
						int num = (nkmtrimPointTemplet != null) ? nkmtrimPointTemplet.RecommendCombatPoint : 0;
						NKCUtil.SetLabelText(this.m_lbTrimName, msg);
						NKCUtil.SetLabelText(this.m_lbTrimLevel, NKCUIPopupTrimDungeon.Instance.SelectedLevel.ToString());
						NKCUtil.SetLabelText(this.m_lbRecommendedPower, num.ToString());
					}
				}
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objDeckType, false);
		}

		// Token: 0x0600638E RID: 25486 RVA: 0x001F77E1 File Offset: 0x001F59E1
		private void OnChangeDeckName()
		{
			if (this.m_ifDeckName != null && !this.m_ifDeckName.isFocused)
			{
				this.m_ifDeckName.Select();
				this.m_ifDeckName.ActivateInputField();
			}
		}

		// Token: 0x0600638F RID: 25487 RVA: 0x001F7814 File Offset: 0x001F5A14
		private void OnDeckNameValueChanged(string text)
		{
			this.m_ifDeckName.text = text.Replace("\r", "").Replace("\n", "");
		}

		// Token: 0x06006390 RID: 25488 RVA: 0x001F7840 File Offset: 0x001F5A40
		private void OnEndEditDeckName(string text)
		{
			NKCPacketSender.Send_NKMPacket_DECK_NAME_UPDATE_REQ(this.m_SelectDeckIndex, text);
		}

		// Token: 0x06006391 RID: 25489 RVA: 0x001F784E File Offset: 0x001F5A4E
		private void SetDeckNameInput(bool bEnable, string name = "")
		{
			if (this.m_ifDeckName != null)
			{
				this.m_ifDeckName.SetTextWithoutNotify(name);
				this.m_ifDeckName.enabled = bEnable;
				this.m_ifDeckName.interactable = bEnable;
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnDeckName, bEnable);
		}

		// Token: 0x06006392 RID: 25490 RVA: 0x001F7890 File Offset: 0x001F5A90
		public void UpdateDeckName(NKMDeckIndex deckIndex, string name)
		{
			if (this.m_SelectDeckIndex == deckIndex)
			{
				NKCUtil.SetLabelText(this.m_lbDeckNamePlaceholder, NKCUIDeckViewer.GetDeckDefaultName(this.m_SelectDeckIndex));
				if (this.m_ifDeckName != null)
				{
					this.m_ifDeckName.SetTextWithoutNotify(name);
				}
			}
			this.m_NKCDeckViewList.UpdateDeckListButton(NKCScenManager.CurrentUserData().m_ArmyData, deckIndex);
		}

		// Token: 0x06006393 RID: 25491 RVA: 0x001F78F1 File Offset: 0x001F5AF1
		public void ResetDeckName()
		{
			this.UpdateDeckName(this.m_SelectDeckIndex, NKCUIDeckViewer.GetDeckName(this.m_SelectDeckIndex));
		}

		// Token: 0x06006394 RID: 25492 RVA: 0x001F790C File Offset: 0x001F5B0C
		public static string GetDeckName(NKMDeckIndex deckIndex)
		{
			switch (deckIndex.m_eDeckType)
			{
			case NKM_DECK_TYPE.NDT_NORMAL:
			case NKM_DECK_TYPE.NDT_PVP:
			case NKM_DECK_TYPE.NDT_DAILY:
			case NKM_DECK_TYPE.NDT_RAID:
			case NKM_DECK_TYPE.NDT_DIVE:
			{
				NKMDeckData deckData = NKCScenManager.CurrentUserData().m_ArmyData.GetDeckData(deckIndex);
				if (deckData == null || string.IsNullOrEmpty(deckData.m_DeckName))
				{
					return NKCUIDeckViewer.GetDeckDefaultName(deckIndex);
				}
				return deckData.m_DeckName;
			}
			case NKM_DECK_TYPE.NDT_PVP_DEFENCE:
				return NKCStringTable.GetString("SI_PF_GAUNTLET_DEFENSEDECK", false);
			}
			return NKCUIDeckViewer.GetDeckDefaultName(deckIndex);
		}

		// Token: 0x06006395 RID: 25493 RVA: 0x001F798B File Offset: 0x001F5B8B
		public static string GetDeckDefaultName(NKMDeckIndex deckIndex)
		{
			if (deckIndex.m_eDeckType == NKM_DECK_TYPE.NDT_PVP_DEFENCE)
			{
				return NKCStringTable.GetString("SI_PF_GAUNTLET_DEFENSEDECK", false);
			}
			return string.Format(NKCUtilString.GET_STRING_SQUAD_ONE_PARAM, (int)(deckIndex.m_iIndex + 1));
		}

		// Token: 0x06006396 RID: 25494 RVA: 0x001F79B9 File Offset: 0x001F5BB9
		public NKCUIComButton GetShipSelectButton()
		{
			return this.m_NKCDeckViewShip.m_cbtnShip;
		}

		// Token: 0x06006397 RID: 25495 RVA: 0x001F79C6 File Offset: 0x001F5BC6
		public NKCUIUnitSelectListSlotBase SetTutorialSelectUnit(NKM_UNIT_TYPE type, int unitID)
		{
			this.OpenDeckSelectList(type, 0L);
			return this.m_NKCDeckViewUnitSelectList.GetAndScrollToTargetUnitSlot(unitID);
		}

		// Token: 0x06006398 RID: 25496 RVA: 0x001F79DD File Offset: 0x001F5BDD
		public NKCUIUnitSelectListSlotBase GetTutorialSelectSlotType(NKCDeckViewUnitSelectList.SlotType type)
		{
			this.OpenDeckSelectList(NKM_UNIT_TYPE.NUT_NORMAL, 0L);
			return this.m_NKCDeckViewUnitSelectList.GetAndScrollSlotBySlotType(type);
		}

		// Token: 0x06006399 RID: 25497 RVA: 0x001F79F4 File Offset: 0x001F5BF4
		private void CheckTutorial()
		{
			NKCTutorialManager.TutorialRequired(TutorialPoint.TeamSetting, true);
		}

		// Token: 0x0600639A RID: 25498 RVA: 0x001F79FF File Offset: 0x001F5BFF
		private void OnClickOperatorEmptySlot()
		{
			this.OpenDeckSelectList(NKM_UNIT_TYPE.NUT_OPERATOR, 0L);
		}

		// Token: 0x04004EC1 RID: 20161
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_deck_view";

		// Token: 0x04004EC2 RID: 20162
		private const string UI_ASSET_NAME = "NKM_UI_DECK_VIEW";

		// Token: 0x04004EC3 RID: 20163
		private static NKCUIDeckViewer m_Instance;

		// Token: 0x04004EC4 RID: 20164
		private NKCUIDeckViewer.DeckViewerOption m_ViewerOptions;

		// Token: 0x04004EC5 RID: 20165
		private bool m_bUnitViewEnable = true;

		// Token: 0x04004EC6 RID: 20166
		private NKMDeckIndex m_SelectDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, 0);

		// Token: 0x04004EC7 RID: 20167
		private int m_SelectUnitSlotIndex = -1;

		// Token: 0x04004EC8 RID: 20168
		private List<long> m_DeckUnitList = new List<long>();

		// Token: 0x04004EC9 RID: 20169
		private bool m_bDeckInMission;

		// Token: 0x04004ECA RID: 20170
		private bool m_bDeckInWarfareBatch;

		// Token: 0x04004ECB RID: 20171
		private bool m_bDeckInDiveBatch;

		// Token: 0x04004ECC RID: 20172
		private bool m_bUnitListActivated;

		// Token: 0x04004ECD RID: 20173
		private NKMDeckData m_AsyncOriginalDeckData = new NKMDeckData();

		// Token: 0x04004ECE RID: 20174
		private NKCUIComSafeArea m_NKM_DECK_VIEW_ARMY_NKCUIComSafeArea;

		// Token: 0x04004ECF RID: 20175
		private NKM_UNIT_TYPE m_eCurrentSelectListType;

		// Token: 0x04004ED0 RID: 20176
		public GameObject m_NKM_DECK_VIEW_BG;

		// Token: 0x04004ED1 RID: 20177
		public GameObject m_objDeckViewSquadTitle;

		// Token: 0x04004ED2 RID: 20178
		public InputField m_ifDeckName;

		// Token: 0x04004ED3 RID: 20179
		public Text m_lbDeckNamePlaceholder;

		// Token: 0x04004ED4 RID: 20180
		public NKCUIComStateButton m_csbtnDeckName;

		// Token: 0x04004ED5 RID: 20181
		public List<NKCUIDeckViewer.DeckTypeIconObject> m_lstDecktypeIcon;

		// Token: 0x04004ED6 RID: 20182
		public Text m_lbSquadNumber;

		// Token: 0x04004ED7 RID: 20183
		public Text m_lbSupporterName;

		// Token: 0x04004ED8 RID: 20184
		public NKCUIComStateButton m_csbtn_NKM_DECK_VIEW_HELP;

		// Token: 0x04004ED9 RID: 20185
		public GameObject m_objDeckViewArmy;

		// Token: 0x04004EDA RID: 20186
		public GameObject m_objDeckViewSideRaidParent;

		// Token: 0x04004EDB RID: 20187
		public NKCDeckViewList m_NKCDeckViewList;

		// Token: 0x04004EDC RID: 20188
		public NKCDeckViewShip m_NKCDeckViewShip;

		// Token: 0x04004EDD RID: 20189
		public Vector3 m_vShipNormalAnchoredPos = new Vector3(1200f, -487f);

		// Token: 0x04004EDE RID: 20190
		public Vector3 m_vShipRaidAnchoredPos = new Vector3(1215f, -368f);

		// Token: 0x04004EDF RID: 20191
		public NKCDeckViewUnit m_NKCDeckViewUnit;

		// Token: 0x04004EE0 RID: 20192
		public NKCUIDeckViewOperator m_NKCDeckViewOperator;

		// Token: 0x04004EE1 RID: 20193
		public NKCDeckViewSide m_NKCDeckViewSide;

		// Token: 0x04004EE2 RID: 20194
		public NKCDeckViewUnitSelectList m_NKCDeckViewUnitSelectList;

		// Token: 0x04004EE3 RID: 20195
		private NKCDeckViewUnit m_NKCDeckViewUnit_24;

		// Token: 0x04004EE4 RID: 20196
		private NKCUIRaidRightSide m_NKCUIRaidRightSide;

		// Token: 0x04004EE5 RID: 20197
		private NKCUIGuildCoopRaidRightSide m_NKCUIGuildCoopRaidRightSide;

		// Token: 0x04004EE6 RID: 20198
		public NKCDeckViewSupportList m_NKCDeckViewSupportList;

		// Token: 0x04004EE7 RID: 20199
		public GameObject m_objNKCDeckViewTitle;

		// Token: 0x04004EE8 RID: 20200
		[Header("스테이지 정보")]
		public GameObject m_NKM_DECK_VIEW_OPERATION_TITLE;

		// Token: 0x04004EE9 RID: 20201
		public Text m_lbOperationEpisode;

		// Token: 0x04004EEA RID: 20202
		public Text m_lbOperationTitle;

		// Token: 0x04004EEB RID: 20203
		public NKCUIComStateButton m_csbtnEnemyList;

		// Token: 0x04004EEC RID: 20204
		[Header("소대작전능력")]
		public RectTransform m_rtDeckOperationPowerRoot;

		// Token: 0x04004EED RID: 20205
		public Vector3 m_vDeckPowerNormalAnchoredPos = new Vector3(336.9f, 78f);

		// Token: 0x04004EEE RID: 20206
		public Vector3 m_vDeckPowerRaidAnchoredPos = new Vector3(336.9f, 78f);

		// Token: 0x04004EEF RID: 20207
		public Text m_lbDeckOperationPower;

		// Token: 0x04004EF0 RID: 20208
		public GameObject m_objBanNotice;

		// Token: 0x04004EF1 RID: 20209
		public GameObject m_objDescOrder;

		// Token: 0x04004EF2 RID: 20210
		public Text m_lbAdditionalInfoTitle;

		// Token: 0x04004EF3 RID: 20211
		public Text m_lbAdditionalInfoNumber;

		// Token: 0x04004EF4 RID: 20212
		public CanvasGroup m_CanvasGroup;

		// Token: 0x04004EF5 RID: 20213
		public GameObject m_objSlotUnlockEffect;

		// Token: 0x04004EF6 RID: 20214
		[Header("덱 타입 안내")]
		public GameObject m_objDeckType;

		// Token: 0x04004EF7 RID: 20215
		public Text m_lbDeckType;

		// Token: 0x04004EF8 RID: 20216
		public NKCUIComStateButton m_csbtnDeckTypeGuide;

		// Token: 0x04004EF9 RID: 20217
		[Header("덱 타입 트리밍")]
		public GameObject m_objDeckTypeTrim;

		// Token: 0x04004EFA RID: 20218
		public Text m_lbTrimLevel;

		// Token: 0x04004EFB RID: 20219
		public Text m_lbTrimName;

		// Token: 0x04004EFC RID: 20220
		public Text m_lbRecommendedPower;

		// Token: 0x04004EFD RID: 20221
		[Header("입장 제한")]
		public GameObject m_NKM_DECK_VIEW_SIDE_UNIT_OPERATION_EnterLimit;

		// Token: 0x04004EFE RID: 20222
		public Text m_EnterLimit_TEXT;

		// Token: 0x04004EFF RID: 20223
		public GameObject m_OPERATION_TITLE_BONUS;

		// Token: 0x04004F00 RID: 20224
		public Image m_BONUS_ICON;

		// Token: 0x04004F01 RID: 20225
		[Header("오퍼레이터")]
		public RectTransform m_NKM_DECK_VIEW_OPERATOR;

		// Token: 0x04004F02 RID: 20226
		public Vector2 m_vOperatorNormalAnchoredPos;

		// Token: 0x04004F03 RID: 20227
		public Vector2 m_vOperatorRaidAnchoredPos;

		// Token: 0x04004F04 RID: 20228
		public NKCUIOperatorDeckSlot m_NKM_UI_OPERATOR_DECK_SLOT;

		// Token: 0x04004F05 RID: 20229
		public GameObject m_EMPTY;

		// Token: 0x04004F06 RID: 20230
		public NKCUIComButton m_opereaterEmpty;

		// Token: 0x04004F07 RID: 20231
		public GameObject m_OperatorSkillInfo;

		// Token: 0x04004F08 RID: 20232
		public NKCUIOperatorSkill m_OperatorMainSkill;

		// Token: 0x04004F09 RID: 20233
		public NKCUIOperatorSkill m_OperatorSubSkill;

		// Token: 0x04004F0A RID: 20234
		public NKCUIOperatorTacticalSkillCombo m_OperatorSkillCombo;

		// Token: 0x0200162A RID: 5674
		public enum DeckViewerMode
		{
			// Token: 0x0400A377 RID: 41847
			DeckSetupOnly,
			// Token: 0x0400A378 RID: 41848
			PrepareBattle,
			// Token: 0x0400A379 RID: 41849
			PvPBattleFindTarget,
			// Token: 0x0400A37A RID: 41850
			AsyncPvPBattleStart,
			// Token: 0x0400A37B RID: 41851
			AsyncPvpDefenseDeck,
			// Token: 0x0400A37C RID: 41852
			PrepareDungeonBattle,
			// Token: 0x0400A37D RID: 41853
			WorldMapMissionDeckSelect,
			// Token: 0x0400A37E RID: 41854
			DeckSelect,
			// Token: 0x0400A37F RID: 41855
			WarfareBatch,
			// Token: 0x0400A380 RID: 41856
			WarfareBatch_Assault,
			// Token: 0x0400A381 RID: 41857
			WarfareRecovery,
			// Token: 0x0400A382 RID: 41858
			MainDeckSelect,
			// Token: 0x0400A383 RID: 41859
			PrepareDungeonBattle_Daily,
			// Token: 0x0400A384 RID: 41860
			PrepareDungeonBattleWithoutCost,
			// Token: 0x0400A385 RID: 41861
			PrepareDungeonBattle_CC,
			// Token: 0x0400A386 RID: 41862
			DeckMultiSelect,
			// Token: 0x0400A387 RID: 41863
			PrepareRaid,
			// Token: 0x0400A388 RID: 41864
			GuildCoopBoss,
			// Token: 0x0400A389 RID: 41865
			PrivatePvPReady,
			// Token: 0x0400A38A RID: 41866
			LeaguePvPMain,
			// Token: 0x0400A38B RID: 41867
			LeaguePvPGlobalBan,
			// Token: 0x0400A38C RID: 41868
			PrepareLocalDeck
		}

		// Token: 0x0200162B RID: 5675
		public struct DeckViewerOption
		{
			// Token: 0x0400A38D RID: 41869
			public string MenuName;

			// Token: 0x0400A38E RID: 41870
			public NKCUIDeckViewer.DeckViewerMode eDeckviewerMode;

			// Token: 0x0400A38F RID: 41871
			public NKCUIDeckViewer.DeckViewerOption.OnDeckSideButtonConfirm dOnSideMenuButtonConfirm;

			// Token: 0x0400A390 RID: 41872
			public NKCUIDeckViewer.DeckViewerOption.OnDeckSideButtonConfirmForMulti dOnDeckSideButtonConfirmForMulti;

			// Token: 0x0400A391 RID: 41873
			public NKCUIDeckViewer.DeckViewerOption.OnDeckSideButtonConfirmForAsync dOnDeckSideButtonConfirmForAsync;

			// Token: 0x0400A392 RID: 41874
			public int maxMultiSelectCount;

			// Token: 0x0400A393 RID: 41875
			public NKCUIDeckViewer.DeckViewerOption.CheckDeckButtonConfirm dCheckSideMenuButton;

			// Token: 0x0400A394 RID: 41876
			public NKCUIDeckViewer.DeckViewerOption.OnChangeDeckUnit dOnChangeDeckUnit;

			// Token: 0x0400A395 RID: 41877
			public NKCUIDeckViewer.DeckViewerOption.OnChangeDeckIndex dOnChangeDeckIndex;

			// Token: 0x0400A396 RID: 41878
			public NKMDeckIndex DeckIndex;

			// Token: 0x0400A397 RID: 41879
			public List<NKMDeckIndex> lstMultiSelectedDeckIndex;

			// Token: 0x0400A398 RID: 41880
			public NKCUIDeckViewer.DeckViewerOption.OnBackButton dOnBackButton;

			// Token: 0x0400A399 RID: 41881
			public bool SelectLeaderUnitOnOpen;

			// Token: 0x0400A39A RID: 41882
			public bool bEnableDefaultBackground;

			// Token: 0x0400A39B RID: 41883
			public bool bUpsideMenuHomeButton;

			// Token: 0x0400A39C RID: 41884
			public int WorldMapMissionID;

			// Token: 0x0400A39D RID: 41885
			public int WorldMapMissionCityID;

			// Token: 0x0400A39E RID: 41886
			public bool bOpenAlphaAni;

			// Token: 0x0400A39F RID: 41887
			public List<int> upsideMenuShowResourceList;

			// Token: 0x0400A3A0 RID: 41888
			public long raidUID;

			// Token: 0x0400A3A1 RID: 41889
			public int CostItemID;

			// Token: 0x0400A3A2 RID: 41890
			public int CostItemCount;

			// Token: 0x0400A3A3 RID: 41891
			public int OperationMultiplyMax;

			// Token: 0x0400A3A4 RID: 41892
			public bool bUsableOperationSkip;

			// Token: 0x0400A3A5 RID: 41893
			public List<int> ShowDeckIndexList;

			// Token: 0x0400A3A6 RID: 41894
			public string DeckListButtonStateText;

			// Token: 0x0400A3A7 RID: 41895
			public string StageBattleStrID;

			// Token: 0x0400A3A8 RID: 41896
			public bool bUsableSupporter;

			// Token: 0x0400A3A9 RID: 41897
			public List<WarfareSupporterListData> lstSupporter;

			// Token: 0x0400A3AA RID: 41898
			public NKCUIDeckViewer.DeckViewerOption.OnSelectSupporter dOnSelectSupporter;

			// Token: 0x0400A3AB RID: 41899
			public NKCUIDeckViewer.DeckViewerOption.IsValidSupport dIsValidSupport;

			// Token: 0x0400A3AC RID: 41900
			public bool bUseAsyncDeckSetting;

			// Token: 0x0400A3AD RID: 41901
			public bool bSlot24Extend;

			// Token: 0x0400A3AE RID: 41902
			public bool bNoUseLeaderBtn;

			// Token: 0x0400A3AF RID: 41903
			public Action dOnHide;

			// Token: 0x0400A3B0 RID: 41904
			public Action dOnUnhide;

			// Token: 0x02001A72 RID: 6770
			// (Invoke) Token: 0x0600BBF5 RID: 48117
			public delegate void OnBackButton();

			// Token: 0x02001A73 RID: 6771
			// (Invoke) Token: 0x0600BBF9 RID: 48121
			public delegate void OnDeckSideButtonConfirm(NKMDeckIndex selectedDeckIndex);

			// Token: 0x02001A74 RID: 6772
			// (Invoke) Token: 0x0600BBFD RID: 48125
			public delegate void OnDeckSideButtonConfirmForMulti(List<NKMDeckIndex> lstSelectedDeckIndex);

			// Token: 0x02001A75 RID: 6773
			// (Invoke) Token: 0x0600BC01 RID: 48129
			public delegate void OnDeckSideButtonConfirmForAsync(NKMDeckIndex selectedDeckIndex, NKMDeckData originalDeck);

			// Token: 0x02001A76 RID: 6774
			// (Invoke) Token: 0x0600BC05 RID: 48133
			public delegate void OnDeckSideButtonConfirmForLeague(int selectedIndex);

			// Token: 0x02001A77 RID: 6775
			// (Invoke) Token: 0x0600BC09 RID: 48137
			public delegate NKM_ERROR_CODE CheckDeckButtonConfirm(NKMDeckIndex selectedDeckIndex);

			// Token: 0x02001A78 RID: 6776
			// (Invoke) Token: 0x0600BC0D RID: 48141
			public delegate void OnSelectSupporter(long friendCode);

			// Token: 0x02001A79 RID: 6777
			// (Invoke) Token: 0x0600BC11 RID: 48145
			public delegate bool IsValidSupport(long friendCode);

			// Token: 0x02001A7A RID: 6778
			// (Invoke) Token: 0x0600BC15 RID: 48149
			public delegate void OnChangeDeckUnit(NKMDeckIndex selectedDeckIndex, long newlyAddedUnitUID);

			// Token: 0x02001A7B RID: 6779
			// (Invoke) Token: 0x0600BC19 RID: 48153
			public delegate void OnChangeDeckIndex(NKMDeckIndex selectedDeckIndex);
		}

		// Token: 0x0200162C RID: 5676
		[Serializable]
		public class DeckTypeIconObject
		{
			// Token: 0x0400A3B1 RID: 41905
			public NKM_DECK_TYPE eDeckType;

			// Token: 0x0400A3B2 RID: 41906
			public GameObject objSquadIcon;
		}
	}
}

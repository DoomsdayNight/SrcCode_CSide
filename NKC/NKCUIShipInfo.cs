using System;
using System.Collections.Generic;
using System.Text;
using ClientPacket.Common;
using ClientPacket.Unit;
using ClientPacket.User;
using Cs.Logging;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKC.UI.Component;
using NKC.UI.Guide;
using NKM;
using NKM.Templet;
using NKM.Templet.Recall;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009E1 RID: 2529
	public class NKCUIShipInfo : NKCUIBase
	{
		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06006C9B RID: 27803 RVA: 0x00237A98 File Offset: 0x00235C98
		public static NKCUIShipInfo Instance
		{
			get
			{
				if (NKCUIShipInfo.m_Instance == null)
				{
					NKCUIShipInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCUIShipInfo>("ab_ui_nkm_ui_ship_info", "NKM_UI_SHIP_INFO", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIShipInfo.CleanupInstance)).GetInstance<NKCUIShipInfo>();
					NKCUIShipInfo.m_Instance.Init();
				}
				return NKCUIShipInfo.m_Instance;
			}
		}

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06006C9C RID: 27804 RVA: 0x00237AE7 File Offset: 0x00235CE7
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIShipInfo.m_Instance != null && NKCUIShipInfo.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006C9D RID: 27805 RVA: 0x00237B02 File Offset: 0x00235D02
		public static void CheckInstanceAndClose()
		{
			if (NKCUIShipInfo.m_Instance != null && NKCUIShipInfo.m_Instance.IsOpen)
			{
				NKCUIShipInfo.m_Instance.Close();
			}
		}

		// Token: 0x06006C9E RID: 27806 RVA: 0x00237B27 File Offset: 0x00235D27
		private static void CleanupInstance()
		{
			NKCUIShipInfo.m_Instance = null;
		}

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06006C9F RID: 27807 RVA: 0x00237B2F File Offset: 0x00235D2F
		public override string GuideTempletID
		{
			get
			{
				if (this.m_curUIState == NKCUIShipInfo.State.INFO)
				{
					return "ARTICLE_SHIP_INFO";
				}
				return "ARTICLE_SHIP_LEVELUP";
			}
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06006CA0 RID: 27808 RVA: 0x00237B44 File Offset: 0x00235D44
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06006CA1 RID: 27809 RVA: 0x00237B47 File Offset: 0x00235D47
		public override string MenuName
		{
			get
			{
				if (this.m_curUIState == NKCUIShipInfo.State.INFO)
				{
					return NKCUtilString.GET_STRING_SHIP_INFO;
				}
				return NKCUtilString.GET_STRING_HANGAR_SHIPYARD;
			}
		}

		// Token: 0x06006CA2 RID: 27810 RVA: 0x00237B5C File Offset: 0x00235D5C
		public override void CloseInternal()
		{
			NKCUIPopupIllustView.CheckInstanceAndClose();
			if (null != this.m_UIUnitSelectList && NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_UNIT_LIST)
			{
				this.UnitSelectList.Close();
			}
			this.m_UIUnitSelectList = null;
			NKCUtil.SetGameobjectActive(this.m_objModuleUnlockFx, false);
			this.BannerCleanUp();
			base.gameObject.SetActive(false);
		}

		// Token: 0x06006CA3 RID: 27811 RVA: 0x00237BBA File Offset: 0x00235DBA
		public override void OnBackButton()
		{
			if (this.m_curUIState == NKCUIShipInfo.State.INFO && this.m_eShipViewState == NKCUIShipInfo.ShipViewState.IllustView)
			{
				this.SetState(NKCUIShipInfo.ShipViewState.Normal, true);
				return;
			}
			base.OnBackButton();
		}

		// Token: 0x06006CA4 RID: 27812 RVA: 0x00237BDC File Offset: 0x00235DDC
		public override void OnUnitUpdate(NKMUserData.eChangeNotifyType eEventType, NKM_UNIT_TYPE eUnitType, long uid, NKMUnitData unitData)
		{
			if (eEventType == NKMUserData.eChangeNotifyType.Update && uid == this.m_curShipData.m_UnitUID)
			{
				if (this.m_curShipData.m_LimitBreakLevel == 0 && unitData.m_LimitBreakLevel == 1)
				{
					NKCUtil.SetGameobjectActive(this.m_objModuleUnlockFx, false);
					NKCUtil.SetGameobjectActive(this.m_objModuleUnlockFx, true);
				}
				for (int i = 0; i < this.m_lstUnitData.Count; i++)
				{
					if (this.m_lstUnitData[i].m_UnitUID == unitData.m_UnitUID)
					{
						this.m_lstUnitData[i] = unitData;
						break;
					}
				}
				this.SetData(unitData);
				return;
			}
			if (eEventType == NKMUserData.eChangeNotifyType.Remove)
			{
				NKMUnitData nkmunitData = this.m_lstUnitData.Find((NKMUnitData x) => x.m_UnitUID == uid);
				if (nkmunitData != null)
				{
					this.m_lstUnitData.Remove(nkmunitData);
				}
				this.SetBannerUnit(this.m_curShipData.m_UnitUID);
			}
		}

		// Token: 0x06006CA5 RID: 27813 RVA: 0x00237CC5 File Offset: 0x00235EC5
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (this.m_curUIState == NKCUIShipInfo.State.REPAIR)
			{
				this.m_ShipInfoRepair.SetData(this.m_curShipData);
			}
		}

		// Token: 0x06006CA6 RID: 27814 RVA: 0x00237CE1 File Offset: 0x00235EE1
		public override void UnHide()
		{
			base.UnHide();
			this.CheckTabLock();
			this.TutorialCheck();
		}

		// Token: 0x06006CA7 RID: 27815 RVA: 0x00237CF5 File Offset: 0x00235EF5
		public override void Hide()
		{
			base.Hide();
			NKCUtil.SetGameobjectActive(this.m_objModuleUnlockFx, false);
			this.TempChangeFX.SetActive(false);
		}

		// Token: 0x06006CA8 RID: 27816 RVA: 0x00237D18 File Offset: 0x00235F18
		public void Init()
		{
			NKCUtil.SetBindFunction(this.m_cbtnChangeIllust, new UnityAction(this.OnClickChangeIllust));
			NKCUtil.SetBindFunction(this.m_cbtnPractice, new UnityAction(this.OnClickPractice));
			NKCUtil.SetBindFunction(this.m_GuideBtn, delegate()
			{
				NKCUIPopUpGuide.Instance.Open(this.m_GuideStrID, 0);
			});
			if (this.m_ctglLock != null)
			{
				this.m_ctglLock.OnValueChanged.RemoveAllListeners();
				this.m_ctglLock.OnValueChanged.AddListener(new UnityAction<bool>(this.OnLockToggle));
			}
			if (this.m_ctglInfo)
			{
				this.m_ctglInfo.OnValueChanged.RemoveAllListeners();
				this.m_ctglInfo.OnValueChanged.AddListener(delegate(bool b)
				{
					if (b)
					{
						this.ChangeState(NKCUIShipInfo.State.INFO);
					}
				});
			}
			if (this.m_ctglRepair)
			{
				this.m_ctglRepair.OnValueChanged.RemoveAllListeners();
				this.m_ctglRepair.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangeRepairTab));
				this.m_ctglRepair.m_bGetCallbackWhileLocked = true;
			}
			if (this.m_ctglModule)
			{
				this.m_ctglModule.OnValueChanged.RemoveAllListeners();
				this.m_ctglModule.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangeModuleTab));
				this.m_ctglModule.m_bGetCallbackWhileLocked = true;
			}
			if (this.m_btnRecall != null)
			{
				this.m_btnRecall.PointerClick.RemoveAllListeners();
				this.m_btnRecall.PointerClick.AddListener(new UnityAction(this.OnClickRecall));
			}
			if (this.m_tglMoveRange)
			{
				this.m_tglMoveRange.OnValueChanged.RemoveAllListeners();
				this.m_tglMoveRange.OnValueChanged.AddListener(new UnityAction<bool>(this.OnChangeMoveRange));
			}
			if (this.m_tglSocket_01)
			{
				this.m_tglSocket_01.OnValueChanged.RemoveAllListeners();
				this.m_tglSocket_01.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedSocket_01));
			}
			if (this.m_tglSocket_02)
			{
				this.m_tglSocket_02.OnValueChanged.RemoveAllListeners();
				this.m_tglSocket_02.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChangedSocket_02));
			}
			NKCUtil.SetBindFunction(this.m_ChangeShip, new UnityAction(this.OnSelectShip));
			if (this.m_ToolTipHP != null)
			{
				this.m_ToolTipHP.SetType(NKM_STAT_TYPE.NST_HP, false);
			}
			if (this.m_ToolTipATK != null)
			{
				this.m_ToolTipATK.SetType(NKM_STAT_TYPE.NST_ATK, false);
			}
			if (this.m_ToolTipDEF != null)
			{
				this.m_ToolTipDEF.SetType(NKM_STAT_TYPE.NST_DEF, false);
			}
			if (this.m_ToolTipCritical != null)
			{
				this.m_ToolTipCritical.SetType(NKM_STAT_TYPE.NST_CRITICAL, false);
			}
			if (this.m_ToolTipHit != null)
			{
				this.m_ToolTipHit.SetType(NKM_STAT_TYPE.NST_HIT, false);
			}
			if (this.m_ToolTipEvade != null)
			{
				this.m_ToolTipEvade.SetType(NKM_STAT_TYPE.NST_EVADE, false);
			}
			this.InitDragSelectablePanel();
			this.m_SkillPanel.Init(new UnityAction(this.OpenPopupSkillFullInfoForShip));
			this.m_ShipInfoRepair.Init(new UnityAction(this.OpenPopupSkillFullInfoForShip));
			this.m_ShipInfoCommandModule.Init(new UnityAction(this.OpenCommandModuleUI));
			base.gameObject.SetActive(false);
		}

		// Token: 0x06006CA9 RID: 27817 RVA: 0x0023805C File Offset: 0x0023625C
		public void Open(NKMUnitData shipData, NKMDeckIndex deckIndex, NKCUIUnitInfo.OpenOption openOption = null, NKC_SCEN_UNIT_LIST.eUIOpenReserve ReserveUI = NKC_SCEN_UNIT_LIST.eUIOpenReserve.Nothing)
		{
			this.SetShipData(shipData, deckIndex);
			base.gameObject.SetActive(true);
			this.SetData(shipData);
			this.SetState(NKCUIShipInfo.ShipViewState.Normal, false);
			if (openOption == null)
			{
				openOption = new NKCUIUnitInfo.OpenOption(new List<long>
				{
					shipData.m_UnitUID
				}, 0);
			}
			if (openOption.m_lstUnitData.Count <= 0 && openOption.m_UnitUIDList.Count <= 0)
			{
				Debug.Log("Can not found ship list info");
			}
			this.TempChangeFX.SetActive(false);
			NKCUtil.SetGameobjectActive(this.m_objModuleUnlockFx, false);
			this.m_ctglInfo.Select(true, true, false);
			this.m_lstUnitData = openOption.m_lstUnitData;
			this.SetBannerUnit(shipData.m_UnitUID);
			if (ReserveUI != NKC_SCEN_UNIT_LIST.eUIOpenReserve.ShipRepair)
			{
				if (ReserveUI != NKC_SCEN_UNIT_LIST.eUIOpenReserve.ShipModule)
				{
					this.m_ctglInfo.Select(true, true, true);
					this.ChangeState(NKCUIShipInfo.State.INFO);
				}
				else if (this.m_ctglModule.m_bLock)
				{
					this.m_ctglInfo.Select(true, true, true);
					this.ChangeState(NKCUIShipInfo.State.INFO);
				}
				else
				{
					this.m_ctglModule.Select(true, true, true);
					this.ChangeState(NKCUIShipInfo.State.MODULE);
				}
			}
			else
			{
				this.m_ctglRepair.Select(true, true, true);
				this.ChangeState(NKCUIShipInfo.State.REPAIR);
			}
			NKCUtil.SetGameobjectActive(this.m_ShipInfoMoveType, false);
			base.UIOpened(true);
		}

		// Token: 0x06006CAA RID: 27818 RVA: 0x00238198 File Offset: 0x00236398
		private void CheckTabLock()
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.HANGER_SHIPYARD, 0, 0))
			{
				this.m_ctglRepair.Lock(false);
			}
			else
			{
				this.m_ctglRepair.UnLock(false);
			}
			NKCUtil.SetGameobjectActive(this.m_objRepairLock, !NKCContentManager.IsContentsUnlocked(ContentsType.HANGER_SHIPYARD, 0, 0));
			if (!NKMShipManager.IsModuleUnlocked(this.m_curShipData))
			{
				this.m_ctglModule.Lock(false);
				return;
			}
			this.m_ctglModule.UnLock(false);
		}

		// Token: 0x06006CAB RID: 27819 RVA: 0x00238208 File Offset: 0x00236408
		private void TutorialCheck()
		{
			switch (this.m_curUIState)
			{
			case NKCUIShipInfo.State.INFO:
				if (this.m_curShipData.m_UnitLevel >= 100 && this.m_curShipData.m_LimitBreakLevel == 0 && NKMShipManager.GetShipLimitBreakTemplet(this.m_curShipData.m_UnitID, 1) != null)
				{
					NKCTutorialManager.TutorialRequired(TutorialPoint.ShipInfoMaxLevel, true);
					return;
				}
				NKCTutorialManager.TutorialRequired(TutorialPoint.ShipInfo, true);
				return;
			case NKCUIShipInfo.State.REPAIR:
				if (this.m_curShipData.m_UnitLevel >= 100 && this.m_curShipData.m_LimitBreakLevel == 0 && NKMShipManager.GetShipLimitBreakTemplet(this.m_curShipData.m_UnitID, 1) != null)
				{
					NKCTutorialManager.TutorialRequired(TutorialPoint.ShipLimitBreak, true);
					return;
				}
				NKCTutorialManager.TutorialRequired(TutorialPoint.ShipOverhaul, true);
				return;
			case NKCUIShipInfo.State.MODULE:
				if (NKMShipManager.IsModuleUnlocked(this.m_curShipData))
				{
					NKCTutorialManager.TutorialRequired(TutorialPoint.ShipModule, true);
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06006CAC RID: 27820 RVA: 0x002382CB File Offset: 0x002364CB
		private void OnClickPractice()
		{
			if (this.m_curShipData.IsSeized)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_SHIP_IS_SEIZED, null, "");
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_MOVE_TO_TEST_MODE, delegate()
			{
				NKCScenManager.GetScenManager().Get_SCEN_GAME().PlayPracticeGame(this.m_curShipData, NKM_SHORTCUT_TYPE.SHORTCUT_NONE, "");
			}, null, false);
		}

		// Token: 0x06006CAD RID: 27821 RVA: 0x00238308 File Offset: 0x00236508
		private void SetData(NKMUnitData shipData)
		{
			if (this.m_ShipInfoRepair.Status == NKCUIShipInfoRepair.RepairState.LevelUp && this.m_curShipData.m_UnitUID == shipData.m_UnitUID && this.m_curShipData.m_UnitLevel < shipData.m_UnitLevel)
			{
				this.TempChangeFX.SetActive(false);
				this.TempChangeFX.SetActive(true);
				NKCSoundManager.PlaySound("FX_UI_SHIP_LEVEL_UP", 1f, 0f, 0f, false, 0f, false, 0f);
			}
			this.m_curShipData = shipData;
			NKMDeckIndex shipDeckIndex = this.NKMArmyData.GetShipDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, this.m_curShipData.m_UnitUID);
			this.SetShipData(this.m_curShipData, shipDeckIndex);
			this.m_fDeltaTime = 0f;
			NKCUtil.SetGameobjectActive(this.m_objRecall, NKCRecallManager.IsRecallTargetUnit(this.m_curShipData, NKCSynchronizedTime.GetServerUTCTime(0.0)));
			if (this.m_objRecall.activeSelf)
			{
				this.SetRecallRemainTime();
			}
			this.m_ShipInfoRepair.SetData(this.m_curShipData);
			this.m_ShipInfoCommandModule.SetData(this.m_curShipData);
			this.CheckTabLock();
			if (this.m_curUIState == NKCUIShipInfo.State.REPAIR)
			{
				GameObject objRepairLock = this.m_objRepairLock;
				if (objRepairLock != null && objRepairLock.activeSelf)
				{
					goto IL_13B;
				}
			}
			if (this.m_curUIState != NKCUIShipInfo.State.MODULE || !this.m_ctglModule.m_bLock)
			{
				goto IL_151;
			}
			IL_13B:
			this.m_ctglInfo.Select(true, true, true);
			this.ChangeState(NKCUIShipInfo.State.INFO);
			IL_151:
			if (base.gameObject.activeSelf)
			{
				this.TutorialCheck();
			}
		}

		// Token: 0x06006CAE RID: 27822 RVA: 0x0023847C File Offset: 0x0023667C
		private void SetShipData(NKMUnitData shipData, NKMDeckIndex deckIndex)
		{
			this.m_curShipData = shipData;
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shipData.m_UnitID);
			this.m_uiSummary.SetShipData(shipData, unitTempletBase, deckIndex, false);
			NKCUtil.SetGameobjectActive(this.m_objModuleStepMini, shipData.ShipCommandModule.Count > 0);
			if (shipData.ShipCommandModule.Count > 0)
			{
				for (int i = 0; i < this.m_lstModuleStepMini.Count; i++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstModuleStepMini[i], i < shipData.ShipCommandModule.Count);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_tglMoveRange, unitTempletBase != null && this.m_curUIState == NKCUIShipInfo.State.INFO);
			if (unitTempletBase != null)
			{
				this.m_ShipInfoMoveType.SetData(unitTempletBase.m_NKM_UNIT_STYLE_TYPE);
			}
			NKMUnitManager.GetUnitStatTemplet(shipData.m_UnitID);
			NKCUtil.SetLabelText(this.m_lbHP, string.Format("{0:#;-#;0}", NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_HP, shipData, null)));
			NKCUtil.SetLabelText(this.m_lbAttack, string.Format("{0:#;-#;0}", NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_ATK, shipData, null)));
			NKCUtil.SetLabelText(this.m_lbDefence, string.Format("{0:#;-#;0}", NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_DEF, shipData, null)));
			NKCUtil.SetLabelText(this.m_lbCritical, string.Format("{0:#;-#;0}", NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_CRITICAL, shipData, null)));
			NKCUtil.SetLabelText(this.m_lbHit, string.Format("{0:#;-#;0}", NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_HIT, shipData, null)));
			NKCUtil.SetLabelText(this.m_lbEvade, string.Format("{0:#;-#;0}", NKMUnitStatManager.CalculateStat(NKM_STAT_TYPE.NST_EVADE, shipData, null)));
			NKCUtil.SetLabelText(this.m_lbPower, shipData.CalculateOperationPower(NKCScenManager.CurrentUserData().m_InventoryData, 0, null, null).ToString());
			NKCUtil.SetGameobjectActive(this.m_objEnabledModule, shipData.m_LimitBreakLevel > 0);
			NKMShipLimitBreakTemplet shipLimitBreakTemplet = NKMShipManager.GetShipLimitBreakTemplet(NKMShipManager.GetMaxLevelShipID(shipData.m_UnitID), 1);
			NKCUtil.SetGameobjectActive(this.m_objNoModule, shipLimitBreakTemplet == null);
			NKCUtil.SetGameobjectActive(this.m_objLockedModule, shipData.m_LimitBreakLevel == 0 && shipLimitBreakTemplet != null);
			if (this.m_objEnabledModule != null && this.m_objEnabledModule.activeSelf)
			{
				for (int j = 0; j < this.m_lstModuleStep.Count; j++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstModuleStep[j], j < (int)shipData.m_LimitBreakLevel);
				}
				NKCUtil.SetLabelText(this.m_lbModuleStep, string.Format(NKCUtilString.GET_STRING_SHIP_INFO_MODULE_STEP_TEXT, shipData.ShipCommandModule.Count));
				this.m_tglSocket_01.Select(true, true, true);
				this.ShowTotalSocketOptions(0);
			}
			NKCUIShipInfoSkillPanel skillPanel = this.m_SkillPanel;
			if (skillPanel != null)
			{
				skillPanel.SetData(unitTempletBase);
			}
			this.m_CurShipID = shipData.m_UnitID;
			this.m_CurShipUID = shipData.m_UnitUID;
			if (this.m_ctglLock != null)
			{
				this.m_ctglLock.Select(shipData.m_bLock, true, true);
			}
			NKCUtil.SetGameobjectActive(this.m_objSeized, shipData.IsSeized);
		}

		// Token: 0x06006CAF RID: 27823 RVA: 0x0023876B File Offset: 0x0023696B
		private void OnValueChangedSocket_01(bool bValue)
		{
			if (bValue)
			{
				this.ShowTotalSocketOptions(0);
			}
		}

		// Token: 0x06006CB0 RID: 27824 RVA: 0x00238777 File Offset: 0x00236977
		private void OnValueChangedSocket_02(bool bValue)
		{
			if (bValue)
			{
				this.ShowTotalSocketOptions(1);
			}
		}

		// Token: 0x06006CB1 RID: 27825 RVA: 0x00238784 File Offset: 0x00236984
		private void ShowTotalSocketOptions(int socketIndex)
		{
			List<NKMShipCmdSlot> list = new List<NKMShipCmdSlot>();
			for (int i = 0; i < this.m_curShipData.ShipCommandModule.Count; i++)
			{
				if (this.m_curShipData.ShipCommandModule[i] != null && this.m_curShipData.ShipCommandModule[i].slots != null)
				{
					for (int j = 0; j < this.m_curShipData.ShipCommandModule[i].slots.Length; j++)
					{
						if (socketIndex == j)
						{
							NKMShipCmdSlot nkmshipCmdSlot = this.m_curShipData.ShipCommandModule[i].slots[j];
							if (nkmshipCmdSlot != null && nkmshipCmdSlot.statType != NKM_STAT_TYPE.NST_RANDOM)
							{
								this.AddSameBuff(ref list, nkmshipCmdSlot);
							}
						}
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int k = 0; k < list.Count; k++)
			{
				stringBuilder.AppendLine(NKCUtilString.GetSlotOptionString(list[k], "[{0}] {1}"));
			}
			NKCUtil.SetLabelText(this.m_lbSocketOptions, stringBuilder.ToString());
			this.m_srSocketOptions.normalizedPosition = Vector2.zero;
		}

		// Token: 0x06006CB2 RID: 27826 RVA: 0x00238894 File Offset: 0x00236A94
		private void AddSameBuff(ref List<NKMShipCmdSlot> lstSocket, NKMShipCmdSlot targetSocket)
		{
			bool flag = false;
			for (int i = 0; i < lstSocket.Count; i++)
			{
				NKMShipCmdSlot nkmshipCmdSlot = lstSocket[i];
				if (nkmshipCmdSlot.statType == targetSocket.statType && nkmshipCmdSlot.targetStyleType.SetEquals(targetSocket.targetStyleType) && nkmshipCmdSlot.targetRoleType.SetEquals(targetSocket.targetRoleType))
				{
					flag = true;
					nkmshipCmdSlot.statValue += targetSocket.statValue;
					nkmshipCmdSlot.statFactor += targetSocket.statFactor;
					break;
				}
			}
			if (!flag)
			{
				NKMShipCmdSlot item = new NKMShipCmdSlot(targetSocket.targetStyleType, targetSocket.targetRoleType, targetSocket.statType, targetSocket.statValue, targetSocket.statFactor, targetSocket.isLock);
				lstSocket.Add(item);
			}
		}

		// Token: 0x06006CB3 RID: 27827 RVA: 0x00238954 File Offset: 0x00236B54
		private void SetState(NKCUIShipInfo.ShipViewState state, bool bAnimate = true)
		{
			this.m_eShipViewState = state;
			this.m_rtLeftRect.DOKill(false);
			this.m_rtRightRect.DOKill(false);
			this.m_rectSpineIllustPanel.DOKill(false);
			this.m_rectIllustRoot.DOKill(false);
			if (this.m_srScrollRect != null)
			{
				this.m_srScrollRect.enabled = (state == NKCUIShipInfo.ShipViewState.IllustView);
			}
			if (state != NKCUIShipInfo.ShipViewState.Normal)
			{
				if (state == NKCUIShipInfo.ShipViewState.IllustView)
				{
					this.m_tglMoveRange.Select(false, false, false);
					if (bAnimate)
					{
						this.m_rtLeftRect.DOAnchorMin(new Vector2(-1.5f, 0f), 0.4f, false).SetEase(Ease.OutCubic);
						this.m_rtLeftRect.DOAnchorMax(new Vector2(-0.5f, 1f), 0.4f, false).SetEase(Ease.OutCubic);
						this.m_rtRightRect.DOAnchorMin(new Vector2(1.5f, 0f), 0.4f, false).SetEase(Ease.OutCubic);
						this.m_rtRightRect.DOAnchorMax(new Vector2(2.5f, 1f), 0.4f, false).SetEase(Ease.OutCubic);
						this.m_rectIllustRoot.DOAnchorMin(this.m_vIllustRootAnchorMinIllustView, 0.4f, false).SetEase(Ease.OutCubic);
						this.m_rectIllustRoot.DOAnchorMax(this.m_vIllustRootAnchorMaxIllustView, 0.4f, false).SetEase(Ease.OutCubic);
						this.m_rmLock.Transit("Out", null);
						NKCUIManager.NKCUIUpsideMenu.Move(true, true);
						return;
					}
					this.m_rtLeftRect.anchorMin = new Vector2(-1.5f, 0f);
					this.m_rtLeftRect.anchorMax = new Vector2(-0.5f, 1f);
					this.m_rtRightRect.anchorMin = new Vector2(1.5f, 0f);
					this.m_rtRightRect.anchorMax = new Vector2(2.5f, 1f);
					this.m_rectIllustRoot.anchorMin = this.m_vIllustRootAnchorMinIllustView;
					this.m_rectIllustRoot.anchorMax = this.m_vIllustRootAnchorMaxIllustView;
					this.m_rmLock.Set("Out");
					NKCUIManager.NKCUIUpsideMenu.Move(true, false);
					return;
				}
			}
			else
			{
				if (bAnimate)
				{
					this.m_rtLeftRect.DOAnchorMin(Vector2.zero, 0.4f, false).SetEase(Ease.OutCubic);
					this.m_rtLeftRect.DOAnchorMax(Vector2.one, 0.4f, false).SetEase(Ease.OutCubic);
					this.m_rtRightRect.DOAnchorMin(Vector2.zero, 0.4f, false).SetEase(Ease.OutCubic);
					this.m_rtRightRect.DOAnchorMax(Vector2.one, 0.4f, false).SetEase(Ease.OutCubic);
					this.m_rectSpineIllustPanel.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutCubic);
					this.m_rectSpineIllustPanel.DOLocalMove(Vector3.zero, 0.4f, false).SetEase(Ease.OutCubic);
					this.m_rectIllustRoot.DOAnchorMin(this.m_vIllustRootAnchorMinNormal, 0.4f, false).SetEase(Ease.OutCubic);
					this.m_rectIllustRoot.DOAnchorMax(this.m_vIllustRootAnchorMaxNormal, 0.4f, false).SetEase(Ease.OutCubic);
					this.m_rmLock.Transit("Base", null);
					NKCUIManager.NKCUIUpsideMenu.Move(false, true);
					return;
				}
				this.m_rtLeftRect.anchorMin = Vector2.zero;
				this.m_rtLeftRect.anchorMax = Vector2.one;
				this.m_rtRightRect.anchorMin = Vector2.zero;
				this.m_rtRightRect.anchorMax = Vector2.one;
				this.m_rectSpineIllustPanel.localScale = Vector3.one;
				this.m_rectSpineIllustPanel.localPosition = Vector3.zero;
				this.m_rectIllustRoot.anchorMin = this.m_vIllustRootAnchorMinNormal;
				this.m_rectIllustRoot.anchorMax = this.m_vIllustRootAnchorMaxNormal;
				NKCUIManager.NKCUIUpsideMenu.Move(false, false);
			}
		}

		// Token: 0x06006CB4 RID: 27828 RVA: 0x00238D21 File Offset: 0x00236F21
		private void SetDeckNumber(NKMDeckIndex deckIndex)
		{
			this.m_uiSummary.SetDeckNumber(deckIndex);
		}

		// Token: 0x06006CB5 RID: 27829 RVA: 0x00238D2F File Offset: 0x00236F2F
		public void OnRecv(NKMPacket_DECK_SHIP_SET_ACK sPacket)
		{
			if (sPacket.shipUID == this.m_curShipData.m_UnitUID)
			{
				this.SetDeckNumber(sPacket.deckIndex);
			}
		}

		// Token: 0x06006CB6 RID: 27830 RVA: 0x00238D50 File Offset: 0x00236F50
		public void OnRecv(NKMPacket_SHIP_UPGRADE_ACK sPacket)
		{
			if (this.m_curUIState == NKCUIShipInfo.State.REPAIR)
			{
				this.m_ShipInfoRepair.OnRecv(sPacket);
			}
		}

		// Token: 0x06006CB7 RID: 27831 RVA: 0x00238D67 File Offset: 0x00236F67
		private void OnLockToggle(bool bValue)
		{
			if (bValue != this.m_curShipData.m_bLock)
			{
				NKCPacketSender.Send_NKMPacket_LOCK_UNIT_REQ(this.m_curShipData.m_UnitUID, !this.m_curShipData.m_bLock);
			}
		}

		// Token: 0x06006CB8 RID: 27832 RVA: 0x00238D98 File Offset: 0x00236F98
		private void OnClickRecall()
		{
			if (this.m_curShipData != null)
			{
				DateTime serverUTCTime = NKCSynchronizedTime.GetServerUTCTime(0.0);
				NKMRecallTemplet nkmrecallTemplet = NKMRecallTemplet.Find(NKCRecallManager.GetFirstLevelShipID(this.m_curShipData.m_UnitID), NKMTime.UTCtoLocal(serverUTCTime, 0));
				if (nkmrecallTemplet != null)
				{
					if (NKCScenManager.CurrentUserData().m_RecallHistoryData.ContainsKey(this.m_curShipData.m_UnitID))
					{
						RecallHistoryInfo recallHistoryInfo = NKCScenManager.CurrentUserData().m_RecallHistoryData[this.m_curShipData.m_UnitID];
						if (NKCRecallManager.IsValidTime(nkmrecallTemplet, recallHistoryInfo.lastUpdateDate))
						{
							NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_RECALL_ALREADY_USED, null, "");
							return;
						}
					}
					if (!NKCRecallManager.IsValidTime(nkmrecallTemplet, serverUTCTime))
					{
						NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_RECALL_PERIOD_EXPIRED, null, "");
						return;
					}
					if (!NKCRecallManager.IsValidRegTime(nkmrecallTemplet, this.m_curShipData.m_regDate))
					{
						NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_RECALL_INVALID_ACCQUIRE_TIME, null, "");
						return;
					}
					if (this.m_curShipData.m_bLock || NKCScenManager.CurrentUserData().m_ArmyData.IsUnitInAnyDeck(this.m_curShipData.m_UnitUID))
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_RECALL_ERROR_ALT_USING_UNIT, null, "");
						return;
					}
					NKCPopupRecall.Instance.Open(this.m_curShipData);
				}
			}
		}

		// Token: 0x06006CB9 RID: 27833 RVA: 0x00238EC8 File Offset: 0x002370C8
		private void SetRecallRemainTime()
		{
			NKMRecallTemplet nkmrecallTemplet = NKMRecallTemplet.Find(this.m_curShipData.m_UnitID, NKMTime.UTCtoLocal(NKCSynchronizedTime.GetServerUTCTime(0.0), 0));
			if (nkmrecallTemplet != null)
			{
				NKCUtil.SetLabelText(this.m_lbRecallTime, string.Format(NKCUtilString.GET_STRING_RECALL_DESC_END_DATE, NKCUtilString.GetRemainTimeStringEx(nkmrecallTemplet.IntervalTemplet.GetEndDateUtc())));
			}
		}

		// Token: 0x06006CBA RID: 27834 RVA: 0x00238F22 File Offset: 0x00237122
		private void OnChangeMoveRange(bool bValue)
		{
			NKCUtil.SetGameobjectActive(this.m_ShipInfoMoveType, bValue);
		}

		// Token: 0x06006CBB RID: 27835 RVA: 0x00238F30 File Offset: 0x00237130
		private void Update()
		{
			if (this.m_curUIState == NKCUIShipInfo.State.INFO && this.m_eShipViewState == NKCUIShipInfo.ShipViewState.IllustView)
			{
				if (NKCScenManager.GetScenManager().GetHasPinch())
				{
					this.m_srScrollRect.enabled = false;
					this.OnPinchZoom(NKCScenManager.GetScenManager().GetPinchCenter(), NKCScenManager.GetScenManager().GetPinchDeltaMagnitude());
				}
				else
				{
					this.m_srScrollRect.enabled = true;
				}
				float y = Input.mouseScrollDelta.y;
				if (y != 0f)
				{
					this.OnPinchZoom(Input.mousePosition, y);
				}
			}
			if (this.m_objRecall.activeSelf)
			{
				this.m_fDeltaTime += Time.deltaTime;
				if (this.m_fDeltaTime > 1f)
				{
					this.m_fDeltaTime -= 1f;
					this.SetRecallRemainTime();
				}
			}
		}

		// Token: 0x06006CBC RID: 27836 RVA: 0x00238FF6 File Offset: 0x002371F6
		private void OnClickChangeIllust()
		{
			NKCUIPopupIllustView.Instance.Open(this.m_curShipData);
		}

		// Token: 0x06006CBD RID: 27837 RVA: 0x00239008 File Offset: 0x00237208
		public void OnPinchZoom(Vector2 PinchCenter, float pinchMagnitude)
		{
			float num = this.m_rectSpineIllustPanel.localScale.x * Mathf.Pow(4f, pinchMagnitude);
			if (num < 0.5f)
			{
				num = 0.5f;
			}
			if (num > 2f)
			{
				num = 2f;
			}
			this.m_rectSpineIllustPanel.localScale = new Vector3(num, num, 1f);
		}

		// Token: 0x06006CBE RID: 27838 RVA: 0x00239068 File Offset: 0x00237268
		private void OpenPopupSkillFullInfoForShip()
		{
			if (this.m_CurShipID == 0)
			{
				return;
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_CurShipID);
			if (unitTempletBase == null)
			{
				return;
			}
			this.m_lstShipSkills.Clear();
			this.m_shipName = unitTempletBase.GetUnitName();
			for (int i = 0; i < 4; i++)
			{
				NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(unitTempletBase, i);
				if (shipSkillTempletByIndex != null)
				{
					this.m_lstShipSkills.Add(shipSkillTempletByIndex);
				}
			}
			NKCPopupSkillFullInfo.ShipInstance.OpenForShip(this.m_CurShipID, this.m_CurShipUID);
		}

		// Token: 0x06006CBF RID: 27839 RVA: 0x002390E0 File Offset: 0x002372E0
		private void InitDragSelectablePanel()
		{
			if (this.m_DragUnitView != null)
			{
				this.m_DragUnitView.Init(true, true);
				this.m_DragUnitView.dOnGetObject += this.MakeMainBannerListSlot;
				this.m_DragUnitView.dOnReturnObject += new NKCUIComDragSelectablePanel.OnReturnObject(this.ReturnMainBannerListSlot);
				this.m_DragUnitView.dOnProvideData += new NKCUIComDragSelectablePanel.OnProvideData(this.ProvideMainBannerListSlotData);
				this.m_DragUnitView.dOnIndexChangeListener += this.SelectCharacter;
				this.m_DragUnitView.dOnFocus += this.Focus;
				this.m_iBannerSlotCnt = 0;
			}
		}

		// Token: 0x06006CC0 RID: 27840 RVA: 0x00239188 File Offset: 0x00237388
		private void SetBannerUnit(long unitUID)
		{
			if (this.m_DragUnitView != null)
			{
				if (this.m_lstUnitData.Count <= 0)
				{
					NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
					if (nkmuserData != null)
					{
						NKMArmyData armyData = nkmuserData.m_ArmyData;
						if (armyData != null)
						{
							NKMUnitData shipFromUID = armyData.GetShipFromUID(unitUID);
							if (shipFromUID != null)
							{
								this.m_lstUnitData.Add(shipFromUID);
							}
						}
					}
				}
				for (int i = 0; i < this.m_lstUnitData.Count; i++)
				{
					if (this.m_lstUnitData[i].m_UnitUID == unitUID)
					{
						this.m_DragUnitView.TotalCount = this.m_lstUnitData.Count;
						this.m_DragUnitView.SetIndex(i);
						return;
					}
				}
			}
		}

		// Token: 0x06006CC1 RID: 27841 RVA: 0x0023922C File Offset: 0x0023742C
		private RectTransform MakeMainBannerListSlot()
		{
			GameObject gameObject = new GameObject(string.Format("Banner{0}", this.m_iBannerSlotCnt), new Type[]
			{
				typeof(RectTransform),
				typeof(LayoutElement)
			});
			LayoutElement component = gameObject.GetComponent<LayoutElement>();
			component.ignoreLayout = false;
			component.preferredWidth = this.m_DragUnitView.m_rtContentRect.GetWidth();
			component.preferredHeight = this.m_DragUnitView.m_rtContentRect.GetHeight();
			component.flexibleWidth = 2f;
			component.flexibleHeight = 2f;
			this.m_iBannerSlotCnt++;
			return gameObject.GetComponent<RectTransform>();
		}

		// Token: 0x06006CC2 RID: 27842 RVA: 0x002392D4 File Offset: 0x002374D4
		private void ProvideMainBannerListSlotData(Transform tr, int idx)
		{
			if (this.m_lstUnitData != null)
			{
				NKMUnitData nkmunitData = this.m_lstUnitData[idx];
				if (nkmunitData != null && tr != null)
				{
					string name = tr.gameObject.name;
					string s = name.Substring(name.Length - 1);
					int key = 0;
					int.TryParse(s, out key);
					if (!this.m_dicUnitIllust.ContainsKey(key))
					{
						NKCASUIUnitIllust nkcasuiunitIllust = NKCResourceUtility.OpenSpineIllust(nkmunitData, false);
						if (nkcasuiunitIllust != null)
						{
							RectTransform rectTransform = nkcasuiunitIllust.GetRectTransform();
							if (rectTransform != null)
							{
								rectTransform.localScale = new Vector3(-1f, rectTransform.localScale.y, rectTransform.localScale.z);
							}
							nkcasuiunitIllust.SetParent(tr.transform, false);
							nkcasuiunitIllust.SetAnchoredPosition(Vector2.zero);
							nkcasuiunitIllust.SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SHIP_IDLE, true, false, 0f);
						}
						this.m_dicUnitIllust.Add(key, nkcasuiunitIllust);
						return;
					}
					if (this.m_dicUnitIllust[key] != null)
					{
						this.m_dicUnitIllust[key].Unload();
						this.m_dicUnitIllust[key] = null;
						this.m_dicUnitIllust[key] = NKCResourceUtility.OpenSpineIllust(nkmunitData, false);
						if (this.m_dicUnitIllust[key] != null)
						{
							RectTransform rectTransform2 = this.m_dicUnitIllust[key].GetRectTransform();
							if (rectTransform2 != null)
							{
								rectTransform2.localScale = new Vector3(-1f, rectTransform2.localScale.y, rectTransform2.localScale.z);
							}
							this.m_dicUnitIllust[key].SetParent(tr.transform, false);
							this.m_dicUnitIllust[key].SetAnchoredPosition(Vector2.zero);
							this.m_dicUnitIllust[key].SetDefaultAnimation(NKCASUIUnitIllust.eAnimation.SHIP_IDLE, true, false, 0f);
						}
					}
				}
			}
		}

		// Token: 0x06006CC3 RID: 27843 RVA: 0x00239498 File Offset: 0x00237698
		private void ReturnMainBannerListSlot(Transform go)
		{
			NKCUtil.SetGameobjectActive(go, false);
			UnityEngine.Object.Destroy(go.gameObject);
		}

		// Token: 0x06006CC4 RID: 27844 RVA: 0x002394AC File Offset: 0x002376AC
		private void Focus(RectTransform rect, bool bFocus)
		{
			NKCUtil.SetGameobjectActive(rect.gameObject, bFocus);
		}

		// Token: 0x06006CC5 RID: 27845 RVA: 0x002394BC File Offset: 0x002376BC
		public void SelectCharacter(int idx)
		{
			if (this.m_lstUnitData.Count < idx || idx < 0)
			{
				Debug.LogWarning(string.Format("Error - Count : {0}, Index : {1}", this.m_lstUnitData.Count, idx));
				return;
			}
			NKMUnitData nkmunitData = this.m_lstUnitData[idx];
			if (nkmunitData != null)
			{
				this.ChangeUnit(nkmunitData);
			}
		}

		// Token: 0x06006CC6 RID: 27846 RVA: 0x00239518 File Offset: 0x00237718
		private void BannerCleanUp()
		{
			foreach (KeyValuePair<int, NKCASUIUnitIllust> keyValuePair in this.m_dicUnitIllust)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.Unload();
				}
			}
			this.m_dicUnitIllust.Clear();
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06006CC7 RID: 27847 RVA: 0x00239584 File Offset: 0x00237784
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

		// Token: 0x06006CC8 RID: 27848 RVA: 0x00239596 File Offset: 0x00237796
		private void ChangeUnit(NKMUnitData shipData)
		{
			if (shipData == this.m_curShipData)
			{
				return;
			}
			this.SetData(shipData);
		}

		// Token: 0x06006CC9 RID: 27849 RVA: 0x002395AC File Offset: 0x002377AC
		private void ChangeState(NKCUIShipInfo.State newState)
		{
			if (newState == NKCUIShipInfo.State.REPAIR && !NKCContentManager.IsContentsUnlocked(ContentsType.HANGER_SHIPYARD, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.HANGER_SHIPYARD, 0);
				return;
			}
			if ((newState == NKCUIShipInfo.State.REPAIR || newState == NKCUIShipInfo.State.MODULE) && this.m_eShipViewState == NKCUIShipInfo.ShipViewState.IllustView)
			{
				this.SetState(NKCUIShipInfo.ShipViewState.Normal, true);
			}
			if (newState == NKCUIShipInfo.State.REPAIR)
			{
				this.m_ShipInfoRepair.SetData(this.m_curShipData);
			}
			if (newState == NKCUIShipInfo.State.MODULE)
			{
				this.m_ShipInfoCommandModule.SetData(this.m_curShipData);
			}
			this.m_curUIState = newState;
			NKCUtil.SetGameobjectActive(this.m_objInfo, this.m_curUIState == NKCUIShipInfo.State.INFO);
			NKCUtil.SetGameobjectActive(this.m_objRepair, this.m_curUIState == NKCUIShipInfo.State.REPAIR);
			NKCUtil.SetGameobjectActive(this.m_objModule, this.m_curUIState == NKCUIShipInfo.State.MODULE);
			NKCUtil.SetGameobjectActive(this.m_cbtnChangeIllust, this.m_curUIState == NKCUIShipInfo.State.INFO);
			NKCUtil.SetGameobjectActive(this.m_ctglLock, this.m_curUIState == NKCUIShipInfo.State.INFO);
			NKCUtil.SetGameobjectActive(this.m_cbtnPractice, this.m_curUIState == NKCUIShipInfo.State.INFO);
			NKCUtil.SetGameobjectActive(this.m_ChangeShip, this.m_curUIState == NKCUIShipInfo.State.REPAIR || this.m_curUIState == NKCUIShipInfo.State.MODULE);
			NKCUtil.SetGameobjectActive(this.m_tglMoveRange, this.m_curUIState == NKCUIShipInfo.State.INFO);
			if (this.m_tglMoveRange.m_bSelect)
			{
				this.m_tglMoveRange.Select(false, false, false);
			}
			NKCUtil.SetImageSprite(this.m_imgBG, this.GetBackgroundSprite(this.m_curUIState), false);
			this.TutorialCheck();
			NKCUIManager.UpdateUpsideMenu();
		}

		// Token: 0x06006CCA RID: 27850 RVA: 0x00239708 File Offset: 0x00237908
		private Sprite GetBackgroundSprite(NKCUIShipInfo.State type)
		{
			string text;
			string text2;
			if (type == NKCUIShipInfo.State.INFO)
			{
				text = "AB_UI_BG_SPRITE";
				text2 = "BG";
			}
			else
			{
				text = "AB_UI_NUF_BASE_BG";
				text2 = "NKM_UI_BASE_HANGAR_BG";
			}
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(text, text2, false);
			if (orLoadAssetResource == null)
			{
				Debug.LogError("Error - NKCUIUnitInfo::GetBackgroundSprite - path:" + text + ", name:" + text2);
			}
			return orLoadAssetResource;
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06006CCB RID: 27851 RVA: 0x00239765 File Offset: 0x00237965
		private NKCUIUnitSelectList UnitSelectList
		{
			get
			{
				if (this.m_UIUnitSelectList == null)
				{
					this.m_UIUnitSelectList = NKCUIUnitSelectList.OpenNewInstance(true);
				}
				return this.m_UIUnitSelectList;
			}
		}

		// Token: 0x06006CCC RID: 27852 RVA: 0x00239788 File Offset: 0x00237988
		private void OnSelectShip()
		{
			NKCUIUnitSelectList.UnitSelectListOptions options = new NKCUIUnitSelectList.UnitSelectListOptions(NKM_UNIT_TYPE.NUT_SHIP, false, NKM_DECK_TYPE.NDT_NORMAL, NKCUIUnitSelectList.eUnitSelectListMode.Normal, true);
			options.bDescending = true;
			options.setFilterOption = new HashSet<NKCUnitSortSystem.eFilterOption>();
			options.lstSortOption = NKCUnitSortSystem.GetDefaultSortOptions(NKM_UNIT_TYPE.NUT_SHIP, false, false);
			options.bShowRemoveSlot = false;
			options.bShowHideDeckedUnitMenu = false;
			options.bHideDeckedUnit = false;
			options.bCanSelectUnitInMission = true;
			options.strUpsideMenuName = NKCUtilString.GET_STRING_SELECT_SHIP;
			options.strEmptyMessage = NKCUtilString.GET_STRING_NO_EXIST_SELECT_SHIP;
			options.setShipFilterCategory = NKCUnitSortSystem.setDefaultShipFilterCategory;
			options.setShipSortCategory = NKCUnitSortSystem.setDefaultShipSortCategory;
			options.m_bHideUnitCount = true;
			options.m_bUseFavorite = true;
			this.UnitSelectList.Open(options, null, new NKCUIUnitSelectList.OnUnitSortList(this.OnUnitSortList), null, null, null);
		}

		// Token: 0x06006CCD RID: 27853 RVA: 0x00239840 File Offset: 0x00237A40
		private void OnUnitSortList(long UID, List<NKMUnitData> unitDataList)
		{
			this.UnitSelectList.Close();
			this.BannerCleanUp();
			this.m_lstUnitData = unitDataList;
			if (this.m_lstUnitData.Count > 0)
			{
				this.m_DragUnitView.TotalCount = this.m_lstUnitData.Count;
				for (int i = 0; i < this.m_lstUnitData.Count; i++)
				{
					if (this.m_lstUnitData[i].m_UnitUID == UID)
					{
						this.m_DragUnitView.SetIndex(i);
						return;
					}
				}
			}
		}

		// Token: 0x06006CCE RID: 27854 RVA: 0x002398C0 File Offset: 0x00237AC0
		private void OnChangeRepairTab(bool bValue)
		{
			if (this.m_ctglRepair.m_bLock && !NKCContentManager.IsContentsUnlocked(ContentsType.HANGER_SHIPYARD, 0, 0))
			{
				NKCContentManager.ShowLockedMessagePopup(ContentsType.HANGER_SHIPYARD, 0);
				return;
			}
			if (bValue)
			{
				this.ChangeState(NKCUIShipInfo.State.REPAIR);
			}
		}

		// Token: 0x06006CCF RID: 27855 RVA: 0x002398F0 File Offset: 0x00237AF0
		private void OnChangeModuleTab(bool bValue)
		{
			if (!this.m_ctglModule.m_bLock)
			{
				if (bValue)
				{
					this.ChangeState(NKCUIShipInfo.State.MODULE);
				}
				return;
			}
			string text = "";
			if (!NKMOpenTagManager.IsOpened("SHIP_LIMITBREAK"))
			{
				text = NKCUtilString.GET_STRING_COMING_SOON_SYSTEM;
			}
			else if (!NKMOpenTagManager.IsOpened("SHIP_COMMANDMODULE"))
			{
				text = NKCUtilString.GET_STRING_COMING_SOON_SYSTEM;
			}
			else if (this.m_curShipData.m_LimitBreakLevel <= 0)
			{
				if (NKMShipManager.GetShipLimitBreakTemplet(this.m_curShipData.m_UnitID, 1) == null)
				{
					text = NKCUtilString.GET_STRING_SHIP_INFO_COMMAND_MODULE_NO_LIMITBREAK;
				}
				else
				{
					text = NKCUtilString.GET_STRING_SHIP_COMMAND_MODULE_NOT_LIMITBREAK;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(text, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
			}
			switch (this.m_curUIState)
			{
			case NKCUIShipInfo.State.INFO:
				this.m_ctglInfo.Select(true, true, true);
				return;
			case NKCUIShipInfo.State.REPAIR:
				this.m_ctglRepair.Select(true, true, true);
				return;
			case NKCUIShipInfo.State.MODULE:
				this.m_ctglInfo.Select(true, true, true);
				this.ChangeState(NKCUIShipInfo.State.INFO);
				return;
			default:
				return;
			}
		}

		// Token: 0x06006CD0 RID: 27856 RVA: 0x002399E8 File Offset: 0x00237BE8
		private void OpenCommandModuleUI()
		{
			if (NKCScenManager.CurrentUserData() == null)
			{
				return;
			}
			NKMShipModuleCandidate shipCandidateData = NKCScenManager.CurrentUserData().GetShipCandidateData();
			if (shipCandidateData.shipUid > 0L)
			{
				if (NKCScenManager.CurrentUserData().m_ArmyData.GetShipFromUID(shipCandidateData.shipUid) != null)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_SHIP_COMMAND_MODULE_SLOT_HAS_RESERVED, new NKCPopupOKCancel.OnButton(this.OpenCommandModulePopup), "");
					return;
				}
				Log.Warn(string.Format("TargetShipData is null - UID : {0}", shipCandidateData.shipUid), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIShipInfo.cs", 1291);
				NKCPacketSender.Send_NKMPacket_SHIP_SLOT_OPTION_CANCEL_REQ();
			}
			NKCPopupShipCommandModule.Instance.Open(this.m_curShipData, -1);
		}

		// Token: 0x06006CD1 RID: 27857 RVA: 0x00239A84 File Offset: 0x00237C84
		private void OpenCommandModulePopup()
		{
			NKMShipModuleCandidate shipCandidateData = NKCScenManager.CurrentUserData().GetShipCandidateData();
			long shipUid = shipCandidateData.shipUid;
			this.SetBannerUnit(shipUid);
			NKCPopupShipCommandModule.Instance.Open(this.m_curShipData, shipCandidateData.moduleId);
		}

		// Token: 0x0400585D RID: 22621
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_ship_info";

		// Token: 0x0400585E RID: 22622
		private const string UI_ASSET_NAME = "NKM_UI_SHIP_INFO";

		// Token: 0x0400585F RID: 22623
		private static NKCUIShipInfo m_Instance;

		// Token: 0x04005860 RID: 22624
		[Header("함선 정보")]
		public NKCUIShipInfoRepair m_ShipInfoRepair;

		// Token: 0x04005861 RID: 22625
		[Header("지휘 모듈")]
		public NKCUIShipInfoCommandModule m_ShipInfoCommandModule;

		// Token: 0x04005862 RID: 22626
		[Header("왼쪽 위 함선 기본정보")]
		public NKCUIShipInfoSummary m_uiSummary;

		// Token: 0x04005863 RID: 22627
		public GameObject m_objModuleStepMini;

		// Token: 0x04005864 RID: 22628
		public List<GameObject> m_lstModuleStepMini;

		// Token: 0x04005865 RID: 22629
		[Header("우측 토글 탭")]
		public NKCUIComToggle m_ctglInfo;

		// Token: 0x04005866 RID: 22630
		public NKCUIComToggle m_ctglRepair;

		// Token: 0x04005867 RID: 22631
		public GameObject m_objRepairLock;

		// Token: 0x04005868 RID: 22632
		public NKCUIComToggle m_ctglModule;

		// Token: 0x04005869 RID: 22633
		[Header("우측 오브젝트")]
		public GameObject m_objInfo;

		// Token: 0x0400586A RID: 22634
		public GameObject m_objRepair;

		// Token: 0x0400586B RID: 22635
		public GameObject m_objModule;

		// Token: 0x0400586C RID: 22636
		[Header("오른쪽 함선 정보")]
		public Text m_lbPower;

		// Token: 0x0400586D RID: 22637
		public Text m_lbHP;

		// Token: 0x0400586E RID: 22638
		public Text m_lbAttack;

		// Token: 0x0400586F RID: 22639
		public Text m_lbDefence;

		// Token: 0x04005870 RID: 22640
		public Text m_lbCritical;

		// Token: 0x04005871 RID: 22641
		public Text m_lbHit;

		// Token: 0x04005872 RID: 22642
		public Text m_lbEvade;

		// Token: 0x04005873 RID: 22643
		[Header("모듈 정보")]
		public GameObject m_objNoModule;

		// Token: 0x04005874 RID: 22644
		public GameObject m_objLockedModule;

		// Token: 0x04005875 RID: 22645
		public GameObject m_objEnabledModule;

		// Token: 0x04005876 RID: 22646
		public List<GameObject> m_lstModuleStep = new List<GameObject>();

		// Token: 0x04005877 RID: 22647
		public Text m_lbModuleStep;

		// Token: 0x04005878 RID: 22648
		public NKCUIComToggle m_tglSocket_01;

		// Token: 0x04005879 RID: 22649
		public NKCUIComToggle m_tglSocket_02;

		// Token: 0x0400587A RID: 22650
		public ScrollRect m_srSocketOptions;

		// Token: 0x0400587B RID: 22651
		public Text m_lbSocketOptions;

		// Token: 0x0400587C RID: 22652
		[Header("스킬")]
		public NKCUIShipInfoSkillPanel m_SkillPanel;

		// Token: 0x0400587D RID: 22653
		[Header("UI State 관련")]
		public RectTransform m_rtLeftRect;

		// Token: 0x0400587E RID: 22654
		public RectTransform m_rtRightRect;

		// Token: 0x0400587F RID: 22655
		public NKCUIRectMove m_rmLock;

		// Token: 0x04005880 RID: 22656
		[Header("스파인 일러스트")]
		public ScrollRect m_srScrollRect;

		// Token: 0x04005881 RID: 22657
		public RectTransform m_rectSpineIllustPanel;

		// Token: 0x04005882 RID: 22658
		public RectTransform m_rectIllustRoot;

		// Token: 0x04005883 RID: 22659
		public Vector2 m_vIllustRootAnchorMinNormal;

		// Token: 0x04005884 RID: 22660
		public Vector2 m_vIllustRootAnchorMaxNormal;

		// Token: 0x04005885 RID: 22661
		public Vector2 m_vIllustRootAnchorMinIllustView;

		// Token: 0x04005886 RID: 22662
		public Vector2 m_vIllustRootAnchorMaxIllustView;

		// Token: 0x04005887 RID: 22663
		[Header("기타 버튼")]
		public NKCUIComStateButton m_ChangeShip;

		// Token: 0x04005888 RID: 22664
		public NKCUIComStateButton m_cbtnChangeIllust;

		// Token: 0x04005889 RID: 22665
		public NKCUIComToggle m_ctglLock;

		// Token: 0x0400588A RID: 22666
		public NKCUIComToggle m_tglMoveRange;

		// Token: 0x0400588B RID: 22667
		public NKCUIShipInfoMoveType m_ShipInfoMoveType;

		// Token: 0x0400588C RID: 22668
		public NKCUIComStateButton m_cbtnPractice;

		// Token: 0x0400588D RID: 22669
		[Space]
		public NKCUIComStateButton m_GuideBtn;

		// Token: 0x0400588E RID: 22670
		public string m_GuideStrID;

		// Token: 0x0400588F RID: 22671
		public NKCComStatInfoToolTip m_ToolTipHP;

		// Token: 0x04005890 RID: 22672
		public NKCComStatInfoToolTip m_ToolTipATK;

		// Token: 0x04005891 RID: 22673
		public NKCComStatInfoToolTip m_ToolTipDEF;

		// Token: 0x04005892 RID: 22674
		public NKCComStatInfoToolTip m_ToolTipCritical;

		// Token: 0x04005893 RID: 22675
		public NKCComStatInfoToolTip m_ToolTipHit;

		// Token: 0x04005894 RID: 22676
		public NKCComStatInfoToolTip m_ToolTipEvade;

		// Token: 0x04005895 RID: 22677
		[Space]
		public GameObject m_objSeized;

		// Token: 0x04005896 RID: 22678
		[Header("임시빤짝이")]
		public GameObject TempChangeFX;

		// Token: 0x04005897 RID: 22679
		public Image m_imgBG;

		// Token: 0x04005898 RID: 22680
		[Header("리콜")]
		public GameObject m_objRecall;

		// Token: 0x04005899 RID: 22681
		public NKCUIComStateButton m_btnRecall;

		// Token: 0x0400589A RID: 22682
		public Text m_lbRecallTime;

		// Token: 0x0400589B RID: 22683
		public GameObject m_objModuleUnlockFx;

		// Token: 0x0400589C RID: 22684
		private NKCUIShipInfo.State m_curUIState;

		// Token: 0x0400589D RID: 22685
		private NKCUIShipInfo.ShipViewState m_eShipViewState;

		// Token: 0x0400589E RID: 22686
		private NKMUnitData m_curShipData;

		// Token: 0x0400589F RID: 22687
		private float m_fDeltaTime;

		// Token: 0x040058A0 RID: 22688
		private const float MIN_ZOOM_SCALE = 0.5f;

		// Token: 0x040058A1 RID: 22689
		private const float MAX_ZOOM_SCALE = 2f;

		// Token: 0x040058A2 RID: 22690
		private int m_CurShipID;

		// Token: 0x040058A3 RID: 22691
		private long m_CurShipUID;

		// Token: 0x040058A4 RID: 22692
		private List<NKMShipSkillTemplet> m_lstShipSkills = new List<NKMShipSkillTemplet>();

		// Token: 0x040058A5 RID: 22693
		private string m_shipName;

		// Token: 0x040058A6 RID: 22694
		[Header("함선 변경")]
		public NKCUIComDragSelectablePanel m_DragUnitView;

		// Token: 0x040058A7 RID: 22695
		private Dictionary<int, NKCASUIUnitIllust> m_dicUnitIllust = new Dictionary<int, NKCASUIUnitIllust>();

		// Token: 0x040058A8 RID: 22696
		private int m_iBannerSlotCnt;

		// Token: 0x040058A9 RID: 22697
		private List<NKMUnitData> m_lstUnitData = new List<NKMUnitData>();

		// Token: 0x040058AA RID: 22698
		private NKCUIUnitSelectList m_UIUnitSelectList;

		// Token: 0x020016ED RID: 5869
		private enum State
		{
			// Token: 0x0400A56E RID: 42350
			INFO,
			// Token: 0x0400A56F RID: 42351
			REPAIR,
			// Token: 0x0400A570 RID: 42352
			MODULE
		}

		// Token: 0x020016EE RID: 5870
		private enum ShipViewState
		{
			// Token: 0x0400A572 RID: 42354
			Normal,
			// Token: 0x0400A573 RID: 42355
			IllustView
		}
	}
}

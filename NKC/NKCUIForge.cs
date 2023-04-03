using System;
using System.Collections.Generic;
using ClientPacket.Common;
using NKM;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000990 RID: 2448
	public class NKCUIForge : NKCUIBase
	{
		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x0600652A RID: 25898 RVA: 0x00202CA0 File Offset: 0x00200EA0
		public static NKCUIForge Instance
		{
			get
			{
				if (NKCUIForge.m_Instance == null)
				{
					NKCUIForge.m_Instance = NKCUIManager.OpenNewInstance<NKCUIForge>("ab_ui_nkm_ui_factory", "NKM_UI_FACTORY", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUIForge.CleanupInstance)).GetInstance<NKCUIForge>();
					NKCUIForge.m_Instance.InitUI();
				}
				return NKCUIForge.m_Instance;
			}
		}

		// Token: 0x0600652B RID: 25899 RVA: 0x00202CEF File Offset: 0x00200EEF
		private static void CleanupInstance()
		{
			NKCUIForge.m_Instance = null;
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x0600652C RID: 25900 RVA: 0x00202CF7 File Offset: 0x00200EF7
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUIForge.m_Instance != null && NKCUIForge.m_Instance.IsOpen;
			}
		}

		// Token: 0x0600652D RID: 25901 RVA: 0x00202D12 File Offset: 0x00200F12
		public static void CheckInstanceAndClose()
		{
			if (NKCUIForge.m_Instance != null && NKCUIForge.m_Instance.IsOpen)
			{
				NKCUIForge.m_Instance.Close();
			}
		}

		// Token: 0x0600652E RID: 25902 RVA: 0x00202D37 File Offset: 0x00200F37
		private void OnDestroy()
		{
			NKCUIForge.m_Instance = null;
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x0600652F RID: 25903 RVA: 0x00202D40 File Offset: 0x00200F40
		public override string MenuName
		{
			get
			{
				switch (this.m_NKC_FORGE_TAB)
				{
				case NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT:
					return NKCUtilString.GET_STRING_FORGE;
				case NKCUIForge.NKC_FORGE_TAB.NFT_TUNING:
					return NKCUtilString.GET_STRING_FORGE_TUNNING;
				case NKCUIForge.NKC_FORGE_TAB.NFT_HIDDEN_OPTION:
					return NKCUtilString.GET_STRING_FACTORY_HIDDEN_OPTION_TITLE;
				default:
					return "";
				}
			}
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06006530 RID: 25904 RVA: 0x00202D7F File Offset: 0x00200F7F
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06006531 RID: 25905 RVA: 0x00202D84 File Offset: 0x00200F84
		public override string GuideTempletID
		{
			get
			{
				switch (this.m_NKC_FORGE_TAB)
				{
				case NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT:
					return "ARTICLE_EQUIP_ENCHANT";
				case NKCUIForge.NKC_FORGE_TAB.NFT_TUNING:
					if (this.m_NKCUIForgeTuning != null && this.m_NKCUIForgeTuning.GetCurTuningTab() == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_SET_OPTION_CHANGE)
					{
						return "ARTICLE_EQUIP_SET_CHANGE";
					}
					return "ARTICLE_EQUIP_TUNING";
				case NKCUIForge.NKC_FORGE_TAB.NFT_HIDDEN_OPTION:
					return "ARTICLE_EQUIP_HIDDEN_OPTION";
				default:
					return "";
				}
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06006532 RID: 25906 RVA: 0x00202DE5 File Offset: 0x00200FE5
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return this.RESOURCE_LIST;
			}
		}

		// Token: 0x06006533 RID: 25907 RVA: 0x00202DED File Offset: 0x00200FED
		public NKCUIForge.NKC_FORGE_TAB GetCurTab()
		{
			return this.m_NKC_FORGE_TAB;
		}

		// Token: 0x06006534 RID: 25908 RVA: 0x00202DF8 File Offset: 0x00200FF8
		public void InitUI()
		{
			this.m_NUM_FORGE = NKCUIManager.OpenUI("NUM_FACTORY");
			this.m_NKCUIForgeEnchant.InitUI();
			this.m_NKCUIForgeTuning.InitUI(new NKCUIForgeTuning.OnSelectSlot(this.SelectEquipslot));
			this.m_NKCUIForgeHiddenOption.InitUI();
			this.m_SkeletonGraphic = base.GetComponentInChildren<SkeletonGraphic>();
			if (this.m_cbtn_NKM_UI_FACTORY_ENCHANT_BUTTON_CHANGE != null)
			{
				this.m_cbtn_NKM_UI_FACTORY_ENCHANT_BUTTON_CHANGE.PointerClick.RemoveAllListeners();
				this.m_cbtn_NKM_UI_FACTORY_ENCHANT_BUTTON_CHANGE.PointerClick.AddListener(new UnityAction(this.OnChangeTargetEquip));
			}
			NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_ARROW_RIGHT, delegate()
			{
				this.ChangeItem(true);
			});
			NKCUtil.SetBindFunction(this.m_NKM_UI_FACTORY_ARROW_LEFT, delegate()
			{
				this.ChangeItem(false);
			});
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData eventData)
			{
				this.SkipAnimation(eventData);
			});
			this.m_etNoTouchPanel.triggers.Add(entry);
		}

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06006535 RID: 25909 RVA: 0x00202EEB File Offset: 0x002010EB
		public NKCUIInventory Inventory
		{
			get
			{
				return this.m_UIInventory;
			}
		}

		// Token: 0x06006536 RID: 25910 RVA: 0x00202EF4 File Offset: 0x002010F4
		private void OpenSelectInstance()
		{
			NKCUIForge.m_AssetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>("ab_ui_nuf_base", "NKM_UI_BASE_UNIT_SELECT", false, null);
			if (NKCUIForge.m_AssetInstanceData.m_Instant != null)
			{
				this.m_NKCUIUnitSelect = NKCUIForge.m_AssetInstanceData.m_Instant.GetComponent<NKCUIUnitSelect>();
				this.m_NKCUIUnitSelect.Init(new UnityAction(this.ChangeTargetEquip));
				this.m_NKCUIUnitSelect.transform.SetParent(this.m_rtNKM_UI_FACTORY_BACK.transform, false);
				this.m_NKCUIUnitSelect.Open();
			}
		}

		// Token: 0x06006537 RID: 25911 RVA: 0x00202F7C File Offset: 0x0020117C
		public void CloseSelectInstance()
		{
			if (NKCUIForge.m_AssetInstanceData != null)
			{
				NKCUIForge.m_AssetInstanceData.Unload();
			}
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x00202F90 File Offset: 0x00201190
		public void Open(NKCUIForge.NKC_FORGE_TAB eNKC_FORGE_TAB, long equipUID = 0L, HashSet<NKCEquipSortSystem.eFilterOption> setFilterOptions = null)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_NUM_FORGE, true);
			if (setFilterOptions == null)
			{
				setFilterOptions = new HashSet<NKCEquipSortSystem.eFilterOption>();
			}
			this.m_setFilterOptions = setFilterOptions;
			if (NKCScenManager.CurrentUserData().hasReservedEquipCandidate())
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_TUNING_HAS_RESERVED_EQUIP_TUNING, new NKCPopupOKCancel.OnButton(this.MoveToReservedEquipTuning), "");
				return;
			}
			if (equipUID == 0L)
			{
				this.OpenSelectInstance();
				this.m_NKC_FORGE_TAB = eNKC_FORGE_TAB;
			}
			this.SetLeftEquip(equipUID, true, false);
			this.SetTab(eNKC_FORGE_TAB);
			NKCUtil.SetGameobjectActive(this.m_NKCUIInvenEquipSlot.gameObject, equipUID != 0L);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_LEFT, equipUID != 0L);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_ENCHANT_BUTTON_CHANGE, equipUID != 0L);
			this.m_NKM_UI_FACTORY_SHORTCUT_MENU.OnConfirmBeforeChangeToCraft(new UnityAction(this.ConfirmExitTuning));
			this.m_NKM_UI_FACTORY_SHORTCUT_MENU.SetData(eNKC_FORGE_TAB);
			if (eNKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_TUNING || eNKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_HIDDEN_OPTION)
			{
				this.RESOURCE_LIST = this.RESOURCE_LIST_FORGE;
			}
			else
			{
				this.RESOURCE_LIST = base.UpsideMenuShowResourceList;
			}
			this.UpdateSelectedItemListToUnitEquip();
			this.SetActiveChangeItemArrow(this.m_lstSelectedItem.Count > 1);
			NKCUIManager.UpdateUpsideMenu();
			base.UIOpened(true);
		}

		// Token: 0x06006539 RID: 25913 RVA: 0x002030B4 File Offset: 0x002012B4
		private void MoveToReservedEquipTuning()
		{
			NKMEquipTuningCandidate tuiningData = NKCScenManager.CurrentUserData().GetTuiningData();
			this.SetTab(NKCUIForge.NKC_FORGE_TAB.NFT_TUNING);
			this.SetLeftEquip(tuiningData.equipUid, false, true);
			NKCUtil.SetGameobjectActive(this.m_NKCUIInvenEquipSlot.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_LEFT, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_ENCHANT_BUTTON_CHANGE, true);
			this.m_NKM_UI_FACTORY_SHORTCUT_MENU.OnConfirmBeforeChangeToCraft(new UnityAction(this.ConfirmExitTuning));
			this.m_NKM_UI_FACTORY_SHORTCUT_MENU.SetData(NKCUIForge.NKC_FORGE_TAB.NFT_TUNING);
			this.RESOURCE_LIST = this.RESOURCE_LIST_FORGE;
			this.UpdateSelectedItemListToUnitEquip();
			this.SetActiveChangeItemArrow(this.m_lstSelectedItem.Count > 1);
			NKCUIManager.UpdateUpsideMenu();
			base.UIOpened(true);
		}

		// Token: 0x0600653A RID: 25914 RVA: 0x00203160 File Offset: 0x00201360
		public void SetTab(NKCUIForge.NKC_FORGE_TAB eNKC_FORGE_TAB)
		{
			switch (eNKC_FORGE_TAB)
			{
			case NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT:
				this.m_NKCUIForgeTuning.SetOut();
				this.m_NKCUIForgeEnchant.AnimateOutToIn();
				this.m_NKCUIForgeHiddenOption.SetOut();
				this.m_NKCUIInvenEquipSlot.SetHighlightOnlyOneStatColor(0);
				this.m_NKCUIForgeEnchant.ResetMaterialEquipSlotsToEnhance();
				break;
			case NKCUIForge.NKC_FORGE_TAB.NFT_TUNING:
				this.m_NKCUIForgeTuning.AnimateOutToIn();
				this.m_NKCUIForgeEnchant.SetOut();
				this.m_NKCUIForgeHiddenOption.SetOut();
				this.m_NKCUIForgeTuning.ResetUI(true, false);
				this.m_NKCUIInvenEquipSlot.SetHighlightOnlyOneStatColor(this.m_NKCUIForgeTuning.GetSelectOption());
				break;
			case NKCUIForge.NKC_FORGE_TAB.NFT_HIDDEN_OPTION:
				this.m_NKCUIForgeTuning.SetOut();
				this.m_NKCUIForgeEnchant.SetOut();
				this.m_NKCUIForgeHiddenOption.AnimateOutToIn();
				this.m_NKCUIForgeHiddenOption.SetEnchantCard(this.m_NKM_UI_FACTORY_ENCHANT_CARD_BACK);
				this.m_NKCUIForgeHiddenOption.SetUI();
				this.m_NKCUIInvenEquipSlot.SetHighlightOnlyOneStatColor(3);
				break;
			}
			this.m_NKC_FORGE_TAB = eNKC_FORGE_TAB;
			if (eNKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_TUNING || eNKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_HIDDEN_OPTION)
			{
				this.RESOURCE_LIST = this.RESOURCE_LIST_FORGE;
			}
			else
			{
				this.RESOURCE_LIST = base.UpsideMenuShowResourceList;
			}
			NKCUIManager.UpdateUpsideMenu();
			if (this.m_LeftEquipUID == 0L)
			{
				this.ClearEquip();
			}
			this.PlaySpineAni();
			this.TutorialCheck();
		}

		// Token: 0x0600653B RID: 25915 RVA: 0x00203298 File Offset: 0x00201498
		private void SelectEquipslot(int idx)
		{
			this.m_NKCUIInvenEquipSlot.SetHighlightOnlyOneStatColor(idx);
		}

		// Token: 0x0600653C RID: 25916 RVA: 0x002032A6 File Offset: 0x002014A6
		public override void Hide()
		{
			base.Hide();
			NKCUtil.SetGameobjectActive(this.m_NUM_FORGE, false);
		}

		// Token: 0x0600653D RID: 25917 RVA: 0x002032BC File Offset: 0x002014BC
		public override void OnBackButton()
		{
			if (NKCScenManager.GetScenManager().GetMyUserData().hasReservedEquipTuningData())
			{
				this.CheckTuningExitConfirm(delegate
				{
					base.OnBackButton();
				}, null);
				return;
			}
			if (NKCScenManager.GetScenManager().GetMyUserData().hasReservedSetOptionData())
			{
				this.CheckSetOptionExitConfirm(delegate
				{
					base.OnBackButton();
				}, null);
				return;
			}
			if (!this.IsHiddenOptionEffectStopped())
			{
				return;
			}
			base.OnBackButton();
		}

		// Token: 0x0600653E RID: 25918 RVA: 0x00203324 File Offset: 0x00201524
		public override bool OnHomeButton()
		{
			if (NKCScenManager.GetScenManager().GetMyUserData().hasReservedEquipTuningData())
			{
				this.CheckTuningExitConfirm(delegate
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, null);
				return false;
			}
			if (NKCScenManager.GetScenManager().GetMyUserData().hasReservedSetOptionData())
			{
				this.CheckSetOptionExitConfirm(delegate
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, true);
				}, null);
				return false;
			}
			return this.IsHiddenOptionEffectStopped() && base.OnHomeButton();
		}

		// Token: 0x0600653F RID: 25919 RVA: 0x002033B4 File Offset: 0x002015B4
		public override void OnInventoryChange(NKMItemMiscData itemData)
		{
			if (this.m_NKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_TUNING && itemData != null && this.m_NKCUIForgeTuning != null && itemData.GetTemplet().m_ItemMiscType == NKM_ITEM_MISC_TYPE.IMT_MISC && this.m_NKCUIForgeTuning.GetCurTuningTab() != NKCUIForgeTuning.NKC_TUNING_TAB.NTT_PRECISION)
			{
				this.m_NKCUIForgeTuning.UpdateRequireItemUI();
			}
		}

		// Token: 0x06006540 RID: 25920 RVA: 0x00203400 File Offset: 0x00201600
		private void ConfirmExitTuning()
		{
			if (NKCScenManager.GetScenManager().GetMyUserData().hasReservedEquipTuningData())
			{
				this.CheckTuningExitConfirm(delegate
				{
					this.m_NKM_UI_FACTORY_SHORTCUT_MENU.MoveToCraft();
				}, delegate
				{
					this.m_NKM_UI_FACTORY_SHORTCUT_MENU.SetData(this.m_NKC_FORGE_TAB);
				});
				return;
			}
			if (NKCScenManager.GetScenManager().GetMyUserData().hasReservedSetOptionData())
			{
				this.CheckSetOptionExitConfirm(delegate
				{
					this.m_NKM_UI_FACTORY_SHORTCUT_MENU.MoveToCraft();
				}, delegate
				{
					this.m_NKM_UI_FACTORY_SHORTCUT_MENU.SetData(this.m_NKC_FORGE_TAB);
				});
				return;
			}
			this.m_NKM_UI_FACTORY_SHORTCUT_MENU.MoveToCraft();
		}

		// Token: 0x06006541 RID: 25921 RVA: 0x00203478 File Offset: 0x00201678
		private void CheckTuningExitConfirm(UnityAction OK, UnityAction CANCEL = null)
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_TUNING_EXIT_CONFIRM, delegate()
			{
				NKCPacketSender.Send_NKMPacket_Equip_Tuning_Cancel_REQ();
				UnityAction ok = OK;
				if (ok == null)
				{
					return;
				}
				ok();
			}, delegate()
			{
				UnityAction cancel = CANCEL;
				if (cancel == null)
				{
					return;
				}
				cancel();
			}, false);
		}

		// Token: 0x06006542 RID: 25922 RVA: 0x002034C4 File Offset: 0x002016C4
		private void CheckSetOptionExitConfirm(UnityAction OK, UnityAction CANCEL = null)
		{
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_SET_OPTION_TUNING_EXIT_CONFIRM, delegate()
			{
				NKCPacketSender.Send_NKMPacket_Equip_Tuning_Cancel_REQ();
				UnityAction ok = OK;
				if (ok == null)
				{
					return;
				}
				ok();
			}, delegate()
			{
				UnityAction cancel = CANCEL;
				if (cancel == null)
				{
					return;
				}
				cancel();
			}, false);
		}

		// Token: 0x06006543 RID: 25923 RVA: 0x00203510 File Offset: 0x00201710
		public override void UnHide()
		{
			base.UnHide();
			NKCUtil.SetGameobjectActive(this.m_NUM_FORGE, true);
			this.PlaySpineAni();
			if (this.m_NKC_FORGE_TAB != NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT)
			{
				if (this.m_NKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_TUNING)
				{
					NKCUIForgeTuning nkcuiforgeTuning = this.m_NKCUIForgeTuning;
					if (nkcuiforgeTuning != null)
					{
						nkcuiforgeTuning.AnimateOutToIn();
					}
					if (this.m_NKCUIForgeTuning != null && this.m_NKCUIForgeTuning.GetCurTuningTab() == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_SET_OPTION_CHANGE)
					{
						this.m_NKCUIForgeTuning.ResetUI(true, false);
						return;
					}
				}
				else if (this.m_NKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_HIDDEN_OPTION && this.m_NKCUIForgeHiddenOption != null)
				{
					this.m_NKCUIForgeHiddenOption.AnimateOutToIn();
					this.m_NKCUIForgeHiddenOption.SetEnchantCard(this.m_NKM_UI_FACTORY_ENCHANT_CARD_BACK);
					this.m_NKCUIForgeHiddenOption.SetUI();
				}
				return;
			}
			NKCUIForgeEnchant nkcuiforgeEnchant = this.m_NKCUIForgeEnchant;
			if (nkcuiforgeEnchant == null)
			{
				return;
			}
			nkcuiforgeEnchant.AnimateOutToIn();
		}

		// Token: 0x06006544 RID: 25924 RVA: 0x002035D0 File Offset: 0x002017D0
		public void ResetEquipEnhanceUI()
		{
			this.SetLeftEquip(this.m_LeftEquipUID, true, false);
		}

		// Token: 0x06006545 RID: 25925 RVA: 0x002035E0 File Offset: 0x002017E0
		public void ResetUI()
		{
			this.SetLeftEquip(this.m_LeftEquipUID, false, false);
		}

		// Token: 0x06006546 RID: 25926 RVA: 0x002035F0 File Offset: 0x002017F0
		public void DoAfterRefine(NKMEquipItemData orgData, int changedSlotNum)
		{
			this.ResetUI();
			if (this.m_NKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_TUNING)
			{
				this.m_NKCUIForgeTuning.DoAfterRefine(orgData, changedSlotNum);
			}
		}

		// Token: 0x06006547 RID: 25927 RVA: 0x0020360E File Offset: 0x0020180E
		public void DoAfterOptionChanged(int selectedSlot)
		{
			if (this.m_NKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_TUNING && this.m_NKCUIForgeTuning.GetCurTuningTab() == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_OPTION_CHANGE)
			{
				this.m_NKCUIForgeTuning.SetEffect(1, selectedSlot);
			}
		}

		// Token: 0x06006548 RID: 25928 RVA: 0x00203634 File Offset: 0x00201834
		public void DoAfterOptionChangedConfirm()
		{
			if (this.m_NKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_TUNING && this.m_NKCUIForgeTuning.GetCurTuningTab() == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_OPTION_CHANGE)
			{
				this.m_NKCUIForgeTuning.SetEffect(3, 0);
			}
		}

		// Token: 0x06006549 RID: 25929 RVA: 0x0020365A File Offset: 0x0020185A
		public void DoAfterSetOptionChanged()
		{
			if (this.m_NKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_TUNING && this.m_NKCUIForgeTuning.GetCurTuningTab() == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_SET_OPTION_CHANGE)
			{
				this.m_NKCUIForgeTuning.SetEffect(2, 0);
			}
		}

		// Token: 0x0600654A RID: 25930 RVA: 0x00203680 File Offset: 0x00201880
		public void DoAfterSetOptionChangeConfirm()
		{
			if (this.m_NKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_TUNING && this.m_NKCUIForgeTuning.GetCurTuningTab() == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_SET_OPTION_CHANGE)
			{
				this.m_NKCUIForgeTuning.SetEffect(4, 0);
			}
		}

		// Token: 0x0600654B RID: 25931 RVA: 0x002036A8 File Offset: 0x002018A8
		public void PlayEnhanceEffect()
		{
			this.m_NKCUIForgeEnchant.PlayEnhanceEffect();
			if (this.m_Animator != null)
			{
				this.m_bPlayingAni = true;
				this.enchantEffectSoundId = NKCSoundManager.PlaySound("FX_UI_UNIT_ENCHANT_START", 1f, 0f, 0f, false, 0f, false, 0f);
				this.m_Animator.Play("ENHANCE");
			}
		}

		// Token: 0x0600654C RID: 25932 RVA: 0x00203710 File Offset: 0x00201910
		public void SkipAnimation(BaseEventData cBaseEventData)
		{
			this.m_NKCUIForgeEnchant.ClearEnhanceEffect();
			if (this.m_Animator != null)
			{
				if (this.enchantEffectSoundId >= 0)
				{
					NKCSoundManager.StopSound(this.enchantEffectSoundId);
					this.enchantEffectSoundId = -1;
				}
				this.ResetEquipEnhanceUI();
				this.m_Animator.Play("BASE");
				this.m_bPlayingAni = false;
			}
		}

		// Token: 0x0600654D RID: 25933 RVA: 0x0020376E File Offset: 0x0020196E
		private void ClearEquip()
		{
			this.PlaySpineAni();
			this.m_NKCUIInvenEquipSlot.SetEmpty(null, null);
			if (this.m_NKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT)
			{
				this.m_NKCUIForgeEnchant.ClearAllUI();
				return;
			}
			this.m_NKCUIForgeTuning.ClearAllUI();
		}

		// Token: 0x0600654E RID: 25934 RVA: 0x002037A4 File Offset: 0x002019A4
		private void SetLeftEquip(long equipUID, bool bForce = true, bool bMoveToTabBeingTuned = false)
		{
			this.m_LeftEquipUID = equipUID;
			if (equipUID == 0L)
			{
				this.ClearEquip();
				return;
			}
			NKMEquipItemData itemEquip = NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip == null)
			{
				return;
			}
			this.m_NKCUIForgeTuning.SetLeftEquipUID(equipUID);
			this.m_NKCUIForgeEnchant.SetLeftEquipUID(equipUID);
			this.m_NKCUIForgeHiddenOption.SetLeftEquipUID(equipUID);
			this.m_NKCUIInvenEquipSlot.SetData(itemEquip, false, false);
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
			if (equipTemplet != null)
			{
				this.m_NKM_UI_FACTORY_SUMMARY_TITLE.text = NKCUtilString.GetEquipPositionStringByUnitStyle(equipTemplet, false);
				this.m_NKM_UI_FACTORY_SUMMARY_NAME.text = NKCUtilString.GetItemEquipNameWithTier(equipTemplet);
			}
			if (this.m_NKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT)
			{
				this.m_NKCUIForgeEnchant.ResetMaterialEquipSlotsToEnhance();
				this.m_NKCUIForgeEnchant.ResetEnhanceStatUI();
				return;
			}
			if (this.m_NKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_HIDDEN_OPTION)
			{
				this.m_NKCUIForgeHiddenOption.SetUI();
				return;
			}
			this.m_NKCUIForgeTuning.ResetUI(bForce, bMoveToTabBeingTuned);
		}

		// Token: 0x0600654F RID: 25935 RVA: 0x00203888 File Offset: 0x00201A88
		public override void CloseInternal()
		{
			this.m_bPlayingAni = false;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_NUM_FORGE, false);
			if (this.m_UIInventory != null && this.m_UIInventory.IsOpen)
			{
				this.m_UIInventory.Close();
			}
			this.m_NKCUIForgeTuning.Close();
			this.m_NKCUIForgeEnchant.Close();
			this.m_NKCUIForgeHiddenOption.Close();
			this.m_UIInventory = null;
			this.CloseSelectInstance();
		}

		// Token: 0x06006550 RID: 25936 RVA: 0x00203908 File Offset: 0x00201B08
		public override void OnCloseInstance()
		{
			base.OnCloseInstance();
			NKCUIForgeEnchant nkcuiforgeEnchant = this.m_NKCUIForgeEnchant;
			if (nkcuiforgeEnchant != null)
			{
				nkcuiforgeEnchant.OnCloseInstance();
			}
			if (this.m_UIInventory != null && this.m_UIInventory.IsOpen)
			{
				this.m_UIInventory.Close();
			}
			this.m_UIInventory = null;
		}

		// Token: 0x06006551 RID: 25937 RVA: 0x00203959 File Offset: 0x00201B59
		public bool IsInventoryInstanceOpen()
		{
			return this.m_UIInventory != null && this.m_UIInventory.IsOpen;
		}

		// Token: 0x06006552 RID: 25938 RVA: 0x00203978 File Offset: 0x00201B78
		private void OnChangeTargetEquip()
		{
			if (NKCScenManager.GetScenManager().GetMyUserData().hasReservedSetOptionData())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_SET_OPTION_TUNING_EXIT_CONFIRM, delegate()
				{
					NKCPacketSender.Send_NKMPacket_Equip_Tuning_Cancel_REQ();
					this.ChangeTargetEquip();
				}, null, false);
				return;
			}
			if (NKCScenManager.GetScenManager().GetMyUserData().hasReservedEquipTuningData())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_TUNING_EXIT_CONFIRM, delegate()
				{
					NKCPacketSender.Send_NKMPacket_Equip_Tuning_Cancel_REQ();
					this.ChangeTargetEquip();
				}, null, false);
				return;
			}
			this.ChangeTargetEquip();
		}

		// Token: 0x06006553 RID: 25939 RVA: 0x002039EC File Offset: 0x00201BEC
		public void ChangeTargetEquip()
		{
			if (!this.IsHiddenOptionEffectStopped())
			{
				return;
			}
			NKCUIInventory.EquipSelectListOptions options = new NKCUIInventory.EquipSelectListOptions(NKC_INVENTORY_OPEN_TYPE.NIOT_NORMAL, false, true);
			options.lstSortOption = NKCEquipSortSystem.FORGE_TARGET_SORT_LIST;
			options.m_NKC_INVENTORY_OPEN_TYPE = NKC_INVENTORY_OPEN_TYPE.NIOT_EQUIP_SELECT;
			options.m_dOnSelectedEquipSlot = new NKCUISlotEquip.OnSelectedEquipSlot(this.OnSelectedEquipSlotForTarget);
			options.m_dOnGetItemListAfterSelected = new NKCUIInventory.OnGetItemListAfterSelected(this.OnGetItemListAfterSelected);
			if (this.m_UIInventory == null)
			{
				this.m_UIInventory = NKCUIInventory.OpenNewInstance();
			}
			if (this.m_NKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT)
			{
				options.strEmptyMessage = NKCUtilString.GET_STRING_NO_EXIST_ENCHANT_EQUIP;
				options.bLockMaxItem = true;
				options.bHideMaxLvItem = true;
			}
			else if (this.m_NKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_TUNING)
			{
				if (this.m_NKCUIForgeTuning.GetCurTuningTab() == NKCUIForgeTuning.NKC_TUNING_TAB.NTT_SET_OPTION_CHANGE)
				{
					options.m_EquipListOptions.bHideNotPossibleSetOptionItem = true;
				}
				options.strEmptyMessage = NKCUtilString.GET_STRING_NO_EXIST_TUNING_EQUIP;
				options.bLockMaxItem = false;
				options.bHideMaxLvItem = false;
			}
			else if (this.m_NKC_FORGE_TAB == NKCUIForge.NKC_FORGE_TAB.NFT_HIDDEN_OPTION)
			{
				options.strEmptyMessage = NKCUtilString.GET_STRING_NO_EXIST_HIDDEN_OPTION_EQUIP;
				options.m_EquipListOptions.AdditionalExcludeFilterFunc = new NKCEquipSortSystem.EquipListOptions.CustomFilterFunc(this.IsRelicTarget);
				options.bLockMaxItem = false;
				options.bHideMaxLvItem = false;
			}
			else
			{
				options.strEmptyMessage = "";
			}
			options.bHideLockItem = false;
			options.m_EquipListOptions.setExcludeFilterOption = new HashSet<NKCEquipSortSystem.eFilterOption>
			{
				NKCEquipSortSystem.eFilterOption.Equip_Enchant
			};
			options.m_ButtonMenuType = NKCPopupItemEquipBox.EQUIP_BOX_BOTTOM_MENU_TYPE.EBBMT_OK;
			options.setFilterOption = this.m_setFilterOptions;
			NKCUIInventory uiinventory = this.m_UIInventory;
			if (uiinventory == null)
			{
				return;
			}
			uiinventory.Open(options, this.RESOURCE_LIST, 0L, NKCUIInventory.NKC_INVENTORY_TAB.NIT_NONE);
		}

		// Token: 0x06006554 RID: 25940 RVA: 0x00203B65 File Offset: 0x00201D65
		public bool IsHiddenOptionEffectStopped()
		{
			return this.m_NKCUIForgeHiddenOption == null || this.m_NKCUIForgeHiddenOption.IsEffectStopped();
		}

		// Token: 0x06006555 RID: 25941 RVA: 0x00203B84 File Offset: 0x00201D84
		private bool IsRelicTarget(NKMEquipItemData equipData)
		{
			if (NKMItemManager.GetEquipTemplet(equipData.m_ItemEquipID).IsRelic())
			{
				if (equipData.potentialOption == null)
				{
					return true;
				}
				bool flag = false;
				int num = equipData.potentialOption.sockets.Length;
				for (int i = 0; i < num; i++)
				{
					if (equipData.potentialOption.sockets[i] == null)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006556 RID: 25942 RVA: 0x00203BE4 File Offset: 0x00201DE4
		private void OnSelectedEquipSlotForTarget(NKCUISlotEquip slot, NKMEquipItemData equipData)
		{
			if (equipData == null)
			{
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMItemManager.CanEnchantItem(NKCScenManager.GetScenManager().GetMyUserData(), equipData);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
				return;
			}
			if (this.m_UIInventory != null && this.m_UIInventory.IsOpen)
			{
				this.m_setFilterOptions = this.m_UIInventory.GetNKCUIInventoryOption().setFilterOption;
				this.m_UIInventory.Close();
			}
			this.SetLeftEquip(equipData.m_ItemUid, true, false);
			if (this.m_NKCUIUnitSelect != null)
			{
				this.m_NKCUIUnitSelect.Close();
			}
			NKCUtil.SetGameobjectActive(this.m_NKCUIInvenEquipSlot.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_LEFT, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_ENCHANT_BUTTON_CHANGE, true);
			this.SetActiveChangeItemArrow(this.m_lstSelectedItem.Count > 1);
			this.PlaySpineAni();
		}

		// Token: 0x06006557 RID: 25943 RVA: 0x00203CC0 File Offset: 0x00201EC0
		private void UpdateSelectedItemListToUnitEquip()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMEquipItemData itemEquip = nkmuserData.m_InventoryData.GetItemEquip(this.m_LeftEquipUID);
			if (itemEquip == null)
			{
				return;
			}
			if (itemEquip.m_OwnerUnitUID > 0L)
			{
				NKMUnitData unitFromUID = nkmuserData.m_ArmyData.GetUnitFromUID(itemEquip.m_OwnerUnitUID);
				if (unitFromUID == null)
				{
					return;
				}
				this.m_lstSelectedItem.Clear();
				if (unitFromUID.GetEquipItemWeaponUid() > 0L)
				{
					this.m_lstSelectedItem.Add(unitFromUID.GetEquipItemWeaponUid());
				}
				if (unitFromUID.GetEquipItemDefenceUid() > 0L)
				{
					this.m_lstSelectedItem.Add(unitFromUID.GetEquipItemDefenceUid());
				}
				if (unitFromUID.GetEquipItemAccessoryUid() > 0L)
				{
					this.m_lstSelectedItem.Add(unitFromUID.GetEquipItemAccessoryUid());
				}
				if (unitFromUID.GetEquipItemAccessory2Uid() > 0L)
				{
					this.m_lstSelectedItem.Add(unitFromUID.GetEquipItemAccessory2Uid());
				}
			}
		}

		// Token: 0x06006558 RID: 25944 RVA: 0x00203D88 File Offset: 0x00201F88
		private void OnGetItemListAfterSelected(List<NKMEquipItemData> lstItemData)
		{
			this.m_lstSelectedItem.Clear();
			foreach (NKMEquipItemData nkmequipItemData in lstItemData)
			{
				this.m_lstSelectedItem.Add(nkmequipItemData.m_ItemUid);
			}
		}

		// Token: 0x06006559 RID: 25945 RVA: 0x00203DEC File Offset: 0x00201FEC
		private void PlaySpineAni()
		{
			NKCSoundManager.PlaySound("FX_UI_FACTORY_WORK_START", 1f, 0f, 0f, false, 0f, false, 0f);
			this.m_SkeletonGraphic.AnimationState.ClearTrack(0);
			this.SetSpineAnimation(0, "BASE", false, true);
			this.SetSpineAnimation(0, "BASE_LOOP", true, false);
		}

		// Token: 0x0600655A RID: 25946 RVA: 0x00203E4C File Offset: 0x0020204C
		private void SetSpineAnimation(int trackID, string animName, bool bLoop, bool bSet)
		{
			if (this.m_SkeletonGraphic == null)
			{
				return;
			}
			if (this.m_SkeletonGraphic.SkeletonData != null && this.m_SkeletonGraphic.SkeletonData.FindAnimation(animName) != null)
			{
				if (bSet)
				{
					this.m_SkeletonGraphic.AnimationState.SetAnimation(trackID, animName, bLoop);
					return;
				}
				Spine.Animation animation = this.m_SkeletonGraphic.SkeletonData.FindAnimation("BASE");
				if (animation != null)
				{
					this.m_SkeletonGraphic.AnimationState.AddAnimation(trackID, animName, bLoop, animation.Duration);
				}
			}
		}

		// Token: 0x0600655B RID: 25947 RVA: 0x00203ED4 File Offset: 0x002020D4
		private void ChangeItem(bool bNext)
		{
			if (this.m_LeftEquipUID == 0L)
			{
				return;
			}
			if (!this.IsHiddenOptionEffectStopped())
			{
				return;
			}
			int num = this.m_lstSelectedItem.FindIndex((long x) => x == this.m_LeftEquipUID);
			if (num < 0 || num > this.m_lstSelectedItem.Count)
			{
				return;
			}
			int targetIndex = -1;
			if (bNext)
			{
				if (this.m_lstSelectedItem.Count <= num + 1)
				{
					targetIndex = 0;
				}
				else
				{
					targetIndex = num + 1;
				}
			}
			else if (num == 0)
			{
				targetIndex = this.m_lstSelectedItem.Count - 1;
			}
			else
			{
				targetIndex = num - 1;
			}
			if (NKCScenManager.GetScenManager().GetMyUserData().hasReservedEquipTuningData())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_TUNING_EXIT_CONFIRM, delegate()
				{
					NKCPacketSender.Send_NKMPacket_Equip_Tuning_Cancel_REQ();
					this.SetLeftEquip(this.m_lstSelectedItem[targetIndex], true, false);
				}, null, false);
				return;
			}
			if (NKCScenManager.GetScenManager().GetMyUserData().hasReservedSetOptionData())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_FORGE_SET_OPTION_TUNING_EXIT_CONFIRM, delegate()
				{
					NKCPacketSender.Send_NKMPacket_Equip_Tuning_Cancel_REQ();
					this.SetLeftEquip(this.m_lstSelectedItem[targetIndex], true, false);
				}, null, false);
				return;
			}
			this.SetLeftEquip(this.m_lstSelectedItem[targetIndex], true, false);
		}

		// Token: 0x0600655C RID: 25948 RVA: 0x00203FF1 File Offset: 0x002021F1
		private void SetActiveChangeItemArrow(bool bActive)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_ARROW_RIGHT.gameObject, bActive);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_FACTORY_ARROW_LEFT.gameObject, bActive);
		}

		// Token: 0x0600655D RID: 25949 RVA: 0x00204018 File Offset: 0x00202218
		private void TutorialCheck()
		{
			switch (this.m_NKC_FORGE_TAB)
			{
			case NKCUIForge.NKC_FORGE_TAB.NFT_ENCHANT:
				NKCTutorialManager.TutorialRequired(TutorialPoint.FactoryEnchant, true);
				return;
			case NKCUIForge.NKC_FORGE_TAB.NFT_TUNING:
				NKCTutorialManager.TutorialRequired(TutorialPoint.FactoryTuning, true);
				return;
			case NKCUIForge.NKC_FORGE_TAB.NFT_HIDDEN_OPTION:
				NKCTutorialManager.TutorialRequired(TutorialPoint.FactoryHiddenOption, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600655E RID: 25950 RVA: 0x0020405C File Offset: 0x0020225C
		private void Update()
		{
			if (this.m_bPlayingAni && Input.anyKeyDown)
			{
				this.m_bPlayingAni = false;
				this.SkipAnimation(null);
			}
		}

		// Token: 0x040050CF RID: 20687
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_factory";

		// Token: 0x040050D0 RID: 20688
		private const string UI_ASSET_NAME = "NKM_UI_FACTORY";

		// Token: 0x040050D1 RID: 20689
		private static NKCUIForge m_Instance;

		// Token: 0x040050D2 RID: 20690
		private readonly List<int> RESOURCE_LIST_FORGE = new List<int>
		{
			1013,
			1035,
			1,
			101
		};

		// Token: 0x040050D3 RID: 20691
		private List<int> RESOURCE_LIST;

		// Token: 0x040050D4 RID: 20692
		private GameObject m_NUM_FORGE;

		// Token: 0x040050D5 RID: 20693
		public NKCUIInvenEquipSlot m_NKCUIInvenEquipSlot;

		// Token: 0x040050D6 RID: 20694
		private long m_LeftEquipUID;

		// Token: 0x040050D7 RID: 20695
		public NKCUIForgeEnchant m_NKCUIForgeEnchant;

		// Token: 0x040050D8 RID: 20696
		public NKCUIForgeTuning m_NKCUIForgeTuning;

		// Token: 0x040050D9 RID: 20697
		public NKCUIForgeHiddenOption m_NKCUIForgeHiddenOption;

		// Token: 0x040050DA RID: 20698
		public Animator m_Animator;

		// Token: 0x040050DB RID: 20699
		public Text m_NKM_UI_FACTORY_SUMMARY_TITLE;

		// Token: 0x040050DC RID: 20700
		public Text m_NKM_UI_FACTORY_SUMMARY_NAME;

		// Token: 0x040050DD RID: 20701
		public NKCUIComButton m_cbtn_NKM_UI_FACTORY_ENCHANT_BUTTON_CHANGE;

		// Token: 0x040050DE RID: 20702
		public NKCUIFactoryShortCutMenu m_NKM_UI_FACTORY_SHORTCUT_MENU;

		// Token: 0x040050DF RID: 20703
		private SkeletonGraphic m_SkeletonGraphic;

		// Token: 0x040050E0 RID: 20704
		private NKCUIForge.NKC_FORGE_TAB m_NKC_FORGE_TAB;

		// Token: 0x040050E1 RID: 20705
		[Header("아이템 변경")]
		public NKCUIComStateButton m_NKM_UI_FACTORY_ARROW_RIGHT;

		// Token: 0x040050E2 RID: 20706
		public NKCUIComStateButton m_NKM_UI_FACTORY_ARROW_LEFT;

		// Token: 0x040050E3 RID: 20707
		public RectTransform m_rtNKM_UI_FACTORY_BACK;

		// Token: 0x040050E4 RID: 20708
		public GameObject m_NKM_UI_FACTORY_ENCHANT_CARD_BACK;

		// Token: 0x040050E5 RID: 20709
		public GameObject m_NKM_UI_FACTORY_LEFT;

		// Token: 0x040050E6 RID: 20710
		public GameObject m_NKM_UI_FACTORY_ENCHANT_BUTTON_CHANGE;

		// Token: 0x040050E7 RID: 20711
		public EventTrigger m_etNoTouchPanel;

		// Token: 0x040050E8 RID: 20712
		private static NKCAssetInstanceData m_AssetInstanceData;

		// Token: 0x040050E9 RID: 20713
		private const string ASSET_SELECT_BUNDLE_NAME = "ab_ui_nuf_base";

		// Token: 0x040050EA RID: 20714
		private const string UI_SELECT_ASSET_NAME = "NKM_UI_BASE_UNIT_SELECT";

		// Token: 0x040050EB RID: 20715
		private NKCUIUnitSelect m_NKCUIUnitSelect;

		// Token: 0x040050EC RID: 20716
		private NKCUIInventory m_UIInventory;

		// Token: 0x040050ED RID: 20717
		private HashSet<NKCEquipSortSystem.eFilterOption> m_setFilterOptions = new HashSet<NKCEquipSortSystem.eFilterOption>();

		// Token: 0x040050EE RID: 20718
		private bool m_bPlayingAni;

		// Token: 0x040050EF RID: 20719
		private int enchantEffectSoundId = -1;

		// Token: 0x040050F0 RID: 20720
		private List<long> m_lstSelectedItem = new List<long>();

		// Token: 0x02001658 RID: 5720
		public enum NKC_FORGE_TAB
		{
			// Token: 0x0400A422 RID: 42018
			NFT_ENCHANT,
			// Token: 0x0400A423 RID: 42019
			NFT_TUNING,
			// Token: 0x0400A424 RID: 42020
			NFT_HIDDEN_OPTION
		}
	}
}

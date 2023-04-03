using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000963 RID: 2403
	public class NKCDeckViewSide : MonoBehaviour
	{
		// Token: 0x06005FE8 RID: 24552 RVA: 0x001DDC4A File Offset: 0x001DBE4A
		public int GetCurrMultiplyRewardCount()
		{
			return this.m_CurrMultiplyRewardCount;
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x06005FE9 RID: 24553 RVA: 0x001DDC52 File Offset: 0x001DBE52
		public bool OperationSkip
		{
			get
			{
				return this.m_bOperationSkip;
			}
		}

		// Token: 0x06005FEA RID: 24554 RVA: 0x001DDC5A File Offset: 0x001DBE5A
		public NKCDeckViewSideUnitIllust GetDeckViewSideUnitIllust()
		{
			return this.m_DeckViewSideUnitIllust;
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x001DDC62 File Offset: 0x001DBE62
		private NKMStageTempletV2 GetStageTemplet()
		{
			if (NKCUIDeckViewer.IsDungeonAtkReadyScen(this.m_DeckViewerMode))
			{
				return NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY().GetStageTemplet();
			}
			return null;
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x001DDC84 File Offset: 0x001DBE84
		public void Init(NKCDeckViewSideUnitIllust.OnUnitInfoClick onUnitInfo, NKCDeckViewSideUnitIllust.OnUnitChangeClick onUnitChange, NKCDeckViewSideUnitIllust.OnLeaderChange onLeaderChange, NKCDeckViewSide.OnConfirm onConfirm, NKCDeckViewSide.OnClickCloseBtn onClickCloseBtn, NKCDeckViewSide.CheckMultiply checkMultiply)
		{
			this.m_DeckViewSideUnitIllust.Init(onUnitInfo, onUnitChange, onLeaderChange, this.m_animLoading);
			this.dOnConfirm = onConfirm;
			this.dOnClickCloseBtn = onClickCloseBtn;
			this.dCheckMultiply = checkMultiply;
			NKCUtil.SetBindFunction(this.m_ResourceButton, new UnityAction(this.OnBtnConfirm));
			NKCUtil.SetBindFunction(this.m_csbtnClose, new UnityAction(this.OnBtnClose));
			NKCUtil.SetToggleValueChangedDelegate(this.m_tglSkip, new UnityAction<bool>(this.OnClickSkip));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnSkillInfo, new UnityAction(this.OpenSkillInfoPopup));
			this.m_CurrMultiplyRewardCount = 1;
			if (this.m_NKCUIOperationSkip != null)
			{
				this.m_NKCUIOperationSkip.Init(new NKCUIOperationSkip.OnCountUpdated(this.OnOperationSkipUpdated), new UnityAction(this.OnClickOperationSkipClose));
			}
			NKCUtil.SetBindFunction(this.m_cbtn_NKM_DECK_VIEW_SIDE_UNIT_ILLUST_SUB_MENU_INFO_TEXT, new UnityAction(this.OpenInfo));
		}

		// Token: 0x06005FED RID: 24557 RVA: 0x001DDD68 File Offset: 0x001DBF68
		private bool IsCanStartEterniumDungeon()
		{
			if (NKCUIDeckViewer.IsDungeonAtkReadyScen(this.m_DeckViewerMode))
			{
				NKC_SCEN_DUNGEON_ATK_READY scen_DUNGEON_ATK_READY = NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY();
				if (scen_DUNGEON_ATK_READY != null)
				{
					return NKCUtil.IsCanStartEterniumStage(scen_DUNGEON_ATK_READY.GetStageTemplet(), true);
				}
			}
			return true;
		}

		// Token: 0x06005FEE RID: 24558 RVA: 0x001DDDA0 File Offset: 0x001DBFA0
		public void OnBtnConfirm()
		{
			if (this.m_eButtonDisableReason == NKM_ERROR_CODE.NEC_OK)
			{
				if ((this.m_DeckViewerMode == NKCUIDeckViewer.DeckViewerMode.AsyncPvpDefenseDeck || this.m_DeckViewerMode == NKCUIDeckViewer.DeckViewerMode.PvPBattleFindTarget) && !NKMArmyData.IsAllUnitsEquipedAllGears(this.m_SelectDeckIndex))
				{
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_GAUNTLET_DECK_UNIT_NOT_ALL_EQUIPED_GEAR_DESC, delegate()
					{
						if (this.dOnConfirm != null)
						{
							this.dOnConfirm();
						}
					}, null, false);
					return;
				}
				if (this.dOnConfirm != null)
				{
					this.dOnConfirm();
					return;
				}
			}
			else
			{
				if (!this.m_berrorCodePopupMsg)
				{
					NKCPopupOKCancel.OpenOKBox(this.m_eButtonDisableReason, null, "");
					return;
				}
				NKCPopupMessageManager.AddPopupMessage(this.m_eButtonDisableReason, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
			}
		}

		// Token: 0x06005FEF RID: 24559 RVA: 0x001DDE34 File Offset: 0x001DC034
		public void OnBtnClose()
		{
			if (this.dOnClickCloseBtn != null)
			{
				this.dOnClickCloseBtn();
			}
		}

		// Token: 0x06005FF0 RID: 24560 RVA: 0x001DDE49 File Offset: 0x001DC049
		public void SetAttackCost(int itemID, int itemCount)
		{
			if (this.m_ResourceButton != null)
			{
				this.m_ResourceButton.SetData(itemID, itemCount);
			}
		}

		// Token: 0x06005FF1 RID: 24561 RVA: 0x001DDE66 File Offset: 0x001DC066
		public void SetMultiSelectedCount(int count, int maxCount)
		{
			NKCUtil.SetLabelText(this.m_lbMultiSelectCount, count.ToString() + " / " + maxCount.ToString());
		}

		// Token: 0x06005FF2 RID: 24562 RVA: 0x001DDE8C File Offset: 0x001DC08C
		private void ResetUIByScen(NKCUIDeckViewer.DeckViewerOption sDeckViewerOption, bool bUseCost)
		{
			bool bValue = false;
			bool flag = false;
			this.m_bDisableButtonExtraMsg = false;
			bool bValue2 = false;
			bool enableControlLeaderBtn = true;
			bool flag2 = bUseCost;
			bool bValue3;
			switch (sDeckViewerOption.eDeckviewerMode)
			{
			case NKCUIDeckViewer.DeckViewerMode.PrepareBattle:
				bValue3 = true;
				bUseCost = false;
				flag2 = false;
				bValue = false;
				this.m_lbStartTextWithoutCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_START;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.PvPBattleFindTarget:
				bValue3 = true;
				bUseCost = false;
				flag2 = false;
				bValue = false;
				this.m_lbStartTextWithoutCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_PVP;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.AsyncPvPBattleStart:
				bValue3 = true;
				bUseCost = false;
				flag2 = false;
				bValue = false;
				this.m_lbStartTextWithoutCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_ACTION;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.AsyncPvpDefenseDeck:
				bValue3 = true;
				bUseCost = false;
				flag2 = false;
				bValue = false;
				this.m_lbStartTextWithoutCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_ACTION;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.PrepareDungeonBattle:
				bValue3 = true;
				bUseCost = true;
				flag2 = true;
				bValue = false;
				this.m_lbStartTextWithCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_START;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.WorldMapMissionDeckSelect:
				bValue3 = true;
				bUseCost = false;
				flag2 = false;
				bValue = true;
				this.m_lbStartTextWithoutCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_OK;
				this.m_bDisableButtonExtraMsg = true;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.DeckSelect:
				bValue3 = true;
				bUseCost = false;
				flag2 = false;
				bValue = false;
				this.m_lbStartTextWithoutCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_OK;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.WarfareBatch:
				bValue3 = true;
				bUseCost = false;
				flag2 = true;
				bValue = false;
				this.m_lbStartTextWithCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_BATCH;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.WarfareBatch_Assault:
				bValue3 = true;
				bUseCost = false;
				flag2 = true;
				bValue = false;
				this.m_lbStartTextWithCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_BATCH;
				this.m_bDisableButtonExtraMsg = true;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.WarfareRecovery:
				bValue3 = true;
				flag2 = true;
				bValue = false;
				this.m_lbStartTextWithCost.text = NKCUtilString.GET_STRING_WARFARE_RECOVERY;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.MainDeckSelect:
				bValue3 = true;
				bUseCost = false;
				flag2 = false;
				bValue = false;
				this.m_lbStartTextWithoutCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_ACTION;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.PrepareDungeonBattle_Daily:
				bValue3 = true;
				bUseCost = true;
				flag2 = true;
				bValue = false;
				this.m_lbStartTextWithCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_START;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.PrepareDungeonBattleWithoutCost:
				bValue3 = true;
				bUseCost = false;
				flag2 = false;
				bValue = false;
				this.m_lbStartTextWithoutCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_START;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.PrepareDungeonBattle_CC:
				bValue3 = true;
				bUseCost = false;
				flag2 = true;
				bValue = false;
				this.m_lbStartTextWithCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_START;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.DeckMultiSelect:
				bValue3 = true;
				bUseCost = false;
				flag2 = false;
				bValue = false;
				flag = true;
				this.m_lbStartTextWithoutCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_SELECT;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.PrepareRaid:
				bValue3 = false;
				bValue2 = true;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.GuildCoopBoss:
				bValue3 = false;
				bValue2 = true;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.PrivatePvPReady:
				bValue3 = true;
				bUseCost = false;
				flag2 = false;
				bValue = false;
				this.m_lbStartTextWithoutCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_ACTION;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.LeaguePvPMain:
				bValue3 = true;
				bUseCost = false;
				flag2 = false;
				bValue = false;
				this.m_lbStartTextWithCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_OK;
				goto IL_2BB;
			case NKCUIDeckViewer.DeckViewerMode.PrepareLocalDeck:
				bValue3 = true;
				this.m_lbStartTextWithoutCost.text = NKCUtilString.GET_STRING_DECK_BUTTON_OK;
				goto IL_2BB;
			}
			bValue3 = false;
			bValue = false;
			IL_2BB:
			if (bUseCost && sDeckViewerOption.CostItemCount > 0)
			{
				this.SetAttackCost(sDeckViewerOption.CostItemID, sDeckViewerOption.CostItemCount * this.m_CurrMultiplyRewardCount);
				this.m_costItemID = sDeckViewerOption.CostItemID;
				this.m_costItemCount = sDeckViewerOption.CostItemCount;
				this.m_multiplyMax = sDeckViewerOption.OperationMultiplyMax;
			}
			else
			{
				bUseCost = false;
				this.m_costItemID = 0;
				this.m_costItemCount = 0;
				this.m_multiplyMax = 0;
			}
			NKMStageTempletV2 stageTemplet = this.GetStageTemplet();
			bool flag3 = false;
			if (stageTemplet != null && stageTemplet.EnterLimit > 0)
			{
				int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(stageTemplet.Key, false, false, false);
				if (stageTemplet.EnterLimit - statePlayCnt <= 0 && stageTemplet.RestoreReqItem != null)
				{
					bUseCost = true;
					flag2 = true;
					flag3 = true;
					this.m_CurrMultiplyRewardCount = 1;
					this.m_costItemID = stageTemplet.RestoreReqItem.ItemId;
					this.m_costItemCount = stageTemplet.RestoreReqItem.Count32;
					this.m_lbStartTextWithCost.text = NKCUtilString.GET_STRING_WARFARE_GAME_HUD_OPERATION_RESTORE;
					this.UpdateAttackCost();
				}
			}
			if (flag3)
			{
				NKCUtil.SetImageSprite(this.m_imgStartArrow, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX_SPRITE", "NKM_UI_COMMON_ICON_ENTERLIMIT_RECOVER_SMALL", false), false);
			}
			else
			{
				NKCUtil.SetImageSprite(this.m_imgStartArrow, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX_SPRITE", "NKM_UI_COMMON_ICON_GAUNTLET", false), false);
			}
			NKCUtil.SetGameobjectActive(this.m_objSuccessRate, bValue);
			NKCUtil.SetGameobjectActive(this.m_objStartButtonRoot, bValue3);
			NKCUtil.SetGameobjectActive(this.m_objCost, bUseCost || flag);
			NKCUtil.SetGameobjectActive(this.m_objCostBG, bUseCost || flag);
			NKCUtil.SetGameobjectActive(this.m_lbStartTextWithCost, flag2);
			NKCUtil.SetGameobjectActive(this.m_lbStartTextWithoutCost, !flag2);
			NKCUtil.SetGameobjectActive(this.m_lbMultiSelectCount, flag);
			if (this.m_ResourceButton != null)
			{
				this.m_ResourceButton.OnShow(bUseCost);
			}
			NKCUtil.SetGameobjectActive(this.m_objCloseBtn, bValue2);
			NKCUtil.SetGameobjectActive(this.m_NKCUIOperationSkip, false);
			NKCUtil.SetGameobjectActive(this.m_objSkip, sDeckViewerOption.bUsableOperationSkip);
			if (sDeckViewerOption.bUsableOperationSkip)
			{
				this.m_tglSkip.Select(false, false, false);
			}
			if (sDeckViewerOption.bNoUseLeaderBtn)
			{
				enableControlLeaderBtn = false;
				NKCUtil.SetGameobjectActive(this.m_DeckViewSideUnitIllust.m_cbtnLeader, false);
			}
			this.m_DeckViewSideUnitIllust.SetEnableControlLeaderBtn(enableControlLeaderBtn);
		}

		// Token: 0x06005FF3 RID: 24563 RVA: 0x001DE368 File Offset: 0x001DC568
		public void Open(NKCUIDeckViewer.DeckViewerOption sDeckViewerOption, bool bInit, bool bUseCost)
		{
			if (!base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(true);
			}
			this.m_DeckViewerMode = sDeckViewerOption.eDeckviewerMode;
			this.m_SelectDeckIndex = sDeckViewerOption.DeckIndex;
			this.m_CurrMultiplyRewardCount = 1;
			this.m_bOperationSkip = false;
			this.m_DeckViewSideUnitIllust.Open(sDeckViewerOption.eDeckviewerMode, bInit);
			this.ResetUIByScen(sDeckViewerOption, bUseCost);
		}

		// Token: 0x06005FF4 RID: 24564 RVA: 0x001DE3CE File Offset: 0x001DC5CE
		public void ChangeDeckIndex(NKMDeckIndex deckIdx)
		{
			this.m_SelectDeckIndex = deckIdx;
		}

		// Token: 0x06005FF5 RID: 24565 RVA: 0x001DE3D8 File Offset: 0x001DC5D8
		public void SetEnableButtons(NKM_ERROR_CODE errorCode)
		{
			this.m_eButtonDisableReason = errorCode;
			bool flag = errorCode == NKM_ERROR_CODE.NEC_OK;
			NKCUtil.SetGameobjectActive(this.m_objPossibleFX, flag);
			this.m_imgStartBG.sprite = (flag ? this.m_spStartButtonBG : this.m_spStartButtonBGDisable);
			this.m_imgStartArrow.color = (flag ? this.m_colArrow : this.m_colArrowDisable);
			this.m_lbStartTextWithCost.color = (flag ? this.m_colMainText : this.m_colMainTextDisable);
			this.m_lbStartTextWithoutCost.color = (flag ? this.m_colMainText : this.m_colMainTextDisable);
			if (this.m_ResourceButton != null)
			{
				if (!flag)
				{
					this.m_ResourceButton.SetTextColor(this.m_colCostDisable);
				}
				else
				{
					this.m_ResourceButton.SetTextColor(this.m_colCost);
					this.UpdateAttackCost();
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objDisableBtnExtraMsg, this.m_bDisableButtonExtraMsg && errorCode == NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_ASSAULT_POSITION);
			if (errorCode == NKM_ERROR_CODE.NEC_FAIL_WORLDMAP_MISSION_DECK_HAS_UNIT_FROM_ANOTHER_CITY || errorCode == NKM_ERROR_CODE.NEC_FAIL_WARFARE_GAME_CANNOT_ASSAULT_POSITION)
			{
				this.m_berrorCodePopupMsg = this.m_bDisableButtonExtraMsg;
				return;
			}
			this.m_berrorCodePopupMsg = false;
		}

		// Token: 0x06005FF6 RID: 24566 RVA: 0x001DE4EA File Offset: 0x001DC6EA
		public void Close()
		{
			this.m_DeckViewSideUnitIllust.Close();
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
		}

		// Token: 0x06005FF7 RID: 24567 RVA: 0x001DE510 File Offset: 0x001DC710
		public void PlayLoadingAnim(string name)
		{
			if (this.m_animLoading == null)
			{
				return;
			}
			if (this.m_animLoading.gameObject.activeInHierarchy)
			{
				this.m_animLoading.Play(name, -1, 0f);
			}
		}

		// Token: 0x06005FF8 RID: 24568 RVA: 0x001DE545 File Offset: 0x001DC745
		public void SetSuccessRate(int number, bool bDeckReady)
		{
			if (bDeckReady)
			{
				NKCUtil.SetLabelText(this.m_lbSuccessRate, string.Format(NKCUtilString.GET_STRING_DECK_SUCCESS_RATE_ONE_PARAM, number));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbSuccessRate, NKCUtilString.GET_STRING_DECK_CANNOT_START);
		}

		// Token: 0x06005FF9 RID: 24569 RVA: 0x001DE578 File Offset: 0x001DC778
		protected void SetEquipListData(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				using (IEnumerator enumerator = Enum.GetValues(typeof(ITEM_EQUIP_POSITION)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						ITEM_EQUIP_POSITION position = (ITEM_EQUIP_POSITION)obj;
						NKCUtil.SetImageSprite(this.GetEquipIconImage(position), this.m_spEquipEmpty, false);
					}
					return;
				}
			}
			foreach (object obj2 in Enum.GetValues(typeof(ITEM_EQUIP_POSITION)))
			{
				ITEM_EQUIP_POSITION position2 = (ITEM_EQUIP_POSITION)obj2;
				this.SetWeaponImage(unitData, position2);
			}
		}

		// Token: 0x06005FFA RID: 24570 RVA: 0x001DE63C File Offset: 0x001DC83C
		private void SetWeaponImage(NKMUnitData unitData, ITEM_EQUIP_POSITION position)
		{
			Image equipIconImage = this.GetEquipIconImage(position);
			if (equipIconImage == null)
			{
				return;
			}
			long equipUid = unitData.GetEquipUid(position);
			if (equipUid == 0L)
			{
				if (position == ITEM_EQUIP_POSITION.IEP_ACC2 && !unitData.IsUnlockAccessory2())
				{
					equipIconImage.sprite = this.m_spEquipLock;
				}
				else
				{
					equipIconImage.sprite = this.m_spEquipEmpty;
				}
				if (position == ITEM_EQUIP_POSITION.IEP_WEAPON)
				{
					NKCUtil.SetGameobjectActive(this.m_EQUIP_1_SET, false);
					return;
				}
				if (position == ITEM_EQUIP_POSITION.IEP_DEFENCE)
				{
					NKCUtil.SetGameobjectActive(this.m_EQUIP_2_SET, false);
					return;
				}
				if (position == ITEM_EQUIP_POSITION.IEP_ACC)
				{
					NKCUtil.SetGameobjectActive(this.m_EQUIP_3_SET, false);
					return;
				}
				if (position == ITEM_EQUIP_POSITION.IEP_ACC2)
				{
					NKCUtil.SetGameobjectActive(this.m_EQUIP_4_SET, false);
				}
				return;
			}
			else
			{
				NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(equipUid);
				if (itemEquip == null)
				{
					Debug.LogError(string.Format("equipped equip not exist. uid {0}", equipUid));
					if (position == ITEM_EQUIP_POSITION.IEP_ACC2 && !unitData.IsUnlockAccessory2())
					{
						equipIconImage.sprite = this.m_spEquipLock;
						return;
					}
					equipIconImage.sprite = this.m_spEquipEmpty;
					return;
				}
				else
				{
					switch (position)
					{
					case ITEM_EQUIP_POSITION.IEP_WEAPON:
						NKCUtil.SetGameobjectActive(this.m_EQUIP_1_SET, NKMItemManager.IsActiveSetOptionItem(itemEquip));
						break;
					case ITEM_EQUIP_POSITION.IEP_DEFENCE:
						NKCUtil.SetGameobjectActive(this.m_EQUIP_2_SET, NKMItemManager.IsActiveSetOptionItem(itemEquip));
						break;
					case ITEM_EQUIP_POSITION.IEP_ACC:
						NKCUtil.SetGameobjectActive(this.m_EQUIP_3_SET, NKMItemManager.IsActiveSetOptionItem(itemEquip));
						break;
					case ITEM_EQUIP_POSITION.IEP_ACC2:
						NKCUtil.SetGameobjectActive(this.m_EQUIP_4_SET, NKMItemManager.IsActiveSetOptionItem(itemEquip));
						break;
					}
					NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
					if (equipTemplet == null)
					{
						Debug.LogError(string.Format("equiptemplet not exist. id {0}", itemEquip.m_ItemEquipID));
						equipIconImage.sprite = this.m_spEquipEmpty;
						return;
					}
					equipIconImage.sprite = this.GetItemSprite(equipTemplet.m_NKM_ITEM_GRADE);
					return;
				}
			}
		}

		// Token: 0x06005FFB RID: 24571 RVA: 0x001DE7C8 File Offset: 0x001DC9C8
		private Image GetEquipIconImage(ITEM_EQUIP_POSITION position)
		{
			switch (position)
			{
			case ITEM_EQUIP_POSITION.IEP_WEAPON:
				return this.m_imgEquipWeapon;
			case ITEM_EQUIP_POSITION.IEP_DEFENCE:
				return this.m_imgEquipArmor;
			case ITEM_EQUIP_POSITION.IEP_ACC:
				return this.m_imgEquipAcc;
			case ITEM_EQUIP_POSITION.IEP_ACC2:
				return this.m_imgEquipAcc2;
			default:
				return null;
			}
		}

		// Token: 0x06005FFC RID: 24572 RVA: 0x001DE7FF File Offset: 0x001DC9FF
		private Sprite GetItemSprite(NKM_ITEM_GRADE grade)
		{
			switch (grade)
			{
			default:
				return this.m_spEquipN;
			case NKM_ITEM_GRADE.NIG_R:
				return this.m_spEquipR;
			case NKM_ITEM_GRADE.NIG_SR:
				return this.m_spEquipSR;
			case NKM_ITEM_GRADE.NIG_SSR:
				return this.m_spEquipSSR;
			}
		}

		// Token: 0x06005FFD RID: 24573 RVA: 0x001DE832 File Offset: 0x001DCA32
		public void UpdateUnitData(NKMUnitData unitData)
		{
			if (this.m_UnitData.m_UnitUID != unitData.m_UnitUID)
			{
				return;
			}
			this.SetUnitData(unitData, true);
		}

		// Token: 0x06005FFE RID: 24574 RVA: 0x001DE850 File Offset: 0x001DCA50
		public void SetUnitData(NKMUnitData unitData, bool bForce = false)
		{
			if ((!bForce && this.m_UnitData == unitData) || unitData == null)
			{
				return;
			}
			this.m_UnitData = unitData;
			this.UpdateRoleSlot();
			this.UpdateSkillSlot();
			this.UpdateBanUI(unitData);
			this.SetEquipListData(unitData);
		}

		// Token: 0x06005FFF RID: 24575 RVA: 0x001DE884 File Offset: 0x001DCA84
		public void SetOperatorData(NKMOperator operatorData, bool bForce = false)
		{
			if ((!bForce && this.m_OperatorData == operatorData) || operatorData == null)
			{
				return;
			}
			this.m_UnitData = null;
			this.m_OperatorData = operatorData;
			NKCUtil.SetLabelText(this.m_lbUnitInfo, " ");
			NKCUtil.SetGameobjectActive(this.m_tacticUpdateLvSlot.gameObject, false);
			NKCUtil.SetGameobjectActive(this.m_objUnitEquip, false);
			this.UpdateOperatorSkillSlot();
			this.UpdateBanUI(this.m_OperatorData);
			this.SetEquipListData(null);
		}

		// Token: 0x06006000 RID: 24576 RVA: 0x001DE8F8 File Offset: 0x001DCAF8
		private void UpdateBanUI(NKMUnitData unitData)
		{
			NKCUtil.SetGameobjectActive(this.m_objBan, false);
			if (unitData == null)
			{
				return;
			}
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitData.m_UnitID);
			if (nkmunitTempletBase == null)
			{
				return;
			}
			if (NKCUtil.CheckPossibleShowBan(this.m_DeckViewerMode) && NKCBanManager.IsBanUnitByUTB(nkmunitTempletBase))
			{
				int unitBanLevelByUTB = NKCBanManager.GetUnitBanLevelByUTB(nkmunitTempletBase);
				NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_BAN_LEVEL_ONE_PARAM, unitBanLevelByUTB));
				this.m_lbBanLevel.color = Color.red;
				NKCUtil.SetGameobjectActive(this.m_objBan, true);
				return;
			}
			if (NKCUtil.CheckPossibleShowUpUnit(this.m_DeckViewerMode) && NKCBanManager.IsUpUnitByUTB(nkmunitTempletBase))
			{
				int unitUpLevelByUTB = NKCBanManager.GetUnitUpLevelByUTB(nkmunitTempletBase);
				NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_UP_LEVEL_ONE_PARAM, unitUpLevelByUTB));
				this.m_lbBanLevel.color = NKCBanManager.UP_COLOR;
				NKCUtil.SetGameobjectActive(this.m_objBan, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objBan, false);
		}

		// Token: 0x06006001 RID: 24577 RVA: 0x001DE9DC File Offset: 0x001DCBDC
		private void UpdateBanUI(NKMOperator operData)
		{
			NKCUtil.SetGameobjectActive(this.m_objBan, false);
			if (operData == null)
			{
				return;
			}
			if (NKCUtil.CheckPossibleShowBan(this.m_DeckViewerMode) && NKCBanManager.IsBanOperator(operData.id, NKCBanManager.BAN_DATA_TYPE.FINAL))
			{
				NKCUtil.SetLabelText(this.m_lbBanLevel, string.Format(NKCUtilString.GET_STRING_GAUNTLET_BAN_LEVEL_ONE_PARAM, NKCBanManager.GetOperBanLevel(operData.id, NKCBanManager.BAN_DATA_TYPE.FINAL)));
				this.m_lbBanLevel.color = Color.red;
				NKCUtil.SetGameobjectActive(this.m_objBan, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objBan, false);
		}

		// Token: 0x06006002 RID: 24578 RVA: 0x001DEA64 File Offset: 0x001DCC64
		private void UpdateRoleSlot()
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitData.m_UnitID);
			if (unitTempletBase == null)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(NKCUtilString.GetUnitStyleName(unitTempletBase.m_NKM_UNIT_STYLE_TYPE));
			if (unitTempletBase.m_NKM_UNIT_STYLE_TYPE_SUB != NKM_UNIT_STYLE_TYPE.NUST_INVALID)
			{
				stringBuilder.Append("·");
				stringBuilder.Append(NKCUtilString.GetUnitStyleName(unitTempletBase.m_NKM_UNIT_STYLE_TYPE_SUB));
			}
			NKCUtil.SetGameobjectActive(this.m_objUnitEquip, unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL);
			if (unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_SHIP || unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				NKCUtil.SetLabelText(this.m_lbUnitInfo, stringBuilder.ToString());
				return;
			}
			stringBuilder.Append("·");
			stringBuilder.Append(NKCUtilString.GetRoleText(unitTempletBase));
			NKCUtil.SetLabelText(this.m_lbUnitInfo, stringBuilder.ToString());
		}

		// Token: 0x06006003 RID: 24579 RVA: 0x001DEB28 File Offset: 0x001DCD28
		private void UpdateSkillSlot()
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_UnitData.m_UnitID);
			if (unitTempletBase == null)
			{
				return;
			}
			this.m_lstShipSkillTemplet.Clear();
			this.m_unitName = unitTempletBase.GetUnitName();
			this.m_tacticUpdateLvSlot.SetLevel(this.m_UnitData.tacticLevel, false);
			NKCUtil.SetGameobjectActive(this.m_tacticUpdateLvSlot.gameObject, unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL);
			NKM_UNIT_TYPE nkm_UNIT_TYPE = unitTempletBase.m_NKM_UNIT_TYPE;
			if (nkm_UNIT_TYPE != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				if (nkm_UNIT_TYPE != NKM_UNIT_TYPE.NUT_SHIP)
				{
					return;
				}
				this.m_CurUnitID = this.m_UnitData.m_UnitID;
				this.m_CurUnitUID = this.m_UnitData.m_UnitUID;
				NKCUtil.SetGameobjectActive(this.m_csbtnSkillInfo, unitTempletBase.GetSkillCount() > 0);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_csbtnSkillInfo, unitTempletBase.GetSkillCount() > 0);
				this.m_unitStarGradeMax = unitTempletBase.m_StarGradeMax;
				this.m_unitLimitBreakLevel = (int)this.m_UnitData.m_LimitBreakLevel;
				this.m_CurUnitID = this.m_UnitData.m_UnitID;
				this.m_CurUnitUID = this.m_UnitData.m_UnitUID;
			}
			NKCUtil.SetGameobjectActive(this.m_lbUnitInfo, unitTempletBase.m_NKM_UNIT_TYPE == NKM_UNIT_TYPE.NUT_NORMAL);
		}

		// Token: 0x06006004 RID: 24580 RVA: 0x001DEC40 File Offset: 0x001DCE40
		private void UpdateOperatorSkillSlot()
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_OperatorData.id);
			if (unitTempletBase == null)
			{
				return;
			}
			this.m_lstShipSkillTemplet.Clear();
			this.m_unitName = unitTempletBase.GetUnitName();
			this.m_CurUnitID = this.m_OperatorData.id;
			this.m_CurUnitUID = this.m_OperatorData.uid;
			if (NKCUtil.CheckPossibleShowBan(this.m_DeckViewerMode))
			{
				NKCBanManager.IsBanOperator(this.m_OperatorData.id, NKCBanManager.BAN_DATA_TYPE.FINAL);
			}
			NKCUtil.SetGameobjectActive(this.m_csbtnSkillInfo, true);
			NKCUtil.SetGameobjectActive(this.m_lbUnitInfo, false);
		}

		// Token: 0x06006005 RID: 24581 RVA: 0x001DECD8 File Offset: 0x001DCED8
		public void OpenSkillInfoPopup()
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(this.m_CurUnitID);
			if (unitTempletBase == null)
			{
				return;
			}
			switch (unitTempletBase.m_NKM_UNIT_TYPE)
			{
			case NKM_UNIT_TYPE.NUT_NORMAL:
				NKCPopupSkillFullInfo.UnitInstance.OpenForUnit(this.m_UnitData, this.m_unitName, this.m_unitStarGradeMax, this.m_unitLimitBreakLevel, unitTempletBase.IsRearmUnit);
				return;
			case NKM_UNIT_TYPE.NUT_SHIP:
				NKCPopupSkillFullInfo.ShipInstance.OpenForShip(this.m_CurUnitID, this.m_CurUnitUID);
				return;
			case NKM_UNIT_TYPE.NUT_OPERATOR:
				NKCUIOperatorPopUpSkill.Instance.Open(this.m_CurUnitUID);
				return;
			default:
				return;
			}
		}

		// Token: 0x06006006 RID: 24582 RVA: 0x001DED61 File Offset: 0x001DCF61
		private void OpenInfo()
		{
			if (this.m_UnitData == null)
			{
				return;
			}
			NKCPopupUnitRoleInfo.Instance.OpenPopup(this.m_UnitData);
		}

		// Token: 0x06006007 RID: 24583 RVA: 0x001DED7C File Offset: 0x001DCF7C
		private void OnClickSkip(bool bSet)
		{
			if (bSet)
			{
				if ((this.dCheckMultiply != null && !this.dCheckMultiply(true)) || !this.IsCanStartEterniumDungeon())
				{
					this.m_tglSkip.Select(false, false, false);
					return;
				}
				this.m_bOperationSkip = true;
				this.UpdateAttackCost();
				this.SetSkipCountUIData();
			}
			NKCUtil.SetGameobjectActive(this.m_NKCUIOperationSkip, bSet);
			if (!bSet)
			{
				this.m_CurrMultiplyRewardCount = 1;
				this.m_bOperationSkip = false;
				this.UpdateAttackCost();
				this.SetSkipCountUIData();
			}
		}

		// Token: 0x06006008 RID: 24584 RVA: 0x001DEDF6 File Offset: 0x001DCFF6
		private void OnOperationSkipUpdated(int newCount)
		{
			this.m_CurrMultiplyRewardCount = newCount;
			this.UpdateAttackCost();
		}

		// Token: 0x06006009 RID: 24585 RVA: 0x001DEE05 File Offset: 0x001DD005
		private void OnClickOperationSkipClose()
		{
			this.m_tglSkip.Select(false, false, false);
		}

		// Token: 0x0600600A RID: 24586 RVA: 0x001DEE18 File Offset: 0x001DD018
		private void SetSkipCountUIData()
		{
			int num = 99;
			NKMStageTempletV2 stageTemplet = this.GetStageTemplet();
			if (stageTemplet != null && stageTemplet.EnterLimit > 0)
			{
				int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(stageTemplet.Key, false, false, false);
				num = stageTemplet.EnterLimit - statePlayCnt;
			}
			if (this.m_multiplyMax != 0)
			{
				num = Mathf.Min(num, this.m_multiplyMax);
			}
			this.m_NKCUIOperationSkip.SetData(NKMCommonConst.SkipCostMiscItemId, NKMCommonConst.SkipCostMiscItemCount, this.m_costItemID, this.m_costItemCount, this.m_CurrMultiplyRewardCount, 1, num);
		}

		// Token: 0x0600600B RID: 24587 RVA: 0x001DEE96 File Offset: 0x001DD096
		public void UpdateAttackCost()
		{
			this.SetAttackCost(this.m_costItemID, this.m_costItemCount * this.m_CurrMultiplyRewardCount);
		}

		// Token: 0x0600600C RID: 24588 RVA: 0x001DEEB4 File Offset: 0x001DD0B4
		public void UpdateCostUI(NKMItemMiscData itemData)
		{
			if (itemData.ItemID == this.m_costItemID)
			{
				NKMStageTempletV2 stageTemplet = this.GetStageTemplet();
				if (stageTemplet != null && this.m_costItemID != 0 && stageTemplet.m_bActiveBattleSkip && stageTemplet.EnterLimit <= 0)
				{
					NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
					if (myUserData != null)
					{
						this.m_multiplyMax = (int)myUserData.m_InventoryData.GetCountMiscItem(this.m_costItemID) / this.m_costItemCount;
					}
				}
				this.UpdateAttackCost();
				this.SetSkipCountUIData();
			}
		}

		// Token: 0x04004C29 RID: 19497
		public Animator m_animLoading;

		// Token: 0x04004C2A RID: 19498
		public NKCDeckViewSideUnitIllust m_DeckViewSideUnitIllust;

		// Token: 0x04004C2B RID: 19499
		[Header("오른쪽 아래 시작 버튼")]
		public GameObject m_objStartButtonRoot;

		// Token: 0x04004C2C RID: 19500
		public GameObject m_objPossibleFX;

		// Token: 0x04004C2D RID: 19501
		public Image m_imgStartBG;

		// Token: 0x04004C2E RID: 19502
		public Image m_imgStartArrow;

		// Token: 0x04004C2F RID: 19503
		public Text m_lbStartTextWithCost;

		// Token: 0x04004C30 RID: 19504
		public Text m_lbStartTextWithoutCost;

		// Token: 0x04004C31 RID: 19505
		public GameObject m_objCost;

		// Token: 0x04004C32 RID: 19506
		public GameObject m_objCostBG;

		// Token: 0x04004C33 RID: 19507
		public GameObject m_objSuccessRate;

		// Token: 0x04004C34 RID: 19508
		public Text m_lbSuccessRate;

		// Token: 0x04004C35 RID: 19509
		public NKCUIComResourceButton m_ResourceButton;

		// Token: 0x04004C36 RID: 19510
		public Text m_lbMultiSelectCount;

		// Token: 0x04004C37 RID: 19511
		public GameObject m_objDisableBtnExtraMsg;

		// Token: 0x04004C38 RID: 19512
		private bool m_bDisableButtonExtraMsg;

		// Token: 0x04004C39 RID: 19513
		public Color m_colMainText;

		// Token: 0x04004C3A RID: 19514
		public Color m_colMainTextDisable;

		// Token: 0x04004C3B RID: 19515
		public Color m_colArrow;

		// Token: 0x04004C3C RID: 19516
		public Color m_colArrowDisable;

		// Token: 0x04004C3D RID: 19517
		public Color m_colCost;

		// Token: 0x04004C3E RID: 19518
		public Color m_colCostDisable;

		// Token: 0x04004C3F RID: 19519
		public Sprite m_spStartButtonBG;

		// Token: 0x04004C40 RID: 19520
		public Sprite m_spStartButtonBGDisable;

		// Token: 0x04004C41 RID: 19521
		[Header("왼쪽에 닫기 버튼")]
		public GameObject m_objCloseBtn;

		// Token: 0x04004C42 RID: 19522
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04004C43 RID: 19523
		[Header("밴 관련")]
		public GameObject m_objBan;

		// Token: 0x04004C44 RID: 19524
		public Text m_lbBanLevel;

		// Token: 0x04004C45 RID: 19525
		[Header("스킵")]
		public GameObject m_objSkip;

		// Token: 0x04004C46 RID: 19526
		public NKCUIOperationSkip m_NKCUIOperationSkip;

		// Token: 0x04004C47 RID: 19527
		public NKCUIComToggle m_tglSkip;

		// Token: 0x04004C48 RID: 19528
		private NKM_ERROR_CODE m_eButtonDisableReason;

		// Token: 0x04004C49 RID: 19529
		private NKCDeckViewSide.OnConfirm dOnConfirm;

		// Token: 0x04004C4A RID: 19530
		private NKCDeckViewSide.OnClickCloseBtn dOnClickCloseBtn;

		// Token: 0x04004C4B RID: 19531
		private NKCDeckViewSide.CheckMultiply dCheckMultiply;

		// Token: 0x04004C4C RID: 19532
		private bool m_berrorCodePopupMsg;

		// Token: 0x04004C4D RID: 19533
		private NKCUIDeckViewer.DeckViewerMode m_DeckViewerMode;

		// Token: 0x04004C4E RID: 19534
		private NKMDeckIndex m_SelectDeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_NORMAL, 0);

		// Token: 0x04004C4F RID: 19535
		private int m_CurrMultiplyRewardCount = 1;

		// Token: 0x04004C50 RID: 19536
		private int m_costItemID;

		// Token: 0x04004C51 RID: 19537
		private int m_costItemCount;

		// Token: 0x04004C52 RID: 19538
		private int m_multiplyMax;

		// Token: 0x04004C53 RID: 19539
		private bool m_bOperationSkip;

		// Token: 0x04004C54 RID: 19540
		public const int MAX_COUNT_MULTIPLY_AND_SKIP = 99;

		// Token: 0x04004C55 RID: 19541
		[Header("유닛정보 텍스트")]
		public Text m_lbUnitInfo;

		// Token: 0x04004C56 RID: 19542
		[Header("장비 표시")]
		[FormerlySerializedAs("m_NKM_DECK_VIEW_SIDE_UNIT_ILLUST_SUB_MENU_INFO_3")]
		public GameObject m_objUnitEquip;

		// Token: 0x04004C57 RID: 19543
		public Sprite m_spEquipLock;

		// Token: 0x04004C58 RID: 19544
		public Sprite m_spEquipEmpty;

		// Token: 0x04004C59 RID: 19545
		public Sprite m_spEquipN;

		// Token: 0x04004C5A RID: 19546
		public Sprite m_spEquipR;

		// Token: 0x04004C5B RID: 19547
		public Sprite m_spEquipSR;

		// Token: 0x04004C5C RID: 19548
		public Sprite m_spEquipSSR;

		// Token: 0x04004C5D RID: 19549
		public Image m_imgEquipWeapon;

		// Token: 0x04004C5E RID: 19550
		public Image m_imgEquipArmor;

		// Token: 0x04004C5F RID: 19551
		public Image m_imgEquipAcc;

		// Token: 0x04004C60 RID: 19552
		public Image m_imgEquipAcc2;

		// Token: 0x04004C61 RID: 19553
		public GameObject m_EQUIP_1_SET;

		// Token: 0x04004C62 RID: 19554
		public GameObject m_EQUIP_2_SET;

		// Token: 0x04004C63 RID: 19555
		public GameObject m_EQUIP_3_SET;

		// Token: 0x04004C64 RID: 19556
		public GameObject m_EQUIP_4_SET;

		// Token: 0x04004C65 RID: 19557
		[Header("스킬")]
		public NKCUIComStateButton m_csbtnSkillInfo;

		// Token: 0x04004C66 RID: 19558
		[Header("전술 업데이트")]
		public NKCUITacticUpdateLevelSlot m_tacticUpdateLvSlot;

		// Token: 0x04004C67 RID: 19559
		private NKMUnitData m_UnitData;

		// Token: 0x04004C68 RID: 19560
		private NKMOperator m_OperatorData;

		// Token: 0x04004C69 RID: 19561
		[Header("정보창 호출")]
		public NKCUIComButton m_cbtn_NKM_DECK_VIEW_SIDE_UNIT_ILLUST_SUB_MENU_INFO_TEXT;

		// Token: 0x04004C6A RID: 19562
		private List<NKMShipSkillTemplet> m_lstShipSkillTemplet = new List<NKMShipSkillTemplet>();

		// Token: 0x04004C6B RID: 19563
		private string m_unitName = "";

		// Token: 0x04004C6C RID: 19564
		private int m_unitStarGradeMax;

		// Token: 0x04004C6D RID: 19565
		private int m_unitLimitBreakLevel;

		// Token: 0x04004C6E RID: 19566
		private int m_CurUnitID;

		// Token: 0x04004C6F RID: 19567
		private long m_CurUnitUID;

		// Token: 0x020015E4 RID: 5604
		private enum COST_TYPE
		{
			// Token: 0x0400A2B6 RID: 41654
			CT_ETERNIUM,
			// Token: 0x0400A2B7 RID: 41655
			CT_TICKET,
			// Token: 0x0400A2B8 RID: 41656
			CT_INFO
		}

		// Token: 0x020015E5 RID: 5605
		// (Invoke) Token: 0x0600AE93 RID: 44691
		public delegate void OnConfirm();

		// Token: 0x020015E6 RID: 5606
		// (Invoke) Token: 0x0600AE97 RID: 44695
		public delegate void OnClickCloseBtn();

		// Token: 0x020015E7 RID: 5607
		// (Invoke) Token: 0x0600AE9B RID: 44699
		public delegate bool CheckMultiply(bool bMsg);

		// Token: 0x020015E8 RID: 5608
		public class SkillSlot
		{
			// Token: 0x0400A2B9 RID: 41657
			public Image SkillImg;

			// Token: 0x0400A2BA RID: 41658
			public Text SkillLevel;
		}
	}
}

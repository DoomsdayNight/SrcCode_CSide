using System;
using System.Collections.Generic;
using ClientPacket.Mode;
using NKC.UI.Guide;
using NKC.UI.NPC;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000982 RID: 2434
	public class NKCUIDiveReady : NKCUIBase
	{
		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x060063A0 RID: 25504 RVA: 0x001F7B5F File Offset: 0x001F5D5F
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_DIVE_READY;
			}
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x060063A1 RID: 25505 RVA: 0x001F7B66 File Offset: 0x001F5D66
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return this.m_upsideMenuResourceList;
			}
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x060063A2 RID: 25506 RVA: 0x001F7B6E File Offset: 0x001F5D6E
		public override string GuideTempletID
		{
			get
			{
				return "ARTICLE_DIVE_INFO";
			}
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x060063A3 RID: 25507 RVA: 0x001F7B75 File Offset: 0x001F5D75
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x060063A4 RID: 25508 RVA: 0x001F7B78 File Offset: 0x001F5D78
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Normal;
			}
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x060063A5 RID: 25509 RVA: 0x001F7B7B File Offset: 0x001F5D7B
		private bool JumpSelected
		{
			get
			{
				return this.m_tglJump != null && this.m_tglJump.gameObject.activeInHierarchy && this.m_tglJump.m_bSelect;
			}
		}

		// Token: 0x060063A6 RID: 25510 RVA: 0x001F7BAA File Offset: 0x001F5DAA
		private void SetBG(bool bHurdle = false)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_BG_NORMAL, !bHurdle);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_BG_HURDLE, bHurdle);
		}

		// Token: 0x060063A7 RID: 25511 RVA: 0x001F7BC7 File Offset: 0x001F5DC7
		private void SetDiveInfoBG(bool bHurdle = false)
		{
			NKCUtil.SetGameobjectActive(this.m_BG_NORMAL, !bHurdle);
			NKCUtil.SetGameobjectActive(this.m_BG_HURDLE, bHurdle);
		}

		// Token: 0x060063A8 RID: 25512 RVA: 0x001F7BE4 File Offset: 0x001F5DE4
		private void Update()
		{
			if (!base.IsOpen)
			{
				return;
			}
			if (this.m_fPrevUpdateTime + 1f < Time.time)
			{
				this.m_fPrevUpdateTime = Time.time;
				this.UpdateDiveResetUIOnlyTime(this.m_DiveResetTicketChargeDate);
			}
		}

		// Token: 0x060063A9 RID: 25513 RVA: 0x001F7C1C File Offset: 0x001F5E1C
		private NKMDiveTemplet GetSelectedDiveTemplet()
		{
			if (this.m_eventDiveTemplet != null)
			{
				if (this.m_SelectedIndex >= 0)
				{
					return this.m_eventDiveTemplet;
				}
				return null;
			}
			else
			{
				if (this.m_SelectedIndex >= 0 && this.m_lstDiveTemplet.Count > this.m_SelectedIndex)
				{
					return this.m_lstDiveTemplet[this.m_SelectedIndex];
				}
				return null;
			}
		}

		// Token: 0x060063AA RID: 25514 RVA: 0x001F7C74 File Offset: 0x001F5E74
		public static NKCUIDiveReady InitUI()
		{
			NKCUIDiveReady nkcuidiveReady = NKCUIManager.OpenUI<NKCUIDiveReady>("NKM_UI_DIVE");
			if (nkcuidiveReady != null)
			{
				if (nkcuidiveReady.gameObject)
				{
					nkcuidiveReady.gameObject.SetActive(false);
				}
				nkcuidiveReady.m_LoopScrollRect.dOnGetObject += nkcuidiveReady.GetDiveReadySlot;
				nkcuidiveReady.m_LoopScrollRect.dOnReturnObject += nkcuidiveReady.ReturnDiveReadySlot;
				nkcuidiveReady.m_LoopScrollRect.dOnProvideData += nkcuidiveReady.ProvideDiveReadySlotData;
				NKCUtil.SetScrollHotKey(nkcuidiveReady.m_LoopScrollRect, null);
				for (int i = 0; i < nkcuidiveReady.m_lstNKCUIDiveReadySquadSlot.Count; i++)
				{
					NKCUIDiveReadySquadSlot nkcuidiveReadySquadSlot = nkcuidiveReady.m_lstNKCUIDiveReadySquadSlot[i];
					nkcuidiveReadySquadSlot.SetSelectedEvent(new NKCUIDiveReadySquadSlot.OnSelectedDiveReadySquadSlot(nkcuidiveReady.OnSelectedSquad));
					NKCUtil.SetGameobjectActive(nkcuidiveReadySquadSlot, true);
					nkcuidiveReadySquadSlot.SetUnSelected();
				}
				List<NKMDiveTemplet> list = new List<NKMDiveTemplet>();
				foreach (NKMDiveTemplet nkmdiveTemplet in NKCDiveManager.SortedTemplates)
				{
					if (!nkmdiveTemplet.IsEventDive && nkmdiveTemplet.EnableByTag)
					{
						list.Add(nkmdiveTemplet);
					}
				}
				nkcuidiveReady.m_lstDiveTemplet = list;
				int num = Math.Max(3, 3);
				for (int i = 0; i < num; i++)
				{
					nkcuidiveReady.m_lstNKCUISlot.Add(NKCUISlot.GetNewInstance(nkcuidiveReady.m_NKM_UI_DIVE_INFO_REWARD_CONTENT.transform));
					nkcuidiveReady.m_lstNKCUISlot[i].transform.localScale = new Vector3(1f, 1f, 1f);
					NKCUtil.SetGameobjectActive(nkcuidiveReady.m_lstNKCUISlot[i], true);
				}
				nkcuidiveReady.m_NKM_UI_DIVE_INFO_DIVE_BUTTON.PointerClick.RemoveAllListeners();
				nkcuidiveReady.m_NKM_UI_DIVE_INFO_DIVE_BUTTON.PointerClick.AddListener(new UnityAction(nkcuidiveReady.OnClickDive));
				nkcuidiveReady.m_NKM_UI_DIVE_INFO_REDIVE_BUTTON.PointerClick.RemoveAllListeners();
				nkcuidiveReady.m_NKM_UI_DIVE_INFO_REDIVE_BUTTON.PointerClick.AddListener(new UnityAction(nkcuidiveReady.OnClickDive));
				nkcuidiveReady.m_UINPCOperatorChloe.Init(false);
				nkcuidiveReady.m_csbtnReset.PointerClick.RemoveAllListeners();
				nkcuidiveReady.m_csbtnReset.PointerClick.AddListener(new UnityAction(nkcuidiveReady.OnClickDiveReset));
				nkcuidiveReady.m_tglJump.OnValueChanged.RemoveAllListeners();
				nkcuidiveReady.m_tglJump.OnValueChanged.AddListener(new UnityAction<bool>(nkcuidiveReady.OnValueChangedJump));
				nkcuidiveReady.m_tglJump.m_bGetCallbackWhileLocked = true;
				nkcuidiveReady.m_btnJumpPopupInfo.PointerDown.RemoveAllListeners();
				nkcuidiveReady.m_btnJumpPopupInfo.PointerDown.AddListener(new UnityAction<PointerEventData>(nkcuidiveReady.OnClickJumpInfo));
				nkcuidiveReady.m_btnJumpPlusInfo.PointerDown.RemoveAllListeners();
				nkcuidiveReady.m_btnJumpPlusInfo.PointerDown.AddListener(new UnityAction<PointerEventData>(nkcuidiveReady.OnClickJumpPlusInfo));
				nkcuidiveReady.m_btnJumpDiscountInfo.PointerDown.RemoveAllListeners();
				nkcuidiveReady.m_btnJumpDiscountInfo.PointerDown.AddListener(new UnityAction<PointerEventData>(nkcuidiveReady.OnClickJumpDiscountInfo));
				nkcuidiveReady.m_btnJumpPopupClose.PointerClick.RemoveAllListeners();
				nkcuidiveReady.m_btnJumpPopupClose.PointerClick.AddListener(new UnityAction(nkcuidiveReady.OnClickJumpClose));
				NKCUIOperationSkip operationSkip = nkcuidiveReady.m_operationSkip;
				if (operationSkip != null)
				{
					operationSkip.Init(new NKCUIOperationSkip.OnCountUpdated(nkcuidiveReady.UpdateDiveResourceCount), new UnityAction(nkcuidiveReady.OnCloseSafeMining));
				}
				NKCUtil.SetButtonClickDelegate(nkcuidiveReady.m_csbtnDeckEdit, new UnityAction(nkcuidiveReady.OnSelectedSquad));
			}
			return nkcuidiveReady;
		}

		// Token: 0x060063AB RID: 25515 RVA: 0x001F7FD0 File Offset: 0x001F61D0
		private void OnClickJumpInfo(PointerEventData eventData)
		{
			NKCUIPopUpGuide.Instance.Open("ARTICLE_DIVE_SEARCH", 1);
		}

		// Token: 0x060063AC RID: 25516 RVA: 0x001F7FE4 File Offset: 0x001F61E4
		private void OnClickJumpPlusInfo(PointerEventData eventData)
		{
			NKCUITooltip.TextData textData = new NKCUITooltip.TextData(NKCStringTable.GetString("SI_PF_DIVE_JUMP_DESC01", false));
			NKCUITooltip.Instance.Open(textData, new Vector2?(eventData.position));
		}

		// Token: 0x060063AD RID: 25517 RVA: 0x001F8018 File Offset: 0x001F6218
		private void OnClickJumpDiscountInfo(PointerEventData eventData)
		{
			NKCUITooltip.TextData textData = new NKCUITooltip.TextData(NKCStringTable.GetString("SI_PF_DIVE_JUMP_DESC02", false));
			NKCUITooltip.Instance.Open(textData, new Vector2?(eventData.position));
		}

		// Token: 0x060063AE RID: 25518 RVA: 0x001F804C File Offset: 0x001F624C
		private void OnClickJumpClose()
		{
			this.OnValueChangedJump(false);
		}

		// Token: 0x060063AF RID: 25519 RVA: 0x001F8058 File Offset: 0x001F6258
		private void OnValueChangedJump(bool bValue)
		{
			if (this.m_tglJump.m_bLock)
			{
				NKCUIManager.NKCPopupMessage.Open(new PopupMessage(NKCUtilString.GET_STRING_CONTENTS_UNLOCK_CLEAR_STAGE, NKCPopupMessage.eMessagePosition.Top, 0f, true, false, false));
				return;
			}
			NKMDiveTemplet selectedDiveTemplet = this.GetSelectedDiveTemplet();
			if (selectedDiveTemplet == null || !selectedDiveTemplet.IsEventDive)
			{
				NKCUtil.SetGameobjectActive(this.m_objJumpPopup, false);
				this.m_tglJump.Select(false, true, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objJumpPopup, bValue);
			this.m_tglJump.Select(bValue, true, false);
			this.UpdateDiveCost();
		}

		// Token: 0x060063B0 RID: 25520 RVA: 0x001F80DF File Offset: 0x001F62DF
		private void OnClickDiveReset()
		{
			NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_DIVE_RESET, NKCUtilString.GET_STRING_DIVE_RESET_CONFIRM, this.GetCurrDiveResetItemID(), 1, delegate()
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null && nkmuserData.m_DiveGameData != null)
				{
					NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_DIVE_ALREADY_STARTED, null, "");
					return;
				}
			}, null, false);
		}

		// Token: 0x060063B1 RID: 25521 RVA: 0x001F8120 File Offset: 0x001F6320
		private void OnClickDive()
		{
			NKMDiveTemplet selectedDiveTemplet = this.GetSelectedDiveTemplet();
			if (selectedDiveTemplet != null)
			{
				if (this.CheckSameSelectedAndOnGoing())
				{
					NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE().SetIntro(true);
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_DIVE, true);
					return;
				}
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				bool flag = false;
				if (nkmuserData != null)
				{
					if (nkmuserData.m_DiveGameData != null)
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(NKM_ERROR_CODE.NEC_FAIL_DIVE_ALREADY_STARTED), null, "");
						return;
					}
					flag = nkmuserData.CheckDiveClear(selectedDiveTemplet.StageID);
				}
				if (flag && this.m_eventDiveTemplet == null && this.m_operationSkip != null)
				{
					if (!this.m_operationSkip.gameObject.activeSelf)
					{
						this.ShowSafeMiningPanel(selectedDiveTemplet);
						return;
					}
					if (this.HaveEnoughResource(selectedDiveTemplet, false))
					{
						NKCPacketSender.Send_NKMPacket_DIVE_SKIP_REQ(selectedDiveTemplet.StageID, this.m_operationSkip.CurrentCount);
					}
					return;
				}
			}
			this.SendDiveReq();
		}

		// Token: 0x060063B2 RID: 25522 RVA: 0x001F81F8 File Offset: 0x001F63F8
		private void SendDiveReq()
		{
			NKMDiveTemplet selectedDiveTemplet = this.GetSelectedDiveTemplet();
			if (selectedDiveTemplet != null)
			{
				if (!this.HaveEnoughResource(selectedDiveTemplet, this.JumpSelected))
				{
					return;
				}
				List<int> list = new List<int>();
				for (int i = 0; i < NKMCommonConst.Deck.MaxDiveDeckCount; i++)
				{
					if (NKMMain.IsValidDeck(NKCScenManager.CurrentArmyData(), NKM_DECK_TYPE.NDT_DIVE, (byte)i) == NKM_ERROR_CODE.NEC_OK)
					{
						list.Add(i);
					}
				}
				NKCPacketSender.Send_NKMPacket_DIVE_START_REQ(this.m_cityID, selectedDiveTemplet.StageID, list, this.JumpSelected);
			}
		}

		// Token: 0x060063B3 RID: 25523 RVA: 0x001F8268 File Offset: 0x001F6468
		private bool HaveEnoughResource(NKMDiveTemplet cNKMDiveTemplet, bool bJump)
		{
			if (cNKMDiveTemplet == null)
			{
				return false;
			}
			int diveCost = NKCDiveManager.GetDiveCost(cNKMDiveTemplet, this.m_cityID, bJump);
			if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(cNKMDiveTemplet.StageReqItemId) < (long)diveCost)
			{
				NKCShopManager.OpenItemLackPopup(cNKMDiveTemplet.StageReqItemId, diveCost);
				return false;
			}
			return true;
		}

		// Token: 0x060063B4 RID: 25524 RVA: 0x001F82B0 File Offset: 0x001F64B0
		private bool IsDiveStartPossible()
		{
			NKMDiveTemplet selectedDiveTemplet = this.GetSelectedDiveTemplet();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			bool flag = false;
			if (nkmuserData != null && nkmuserData.m_DiveClearData != null)
			{
				flag = nkmuserData.m_DiveClearData.Contains(selectedDiveTemplet.StageID);
			}
			if (flag)
			{
				return false;
			}
			NKMArmyData cNKMArmyData = NKCScenManager.CurrentArmyData();
			byte b = 0;
			while ((int)b < NKMCommonConst.Deck.MaxDiveDeckCount)
			{
				if (NKMMain.IsValidDeck(cNKMArmyData, NKM_DECK_TYPE.NDT_DIVE, b) == NKM_ERROR_CODE.NEC_OK)
				{
					return true;
				}
				b += 1;
			}
			return false;
		}

		// Token: 0x060063B5 RID: 25525 RVA: 0x001F831C File Offset: 0x001F651C
		private void SetDivePossibleFX()
		{
			if (this.GetSelectedDiveTemplet() != null)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON_FX, this.IsDiveStartPossible());
			}
		}

		// Token: 0x060063B6 RID: 25526 RVA: 0x001F8337 File Offset: 0x001F6537
		private void OnDeckViewConfirm(NKMDeckIndex selectIndex)
		{
			this.SetDivePossibleFX();
			this.UpdateSquadSlots();
			NKCUIDeckViewer.CheckInstanceAndClose();
		}

		// Token: 0x060063B7 RID: 25527 RVA: 0x001F834A File Offset: 0x001F654A
		public override void UnHide()
		{
			base.UnHide();
			if (this.GetSelectedDiveTemplet() != null)
			{
				if (NKCScenManager.CurrentUserData() == null)
				{
					return;
				}
				this.UpdateDiveInfo();
			}
		}

		// Token: 0x060063B8 RID: 25528 RVA: 0x001F8368 File Offset: 0x001F6568
		public void Refresh()
		{
			LoopScrollRect loopScrollRect = this.m_LoopScrollRect;
			if (loopScrollRect != null)
			{
				loopScrollRect.RefreshCells(false);
			}
			this.UpdateDiveInfo();
		}

		// Token: 0x060063B9 RID: 25529 RVA: 0x001F8384 File Offset: 0x001F6584
		private void SelectSquad()
		{
			if (this.GetSelectedDiveTemplet() != null)
			{
				NKCUIDeckViewer.DeckViewerOption options = default(NKCUIDeckViewer.DeckViewerOption);
				options.MenuName = NKCUtilString.GET_STRING_SELECT_SQUAD;
				options.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.DeckSetupOnly;
				options.dOnSideMenuButtonConfirm = new NKCUIDeckViewer.DeckViewerOption.OnDeckSideButtonConfirm(this.OnDeckViewConfirm);
				options.DeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_DIVE, 0);
				options.dOnBackButton = new NKCUIDeckViewer.DeckViewerOption.OnBackButton(NKCUIDeckViewer.CheckInstanceAndClose);
				options.SelectLeaderUnitOnOpen = false;
				options.bEnableDefaultBackground = true;
				options.bUpsideMenuHomeButton = false;
				options.StageBattleStrID = string.Empty;
				NKCUIDeckViewer.Instance.Open(options, true);
			}
		}

		// Token: 0x060063BA RID: 25530 RVA: 0x001F841C File Offset: 0x001F661C
		private void SendDiveGiveUp()
		{
			NKMPacket_DIVE_GIVE_UP_REQ packet = new NKMPacket_DIVE_GIVE_UP_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060063BB RID: 25531 RVA: 0x001F8442 File Offset: 0x001F6642
		public void OnRecv(NKMPacket_DIVE_GIVE_UP_ACK cNKMPacket_DIVE_GIVE_UP_ACK)
		{
			this.m_LoopScrollRect.RefreshCells(false);
			this.UpdateDiveInfo();
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x001F8458 File Offset: 0x001F6658
		public void OnRecv(NKMPacket_DIVE_EXPIRE_NOT cNKMPacket_DIVE_EXPIRE_NOT)
		{
			NKMDiveTemplet nkmdiveTemplet = NKMDiveTemplet.Find(cNKMPacket_DIVE_EXPIRE_NOT.stageID);
			if (nkmdiveTemplet == null)
			{
				return;
			}
			if (nkmdiveTemplet.IsEventDive)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().SetReservedDiveReverseAni(true);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WORLDMAP, true);
				return;
			}
			NKMDiveTemplet selectedDiveTemplet = this.GetSelectedDiveTemplet();
			if (selectedDiveTemplet != null && nkmdiveTemplet.StageID == selectedDiveTemplet.StageID)
			{
				this.CloseDiveInfo();
				this.m_SelectedIndex = -1;
				this.m_LoopScrollRect.RefreshCells(false);
			}
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x001F84CC File Offset: 0x001F66CC
		private void OnSelectedSquad()
		{
			if (this.GetSelectedDiveTemplet() != null)
			{
				if (this.CheckSameSelectedAndOnGoing())
				{
					return;
				}
				if (this.CheckDiffSelectedAndOnGoingExist())
				{
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_DIVE_GIVE_UP_AND_START, new NKCPopupOKCancel.OnButton(this.SendDiveGiveUp), null, false);
					return;
				}
				this.SelectSquad();
			}
		}

		// Token: 0x060063BE RID: 25534 RVA: 0x001F850C File Offset: 0x001F670C
		private void UpdateSquadSlots()
		{
			NKMDiveTemplet selectedDiveTemplet = this.GetSelectedDiveTemplet();
			if (selectedDiveTemplet != null)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				NKMUserData cNKMUserData = myUserData;
				UnlockInfo unlockInfo = new UnlockInfo(selectedDiveTemplet.StageUnlockReqType, selectedDiveTemplet.StageUnlockReqValue);
				bool flag = NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false);
				bool flag2 = false;
				if (myUserData != null && myUserData.m_DiveClearData != null)
				{
					flag2 = myUserData.m_DiveClearData.Contains(selectedDiveTemplet.StageID);
				}
				if (flag && !flag2)
				{
					int maxDiveDeckCount = NKMCommonConst.Deck.MaxDiveDeckCount;
					for (int i = 0; i < this.m_lstNKCUIDiveReadySquadSlot.Count; i++)
					{
						NKCUIDiveReadySquadSlot nkcuidiveReadySquadSlot = this.m_lstNKCUIDiveReadySquadSlot[i];
						if (nkcuidiveReadySquadSlot != null)
						{
							if (i < maxDiveDeckCount)
							{
								NKCUtil.SetGameobjectActive(nkcuidiveReadySquadSlot, true);
								NKMDeckIndex nkmdeckIndex = new NKMDeckIndex(NKM_DECK_TYPE.NDT_DIVE, i);
								NKMDeckData deckData = NKCScenManager.CurrentArmyData().GetDeckData(nkmdeckIndex);
								if (NKMMain.IsValidDeck(NKCScenManager.CurrentArmyData(), nkmdeckIndex) == NKM_ERROR_CODE.NEC_OK || (deckData != null && deckData.GetState() == NKM_DECK_STATE.DECK_STATE_DIVE))
								{
									nkcuidiveReadySquadSlot.SetSelected(nkmdeckIndex);
								}
								else
								{
									nkcuidiveReadySquadSlot.SetUnSelected();
								}
							}
							else
							{
								NKCUtil.SetGameobjectActive(nkcuidiveReadySquadSlot, false);
							}
						}
					}
					return;
				}
				for (int j = 0; j < this.m_lstNKCUIDiveReadySquadSlot.Count; j++)
				{
					NKCUIDiveReadySquadSlot nkcuidiveReadySquadSlot2 = this.m_lstNKCUIDiveReadySquadSlot[j];
					if (nkcuidiveReadySquadSlot2 != null)
					{
						NKCUtil.SetGameobjectActive(nkcuidiveReadySquadSlot2, false);
					}
				}
			}
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x001F8654 File Offset: 0x001F6854
		public RectTransform GetDiveReadySlot(int index)
		{
			NKCUIDiveReadySlot nkcuidiveReadySlot;
			if (this.m_stkNKCUIDiveReadySlot.Count > 0)
			{
				nkcuidiveReadySlot = this.m_stkNKCUIDiveReadySlot.Pop();
			}
			else
			{
				nkcuidiveReadySlot = NKCUIDiveReadySlot.GetNewInstance(this.m_NKM_UI_DIVE_LIST_Content.transform, new NKCUIDiveReadySlot.OnSelectedDiveReadySlot(this.OnSelectedDiveReadySlot));
			}
			if (nkcuidiveReadySlot != null)
			{
				this.m_lstNKCUIDiveReadySlot.Add(nkcuidiveReadySlot);
				return nkcuidiveReadySlot.GetComponent<RectTransform>();
			}
			return null;
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x001F86BC File Offset: 0x001F68BC
		public void ReturnDiveReadySlot(Transform tr)
		{
			NKCUIDiveReadySlot component = tr.GetComponent<NKCUIDiveReadySlot>();
			this.m_lstNKCUIDiveReadySlot.Remove(component);
			this.m_stkNKCUIDiveReadySlot.Push(component);
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x001F86FC File Offset: 0x001F68FC
		public void ProvideDiveReadySlotData(Transform tr, int index)
		{
			NKCUIDiveReadySlot component = tr.GetComponent<NKCUIDiveReadySlot>();
			if (component != null)
			{
				if (this.m_eventDiveTemplet != null)
				{
					component.SetUI(index, this.m_eventDiveTemplet, this.m_cityID);
				}
				else
				{
					NKMDiveTemplet cNKMDiveTemplet = this.m_lstDiveTemplet[index];
					component.SetUI(index, cNKMDiveTemplet, -1);
				}
				component.SetSelected(this.m_SelectedIndex == index);
			}
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x001F875C File Offset: 0x001F695C
		private void OnSelectedDiveReadySlot(NKCUIDiveReadySlot cNKCUIDiveReadySlotSelected)
		{
			for (int i = 0; i < this.m_lstNKCUIDiveReadySlot.Count; i++)
			{
				NKCUIDiveReadySlot nkcuidiveReadySlot = this.m_lstNKCUIDiveReadySlot[i];
				nkcuidiveReadySlot.SetSelected(nkcuidiveReadySlot == cNKCUIDiveReadySlotSelected);
			}
			this.m_SelectedIndex = cNKCUIDiveReadySlotSelected.GetIndex();
			this.OpenDiveInfo(cNKCUIDiveReadySlotSelected);
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x001F87AC File Offset: 0x001F69AC
		private bool CheckSameSelectedAndOnGoing()
		{
			NKMDiveGameData diveGameData = NKCScenManager.GetScenManager().GetMyUserData().m_DiveGameData;
			if (diveGameData == null || diveGameData.Floor.Templet == null)
			{
				return false;
			}
			if (this.m_SelectedIndex < 0)
			{
				return false;
			}
			NKMDiveTemplet selectedDiveTemplet = this.GetSelectedDiveTemplet();
			if (selectedDiveTemplet != null && diveGameData.Floor.Templet.StageID == selectedDiveTemplet.StageID)
			{
				if (!diveGameData.Floor.Templet.IsEventDive)
				{
					return true;
				}
				int cityIDByEventData = NKCScenManager.CurrentUserData().m_WorldmapData.GetCityIDByEventData(NKM_WORLDMAP_EVENT_TYPE.WET_DIVE, diveGameData.DiveUid);
				if (this.m_cityID == cityIDByEventData)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x001F8840 File Offset: 0x001F6A40
		private bool CheckDiffSelectedAndOnGoingExist()
		{
			NKMDiveGameData diveGameData = NKCScenManager.GetScenManager().GetMyUserData().m_DiveGameData;
			if (diveGameData == null || diveGameData.Floor.Templet == null)
			{
				return false;
			}
			if (this.m_SelectedIndex < 0)
			{
				return false;
			}
			NKMDiveTemplet selectedDiveTemplet = this.GetSelectedDiveTemplet();
			if (selectedDiveTemplet != null)
			{
				if (diveGameData.Floor.Templet.StageID != selectedDiveTemplet.StageID)
				{
					return true;
				}
				if (diveGameData.Floor.Templet.IsEventDive)
				{
					int cityIDByEventData = NKCScenManager.CurrentUserData().m_WorldmapData.GetCityIDByEventData(NKM_WORLDMAP_EVENT_TYPE.WET_DIVE, diveGameData.DiveUid);
					if (this.m_cityID != cityIDByEventData)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x001F88D4 File Offset: 0x001F6AD4
		private void UpdateDiveInfo()
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			bool flag = this.CheckSameSelectedAndOnGoing();
			this.m_tglJump.Select(false, false, false);
			this.UpdateSquadSlots();
			NKMDiveTemplet selectedDiveTemplet = this.GetSelectedDiveTemplet();
			if (selectedDiveTemplet != null)
			{
				this.SetBG(selectedDiveTemplet.StageType == NKM_DIVE_STAGE_TYPE.NDST_HARD);
				this.SetDiveInfoBG(selectedDiveTemplet.StageType == NKM_DIVE_STAGE_TYPE.NDST_HARD);
				NKMUserData cNKMUserData = myUserData;
				UnlockInfo unlockInfo = new UnlockInfo(selectedDiveTemplet.StageUnlockReqType, selectedDiveTemplet.StageUnlockReqValue);
				bool flag2 = NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, unlockInfo, false);
				bool flag3 = false;
				if (myUserData.m_DiveClearData != null)
				{
					flag3 = myUserData.m_DiveClearData.Contains(selectedDiveTemplet.StageID);
				}
				if (selectedDiveTemplet.IsEventDive)
				{
					this.m_NKM_UI_DIVE_INFO_FIRST_REWARD_TEXT.text = NKCUtilString.GET_STRING_DIVE_READY_EXPLORE_REWARD;
				}
				else if (flag3)
				{
					this.m_NKM_UI_DIVE_INFO_FIRST_REWARD_TEXT.text = NKCUtilString.GET_STRING_DIVE_READY_SAFE_MINING_REWARD;
				}
				else
				{
					this.m_NKM_UI_DIVE_INFO_FIRST_REWARD_TEXT.text = NKCUtilString.GET_STRING_DIVE_READY_FIRST_REWARD;
				}
				bool bValue = NKMOpenTagManager.IsOpened("DIVE_SKIP") && selectedDiveTemplet.IsEventDive && flag2;
				NKCUtil.SetGameobjectActive(this.m_objJumpToggle, bValue);
				this.m_tglJump.UnLock(false);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_DENIED, !flag2);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_CLEARED_ICON, flag3);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_CLEARED, flag3);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_SQUAD_TEXT, !flag3);
				if (flag2)
				{
					NKCUtil.SetGameobjectActive(this.m_lbLocked, false);
					this.m_NKM_UI_DIVE_INFO_LEVEL_COUNT.text = selectedDiveTemplet.StageLevel.ToString();
					this.m_NKM_UI_DIVE_INFO_AREA_COUNT.text = selectedDiveTemplet.RandomSetCount.ToString();
					if (flag)
					{
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_REDIVE_BUTTON, true);
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON, false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_REDIVE_BUTTON, false);
						NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON.gameObject, flag2);
						NKCUtil.SetLabelText(this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON_TEXT, flag3 ? NKCUtilString.GET_STRING_DIVE_SAFE_MINING : NKCUtilString.GET_STRING_DIVE_GO);
						this.UpdateDiveButtonState(flag3, selectedDiveTemplet, myUserData);
						this.UpdateDiveCost();
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lbLocked, true);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON.gameObject, false);
					NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_REDIVE_BUTTON, false);
					this.m_NKM_UI_DIVE_INFO_LEVEL_COUNT.text = "?";
					this.m_NKM_UI_DIVE_INFO_AREA_COUNT.text = "?";
				}
				this.m_NKM_UI_DIVE_INFO_TITLE_TEXT.text = selectedDiveTemplet.Get_STAGE_NAME();
				this.m_NKM_UI_DIVE_INFO_SUBTITLE_TEXT.text = selectedDiveTemplet.Get_STAGE_NAME_SUB();
				for (int i = 0; i < this.m_lstNKCUISlot.Count; i++)
				{
					bool flag4 = false;
					if (!flag3)
					{
						if (i < selectedDiveTemplet.FirstRewardList.Count && selectedDiveTemplet.FirstRewardList[i].FIRSTREWARD_TYPE != NKM_REWARD_TYPE.RT_NONE)
						{
							NKCUtil.SetGameobjectActive(this.m_lstNKCUISlot[i], true);
							NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeRewardTypeData(selectedDiveTemplet.FirstRewardList[i].FIRSTREWARD_TYPE, selectedDiveTemplet.FirstRewardList[i].FIRSTREWARD_ID, selectedDiveTemplet.FirstRewardList[i].FIRSTREWARD_QUANTITY, 0);
							this.m_lstNKCUISlot[i].SetData(data, true, null);
							this.m_lstNKCUISlot[i].SetCompleteMark(flag3);
							flag4 = true;
						}
					}
					else if (i < selectedDiveTemplet.SafeRewards.Count && selectedDiveTemplet.SafeRewards[i].RewardType != NKM_REWARD_TYPE.RT_NONE)
					{
						NKCUtil.SetGameobjectActive(this.m_lstNKCUISlot[i], true);
						NKCUISlot.SlotData data2 = NKCUISlot.SlotData.MakeRewardTypeData(selectedDiveTemplet.SafeRewards[i].RewardType, selectedDiveTemplet.SafeRewards[i].RewardId, selectedDiveTemplet.SafeRewards[i].RewardQuantity, 0);
						this.m_lstNKCUISlot[i].SetData(data2, true, null);
						this.m_lstNKCUISlot[i].SetCompleteMark(false);
						flag4 = true;
					}
					if (!flag4)
					{
						NKCUtil.SetGameobjectActive(this.m_lstNKCUISlot[i], false);
					}
				}
				if (!flag)
				{
					this.SetDivePossibleFX();
				}
				this.UpdateUpsideResourceList(selectedDiveTemplet.StageReqItemId);
			}
			else
			{
				this.SetBG(false);
				this.SetDiveInfoBG(false);
				NKCUtil.SetGameobjectActive(this.m_objJumpToggle, false);
			}
			if (this.m_operationSkip != null)
			{
				this.m_operationSkip.Close();
			}
		}

		// Token: 0x060063C6 RID: 25542 RVA: 0x001F8D18 File Offset: 0x001F6F18
		private void UpdateDiveCost()
		{
			NKMDiveTemplet selectedDiveTemplet = this.GetSelectedDiveTemplet();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			int diveCost = NKCDiveManager.GetDiveCost(selectedDiveTemplet, this.m_cityID, this.JumpSelected);
			NKCUtil.SetLabelText(this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON_COUNT, diveCost.ToString());
			if (selectedDiveTemplet != null)
			{
				Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(selectedDiveTemplet.StageReqItemId);
				NKCUtil.SetImageSprite(this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON_ICON, orLoadMiscItemSmallIcon, false);
			}
			if (selectedDiveTemplet != null && this.m_objJumpPopup.activeSelf)
			{
				this.m_slotJumpBasic.SetData(selectedDiveTemplet.StageReqItemId, selectedDiveTemplet.StageReqItemCount, nkmuserData.m_InventoryData.GetCountMiscItem(selectedDiveTemplet.StageReqItemId), true, true, false);
				this.m_slotJumpPlus.SetData(NKMDiveTemplet.DiveStormCostMiscId, selectedDiveTemplet.GetDiveJumpPlusCost(), nkmuserData.m_InventoryData.GetCountMiscItem(NKMDiveTemplet.DiveStormCostMiscId), true, true, false);
				this.m_slotJumpDiscount.SetData(selectedDiveTemplet.StageReqItemId, NKCDiveManager.GetDiveDiscountCost(this.m_cityID, selectedDiveTemplet.StageReqItemCount + selectedDiveTemplet.GetDiveJumpPlusCost()), nkmuserData.m_InventoryData.GetCountMiscItem(selectedDiveTemplet.StageReqItemId), true, true, false);
			}
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x001F8E1C File Offset: 0x001F701C
		private void UpdateDiveButtonState(bool bCheckClear, NKMDiveTemplet cNKMDiveTemplet, NKMUserData cNKMUserData)
		{
			if (this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON == null)
			{
				return;
			}
			if (!bCheckClear)
			{
				this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON.UnLock();
				return;
			}
			bool flag = true;
			if (cNKMDiveTemplet != null && cNKMUserData != null)
			{
				flag = ((long)cNKMDiveTemplet.StageReqItemCount <= cNKMUserData.m_InventoryData.GetCountMiscItem(cNKMDiveTemplet.StageReqItemId));
			}
			if (flag)
			{
				this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON.UnLock();
				return;
			}
			this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON.Lock();
		}

		// Token: 0x060063C8 RID: 25544 RVA: 0x001F8E88 File Offset: 0x001F7088
		private void OpenDiveInfo(NKCUIDiveReadySlot cNKCUIDiveReadySlot)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (cNKCUIDiveReadySlot == null || myUserData == null)
			{
				this.CloseDiveInfo();
				return;
			}
			this.OpenDiveInfo();
		}

		// Token: 0x060063C9 RID: 25545 RVA: 0x001F8EBC File Offset: 0x001F70BC
		private void OpenDiveInfo()
		{
			bool flag = false;
			if (!this.m_NKM_UI_DIVE_INFO.activeSelf)
			{
				flag = true;
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO, true);
			if (!flag)
			{
				this.m_NKM_UI_DIVE_INFO_Animator.Play("NKM_UI_DIVE_INFO_INTRO");
			}
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON_FX, false);
			this.UpdateDiveInfo();
		}

		// Token: 0x060063CA RID: 25546 RVA: 0x001F8F0B File Offset: 0x001F710B
		private void CloseDiveInfo()
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_DIVE_INFO, false);
			this.SetBG(false);
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x001F8F20 File Offset: 0x001F7120
		private void AutoSelect()
		{
			NKCScenManager.GetScenManager().GetMyUserData();
			int selectedIndex;
			if (this.m_eventDiveTemplet == null)
			{
				int num;
				NKCDiveManager.GetCurrNormalDiveTemplet(out num);
				selectedIndex = num;
			}
			else
			{
				selectedIndex = 0;
			}
			this.m_SelectedIndex = selectedIndex;
			this.m_LoopScrollRect.velocity = new Vector2(0f, 0f);
			this.m_LoopScrollRect.SetIndexPosition(0);
			if (this.m_SelectedIndex >= 0)
			{
				int num2 = Mathf.Max(0, this.m_SelectedIndex - 15);
				float time = Mathf.Max(1f, (float)(this.m_SelectedIndex - num2) / 10f);
				this.m_LoopScrollRect.SetIndexPosition(num2);
				this.m_LoopScrollRect.ScrollToCell(this.m_SelectedIndex, time, LoopScrollRect.ScrollTarget.Center, new UnityAction(this.OnCompleteScroll));
				this.OpenDiveInfo();
			}
		}

		// Token: 0x060063CC RID: 25548 RVA: 0x001F8FE4 File Offset: 0x001F71E4
		private void OnCompleteScroll()
		{
			for (int i = 0; i < this.m_lstNKCUIDiveReadySlot.Count; i++)
			{
				NKCUIDiveReadySlot nkcuidiveReadySlot = this.m_lstNKCUIDiveReadySlot[i];
				if (nkcuidiveReadySlot != null && nkcuidiveReadySlot.IsSelected())
				{
					nkcuidiveReadySlot.PlayScrollArriveEffect();
					return;
				}
			}
		}

		// Token: 0x060063CD RID: 25549 RVA: 0x001F902C File Offset: 0x001F722C
		public void Open(int cityID, int eventDiveID, DateTime _ResetTicketChargeDate)
		{
			this.m_DiveResetTicketChargeDate = _ResetTicketChargeDate;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			if (this.m_bFirstOpen)
			{
				this.m_LoopScrollRect.PrepareCells(0);
				this.m_bFirstOpen = false;
			}
			for (int i = 0; i < this.m_lstNKCUIDiveReadySquadSlot.Count; i++)
			{
				NKCUIDiveReadySquadSlot nkcuidiveReadySquadSlot = this.m_lstNKCUIDiveReadySquadSlot[i];
				if (nkcuidiveReadySquadSlot != null)
				{
					nkcuidiveReadySquadSlot.SetUnSelected();
				}
			}
			if (cityID > 0 && eventDiveID > 0)
			{
				this.m_cityID = cityID;
				this.m_LoopScrollRect.TotalCount = 1;
				this.m_eventDiveTemplet = NKMDiveTemplet.Find(eventDiveID);
			}
			else
			{
				this.m_LoopScrollRect.TotalCount = this.m_lstDiveTemplet.Count;
				this.m_cityID = 0;
				this.m_eventDiveTemplet = null;
			}
			NKCUtil.SetGameobjectActive(this.m_objJumpPopup, false);
			this.m_SelectedIndex = -1;
			for (int i = 0; i < this.m_lstNKCUIDiveReadySlot.Count; i++)
			{
				this.m_lstNKCUIDiveReadySlot[i].SetSelected(false);
			}
			this.CloseDiveInfo();
			this.UpdateDiveResetUI(_ResetTicketChargeDate);
			base.UIOpened(true);
			this.AutoSelect();
			bool bMute = this.CheckTutorial();
			this.m_UINPCOperatorChloe.PlayAni(NPC_ACTION_TYPE.START, bMute);
		}

		// Token: 0x060063CE RID: 25550 RVA: 0x001F9154 File Offset: 0x001F7354
		private int GetCurrDiveResetItemID()
		{
			int result = 1041;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return result;
			}
			if (!nkmuserData.CheckPrice(1, 1041) && nkmuserData.CheckPrice(1, 1042))
			{
				result = 1042;
			}
			return result;
		}

		// Token: 0x060063CF RID: 25551 RVA: 0x001F9198 File Offset: 0x001F7398
		private long GetResetItemTotalCount()
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return 0L;
			}
			return nkmuserData.m_InventoryData.GetCountMiscItem(this.GetCurrDiveResetItemID());
		}

		// Token: 0x060063D0 RID: 25552 RVA: 0x001F91C4 File Offset: 0x001F73C4
		private void UpdateDiveResetUI(DateTime _ResetTicketChargeDate)
		{
			NKCUtil.SetGameobjectActive(this.m_objDiveReset, this.m_eventDiveTemplet == null);
			if (this.m_objDiveReset != null && this.m_objDiveReset.activeSelf)
			{
				NKCUtil.SetLabelText(this.m_lbResetCost, this.GetResetItemTotalCount().ToString());
				NKCUtil.SetImageSprite(this.m_imgResetCostIcon, NKCResourceUtility.GetOrLoadMiscItemSmallIcon(this.GetCurrDiveResetItemID()), false);
			}
			this.UpdateDiveResetUIOnlyTime(_ResetTicketChargeDate);
		}

		// Token: 0x060063D1 RID: 25553 RVA: 0x001F9237 File Offset: 0x001F7437
		private void UpdateDiveResetUIOnlyTime(DateTime _ResetTicketChargeDate)
		{
			if (this.m_objDiveReset != null && this.m_objDiveReset.activeSelf)
			{
				NKCUtil.SetLabelText(this.m_lbRemainTimeToReset, string.Format(NKCUtilString.GET_STRING_DIVE_REMAIN_TIME_TO_RESET, NKCUtilString.GetRemainTimeStringExWithoutEnd(_ResetTicketChargeDate)));
			}
		}

		// Token: 0x060063D2 RID: 25554 RVA: 0x001F9270 File Offset: 0x001F7470
		private void UpdateUpsideResourceList(int resourceID = 0)
		{
			this.m_upsideMenuResourceList.Clear();
			this.m_upsideMenuResourceList.Add(1);
			this.m_upsideMenuResourceList.Add(2);
			this.m_upsideMenuResourceList.Add(101);
			if (resourceID > 0)
			{
				this.m_upsideMenuResourceList.Insert(0, resourceID);
			}
			base.UpdateUpsideMenu();
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x001F92C4 File Offset: 0x001F74C4
		private void ShowSafeMiningPanel(NKMDiveTemplet cNKMDiveTemplet)
		{
			if (cNKMDiveTemplet != null)
			{
				NKCUtil.SetGameobjectActive(this.m_operationSkip, true);
				NKCUIOperationSkip operationSkip = this.m_operationSkip;
				if (operationSkip != null)
				{
					operationSkip.SetData(0, 0, cNKMDiveTemplet.StageReqItemId, cNKMDiveTemplet.StageReqItemCount, 1, 1, 99);
				}
				NKCUtil.SetLabelText(this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON_TEXT, NKCUtilString.GET_STRING_DIVE_SAFE_MINING_START);
				return;
			}
			NKCUIOperationSkip operationSkip2 = this.m_operationSkip;
			if (operationSkip2 == null)
			{
				return;
			}
			operationSkip2.Close();
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x001F9324 File Offset: 0x001F7524
		private void UpdateDiveResourceCount(int count)
		{
			if (count <= 0)
			{
				return;
			}
			NKMDiveTemplet selectedDiveTemplet = this.GetSelectedDiveTemplet();
			NKCScenManager.CurrentUserData();
			int num = NKCDiveManager.GetDiveCost(selectedDiveTemplet, this.m_cityID, this.JumpSelected) * count;
			NKCUtil.SetLabelText(this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON_COUNT, num.ToString());
		}

		// Token: 0x060063D5 RID: 25557 RVA: 0x001F9368 File Offset: 0x001F7568
		private void OnCloseSafeMining()
		{
			this.UpdateDiveCost();
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			NKMDiveTemplet selectedDiveTemplet = this.GetSelectedDiveTemplet();
			bool flag = false;
			if (nkmuserData != null && nkmuserData.m_DiveClearData != null && selectedDiveTemplet != null)
			{
				flag = nkmuserData.m_DiveClearData.Contains(selectedDiveTemplet.StageID);
			}
			if (flag)
			{
				NKCUtil.SetLabelText(this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON_TEXT, NKCUtilString.GET_STRING_DIVE_SAFE_MINING);
				return;
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_DIVE_INFO_DIVE_BUTTON_TEXT, NKCUtilString.GET_STRING_DIVE_GO);
		}

		// Token: 0x060063D6 RID: 25558 RVA: 0x001F93CE File Offset: 0x001F75CE
		public override void CloseInternal()
		{
			NKCSoundManager.StopAllSound(SOUND_TRACK.VOICE);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060063D7 RID: 25559 RVA: 0x001F93E4 File Offset: 0x001F75E4
		public override void OnBackButton()
		{
			if (this.m_operationSkip != null && this.m_operationSkip.gameObject.activeSelf)
			{
				this.m_operationSkip.Close();
				return;
			}
			NKCScenManager.GetScenManager().Get_NKC_SCEN_WORLDMAP().SetReservedDiveReverseAni(true);
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_WORLDMAP, true);
		}

		// Token: 0x060063D8 RID: 25560 RVA: 0x001F943A File Offset: 0x001F763A
		private bool CheckTutorial()
		{
			return NKCTutorialManager.TutorialRequired(TutorialPoint.DiveReady, true) > TutorialStep.None;
		}

		// Token: 0x04004F0E RID: 20238
		[Header("배경")]
		public GameObject m_NKM_UI_DIVE_BG_NORMAL;

		// Token: 0x04004F0F RID: 20239
		public GameObject m_NKM_UI_DIVE_BG_HURDLE;

		// Token: 0x04004F10 RID: 20240
		[Header("왼쪽")]
		public NKCUINPCOperatorChloe m_UINPCOperatorChloe;

		// Token: 0x04004F11 RID: 20241
		public GameObject m_objDiveReset;

		// Token: 0x04004F12 RID: 20242
		public NKCUIComStateButton m_csbtnReset;

		// Token: 0x04004F13 RID: 20243
		public Text m_lbRemainTimeToReset;

		// Token: 0x04004F14 RID: 20244
		public Image m_imgResetCostIcon;

		// Token: 0x04004F15 RID: 20245
		public Text m_lbResetCost;

		// Token: 0x04004F16 RID: 20246
		[Header("가운데 다이브 슬롯")]
		public LoopScrollRect m_LoopScrollRect;

		// Token: 0x04004F17 RID: 20247
		public GameObject m_NKM_UI_DIVE_LIST_Content;

		// Token: 0x04004F18 RID: 20248
		private List<NKCUIDiveReadySlot> m_lstNKCUIDiveReadySlot = new List<NKCUIDiveReadySlot>();

		// Token: 0x04004F19 RID: 20249
		private Stack<NKCUIDiveReadySlot> m_stkNKCUIDiveReadySlot = new Stack<NKCUIDiveReadySlot>();

		// Token: 0x04004F1A RID: 20250
		[Header("오른쪽 다이브 정보")]
		public GameObject m_NKM_UI_DIVE_INFO;

		// Token: 0x04004F1B RID: 20251
		public Animator m_NKM_UI_DIVE_INFO_Animator;

		// Token: 0x04004F1C RID: 20252
		public Text m_NKM_UI_DIVE_INFO_TITLE_TEXT;

		// Token: 0x04004F1D RID: 20253
		public Text m_NKM_UI_DIVE_INFO_SUBTITLE_TEXT;

		// Token: 0x04004F1E RID: 20254
		public GameObject m_NKM_UI_DIVE_INFO_CLEARED_ICON;

		// Token: 0x04004F1F RID: 20255
		public Text m_NKM_UI_DIVE_INFO_LEVEL_COUNT;

		// Token: 0x04004F20 RID: 20256
		public Text m_NKM_UI_DIVE_INFO_AREA_COUNT;

		// Token: 0x04004F21 RID: 20257
		public GameObject m_NKM_UI_DIVE_INFO_DENIED;

		// Token: 0x04004F22 RID: 20258
		public GameObject m_NKM_UI_DIVE_INFO_CLEARED;

		// Token: 0x04004F23 RID: 20259
		public GameObject m_NKM_UI_DIVE_INFO_DIVE_BUTTON_FX;

		// Token: 0x04004F24 RID: 20260
		public GameObject m_BG_HURDLE;

		// Token: 0x04004F25 RID: 20261
		public GameObject m_BG_NORMAL;

		// Token: 0x04004F26 RID: 20262
		public GameObject m_NKM_UI_DIVE_INFO_SQUAD_TEXT;

		// Token: 0x04004F27 RID: 20263
		public Text m_NKM_UI_DIVE_INFO_FIRST_REWARD_TEXT;

		// Token: 0x04004F28 RID: 20264
		public List<NKCUIDiveReadySquadSlot> m_lstNKCUIDiveReadySquadSlot = new List<NKCUIDiveReadySquadSlot>();

		// Token: 0x04004F29 RID: 20265
		public NKCUIComStateButton m_csbtnDeckEdit;

		// Token: 0x04004F2A RID: 20266
		public GameObject m_NKM_UI_DIVE_INFO_SQUAD_LIST;

		// Token: 0x04004F2B RID: 20267
		public GameObject m_NKM_UI_DIVE_INFO_REWARD_CONTENT;

		// Token: 0x04004F2C RID: 20268
		public Text m_NKM_UI_DIVE_INFO_DIVE_BUTTON_COUNT;

		// Token: 0x04004F2D RID: 20269
		public Image m_NKM_UI_DIVE_INFO_DIVE_BUTTON_ICON;

		// Token: 0x04004F2E RID: 20270
		public NKCUIComButton m_NKM_UI_DIVE_INFO_DIVE_BUTTON;

		// Token: 0x04004F2F RID: 20271
		public Text m_NKM_UI_DIVE_INFO_DIVE_BUTTON_TEXT;

		// Token: 0x04004F30 RID: 20272
		public NKCUIComButton m_NKM_UI_DIVE_INFO_REDIVE_BUTTON;

		// Token: 0x04004F31 RID: 20273
		public Text m_lbLocked;

		// Token: 0x04004F32 RID: 20274
		public NKCUIOperationSkip m_operationSkip;

		// Token: 0x04004F33 RID: 20275
		[Header("강습전")]
		public GameObject m_objJumpToggle;

		// Token: 0x04004F34 RID: 20276
		public NKCUIComToggle m_tglJump;

		// Token: 0x04004F35 RID: 20277
		public GameObject m_objJumpEvent;

		// Token: 0x04004F36 RID: 20278
		public GameObject m_objJumpPopup;

		// Token: 0x04004F37 RID: 20279
		public NKCUIComStateButton m_btnJumpPopupInfo;

		// Token: 0x04004F38 RID: 20280
		public NKCUIComStateButton m_btnJumpPopupClose;

		// Token: 0x04004F39 RID: 20281
		public NKCUIComStateButton m_btnJumpPlusInfo;

		// Token: 0x04004F3A RID: 20282
		public NKCUIComStateButton m_btnJumpDiscountInfo;

		// Token: 0x04004F3B RID: 20283
		public NKCUIItemCostSlot m_slotJumpBasic;

		// Token: 0x04004F3C RID: 20284
		public NKCUIItemCostSlot m_slotJumpPlus;

		// Token: 0x04004F3D RID: 20285
		public NKCUIItemCostSlot m_slotJumpDiscount;

		// Token: 0x04004F3E RID: 20286
		private int m_SelectedIndex = -1;

		// Token: 0x04004F3F RID: 20287
		private bool m_bFirstOpen = true;

		// Token: 0x04004F40 RID: 20288
		private int m_cityID;

		// Token: 0x04004F41 RID: 20289
		private NKMDiveTemplet m_eventDiveTemplet;

		// Token: 0x04004F42 RID: 20290
		private IReadOnlyList<NKMDiveTemplet> m_lstDiveTemplet;

		// Token: 0x04004F43 RID: 20291
		private List<NKCUISlot> m_lstNKCUISlot = new List<NKCUISlot>();

		// Token: 0x04004F44 RID: 20292
		private float m_fPrevUpdateTime;

		// Token: 0x04004F45 RID: 20293
		private DateTime m_DiveResetTicketChargeDate;

		// Token: 0x04004F46 RID: 20294
		private List<int> m_upsideMenuResourceList = new List<int>();

		// Token: 0x02001632 RID: 5682
		public class CompAscendDiveTemplet : IComparer<NKMDiveTemplet>
		{
			// Token: 0x0600AF75 RID: 44917 RVA: 0x0035C4FD File Offset: 0x0035A6FD
			public int Compare(NKMDiveTemplet x, NKMDiveTemplet y)
			{
				if (y.IndexID <= x.IndexID)
				{
					return 1;
				}
				return -1;
			}
		}
	}
}

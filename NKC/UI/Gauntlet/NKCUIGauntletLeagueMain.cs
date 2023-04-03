using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Pvp;
using Cs.Core.Util;
using Cs.Logging;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Gauntlet
{
	// Token: 0x02000B6F RID: 2927
	public class NKCUIGauntletLeagueMain : NKCUIBase
	{
		// Token: 0x060085E4 RID: 34276 RVA: 0x002D54CE File Offset: 0x002D36CE
		public static NKCUIManager.LoadedUIData OpenNewInstanceAsync()
		{
			return NKCUIManager.OpenNewInstanceAsync<NKCUIGauntletLeagueMain>("AB_UI_NKM_UI_GAUNTLET", "NKM_UI_GAUNTLET_LEAGUE_MAIN", NKCUIManager.eUIBaseRect.UIFrontCommon, null);
		}

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x060085E5 RID: 34277 RVA: 0x002D54E1 File Offset: 0x002D36E1
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x060085E6 RID: 34278 RVA: 0x002D54E4 File Offset: 0x002D36E4
		public override bool IgnoreBackButtonWhenOpen
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x060085E7 RID: 34279 RVA: 0x002D54E7 File Offset: 0x002D36E7
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x060085E8 RID: 34280 RVA: 0x002D54EA File Offset: 0x002D36EA
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_GAUNTLET;
			}
		}

		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x060085E9 RID: 34281 RVA: 0x002D54F1 File Offset: 0x002D36F1
		public override List<int> UpsideMenuShowResourceList
		{
			get
			{
				return new List<int>
				{
					101
				};
			}
		}

		// Token: 0x060085EA RID: 34282 RVA: 0x002D5500 File Offset: 0x002D3700
		public override void CloseInternal()
		{
			if (this.m_CharacterView != null)
			{
				this.m_CharacterView.CleanUp();
				this.m_CharacterView = null;
			}
			if (this.m_UserLeft != null)
			{
				this.m_UserLeft.Close();
			}
			if (this.m_UserRight != null)
			{
				this.m_UserRight.Close();
			}
			if (this.m_animatorStart != null)
			{
				this.m_animatorStart.Play("MAIN_OUTRO");
			}
		}

		// Token: 0x060085EB RID: 34283 RVA: 0x002D5574 File Offset: 0x002D3774
		public void Init()
		{
			this.m_prevRemainingSeconds = 0;
			this.m_ViewerOptions = default(NKCUIDeckViewer.DeckViewerOption);
			this.m_ViewerOptions.MenuName = "";
			this.m_ViewerOptions.eDeckviewerMode = NKCUIDeckViewer.DeckViewerMode.LeaguePvPMain;
			this.m_ViewerOptions.dOnSideMenuButtonConfirm = null;
			NKCUtil.SetGameobjectActive(this, false);
			if (this.m_CharacterView != null)
			{
				this.m_CharacterView.Init(null, delegate(PointerEventData cPointerEventData, NKCUICharacterView charView)
				{
				});
			}
			this.m_UserLeft.Init(new NKCUIGauntletLeagueMain.LeagueUserInfoUI.OnClickPickedUnit(this.OnClickSetLeaderUnit), null);
			this.m_UserRight.Init(new NKCUIGauntletLeagueMain.LeagueUserInfoUI.OnClickPickedUnit(this.OnClickBanPickedUnit), null);
			this.m_NKCLeaguePvpUnitSelectList.Init(null, new NKCLeaguePvpUnitSelectList.OnDeckUnitChangeClicked(this.OnDeckUnitChangeClicked), null, null);
			if (this.m_SelectConfirmButton != null)
			{
				this.m_SelectConfirmButton.PointerClick.RemoveAllListeners();
				this.m_SelectConfirmButton.PointerClick.AddListener(new UnityAction(this.OnClickSlotSelectConfirm));
			}
			if (this.m_LeaveRoom != null)
			{
				this.m_LeaveRoom.PointerClick.RemoveAllListeners();
				this.m_LeaveRoom.PointerClick.AddListener(new UnityAction(this.OnClickGiveup));
			}
			NKCUtil.SetGameobjectActive(this.m_SelectConfirmButton, false);
			NKCUtil.SetGameobjectActive(this.m_objWatingNotice, false);
			NKCUtil.SetGameobjectActive(this.m_objPickDisableScreen, false);
			NKCUtil.SetGameobjectActive(this.m_LeaveRoom, false);
			if (NKCLeaguePVPMgr.IsObserver())
			{
				this.m_rectCenterContent.offsetMax = new Vector2(0f, this.m_rectCenterContent.offsetMax.y);
			}
		}

		// Token: 0x060085EC RID: 34284 RVA: 0x002D5717 File Offset: 0x002D3917
		public void Open(DateTime endTime)
		{
			NKCUtil.SetGameobjectActive(this, true);
			this.m_animatorStart.Play("MAIN_INTRO");
			this.UpdatePickAnimation();
			base.UIOpened(true);
			this.RefreshDraftData(endTime, true);
		}

		// Token: 0x060085ED RID: 34285 RVA: 0x002D5745 File Offset: 0x002D3945
		public void ProcessBackButton()
		{
		}

		// Token: 0x060085EE RID: 34286 RVA: 0x002D5748 File Offset: 0x002D3948
		public void OnClickGiveup()
		{
			if (!NKCLeaguePVPMgr.CanLeaveRoom())
			{
				return;
			}
			if (!NKCLeaguePVPMgr.IsPrivate())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_GIVEUP_POPUP", false), delegate()
				{
					NKCLeaguePVPMgr.Send_NKMPacket_LEAGUE_PVP_GIVEUP_REQ();
				}, null, false);
				return;
			}
			if (NKCLeaguePVPMgr.IsObserver())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, "관전에서 나가시겠습니까? 텍스트", delegate()
				{
					NKCPrivatePVPRoomMgr.Send_NKMPacket_PRIVATE_PVP_EXIT_REQ();
				}, null, false);
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_PRIVATE_PVP_READY_CANCEL_TITLE, NKCUtilString.GET_STRING_PRIVATE_PVP_READY_CANCEL, delegate()
			{
				NKCLeaguePVPMgr.Send_NKMPacket_PRIVATE_PVP_EXIT_REQ();
			}, null, false);
		}

		// Token: 0x060085EF RID: 34287 RVA: 0x002D5803 File Offset: 0x002D3A03
		public void SetLeaveRoomState()
		{
			NKCUtil.SetGameobjectActive(this.m_LeaveRoom, NKCLeaguePVPMgr.CanLeaveRoom());
		}

		// Token: 0x060085F0 RID: 34288 RVA: 0x002D5818 File Offset: 0x002D3A18
		public void UpdatePickAnimation()
		{
			bool flag = NKCLeaguePVPMgr.GetLeftDraftTeamData().teamType == NKM_TEAM_TYPE.NTT_A1;
			string text = flag ? "PICK_LEFT_IN" : "PICK_RIGHT_IN";
			string text2 = flag ? "PICK_RIGHT_IN" : "PICK_LEFT_IN";
			bool flag2 = NKCLeaguePVPMgr.CanPickUnit(NKCLeaguePVPMgr.MyTeamType) || NKCLeaguePVPMgr.CanPickETC();
			NKCUtil.SetGameobjectActive(this.m_SelectConfirmButton, flag2);
			NKCUtil.SetGameobjectActive(this.m_objWatingNotice, !flag2);
			string text3 = "PICK_NONE";
			switch (NKCLeaguePVPMgr.DraftRoomData.roomState)
			{
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_1:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_3:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_5:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_7:
				text3 = text;
				break;
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_2:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_4:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_6:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_8:
				text3 = text2;
				break;
			case DRAFT_PVP_ROOM_STATE.BAN_OPPONENT:
				text3 = "PICK_BOTH_IN";
				NKCUtil.SetGameobjectActive(this.m_SelectConfirmButton, true);
				NKCUtil.SetGameobjectActive(this.m_objPickDisableScreen, true);
				NKCUtil.SetGameobjectActive(this.m_objWatingNotice, false);
				if (this.m_SelectConfirmButton != null)
				{
					this.m_SelectConfirmButton.PointerClick.RemoveAllListeners();
					this.m_SelectConfirmButton.PointerClick.AddListener(new UnityAction(this.OnClickOponentBanConfirm));
				}
				break;
			case DRAFT_PVP_ROOM_STATE.PICK_ETC:
				text3 = "PICK_BOTH_IN";
				NKCUtil.SetGameobjectActive(this.m_SelectConfirmButton, true);
				NKCUtil.SetGameobjectActive(this.m_objPickDisableScreen, false);
				NKCUtil.SetGameobjectActive(this.m_objWatingNotice, false);
				break;
			}
			if (!string.IsNullOrEmpty(text3))
			{
				this.m_animatorPick.Play(text3);
			}
		}

		// Token: 0x060085F1 RID: 34289 RVA: 0x002D597B File Offset: 0x002D3B7B
		public void RefreshDraftData(DateTime endTime, bool isStateChanged)
		{
			if (!base.IsOpen)
			{
				this.Open(endTime);
				return;
			}
			this.SetEndTime(endTime);
			this.UpdateUserInfo();
			this.UpdateCharacterView();
			this.UpdateSelectList();
		}

		// Token: 0x060085F2 RID: 34290 RVA: 0x002D59A8 File Offset: 0x002D3BA8
		public void SetEndTime(DateTime endTime)
		{
			this.m_endTime = endTime;
			this.m_prevRemainingSeconds = Math.Max(0, Convert.ToInt32((this.m_endTime - ServiceTime.Now).TotalSeconds));
		}

		// Token: 0x060085F3 RID: 34291 RVA: 0x002D59E8 File Offset: 0x002D3BE8
		public void UpdateUserInfo()
		{
			this.m_UserLeft.SetData(NKCLeaguePVPMgr.GetLeftDraftTeamData(), true, true, this.m_bSelectLeader, -1, this.m_currentSelectedLeaderIndex, NKCLeaguePVPMgr.IsPrivate());
			this.m_UserRight.SetData(NKCLeaguePVPMgr.GetRightDraftTeamData(), false, true, false, this.m_currentSelectedBanIndex, -1, NKCLeaguePVPMgr.IsPrivate());
			if (NKCLeaguePVPMgr.DraftRoomData.roomState != DRAFT_PVP_ROOM_STATE.PICK_ETC)
			{
				this.m_UserLeft.ShowPickEffect(false, false);
				this.m_UserRight.ShowPickEffect(false, false);
			}
		}

		// Token: 0x060085F4 RID: 34292 RVA: 0x002D5A60 File Offset: 0x002D3C60
		private void UpdateCharacterView()
		{
			if (NKCLeaguePVPMgr.DraftRoomData == null || NKCLeaguePVPMgr.DraftRoomData.selectedUnit == null || this.m_CharacterView == null)
			{
				return;
			}
			if (NKCLeaguePVPMgr.DraftRoomData.selectedUnit.skinId != 0)
			{
				NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(NKCLeaguePVPMgr.DraftRoomData.selectedUnit.skinId);
				this.m_CharacterView.SetCharacterIllust(skinTemplet, false, false, 0);
			}
			else if (NKCLeaguePVPMgr.DraftRoomData.selectedUnit.unitId != 0)
			{
				NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(NKCLeaguePVPMgr.DraftRoomData.selectedUnit.unitId);
				this.m_CharacterView.SetCharacterIllust(unitTempletBase, 0, false, false, 0);
			}
			NKMUnitData unitData = NKMDungeonManager.MakeUnitDataFromID(NKCLeaguePVPMgr.DraftRoomData.selectedUnit.unitId, -1L, NKCLeaguePVPMgr.DraftRoomData.selectedUnit.unitLevel, NKCLeaguePVPMgr.DraftRoomData.selectedUnit.limitBreakLevel, NKCLeaguePVPMgr.DraftRoomData.selectedUnit.skinId, 0);
			NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_BATTLE_READY, unitData, false, false);
		}

		// Token: 0x060085F5 RID: 34293 RVA: 0x002D5B4C File Offset: 0x002D3D4C
		private void UpdateSelectList()
		{
			if (this.m_NKCLeaguePvpUnitSelectList != null && this.m_NKCLeaguePvpUnitSelectList.m_LoopScrollRect != null)
			{
				this.m_NKCLeaguePvpUnitSelectList.m_LoopScrollRect.RefreshCells(false);
			}
			if (this.m_currentSelectedUnit != null && this.m_currentSelectedUnit.UnitSelectState != NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED)
			{
				this.m_currentSelectedUnit = null;
				this.m_currentSelectedUnitUID = 0L;
			}
		}

		// Token: 0x060085F6 RID: 34294 RVA: 0x002D5BB8 File Offset: 0x002D3DB8
		private void Update()
		{
			if (this.m_prevRemainingSeconds > 0)
			{
				int num = Math.Max(0, Convert.ToInt32((this.m_endTime - ServiceTime.Now).TotalSeconds));
				if (num != this.m_prevRemainingSeconds)
				{
					NKCUtil.SetLabelText(this.m_timeText, num.ToString());
					this.m_prevRemainingSeconds = num;
					this.m_animatorCountDown.Play("COUNT_IN");
				}
			}
		}

		// Token: 0x060085F7 RID: 34295 RVA: 0x002D5C24 File Offset: 0x002D3E24
		public void OpenUnitSelect()
		{
			this.OpenDeckSelectList(NKM_UNIT_TYPE.NUT_NORMAL);
			this.UpdateSelectList();
		}

		// Token: 0x060085F8 RID: 34296 RVA: 0x002D5C34 File Offset: 0x002D3E34
		public void OnClickSetLeaderUnit(int index)
		{
			Log.Info("[League][Clicked - SetLeaderUnit]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCUIGauntletLeagueMain.cs", 627);
			if (NKCLeaguePVPMgr.DraftRoomData.roomState != DRAFT_PVP_ROOM_STATE.PICK_ETC)
			{
				return;
			}
			if (NKCLeaguePVPMgr.GetLeftDraftTeamData().leaderIndex != -1)
			{
				return;
			}
			if (NKCLeaguePVPMgr.GetLeftDraftTeamData().banishedUnitIndex == index)
			{
				return;
			}
			this.m_currentSelectedLeaderIndex = index;
			this.UpdateUserInfo();
		}

		// Token: 0x060085F9 RID: 34297 RVA: 0x002D5C8D File Offset: 0x002D3E8D
		public void OnClickBanPickedUnit(int index)
		{
			Log.Info("[League][Clicked - BanPickedUnit]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCUIGauntletLeagueMain.cs", 649);
			if (NKCLeaguePVPMgr.DraftRoomData.roomState != DRAFT_PVP_ROOM_STATE.BAN_OPPONENT)
			{
				return;
			}
			if (NKCLeaguePVPMgr.GetRightDraftTeamData().banishedUnitIndex != -1)
			{
				return;
			}
			this.m_currentSelectedBanIndex = index;
			this.UpdateUserInfo();
		}

		// Token: 0x060085FA RID: 34298 RVA: 0x002D5CD0 File Offset: 0x002D3ED0
		public void OnClickSlotSelectConfirm()
		{
			if (this.m_eCurrentSelectListType == NKM_UNIT_TYPE.NUT_NORMAL)
			{
				if (NKCLeaguePVPMgr.CanPickUnit(NKCLeaguePVPMgr.MyTeamType) && this.m_currentSelectedUnitUID != 0L)
				{
					Log.Info(string.Format("[League][UnitPick - confirm] UID[{0}]", this.m_currentSelectedUnitUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCUIGauntletLeagueMain.cs", 670);
					NKCLeaguePVPMgr.Send_NKMPacket_DRAFT_PVP_PICK_UNIT_REQ(this.m_currentSelectedUnitUID);
					return;
				}
			}
			else if (this.m_eCurrentSelectListType == NKM_UNIT_TYPE.NUT_SHIP)
			{
				if (NKCLeaguePVPMgr.CanPickETC() && this.m_currentSelectedShipUID != 0L)
				{
					Log.Info(string.Format("[League][ShipPick - confirm] UID[{0}]", this.m_currentSelectedShipUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCUIGauntletLeagueMain.cs", 679);
					NKCLeaguePVPMgr.Send_NKMPacket_DRAFT_PVP_PICK_SHIP_REQ(this.m_currentSelectedShipUID);
					return;
				}
			}
			else if (this.m_eCurrentSelectListType == NKM_UNIT_TYPE.NUT_OPERATOR && NKCLeaguePVPMgr.CanPickETC())
			{
				Log.Info(string.Format("[League][OperatorPick - confirm] UID[{0}]", this.m_currentSelectedOperatorUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCUIGauntletLeagueMain.cs", 689);
				NKCLeaguePVPMgr.Send_NKMPacket_DRAFT_PVP_PICK_OPERATOR_REQ(this.m_currentSelectedOperatorUID);
				return;
			}
		}

		// Token: 0x060085FB RID: 34299 RVA: 0x002D5DB8 File Offset: 0x002D3FB8
		public void OnClickOponentBanConfirm()
		{
			if (this.m_currentSelectedBanIndex == -1)
			{
				this.ShowSequenceGuidePopup(NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_LOCAL_BAN", false));
				return;
			}
			NKCLeaguePVPMgr.Send_NKMPacket_DRAFT_PVP_OPPONENT_BAN_REQ(this.m_currentSelectedBanIndex);
		}

		// Token: 0x060085FC RID: 34300 RVA: 0x002D5DE0 File Offset: 0x002D3FE0
		public void OnRecv(NKMPacket_DRAFT_PVP_OPPONENT_BAN_ACK sPacket)
		{
			this.m_currentSelectedBanIndex = -1;
			NKCUtil.SetGameobjectActive(this.m_SelectConfirmButton, false);
		}

		// Token: 0x060085FD RID: 34301 RVA: 0x002D5DF5 File Offset: 0x002D3FF5
		public void OnClickLeaderSelectConfirm()
		{
			if (this.m_currentSelectedLeaderIndex == -1)
			{
				this.ShowSequenceGuidePopup(NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_LEADER", false));
				return;
			}
			NKCLeaguePVPMgr.Send_NKMPacket_DRAFT_PVP_PICK_LEADER_REQ(this.m_currentSelectedLeaderIndex);
		}

		// Token: 0x060085FE RID: 34302 RVA: 0x002D5E1D File Offset: 0x002D401D
		public void OnDeckUnitChangeClicked(NKMDeckIndex selectedDeckIndex, long unitUID, NKM_UNIT_TYPE unitType)
		{
			Log.Info(string.Format("[League][Clicked - unit] Type[{0}] - UID[{1}]", unitType.ToString(), unitUID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCUIGauntletLeagueMain.cs", 729);
			this.SelectUnit(unitType, unitUID);
		}

		// Token: 0x060085FF RID: 34303 RVA: 0x002D5E54 File Offset: 0x002D4054
		private void SelectUnit(NKM_UNIT_TYPE unitType, long unitUID)
		{
			if (this.m_currentSelectedUnit != null)
			{
				this.m_currentSelectedUnit.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
			}
			if (unitType != this.m_eCurrentSelectListType)
			{
				return;
			}
			NKCUIUnitSelectListSlotBase nkcuiunitSelectListSlotBase = this.m_NKCLeaguePvpUnitSelectList.FindSlotFromCurrentList(unitType, unitUID);
			if (nkcuiunitSelectListSlotBase == null)
			{
				return;
			}
			nkcuiunitSelectListSlotBase.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.SELECTED);
			this.m_currentSelectedUnit = nkcuiunitSelectListSlotBase;
			if (unitType == NKM_UNIT_TYPE.NUT_NORMAL)
			{
				this.m_currentSelectedUnitUID = unitUID;
				NKCLeaguePVPMgr.Send_NKMPacket_DRAFT_PVP_SELECT_UNIT_REQ(this.m_currentSelectedUnitUID);
				return;
			}
			if (unitType == NKM_UNIT_TYPE.NUT_SHIP)
			{
				this.m_currentSelectedShipUID = unitUID;
				return;
			}
			if (unitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				this.m_currentSelectedOperatorUID = unitUID;
			}
		}

		// Token: 0x06008600 RID: 34304 RVA: 0x002D5EDC File Offset: 0x002D40DC
		public void OpenShipSelect(bool bObserver)
		{
			string strID = "SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_SHIP";
			if (bObserver)
			{
				strID = "SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_ETC_OBSERVER";
				base.UpdateUpsideMenu();
				this.UpdateUserInfo();
			}
			else
			{
				this.OpenDeckSelectList(NKM_UNIT_TYPE.NUT_SHIP);
				this.UpdateSelectList();
			}
			this.ShowSequenceGuidePopup(NKCStringTable.GetString(strID, false));
			this.m_UserLeft.ShowPickEffect(true, false);
			this.m_UserRight.ShowPickEffect(true, false);
			if (this.m_SelectConfirmButton != null && !bObserver)
			{
				this.m_SelectConfirmButton.PointerClick.RemoveAllListeners();
				this.m_SelectConfirmButton.PointerClick.AddListener(new UnityAction(this.OnClickSlotSelectConfirm));
			}
		}

		// Token: 0x06008601 RID: 34305 RVA: 0x002D5F78 File Offset: 0x002D4178
		public void OpenOperatorSelect(bool bObserver)
		{
			string strID = "SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_OPERATOR";
			if (bObserver)
			{
				strID = "SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_OPERATOR_OBSERVER";
			}
			this.ShowSequenceGuidePopup(NKCStringTable.GetString(strID, false));
			this.m_UserLeft.ShowPickEffect(false, true);
			this.m_UserRight.ShowPickEffect(false, true);
			if (NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData.GetCurrentOperatorCount() == 0)
			{
				this.m_currentSelectedUnit = null;
				this.m_currentSelectedOperatorUID = 0L;
				this.m_eCurrentSelectListType = NKM_UNIT_TYPE.NUT_OPERATOR;
				this.OnClickSlotSelectConfirm();
				return;
			}
			if (bObserver)
			{
				base.UpdateUpsideMenu();
				this.UpdateUserInfo();
				return;
			}
			this.OpenDeckSelectList(NKM_UNIT_TYPE.NUT_OPERATOR);
			this.UpdateSelectList();
		}

		// Token: 0x06008602 RID: 34306 RVA: 0x002D600C File Offset: 0x002D420C
		public void OpenLeaderSelect()
		{
			this.m_UserLeft.ShowPickEffect(false, false);
			this.m_UserRight.ShowPickEffect(false, false);
			this.ShowSequenceGuidePopup(NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_LEADER", false));
			this.m_bSelectLeader = true;
			NKCUtil.SetGameobjectActive(this.m_objPickDisableScreen, true);
			this.UpdateUserInfo();
			if (this.m_SelectConfirmButton != null)
			{
				this.m_SelectConfirmButton.PointerClick.RemoveAllListeners();
				this.m_SelectConfirmButton.PointerClick.AddListener(new UnityAction(this.OnClickLeaderSelectConfirm));
			}
		}

		// Token: 0x06008603 RID: 34307 RVA: 0x002D6098 File Offset: 0x002D4298
		public void CloseUnitSelectList()
		{
			if (this.m_currentSelectedUnit != null)
			{
				this.m_currentSelectedUnit.SetSlotSelectState(NKCUIUnitSelectList.eUnitSlotSelectState.NONE);
				this.m_currentSelectedUnit = null;
			}
			this.m_bSelectLeader = false;
			this.m_currentSelectedUnitUID = 0L;
			this.m_currentSelectedShipUID = 0L;
			this.m_currentSelectedOperatorUID = 0L;
			this.m_currentSelectedBanIndex = -1;
			this.m_currentSelectedLeaderIndex = -1;
		}

		// Token: 0x06008604 RID: 34308 RVA: 0x002D60F3 File Offset: 0x002D42F3
		public void ApplyPickETCResults()
		{
			this.m_UserLeft.ShowPickEffect(false, false);
			NKCUtil.SetGameobjectActive(this.m_objPickDisableScreen, true);
			NKCUtil.SetGameobjectActive(this.m_objWatingNotice, true);
			this.CloseUnitSelectList();
			this.UpdateUserInfo();
		}

		// Token: 0x06008605 RID: 34309 RVA: 0x002D6128 File Offset: 0x002D4328
		private void OpenDeckSelectList(NKM_UNIT_TYPE eUnitType)
		{
			this.m_currentSelectedUnit = null;
			this.m_currentSelectedUnitUID = 0L;
			this.m_currentSelectedShipUID = 0L;
			this.m_currentSelectedOperatorUID = 0L;
			this.m_currentSelectedLeaderIndex = -1;
			this.m_currentSelectedBanIndex = -1;
			this.m_bSelectLeader = false;
			this.m_eCurrentSelectListType = eUnitType;
			if (this.m_NKCLeaguePvpUnitSelectList.IsOpen)
			{
				this.UpdateDeckSelectList(eUnitType);
			}
			else if (eUnitType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				this.m_NKCLeaguePvpUnitSelectList.Open(true, eUnitType, this.MakeOperatorSortOptions(), this.m_ViewerOptions);
			}
			else
			{
				this.m_NKCLeaguePvpUnitSelectList.Open(true, eUnitType, this.MakeSortOptions(), this.m_ViewerOptions);
			}
			base.UpdateUpsideMenu();
			this.UpdateUserInfo();
		}

		// Token: 0x06008606 RID: 34310 RVA: 0x002D61CA File Offset: 0x002D43CA
		private void UpdateDeckSelectList(NKM_UNIT_TYPE eUnitType)
		{
			this.m_eCurrentSelectListType = eUnitType;
			if (this.m_eCurrentSelectListType == NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				this.m_NKCLeaguePvpUnitSelectList.UpdateLoopScrollList(eUnitType, this.MakeOperatorSortOptions());
				return;
			}
			this.m_NKCLeaguePvpUnitSelectList.UpdateLoopScrollList(eUnitType, this.MakeSortOptions());
		}

		// Token: 0x06008607 RID: 34311 RVA: 0x002D6204 File Offset: 0x002D4404
		private NKCUnitSortSystem.UnitListOptions MakeSortOptions()
		{
			return new NKCUnitSortSystem.UnitListOptions
			{
				eDeckType = NKM_DECK_TYPE.NDT_NONE,
				setExcludeUnitID = null,
				setOnlyIncludeUnitID = null,
				setDuplicateUnitID = null,
				setExcludeUnitUID = null,
				bExcludeLockedUnit = false,
				bExcludeDeckedUnit = false,
				bIgnoreCityState = true,
				bIgnoreWorldMapLeader = true,
				setFilterOption = this.m_NKCLeaguePvpUnitSelectList.SortOptions.setFilterOption,
				lstSortOption = this.m_NKCLeaguePvpUnitSelectList.SortOptions.lstSortOption,
				bDescending = this.m_NKCLeaguePvpUnitSelectList.SortOptions.bDescending,
				bIncludeUndeckableUnit = true,
				bHideDeckedUnit = false,
				bPushBackUnselectable = true
			};
		}

		// Token: 0x06008608 RID: 34312 RVA: 0x002D62C0 File Offset: 0x002D44C0
		private NKCOperatorSortSystem.OperatorListOptions MakeOperatorSortOptions()
		{
			return new NKCOperatorSortSystem.OperatorListOptions
			{
				eDeckType = NKM_DECK_TYPE.NDT_NONE,
				setExcludeOperatorID = null,
				setOnlyIncludeOperatorID = null,
				setDuplicateOperatorID = null,
				setExcludeOperatorUID = null,
				setFilterOption = this.m_NKCLeaguePvpUnitSelectList.SortOperatorOptions.setFilterOption,
				lstSortOption = this.m_NKCLeaguePvpUnitSelectList.SortOperatorOptions.lstSortOption
			};
		}

		// Token: 0x06008609 RID: 34313 RVA: 0x002D632C File Offset: 0x002D452C
		public void ShowSequenceGuidePopup(string text)
		{
			NKCUtil.SetGameobjectActive(this.m_PopupSequenceGuide, true);
			NKCUtil.SetLabelText(this.m_sequenceGuideText, text);
			if (this.m_animatorsequenceGuide != null)
			{
				this.m_animatorsequenceGuide.Play("NKM_UI_GAUNTLET_LEAGUE_SEQUENCE_GUIDE_INTRO");
			}
		}

		// Token: 0x0600860A RID: 34314 RVA: 0x002D6364 File Offset: 0x002D4564
		public void HideSequenceGuidePopup()
		{
			if (this.m_animatorsequenceGuide != null)
			{
				this.m_animatorsequenceGuide.Play("NKM_UI_GAUNTLET_LEAGUE_SEQUENCE_GUIDE_OUTRO");
			}
		}

		// Token: 0x04007291 RID: 29329
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_GAUNTLET";

		// Token: 0x04007292 RID: 29330
		private const string UI_ASSET_NAME = "NKM_UI_GAUNTLET_LEAGUE_MAIN";

		// Token: 0x04007293 RID: 29331
		[SerializeField]
		[Header("유저정보")]
		public NKCUIGauntletLeagueMain.LeagueUserInfoUI m_UserLeft;

		// Token: 0x04007294 RID: 29332
		public NKCUIGauntletLeagueMain.LeagueUserInfoUI m_UserRight;

		// Token: 0x04007295 RID: 29333
		public RectTransform m_rectCenterContent;

		// Token: 0x04007296 RID: 29334
		public NKCLeaguePvpUnitSelectList m_NKCLeaguePvpUnitSelectList;

		// Token: 0x04007297 RID: 29335
		public NKCUIComStateButton m_SelectConfirmButton;

		// Token: 0x04007298 RID: 29336
		public NKCUIComButton m_LeaveRoom;

		// Token: 0x04007299 RID: 29337
		public GameObject m_objPickDisableScreen;

		// Token: 0x0400729A RID: 29338
		public GameObject m_objWatingNotice;

		// Token: 0x0400729B RID: 29339
		[Header("애니메이터")]
		public Animator m_animatorStart;

		// Token: 0x0400729C RID: 29340
		public Animator m_animatorPick;

		// Token: 0x0400729D RID: 29341
		public Animator m_animatorCountDown;

		// Token: 0x0400729E RID: 29342
		[Header("시간")]
		public Text m_timeText;

		// Token: 0x0400729F RID: 29343
		[Header("유닛 일러스트")]
		public NKCUICharacterView m_CharacterView;

		// Token: 0x040072A0 RID: 29344
		[Header("팝업 가이드")]
		public GameObject m_PopupSequenceGuide;

		// Token: 0x040072A1 RID: 29345
		public Text m_sequenceGuideText;

		// Token: 0x040072A2 RID: 29346
		public Animator m_animatorsequenceGuide;

		// Token: 0x040072A3 RID: 29347
		private NKM_UNIT_TYPE m_eCurrentSelectListType;

		// Token: 0x040072A4 RID: 29348
		private NKCUIDeckViewer.DeckViewerOption m_ViewerOptions;

		// Token: 0x040072A5 RID: 29349
		private DateTime m_endTime;

		// Token: 0x040072A6 RID: 29350
		private int m_prevRemainingSeconds;

		// Token: 0x040072A7 RID: 29351
		private NKCUIUnitSelectListSlotBase m_currentSelectedUnit;

		// Token: 0x040072A8 RID: 29352
		private long m_currentSelectedUnitUID;

		// Token: 0x040072A9 RID: 29353
		private long m_currentSelectedShipUID;

		// Token: 0x040072AA RID: 29354
		private long m_currentSelectedOperatorUID;

		// Token: 0x040072AB RID: 29355
		private int m_currentSelectedLeaderIndex = -1;

		// Token: 0x040072AC RID: 29356
		private int m_currentSelectedBanIndex = -1;

		// Token: 0x040072AD RID: 29357
		private bool m_bSelectLeader;

		// Token: 0x02001914 RID: 6420
		[Serializable]
		public class LeagueUserInfoUI
		{
			// Token: 0x0600B798 RID: 47000 RVA: 0x00367D5C File Offset: 0x00365F5C
			public void Init(NKCUIGauntletLeagueMain.LeagueUserInfoUI.OnClickPickedUnit onClickDeckSlot, NKCDeckViewShip.OnShipClicked onShipClicked)
			{
				for (int i = 0; i < this.Unit.Length; i++)
				{
					NKCDeckViewUnitSlot nkcdeckViewUnitSlot = this.Unit[i];
					nkcdeckViewUnitSlot.Init(0, false);
					nkcdeckViewUnitSlot.SetData(null, false);
					nkcdeckViewUnitSlot.SetLeader(false, false);
					nkcdeckViewUnitSlot.m_NKCUIComButton.PointerClick.RemoveAllListeners();
					if (onClickDeckSlot != null)
					{
						int index = i;
						nkcdeckViewUnitSlot.m_NKCUIComButton.PointerClick.AddListener(delegate()
						{
							onClickDeckSlot(index);
						});
					}
				}
				this.DeckViewShip.Init(onShipClicked);
				this.DeckViewShip.Open(null, false);
				this.DeckViewShip.SetSelectEffect(false);
				this.DeckViewOperator.Init();
				this.DeckViewOperator.Enable();
				this.OperatorSlot.Init(null);
				this.OperatorSlot.SetData(null, false);
				for (int j = 0; j < this.GlobalBanList.Length; j++)
				{
					NKCDeckViewUnitSelectListSlot nkcdeckViewUnitSelectListSlot = this.GlobalBanList[j];
					nkcdeckViewUnitSelectListSlot.Init(false);
					nkcdeckViewUnitSelectListSlot.SetData(null, 0, false, null);
				}
			}

			// Token: 0x0600B799 RID: 47001 RVA: 0x00367E78 File Offset: 0x00366078
			public void SetData(DraftPvpRoomData.DraftTeamData draftTeamData, bool isLeftTeam, bool includePickBan, bool forceEnableButton, int selectedBanIndex, int selectedLeaderIndex, bool isPrivatePvp)
			{
				int seasonID = NKCPVPManager.FindPvPSeasonID(NKM_GAME_TYPE.NGT_PVP_LEAGUE, NKCSynchronizedTime.GetServerUTCTime(0.0));
				LEAGUE_TIER_ICON tierIconByScore = NKCPVPManager.GetTierIconByScore(NKM_GAME_TYPE.NGT_PVP_LEAGUE, seasonID, draftTeamData.userProfileData.leaguePvpData.score);
				int tierNumberByScore = NKCPVPManager.GetTierNumberByScore(NKM_GAME_TYPE.NGT_PVP_LEAGUE, seasonID, draftTeamData.userProfileData.leaguePvpData.score);
				if (this.TierIcon != null)
				{
					this.TierIcon.SetUI(tierIconByScore, tierNumberByScore);
				}
				NKCUtil.SetLabelText(this.TierScore, draftTeamData.userProfileData.leaguePvpData.score.ToString());
				if (isPrivatePvp)
				{
					NKCUILeagueTier tierIcon = this.TierIcon;
					NKCUtil.SetGameobjectActive((tierIcon != null) ? tierIcon.gameObject : null, false);
					Text tierScore = this.TierScore;
					NKCUtil.SetGameobjectActive((tierScore != null) ? tierScore.gameObject : null, false);
					Text tierScore2 = this.TierScore;
					GameObject targetObj;
					if (tierScore2 == null)
					{
						targetObj = null;
					}
					else
					{
						Transform parent = tierScore2.transform.parent;
						targetObj = ((parent != null) ? parent.gameObject : null);
					}
					NKCUtil.SetGameobjectActive(targetObj, false);
				}
				NKCUtil.SetLabelText(this.Level, NKCUtilString.GET_STRING_LEVEL_ONE_PARAM, new object[]
				{
					draftTeamData.userProfileData.commonProfile.level
				});
				NKCUtil.SetLabelText(this.Name, NKCUtilString.GetUserNickname(draftTeamData.userProfileData.commonProfile.nickname, !isLeftTeam));
				bool bValue = true;
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD, 0, 0) || draftTeamData.userProfileData.guildData == null || draftTeamData.userProfileData.guildData.guildUid == 0L)
				{
					bValue = false;
				}
				NKCUtil.SetGameobjectActive(this.Guild, bValue);
				if (this.Guild != null && this.Guild.activeSelf)
				{
					this.GuildBadge.SetData(draftTeamData.userProfileData.guildData.badgeId, !isLeftTeam);
					NKCUtil.SetLabelText(this.GuildName, NKCUtilString.GetUserGuildName(draftTeamData.userProfileData.guildData.guildName, !isLeftTeam));
				}
				for (int i = 0; i < draftTeamData.globalBanUnitIdList.Count; i++)
				{
					NKMUnitTempletBase templetBase = NKMUnitTempletBase.Find(draftTeamData.globalBanUnitIdList[i]);
					this.GlobalBanList[i].SetDataForBan(templetBase, true, null, false, false);
				}
				int num = 0;
				for (int j = 0; j < draftTeamData.pickUnitList.Count; j++)
				{
					NKMAsyncUnitData nkmasyncUnitData = draftTeamData.pickUnitList[j];
					if (nkmasyncUnitData != null && (j != draftTeamData.banishedUnitIndex || includePickBan))
					{
						NKMUnitData cNKMUnitData = NKMDungeonManager.MakeUnitDataFromID(nkmasyncUnitData.unitId, -1L, nkmasyncUnitData.unitLevel, nkmasyncUnitData.limitBreakLevel, nkmasyncUnitData.skinId, nkmasyncUnitData.tacticLevel);
						bool flag = false;
						if (!NKCLeaguePVPMgr.IsObserver())
						{
							if (!isLeftTeam && includePickBan && draftTeamData.banishedUnitIndex == -1 && NKCLeaguePVPMgr.DraftRoomData.roomState == DRAFT_PVP_ROOM_STATE.BAN_OPPONENT)
							{
								if (j != selectedBanIndex)
								{
									flag = true;
								}
							}
							else if (isLeftTeam && includePickBan && forceEnableButton && j != selectedLeaderIndex)
							{
								flag = true;
							}
						}
						this.Unit[num].SetData(cNKMUnitData, flag);
						this.Unit[num].SetLeaguePickEnable(flag);
						if (j == draftTeamData.banishedUnitIndex)
						{
							this.Unit[num].SetLeagueBan(true);
							this.Unit[num].SetLeaguePickEnable(false);
						}
						if (j == selectedBanIndex || j == selectedLeaderIndex)
						{
							this.Unit[num].SetSelectable(true);
						}
						else
						{
							this.Unit[num].SetSelectable(false);
						}
						if ((isLeftTeam || NKCLeaguePVPMgr.DraftRoomData.roomState != DRAFT_PVP_ROOM_STATE.PICK_ETC || NKCLeaguePVPMgr.IsObserver()) && j == draftTeamData.leaderIndex)
						{
							this.Unit[num].SetLeader(true, true);
						}
						num++;
					}
				}
				int currentSelectedSlot = NKCLeaguePVPMgr.GetCurrentSelectedSlot(draftTeamData.teamType);
				foreach (int num2 in NKCLeaguePVPMgr.GetPickEnabledSlot(draftTeamData.teamType))
				{
					if (this.Unit[num2].m_NKMUnitData == null)
					{
						this.Unit[num2].SetLeaguePickEnable(true);
					}
					if (NKCLeaguePVPMgr.DraftRoomData.selectedUnit != null && currentSelectedSlot == num2)
					{
						Log.Info(string.Format("[League] SelectedUnit [{0}]", currentSelectedSlot), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCUIGauntletLeagueMain.cs", 236);
						NKMUnitData cNKMUnitData2 = NKMDungeonManager.MakeUnitDataFromID(NKCLeaguePVPMgr.DraftRoomData.selectedUnit.unitId, -1L, NKCLeaguePVPMgr.DraftRoomData.selectedUnit.unitLevel, NKCLeaguePVPMgr.DraftRoomData.selectedUnit.limitBreakLevel, NKCLeaguePVPMgr.DraftRoomData.selectedUnit.skinId, NKCLeaguePVPMgr.DraftRoomData.selectedUnit.tacticLevel);
						this.Unit[currentSelectedSlot].SetData(cNKMUnitData2, false);
						this.Unit[currentSelectedSlot].SetSelectable(false);
					}
				}
				if (!isLeftTeam && NKCLeaguePVPMgr.DraftRoomData.roomState == DRAFT_PVP_ROOM_STATE.PICK_ETC && !NKCLeaguePVPMgr.IsObserver())
				{
					NKCUtil.SetGameobjectActive(this.OperatorSlot, false);
					NKCUtil.SetGameobjectActive(this.OperatorSkillInfo, false);
					NKCUtil.SetGameobjectActive(this.OperatorSkillCombo, false);
					NKCUtil.SetGameobjectActive(this.OperatorEmpty, false);
					return;
				}
				if (draftTeamData.mainShip != null && draftTeamData.mainShip.unitId != 0)
				{
					Log.Info(string.Format("[League][{0}] ShipInfo ID[{1}] Level[{2}]", isLeftTeam, draftTeamData.mainShip.unitId, draftTeamData.mainShip.unitLevel), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCUIGauntletLeagueMain.cs", 260);
					NKMUnitData shipUnitData = NKMDungeonManager.MakeUnitDataFromID(draftTeamData.mainShip.unitId, -1L, draftTeamData.mainShip.unitLevel, draftTeamData.mainShip.limitBreakLevel, draftTeamData.mainShip.skinId, 0);
					this.DeckViewShip.Open(shipUnitData, false);
				}
				bool flag2 = NKCContentManager.IsContentsUnlocked(ContentsType.OPERATOR, 0, 0);
				NKCUtil.SetGameobjectActive(this.OperatorSlot, draftTeamData.operatorUnit != null && flag2);
				NKCUtil.SetGameobjectActive(this.OperatorSkillInfo, draftTeamData.operatorUnit != null && flag2);
				NKCUtil.SetGameobjectActive(this.OperatorSkillCombo, draftTeamData.operatorUnit != null && flag2);
				NKCUtil.SetGameobjectActive(this.OperatorEmpty, draftTeamData.operatorUnit == null && flag2);
				if (draftTeamData.operatorUnit != null && draftTeamData.operatorUnit.id != 0 && flag2)
				{
					Log.Info(string.Format("[League][{0}] OperatorInfo ID[{1}]", isLeftTeam, draftTeamData.operatorUnit.id), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Gauntlet/NKCUIGauntletLeagueMain.cs", 274);
					this.OperatorSlot.SetData(draftTeamData.operatorUnit, false);
					this.OperatorMainSkill.SetData(draftTeamData.operatorUnit.mainSkill.id, (int)draftTeamData.operatorUnit.mainSkill.level, false);
					this.OperatorSubSkill.SetData(draftTeamData.operatorUnit.subSkill.id, (int)draftTeamData.operatorUnit.subSkill.level, false);
					this.OperatorSkillCombo.SetData(draftTeamData.operatorUnit.id);
				}
			}

			// Token: 0x0600B79A RID: 47002 RVA: 0x0036853C File Offset: 0x0036673C
			public void ShowPickEffect(bool showShipEffect, bool showOperatorEffect)
			{
				if (showShipEffect || showOperatorEffect)
				{
					NKCUtil.SetGameobjectActive(this.ETCSelection, true);
				}
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.OPERATOR, 0, 0))
				{
					showOperatorEffect = false;
				}
				NKCUtil.SetGameobjectActive(this.PickEffectETC, showShipEffect || showOperatorEffect);
				NKCUtil.SetGameobjectActive(this.PickEffectShip, showShipEffect);
				NKCUtil.SetGameobjectActive(this.PickEffectOperator, showOperatorEffect);
			}

			// Token: 0x0600B79B RID: 47003 RVA: 0x00368590 File Offset: 0x00366790
			public void Close()
			{
				if (this.DeckViewShip != null)
				{
					this.DeckViewShip.Close();
				}
				if (this.DeckViewOperator != null)
				{
					this.DeckViewOperator.Close();
				}
				for (int i = 0; i < this.GlobalBanList.Length; i++)
				{
					this.GlobalBanList[i] = null;
				}
				for (int j = 0; j < this.Unit.Length; j++)
				{
					this.Unit[j].CloseInstance();
				}
			}

			// Token: 0x0400AA7A RID: 43642
			public NKCDeckViewShip DeckViewShip;

			// Token: 0x0400AA7B RID: 43643
			public NKCUIDeckViewOperator DeckViewOperator;

			// Token: 0x0400AA7C RID: 43644
			public NKCUIOperatorDeckSlot OperatorSlot;

			// Token: 0x0400AA7D RID: 43645
			public GameObject OperatorEmpty;

			// Token: 0x0400AA7E RID: 43646
			public GameObject OperatorSkillInfo;

			// Token: 0x0400AA7F RID: 43647
			public NKCUIOperatorSkill OperatorMainSkill;

			// Token: 0x0400AA80 RID: 43648
			public NKCUIOperatorSkill OperatorSubSkill;

			// Token: 0x0400AA81 RID: 43649
			public NKCUIOperatorTacticalSkillCombo OperatorSkillCombo;

			// Token: 0x0400AA82 RID: 43650
			public GameObject ETCSelection;

			// Token: 0x0400AA83 RID: 43651
			public GameObject PickEffectETC;

			// Token: 0x0400AA84 RID: 43652
			public GameObject PickEffectShip;

			// Token: 0x0400AA85 RID: 43653
			public GameObject PickEffectOperator;

			// Token: 0x0400AA86 RID: 43654
			public NKCDeckViewUnitSlot[] Unit = new NKCDeckViewUnitSlot[9];

			// Token: 0x0400AA87 RID: 43655
			public NKCUILeagueTier TierIcon;

			// Token: 0x0400AA88 RID: 43656
			public Text TierScore;

			// Token: 0x0400AA89 RID: 43657
			public Text Level;

			// Token: 0x0400AA8A RID: 43658
			public Text Name;

			// Token: 0x0400AA8B RID: 43659
			public GameObject Guild;

			// Token: 0x0400AA8C RID: 43660
			public NKCUIGuildBadge GuildBadge;

			// Token: 0x0400AA8D RID: 43661
			public Text GuildName;

			// Token: 0x0400AA8E RID: 43662
			public NKCDeckViewUnitSelectListSlot[] GlobalBanList = new NKCDeckViewUnitSelectListSlot[2];

			// Token: 0x02001A94 RID: 6804
			// (Invoke) Token: 0x0600BC62 RID: 48226
			public delegate void OnClickPickedUnit(int index);
		}
	}
}

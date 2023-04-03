using System;
using System.Collections.Generic;
using ClientPacket.Common;
using ClientPacket.Game;
using ClientPacket.Pvp;
using Cs.Logging;
using NKC.PacketHandler;
using NKC.Publisher;
using NKC.UI;
using NKC.UI.Gauntlet;
using NKC.UI.Result;
using NKM;
using NKM.Templet;

namespace NKC
{
	// Token: 0x02000692 RID: 1682
	public static class NKCLeaguePVPMgr
	{
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x0011B6BE File Offset: 0x001198BE
		public static DraftPvpRoomData DraftRoomData
		{
			get
			{
				return NKCLeaguePVPMgr.m_DraftPvpRoomData;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x0011B6C5 File Offset: 0x001198C5
		public static NKM_TEAM_TYPE MyTeamType
		{
			get
			{
				return NKCLeaguePVPMgr.m_myTeamType;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06003706 RID: 14086 RVA: 0x0011B6CC File Offset: 0x001198CC
		public static NKM_TEAM_TYPE OponentTeamType
		{
			get
			{
				if (NKCLeaguePVPMgr.m_myTeamType != NKM_TEAM_TYPE.NTT_A1)
				{
					return NKM_TEAM_TYPE.NTT_A1;
				}
				return NKM_TEAM_TYPE.NTT_B1;
			}
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x0011B6DC File Offset: 0x001198DC
		public static void InitDraftRoom(DraftPvpRoomData draftPvpRoomData)
		{
			Log.Debug("[DRAFT] InitDraftRoom [" + draftPvpRoomData.roomState.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 34);
			NKCLeaguePVPMgr.m_LeagueRoomStarted = false;
			NKCLeaguePVPMgr.m_DraftPvpRoomData = draftPvpRoomData;
			NKCLeaguePVPMgr.m_requestedBanIndex = -1;
			NKCLeaguePVPMgr.UpdateMyTeamType();
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAUNTLET_LEAGUE_ROOM)
			{
				NKCLeaguePVPMgr.ChangeScene();
			}
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x0011B740 File Offset: 0x00119940
		public static void UpdateMyTeamType()
		{
			NKCLeaguePVPMgr.m_myTeamType = NKM_TEAM_TYPE.NTT_INVALID;
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (NKCLeaguePVPMgr.m_DraftPvpRoomData.draftTeamDataA.userProfileData.commonProfile.userUid == myUserData.m_UserUID)
			{
				NKCLeaguePVPMgr.m_myTeamType = NKCLeaguePVPMgr.m_DraftPvpRoomData.draftTeamDataA.teamType;
				return;
			}
			if (NKCLeaguePVPMgr.m_DraftPvpRoomData.draftTeamDataB.userProfileData.commonProfile.userUid == myUserData.m_UserUID)
			{
				NKCLeaguePVPMgr.m_myTeamType = NKCLeaguePVPMgr.m_DraftPvpRoomData.draftTeamDataB.teamType;
			}
		}

		// Token: 0x06003709 RID: 14089 RVA: 0x0011B7C9 File Offset: 0x001199C9
		public static DraftPvpRoomData.DraftTeamData GetMyDraftTeamData()
		{
			if (NKCLeaguePVPMgr.m_myTeamType == NKM_TEAM_TYPE.NTT_A1 || NKCLeaguePVPMgr.m_myTeamType == NKM_TEAM_TYPE.NTT_B1)
			{
				return NKCLeaguePVPMgr.GetDraftTeamData(NKCLeaguePVPMgr.m_myTeamType);
			}
			return null;
		}

		// Token: 0x0600370A RID: 14090 RVA: 0x0011B7E7 File Offset: 0x001199E7
		public static DraftPvpRoomData.DraftTeamData GetLeftDraftTeamData()
		{
			if (NKCLeaguePVPMgr.m_myTeamType == NKM_TEAM_TYPE.NTT_B1)
			{
				return NKCLeaguePVPMgr.m_DraftPvpRoomData.draftTeamDataB;
			}
			return NKCLeaguePVPMgr.m_DraftPvpRoomData.draftTeamDataA;
		}

		// Token: 0x0600370B RID: 14091 RVA: 0x0011B806 File Offset: 0x00119A06
		public static DraftPvpRoomData.DraftTeamData GetRightDraftTeamData()
		{
			if (NKCLeaguePVPMgr.m_myTeamType == NKM_TEAM_TYPE.NTT_B1)
			{
				return NKCLeaguePVPMgr.m_DraftPvpRoomData.draftTeamDataA;
			}
			return NKCLeaguePVPMgr.m_DraftPvpRoomData.draftTeamDataB;
		}

		// Token: 0x0600370C RID: 14092 RVA: 0x0011B828 File Offset: 0x00119A28
		public static NKMAsyncUnitData GetTeamLeaderUnit(bool isLeft)
		{
			DraftPvpRoomData.DraftTeamData draftTeamData = isLeft ? NKCLeaguePVPMgr.GetLeftDraftTeamData() : NKCLeaguePVPMgr.GetRightDraftTeamData();
			if (draftTeamData == null)
			{
				return null;
			}
			if (draftTeamData.leaderIndex >= draftTeamData.pickUnitList.Count)
			{
				return null;
			}
			return draftTeamData.pickUnitList[draftTeamData.leaderIndex];
		}

		// Token: 0x0600370D RID: 14093 RVA: 0x0011B870 File Offset: 0x00119A70
		public static NKCUnitSortSystem.eUnitState GetUnitSlotState(NKM_UNIT_TYPE unitType, int unitID, bool checkMyTeamOnly)
		{
			if (unitType != NKM_UNIT_TYPE.NUT_NORMAL)
			{
				return NKCUnitSortSystem.eUnitState.NONE;
			}
			DraftPvpRoomData.DraftTeamData leftDraftTeamData = NKCLeaguePVPMgr.GetLeftDraftTeamData();
			if (leftDraftTeamData != null)
			{
				if (leftDraftTeamData.globalBanUnitIdList.Contains(unitID))
				{
					return NKCUnitSortSystem.eUnitState.LEAGUE_BANNED;
				}
				if (leftDraftTeamData.pickUnitList.Find((NKMAsyncUnitData x) => x.unitId == unitID) != null)
				{
					return NKCUnitSortSystem.eUnitState.LEAGUE_DECKED_LEFT;
				}
			}
			if (checkMyTeamOnly)
			{
				return NKCUnitSortSystem.eUnitState.NONE;
			}
			DraftPvpRoomData.DraftTeamData rightDraftTeamData = NKCLeaguePVPMgr.GetRightDraftTeamData();
			if (rightDraftTeamData != null)
			{
				if (rightDraftTeamData.globalBanUnitIdList.Contains(unitID))
				{
					return NKCUnitSortSystem.eUnitState.LEAGUE_BANNED;
				}
				if (rightDraftTeamData.pickUnitList.Find((NKMAsyncUnitData x) => x.unitId == unitID) != null)
				{
					return NKCUnitSortSystem.eUnitState.LEAGUE_DECKED_RIGHT;
				}
			}
			return NKCUnitSortSystem.eUnitState.NONE;
		}

		// Token: 0x0600370E RID: 14094 RVA: 0x0011B90C File Offset: 0x00119B0C
		public static List<int> GetPickEnabledSlot(NKM_TEAM_TYPE teamType)
		{
			List<int> list = new List<int>();
			switch (NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState)
			{
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_1:
				if (teamType == NKM_TEAM_TYPE.NTT_A1)
				{
					list.Add(0);
				}
				break;
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_2:
				if (teamType == NKM_TEAM_TYPE.NTT_B1)
				{
					list.Add(0);
					list.Add(1);
				}
				break;
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_3:
				if (teamType == NKM_TEAM_TYPE.NTT_A1)
				{
					list.Add(1);
					list.Add(2);
					list.Add(3);
				}
				break;
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_4:
				if (teamType == NKM_TEAM_TYPE.NTT_B1)
				{
					list.Add(2);
					list.Add(3);
				}
				break;
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_5:
				if (teamType == NKM_TEAM_TYPE.NTT_A1)
				{
					list.Add(4);
					list.Add(5);
				}
				break;
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_6:
				if (teamType == NKM_TEAM_TYPE.NTT_B1)
				{
					list.Add(4);
					list.Add(5);
					list.Add(6);
				}
				break;
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_7:
				if (teamType == NKM_TEAM_TYPE.NTT_A1)
				{
					list.Add(6);
					list.Add(7);
					list.Add(8);
				}
				break;
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_8:
				if (teamType == NKM_TEAM_TYPE.NTT_B1)
				{
					list.Add(7);
					list.Add(8);
				}
				break;
			}
			return list;
		}

		// Token: 0x0600370F RID: 14095 RVA: 0x0011BA13 File Offset: 0x00119C13
		public static bool IsCurrentSelectedUnit(NKM_TEAM_TYPE teamType, int slotIndex)
		{
			return false;
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x0011BA18 File Offset: 0x00119C18
		public static void OnRecv(NKMPacket_LEAGUE_PVP_ACCEPT_NOT sPacket)
		{
			NKCPopupOKCancel.ClosePopupBox();
			if (NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH() != null && NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH().NKCUIGuantletMatch != null)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_MATCH().NKCUIGuantletMatch.OnRecv(sPacket);
			}
			NKCLeaguePVPMgr.InitDraftRoom(sPacket.roomData);
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x0011BA70 File Offset: 0x00119C70
		public static void OnRecv(NKMPacket_LEAGUE_PVP_UPDATED_NOT sPacket)
		{
			if (!NKCLeaguePVPMgr.m_LeagueRoomStarted)
			{
				Log.Info("[League][Relogin] Reserved Roomdata [" + sPacket.roomData.roomState.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 203);
				NKCLeaguePVPMgr.m_ReservedPvpRoomData = sPacket.roomData;
				return;
			}
			if (NKCLeaguePVPMgr.m_DraftPvpRoomData == null || NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState != sPacket.roomData.roomState)
			{
				Log.Info(string.Format("[League][RoomStateChanged] [{0}] -> [{1}]", NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState.ToString(), sPacket.roomData.roomState), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 210);
				NKCLeaguePVPMgr.m_DraftPvpRoomData = sPacket.roomData;
				NKCLeaguePVPMgr.OnRoomStateChanged();
				return;
			}
			NKCLeaguePVPMgr.m_DraftPvpRoomData = sPacket.roomData;
			switch (NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState)
			{
			case DRAFT_PVP_ROOM_STATE.INIT:
			case DRAFT_PVP_ROOM_STATE.BAN_ALL:
			case DRAFT_PVP_ROOM_STATE.BAN_COMPLETE:
				NKCLeaguePVPMgr.RefreshBanProgress();
				return;
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_1:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_2:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_3:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_4:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_5:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_6:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_7:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_8:
				NKCLeaguePVPMgr.RefreshPickProgress(false);
				NKCLeaguePVPMgr.ShowPickNotice();
				return;
			case DRAFT_PVP_ROOM_STATE.BAN_OPPONENT:
			case DRAFT_PVP_ROOM_STATE.PICK_ETC:
			case DRAFT_PVP_ROOM_STATE.DRAFT_COMPLETE:
				NKCLeaguePVPMgr.RefreshPickProgress(false);
				return;
			default:
				return;
			}
		}

		// Token: 0x06003712 RID: 14098 RVA: 0x0011BBA0 File Offset: 0x00119DA0
		public static void OnRoomStateChanged()
		{
			NKCLeaguePVPMgr.CheckLeaveRoomState();
			switch (NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState)
			{
			case DRAFT_PVP_ROOM_STATE.INIT:
				Log.Debug("[DRAFT] OpenGlobalBan [" + NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 251);
				NKCLeaguePVPMgr.OpenGlobalBan();
				return;
			case DRAFT_PVP_ROOM_STATE.BAN_ALL:
				Log.Debug("[DRAFT] OpenCandidateList [" + NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 257);
				NKCLeaguePVPMgr.OpenCandidateList();
				return;
			case DRAFT_PVP_ROOM_STATE.BAN_COMPLETE:
				Log.Debug("[DRAFT] OpenFinalResult [" + NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 263);
				NKCLeaguePVPMgr.OpenFinalResult();
				return;
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_1:
				Log.Debug("[DRAFT] OpenPickSequence [" + NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 269);
				NKCLeaguePVPMgr.OpenPickSequence();
				return;
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_2:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_3:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_4:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_5:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_6:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_7:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_8:
				Log.Debug("[DRAFT] RefreshPickProgress [" + NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 281);
				NKCLeaguePVPMgr.RefreshPickProgress(true);
				NKCLeaguePVPMgr.ShowPickNotice();
				return;
			case DRAFT_PVP_ROOM_STATE.BAN_OPPONENT:
				Log.Debug("[DRAFT] RefreshPickProgress [" + NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 288);
				NKCLeaguePVPMgr.RefreshPickProgress(true);
				if (NKCLeaguePVPMgr.IsObserver())
				{
					NKCLeaguePVPMgr.ShowSequenceGuidePopup(NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_BAN_OBSERVER", false));
					return;
				}
				NKCLeaguePVPMgr.ShowSequenceGuidePopup(NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_LOCAL_BAN", false));
				return;
			case DRAFT_PVP_ROOM_STATE.PICK_ETC:
				Log.Debug("[DRAFT] RefreshPickProgress [" + NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 299);
				NKCLeaguePVPMgr.RefreshPickProgress(true);
				return;
			case DRAFT_PVP_ROOM_STATE.DRAFT_COMPLETE:
				Log.Debug("[DRAFT] RefreshPickProgress [" + NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 305);
				NKCLeaguePVPMgr.RefreshPickProgress(true);
				NKCLeaguePVPMgr.OpenMatchUI();
				return;
			default:
				return;
			}
		}

		// Token: 0x06003713 RID: 14099 RVA: 0x0011BDF9 File Offset: 0x00119FF9
		public static void ChangeScene()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LEAGUE_ROOM, true);
		}

		// Token: 0x06003714 RID: 14100 RVA: 0x0011BE08 File Offset: 0x0011A008
		public static void CancelLeaguePvp()
		{
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x0011BE0C File Offset: 0x0011A00C
		public static void OpenGlobalBan()
		{
			NKC_SCEN_GAUNTLET_LEAGUE_ROOM nkc_SCEN_GAUNTLET_LEAGUE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM == null)
			{
				return;
			}
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeageGlobalBan.RefreshBanProgress();
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x0011BE34 File Offset: 0x0011A034
		public static void OpenCandidateList()
		{
			NKC_SCEN_GAUNTLET_LEAGUE_ROOM nkc_SCEN_GAUNTLET_LEAGUE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM == null)
			{
				return;
			}
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeageGlobalBan.OpenCandidateList();
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x0011BE5C File Offset: 0x0011A05C
		public static void OpenFinalResult()
		{
			NKC_SCEN_GAUNTLET_LEAGUE_ROOM nkc_SCEN_GAUNTLET_LEAGUE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM == null)
			{
				return;
			}
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeageGlobalBan.ShowFinalResult();
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x0011BE84 File Offset: 0x0011A084
		public static void RefreshBanProgress()
		{
			NKC_SCEN_GAUNTLET_LEAGUE_ROOM nkc_SCEN_GAUNTLET_LEAGUE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM == null)
			{
				return;
			}
			DraftPvpRoomData.DraftTeamData leftDraftTeamData = NKCLeaguePVPMgr.GetLeftDraftTeamData();
			DraftPvpRoomData.DraftTeamData rightDraftTeamData = NKCLeaguePVPMgr.GetRightDraftTeamData();
			if (leftDraftTeamData != null && rightDraftTeamData != null)
			{
				nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeageGlobalBan.RefreshMyBanUnitList(leftDraftTeamData.globalBanUnitIdList, rightDraftTeamData.globalBanUnitIdList.Count);
			}
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeageGlobalBan.RefreshBanProgress();
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x0011BEDC File Offset: 0x0011A0DC
		public static void Send_NKMPacket_LEAGUE_PVP_GIVEUP_REQ()
		{
			NKMPacket_LEAGUE_PVP_GIVEUP_REQ packet = new NKMPacket_LEAGUE_PVP_GIVEUP_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x0011BF02 File Offset: 0x0011A102
		public static void OnRecv(NKMPacket_LEAGUE_PVP_GIVEUP_ACK sPacket)
		{
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x0011BF04 File Offset: 0x0011A104
		public static void Send_NKMPacket_PRIVATE_PVP_EXIT_REQ()
		{
			NKMPacket_PRIVATE_PVP_EXIT_REQ packet = new NKMPacket_PRIVATE_PVP_EXIT_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x0011BF2A File Offset: 0x0011A12A
		public static bool OnRecv(NKMPacket_PRIVATE_PVP_EXIT_ACK cNKMPacket_PRIVATE_PVP_EXIT_ACK)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAUNTLET_LEAGUE_ROOM)
			{
				return false;
			}
			NKCPopupOKCancel.ClosePopupBox();
			NKCLeaguePVPMgr.CancelAllProcess();
			return true;
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x0011BF48 File Offset: 0x0011A148
		public static bool OnRecv(NKMPacket_PRIVATE_PVP_CANCEL_NOT sPacket)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAUNTLET_LEAGUE_ROOM)
			{
				return false;
			}
			if (!NKCLeaguePVPMgr.IsPlayer(sPacket.targetUserUid))
			{
				Log.Debug(string.Format("[PrivatePvpRoom] TargetUser[{0}] CancelType[{1}]", sPacket.targetUserUid, sPacket.cancelType.ToString()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 416);
				return false;
			}
			NKCPopupOKCancel.ClosePopupBox();
			NKCLeaguePVPMgr.CancelAllProcess();
			return true;
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x0011BFB4 File Offset: 0x0011A1B4
		public static bool CanPickUnit(NKM_TEAM_TYPE teamType)
		{
			switch (NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState)
			{
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_1:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_3:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_5:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_7:
				if (teamType == NKM_TEAM_TYPE.NTT_A1)
				{
					return true;
				}
				break;
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_2:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_4:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_6:
			case DRAFT_PVP_ROOM_STATE.PICK_UNIT_8:
				if (teamType == NKM_TEAM_TYPE.NTT_B1)
				{
					return true;
				}
				break;
			}
			return false;
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x0011C003 File Offset: 0x0011A203
		public static bool CanPickETC()
		{
			return NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState == DRAFT_PVP_ROOM_STATE.PICK_ETC;
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x0011C018 File Offset: 0x0011A218
		public static int GetCurrentSelectedSlot(NKM_TEAM_TYPE teamType)
		{
			if (!NKCLeaguePVPMgr.CanPickUnit(teamType))
			{
				return -1;
			}
			DraftPvpRoomData.DraftTeamData draftTeamData = NKCLeaguePVPMgr.GetDraftTeamData(teamType);
			if (draftTeamData == null)
			{
				return -1;
			}
			return draftTeamData.pickUnitList.Count;
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x0011C048 File Offset: 0x0011A248
		public static void OpenPickSequence()
		{
			NKC_SCEN_GAUNTLET_LEAGUE_ROOM nkc_SCEN_GAUNTLET_LEAGUE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM == null)
			{
				return;
			}
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeageGlobalBan.Close();
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.Open(NKCLeaguePVPMgr.m_DraftPvpRoomData.stateEndTime);
			if (!NKCLeaguePVPMgr.IsObserver())
			{
				nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.OpenUnitSelect();
			}
			NKCLeaguePVPMgr.CheckLeaveRoomState();
			NKCLeaguePVPMgr.ShowPickNotice();
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x0011C0A0 File Offset: 0x0011A2A0
		public static void ShowPickNotice()
		{
			string strID;
			if (NKCLeaguePVPMgr.IsObserver())
			{
				strID = "SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_OBSERVER";
				NKCLeaguePVPMgr.ShowSequenceGuidePopup(NKCStringTable.GetString(strID, false) ?? "");
				return;
			}
			NKM_TEAM_TYPE teamType;
			if (!NKCLeaguePVPMgr.CanPickUnit(NKCLeaguePVPMgr.m_myTeamType))
			{
				teamType = NKCLeaguePVPMgr.OponentTeamType;
				strID = "SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_OPPONENT";
			}
			else
			{
				teamType = NKCLeaguePVPMgr.MyTeamType;
				strID = "SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_USER";
			}
			List<int> pickEnabledSlot = NKCLeaguePVPMgr.GetPickEnabledSlot(teamType);
			if (pickEnabledSlot.Count > 0)
			{
				int count = pickEnabledSlot.Count;
				int num = pickEnabledSlot[0];
				int num2 = Math.Min(NKCLeaguePVPMgr.GetCurrentSelectedSlot(teamType) - num, count);
				NKCLeaguePVPMgr.ShowSequenceGuidePopup(string.Format("{0} {1}/{2}", NKCStringTable.GetString(strID, false), num2, count));
			}
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x0011C154 File Offset: 0x0011A354
		public static void ShowSequenceGuidePopup(string textMessage)
		{
			NKC_SCEN_GAUNTLET_LEAGUE_ROOM nkc_SCEN_GAUNTLET_LEAGUE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM == null)
			{
				return;
			}
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.ShowSequenceGuidePopup(textMessage);
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x0011C17C File Offset: 0x0011A37C
		public static void RefreshPickProgress(bool roomStateChanged)
		{
			NKC_SCEN_GAUNTLET_LEAGUE_ROOM nkc_SCEN_GAUNTLET_LEAGUE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM == null)
			{
				return;
			}
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeageGlobalBan.IsOpen)
			{
				nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeageGlobalBan.Close();
			}
			if (!nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.IsOpen)
			{
				nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.Open(NKCLeaguePVPMgr.m_DraftPvpRoomData.stateEndTime);
				if (!NKCLeaguePVPMgr.IsObserver())
				{
					nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.OpenUnitSelect();
				}
			}
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.RefreshDraftData(NKCLeaguePVPMgr.m_DraftPvpRoomData.stateEndTime, roomStateChanged);
			if (roomStateChanged)
			{
				nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.UpdatePickAnimation();
				if (NKCLeaguePVPMgr.m_DraftPvpRoomData.roomState == DRAFT_PVP_ROOM_STATE.PICK_ETC)
				{
					nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.OpenShipSelect(NKCLeaguePVPMgr.IsObserver());
				}
			}
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x0011C228 File Offset: 0x0011A428
		public static void Send_NKMPacket_DRAFT_PVP_PICK_UNIT_REQ(long unitUID)
		{
			NKMPacket_DRAFT_PVP_PICK_UNIT_REQ nkmpacket_DRAFT_PVP_PICK_UNIT_REQ = new NKMPacket_DRAFT_PVP_PICK_UNIT_REQ();
			nkmpacket_DRAFT_PVP_PICK_UNIT_REQ.unitUid = unitUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DRAFT_PVP_PICK_UNIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x06003726 RID: 14118 RVA: 0x0011C255 File Offset: 0x0011A455
		public static void OnRecv(NKMPacket_DRAFT_PVP_PICK_UNIT_ACK sPacket)
		{
		}

		// Token: 0x06003727 RID: 14119 RVA: 0x0011C258 File Offset: 0x0011A458
		public static void Send_NKMPacket_DRAFT_PVP_OPPONENT_BAN_REQ(int slotIndex)
		{
			NKCLeaguePVPMgr.m_requestedBanIndex = slotIndex;
			NKMPacket_DRAFT_PVP_OPPONENT_BAN_REQ nkmpacket_DRAFT_PVP_OPPONENT_BAN_REQ = new NKMPacket_DRAFT_PVP_OPPONENT_BAN_REQ();
			nkmpacket_DRAFT_PVP_OPPONENT_BAN_REQ.unitIndex = slotIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DRAFT_PVP_OPPONENT_BAN_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x06003728 RID: 14120 RVA: 0x0011C28C File Offset: 0x0011A48C
		public static void OnRecv(NKMPacket_DRAFT_PVP_OPPONENT_BAN_ACK sPacket)
		{
			if (NKCLeaguePVPMgr.m_requestedBanIndex != -1)
			{
				NKCLeaguePVPMgr.GetRightDraftTeamData().banishedUnitIndex = NKCLeaguePVPMgr.m_requestedBanIndex;
				NKCLeaguePVPMgr.m_requestedBanIndex = -1;
			}
			NKC_SCEN_GAUNTLET_LEAGUE_ROOM nkc_SCEN_GAUNTLET_LEAGUE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM == null)
			{
				return;
			}
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.OnRecv(sPacket);
			NKCLeaguePVPMgr.RefreshPickProgress(false);
		}

		// Token: 0x06003729 RID: 14121 RVA: 0x0011C2D8 File Offset: 0x0011A4D8
		public static void Send_NKMPacket_DRAFT_PVP_PICK_SHIP_REQ(long shipUID)
		{
			NKMPacket_DRAFT_PVP_PICK_SHIP_REQ nkmpacket_DRAFT_PVP_PICK_SHIP_REQ = new NKMPacket_DRAFT_PVP_PICK_SHIP_REQ();
			nkmpacket_DRAFT_PVP_PICK_SHIP_REQ.shipUid = shipUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DRAFT_PVP_PICK_SHIP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x0600372A RID: 14122 RVA: 0x0011C308 File Offset: 0x0011A508
		public static void OnRecv(NKMPacket_DRAFT_PVP_PICK_SHIP_ACK sPacket)
		{
			NKC_SCEN_GAUNTLET_LEAGUE_ROOM nkc_SCEN_GAUNTLET_LEAGUE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM == null)
			{
				return;
			}
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.OpenOperatorSelect(NKCLeaguePVPMgr.IsObserver());
		}

		// Token: 0x0600372B RID: 14123 RVA: 0x0011C334 File Offset: 0x0011A534
		public static void Send_NKMPacket_DRAFT_PVP_PICK_OPERATOR_REQ(long operatorUID)
		{
			NKMPacket_DRAFT_PVP_PICK_OPERATOR_REQ nkmpacket_DRAFT_PVP_PICK_OPERATOR_REQ = new NKMPacket_DRAFT_PVP_PICK_OPERATOR_REQ();
			nkmpacket_DRAFT_PVP_PICK_OPERATOR_REQ.operatorUid = operatorUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DRAFT_PVP_PICK_OPERATOR_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x0011C364 File Offset: 0x0011A564
		public static void OnRecv(NKMPacket_DRAFT_PVP_PICK_OPERATOR_ACK sPacket)
		{
			NKC_SCEN_GAUNTLET_LEAGUE_ROOM nkc_SCEN_GAUNTLET_LEAGUE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM == null)
			{
				return;
			}
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.OpenLeaderSelect();
		}

		// Token: 0x0600372D RID: 14125 RVA: 0x0011C38C File Offset: 0x0011A58C
		public static void Send_NKMPacket_DRAFT_PVP_PICK_LEADER_REQ(int leaderIndex)
		{
			NKMPacket_DRAFT_PVP_PICK_LEADER_REQ nkmpacket_DRAFT_PVP_PICK_LEADER_REQ = new NKMPacket_DRAFT_PVP_PICK_LEADER_REQ();
			nkmpacket_DRAFT_PVP_PICK_LEADER_REQ.leaderIndex = leaderIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DRAFT_PVP_PICK_LEADER_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x0011C3BC File Offset: 0x0011A5BC
		public static void OnRecv(NKMPacket_DRAFT_PVP_PICK_LEADER_ACK sPacket)
		{
			NKC_SCEN_GAUNTLET_LEAGUE_ROOM nkc_SCEN_GAUNTLET_LEAGUE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM == null)
			{
				return;
			}
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.ApplyPickETCResults();
			if (NKCLeaguePVPMgr.GetRightDraftTeamData().leaderIndex == -1)
			{
				NKCLeaguePVPMgr.ShowSequenceGuidePopup(NKCStringTable.GetString("SI_DP_GAUNTLET_LEAGUE_PICK_SEQUENCE_WAIT", false));
			}
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x0011C400 File Offset: 0x0011A600
		public static void OnRecv(NKMPacket_DRAFT_PVP_SELECT_UNIT_ACK sPacket)
		{
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x0011C404 File Offset: 0x0011A604
		public static void Send_NKMPacket_DRAFT_PVP_SELECT_UNIT_REQ(long unitUID)
		{
			if (!NKCLeaguePVPMgr.CanPickUnit(NKCLeaguePVPMgr.MyTeamType))
			{
				return;
			}
			NKMPacket_DRAFT_PVP_SELECT_UNIT_REQ nkmpacket_DRAFT_PVP_SELECT_UNIT_REQ = new NKMPacket_DRAFT_PVP_SELECT_UNIT_REQ();
			nkmpacket_DRAFT_PVP_SELECT_UNIT_REQ.unitUid = unitUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DRAFT_PVP_SELECT_UNIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x0011C440 File Offset: 0x0011A640
		public static void OpenMatchUI()
		{
			NKC_SCEN_GAUNTLET_LEAGUE_ROOM nkc_SCEN_GAUNTLET_LEAGUE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM == null)
			{
				return;
			}
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.HideSequenceGuidePopup();
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.Close();
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMatch.Open(NKCLeaguePVPMgr.m_DraftPvpRoomData.stateEndTime);
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x0011C487 File Offset: 0x0011A687
		public static DraftPvpRoomData.DraftTeamData GetDraftTeamData(NKM_TEAM_TYPE targetTeamType)
		{
			if (NKCLeaguePVPMgr.m_DraftPvpRoomData.draftTeamDataA.teamType == targetTeamType)
			{
				return NKCLeaguePVPMgr.m_DraftPvpRoomData.draftTeamDataA;
			}
			return NKCLeaguePVPMgr.m_DraftPvpRoomData.draftTeamDataB;
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x0011C4B0 File Offset: 0x0011A6B0
		public static bool SelectGlobalBanUnit(NKMUnitTempletBase unitTempletBase)
		{
			if (NKCLeaguePVPMgr.GetDraftTeamData(NKCLeaguePVPMgr.m_myTeamType).globalBanUnitIdList.Count >= 2)
			{
				return false;
			}
			NKMPacket_DRAFT_PVP_GLOBAL_BAN_REQ nkmpacket_DRAFT_PVP_GLOBAL_BAN_REQ = new NKMPacket_DRAFT_PVP_GLOBAL_BAN_REQ();
			nkmpacket_DRAFT_PVP_GLOBAL_BAN_REQ.unitId = unitTempletBase.m_UnitID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DRAFT_PVP_GLOBAL_BAN_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
			return true;
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x0011C4FC File Offset: 0x0011A6FC
		public static void OnRecv(NKMPacket_DRAFT_PVP_GLOBAL_BAN_ACK sPacket)
		{
			NKC_SCEN_GAUNTLET_LEAGUE_ROOM nkc_SCEN_GAUNTLET_LEAGUE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM == null)
			{
				return;
			}
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeageGlobalBan.BanSelectedUnit();
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x0011C523 File Offset: 0x0011A723
		public static void OnRecv(NKMPacket_PVP_GAME_MATCH_COMPLETE_NOT cNKMPacket_PVP_GAME_MATCH_COMPLETE_NOT)
		{
			NKCLeaguePVPMgr.m_DraftPvpRoomData = null;
			NKCLeaguePVPMgr.m_myTeamType = NKM_TEAM_TYPE.NTT_INVALID;
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x0011C534 File Offset: 0x0011A734
		public static void ProcessReLogin(NKMGameData gameData, DraftPvpRoomData draftPvpRoomData)
		{
			if (NKCLeaguePVPMgr.m_DraftPvpRoomData == null)
			{
				NKCLeaguePVPMgr.InitDraftRoom(new DraftPvpRoomData
				{
					roomState = DRAFT_PVP_ROOM_STATE.INIT,
					draftTeamDataA = draftPvpRoomData.draftTeamDataA,
					draftTeamDataB = draftPvpRoomData.draftTeamDataB,
					stateEndTime = draftPvpRoomData.stateEndTime
				});
				NKCLeaguePVPMgr.m_ReservedPvpRoomData = draftPvpRoomData;
				return;
			}
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAUNTLET_LEAGUE_ROOM)
			{
				NKCLeaguePVPMgr.ChangeScene();
			}
			NKCLeaguePVPMgr.m_ReservedPvpRoomData = draftPvpRoomData;
			NKCLeaguePVPMgr.UpdateReservedRoomData();
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x0011C5A4 File Offset: 0x0011A7A4
		public static void UpdateReservedRoomData()
		{
			if (NKCLeaguePVPMgr.m_ReservedPvpRoomData != null)
			{
				NKMPacket_LEAGUE_PVP_UPDATED_NOT nkmpacket_LEAGUE_PVP_UPDATED_NOT = new NKMPacket_LEAGUE_PVP_UPDATED_NOT();
				nkmpacket_LEAGUE_PVP_UPDATED_NOT.roomData = NKCLeaguePVPMgr.m_ReservedPvpRoomData;
				Log.Info("[League][Relogin] RoomState[" + NKCLeaguePVPMgr.m_ReservedPvpRoomData.roomState.ToString() + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 794);
				NKCLeaguePVPMgr.OnRecv(nkmpacket_LEAGUE_PVP_UPDATED_NOT);
			}
			NKCLeaguePVPMgr.m_ReservedPvpRoomData = null;
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x0011C608 File Offset: 0x0011A808
		public static bool OnRecv(NKMPacket_GAME_END_NOT cPacket_GAME_END_NOT)
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAUNTLET_LEAGUE_ROOM)
			{
				return false;
			}
			BATTLE_RESULT_TYPE battle_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_LOSE;
			if (cPacket_GAME_END_NOT.pvpResultData != null)
			{
				if (cPacket_GAME_END_NOT.pvpResultData.result == PVP_RESULT.WIN)
				{
					battle_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_WIN;
				}
				else if (cPacket_GAME_END_NOT.pvpResultData.result == PVP_RESULT.LOSE)
				{
					battle_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_LOSE;
				}
				else
				{
					battle_RESULT_TYPE = BATTLE_RESULT_TYPE.BRT_DRAW;
				}
			}
			NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.PvPGameFinished, (int)battle_RESULT_TYPE, null);
			NKMItemMiscData cNKMItemMiscData = null;
			if (cPacket_GAME_END_NOT.pvpResultData != null)
			{
				cNKMItemMiscData = cPacket_GAME_END_NOT.pvpResultData.pvpPoint;
			}
			NKCScenManager.GetScenManager().GetGameClient();
			NKCUIResult.BattleResultData resultUIData = NKCUIResult.MakePvPResultData(battle_RESULT_TYPE, cNKMItemMiscData, new NKCUIBattleStatistics.BattleData(), NKM_GAME_TYPE.NGT_PVP_LEAGUE);
			NKCUIGauntletResult.SetResultData(resultUIData);
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_LEAGUE);
			NKCPacketHandlersLobby.UpdateLeagueGiveupUserData(cPacket_GAME_END_NOT.pvpResultData);
			NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.UpdateItemInfo(cPacket_GAME_END_NOT.costItemDataList);
			NKCUIGauntletResult.OnClose <>9__1;
			NKCScenManager.GetScenManager().Get_NKC_SCEN_GAME_RESULT().SetDoAtScenStart(delegate
			{
				NKCUIGauntletResult nkcuigauntletResult = NKCUIManager.NKCUIGauntletResult;
				NKCUIGauntletResult.OnClose dOnClose;
				if ((dOnClose = <>9__1) == null)
				{
					dOnClose = (<>9__1 = delegate()
					{
						NKCUIResult.Instance.OpenComplexResult(NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData, resultUIData.m_RewardData, delegate
						{
							NKCContentManager.ShowContentUnlockPopup(delegate
							{
								NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
							}, new STAGE_UNLOCK_REQ_TYPE[]
							{
								STAGE_UNLOCK_REQ_TYPE.SURT_PVP_RANK_SCORE,
								STAGE_UNLOCK_REQ_TYPE.SURT_PVP_RANK_SCORE_RECORD,
								STAGE_UNLOCK_REQ_TYPE.SURT_PVP_ASYNC_SCORE,
								STAGE_UNLOCK_REQ_TYPE.SURT_PVP_ASYNC_SCORE_RECORD
							});
						}, resultUIData.m_OrgDoubleToken, resultUIData.m_battleData, true, true);
					});
				}
				nkcuigauntletResult.Open(dOnClose);
			});
			return true;
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x0011C6FC File Offset: 0x0011A8FC
		public static void CheckLeagueModeBuff(NKMBuffData buffData, NKCUnitClient unitClient)
		{
			NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
			if (gameClient == null)
			{
				return;
			}
			if (buffData.m_NKMBuffTemplet == NKMPvpCommonConst.Instance.LeaguePvp.UiRageBuff)
			{
				Log.Info("[Leage] RageMod On!", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 885);
				gameClient.GetGameHud().SetRageMode(true, unitClient.IsMyTeam());
			}
			if (buffData.m_NKMBuffTemplet == NKMPvpCommonConst.Instance.LeaguePvp.UiDeadlineBuff && gameClient.GetGameHud().DeadlineBuffLevel < (int)buffData.m_BuffSyncData.m_BuffStatLevel)
			{
				Log.Info(string.Format("[Leage] Deadline On[{0}]", buffData.m_BuffSyncData.m_BuffStatLevel), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCLeaguePVPMgr.cs", 892);
				gameClient.GetGameHud().SetDeadlineMode((int)buffData.m_BuffSyncData.m_BuffStatLevel, unitClient.GetBuffDescText(NKMPvpCommonConst.Instance.LeaguePvp.DeadlineBuff, buffData.m_BuffSyncData));
			}
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x0011C7DE File Offset: 0x0011A9DE
		public static bool CanLeaveRoom()
		{
			return NKCLeaguePVPMgr.DraftRoomData.roomState >= DRAFT_PVP_ROOM_STATE.BAN_ALL && NKCLeaguePVPMgr.DraftRoomData.roomState < DRAFT_PVP_ROOM_STATE.PICK_ETC;
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x0011C800 File Offset: 0x0011AA00
		public static void CheckLeaveRoomState()
		{
			NKC_SCEN_GAUNTLET_LEAGUE_ROOM nkc_SCEN_GAUNTLET_LEAGUE_ROOM = NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LEAGUE_ROOM();
			if (nkc_SCEN_GAUNTLET_LEAGUE_ROOM == null)
			{
				return;
			}
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeageGlobalBan.SetLeaveRoomState();
			nkc_SCEN_GAUNTLET_LEAGUE_ROOM.m_gauntletLeagueMain.SetLeaveRoomState();
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x0011C832 File Offset: 0x0011AA32
		public static bool IsObserver()
		{
			return NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.PVP_PRIVATE_OBSERVE_MODE) && (NKCLeaguePVPMgr.m_myTeamType != NKM_TEAM_TYPE.NTT_A1 && NKCLeaguePVPMgr.m_myTeamType != NKM_TEAM_TYPE.NTT_B1);
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x0011C852 File Offset: 0x0011AA52
		public static bool IsPlayer(long userUid)
		{
			return NKCLeaguePVPMgr.m_DraftPvpRoomData.draftTeamDataA.userProfileData.commonProfile.userUid == userUid || NKCLeaguePVPMgr.m_DraftPvpRoomData.draftTeamDataB.userProfileData.commonProfile.userUid == userUid;
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x0011C88F File Offset: 0x0011AA8F
		public static bool IsPrivate()
		{
			return NKCLeaguePVPMgr.m_DraftPvpRoomData != null && NKCLeaguePVPMgr.m_DraftPvpRoomData.gameType == NKM_GAME_TYPE.NGT_PVP_PRIVATE;
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x0011C8AB File Offset: 0x0011AAAB
		private static void CancelAllProcess()
		{
			NKCLeaguePVPMgr.ResetData();
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAUNTLET_LEAGUE_ROOM)
			{
				NKCScenManager.GetScenManager().Get_NKC_SCEN_GAUNTLET_LOBBY().SetReservedLobbyTab(NKC_GAUNTLET_LOBBY_TAB.NGLT_PRIVATE);
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GAUNTLET_LOBBY, true);
			}
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x0011C8DD File Offset: 0x0011AADD
		private static void ResetData()
		{
			NKCLeaguePVPMgr.m_DraftPvpRoomData = null;
			NKCLeaguePVPMgr.m_ReservedPvpRoomData = null;
			NKCLeaguePVPMgr.m_myTeamType = NKM_TEAM_TYPE.NTT_INVALID;
			NKCLeaguePVPMgr.m_LeagueRoomStarted = false;
		}

		// Token: 0x0400340F RID: 13327
		private static DraftPvpRoomData m_DraftPvpRoomData = null;

		// Token: 0x04003410 RID: 13328
		private static DraftPvpRoomData m_ReservedPvpRoomData = null;

		// Token: 0x04003411 RID: 13329
		public const int MaxGlobalBanCount = 2;

		// Token: 0x04003412 RID: 13330
		private static NKM_TEAM_TYPE m_myTeamType = NKM_TEAM_TYPE.NTT_INVALID;

		// Token: 0x04003413 RID: 13331
		public static bool m_LeagueRoomStarted = false;

		// Token: 0x04003414 RID: 13332
		private static int m_requestedBanIndex = -1;
	}
}

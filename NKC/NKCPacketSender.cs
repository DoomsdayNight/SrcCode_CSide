using System;
using System.Collections.Generic;
using ClientPacket.Account;
using ClientPacket.Chat;
using ClientPacket.Common;
using ClientPacket.Community;
using ClientPacket.Contract;
using ClientPacket.Event;
using ClientPacket.Game;
using ClientPacket.Guild;
using ClientPacket.Item;
using ClientPacket.LeaderBoard;
using ClientPacket.Mode;
using ClientPacket.Negotiation;
using ClientPacket.Office;
using ClientPacket.Pvp;
using ClientPacket.Raid;
using ClientPacket.Service;
using ClientPacket.Shop;
using ClientPacket.Unit;
using ClientPacket.User;
using ClientPacket.Warfare;
using ClientPacket.WorldMap;
using NKC.PacketHandler;
using NKC.UI;
using NKM;
using NKM.Contract2;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006B8 RID: 1720
	public class NKCPacketSender
	{
		// Token: 0x06003938 RID: 14648 RVA: 0x00128920 File Offset: 0x00126B20
		public static void Send_NKMPacket_PHASE_START_REQ(int stageId, NKMDeckIndex deckIndex)
		{
			NKMPacket_PHASE_START_REQ nkmpacket_PHASE_START_REQ = new NKMPacket_PHASE_START_REQ();
			nkmpacket_PHASE_START_REQ.stageId = stageId;
			nkmpacket_PHASE_START_REQ.deckIndex = deckIndex;
			nkmpacket_PHASE_START_REQ.eventDeckData = null;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PHASE_START_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x0012895C File Offset: 0x00126B5C
		public static void Send_NKMPacket_PHASE_START_REQ(int stageId, NKMEventDeckData eventDeckData)
		{
			NKMPacket_PHASE_START_REQ nkmpacket_PHASE_START_REQ = new NKMPacket_PHASE_START_REQ();
			nkmpacket_PHASE_START_REQ.stageId = stageId;
			nkmpacket_PHASE_START_REQ.deckIndex = NKMDeckIndex.None;
			nkmpacket_PHASE_START_REQ.eventDeckData = eventDeckData;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PHASE_START_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x0012899B File Offset: 0x00126B9B
		public static void Send_NKMPacket_GAME_LOAD_REQ(byte DeckIndex, int stageID, int diveID, string dungeonStrID, int palaceID, bool bLocal = false, int multiplyReward = 1, int fierceID = 0)
		{
			NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(DeckIndex, stageID, diveID, NKMDungeonManager.GetDungeonID(dungeonStrID), palaceID, bLocal, multiplyReward, fierceID);
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x001289B4 File Offset: 0x00126BB4
		public static void Send_NKMPacket_GAME_LOAD_REQ(byte DeckIndex, int stageID, int diveID, int dungeonID, int palaceID, bool bLocal = false, int multiplyReward = 1, int fierceID = 0)
		{
			NKMPacket_GAME_LOAD_REQ nkmpacket_GAME_LOAD_REQ = new NKMPacket_GAME_LOAD_REQ();
			nkmpacket_GAME_LOAD_REQ.isDev = false;
			nkmpacket_GAME_LOAD_REQ.selectDeckIndex = DeckIndex;
			nkmpacket_GAME_LOAD_REQ.stageID = stageID;
			nkmpacket_GAME_LOAD_REQ.diveStageID = diveID;
			nkmpacket_GAME_LOAD_REQ.dungeonID = dungeonID;
			nkmpacket_GAME_LOAD_REQ.eventDeckData = null;
			nkmpacket_GAME_LOAD_REQ.palaceID = palaceID;
			nkmpacket_GAME_LOAD_REQ.rewardMultiply = multiplyReward;
			nkmpacket_GAME_LOAD_REQ.fierceBossId = fierceID;
			if (!bLocal)
			{
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_LOAD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				return;
			}
			NKCLocalPacketHandler.SendPacketToLocalServer(nkmpacket_GAME_LOAD_REQ);
			NKMPopUpBox.OpenWaitBox(0f, "");
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x00128A36 File Offset: 0x00126C36
		public static void Send_NKMPacket_GAME_LOAD_REQ(NKMEventDeckData eventDeckData, int stageID, int diveID, string dungeonStrID, int palaceID, bool bLocal = false, int multiplyReward = 1, int fierceID = 0)
		{
			NKCPacketSender.Send_NKMPacket_GAME_LOAD_REQ(eventDeckData, stageID, diveID, NKMDungeonManager.GetDungeonID(dungeonStrID), palaceID, bLocal, multiplyReward, fierceID);
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x00128A50 File Offset: 0x00126C50
		public static void Send_NKMPacket_GAME_LOAD_REQ(NKMEventDeckData eventDeckData, int stageID, int diveID, int dungeonID, int palaceID, bool bLocal = false, int multiplyReward = 1, int fierceID = 0)
		{
			NKMPacket_GAME_LOAD_REQ nkmpacket_GAME_LOAD_REQ = new NKMPacket_GAME_LOAD_REQ();
			nkmpacket_GAME_LOAD_REQ.isDev = false;
			nkmpacket_GAME_LOAD_REQ.selectDeckIndex = 0;
			nkmpacket_GAME_LOAD_REQ.stageID = stageID;
			nkmpacket_GAME_LOAD_REQ.diveStageID = diveID;
			nkmpacket_GAME_LOAD_REQ.dungeonID = dungeonID;
			nkmpacket_GAME_LOAD_REQ.eventDeckData = eventDeckData;
			nkmpacket_GAME_LOAD_REQ.rewardMultiply = multiplyReward;
			nkmpacket_GAME_LOAD_REQ.palaceID = palaceID;
			nkmpacket_GAME_LOAD_REQ.fierceBossId = fierceID;
			if (!bLocal)
			{
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_LOAD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				return;
			}
			NKCLocalPacketHandler.SendPacketToLocalServer(nkmpacket_GAME_LOAD_REQ);
			NKMPopUpBox.OpenWaitBox(0f, "");
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x00128AD2 File Offset: 0x00126CD2
		public static void Send_NKMPacket_PRACTICE_GAME_LOAD_REQ(NKMUnitData cNKMUnitData)
		{
			NKCLocalPacketHandler.SendPacketToLocalServer(new NKMPacket_PRACTICE_GAME_LOAD_REQ
			{
				practiceUnitData = cNKMUnitData
			});
			NKMPopUpBox.OpenWaitBox(0f, "");
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x00128AF4 File Offset: 0x00126CF4
		public static void Send_NKMPacket_INFORM_MY_LOADING_PROGRESS_REQ(byte progress)
		{
			NKMPacket_INFORM_MY_LOADING_PROGRESS_REQ nkmpacket_INFORM_MY_LOADING_PROGRESS_REQ = new NKMPacket_INFORM_MY_LOADING_PROGRESS_REQ();
			nkmpacket_INFORM_MY_LOADING_PROGRESS_REQ.progress = progress;
			if (!NKCReplayMgr.IsPlayingReplay())
			{
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_INFORM_MY_LOADING_PROGRESS_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
				return;
			}
			NKCReplayMgr nkcreplaMgr = NKCReplayMgr.GetNKCReplaMgr();
			if (nkcreplaMgr == null)
			{
				return;
			}
			nkcreplaMgr.OnRecv(nkmpacket_INFORM_MY_LOADING_PROGRESS_REQ);
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x00128B39 File Offset: 0x00126D39
		public static void Send_NKMPacket_DEV_GAME_LOAD_REQ(string dungeonStrID)
		{
			NKCPacketSender.Send_NKMPacket_DEV_GAME_LOAD_REQ(NKMDungeonManager.GetDungeonID(dungeonStrID));
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x00128B46 File Offset: 0x00126D46
		public static void Send_NKMPacket_DEV_GAME_LOAD_REQ(int dungeonID)
		{
			NKCLocalPacketHandler.SendPacketToLocalServer(new NKMPacket_GAME_LOAD_REQ
			{
				isDev = true,
				dungeonID = dungeonID
			});
			NKMPopUpBox.OpenWaitBox(0f, "");
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x00128B70 File Offset: 0x00126D70
		public static void Send_Packet_GAME_LOAD_COMPLETE_REQ(bool bIntrude)
		{
			NKMPacket_GAME_LOAD_COMPLETE_REQ nkmpacket_GAME_LOAD_COMPLETE_REQ = new NKMPacket_GAME_LOAD_COMPLETE_REQ();
			nkmpacket_GAME_LOAD_COMPLETE_REQ.isIntrude = bIntrude;
			Debug.Log(string.Format("[NKMPacket_GAME_LOAD_COMPLETE_REQ] isIntrude : {0}", nkmpacket_GAME_LOAD_COMPLETE_REQ.isIntrude));
			if (NKCReplayMgr.IsPlayingReplay())
			{
				NKCReplayMgr nkcreplaMgr = NKCReplayMgr.GetNKCReplaMgr();
				if (nkcreplaMgr == null)
				{
					return;
				}
				nkcreplaMgr.OnRecv(nkmpacket_GAME_LOAD_COMPLETE_REQ);
				return;
			}
			else
			{
				if (!NKCScenManager.GetScenManager().GetGameClient().GetGameData().m_bLocal)
				{
					NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_LOAD_COMPLETE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
					return;
				}
				NKCLocalPacketHandler.SendPacketToLocalServer(nkmpacket_GAME_LOAD_COMPLETE_REQ);
				return;
			}
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x00128BEC File Offset: 0x00126DEC
		public static void Send_NKMPacket_GAME_GIVEUP_REQ()
		{
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_GAME)
			{
				return;
			}
			if (NKCScenManager.GetScenManager().GetGameClient() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameData() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameData().m_NKM_GAME_TYPE == NKM_GAME_TYPE.NGT_TUTORIAL && !NKCTutorialManager.CanGiveupDungeon(NKCScenManager.GetScenManager().GetGameClient().GetGameData().m_DungeonID))
			{
				Debug.LogWarning("impossible to giveup prologue dungeon");
				return;
			}
			NKMPacket_GAME_GIVEUP_REQ packet = new NKMPacket_GAME_GIVEUP_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x00128C7C File Offset: 0x00126E7C
		public static void Send_NKMPacket_CHANGE_NICKNAME_REQ(string nickname)
		{
			NKMPacket_CHANGE_NICKNAME_REQ nkmpacket_CHANGE_NICKNAME_REQ = new NKMPacket_CHANGE_NICKNAME_REQ();
			nkmpacket_CHANGE_NICKNAME_REQ.nickname = nickname;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_CHANGE_NICKNAME_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x00128CA9 File Offset: 0x00126EA9
		public static void Send_NKMPacket_NKMPacket_CONNECT_CHECK_REQ()
		{
			NKCScenManager.GetScenManager().GetConnectGame().Send(NKCPacketSender.m_NKMPacket_CONNECT_CHECK_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, false);
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x00128CC4 File Offset: 0x00126EC4
		public static void Send_NKMPacket_LOCK_UNIT_REQ(long targetUnitUID, bool bLock)
		{
			NKMPacket_LOCK_UNIT_REQ nkmpacket_LOCK_UNIT_REQ = new NKMPacket_LOCK_UNIT_REQ();
			nkmpacket_LOCK_UNIT_REQ.unitUID = targetUnitUID;
			nkmpacket_LOCK_UNIT_REQ.isLock = bLock;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_LOCK_UNIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x00128CF8 File Offset: 0x00126EF8
		public static void Send_NKMPacket_FAVORITE_UNIT_REQ(long targetUnitUID, bool bFavorite)
		{
			NKMPacket_FAVORITE_UNIT_REQ nkmpacket_FAVORITE_UNIT_REQ = new NKMPacket_FAVORITE_UNIT_REQ();
			nkmpacket_FAVORITE_UNIT_REQ.unitUid = targetUnitUID;
			nkmpacket_FAVORITE_UNIT_REQ.isFavorite = bFavorite;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FAVORITE_UNIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x00128D2C File Offset: 0x00126F2C
		public static void Send_NKMPacket_DECK_UNIT_SWAP_REQ(NKMDeckIndex deckIndex, int slotFrom, int slotTo)
		{
			NKMPacket_DECK_UNIT_SWAP_REQ nkmpacket_DECK_UNIT_SWAP_REQ = new NKMPacket_DECK_UNIT_SWAP_REQ();
			nkmpacket_DECK_UNIT_SWAP_REQ.deckIndex = deckIndex;
			nkmpacket_DECK_UNIT_SWAP_REQ.slotIndexFrom = (byte)slotFrom;
			nkmpacket_DECK_UNIT_SWAP_REQ.slotIndexTo = (byte)slotTo;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DECK_UNIT_SWAP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x00128D6C File Offset: 0x00126F6C
		public static void Send_NKMPacket_DECK_UNLOCK_REQ(NKM_DECK_TYPE eType)
		{
			NKMPacket_DECK_UNLOCK_REQ nkmpacket_DECK_UNLOCK_REQ = new NKMPacket_DECK_UNLOCK_REQ();
			nkmpacket_DECK_UNLOCK_REQ.deckType = eType;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DECK_UNLOCK_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x00128D9C File Offset: 0x00126F9C
		public static void Send_NKMPacket_DECK_UNIT_SET_REQ(NKMDeckIndex deckIndex, int slotIndex, long unitUID)
		{
			NKMPacket_DECK_UNIT_SET_REQ nkmpacket_DECK_UNIT_SET_REQ = new NKMPacket_DECK_UNIT_SET_REQ();
			nkmpacket_DECK_UNIT_SET_REQ.deckIndex = deckIndex;
			nkmpacket_DECK_UNIT_SET_REQ.slotIndex = (byte)slotIndex;
			nkmpacket_DECK_UNIT_SET_REQ.unitUID = unitUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DECK_UNIT_SET_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x00128DD8 File Offset: 0x00126FD8
		public static void Send_NKMPacket_DECK_SHIP_SET_REQ(NKMDeckIndex deckIndex, long shipUID)
		{
			NKMPacket_DECK_SHIP_SET_REQ nkmpacket_DECK_SHIP_SET_REQ = new NKMPacket_DECK_SHIP_SET_REQ();
			nkmpacket_DECK_SHIP_SET_REQ.deckIndex = deckIndex;
			nkmpacket_DECK_SHIP_SET_REQ.shipUID = shipUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DECK_SHIP_SET_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x00128E0C File Offset: 0x0012700C
		public static void Send_Packet_DECK_UNIT_SET_LEADER_REQ(NKMDeckIndex deckIndex, sbyte leaderIndex)
		{
			NKMPacket_DECK_UNIT_SET_LEADER_REQ nkmpacket_DECK_UNIT_SET_LEADER_REQ = new NKMPacket_DECK_UNIT_SET_LEADER_REQ();
			nkmpacket_DECK_UNIT_SET_LEADER_REQ.deckIndex = deckIndex;
			nkmpacket_DECK_UNIT_SET_LEADER_REQ.leaderSlotIndex = leaderIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DECK_UNIT_SET_LEADER_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x00128E40 File Offset: 0x00127040
		public static void Send_Packet_DECK_UNIT_AUTO_SET_REQ(NKMDeckIndex deckIndex, List<long> unitUIDList, long shipUID, long operatorUID)
		{
			if (unitUIDList == null)
			{
				return;
			}
			NKMPacket_DECK_UNIT_AUTO_SET_REQ nkmpacket_DECK_UNIT_AUTO_SET_REQ = new NKMPacket_DECK_UNIT_AUTO_SET_REQ();
			nkmpacket_DECK_UNIT_AUTO_SET_REQ.deckIndex = deckIndex;
			nkmpacket_DECK_UNIT_AUTO_SET_REQ.unitUIDList = unitUIDList;
			nkmpacket_DECK_UNIT_AUTO_SET_REQ.shipUID = shipUID;
			nkmpacket_DECK_UNIT_AUTO_SET_REQ.operatorUid = operatorUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DECK_UNIT_AUTO_SET_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x00128E88 File Offset: 0x00127088
		public static void Send_NKMPacket_DECK_NAME_UPDATE_REQ(NKMDeckIndex deckIndex, string name)
		{
			name = name.Trim().Replace("\r", "").Replace("\n", "");
			NKMPacket_DECK_NAME_UPDATE_REQ nkmpacket_DECK_NAME_UPDATE_REQ = new NKMPacket_DECK_NAME_UPDATE_REQ();
			nkmpacket_DECK_NAME_UPDATE_REQ.deckIndex = deckIndex;
			nkmpacket_DECK_NAME_UPDATE_REQ.name = name;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DECK_NAME_UPDATE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x00128EE4 File Offset: 0x001270E4
		public static void Send_NKMPacket_POST_LIST_REQ(long lastindex)
		{
			NKMPacket_POST_LIST_REQ nkmpacket_POST_LIST_REQ = new NKMPacket_POST_LIST_REQ();
			nkmpacket_POST_LIST_REQ.lastPostIndex = lastindex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_POST_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x00128F14 File Offset: 0x00127114
		public static void Send_NKMPacket_POST_RECEIVE_REQ(long index)
		{
			NKMPacket_POST_RECEIVE_REQ nkmpacket_POST_RECEIVE_REQ = new NKMPacket_POST_RECEIVE_REQ();
			nkmpacket_POST_RECEIVE_REQ.postIndex = index;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_POST_RECEIVE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x00128F44 File Offset: 0x00127144
		public static void Send_NKMPacket_REMOVE_UNIT_REQ(List<long> lstTargetUnitUID)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMArmyData armyData = myUserData.m_ArmyData;
			foreach (long uid in lstTargetUnitUID)
			{
				NKMUnitData unitOrTrophyFromUID = armyData.GetUnitOrTrophyFromUID(uid);
				if (unitOrTrophyFromUID == null)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL, NKCUtilString.GET_STRING_NO_EXIST_UNIT, null, "");
					return;
				}
				NKM_ERROR_CODE canDeleteUnit = NKMUnitManager.GetCanDeleteUnit(unitOrTrophyFromUID, myUserData);
				if (canDeleteUnit != NKM_ERROR_CODE.NEC_OK)
				{
					switch (canDeleteUnit)
					{
					case NKM_ERROR_CODE.NEC_FAIL_UNIT_LOCKED:
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL, NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL_LOCKED, null, "");
						return;
					case NKM_ERROR_CODE.NEC_FAIL_UNIT_IN_DECK:
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL, NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL_IN_DECK, null, "");
						return;
					case NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_LOBBYUNIT:
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL, NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL_MAINUNIT, null, "");
						return;
					case NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_WORLDMAP_LEADER:
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL, NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL_WORLDMAP_LEADER, null, "");
						return;
					}
					NKCPopupOKCancel.OpenOKBox(canDeleteUnit, null, "");
					return;
				}
			}
			armyData.InitUnitDelete();
			armyData.SetUnitDeleteList(lstTargetUnitUID);
			NKCPacketSender.Send_NKMPacket_REMOVE_UNIT_REQ();
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x00129080 File Offset: 0x00127280
		public static void Send_NKMPacket_REMOVE_UNIT_REQ()
		{
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			if (armyData.IsEmptyUnitDeleteList)
			{
				return;
			}
			List<long> unitDeleteList = armyData.GetUnitDeleteList();
			NKMPacket_REMOVE_UNIT_REQ nkmpacket_REMOVE_UNIT_REQ = new NKMPacket_REMOVE_UNIT_REQ();
			nkmpacket_REMOVE_UNIT_REQ.removeUnitUIDList = unitDeleteList;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_REMOVE_UNIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x001290D0 File Offset: 0x001272D0
		public static void Send_NKMPacket_RANDOM_ITEM_BOX_OPEN_REQ(int id, int count)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData.m_InventoryData.GetItemMisc(id) == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_INVALID_ITEM_ID, null, "");
				return;
			}
			if (myUserData.m_InventoryData.GetCountMiscItem(id) < (long)count)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM, null, "");
				return;
			}
			NKMPacket_RANDOM_ITEM_BOX_OPEN_REQ nkmpacket_RANDOM_ITEM_BOX_OPEN_REQ = new NKMPacket_RANDOM_ITEM_BOX_OPEN_REQ();
			nkmpacket_RANDOM_ITEM_BOX_OPEN_REQ.itemID = id;
			nkmpacket_RANDOM_ITEM_BOX_OPEN_REQ.count = count;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_RANDOM_ITEM_BOX_OPEN_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x0012914C File Offset: 0x0012734C
		public static void Send_NKMPacket_CHOICE_ITEM_USE_REQ(int itemID, int rewardID, int count, int setOptionID = 0)
		{
			if (NKCScenManager.GetScenManager().GetMyUserData().m_InventoryData.GetItemMisc(itemID) == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKM_ERROR_CODE.NEC_FAIL_INVALID_ITEM_ID, null, "");
				return;
			}
			NKMPacket_CHOICE_ITEM_USE_REQ nkmpacket_CHOICE_ITEM_USE_REQ = new NKMPacket_CHOICE_ITEM_USE_REQ();
			nkmpacket_CHOICE_ITEM_USE_REQ.itemId = itemID;
			nkmpacket_CHOICE_ITEM_USE_REQ.rewardId = rewardID;
			nkmpacket_CHOICE_ITEM_USE_REQ.count = count;
			nkmpacket_CHOICE_ITEM_USE_REQ.setOptionId = setOptionID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_CHOICE_ITEM_USE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x001291B8 File Offset: 0x001273B8
		public static void Send_NKMPacket_MISC_CONTRACT_OPEN_REQ(int itemID, int count)
		{
			NKMPacket_MISC_CONTRACT_OPEN_REQ nkmpacket_MISC_CONTRACT_OPEN_REQ = new NKMPacket_MISC_CONTRACT_OPEN_REQ();
			nkmpacket_MISC_CONTRACT_OPEN_REQ.miscItemId = itemID;
			nkmpacket_MISC_CONTRACT_OPEN_REQ.count = count;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_MISC_CONTRACT_OPEN_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x001291EC File Offset: 0x001273EC
		public static void Send_NKMPacket_SET_UNIT_SKIN_REQ(long unitUID, int skinID)
		{
			NKMPacket_SET_UNIT_SKIN_REQ nkmpacket_SET_UNIT_SKIN_REQ = new NKMPacket_SET_UNIT_SKIN_REQ();
			nkmpacket_SET_UNIT_SKIN_REQ.unitUID = unitUID;
			nkmpacket_SET_UNIT_SKIN_REQ.skinID = skinID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SET_UNIT_SKIN_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x00129220 File Offset: 0x00127420
		public static void Send_NKMPacket_UNIT_REVIEW_COMMENT_AND_SCORE_REQ(int unitID, int pageNum = 1, bool bOrderByVotedCount = false)
		{
			if (NKMPopUpBox.IsOpenedWaitBox())
			{
				return;
			}
			NKMPacket_UNIT_REVIEW_COMMENT_AND_SCORE_REQ nkmpacket_UNIT_REVIEW_COMMENT_AND_SCORE_REQ = new NKMPacket_UNIT_REVIEW_COMMENT_AND_SCORE_REQ();
			nkmpacket_UNIT_REVIEW_COMMENT_AND_SCORE_REQ.unitID = unitID;
			nkmpacket_UNIT_REVIEW_COMMENT_AND_SCORE_REQ.isOrderByVotedCount = bOrderByVotedCount;
			nkmpacket_UNIT_REVIEW_COMMENT_AND_SCORE_REQ.pageNumber = pageNum;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_REVIEW_COMMENT_AND_SCORE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x00129264 File Offset: 0x00127464
		public static void Send_NKMPacket_UNIT_REVIEW_COMMENT_LIST_REQ(int unitID, int pageNum = 1, bool bOrderByVotedCount = true)
		{
			if (NKMPopUpBox.IsOpenedWaitBox())
			{
				return;
			}
			NKMPacket_UNIT_REVIEW_COMMENT_LIST_REQ nkmpacket_UNIT_REVIEW_COMMENT_LIST_REQ = new NKMPacket_UNIT_REVIEW_COMMENT_LIST_REQ();
			nkmpacket_UNIT_REVIEW_COMMENT_LIST_REQ.unitID = unitID;
			nkmpacket_UNIT_REVIEW_COMMENT_LIST_REQ.isOrderByVotedCount = bOrderByVotedCount;
			nkmpacket_UNIT_REVIEW_COMMENT_LIST_REQ.pageNumber = pageNum;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_REVIEW_COMMENT_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x001292A8 File Offset: 0x001274A8
		public static void Send_NKMPacket_UNIT_REVIEW_COMMENT_WRITE_REQ(int unitID, string content, bool bRewrite)
		{
			if (NKMPopUpBox.IsOpenedWaitBox())
			{
				return;
			}
			NKMPacket_UNIT_REVIEW_COMMENT_WRITE_REQ nkmpacket_UNIT_REVIEW_COMMENT_WRITE_REQ = new NKMPacket_UNIT_REVIEW_COMMENT_WRITE_REQ();
			nkmpacket_UNIT_REVIEW_COMMENT_WRITE_REQ.unitID = unitID;
			nkmpacket_UNIT_REVIEW_COMMENT_WRITE_REQ.content = content;
			nkmpacket_UNIT_REVIEW_COMMENT_WRITE_REQ.isRewrite = bRewrite;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_REVIEW_COMMENT_WRITE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x001292EC File Offset: 0x001274EC
		public static void Send_NKMPacket_UNIT_REVIEW_COMMENT_DELETE_REQ(int unitID)
		{
			if (NKMPopUpBox.IsOpenedWaitBox())
			{
				return;
			}
			NKMPacket_UNIT_REVIEW_COMMENT_DELETE_REQ nkmpacket_UNIT_REVIEW_COMMENT_DELETE_REQ = new NKMPacket_UNIT_REVIEW_COMMENT_DELETE_REQ();
			nkmpacket_UNIT_REVIEW_COMMENT_DELETE_REQ.unitID = unitID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_REVIEW_COMMENT_DELETE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x00129324 File Offset: 0x00127524
		public static void Send_NKMPacket_UNIT_REVIEW_COMMENT_VOTE_REQ(int unitID, long commentUID)
		{
			if (NKMPopUpBox.IsOpenedWaitBox())
			{
				return;
			}
			NKMPacket_UNIT_REVIEW_COMMENT_VOTE_REQ nkmpacket_UNIT_REVIEW_COMMENT_VOTE_REQ = new NKMPacket_UNIT_REVIEW_COMMENT_VOTE_REQ();
			nkmpacket_UNIT_REVIEW_COMMENT_VOTE_REQ.unitID = unitID;
			nkmpacket_UNIT_REVIEW_COMMENT_VOTE_REQ.commentUID = commentUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_REVIEW_COMMENT_VOTE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x00129360 File Offset: 0x00127560
		public static void Send_NKMPacket_UNIT_REVIEW_COMMENT_VOTE_CANCEL_REQ(int unitID, long commentUID)
		{
			if (NKMPopUpBox.IsOpenedWaitBox())
			{
				return;
			}
			NKMPacket_UNIT_REVIEW_COMMENT_VOTE_CANCEL_REQ nkmpacket_UNIT_REVIEW_COMMENT_VOTE_CANCEL_REQ = new NKMPacket_UNIT_REVIEW_COMMENT_VOTE_CANCEL_REQ();
			nkmpacket_UNIT_REVIEW_COMMENT_VOTE_CANCEL_REQ.unitID = unitID;
			nkmpacket_UNIT_REVIEW_COMMENT_VOTE_CANCEL_REQ.commentUID = commentUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_REVIEW_COMMENT_VOTE_CANCEL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x0012939C File Offset: 0x0012759C
		public static void Send_NKMPacket_UNIT_REVIEW_SCORE_VOTE_REQ(int unitID, int score)
		{
			if (NKMPopUpBox.IsOpenedWaitBox())
			{
				return;
			}
			NKMPacket_UNIT_REVIEW_SCORE_VOTE_REQ nkmpacket_UNIT_REVIEW_SCORE_VOTE_REQ = new NKMPacket_UNIT_REVIEW_SCORE_VOTE_REQ();
			nkmpacket_UNIT_REVIEW_SCORE_VOTE_REQ.unitID = unitID;
			nkmpacket_UNIT_REVIEW_SCORE_VOTE_REQ.score = score;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_REVIEW_SCORE_VOTE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x001293D8 File Offset: 0x001275D8
		public static void Send_NKMPacket_UNIT_REVIEW_USER_BAN_REQ(long userUid)
		{
			if (NKMPopUpBox.IsOpenedWaitBox())
			{
				return;
			}
			NKMPacket_UNIT_REVIEW_USER_BAN_REQ nkmpacket_UNIT_REVIEW_USER_BAN_REQ = new NKMPacket_UNIT_REVIEW_USER_BAN_REQ();
			nkmpacket_UNIT_REVIEW_USER_BAN_REQ.targetUserUid = userUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_REVIEW_USER_BAN_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x00129410 File Offset: 0x00127610
		public static void Send_NKMPacket_UNIT_REVIEW_USER_BAN_CANCEL_REQ(long userUid)
		{
			if (NKMPopUpBox.IsOpenedWaitBox())
			{
				return;
			}
			NKMPacket_UNIT_REVIEW_USER_BAN_CANCEL_REQ nkmpacket_UNIT_REVIEW_USER_BAN_CANCEL_REQ = new NKMPacket_UNIT_REVIEW_USER_BAN_CANCEL_REQ();
			nkmpacket_UNIT_REVIEW_USER_BAN_CANCEL_REQ.targetUserUid = userUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_REVIEW_USER_BAN_CANCEL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x00129448 File Offset: 0x00127648
		public static void Send_NKMPacket_UNIT_REVIEW_USER_BAN_LIST_REQ()
		{
			if (NKMPopUpBox.IsOpenedWaitBox())
			{
				return;
			}
			NKMPacket_UNIT_REVIEW_USER_BAN_LIST_REQ packet = new NKMPacket_UNIT_REVIEW_USER_BAN_LIST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x00129478 File Offset: 0x00127678
		public static void Send_Packet_NKMPacket_LIMIT_BREAK_UNIT_REQ(long targetUnitUID)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMUnitData unitFromUID = myUserData.m_ArmyData.GetUnitFromUID(targetUnitUID);
			List<NKMItemMiscData> list;
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMUnitLimitBreakManager.CanLimitBreak(myUserData, unitFromUID, out list);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK && nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_LIMITBREAK_ALREADY_MAX_LEVEL)
			{
				Debug.LogError("이미 최대치까지 초월한 유닛을 초월 시도");
				return;
			}
			NKMPacket_LIMIT_BREAK_UNIT_REQ nkmpacket_LIMIT_BREAK_UNIT_REQ = new NKMPacket_LIMIT_BREAK_UNIT_REQ();
			nkmpacket_LIMIT_BREAK_UNIT_REQ.unitUID = targetUnitUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_LIMIT_BREAK_UNIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x001294DC File Offset: 0x001276DC
		public static void Send_Packet_NKMPacket_UNIT_SKILL_UPGRADE_REQ(long targetUnitUID, int skillID)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMUnitData unitFromUID = myUserData.m_ArmyData.GetUnitFromUID(targetUnitUID);
			if (unitFromUID == null)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_NO_EXIST_UNIT, null, "");
				return;
			}
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMUnitSkillManager.CanTrainSkill(myUserData, unitFromUID, skillID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
				return;
			}
			NKMPacket_UNIT_SKILL_UPGRADE_REQ nkmpacket_UNIT_SKILL_UPGRADE_REQ = new NKMPacket_UNIT_SKILL_UPGRADE_REQ();
			nkmpacket_UNIT_SKILL_UPGRADE_REQ.unitUID = targetUnitUID;
			nkmpacket_UNIT_SKILL_UPGRADE_REQ.skillID = skillID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_SKILL_UPGRADE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x0012955C File Offset: 0x0012775C
		public static void Send_NKMPacket_SHIP_BUILD_REQ(int shipID)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMShipManager.CanShipBuild(NKCScenManager.GetScenManager().GetMyUserData(), shipID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_SHIP_BUILD_FAIL, NKCPacketHandlers.GetErrorMessage(nkm_ERROR_CODE), null, "");
				return;
			}
			NKMPacket_SHIP_BUILD_REQ nkmpacket_SHIP_BUILD_REQ = new NKMPacket_SHIP_BUILD_REQ();
			nkmpacket_SHIP_BUILD_REQ.shipID = shipID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHIP_BUILD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x001295B4 File Offset: 0x001277B4
		public static void Send_NKMPacket_SHIP_LEVELUP_REQ(long uid, int targetLv = 0)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMUnitData shipFromUID = myUserData.m_ArmyData.GetShipFromUID(uid);
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMShipManager.CanShipLevelup(myUserData, shipFromUID, targetLv);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_SHIP_LEVEL_UP_FAIL, NKCPacketHandlers.GetErrorMessage(nkm_ERROR_CODE), null, "");
				return;
			}
			NKMPacket_SHIP_LEVELUP_REQ nkmpacket_SHIP_LEVELUP_REQ = new NKMPacket_SHIP_LEVELUP_REQ();
			nkmpacket_SHIP_LEVELUP_REQ.shipUID = uid;
			nkmpacket_SHIP_LEVELUP_REQ.nextLevel = targetLv;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHIP_LEVELUP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x00129624 File Offset: 0x00127824
		public static void Send_NKMPacket_SHIP_DIVISION_REQ(List<long> lstShipUID)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			foreach (long shipUid in lstShipUID)
			{
				NKMUnitData shipFromUID = myUserData.m_ArmyData.GetShipFromUID(shipUid);
				NKM_ERROR_CODE nkm_ERROR_CODE = NKMShipManager.CanShipDivision(myUserData, shipFromUID);
				if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_SHIP_DIVISION_FAIL, NKCPacketHandlers.GetErrorMessage(nkm_ERROR_CODE), null, "");
					return;
				}
			}
			NKMPacket_SHIP_DIVISION_REQ nkmpacket_SHIP_DIVISION_REQ = new NKMPacket_SHIP_DIVISION_REQ();
			nkmpacket_SHIP_DIVISION_REQ.removeShipUIDList = lstShipUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHIP_DIVISION_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x001296CC File Offset: 0x001278CC
		public static void Send_NKMPacket_SHIP_UPGRADE_REQ(long ShipUID, int UpgradeShipID)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKMUnitData shipFromUID = myUserData.m_ArmyData.GetShipFromUID(ShipUID);
			NKM_ERROR_CODE nkm_ERROR_CODE = NKMShipManager.CanShipUpgrade(myUserData, shipFromUID, UpgradeShipID);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_SHIP_UPGRADE_FAIL, NKCPacketHandlers.GetErrorMessage(nkm_ERROR_CODE), null, "");
				return;
			}
			NKMPacket_SHIP_UPGRADE_REQ nkmpacket_SHIP_UPGRADE_REQ = new NKMPacket_SHIP_UPGRADE_REQ();
			nkmpacket_SHIP_UPGRADE_REQ.shipUID = ShipUID;
			nkmpacket_SHIP_UPGRADE_REQ.nextShipID = UpgradeShipID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHIP_UPGRADE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x0012973C File Offset: 0x0012793C
		public static void Send_NKMPacket_LIMIT_BREAK_SHIP_REQ(long shipUid, long consumeShipUid)
		{
			NKMPacket_LIMIT_BREAK_SHIP_REQ nkmpacket_LIMIT_BREAK_SHIP_REQ = new NKMPacket_LIMIT_BREAK_SHIP_REQ();
			nkmpacket_LIMIT_BREAK_SHIP_REQ.shipUid = shipUid;
			nkmpacket_LIMIT_BREAK_SHIP_REQ.consumeShipUid = consumeShipUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_LIMIT_BREAK_SHIP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x00129770 File Offset: 0x00127970
		public static void Send_NKMPacket_SHIP_SLOT_FIRST_OPTION_REQ(long shipUid, int moduleId)
		{
			NKMPacket_SHIP_SLOT_FIRST_OPTION_REQ nkmpacket_SHIP_SLOT_FIRST_OPTION_REQ = new NKMPacket_SHIP_SLOT_FIRST_OPTION_REQ();
			nkmpacket_SHIP_SLOT_FIRST_OPTION_REQ.shipUid = shipUid;
			nkmpacket_SHIP_SLOT_FIRST_OPTION_REQ.moduleId = moduleId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHIP_SLOT_FIRST_OPTION_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x001297A4 File Offset: 0x001279A4
		public static void Send_NKMPacket_SHIP_SLOT_LOCK_REQ(long shipUid, int moduleId, int slotId, bool locked)
		{
			NKMPacket_SHIP_SLOT_LOCK_REQ nkmpacket_SHIP_SLOT_LOCK_REQ = new NKMPacket_SHIP_SLOT_LOCK_REQ();
			nkmpacket_SHIP_SLOT_LOCK_REQ.shipUid = shipUid;
			nkmpacket_SHIP_SLOT_LOCK_REQ.moduleId = moduleId;
			nkmpacket_SHIP_SLOT_LOCK_REQ.slotId = slotId;
			nkmpacket_SHIP_SLOT_LOCK_REQ.locked = locked;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHIP_SLOT_LOCK_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x001297E8 File Offset: 0x001279E8
		public static void Send_NKMPacket_SHIP_SLOT_OPTION_CHANGE_REQ(long shipUid, int moduleId)
		{
			NKMPacket_SHIP_SLOT_OPTION_CHANGE_REQ nkmpacket_SHIP_SLOT_OPTION_CHANGE_REQ = new NKMPacket_SHIP_SLOT_OPTION_CHANGE_REQ();
			nkmpacket_SHIP_SLOT_OPTION_CHANGE_REQ.shipUid = shipUid;
			nkmpacket_SHIP_SLOT_OPTION_CHANGE_REQ.moduleId = moduleId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHIP_SLOT_OPTION_CHANGE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x0012981C File Offset: 0x00127A1C
		public static void Send_NKMPacket_SHIP_SLOT_OPTION_CONFIRM_REQ(long shipUid, int moduleId)
		{
			NKMPacket_SHIP_SLOT_OPTION_CONFIRM_REQ nkmpacket_SHIP_SLOT_OPTION_CONFIRM_REQ = new NKMPacket_SHIP_SLOT_OPTION_CONFIRM_REQ();
			nkmpacket_SHIP_SLOT_OPTION_CONFIRM_REQ.shipUid = shipUid;
			nkmpacket_SHIP_SLOT_OPTION_CONFIRM_REQ.moduleId = moduleId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHIP_SLOT_OPTION_CONFIRM_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x00129850 File Offset: 0x00127A50
		public static void Send_NKMPacket_SHIP_SLOT_OPTION_CANCEL_REQ()
		{
			NKMPacket_SHIP_SLOT_OPTION_CANCEL_REQ packet = new NKMPacket_SHIP_SLOT_OPTION_CANCEL_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x00129878 File Offset: 0x00127A78
		public static bool Send_NKMPacket_CONTRACT_REQ(int contractID, ContractCostType costType, int contractCnt)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			ContractTempletV2 contractTempletV = ContractTempletV2.Find(contractID);
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCContractDataMgr.CanTryContract(myUserData, contractTempletV, costType, contractCnt);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_RESOURCE)
				{
					MiscItemUnit[] singleTryRequireItems = contractTempletV.m_SingleTryRequireItems;
					for (int i = 0; i < singleTryRequireItems.Length; i++)
					{
						if (myUserData.m_InventoryData.GetCountMiscItem(singleTryRequireItems[i].ItemId) < singleTryRequireItems[i].Count * (long)contractCnt && i != 0)
						{
							NKCShopManager.OpenItemLackPopup(singleTryRequireItems[i].ItemId, singleTryRequireItems[i].Count32);
							return false;
						}
					}
				}
				NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
				return false;
			}
			Debug.Log(string.Concat(new string[]
			{
				"Send_NKMPacket_CONTRACT_REQ : Contract ID ",
				contractID.ToString(),
				" Cost Type : ",
				costType.ToString(),
				", Contract Count : ",
				contractCnt.ToString()
			}));
			NKMPacket_CONTRACT_REQ packet = new NKMPacket_CONTRACT_REQ
			{
				contractId = contractID,
				count = contractCnt,
				costType = costType
			};
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
			return true;
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x00129990 File Offset: 0x00127B90
		public static bool Send_NKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_REQ(int contractID)
		{
			if (SelectableContractTemplet.Find(contractID) != null)
			{
				Debug.Log("NKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_REQ : Contract ID " + contractID.ToString());
				NKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_REQ packet = new NKMPacket_SELECTABLE_CONTRACT_CHANGE_POOL_REQ
				{
					contractId = contractID
				};
				NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				return true;
			}
			return false;
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x001299E0 File Offset: 0x00127BE0
		public static bool Send_NKMPacket_SELECTABLE_CONTRACT_CONFIRM_REQ(int contractID)
		{
			if (SelectableContractTemplet.Find(contractID) != null)
			{
				Debug.Log("NKMPacket_SELECTABLE_CONTRACT_CONFIRM_REQ : Contract ID " + contractID.ToString());
				NKMPacket_SELECTABLE_CONTRACT_CONFIRM_REQ packet = new NKMPacket_SELECTABLE_CONTRACT_CONFIRM_REQ
				{
					contractId = contractID
				};
				NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				return true;
			}
			return false;
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x00129A30 File Offset: 0x00127C30
		public static void Send_NKMPacket_CONTRACT_STATE_LIST_REQ()
		{
			NKMPacket_CONTRACT_STATE_LIST_REQ packet = new NKMPacket_CONTRACT_STATE_LIST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x00129A58 File Offset: 0x00127C58
		public static void NKMPacket_INSTANT_CONTRACT_LIST_REQ()
		{
			NKMPacket_INSTANT_CONTRACT_LIST_REQ packet = new NKMPacket_INSTANT_CONTRACT_LIST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x00129A80 File Offset: 0x00127C80
		public static bool Send_NKMPacket_NEGOTIATE_REQ2(NKMUnitData unitData, NEGOTIATE_BOSS_SELECTION bossSelection, List<MiscItemData> materials)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCNegotiateManager.CanStartNegotiate(NKCScenManager.CurrentUserData(), unitData, bossSelection, materials);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCPacketHandlers.GetErrorMessage(nkm_ERROR_CODE), null, "");
				return false;
			}
			NKMPacket_NEGOTIATE_REQ nkmpacket_NEGOTIATE_REQ = new NKMPacket_NEGOTIATE_REQ();
			nkmpacket_NEGOTIATE_REQ.unitUid = unitData.m_UnitUID;
			nkmpacket_NEGOTIATE_REQ.negotiateBossSelection = bossSelection;
			nkmpacket_NEGOTIATE_REQ.materials = new List<MiscItemData>();
			for (int i = 0; i < materials.Count; i++)
			{
				if (materials[i].count > 0)
				{
					nkmpacket_NEGOTIATE_REQ.materials.Add(materials[i]);
				}
			}
			if (nkmpacket_NEGOTIATE_REQ.materials.Count == 0)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_NOT_ENOUGH_NEGOTIATE_MATERIALS, null, "");
				return false;
			}
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_NEGOTIATE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
			return true;
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x00129B44 File Offset: 0x00127D44
		public static void Send_NKMPacket_CONTRACT_PERMANENTLY_REQ(long unitUID)
		{
			NKMPacket_CONTRACT_PERMANENTLY_REQ nkmpacket_CONTRACT_PERMANENTLY_REQ = new NKMPacket_CONTRACT_PERMANENTLY_REQ();
			nkmpacket_CONTRACT_PERMANENTLY_REQ.unitUID = unitUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_CONTRACT_PERMANENTLY_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x00129B74 File Offset: 0x00127D74
		public static void Send_NKMPacket_EXCHANGE_PIECE_TO_UNIT_REQ(int itemID, int count)
		{
			NKMPacket_EXCHANGE_PIECE_TO_UNIT_REQ nkmpacket_EXCHANGE_PIECE_TO_UNIT_REQ = new NKMPacket_EXCHANGE_PIECE_TO_UNIT_REQ();
			nkmpacket_EXCHANGE_PIECE_TO_UNIT_REQ.itemId = itemID;
			nkmpacket_EXCHANGE_PIECE_TO_UNIT_REQ.count = count;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EXCHANGE_PIECE_TO_UNIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x00129BA8 File Offset: 0x00127DA8
		public static void Send_NKMPacket_DIVE_SUICIDE_REQ(byte deckIndex)
		{
			NKMPacket_DIVE_SUICIDE_REQ nkmpacket_DIVE_SUICIDE_REQ = new NKMPacket_DIVE_SUICIDE_REQ();
			nkmpacket_DIVE_SUICIDE_REQ.selectDeckIndex = deckIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DIVE_SUICIDE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x00129BD8 File Offset: 0x00127DD8
		public static void Send_NKMPacket_DIVE_START_REQ(int cityID, int stageID, List<int> lstDiveDeckIndex, bool bJump)
		{
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCDiveManager.CanStart(cityID, stageID, lstDiveDeckIndex, NKCScenManager.GetScenManager().GetMyUserData(), NKCSynchronizedTime.GetServerUTCTime(0.0), bJump);
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_OK)
			{
				NKMPacket_DIVE_START_REQ nkmpacket_DIVE_START_REQ = new NKMPacket_DIVE_START_REQ();
				nkmpacket_DIVE_START_REQ.stageID = stageID;
				nkmpacket_DIVE_START_REQ.cityID = cityID;
				nkmpacket_DIVE_START_REQ.deckIndexeList = lstDiveDeckIndex;
				nkmpacket_DIVE_START_REQ.isDiveStorm = bJump;
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DIVE_START_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				return;
			}
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_INSUFFICIENT_ITEM)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_DIVE_NO_EXIST_COST, null, "");
				return;
			}
			if (nkm_ERROR_CODE == NKM_ERROR_CODE.NEC_FAIL_DIVE_NOT_ENOUGH_SQUAD_COUNT)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_DIVE_NO_ENOUGH_DECK, null, "");
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCStringTable.GetString(nkm_ERROR_CODE.ToString(), false), null, "");
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x00129C9C File Offset: 0x00127E9C
		public static void Send_NKMPacket_DIVE_SELECT_ARTIFACT_REQ(int artifactID)
		{
			NKMPacket_DIVE_SELECT_ARTIFACT_REQ nkmpacket_DIVE_SELECT_ARTIFACT_REQ = new NKMPacket_DIVE_SELECT_ARTIFACT_REQ();
			nkmpacket_DIVE_SELECT_ARTIFACT_REQ.artifactID = artifactID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DIVE_SELECT_ARTIFACT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x00129CCC File Offset: 0x00127ECC
		public static void Send_NKMPacket_DIVE_SKIP_REQ(int stageId, int skipCount)
		{
			NKMPacket_DIVE_SKIP_REQ nkmpacket_DIVE_SKIP_REQ = new NKMPacket_DIVE_SKIP_REQ();
			nkmpacket_DIVE_SKIP_REQ.stageId = stageId;
			nkmpacket_DIVE_SKIP_REQ.skipCount = skipCount;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DIVE_SKIP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x00129D00 File Offset: 0x00127F00
		public static void Send_NKMPacket_RAID_RESULT_LIST_REQ()
		{
			NKMPacket_RAID_RESULT_LIST_REQ packet = new NKMPacket_RAID_RESULT_LIST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x00129D28 File Offset: 0x00127F28
		public static void Send_NKMPacket_RAID_COOP_LIST_REQ()
		{
			NKMPacket_RAID_COOP_LIST_REQ packet = new NKMPacket_RAID_COOP_LIST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x00129D50 File Offset: 0x00127F50
		public static void Send_NKMPacket_RAID_SET_COOP_REQ(long raidUID)
		{
			NKMPacket_RAID_SET_COOP_REQ nkmpacket_RAID_SET_COOP_REQ = new NKMPacket_RAID_SET_COOP_REQ();
			nkmpacket_RAID_SET_COOP_REQ.raidUID = raidUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_RAID_SET_COOP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x00129D80 File Offset: 0x00127F80
		public static void Send_NKMPacket_RAID_RESULT_ACCEPT_REQ(long raidUID)
		{
			NKMPacket_RAID_RESULT_ACCEPT_REQ nkmpacket_RAID_RESULT_ACCEPT_REQ = new NKMPacket_RAID_RESULT_ACCEPT_REQ();
			nkmpacket_RAID_RESULT_ACCEPT_REQ.raidUID = raidUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_RAID_RESULT_ACCEPT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x00129DAD File Offset: 0x00127FAD
		public static void Send_NKMPacket_RAID_RESULT_ACCEPT_ALL_REQ()
		{
			NKCScenManager.GetScenManager().GetConnectGame().Send(new NKMPacket_RAID_RESULT_ACCEPT_ALL_REQ(), NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x00129DC8 File Offset: 0x00127FC8
		public static void Send_NKMPacket_MY_RAID_LIST_REQ()
		{
			NKMPacket_MY_RAID_LIST_REQ packet = new NKMPacket_MY_RAID_LIST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x00129DF0 File Offset: 0x00127FF0
		public static void Send_NKMPacket_RAID_DETAIL_INFO_REQ(long raidUID)
		{
			NKMPacket_RAID_DETAIL_INFO_REQ nkmpacket_RAID_DETAIL_INFO_REQ = new NKMPacket_RAID_DETAIL_INFO_REQ();
			nkmpacket_RAID_DETAIL_INFO_REQ.raidUID = raidUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_RAID_DETAIL_INFO_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x00129E20 File Offset: 0x00128020
		public static void Send_NKMPacket_RAID_GAME_LOAD_REQ(long raidUID, byte selectDeckIndex, List<int> _Buffs, bool isTryAssist)
		{
			NKMPacket_RAID_GAME_LOAD_REQ nkmpacket_RAID_GAME_LOAD_REQ = new NKMPacket_RAID_GAME_LOAD_REQ();
			nkmpacket_RAID_GAME_LOAD_REQ.raidUID = raidUID;
			nkmpacket_RAID_GAME_LOAD_REQ.selectDeckIndex = selectDeckIndex;
			nkmpacket_RAID_GAME_LOAD_REQ.buffList = _Buffs;
			nkmpacket_RAID_GAME_LOAD_REQ.isTryAssist = isTryAssist;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_RAID_GAME_LOAD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x00129E64 File Offset: 0x00128064
		public static void Send_NKMPacket_RAID_POINT_REWARD_REQ(int raidPointReward)
		{
			NKMPacket_RAID_POINT_REWARD_REQ nkmpacket_RAID_POINT_REWARD_REQ = new NKMPacket_RAID_POINT_REWARD_REQ();
			nkmpacket_RAID_POINT_REWARD_REQ.raidPointReward = raidPointReward;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_RAID_POINT_REWARD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x00129E94 File Offset: 0x00128094
		public static void Send_NKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_REQ(int cityID)
		{
			NKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_REQ nkmpacket_WORLDMAP_REMOVE_EVENT_DUNGEON_REQ = new NKMPacket_WORLDMAP_REMOVE_EVENT_DUNGEON_REQ();
			nkmpacket_WORLDMAP_REMOVE_EVENT_DUNGEON_REQ.cityID = cityID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WORLDMAP_REMOVE_EVENT_DUNGEON_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x00129EC4 File Offset: 0x001280C4
		public static void Send_NKMPacket_WORLDMAP_EVENT_CANCEL_REQ(int cityID)
		{
			NKMPacket_WORLDMAP_EVENT_CANCEL_REQ nkmpacket_WORLDMAP_EVENT_CANCEL_REQ = new NKMPacket_WORLDMAP_EVENT_CANCEL_REQ();
			nkmpacket_WORLDMAP_EVENT_CANCEL_REQ.cityID = cityID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WORLDMAP_EVENT_CANCEL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x00129EF4 File Offset: 0x001280F4
		public static void Send_NKMPacket_MY_USER_PROFILE_INFO_REQ()
		{
			NKMPacket_MY_USER_PROFILE_INFO_REQ packet = new NKMPacket_MY_USER_PROFILE_INFO_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x00129F1C File Offset: 0x0012811C
		public static void Send_NKMPacket_USER_PROFILE_INFO_REQ(long userUID, NKM_DECK_TYPE requestDeckType)
		{
			NKMPacket_USER_PROFILE_INFO_REQ nkmpacket_USER_PROFILE_INFO_REQ = new NKMPacket_USER_PROFILE_INFO_REQ();
			nkmpacket_USER_PROFILE_INFO_REQ.userUID = userUID;
			nkmpacket_USER_PROFILE_INFO_REQ.deckType = requestDeckType;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_USER_PROFILE_INFO_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x00129F50 File Offset: 0x00128150
		public static void Send_NKMPacket_UPDATE_DEFENCE_DECK_REQ(NKMDeckData deckData)
		{
			NKMPacket_UPDATE_DEFENCE_DECK_REQ nkmpacket_UPDATE_DEFENCE_DECK_REQ = new NKMPacket_UPDATE_DEFENCE_DECK_REQ();
			nkmpacket_UPDATE_DEFENCE_DECK_REQ.deckData = deckData;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UPDATE_DEFENCE_DECK_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x00129F80 File Offset: 0x00128180
		public static void Send_NKMPacket_ASYNC_PVP_START_GAME_REQ(long friendCode, byte deckIndex, NKM_GAME_TYPE gameType)
		{
			NKMPacket_ASYNC_PVP_START_GAME_REQ nkmpacket_ASYNC_PVP_START_GAME_REQ = new NKMPacket_ASYNC_PVP_START_GAME_REQ();
			nkmpacket_ASYNC_PVP_START_GAME_REQ.targetFriendCode = friendCode;
			nkmpacket_ASYNC_PVP_START_GAME_REQ.selectDeckIndex = deckIndex;
			nkmpacket_ASYNC_PVP_START_GAME_REQ.gameType = gameType;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_ASYNC_PVP_START_GAME_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x00129FBC File Offset: 0x001281BC
		public static void Send_NKMPacket_LEAGUE_PVP_WEEKLY_RANKER_REQ()
		{
			NKMPacket_LEAGUE_PVP_WEEKLY_RANKER_REQ packet = new NKMPacket_LEAGUE_PVP_WEEKLY_RANKER_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003989 RID: 14729 RVA: 0x00129FE4 File Offset: 0x001281E4
		public static void Send_NKMPacket_FRIEND_LIST_REQ(NKM_FRIEND_LIST_TYPE listType)
		{
			NKMPacket_FRIEND_LIST_REQ nkmpacket_FRIEND_LIST_REQ = new NKMPacket_FRIEND_LIST_REQ();
			nkmpacket_FRIEND_LIST_REQ.friendListType = listType;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600398A RID: 14730 RVA: 0x0012A014 File Offset: 0x00128214
		public static void Send_NKMPacket_FRIEND_REQUEST_REQ(long friendCode)
		{
			NKMPacket_FRIEND_REQUEST_REQ nkmpacket_FRIEND_REQUEST_REQ = new NKMPacket_FRIEND_REQUEST_REQ();
			nkmpacket_FRIEND_REQUEST_REQ.friendCode = friendCode;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FRIEND_REQUEST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x0012A044 File Offset: 0x00128244
		public static void Send_NKMPacket_USER_PROFILE_BY_FRIEND_CODE_REQ(long friendCode)
		{
			NKMPacket_USER_PROFILE_BY_FRIEND_CODE_REQ nkmpacket_USER_PROFILE_BY_FRIEND_CODE_REQ = new NKMPacket_USER_PROFILE_BY_FRIEND_CODE_REQ();
			nkmpacket_USER_PROFILE_BY_FRIEND_CODE_REQ.friendCode = friendCode;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_USER_PROFILE_BY_FRIEND_CODE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x0012A074 File Offset: 0x00128274
		public static void Send_NKMPacket_PVP_CHARGE_POINT_REFRESH_REQ(int itemID)
		{
			NKMPacket_PVP_CHARGE_POINT_REFRESH_REQ nkmpacket_PVP_CHARGE_POINT_REFRESH_REQ = new NKMPacket_PVP_CHARGE_POINT_REFRESH_REQ();
			nkmpacket_PVP_CHARGE_POINT_REFRESH_REQ.itemId = itemID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PVP_CHARGE_POINT_REFRESH_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x0012A0A4 File Offset: 0x001282A4
		public static void Send_NKMPacket_SET_EMBLEM_REQ(int index, int itemID)
		{
			NKMPacket_SET_EMBLEM_REQ nkmpacket_SET_EMBLEM_REQ = new NKMPacket_SET_EMBLEM_REQ();
			nkmpacket_SET_EMBLEM_REQ.index = (sbyte)index;
			nkmpacket_SET_EMBLEM_REQ.itemId = itemID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SET_EMBLEM_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x0012A0DC File Offset: 0x001282DC
		public static void Send_NKMPacket_USER_PROFILE_CHANGE_FRAME_REQ(int frameID)
		{
			NKMPacket_USER_PROFILE_CHANGE_FRAME_REQ nkmpacket_USER_PROFILE_CHANGE_FRAME_REQ = new NKMPacket_USER_PROFILE_CHANGE_FRAME_REQ();
			nkmpacket_USER_PROFILE_CHANGE_FRAME_REQ.selfiFrameId = frameID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_USER_PROFILE_CHANGE_FRAME_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600398F RID: 14735 RVA: 0x0012A10C File Offset: 0x0012830C
		public static void Send_NKMPacket_GAME_EMOTICON_REQ(int emoticonID)
		{
			NKMPacket_GAME_EMOTICON_REQ nkmpacket_GAME_EMOTICON_REQ = new NKMPacket_GAME_EMOTICON_REQ();
			nkmpacket_GAME_EMOTICON_REQ.emoticonID = emoticonID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_EMOTICON_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x0012A13C File Offset: 0x0012833C
		public static void Send_NKMPacket_EMOTICON_DATA_REQ(NKC_OPEN_WAIT_BOX_TYPE eType = NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL)
		{
			NKMPacket_EMOTICON_DATA_REQ packet = new NKMPacket_EMOTICON_DATA_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, eType, true);
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x0012A164 File Offset: 0x00128364
		public static void Send_NKMPacket_EMOTICON_ANI_CHANGE_REQ(int presetIndex, int emoticonID)
		{
			NKMPacket_EMOTICON_ANI_CHANGE_REQ nkmpacket_EMOTICON_ANI_CHANGE_REQ = new NKMPacket_EMOTICON_ANI_CHANGE_REQ();
			nkmpacket_EMOTICON_ANI_CHANGE_REQ.presetIndex = presetIndex;
			nkmpacket_EMOTICON_ANI_CHANGE_REQ.emoticonId = emoticonID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EMOTICON_ANI_CHANGE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x0012A198 File Offset: 0x00128398
		public static void Send_NKMPacket_EMOTICON_TEXT_CHANGE_REQ(int presetIndex, int emoticonID)
		{
			NKMPacket_EMOTICON_TEXT_CHANGE_REQ nkmpacket_EMOTICON_TEXT_CHANGE_REQ = new NKMPacket_EMOTICON_TEXT_CHANGE_REQ();
			nkmpacket_EMOTICON_TEXT_CHANGE_REQ.presetIndex = presetIndex;
			nkmpacket_EMOTICON_TEXT_CHANGE_REQ.emoticonId = emoticonID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EMOTICON_TEXT_CHANGE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003993 RID: 14739 RVA: 0x0012A1CC File Offset: 0x001283CC
		public static void Send_NKMPacket_SHOP_FIXED_LIST_REQ(NKC_OPEN_WAIT_BOX_TYPE waitboxType)
		{
			NKMPacket_SHOP_FIXED_LIST_REQ packet = new NKMPacket_SHOP_FIXED_LIST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, waitboxType, true);
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x0012A1F4 File Offset: 0x001283F4
		public static void Send_NKMPacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ(string productMarketID, List<int> lstSelection = null)
		{
			NKMPacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ nkmpacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ = new NKMPacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ();
			nkmpacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ.productMarketID = productMarketID;
			nkmpacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ.selectIndices = ((lstSelection != null) ? lstSelection : new List<int>());
			if (NKCDefineManager.DEFINE_ONESTORE())
			{
				NKCPacketHandlersLobby.OnRecv(new NKMPacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_ACK
				{
					errorCode = NKM_ERROR_CODE.NEC_OK,
					productMarketID = productMarketID,
					selectIndices = nkmpacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ.selectIndices
				});
				return;
			}
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x0012A260 File Offset: 0x00128460
		public static void Send_NKMPacket_SHOP_FIX_SHOP_BUY_REQ(int productID, int productCount, List<int> lstSelection = null)
		{
			ShopItemTemplet shop_templet = ShopItemTemplet.Find(productID);
			bool flag;
			long num;
			NKM_ERROR_CODE nkm_ERROR_CODE = NKCShopManager.CanBuyFixShop(NKCScenManager.CurrentUserData(), shop_templet, out flag, out num, true);
			if (nkm_ERROR_CODE != NKM_ERROR_CODE.NEC_OK)
			{
				NKCPopupOKCancel.OpenOKBox(nkm_ERROR_CODE, null, "");
				return;
			}
			NKMPacket_SHOP_FIX_SHOP_BUY_REQ nkmpacket_SHOP_FIX_SHOP_BUY_REQ = new NKMPacket_SHOP_FIX_SHOP_BUY_REQ();
			nkmpacket_SHOP_FIX_SHOP_BUY_REQ.productID = productID;
			nkmpacket_SHOP_FIX_SHOP_BUY_REQ.productCount = productCount;
			nkmpacket_SHOP_FIX_SHOP_BUY_REQ.selectIndices = ((lstSelection != null) ? lstSelection : new List<int>());
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHOP_FIX_SHOP_BUY_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x0012A2D4 File Offset: 0x001284D4
		public static void Send_NKMPacket_SHOP_FIX_SHOP_CASH_BUY_REQ(string productMarketID, string validationToken, double realCash, string currencyCode, List<int> lstSelection = null)
		{
			Debug.Log("[InappPurchase] NKMPacket_SHOP_FIX_SHOP_CASH_BUY_REQ");
			NKCCurrencyTemplet nkccurrencyTemplet = NKMTempletContainer<NKCCurrencyTemplet>.Find(currencyCode);
			if (nkccurrencyTemplet == null)
			{
				Debug.Log("[InappPurchase] CurrentyTemplet is null");
			}
			NKMPacket_SHOP_FIX_SHOP_CASH_BUY_REQ nkmpacket_SHOP_FIX_SHOP_CASH_BUY_REQ = new NKMPacket_SHOP_FIX_SHOP_CASH_BUY_REQ();
			nkmpacket_SHOP_FIX_SHOP_CASH_BUY_REQ.productMarketID = productMarketID;
			nkmpacket_SHOP_FIX_SHOP_CASH_BUY_REQ.validationToken = validationToken;
			nkmpacket_SHOP_FIX_SHOP_CASH_BUY_REQ.currencyCode = currencyCode;
			nkmpacket_SHOP_FIX_SHOP_CASH_BUY_REQ.currencyType = ((nkccurrencyTemplet == null) ? 0 : nkccurrencyTemplet.m_Type);
			nkmpacket_SHOP_FIX_SHOP_CASH_BUY_REQ.realCash = realCash;
			nkmpacket_SHOP_FIX_SHOP_CASH_BUY_REQ.selectIndices = ((lstSelection != null) ? lstSelection : new List<int>());
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHOP_FIX_SHOP_CASH_BUY_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x0012A359 File Offset: 0x00128559
		public static void Send_NKMPacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ(string productMarketID)
		{
			NKCPacketSender.Send_NKMPacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ(productMarketID);
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x0012A364 File Offset: 0x00128564
		public static void Send_NKMPacket_GAMEBASE_BUY_REQ(string paymentSeq, string accessToken, string paymentId, List<int> selectIndices)
		{
			NKMPacket_GAMEBASE_BUY_REQ packet = new NKMPacket_GAMEBASE_BUY_REQ
			{
				paymentSeq = paymentSeq,
				accessToken = accessToken,
				selectIndices = selectIndices,
				paymentId = paymentId
			};
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x0012A3A8 File Offset: 0x001285A8
		public static void Send_NKMPacket_RANDOM_MISSION_CHANGE_REQ(int tabId, int missionId)
		{
			NKMPacket_RANDOM_MISSION_CHANGE_REQ nkmpacket_RANDOM_MISSION_CHANGE_REQ = new NKMPacket_RANDOM_MISSION_CHANGE_REQ();
			nkmpacket_RANDOM_MISSION_CHANGE_REQ.tabId = tabId;
			nkmpacket_RANDOM_MISSION_CHANGE_REQ.missionId = missionId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_RANDOM_MISSION_CHANGE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x0012A3DC File Offset: 0x001285DC
		public static void Send_NKMPacket_MISSION_GIVE_ITEM_REQ(int missionID, int itemCount)
		{
			NKMPacket_MISSION_GIVE_ITEM_REQ nkmpacket_MISSION_GIVE_ITEM_REQ = new NKMPacket_MISSION_GIVE_ITEM_REQ();
			nkmpacket_MISSION_GIVE_ITEM_REQ.missionId = missionID;
			nkmpacket_MISSION_GIVE_ITEM_REQ.count = itemCount;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_MISSION_GIVE_ITEM_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x0012A410 File Offset: 0x00128610
		public static void Send_NKMPacket_MISSION_COMPLETE_REQ(int missionTabID, int groupID, int missionID)
		{
			NKMPacket_MISSION_COMPLETE_REQ nkmpacket_MISSION_COMPLETE_REQ = new NKMPacket_MISSION_COMPLETE_REQ();
			nkmpacket_MISSION_COMPLETE_REQ.tabId = missionTabID;
			nkmpacket_MISSION_COMPLETE_REQ.groupId = groupID;
			nkmpacket_MISSION_COMPLETE_REQ.missionID = missionID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_MISSION_COMPLETE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x0012A44C File Offset: 0x0012864C
		public static void Send_NKMPacket_MISSION_COMPLETE_ALL_REQ(int missionTabID)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			if (myUserData == null)
			{
				return;
			}
			NKMUserMissionData missionData = NKCScenManager.GetScenManager().GetMyUserData().m_MissionData;
			if (missionData == null)
			{
				return;
			}
			if (!missionData.CheckCompletableMission(myUserData, missionTabID, false))
			{
				return;
			}
			NKMPacket_MISSION_COMPLETE_ALL_REQ nkmpacket_MISSION_COMPLETE_ALL_REQ = new NKMPacket_MISSION_COMPLETE_ALL_REQ();
			nkmpacket_MISSION_COMPLETE_ALL_REQ.tabId = missionTabID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_MISSION_COMPLETE_ALL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x0600399D RID: 14749 RVA: 0x0012A4A8 File Offset: 0x001286A8
		public static void Send_NKMPacket_GAME_OPTION_CHANGE_REQ(bool bActionCamera, bool bTrackCamera, bool bViewSkillCutIn, NKM_GAME_AUTO_RESPAWN_TYPE ePvpAutoRespawn, bool bAutoSyncFriendDeck, bool bLocal = false)
		{
			NKMPacket_GAME_OPTION_CHANGE_REQ nkmpacket_GAME_OPTION_CHANGE_REQ = new NKMPacket_GAME_OPTION_CHANGE_REQ();
			nkmpacket_GAME_OPTION_CHANGE_REQ.isActionCamera = bActionCamera;
			nkmpacket_GAME_OPTION_CHANGE_REQ.isTrackCamera = bTrackCamera;
			nkmpacket_GAME_OPTION_CHANGE_REQ.isViewSkillCutIn = bViewSkillCutIn;
			nkmpacket_GAME_OPTION_CHANGE_REQ.defaultPvpAutoRespawn = ePvpAutoRespawn;
			nkmpacket_GAME_OPTION_CHANGE_REQ.autoSyncFriendDeck = bAutoSyncFriendDeck;
			if (!bLocal)
			{
				NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GAME_OPTION_CHANGE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
				return;
			}
			NKCLocalPacketHandler.SendPacketToLocalServer(nkmpacket_GAME_OPTION_CHANGE_REQ);
			NKMPopUpBox.OpenWaitBox(0f, "");
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x0012A50C File Offset: 0x0012870C
		public static void Send_NKMPacket_INVENTORY_EXPAND_REQ(NKM_INVENTORY_EXPAND_TYPE inventoryType, int count)
		{
			NKMPacket_INVENTORY_EXPAND_REQ nkmpacket_INVENTORY_EXPAND_REQ = new NKMPacket_INVENTORY_EXPAND_REQ();
			nkmpacket_INVENTORY_EXPAND_REQ.inventoryExpandType = inventoryType;
			nkmpacket_INVENTORY_EXPAND_REQ.count = count;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_INVENTORY_EXPAND_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x0012A540 File Offset: 0x00128740
		public static void Send_NKMPacket_REMOVE_EQUIP_ITEM_REQ(List<long> listEquipSlot)
		{
			if (listEquipSlot == null || listEquipSlot.Count <= 0)
			{
				return;
			}
			NKMPacket_REMOVE_EQUIP_ITEM_REQ nkmpacket_REMOVE_EQUIP_ITEM_REQ = new NKMPacket_REMOVE_EQUIP_ITEM_REQ();
			nkmpacket_REMOVE_EQUIP_ITEM_REQ.removeEquipItemUIDList = new List<long>(listEquipSlot);
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_REMOVE_EQUIP_ITEM_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x0012A580 File Offset: 0x00128780
		public static void Send_NKMPacket_LOCK_ITEM_REQ(long targetItemUID, bool bLock)
		{
			NKMPacket_LOCK_EQUIP_ITEM_REQ nkmpacket_LOCK_EQUIP_ITEM_REQ = new NKMPacket_LOCK_EQUIP_ITEM_REQ();
			nkmpacket_LOCK_EQUIP_ITEM_REQ.equipItemUID = targetItemUID;
			nkmpacket_LOCK_EQUIP_ITEM_REQ.isLock = bLock;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_LOCK_EQUIP_ITEM_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x0012A5B4 File Offset: 0x001287B4
		public static void Send_NKMPacket_EQUIP_ITEM_EQUIP_REQ(bool bEquip, long unitUID, long equipUID, ITEM_EQUIP_POSITION equipPosition)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				Debug.Log("유저 정보를 찾을 수 없습니다.");
				return;
			}
			if (nkmuserData.m_InventoryData.GetItemEquip(equipUID) == null)
			{
				Debug.Log(string.Format("해당 아이템 정보를 찾을 수 없습니다. : {0}", equipUID));
				return;
			}
			if (nkmuserData.m_ArmyData.GetUnitOrShipFromUID(unitUID) == null)
			{
				Debug.Log(string.Format("해당 유닛 정보를 찾을 수 없습니다. : {0}", unitUID));
				return;
			}
			NKMPacket_EQUIP_ITEM_EQUIP_REQ nkmpacket_EQUIP_ITEM_EQUIP_REQ = new NKMPacket_EQUIP_ITEM_EQUIP_REQ();
			nkmpacket_EQUIP_ITEM_EQUIP_REQ.isEquip = bEquip;
			nkmpacket_EQUIP_ITEM_EQUIP_REQ.unitUID = unitUID;
			nkmpacket_EQUIP_ITEM_EQUIP_REQ.equipItemUID = equipUID;
			nkmpacket_EQUIP_ITEM_EQUIP_REQ.equipPosition = equipPosition;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_ITEM_EQUIP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x0012A654 File Offset: 0x00128854
		public static void Send_NKMPacket_UI_SCEN_CHANGED_REQ(NKM_SCEN_ID scenID)
		{
			NKMPacket_UI_SCEN_CHANGED_REQ nkmpacket_UI_SCEN_CHANGED_REQ = new NKMPacket_UI_SCEN_CHANGED_REQ();
			nkmpacket_UI_SCEN_CHANGED_REQ.scenID = scenID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UI_SCEN_CHANGED_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x0012A684 File Offset: 0x00128884
		public static void Send_NKMPacket_CUTSCENE_DUNGEON_START_REQ(int cutsceneDungeonID)
		{
			NKMPacket_CUTSCENE_DUNGEON_START_REQ nkmpacket_CUTSCENE_DUNGEON_START_REQ = new NKMPacket_CUTSCENE_DUNGEON_START_REQ();
			nkmpacket_CUTSCENE_DUNGEON_START_REQ.dungeonID = cutsceneDungeonID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_CUTSCENE_DUNGEON_START_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x0012A6B4 File Offset: 0x001288B4
		public static void Send_NKMPacket_NEXON_NGS_DATA_NOT(byte[] buffer)
		{
			if (NKCScenManager.GetScenManager() == null || NKCScenManager.GetScenManager().GetConnectGame() == null || !NKCScenManager.GetScenManager().GetConnectGame().IsConnected)
			{
				Debug.Log("NKMPacket_NEXON_NGS_DATA_NOT, skip because not connected");
				return;
			}
			NKMPacket_NEXON_NGS_DATA_NOT nkmpacket_NEXON_NGS_DATA_NOT = new NKMPacket_NEXON_NGS_DATA_NOT();
			nkmpacket_NEXON_NGS_DATA_NOT.buffer = buffer;
			Debug.Log("NKMPacket_NEXON_NGS_DATA_NOT, buff Size : " + buffer.Length.ToString());
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_NEXON_NGS_DATA_NOT, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x0012A730 File Offset: 0x00128930
		public static void Send_NKMPacket_WARFARE_GAME_START_REQ(NKMPacket_WARFARE_GAME_START_REQ req)
		{
			NKCScenManager.GetScenManager().GetConnectGame().Send(req, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x0012A748 File Offset: 0x00128948
		public static void Send_NKMPacket_WARFARE_GAME_MOVE_REQ(byte fromTileIndex, byte toTileIndex)
		{
			NKMPacket_WARFARE_GAME_MOVE_REQ nkmpacket_WARFARE_GAME_MOVE_REQ = new NKMPacket_WARFARE_GAME_MOVE_REQ();
			nkmpacket_WARFARE_GAME_MOVE_REQ.tileIndexFrom = fromTileIndex;
			nkmpacket_WARFARE_GAME_MOVE_REQ.tileIndexTo = toTileIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WARFARE_GAME_MOVE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x0012A77C File Offset: 0x0012897C
		public static void Send_NKMPacket_WARFARE_GAME_USE_SERVICE_REQ(int warfareGameUnitUID, NKM_WARFARE_SERVICE_TYPE serviceType)
		{
			NKMPacket_WARFARE_GAME_USE_SERVICE_REQ nkmpacket_WARFARE_GAME_USE_SERVICE_REQ = new NKMPacket_WARFARE_GAME_USE_SERVICE_REQ();
			nkmpacket_WARFARE_GAME_USE_SERVICE_REQ.warfareGameUnitUID = warfareGameUnitUID;
			nkmpacket_WARFARE_GAME_USE_SERVICE_REQ.serviceType = serviceType;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WARFARE_GAME_USE_SERVICE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x0012A7B0 File Offset: 0x001289B0
		public static void Send_NKMPacket_WARFARE_GAME_NEXT_ORDER_REQ()
		{
			Debug.Log("NKMPacket_WARFARE_GAME_NEXT_ORDER_REQ - CurrentScenID : " + NKCScenManager.GetScenManager().GetNowScenID().ToString());
			NKMPacket_WARFARE_GAME_NEXT_ORDER_REQ packet = new NKMPacket_WARFARE_GAME_NEXT_ORDER_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x0012A800 File Offset: 0x00128A00
		public static void Send_NKMPacket_WARFARE_GAME_GIVE_UP_REQ()
		{
			NKMPacket_WARFARE_GAME_GIVE_UP_REQ packet = new NKMPacket_WARFARE_GAME_GIVE_UP_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x0012A828 File Offset: 0x00128A28
		public static void Send_NKMPacket_WARFARE_GAME_AUTO_REQ(bool bAuto)
		{
			NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
			NKCPacketSender.Send_NKMPacket_WARFARE_GAME_AUTO_REQ(bAuto, myUserData.m_UserOption.m_bAutoWarfareRepair);
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x0012A854 File Offset: 0x00128A54
		public static void Send_NKMPacket_WARFARE_GAME_AUTO_REQ(bool bAuto, bool bRepair)
		{
			NKMPacket_WARFARE_GAME_AUTO_REQ nkmpacket_WARFARE_GAME_AUTO_REQ = new NKMPacket_WARFARE_GAME_AUTO_REQ();
			nkmpacket_WARFARE_GAME_AUTO_REQ.isAuto = bAuto;
			nkmpacket_WARFARE_GAME_AUTO_REQ.isAutoRepair = bRepair;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WARFARE_GAME_AUTO_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x0012A888 File Offset: 0x00128A88
		public static void Send_NKMPacket_WARFARE_GAME_TURN_FINISH_REQ()
		{
			NKMPacket_WARFARE_GAME_TURN_FINISH_REQ packet = new NKMPacket_WARFARE_GAME_TURN_FINISH_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x0012A8B0 File Offset: 0x00128AB0
		public static void Send_NKMPacket_WARFARE_FRIEND_LIST_REQ()
		{
			NKMPacket_WARFARE_FRIEND_LIST_REQ packet = new NKMPacket_WARFARE_FRIEND_LIST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x0012A8D8 File Offset: 0x00128AD8
		public static void Send_NKMPacket_WARFARE_RECOVER_REQ(byte deckIndex, short tileIndex)
		{
			NKMPacket_WARFARE_RECOVER_REQ nkmpacket_WARFARE_RECOVER_REQ = new NKMPacket_WARFARE_RECOVER_REQ();
			nkmpacket_WARFARE_RECOVER_REQ.deckIndex = deckIndex;
			nkmpacket_WARFARE_RECOVER_REQ.tileIndex = tileIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WARFARE_RECOVER_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x0012A90C File Offset: 0x00128B0C
		public static void Send_NKMPacket_STAGE_UNLOCK_REQ(int stageID)
		{
			NKMPacket_STAGE_UNLOCK_REQ nkmpacket_STAGE_UNLOCK_REQ = new NKMPacket_STAGE_UNLOCK_REQ();
			nkmpacket_STAGE_UNLOCK_REQ.stageId = stageID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_STAGE_UNLOCK_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x0012A93C File Offset: 0x00128B3C
		public static void Send_NKMPacket_FAVORITES_STAGE_REQ()
		{
			NKMPacket_FAVORITES_STAGE_REQ packet = new NKMPacket_FAVORITES_STAGE_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x0012A964 File Offset: 0x00128B64
		public static void Send_NKMPacket_FAVORITES_STAGE_ADD_REQ(int stageID)
		{
			NKMPacket_FAVORITES_STAGE_ADD_REQ nkmpacket_FAVORITES_STAGE_ADD_REQ = new NKMPacket_FAVORITES_STAGE_ADD_REQ();
			nkmpacket_FAVORITES_STAGE_ADD_REQ.stageId = stageID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FAVORITES_STAGE_ADD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x0012A994 File Offset: 0x00128B94
		public static void Send_NKMPacket_FAVORITES_STAGE_DELETE_REQ(int stageID)
		{
			NKMPacket_FAVORITES_STAGE_DELETE_REQ nkmpacket_FAVORITES_STAGE_DELETE_REQ = new NKMPacket_FAVORITES_STAGE_DELETE_REQ();
			nkmpacket_FAVORITES_STAGE_DELETE_REQ.stageId = stageID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FAVORITES_STAGE_DELETE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x0012A9C4 File Offset: 0x00128BC4
		public static void Send_NKMPacket_TEAM_COLLECTION_REWARD_REQ(int teamID)
		{
			NKMPacket_TEAM_COLLECTION_REWARD_REQ nkmpacket_TEAM_COLLECTION_REWARD_REQ = new NKMPacket_TEAM_COLLECTION_REWARD_REQ();
			nkmpacket_TEAM_COLLECTION_REWARD_REQ.teamID = teamID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_TEAM_COLLECTION_REWARD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x0012A9F4 File Offset: 0x00128BF4
		public static void Send_NKMPacket_UNIT_REVIEW_TAG_LIST_REQ(int unitID)
		{
			NKMPacket_UNIT_REVIEW_TAG_LIST_REQ nkmpacket_UNIT_REVIEW_TAG_LIST_REQ = new NKMPacket_UNIT_REVIEW_TAG_LIST_REQ();
			nkmpacket_UNIT_REVIEW_TAG_LIST_REQ.unitID = unitID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_REVIEW_TAG_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x0012AA24 File Offset: 0x00128C24
		public static void Send_NKMPacket_UNIT_REVIEW_TAG_VOTE_REQ(int unitID, short tagType)
		{
			NKMPacket_UNIT_REVIEW_TAG_VOTE_REQ nkmpacket_UNIT_REVIEW_TAG_VOTE_REQ = new NKMPacket_UNIT_REVIEW_TAG_VOTE_REQ();
			nkmpacket_UNIT_REVIEW_TAG_VOTE_REQ.unitID = unitID;
			nkmpacket_UNIT_REVIEW_TAG_VOTE_REQ.tagType = tagType;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_REVIEW_TAG_VOTE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x0012AA58 File Offset: 0x00128C58
		public static void Send_NKMPacket_UNIT_REVIEW_TAG_VOTE_CANCEL_REQ(int unitID, short tagType)
		{
			NKMPacket_UNIT_REVIEW_TAG_VOTE_CANCEL_REQ nkmpacket_UNIT_REVIEW_TAG_VOTE_CANCEL_REQ = new NKMPacket_UNIT_REVIEW_TAG_VOTE_CANCEL_REQ();
			nkmpacket_UNIT_REVIEW_TAG_VOTE_CANCEL_REQ.unitID = unitID;
			nkmpacket_UNIT_REVIEW_TAG_VOTE_CANCEL_REQ.tagType = tagType;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_REVIEW_TAG_VOTE_CANCEL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x0012AA8C File Offset: 0x00128C8C
		public static void Send_NKMPacket_REFRESH_COMPANY_BUFF_REQ()
		{
			NKMPacket_REFRESH_COMPANY_BUFF_REQ packet = new NKMPacket_REFRESH_COMPANY_BUFF_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x0012AAB4 File Offset: 0x00128CB4
		public static void Send_NKMPacket_ACCOUNT_LINK_REQ()
		{
			NKMPacket_ACCOUNT_LINK_REQ packet = new NKMPacket_ACCOUNT_LINK_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x0012AADC File Offset: 0x00128CDC
		public static void Send_NKMPacket_ACCOUNT_LEAVE_STATE_REQ(bool bLeave)
		{
			NKMPacket_ACCOUNT_LEAVE_STATE_REQ nkmpacket_ACCOUNT_LEAVE_STATE_REQ = new NKMPacket_ACCOUNT_LEAVE_STATE_REQ();
			nkmpacket_ACCOUNT_LEAVE_STATE_REQ.leave = bLeave;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_ACCOUNT_LEAVE_STATE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x0012AB0C File Offset: 0x00128D0C
		public static void Send_NKMPacket_EVENT_BINGO_RANDOM_MARK_REQ(int eventID)
		{
			NKMPacket_EVENT_BINGO_RANDOM_MARK_REQ nkmpacket_EVENT_BINGO_RANDOM_MARK_REQ = new NKMPacket_EVENT_BINGO_RANDOM_MARK_REQ();
			nkmpacket_EVENT_BINGO_RANDOM_MARK_REQ.eventId = eventID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EVENT_BINGO_RANDOM_MARK_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x0012AB3C File Offset: 0x00128D3C
		public static void Send_NKMPacket_EVENT_BINGO_INDEX_MARK_REQ(int eventID, int tileIndex)
		{
			NKMPacket_EVENT_BINGO_INDEX_MARK_REQ nkmpacket_EVENT_BINGO_INDEX_MARK_REQ = new NKMPacket_EVENT_BINGO_INDEX_MARK_REQ();
			nkmpacket_EVENT_BINGO_INDEX_MARK_REQ.eventId = eventID;
			nkmpacket_EVENT_BINGO_INDEX_MARK_REQ.tileIndex = tileIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EVENT_BINGO_INDEX_MARK_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x0012AB70 File Offset: 0x00128D70
		public static void Send_NKMPacket_EVENT_BINGO_REWARD_REQ(int eventID, int rewardIndex)
		{
			NKMPacket_EVENT_BINGO_REWARD_REQ nkmpacket_EVENT_BINGO_REWARD_REQ = new NKMPacket_EVENT_BINGO_REWARD_REQ();
			nkmpacket_EVENT_BINGO_REWARD_REQ.eventId = eventID;
			nkmpacket_EVENT_BINGO_REWARD_REQ.rewardIndex = rewardIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EVENT_BINGO_REWARD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x0012ABA4 File Offset: 0x00128DA4
		public static void Send_NKMPacket_ZLONG_USE_COUPON_REQ(string code)
		{
			int zlongServerId = 1;
			if (!int.TryParse(NKCDownloadConfig.s_ServerID, out zlongServerId))
			{
				return;
			}
			NKMPacket_ZLONG_USE_COUPON_REQ2 nkmpacket_ZLONG_USE_COUPON_REQ = new NKMPacket_ZLONG_USE_COUPON_REQ2();
			nkmpacket_ZLONG_USE_COUPON_REQ.couponCode = code;
			nkmpacket_ZLONG_USE_COUPON_REQ.zlongServerId = zlongServerId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_ZLONG_USE_COUPON_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x0012ABEC File Offset: 0x00128DEC
		public static void Send_NKMPacket_SURVEY_COMPLETE_REQ(long id)
		{
			NKMPacket_SURVEY_COMPLETE_REQ nkmpacket_SURVEY_COMPLETE_REQ = new NKMPacket_SURVEY_COMPLETE_REQ();
			nkmpacket_SURVEY_COMPLETE_REQ.surveyId = id;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SURVEY_COMPLETE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x0012AC1C File Offset: 0x00128E1C
		public static void Send_NKMPacket_EQUIP_ITEM_CHANGE_SET_OPTION_REQ(long equipUID)
		{
			NKMInventoryData inventoryData = NKCScenManager.CurrentUserData().m_InventoryData;
			NKMEquipItemData itemEquip = inventoryData.GetItemEquip(equipUID);
			if (itemEquip == null)
			{
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			if (equipTemplet.SetGroupList == null)
			{
				return;
			}
			if (equipTemplet.SetGroupList.Count <= 0)
			{
				return;
			}
			bool flag = true;
			if ((long)equipTemplet.m_RandomSetReqItemValue > inventoryData.GetCountMiscItem(equipTemplet.m_RandomSetReqItemID))
			{
				flag = false;
			}
			if (flag && (long)equipTemplet.m_RandomSetReqResource > inventoryData.GetCountMiscItem(1))
			{
				int randomSetReqResource = equipTemplet.m_RandomSetReqResource;
				if (NKCCompanyBuff.NeedShowEventMark(NKCScenManager.CurrentUserData().m_companyBuffDataList, NKMConst.Buff.BuffType.BASE_FACTORY_ENCHANT_TUNING_CREDIT_DISCOUNT))
				{
					NKCCompanyBuff.SetDiscountOfCreditInEnchantTuning(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref randomSetReqResource);
				}
				if ((long)randomSetReqResource > inventoryData.GetCountMiscItem(1))
				{
					flag = false;
				}
			}
			if (!flag)
			{
				Debug.Log(string.Format("자원이 부족합니다 - Send_NKMPacket_EQUIP_ITEM_CHANGE_SET_OPTION_REQ : id : {0}", itemEquip.m_ItemEquipID));
				return;
			}
			NKMPacket_EQUIP_ITEM_CHANGE_SET_OPTION_REQ nkmpacket_EQUIP_ITEM_CHANGE_SET_OPTION_REQ = new NKMPacket_EQUIP_ITEM_CHANGE_SET_OPTION_REQ();
			nkmpacket_EQUIP_ITEM_CHANGE_SET_OPTION_REQ.equipUID = equipUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_ITEM_CHANGE_SET_OPTION_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x0012AD10 File Offset: 0x00128F10
		public static void Send_NKMPacket_EQUIP_ITEM_CONFIRM_SET_OPTION_REQ(long equipUID)
		{
			NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(equipUID);
			if (itemEquip == null)
			{
				return;
			}
			if (NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID) == null)
			{
				return;
			}
			NKMPacket_EQUIP_ITEM_CONFIRM_SET_OPTION_REQ nkmpacket_EQUIP_ITEM_CONFIRM_SET_OPTION_REQ = new NKMPacket_EQUIP_ITEM_CONFIRM_SET_OPTION_REQ();
			nkmpacket_EQUIP_ITEM_CONFIRM_SET_OPTION_REQ.equipUID = equipUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_ITEM_CONFIRM_SET_OPTION_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x0012AD60 File Offset: 0x00128F60
		public static void Send_NKMPacket_EQUIP_ITEM_FIRST_SET_OPTION_REQ(long equipUID)
		{
			if (equipUID == 0L)
			{
				return;
			}
			NKMEquipItemData itemEquip = NKCScenManager.CurrentUserData().m_InventoryData.GetItemEquip(equipUID);
			if (itemEquip == null)
			{
				return;
			}
			if (itemEquip.m_SetOptionId > 0)
			{
				return;
			}
			NKMEquipTemplet equipTemplet = NKMItemManager.GetEquipTemplet(itemEquip.m_ItemEquipID);
			if (equipTemplet == null)
			{
				return;
			}
			if (equipTemplet.m_lstSetGroup.Count <= 0)
			{
				return;
			}
			NKMPacket_EQUIP_ITEM_FIRST_SET_OPTION_REQ nkmpacket_EQUIP_ITEM_FIRST_SET_OPTION_REQ = new NKMPacket_EQUIP_ITEM_FIRST_SET_OPTION_REQ();
			nkmpacket_EQUIP_ITEM_FIRST_SET_OPTION_REQ.equipUID = equipUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_ITEM_FIRST_SET_OPTION_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x0012ADD0 File Offset: 0x00128FD0
		public static void Send_NKMPacket_Equip_Tuning_Cancel_REQ()
		{
			NKMPacket_EQUIP_TUNING_CANCEL_REQ packet = new NKMPacket_EQUIP_TUNING_CANCEL_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x0012ADF8 File Offset: 0x00128FF8
		public static void Send_NKMPacket_EQUIP_UPGRADE_REQ(long equipUID, List<long> consumeEquipItemUidList)
		{
			NKMPacket_EQUIP_UPGRADE_REQ nkmpacket_EQUIP_UPGRADE_REQ = new NKMPacket_EQUIP_UPGRADE_REQ();
			nkmpacket_EQUIP_UPGRADE_REQ.equipUid = equipUID;
			nkmpacket_EQUIP_UPGRADE_REQ.consumeEquipItemUidList = consumeEquipItemUidList;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_UPGRADE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x0012AE2C File Offset: 0x0012902C
		public static void Send_NKMPacket_EQUIP_OPEN_SOCKET_REQ(long equipUId, int socketIndex)
		{
			NKMPacket_EQUIP_OPEN_SOCKET_REQ nkmpacket_EQUIP_OPEN_SOCKET_REQ = new NKMPacket_EQUIP_OPEN_SOCKET_REQ();
			nkmpacket_EQUIP_OPEN_SOCKET_REQ.equipUid = equipUId;
			nkmpacket_EQUIP_OPEN_SOCKET_REQ.socketIndex = socketIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_OPEN_SOCKET_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x0012AE60 File Offset: 0x00129060
		public static void Send_NKMPacket_RESET_STAGE_PLAY_COUNT_REQ(int stageID)
		{
			NKMPacket_RESET_STAGE_PLAY_COUNT_REQ nkmpacket_RESET_STAGE_PLAY_COUNT_REQ = new NKMPacket_RESET_STAGE_PLAY_COUNT_REQ();
			nkmpacket_RESET_STAGE_PLAY_COUNT_REQ.stageId = stageID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_RESET_STAGE_PLAY_COUNT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x0012AE90 File Offset: 0x00129090
		public static void Send_NKMPacket_BACKGROUND_CHANGE_REQ(NKMBackgroundInfo bgInfo)
		{
			NKMPacket_BACKGROUND_CHANGE_REQ nkmpacket_BACKGROUND_CHANGE_REQ = new NKMPacket_BACKGROUND_CHANGE_REQ();
			nkmpacket_BACKGROUND_CHANGE_REQ.backgroundInfo = bgInfo;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_BACKGROUND_CHANGE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x0012AEC0 File Offset: 0x001290C0
		public static void Send_NKMPacket_GUILD_CHAT_TRANSLATE_REQ(long guildUID, long chatUID, string targetLangCode)
		{
			NKMPacket_GUILD_CHAT_TRANSLATE_REQ nkmpacket_GUILD_CHAT_TRANSLATE_REQ = new NKMPacket_GUILD_CHAT_TRANSLATE_REQ();
			nkmpacket_GUILD_CHAT_TRANSLATE_REQ.guildUid = guildUID;
			nkmpacket_GUILD_CHAT_TRANSLATE_REQ.messageUid = chatUID;
			nkmpacket_GUILD_CHAT_TRANSLATE_REQ.targetLanguage = targetLangCode;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_CHAT_TRANSLATE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x0012AEFC File Offset: 0x001290FC
		public static void Send_NKMPacket_GUILD_CREATE_REQ(string name, GuildJoinType joinType, long badgeId, string greeting)
		{
			NKMPacket_GUILD_CREATE_REQ nkmpacket_GUILD_CREATE_REQ = new NKMPacket_GUILD_CREATE_REQ();
			nkmpacket_GUILD_CREATE_REQ.guildName = name;
			nkmpacket_GUILD_CREATE_REQ.guildJoinType = joinType;
			nkmpacket_GUILD_CREATE_REQ.badgeId = badgeId;
			nkmpacket_GUILD_CREATE_REQ.greeting = greeting;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_CREATE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x0012AF40 File Offset: 0x00129140
		public static void Send_NKMPacket_GUILD_CLOSE_REQ(long guildUid)
		{
			NKMPacket_GUILD_CLOSE_REQ nkmpacket_GUILD_CLOSE_REQ = new NKMPacket_GUILD_CLOSE_REQ();
			nkmpacket_GUILD_CLOSE_REQ.guildUid = guildUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_CLOSE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x0012AF70 File Offset: 0x00129170
		public static void Send_NKMPacket_GUILD_CLOSE_CANCEL_REQ(long guildUid)
		{
			NKMPacket_GUILD_CLOSE_CANCEL_REQ nkmpacket_GUILD_CLOSE_CANCEL_REQ = new NKMPacket_GUILD_CLOSE_CANCEL_REQ();
			nkmpacket_GUILD_CLOSE_CANCEL_REQ.guildUid = guildUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_CLOSE_CANCEL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x0012AFA0 File Offset: 0x001291A0
		public static void Send_NKMPacket_GUILD_SEARCH_REQ(string keyword)
		{
			NKMPacket_GUILD_SEARCH_REQ nkmpacket_GUILD_SEARCH_REQ = new NKMPacket_GUILD_SEARCH_REQ();
			nkmpacket_GUILD_SEARCH_REQ.keyword = keyword;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_SEARCH_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x0012AFD0 File Offset: 0x001291D0
		public static void Send_NKMPacket_GUILD_LIST_REQ(GuildListType listType)
		{
			NKMPacket_GUILD_LIST_REQ nkmpacket_GUILD_LIST_REQ = new NKMPacket_GUILD_LIST_REQ();
			nkmpacket_GUILD_LIST_REQ.guildListType = listType;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x0012B000 File Offset: 0x00129200
		public static void Send_NKMPacket_GUILD_DATA_REQ(long uid)
		{
			NKMPacket_GUILD_DATA_REQ nkmpacket_GUILD_DATA_REQ = new NKMPacket_GUILD_DATA_REQ();
			nkmpacket_GUILD_DATA_REQ.guildUid = uid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_DATA_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x0012B030 File Offset: 0x00129230
		public static void Send_NKMPacket_GUILD_JOIN_REQ(long guildUid, GuildJoinType joinType)
		{
			NKMPacket_GUILD_JOIN_REQ nkmpacket_GUILD_JOIN_REQ = new NKMPacket_GUILD_JOIN_REQ();
			nkmpacket_GUILD_JOIN_REQ.guildUid = guildUid;
			nkmpacket_GUILD_JOIN_REQ.guildJoinType = joinType;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_JOIN_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x0012B064 File Offset: 0x00129264
		public static void Send_NKMPacket_GUILD_CANCEL_JOIN_REQ(long guildUid)
		{
			NKMPacket_GUILD_CANCEL_JOIN_REQ nkmpacket_GUILD_CANCEL_JOIN_REQ = new NKMPacket_GUILD_CANCEL_JOIN_REQ();
			nkmpacket_GUILD_CANCEL_JOIN_REQ.guildUid = guildUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_CANCEL_JOIN_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x0012B094 File Offset: 0x00129294
		public static void Send_NKMPacket_GUILD_ACCEPT_JOIN_REQ(long guildUid, long userUid, bool bAllow)
		{
			NKMPacket_GUILD_ACCEPT_JOIN_REQ nkmpacket_GUILD_ACCEPT_JOIN_REQ = new NKMPacket_GUILD_ACCEPT_JOIN_REQ();
			nkmpacket_GUILD_ACCEPT_JOIN_REQ.joinUserUid = userUid;
			nkmpacket_GUILD_ACCEPT_JOIN_REQ.guildUid = guildUid;
			nkmpacket_GUILD_ACCEPT_JOIN_REQ.isAllow = bAllow;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_ACCEPT_JOIN_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x0012B0D0 File Offset: 0x001292D0
		public static void Send_NKMPacket_GUILD_INVITE_REQ(long guildUid, long userUid)
		{
			NKMPacket_GUILD_INVITE_REQ nkmpacket_GUILD_INVITE_REQ = new NKMPacket_GUILD_INVITE_REQ();
			nkmpacket_GUILD_INVITE_REQ.guildUid = guildUid;
			nkmpacket_GUILD_INVITE_REQ.userUid = userUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_INVITE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x0012B104 File Offset: 0x00129304
		public static void Send_NKMPacket_GUILD_CANCEL_INVITE_REQ(long guildUid, long userUid)
		{
			NKMPacket_GUILD_CANCEL_INVITE_REQ nkmpacket_GUILD_CANCEL_INVITE_REQ = new NKMPacket_GUILD_CANCEL_INVITE_REQ();
			nkmpacket_GUILD_CANCEL_INVITE_REQ.guildUid = guildUid;
			nkmpacket_GUILD_CANCEL_INVITE_REQ.userUid = userUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_CANCEL_INVITE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x0012B138 File Offset: 0x00129338
		public static void Send_NKMPacket_GUILD_ACCEPT_INVITE_REQ(long guildUid, bool bAllow)
		{
			NKMPacket_GUILD_ACCEPT_INVITE_REQ nkmpacket_GUILD_ACCEPT_INVITE_REQ = new NKMPacket_GUILD_ACCEPT_INVITE_REQ();
			nkmpacket_GUILD_ACCEPT_INVITE_REQ.guildUid = guildUid;
			nkmpacket_GUILD_ACCEPT_INVITE_REQ.isAllow = bAllow;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_ACCEPT_INVITE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x0012B16C File Offset: 0x0012936C
		public static void Send_NKMPacket_GUILD_EXIT_REQ(long guildUid)
		{
			NKMPacket_GUILD_EXIT_REQ nkmpacket_GUILD_EXIT_REQ = new NKMPacket_GUILD_EXIT_REQ();
			nkmpacket_GUILD_EXIT_REQ.guildUid = guildUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_EXIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x0012B19C File Offset: 0x0012939C
		public static void Send_NKMPacket_GUILD_SET_MEMBER_GRADE_REQ(long guildUid, long targetUserUid, GuildMemberGrade grade)
		{
			NKMPacket_GUILD_SET_MEMBER_GRADE_REQ nkmpacket_GUILD_SET_MEMBER_GRADE_REQ = new NKMPacket_GUILD_SET_MEMBER_GRADE_REQ();
			nkmpacket_GUILD_SET_MEMBER_GRADE_REQ.guildUid = guildUid;
			nkmpacket_GUILD_SET_MEMBER_GRADE_REQ.targetUserUid = targetUserUid;
			nkmpacket_GUILD_SET_MEMBER_GRADE_REQ.grade = grade;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_SET_MEMBER_GRADE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x0012B1D8 File Offset: 0x001293D8
		public static void Send_NKMPacket_GUILD_BAN_REQ(long guildUid, long targetUserUid, int banReason)
		{
			NKMPacket_GUILD_BAN_REQ nkmpacket_GUILD_BAN_REQ = new NKMPacket_GUILD_BAN_REQ();
			nkmpacket_GUILD_BAN_REQ.guildUid = guildUid;
			nkmpacket_GUILD_BAN_REQ.targetUserUid = targetUserUid;
			nkmpacket_GUILD_BAN_REQ.banReason = banReason;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_BAN_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x0012B214 File Offset: 0x00129414
		public static void Send_NKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_REQ(long guildUid, long targetUserUid)
		{
			NKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_REQ nkmpacket_GUILD_MASTER_SPECIFIED_MIGRATION_REQ = new NKMPacket_GUILD_MASTER_SPECIFIED_MIGRATION_REQ();
			nkmpacket_GUILD_MASTER_SPECIFIED_MIGRATION_REQ.guildUid = guildUid;
			nkmpacket_GUILD_MASTER_SPECIFIED_MIGRATION_REQ.targetUserUid = targetUserUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_MASTER_SPECIFIED_MIGRATION_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x0012B248 File Offset: 0x00129448
		public static void Send_NKMPacket_GUILD_MASTER_MIGRATION_REQ(long guildUid)
		{
			NKMPacket_GUILD_MASTER_MIGRATION_REQ nkmpacket_GUILD_MASTER_MIGRATION_REQ = new NKMPacket_GUILD_MASTER_MIGRATION_REQ();
			nkmpacket_GUILD_MASTER_MIGRATION_REQ.guildUid = guildUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_MASTER_MIGRATION_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x0012B278 File Offset: 0x00129478
		public static void Send_NKMPacket_GUILD_UPDATE_DATA_REQ(long guildUid, long badgeId, string greeting, GuildJoinType guildJoinType)
		{
			NKMPacket_GUILD_UPDATE_DATA_REQ nkmpacket_GUILD_UPDATE_DATA_REQ = new NKMPacket_GUILD_UPDATE_DATA_REQ();
			nkmpacket_GUILD_UPDATE_DATA_REQ.guildUid = guildUid;
			nkmpacket_GUILD_UPDATE_DATA_REQ.badgeId = badgeId;
			nkmpacket_GUILD_UPDATE_DATA_REQ.greeting = greeting;
			nkmpacket_GUILD_UPDATE_DATA_REQ.guildJoinType = guildJoinType;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_UPDATE_DATA_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x0012B2BC File Offset: 0x001294BC
		public static void Send_NKMPacket_GUILD_UPDATE_NOTICE_REQ(long guildUid, string notice)
		{
			NKMPacket_GUILD_UPDATE_NOTICE_REQ nkmpacket_GUILD_UPDATE_NOTICE_REQ = new NKMPacket_GUILD_UPDATE_NOTICE_REQ();
			nkmpacket_GUILD_UPDATE_NOTICE_REQ.guildUid = guildUid;
			nkmpacket_GUILD_UPDATE_NOTICE_REQ.notice = notice;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_UPDATE_NOTICE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x0012B2F0 File Offset: 0x001294F0
		public static void Send_NKMPacket_GUILD_UPDATE_MEMBER_GREETING_REQ(long guildUid, string greeting)
		{
			NKMPacket_GUILD_UPDATE_MEMBER_GREETING_REQ nkmpacket_GUILD_UPDATE_MEMBER_GREETING_REQ = new NKMPacket_GUILD_UPDATE_MEMBER_GREETING_REQ();
			nkmpacket_GUILD_UPDATE_MEMBER_GREETING_REQ.guildUid = guildUid;
			nkmpacket_GUILD_UPDATE_MEMBER_GREETING_REQ.greeting = greeting;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_UPDATE_MEMBER_GREETING_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x0012B324 File Offset: 0x00129524
		public static void Send_NKMPacket_GUILD_ATTENDANCE_REQ(long guildUid)
		{
			NKMPacket_GUILD_ATTENDANCE_REQ nkmpacket_GUILD_ATTENDANCE_REQ = new NKMPacket_GUILD_ATTENDANCE_REQ();
			nkmpacket_GUILD_ATTENDANCE_REQ.guildUid = guildUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_ATTENDANCE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x0012B354 File Offset: 0x00129554
		public static void Send_NKMPacket_GUILD_RECOMMEND_INVITE_LIST_REQ(long guildUid)
		{
			NKMPacket_GUILD_RECOMMEND_INVITE_LIST_REQ nkmpacket_GUILD_RECOMMEND_INVITE_LIST_REQ = new NKMPacket_GUILD_RECOMMEND_INVITE_LIST_REQ();
			nkmpacket_GUILD_RECOMMEND_INVITE_LIST_REQ.guildUid = guildUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_RECOMMEND_INVITE_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x0012B384 File Offset: 0x00129584
		public static void Send_NKMPacket_GUILD_DONATION_REQ(int donationId)
		{
			NKMPacket_GUILD_DONATION_REQ nkmpacket_GUILD_DONATION_REQ = new NKMPacket_GUILD_DONATION_REQ();
			nkmpacket_GUILD_DONATION_REQ.guildUid = NKCGuildManager.MyData.guildUid;
			nkmpacket_GUILD_DONATION_REQ.donationId = donationId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_DONATION_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x0012B3C4 File Offset: 0x001295C4
		public static void Send_NKMPacket_GUILD_BUY_BUFF_REQ(int welfareId)
		{
			NKMPacket_GUILD_BUY_BUFF_REQ nkmpacket_GUILD_BUY_BUFF_REQ = new NKMPacket_GUILD_BUY_BUFF_REQ();
			nkmpacket_GUILD_BUY_BUFF_REQ.guildUid = NKCGuildManager.MyData.guildUid;
			nkmpacket_GUILD_BUY_BUFF_REQ.welfareId = welfareId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_BUY_BUFF_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x0012B404 File Offset: 0x00129604
		public static void Send_NKMPacket_GUILD_BUY_WELFARE_POINT_REQ(int buyCount)
		{
			NKMPacket_GUILD_BUY_WELFARE_POINT_REQ nkmpacket_GUILD_BUY_WELFARE_POINT_REQ = new NKMPacket_GUILD_BUY_WELFARE_POINT_REQ();
			nkmpacket_GUILD_BUY_WELFARE_POINT_REQ.buyCount = buyCount;
			nkmpacket_GUILD_BUY_WELFARE_POINT_REQ.guildUid = NKCGuildManager.MyData.guildUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_BUY_WELFARE_POINT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x0012B444 File Offset: 0x00129644
		public static void Send_NKMPacket_GUILD_DUNGEON_INFO_REQ(long guildUid)
		{
			if (NKCPopupOKCancel.isOpen())
			{
				return;
			}
			NKMPacket_GUILD_DUNGEON_INFO_REQ nkmpacket_GUILD_DUNGEON_INFO_REQ = new NKMPacket_GUILD_DUNGEON_INFO_REQ();
			nkmpacket_GUILD_DUNGEON_INFO_REQ.guildUid = guildUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_DUNGEON_INFO_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x0012B47C File Offset: 0x0012967C
		public static void Send_NKMPacket_GUILD_DUNGEON_MEMBER_INFO_REQ(long guildUid)
		{
			NKMPacket_GUILD_DUNGEON_MEMBER_INFO_REQ nkmpacket_GUILD_DUNGEON_MEMBER_INFO_REQ = new NKMPacket_GUILD_DUNGEON_MEMBER_INFO_REQ();
			nkmpacket_GUILD_DUNGEON_MEMBER_INFO_REQ.guildUid = guildUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_DUNGEON_MEMBER_INFO_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x0012B4AC File Offset: 0x001296AC
		public static void Send_NKMPacket_GUILD_DUNGEON_SEASON_REWARD_REQ(GuildDungeonRewardCategory category, int rewardCountValue)
		{
			NKMPacket_GUILD_DUNGEON_SEASON_REWARD_REQ nkmpacket_GUILD_DUNGEON_SEASON_REWARD_REQ = new NKMPacket_GUILD_DUNGEON_SEASON_REWARD_REQ();
			nkmpacket_GUILD_DUNGEON_SEASON_REWARD_REQ.rewardCategory = category;
			nkmpacket_GUILD_DUNGEON_SEASON_REWARD_REQ.rewardCountValue = rewardCountValue;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_DUNGEON_SEASON_REWARD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x0012B4E0 File Offset: 0x001296E0
		public static void Send_NKMPacket_GUILD_DUNGEON_TICKET_BUY_REQ()
		{
			NKMPacket_GUILD_DUNGEON_TICKET_BUY_REQ packet = new NKMPacket_GUILD_DUNGEON_TICKET_BUY_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x0012B508 File Offset: 0x00129708
		public static void Send_NKMPacket_GUILD_DUNGEON_BOSS_GAME_LOAD_REQ(int bossStageId, byte selectedDeckIndex)
		{
			NKMPacket_GUILD_DUNGEON_BOSS_GAME_LOAD_REQ nkmpacket_GUILD_DUNGEON_BOSS_GAME_LOAD_REQ = new NKMPacket_GUILD_DUNGEON_BOSS_GAME_LOAD_REQ();
			nkmpacket_GUILD_DUNGEON_BOSS_GAME_LOAD_REQ.bossStageId = bossStageId;
			nkmpacket_GUILD_DUNGEON_BOSS_GAME_LOAD_REQ.selectDeckIndex = selectedDeckIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_DUNGEON_BOSS_GAME_LOAD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x0012B53C File Offset: 0x0012973C
		public static void Send_NKMPacket_GUILD_DUNGEON_SESSION_REWARD_REQ()
		{
			NKMPacket_GUILD_DUNGEON_SESSION_REWARD_REQ packet = new NKMPacket_GUILD_DUNGEON_SESSION_REWARD_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x0012B564 File Offset: 0x00129764
		public static void Send_NKMPacket_GUILD_CHAT_LIST_REQ(long guildUid)
		{
			NKMPacket_GUILD_CHAT_LIST_REQ nkmpacket_GUILD_CHAT_LIST_REQ = new NKMPacket_GUILD_CHAT_LIST_REQ();
			nkmpacket_GUILD_CHAT_LIST_REQ.guildUid = guildUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_CHAT_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x0012B594 File Offset: 0x00129794
		public static void Send_NKMPacket_GUILD_CHAT_REQ(long guildUid, ChatMessageType messageType, string message, int emotionId)
		{
			NKMPacket_GUILD_CHAT_REQ nkmpacket_GUILD_CHAT_REQ = new NKMPacket_GUILD_CHAT_REQ();
			nkmpacket_GUILD_CHAT_REQ.guildUid = guildUid;
			nkmpacket_GUILD_CHAT_REQ.messageType = messageType;
			nkmpacket_GUILD_CHAT_REQ.message = message;
			nkmpacket_GUILD_CHAT_REQ.emotionId = emotionId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_CHAT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x0012B5D8 File Offset: 0x001297D8
		public static void Send_NKMPacket_GUILD_CHAT_COMPLAIN_REQ(long guildUid, long messageUid)
		{
			NKMPacket_GUILD_CHAT_COMPLAIN_REQ nkmpacket_GUILD_CHAT_COMPLAIN_REQ = new NKMPacket_GUILD_CHAT_COMPLAIN_REQ();
			nkmpacket_GUILD_CHAT_COMPLAIN_REQ.guildUid = guildUid;
			nkmpacket_GUILD_CHAT_COMPLAIN_REQ.messageUid = messageUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_GUILD_CHAT_COMPLAIN_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x0012B60C File Offset: 0x0012980C
		public static void Send_NKMPacket_PRIVATE_CHAT_REQ(long userUid, string message, int emotionId)
		{
			NKMPacket_PRIVATE_CHAT_REQ nkmpacket_PRIVATE_CHAT_REQ = new NKMPacket_PRIVATE_CHAT_REQ();
			nkmpacket_PRIVATE_CHAT_REQ.userUid = userUid;
			nkmpacket_PRIVATE_CHAT_REQ.message = message;
			nkmpacket_PRIVATE_CHAT_REQ.emotionId = emotionId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PRIVATE_CHAT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x0012B648 File Offset: 0x00129848
		public static void Send_NKMPacket_PRIVATE_CHAT_LIST_REQ(long userUid)
		{
			NKMPacket_PRIVATE_CHAT_LIST_REQ nkmpacket_PRIVATE_CHAT_LIST_REQ = new NKMPacket_PRIVATE_CHAT_LIST_REQ();
			nkmpacket_PRIVATE_CHAT_LIST_REQ.userUid = userUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PRIVATE_CHAT_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x0012B678 File Offset: 0x00129878
		public static void Send_NKMPacket_PRIVATE_CHAT_ALL_LIST_REQ()
		{
			NKMPacket_PRIVATE_CHAT_ALL_LIST_REQ packet = new NKMPacket_PRIVATE_CHAT_ALL_LIST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x0012B6A0 File Offset: 0x001298A0
		public static void Send_NKMPacket_LEADERBOARD_ACHIEVE_LIST_REQ(bool bAll)
		{
			NKMPacket_LEADERBOARD_ACHIEVE_LIST_REQ nkmpacket_LEADERBOARD_ACHIEVE_LIST_REQ = new NKMPacket_LEADERBOARD_ACHIEVE_LIST_REQ();
			nkmpacket_LEADERBOARD_ACHIEVE_LIST_REQ.isAll = bAll;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_LEADERBOARD_ACHIEVE_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x0012B6D0 File Offset: 0x001298D0
		public static void Send_NKMPacket_LEADERBOARD_SHADOWPALACE_LIST_REQ(int actId, bool bAll = false)
		{
			NKMPacket_LEADERBOARD_SHADOWPALACE_LIST_REQ nkmpacket_LEADERBOARD_SHADOWPALACE_LIST_REQ = new NKMPacket_LEADERBOARD_SHADOWPALACE_LIST_REQ();
			nkmpacket_LEADERBOARD_SHADOWPALACE_LIST_REQ.actId = actId;
			nkmpacket_LEADERBOARD_SHADOWPALACE_LIST_REQ.isAll = bAll;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_LEADERBOARD_SHADOWPALACE_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x0012B704 File Offset: 0x00129904
		public static void Send_NKMPacket_LEADERBOARD_FIERCE_LIST_REQ(bool bIsAll = false)
		{
			NKMPacket_LEADERBOARD_FIERCE_LIST_REQ nkmpacket_LEADERBOARD_FIERCE_LIST_REQ = new NKMPacket_LEADERBOARD_FIERCE_LIST_REQ();
			nkmpacket_LEADERBOARD_FIERCE_LIST_REQ.isAll = bIsAll;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_LEADERBOARD_FIERCE_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x0012B734 File Offset: 0x00129934
		public static void Send_NKMPacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_REQ(int bossGroupID, bool bIsAll = false)
		{
			NKMPacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_REQ nkmpacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_REQ = new NKMPacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_REQ();
			nkmpacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_REQ.fierceBossGroupId = bossGroupID;
			nkmpacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_REQ.isAll = bIsAll;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_LEADERBOARD_FIERCE_BOSSGROUP_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039F1 RID: 14833 RVA: 0x0012B768 File Offset: 0x00129968
		public static void Send_NKMPacket_LEADERBOARD_GUILD_LEVEL_RANK_LIST_REQ()
		{
			NKMPacket_LEADERBOARD_GUILD_LEVEL_RANK_LIST_REQ packet = new NKMPacket_LEADERBOARD_GUILD_LEVEL_RANK_LIST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x0012B790 File Offset: 0x00129990
		public static void Send_NKMPacket_LEADERBOARD_GUILD_UNION_RANK_LIST_REQ(int seasonId)
		{
			NKMPacket_LEADERBOARD_GUILD_UNION_RANK_LIST_REQ nkmpacket_LEADERBOARD_GUILD_UNION_RANK_LIST_REQ = new NKMPacket_LEADERBOARD_GUILD_UNION_RANK_LIST_REQ();
			nkmpacket_LEADERBOARD_GUILD_UNION_RANK_LIST_REQ.seasonId = seasonId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_LEADERBOARD_GUILD_UNION_RANK_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x0012B7C0 File Offset: 0x001299C0
		public static void Send_NKMPacket_LEADERBOARD_TIMEATTACK_LIST_REQ(int stageId, bool bIsAll = false)
		{
			NKMPacket_LEADERBOARD_TIMEATTACK_LIST_REQ nkmpacket_LEADERBOARD_TIMEATTACK_LIST_REQ = new NKMPacket_LEADERBOARD_TIMEATTACK_LIST_REQ();
			nkmpacket_LEADERBOARD_TIMEATTACK_LIST_REQ.stageId = stageId;
			nkmpacket_LEADERBOARD_TIMEATTACK_LIST_REQ.isAll = bIsAll;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_LEADERBOARD_TIMEATTACK_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x0012B7F4 File Offset: 0x001299F4
		public static void Send_NKMPacket_SHADOW_PALACE_START_REQ(int palaceID)
		{
			NKMPacket_SHADOW_PALACE_START_REQ nkmpacket_SHADOW_PALACE_START_REQ = new NKMPacket_SHADOW_PALACE_START_REQ();
			nkmpacket_SHADOW_PALACE_START_REQ.palaceId = palaceID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHADOW_PALACE_START_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x0012B824 File Offset: 0x00129A24
		public static void Send_NKMPacket_SHADOW_PALACE_GIVEUP_ACK(int palaceID)
		{
			NKMPacket_SHADOW_PALACE_GIVEUP_REQ nkmpacket_SHADOW_PALACE_GIVEUP_REQ = new NKMPacket_SHADOW_PALACE_GIVEUP_REQ();
			nkmpacket_SHADOW_PALACE_GIVEUP_REQ.palaceId = palaceID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHADOW_PALACE_GIVEUP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x0012B854 File Offset: 0x00129A54
		public static void Send_NKMPacket_SHADOW_PALACE_SKIP_REQ(int palaceId, int skipCount)
		{
			NKMPacket_SHADOW_PALACE_SKIP_REQ nkmpacket_SHADOW_PALACE_SKIP_REQ = new NKMPacket_SHADOW_PALACE_SKIP_REQ();
			nkmpacket_SHADOW_PALACE_SKIP_REQ.palaceId = palaceId;
			nkmpacket_SHADOW_PALACE_SKIP_REQ.skipCount = skipCount;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHADOW_PALACE_SKIP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x0012B888 File Offset: 0x00129A88
		public static void Send_NKMPacket_FIERCE_DATA_REQ()
		{
			NKCScenManager.GetScenManager().GetConnectGame().Send(new NKMPacket_FIERCE_DATA_REQ(), NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x0012B8A1 File Offset: 0x00129AA1
		public static void Send_NKMPacket_FIERCE_COMPLETE_RANK_REWARD_REQ()
		{
			NKCScenManager.GetScenManager().GetConnectGame().Send(new NKMPacket_FIERCE_COMPLETE_RANK_REWARD_REQ(), NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x0012B8BC File Offset: 0x00129ABC
		public static void Send_NKMPacket_FIERCE_COMPLETE_POINT_REWARD_REQ(int pointRewardID)
		{
			NKMPacket_FIERCE_COMPLETE_POINT_REWARD_REQ nkmpacket_FIERCE_COMPLETE_POINT_REWARD_REQ = new NKMPacket_FIERCE_COMPLETE_POINT_REWARD_REQ();
			nkmpacket_FIERCE_COMPLETE_POINT_REWARD_REQ.fiercePointRewardId = pointRewardID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FIERCE_COMPLETE_POINT_REWARD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x0012B8E9 File Offset: 0x00129AE9
		public static void Send_NKMPacket_FIERCE_COMPLETE_POINT_REWARD_ALL_REQ()
		{
			NKCScenManager.GetScenManager().GetConnectGame().Send(new NKMPacket_FIERCE_COMPLETE_POINT_REWARD_ALL_REQ(), NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x0012B904 File Offset: 0x00129B04
		public static void Send_NKMPacket_FIERCE_PROFILE_REQ(long userUID, bool bForce)
		{
			NKMPacket_FIERCE_PROFILE_REQ nkmpacket_FIERCE_PROFILE_REQ = new NKMPacket_FIERCE_PROFILE_REQ();
			nkmpacket_FIERCE_PROFILE_REQ.userUid = userUID;
			nkmpacket_FIERCE_PROFILE_REQ.isForce = bForce;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FIERCE_PROFILE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x0012B938 File Offset: 0x00129B38
		public static void Send_NKMPacket_FIERCE_PENALTY_REQ(int fierceBossID, List<int> lstPenaltys)
		{
			NKMPacket_FIERCE_PENALTY_REQ nkmpacket_FIERCE_PENALTY_REQ = new NKMPacket_FIERCE_PENALTY_REQ();
			nkmpacket_FIERCE_PENALTY_REQ.fierceBossId = fierceBossID;
			nkmpacket_FIERCE_PENALTY_REQ.penaltyIds = lstPenaltys;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_FIERCE_PENALTY_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039FD RID: 14845 RVA: 0x0012B96C File Offset: 0x00129B6C
		public static void Send_NKMPacket_FIERCE_DAILY_REWARD_REQ()
		{
			NKMPacket_FIERCE_DAILY_REWARD_REQ packet = new NKMPacket_FIERCE_DAILY_REWARD_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x0012B992 File Offset: 0x00129B92
		public static void Send_NKMPacket_MENTORING_DATA_REQ()
		{
			NKCScenManager.GetScenManager().GetConnectGame().Send(new NKMPacket_MENTORING_DATA_REQ(), NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x0012B9AC File Offset: 0x00129BAC
		public static void Send_NKMPacket_MENTORING_MATCH_LIST_REQ(bool bForce = false)
		{
			NKMPacket_MENTORING_MATCH_LIST_REQ nkmpacket_MENTORING_MATCH_LIST_REQ = new NKMPacket_MENTORING_MATCH_LIST_REQ();
			nkmpacket_MENTORING_MATCH_LIST_REQ.isForce = bForce;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_MENTORING_MATCH_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x0012B9DC File Offset: 0x00129BDC
		public static void Send_kNKMPacket_MENTORING_RECEIVE_LIST_REQ(MentoringIdentity mentoringIdentity, bool bForce = false)
		{
			NKMPacket_MENTORING_RECEIVE_LIST_REQ nkmpacket_MENTORING_RECEIVE_LIST_REQ = new NKMPacket_MENTORING_RECEIVE_LIST_REQ();
			nkmpacket_MENTORING_RECEIVE_LIST_REQ.identity = mentoringIdentity;
			nkmpacket_MENTORING_RECEIVE_LIST_REQ.isForce = bForce;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_MENTORING_RECEIVE_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x0012BA10 File Offset: 0x00129C10
		public static void Send_NKMPacket_MENTORING_SEARCH_LIST_REQ(MentoringIdentity mentoringIdentity, string keyword)
		{
			NKMPacket_MENTORING_SEARCH_LIST_REQ nkmpacket_MENTORING_SEARCH_LIST_REQ = new NKMPacket_MENTORING_SEARCH_LIST_REQ();
			nkmpacket_MENTORING_SEARCH_LIST_REQ.identity = mentoringIdentity;
			nkmpacket_MENTORING_SEARCH_LIST_REQ.keyword = keyword;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_MENTORING_SEARCH_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x0012BA44 File Offset: 0x00129C44
		public static void Send_NKMPacket_MENTORING_DELETE_MENTEE_REQ(long deleteMenteeUID)
		{
			NKMPacket_MENTORING_DELETE_MENTEE_REQ nkmpacket_MENTORING_DELETE_MENTEE_REQ = new NKMPacket_MENTORING_DELETE_MENTEE_REQ();
			nkmpacket_MENTORING_DELETE_MENTEE_REQ.menteeUid = deleteMenteeUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_MENTORING_DELETE_MENTEE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x0012BA74 File Offset: 0x00129C74
		public static void Send_NKMPacket_MENTORING_ADD_REQ(MentoringIdentity mentoringType, long userUid)
		{
			NKMPacket_MENTORING_ADD_REQ nkmpacket_MENTORING_ADD_REQ = new NKMPacket_MENTORING_ADD_REQ();
			nkmpacket_MENTORING_ADD_REQ.identity = mentoringType;
			nkmpacket_MENTORING_ADD_REQ.userUid = userUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_MENTORING_ADD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A04 RID: 14852 RVA: 0x0012BAA8 File Offset: 0x00129CA8
		public static void Send_NKMPacket_MENTORING_ACCEPT_MENTOR_REQ(long userUid)
		{
			NKMPacket_MENTORING_ACCEPT_MENTOR_REQ nkmpacket_MENTORING_ACCEPT_MENTOR_REQ = new NKMPacket_MENTORING_ACCEPT_MENTOR_REQ();
			nkmpacket_MENTORING_ACCEPT_MENTOR_REQ.mentorUid = userUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_MENTORING_ACCEPT_MENTOR_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x0012BAD8 File Offset: 0x00129CD8
		public static void Send_NKMPacket_MENTORING_DISACCEPT_MENTOR_REQ(long userUid)
		{
			NKMPacket_MENTORING_DISACCEPT_MENTOR_REQ nkmpacket_MENTORING_DISACCEPT_MENTOR_REQ = new NKMPacket_MENTORING_DISACCEPT_MENTOR_REQ();
			nkmpacket_MENTORING_DISACCEPT_MENTOR_REQ.mentorUid = userUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_MENTORING_DISACCEPT_MENTOR_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x0012BB08 File Offset: 0x00129D08
		public static void Send_NKMPacket_MENTORING_INVITE_REWARD_LIST_REQ()
		{
			NKMPacket_MENTORING_INVITE_REWARD_LIST_REQ packet = new NKMPacket_MENTORING_INVITE_REWARD_LIST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x0012BB30 File Offset: 0x00129D30
		public static void Send_NKMPacket_MENTORING_COMPLETE_INVITE_REWARD_REQ(int inviteSuccessRequireCnt)
		{
			NKMPacket_MENTORING_COMPLETE_INVITE_REWARD_REQ nkmpacket_MENTORING_COMPLETE_INVITE_REWARD_REQ = new NKMPacket_MENTORING_COMPLETE_INVITE_REWARD_REQ();
			nkmpacket_MENTORING_COMPLETE_INVITE_REWARD_REQ.inviteSuccessRequireCnt = inviteSuccessRequireCnt;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_MENTORING_COMPLETE_INVITE_REWARD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x0012BB60 File Offset: 0x00129D60
		public static void Send_NKMPacket_MENTORING_COMPLETE_INVITE_REWARD_ALL_REQ()
		{
			NKMPacket_MENTORING_COMPLETE_INVITE_REWARD_ALL_REQ packet = new NKMPacket_MENTORING_COMPLETE_INVITE_REWARD_ALL_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x0012BB86 File Offset: 0x00129D86
		public static void Send_NKMPacket_MENTORING_SEASON_ID_REQ()
		{
			NKCScenManager.GetScenManager().GetConnectGame().Send(new NKMPacket_MENTORING_SEASON_ID_REQ(), NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A0A RID: 14858 RVA: 0x0012BBA0 File Offset: 0x00129DA0
		public static void Send_NKMPacket_OPERATOR_LEVELUP_REQ(long operatorUID, List<MiscItemData> lstMat)
		{
			NKMPacket_OPERATOR_LEVELUP_REQ nkmpacket_OPERATOR_LEVELUP_REQ = new NKMPacket_OPERATOR_LEVELUP_REQ();
			nkmpacket_OPERATOR_LEVELUP_REQ.targetUnitUid = operatorUID;
			List<MiscItemData> list = new List<MiscItemData>();
			foreach (MiscItemData miscItemData in lstMat)
			{
				if (miscItemData.count > 0)
				{
					list.Add(miscItemData);
				}
			}
			nkmpacket_OPERATOR_LEVELUP_REQ.materials = list;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OPERATOR_LEVELUP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x0012BC24 File Offset: 0x00129E24
		public static void Send_NKMPacket_OPERATOR_ENHANCE_REQ(long targetOperatorUID, long matOperatorUID, bool bTransfer)
		{
			NKMPacket_OPERATOR_ENHANCE_REQ nkmpacket_OPERATOR_ENHANCE_REQ = new NKMPacket_OPERATOR_ENHANCE_REQ();
			nkmpacket_OPERATOR_ENHANCE_REQ.targetUnitUid = targetOperatorUID;
			nkmpacket_OPERATOR_ENHANCE_REQ.sourceUnitUid = matOperatorUID;
			nkmpacket_OPERATOR_ENHANCE_REQ.transSkill = bTransfer;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OPERATOR_ENHANCE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x0012BC60 File Offset: 0x00129E60
		public static void Send_NKMPacket_OPERATOR_LOCK_REQ(long OperatorUID, bool bLock)
		{
			NKMPacket_OPERATOR_LOCK_REQ nkmpacket_OPERATOR_LOCK_REQ = new NKMPacket_OPERATOR_LOCK_REQ();
			nkmpacket_OPERATOR_LOCK_REQ.unitUID = OperatorUID;
			nkmpacket_OPERATOR_LOCK_REQ.locked = bLock;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OPERATOR_LOCK_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A0D RID: 14861 RVA: 0x0012BC94 File Offset: 0x00129E94
		public static void Send_NKMPacket_OPERATOR_REMOVE_REQ(List<long> lstRemoveOperator)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				return;
			}
			NKMArmyData armyData = nkmuserData.m_ArmyData;
			if (armyData == null)
			{
				return;
			}
			foreach (long num in lstRemoveOperator)
			{
				if (!armyData.m_dicMyOperator.ContainsKey(num))
				{
					NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL, NKCUtilString.GET_STRING_NO_EXIST_UNIT, null, "");
					return;
				}
				NKM_ERROR_CODE canDeleteOperator = NKMUnitManager.GetCanDeleteOperator(armyData.GetOperatorFromUId(num), nkmuserData);
				if (canDeleteOperator != NKM_ERROR_CODE.NEC_OK)
				{
					switch (canDeleteOperator)
					{
					case NKM_ERROR_CODE.NEC_FAIL_UNIT_LOCKED:
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL, NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL_LOCKED, null, "");
						return;
					case NKM_ERROR_CODE.NEC_FAIL_UNIT_IN_DECK:
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL, NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL_IN_DECK, null, "");
						return;
					case NKM_ERROR_CODE.NEC_FAIL_UNIT_IS_LOBBYUNIT:
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL, NKCUtilString.GET_STRING_REMOVE_UNIT_FAIL_MAINUNIT, null, "");
						return;
					default:
						NKCPopupOKCancel.OpenOKBox(canDeleteOperator, null, "");
						return;
					}
				}
			}
			armyData.InitUnitDelete();
			armyData.SetUnitDeleteList(lstRemoveOperator);
			NKCPacketSender.Send_NKMPacket_OPERATOR_REMOVE_REQ();
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x0012BDB4 File Offset: 0x00129FB4
		public static void Send_NKMPacket_OPERATOR_REMOVE_REQ()
		{
			NKMArmyData armyData = NKCScenManager.GetScenManager().GetMyUserData().m_ArmyData;
			if (armyData.IsEmptyUnitDeleteList)
			{
				return;
			}
			List<long> unitDeleteList = armyData.GetUnitDeleteList();
			NKMPacket_OPERATOR_REMOVE_REQ nkmpacket_OPERATOR_REMOVE_REQ = new NKMPacket_OPERATOR_REMOVE_REQ();
			nkmpacket_OPERATOR_REMOVE_REQ.removeUnitUIDList = unitDeleteList;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OPERATOR_REMOVE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x0012BE04 File Offset: 0x0012A004
		public static void Send_NKMPacket_UPDATE_MARKET_REVIEW_REQ(string reviewDesc)
		{
			NKMPacket_UPDATE_MARKET_REVIEW_REQ nkmpacket_UPDATE_MARKET_REVIEW_REQ = new NKMPacket_UPDATE_MARKET_REVIEW_REQ();
			nkmpacket_UPDATE_MARKET_REVIEW_REQ.description = reviewDesc;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UPDATE_MARKET_REVIEW_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x0012BE34 File Offset: 0x0012A034
		public static void Send_NKMPacket_DECK_OPERATOR_SET_REQ(NKMDeckIndex deckIdx, long operatorUID)
		{
			NKMPacket_DECK_OPERATOR_SET_REQ nkmpacket_DECK_OPERATOR_SET_REQ = new NKMPacket_DECK_OPERATOR_SET_REQ();
			nkmpacket_DECK_OPERATOR_SET_REQ.deckIndex = deckIdx;
			nkmpacket_DECK_OPERATOR_SET_REQ.operatorUid = operatorUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DECK_OPERATOR_SET_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x0012BE68 File Offset: 0x0012A068
		public static void Send_NKMPacket_PVP_CASTING_VOTE_UNIT_REQ(List<int> lstUnitIDs)
		{
			if (lstUnitIDs == null)
			{
				return;
			}
			NKMPacket_PVP_CASTING_VOTE_UNIT_REQ nkmpacket_PVP_CASTING_VOTE_UNIT_REQ = new NKMPacket_PVP_CASTING_VOTE_UNIT_REQ();
			nkmpacket_PVP_CASTING_VOTE_UNIT_REQ.unitIdList = lstUnitIDs;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PVP_CASTING_VOTE_UNIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x0012BE9C File Offset: 0x0012A09C
		public static void Send_NKMPacket_PVP_CASTING_VOTE_SHIP_REQ(List<int> lstShipGroupIDs)
		{
			NKMPacket_PVP_CASTING_VOTE_SHIP_REQ nkmpacket_PVP_CASTING_VOTE_SHIP_REQ = new NKMPacket_PVP_CASTING_VOTE_SHIP_REQ();
			nkmpacket_PVP_CASTING_VOTE_SHIP_REQ.shipIdList = lstShipGroupIDs;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PVP_CASTING_VOTE_SHIP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x0012BECC File Offset: 0x0012A0CC
		public static void Send_NKMPacket_PVP_CASTING_VOTE_OPERATOR_REQ(List<int> lstOperIDs)
		{
			NKMPacket_PVP_CASTING_VOTE_OPERATOR_REQ nkmpacket_PVP_CASTING_VOTE_OPERATOR_REQ = new NKMPacket_PVP_CASTING_VOTE_OPERATOR_REQ();
			nkmpacket_PVP_CASTING_VOTE_OPERATOR_REQ.operatorIdList = lstOperIDs;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_PVP_CASTING_VOTE_OPERATOR_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x0012BEFC File Offset: 0x0012A0FC
		public static void Send_NKMPacket_EVENT_PASS_REQ(NKC_OPEN_WAIT_BOX_TYPE waitBoxType)
		{
			NKMPacket_EVENT_PASS_REQ packet = new NKMPacket_EVENT_PASS_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, waitBoxType, true);
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x0012BF24 File Offset: 0x0012A124
		public static void Send_NKMPacket_EVENT_PASS_LEVEL_COMPLETE_REQ()
		{
			NKMPacket_EVENT_PASS_LEVEL_COMPLETE_REQ packet = new NKMPacket_EVENT_PASS_LEVEL_COMPLETE_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x0012BF4C File Offset: 0x0012A14C
		public static void Send_NKMPacket_EVENT_PASS_MISSION_REQ(EventPassMissionType eventPassMissionType)
		{
			NKMPacket_EVENT_PASS_MISSION_REQ nkmpacket_EVENT_PASS_MISSION_REQ = new NKMPacket_EVENT_PASS_MISSION_REQ();
			nkmpacket_EVENT_PASS_MISSION_REQ.missionType = eventPassMissionType;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EVENT_PASS_MISSION_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x0012BF7C File Offset: 0x0012A17C
		public static void Send_NKMPacket_EVENT_PASS_FINAL_MISSION_COMPLETE_REQ(EventPassMissionType eventPassMissionType)
		{
			NKMPacket_EVENT_PASS_FINAL_MISSION_COMPLETE_REQ nkmpacket_EVENT_PASS_FINAL_MISSION_COMPLETE_REQ = new NKMPacket_EVENT_PASS_FINAL_MISSION_COMPLETE_REQ();
			nkmpacket_EVENT_PASS_FINAL_MISSION_COMPLETE_REQ.missionType = eventPassMissionType;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EVENT_PASS_FINAL_MISSION_COMPLETE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x0012BFAC File Offset: 0x0012A1AC
		public static void Send_cNKMPacket_EVENT_PASS_DAILY_MISSION_RETRY_REQ(int retryMissionId)
		{
			NKMPacket_EVENT_PASS_DAILY_MISSION_RETRY_REQ nkmpacket_EVENT_PASS_DAILY_MISSION_RETRY_REQ = new NKMPacket_EVENT_PASS_DAILY_MISSION_RETRY_REQ();
			nkmpacket_EVENT_PASS_DAILY_MISSION_RETRY_REQ.missionId = retryMissionId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EVENT_PASS_DAILY_MISSION_RETRY_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x0012BFDC File Offset: 0x0012A1DC
		public static void Send_NKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_REQ()
		{
			NKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_REQ packet = new NKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x0012C004 File Offset: 0x0012A204
		public static void Send_NKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_PLUS_REQ()
		{
			NKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_PLUS_REQ packet = new NKMPacket_EVENT_PASS_PURCHASE_CORE_PASS_PLUS_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x0012C02C File Offset: 0x0012A22C
		public static void Send_NKMPacket_EVENT_PASS_LEVEL_UP_REQ(int increasedLevel)
		{
			NKMPacket_EVENT_PASS_LEVEL_UP_REQ nkmpacket_EVENT_PASS_LEVEL_UP_REQ = new NKMPacket_EVENT_PASS_LEVEL_UP_REQ();
			nkmpacket_EVENT_PASS_LEVEL_UP_REQ.increaseLv = increasedLevel;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EVENT_PASS_LEVEL_UP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x0012C05C File Offset: 0x0012A25C
		public static void Send_NKMPacket_EQUIP_PRESET_LIST_REQ(NKC_OPEN_WAIT_BOX_TYPE waitBoxType)
		{
			NKMPacket_EQUIP_PRESET_LIST_REQ packet = new NKMPacket_EQUIP_PRESET_LIST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, waitBoxType, true);
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x0012C084 File Offset: 0x0012A284
		public static void Send_NKMPacket_EQUIP_PRESET_ADD_REQ(int value)
		{
			NKMPacket_EQUIP_PRESET_ADD_REQ nkmpacket_EQUIP_PRESET_ADD_REQ = new NKMPacket_EQUIP_PRESET_ADD_REQ();
			nkmpacket_EQUIP_PRESET_ADD_REQ.addPresetCount = value;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_PRESET_ADD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x0012C0B4 File Offset: 0x0012A2B4
		public static void Send_NKMPacket_EQUIP_PRESET_NAME_CHANGE_REQ(int presetIndex, string presetName)
		{
			NKMPacket_EQUIP_PRESET_CHANGE_NAME_REQ nkmpacket_EQUIP_PRESET_CHANGE_NAME_REQ = new NKMPacket_EQUIP_PRESET_CHANGE_NAME_REQ();
			nkmpacket_EQUIP_PRESET_CHANGE_NAME_REQ.presetIndex = presetIndex;
			nkmpacket_EQUIP_PRESET_CHANGE_NAME_REQ.newPresetName = presetName;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_PRESET_CHANGE_NAME_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x0012C0E8 File Offset: 0x0012A2E8
		public static void Send_NKMPacket_EQUIP_PRESET_REGISTER_ALL_REQ(int presetIndex, long unitUId)
		{
			NKMPacket_EQUIP_PRESET_REGISTER_ALL_REQ nkmpacket_EQUIP_PRESET_REGISTER_ALL_REQ = new NKMPacket_EQUIP_PRESET_REGISTER_ALL_REQ();
			nkmpacket_EQUIP_PRESET_REGISTER_ALL_REQ.presetIndex = presetIndex;
			nkmpacket_EQUIP_PRESET_REGISTER_ALL_REQ.unitUid = unitUId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_PRESET_REGISTER_ALL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x0012C11C File Offset: 0x0012A31C
		public static void Send_NKMPacket_EQUIP_PRESET_REGISTER_REQ(int presetIndex, ITEM_EQUIP_POSITION equipPosition, long equipUId)
		{
			NKMPacket_EQUIP_PRESET_REGISTER_REQ nkmpacket_EQUIP_PRESET_REGISTER_REQ = new NKMPacket_EQUIP_PRESET_REGISTER_REQ();
			nkmpacket_EQUIP_PRESET_REGISTER_REQ.presetIndex = presetIndex;
			nkmpacket_EQUIP_PRESET_REGISTER_REQ.equipPosition = equipPosition;
			nkmpacket_EQUIP_PRESET_REGISTER_REQ.equipUid = equipUId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_PRESET_REGISTER_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x0012C158 File Offset: 0x0012A358
		public static void Send_NKMPacket_EQUIP_PRESET_APPLY_REQ(int presetIndex, long applyUnitUId)
		{
			NKMPacket_EQUIP_PRESET_APPLY_REQ nkmpacket_EQUIP_PRESET_APPLY_REQ = new NKMPacket_EQUIP_PRESET_APPLY_REQ();
			nkmpacket_EQUIP_PRESET_APPLY_REQ.presetIndex = presetIndex;
			nkmpacket_EQUIP_PRESET_APPLY_REQ.applyUnitUid = applyUnitUId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_PRESET_APPLY_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x0012C18C File Offset: 0x0012A38C
		public static void Send_NKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ(List<NKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ.PresetIndexData> changeIndices)
		{
			NKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ nkmpacket_EQUIP_PRESET_CHANGE_INDEX_REQ = new NKMPacket_EQUIP_PRESET_CHANGE_INDEX_REQ();
			if (changeIndices != null)
			{
				nkmpacket_EQUIP_PRESET_CHANGE_INDEX_REQ.changeIndices = changeIndices;
			}
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EQUIP_PRESET_CHANGE_INDEX_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x0012C1BC File Offset: 0x0012A3BC
		public static void Send_NKMPACKET_RACE_TEAM_SELECT_REQ(RaceTeam selectedTeam)
		{
			NKMPACKET_RACE_TEAM_SELECT_REQ nkmpacket_RACE_TEAM_SELECT_REQ = new NKMPACKET_RACE_TEAM_SELECT_REQ();
			nkmpacket_RACE_TEAM_SELECT_REQ.selectTeam = selectedTeam;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_RACE_TEAM_SELECT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x0012C1EC File Offset: 0x0012A3EC
		public static void Send_NKMPACKET_RACE_START_REQ(int selectedLine)
		{
			NKMPACKET_RACE_START_REQ nkmpacket_RACE_START_REQ = new NKMPACKET_RACE_START_REQ();
			nkmpacket_RACE_START_REQ.selectLine = selectedLine;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_RACE_START_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x0012C21C File Offset: 0x0012A41C
		public static void Send_NKMPacket_DUNGEON_SKIP_REQ(int dungeonId, List<long> lstUnits, int skip)
		{
			NKMPacket_DUNGEON_SKIP_REQ nkmpacket_DUNGEON_SKIP_REQ = new NKMPacket_DUNGEON_SKIP_REQ();
			nkmpacket_DUNGEON_SKIP_REQ.dungeonId = dungeonId;
			nkmpacket_DUNGEON_SKIP_REQ.skip = skip;
			nkmpacket_DUNGEON_SKIP_REQ.unitUids = lstUnits;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_DUNGEON_SKIP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x0012C258 File Offset: 0x0012A458
		public static void Send_NKMPacket_OFFICE_OPEN_SECTION_REQ(int sectionId)
		{
			NKMPacket_OFFICE_OPEN_SECTION_REQ nkmpacket_OFFICE_OPEN_SECTION_REQ = new NKMPacket_OFFICE_OPEN_SECTION_REQ();
			nkmpacket_OFFICE_OPEN_SECTION_REQ.sectionId = sectionId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_OPEN_SECTION_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x0012C288 File Offset: 0x0012A488
		public static void Send_NKMPacket_OFFICE_OPEN_ROOM_REQ(int roomId)
		{
			NKMPacket_OFFICE_OPEN_ROOM_REQ nkmpacket_OFFICE_OPEN_ROOM_REQ = new NKMPacket_OFFICE_OPEN_ROOM_REQ();
			nkmpacket_OFFICE_OPEN_ROOM_REQ.roomId = roomId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_OPEN_ROOM_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x0012C2B8 File Offset: 0x0012A4B8
		public static void Send_NKMPacket_OFFICE_SET_ROOM_NAME_REQ(int roomId, string roomName)
		{
			NKMPacket_OFFICE_SET_ROOM_NAME_REQ nkmpacket_OFFICE_SET_ROOM_NAME_REQ = new NKMPacket_OFFICE_SET_ROOM_NAME_REQ();
			nkmpacket_OFFICE_SET_ROOM_NAME_REQ.roomId = roomId;
			nkmpacket_OFFICE_SET_ROOM_NAME_REQ.roomName = roomName;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_SET_ROOM_NAME_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x0012C2EC File Offset: 0x0012A4EC
		public static void Send_NKMPacket_OFFICE_ROOM_SET_UNIT_REQ(int roomId, List<long> unitUId)
		{
			NKMPacket_OFFICE_SET_ROOM_UNIT_REQ nkmpacket_OFFICE_SET_ROOM_UNIT_REQ = new NKMPacket_OFFICE_SET_ROOM_UNIT_REQ();
			nkmpacket_OFFICE_SET_ROOM_UNIT_REQ.roomId = roomId;
			nkmpacket_OFFICE_SET_ROOM_UNIT_REQ.unitUids = unitUId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_SET_ROOM_UNIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x0012C320 File Offset: 0x0012A520
		public static void Send_NKMPacket_OFFICE_SET_ROOM_WALL_REQ(int roomID, int interiorID)
		{
			NKMPacket_OFFICE_SET_ROOM_WALL_REQ nkmpacket_OFFICE_SET_ROOM_WALL_REQ = new NKMPacket_OFFICE_SET_ROOM_WALL_REQ();
			nkmpacket_OFFICE_SET_ROOM_WALL_REQ.roomId = roomID;
			nkmpacket_OFFICE_SET_ROOM_WALL_REQ.wallInteriorId = interiorID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_SET_ROOM_WALL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x0012C354 File Offset: 0x0012A554
		public static void Send_NKMPacket_OFFICE_SET_ROOM_FLOOR_REQ(int roomID, int interiorID)
		{
			NKMPacket_OFFICE_SET_ROOM_FLOOR_REQ nkmpacket_OFFICE_SET_ROOM_FLOOR_REQ = new NKMPacket_OFFICE_SET_ROOM_FLOOR_REQ();
			nkmpacket_OFFICE_SET_ROOM_FLOOR_REQ.roomId = roomID;
			nkmpacket_OFFICE_SET_ROOM_FLOOR_REQ.floorInteriorId = interiorID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_SET_ROOM_FLOOR_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x0012C388 File Offset: 0x0012A588
		public static void Send_NKMPacket_OFFICE_SET_ROOM_BACKGROUND_REQ(int roomID, int interiorID)
		{
			NKMPacket_OFFICE_SET_ROOM_BACKGROUND_REQ nkmpacket_OFFICE_SET_ROOM_BACKGROUND_REQ = new NKMPacket_OFFICE_SET_ROOM_BACKGROUND_REQ();
			nkmpacket_OFFICE_SET_ROOM_BACKGROUND_REQ.roomId = roomID;
			nkmpacket_OFFICE_SET_ROOM_BACKGROUND_REQ.backgroundId = interiorID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_SET_ROOM_BACKGROUND_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x0012C3BC File Offset: 0x0012A5BC
		public static void Send_NKMPacket_OFFICE_ADD_FURNITURE_REQ(int roomId, int itemId, OfficePlaneType planeType, int positionX, int positionY, bool inverted)
		{
			NKMPacket_OFFICE_ADD_FURNITURE_REQ nkmpacket_OFFICE_ADD_FURNITURE_REQ = new NKMPacket_OFFICE_ADD_FURNITURE_REQ();
			nkmpacket_OFFICE_ADD_FURNITURE_REQ.roomId = roomId;
			nkmpacket_OFFICE_ADD_FURNITURE_REQ.itemId = itemId;
			nkmpacket_OFFICE_ADD_FURNITURE_REQ.planeType = planeType;
			nkmpacket_OFFICE_ADD_FURNITURE_REQ.positionX = positionX;
			nkmpacket_OFFICE_ADD_FURNITURE_REQ.positionY = positionY;
			nkmpacket_OFFICE_ADD_FURNITURE_REQ.inverted = inverted;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_ADD_FURNITURE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x0012C410 File Offset: 0x0012A610
		public static void Send_NKMPacket_OFFICE_UPDATE_FURNITURE_REQ(int roomId, long furnitureUid, OfficePlaneType planeType, int positionX, int positionY, bool inverted)
		{
			NKMPacket_OFFICE_UPDATE_FURNITURE_REQ nkmpacket_OFFICE_UPDATE_FURNITURE_REQ = new NKMPacket_OFFICE_UPDATE_FURNITURE_REQ();
			nkmpacket_OFFICE_UPDATE_FURNITURE_REQ.roomId = roomId;
			nkmpacket_OFFICE_UPDATE_FURNITURE_REQ.furnitureUid = furnitureUid;
			nkmpacket_OFFICE_UPDATE_FURNITURE_REQ.planeType = planeType;
			nkmpacket_OFFICE_UPDATE_FURNITURE_REQ.positionX = positionX;
			nkmpacket_OFFICE_UPDATE_FURNITURE_REQ.positionY = positionY;
			nkmpacket_OFFICE_UPDATE_FURNITURE_REQ.inverted = inverted;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_UPDATE_FURNITURE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x0012C464 File Offset: 0x0012A664
		public static void Send_NKMPacket_OFFICE_REMOVE_FURNITURE_REQ(int roomId, long furnitureUid)
		{
			NKMPacket_OFFICE_REMOVE_FURNITURE_REQ nkmpacket_OFFICE_REMOVE_FURNITURE_REQ = new NKMPacket_OFFICE_REMOVE_FURNITURE_REQ();
			nkmpacket_OFFICE_REMOVE_FURNITURE_REQ.roomId = roomId;
			nkmpacket_OFFICE_REMOVE_FURNITURE_REQ.furnitureUid = furnitureUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_REMOVE_FURNITURE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x0012C498 File Offset: 0x0012A698
		public static void Send_NKMPacket_OFFICE_CLEAR_ALL_FURNITURE_REQ(int roomId)
		{
			NKMPacket_OFFICE_CLEAR_ALL_FURNITURE_REQ nkmpacket_OFFICE_CLEAR_ALL_FURNITURE_REQ = new NKMPacket_OFFICE_CLEAR_ALL_FURNITURE_REQ();
			nkmpacket_OFFICE_CLEAR_ALL_FURNITURE_REQ.roomId = roomId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_CLEAR_ALL_FURNITURE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_SMALL, true);
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x0012C4C8 File Offset: 0x0012A6C8
		public static void Send_NKMPacket_OFFICE_TAKE_HEART_REQ(long unitUid)
		{
			NKMPacket_OFFICE_TAKE_HEART_REQ nkmpacket_OFFICE_TAKE_HEART_REQ = new NKMPacket_OFFICE_TAKE_HEART_REQ();
			nkmpacket_OFFICE_TAKE_HEART_REQ.unitUid = unitUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_TAKE_HEART_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x0012C4F8 File Offset: 0x0012A6F8
		public static void Send_NKMPacket_OFFICE_STATE_REQ(long userUId)
		{
			NKMPacket_OFFICE_STATE_REQ nkmpacket_OFFICE_STATE_REQ = new NKMPacket_OFFICE_STATE_REQ();
			nkmpacket_OFFICE_STATE_REQ.userUid = userUId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_STATE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x0012C528 File Offset: 0x0012A728
		public static void Send_NKMPacket_OFFICE_POST_SEND_REQ(long receiverUserUid)
		{
			NKMPacket_OFFICE_POST_SEND_REQ nkmpacket_OFFICE_POST_SEND_REQ = new NKMPacket_OFFICE_POST_SEND_REQ();
			nkmpacket_OFFICE_POST_SEND_REQ.receiverUserUid = receiverUserUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_POST_SEND_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x0012C558 File Offset: 0x0012A758
		public static void Send_NKMPacket_OFFICE_POST_LIST_REQ(long lastPostUid = 0L)
		{
			NKMPacket_OFFICE_POST_LIST_REQ nkmpacket_OFFICE_POST_LIST_REQ = new NKMPacket_OFFICE_POST_LIST_REQ();
			nkmpacket_OFFICE_POST_LIST_REQ.lastPostUid = lastPostUid;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_POST_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x0012C588 File Offset: 0x0012A788
		public static void Send_NKMPacket_OFFICE_POST_RECV_REQ()
		{
			NKMPacket_OFFICE_POST_RECV_REQ packet = new NKMPacket_OFFICE_POST_RECV_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x0012C5B0 File Offset: 0x0012A7B0
		public static void Send_NKMPacket_OFFICE_POST_BROADCAST_REQ()
		{
			NKMPacket_OFFICE_POST_BROADCAST_REQ packet = new NKMPacket_OFFICE_POST_BROADCAST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x0012C5D8 File Offset: 0x0012A7D8
		public static void Send_NKMPacket_OFFICE_RANDOM_VISIT_REQ()
		{
			NKMPacket_OFFICE_RANDOM_VISIT_REQ packet = new NKMPacket_OFFICE_RANDOM_VISIT_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x0012C600 File Offset: 0x0012A800
		public static void Send_NKMPacket_OFFICE_PARTY_REQ(int roomID)
		{
			NKMPacket_OFFICE_PARTY_REQ nkmpacket_OFFICE_PARTY_REQ = new NKMPacket_OFFICE_PARTY_REQ();
			nkmpacket_OFFICE_PARTY_REQ.roomId = roomID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_PARTY_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x0012C630 File Offset: 0x0012A830
		public static void Send_NKMPacket_OFFICE_PRESET_REGISTER_REQ(int roomID, int presetID)
		{
			NKMPacket_OFFICE_PRESET_REGISTER_REQ nkmpacket_OFFICE_PRESET_REGISTER_REQ = new NKMPacket_OFFICE_PRESET_REGISTER_REQ();
			nkmpacket_OFFICE_PRESET_REGISTER_REQ.roomId = roomID;
			nkmpacket_OFFICE_PRESET_REGISTER_REQ.presetId = presetID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_PRESET_REGISTER_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x0012C664 File Offset: 0x0012A864
		public static void Send_NKMPacket_OFFICE_PRESET_APPLY_REQ(int roomID, int presetID)
		{
			NKMPacket_OFFICE_PRESET_APPLY_REQ nkmpacket_OFFICE_PRESET_APPLY_REQ = new NKMPacket_OFFICE_PRESET_APPLY_REQ();
			nkmpacket_OFFICE_PRESET_APPLY_REQ.roomId = roomID;
			nkmpacket_OFFICE_PRESET_APPLY_REQ.presetId = presetID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_PRESET_APPLY_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x0012C698 File Offset: 0x0012A898
		public static void Send_NKMPacket_OFFICE_PRESET_ADD_REQ(int addCount)
		{
			NKMPacket_OFFICE_PRESET_ADD_REQ nkmpacket_OFFICE_PRESET_ADD_REQ = new NKMPacket_OFFICE_PRESET_ADD_REQ();
			nkmpacket_OFFICE_PRESET_ADD_REQ.addPresetCount = addCount;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_PRESET_ADD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x0012C6C8 File Offset: 0x0012A8C8
		public static void Send_NKMPacket_OFFICE_PRESET_RESET_REQ(int presetID)
		{
			NKMPacket_OFFICE_PRESET_RESET_REQ nkmpacket_OFFICE_PRESET_RESET_REQ = new NKMPacket_OFFICE_PRESET_RESET_REQ();
			nkmpacket_OFFICE_PRESET_RESET_REQ.presetId = presetID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_PRESET_RESET_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x0012C6F8 File Offset: 0x0012A8F8
		public static void Send_NKMPacket_OFFICE_PRESET_CHANGE_NAME_REQ(int presetID, string name)
		{
			NKMPacket_OFFICE_PRESET_CHANGE_NAME_REQ nkmpacket_OFFICE_PRESET_CHANGE_NAME_REQ = new NKMPacket_OFFICE_PRESET_CHANGE_NAME_REQ();
			nkmpacket_OFFICE_PRESET_CHANGE_NAME_REQ.newPresetName = name;
			nkmpacket_OFFICE_PRESET_CHANGE_NAME_REQ.presetId = presetID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_PRESET_CHANGE_NAME_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x0012C72C File Offset: 0x0012A92C
		public static void Send_NKMPacket_OFFICE_PRESET_APPLY_THEMA_REQ(int roomID, int themeID)
		{
			NKMPacket_OFFICE_PRESET_APPLY_THEMA_REQ nkmpacket_OFFICE_PRESET_APPLY_THEMA_REQ = new NKMPacket_OFFICE_PRESET_APPLY_THEMA_REQ();
			nkmpacket_OFFICE_PRESET_APPLY_THEMA_REQ.roomId = roomID;
			nkmpacket_OFFICE_PRESET_APPLY_THEMA_REQ.themaIndex = themeID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_OFFICE_PRESET_APPLY_THEMA_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x0012C760 File Offset: 0x0012A960
		public static void Send_NKMPacket_RECALL_UNIT_REQ(long sourceUnitUID, int targetUnitID)
		{
			NKMPacket_RECALL_UNIT_REQ nkmpacket_RECALL_UNIT_REQ = new NKMPacket_RECALL_UNIT_REQ();
			nkmpacket_RECALL_UNIT_REQ.recallUnitUid = sourceUnitUID;
			nkmpacket_RECALL_UNIT_REQ.exchangeUnitId = targetUnitID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_RECALL_UNIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x0012C794 File Offset: 0x0012A994
		public static void Send_NKMPacket_KAKAO_MISSION_REFRESH_STATE_REQ(int eventID)
		{
			NKMPacket_KAKAO_MISSION_REFRESH_STATE_REQ nkmpacket_KAKAO_MISSION_REFRESH_STATE_REQ = new NKMPacket_KAKAO_MISSION_REFRESH_STATE_REQ();
			nkmpacket_KAKAO_MISSION_REFRESH_STATE_REQ.eventId = eventID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_KAKAO_MISSION_REFRESH_STATE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x0012C7C4 File Offset: 0x0012A9C4
		public static void Send_NKMPacket_WECHAT_COUPON_CHECK_REQ(int eventTabTempletId, NKC_OPEN_WAIT_BOX_TYPE eNKC_OPEN_WAIT_BOX_TYPE)
		{
			int zlongServerId = 1;
			if (!int.TryParse(NKCDownloadConfig.s_ServerID, out zlongServerId))
			{
				return;
			}
			NKMPacket_WECHAT_COUPON_CHECK_REQ nkmpacket_WECHAT_COUPON_CHECK_REQ = new NKMPacket_WECHAT_COUPON_CHECK_REQ();
			nkmpacket_WECHAT_COUPON_CHECK_REQ.templetId = eventTabTempletId;
			nkmpacket_WECHAT_COUPON_CHECK_REQ.zlongServerId = zlongServerId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WECHAT_COUPON_CHECK_REQ, eNKC_OPEN_WAIT_BOX_TYPE, true);
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x0012C80C File Offset: 0x0012AA0C
		public static void Send_NKMPacket_WECHAT_COUPON_REWARD_REQ(int eventTabTempletId)
		{
			NKMPacket_WECHAT_COUPON_REWARD_REQ nkmpacket_WECHAT_COUPON_REWARD_REQ = new NKMPacket_WECHAT_COUPON_REWARD_REQ();
			nkmpacket_WECHAT_COUPON_REWARD_REQ.templetId = eventTabTempletId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_WECHAT_COUPON_REWARD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x0012C83C File Offset: 0x0012AA3C
		public static void Send_NKMPacket_KILL_COUNT_USER_REWARD_REQ(int templetId, int stepId)
		{
			NKMPacket_KILL_COUNT_USER_REWARD_REQ nkmpacket_KILL_COUNT_USER_REWARD_REQ = new NKMPacket_KILL_COUNT_USER_REWARD_REQ();
			nkmpacket_KILL_COUNT_USER_REWARD_REQ.templetId = templetId;
			nkmpacket_KILL_COUNT_USER_REWARD_REQ.stepId = stepId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_KILL_COUNT_USER_REWARD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x0012C870 File Offset: 0x0012AA70
		public static void Send_NKMPacket_KILL_COUNT_SERVER_REWARD_REQ(int templetId, int stepId)
		{
			NKMPacket_KILL_COUNT_SERVER_REWARD_REQ nkmpacket_KILL_COUNT_SERVER_REWARD_REQ = new NKMPacket_KILL_COUNT_SERVER_REWARD_REQ();
			nkmpacket_KILL_COUNT_SERVER_REWARD_REQ.templetId = templetId;
			nkmpacket_KILL_COUNT_SERVER_REWARD_REQ.stepId = stepId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_KILL_COUNT_SERVER_REWARD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x0012C8A4 File Offset: 0x0012AAA4
		public static void Send_NKMPacket_EXTRACT_UNIT_REQ(List<long> lstExtractUnits)
		{
			NKMPacket_EXTRACT_UNIT_REQ nkmpacket_EXTRACT_UNIT_REQ = new NKMPacket_EXTRACT_UNIT_REQ();
			nkmpacket_EXTRACT_UNIT_REQ.extractUnitUidList = lstExtractUnits;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EXTRACT_UNIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x0012C8D4 File Offset: 0x0012AAD4
		public static void Send_NKMPacket_REARMAMENT_UNIT_REQ(long lResourceUnitUID, int iRearmUnitID)
		{
			NKMPacket_REARMAMENT_UNIT_REQ nkmpacket_REARMAMENT_UNIT_REQ = new NKMPacket_REARMAMENT_UNIT_REQ();
			nkmpacket_REARMAMENT_UNIT_REQ.unitUid = lResourceUnitUID;
			nkmpacket_REARMAMENT_UNIT_REQ.rearmamentId = iRearmUnitID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_REARMAMENT_UNIT_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A47 RID: 14919 RVA: 0x0012C908 File Offset: 0x0012AB08
		public static void Send_NKMPacket_UNIT_MISSION_REWARD_REQ(int unitId, int missionId, int stepId)
		{
			NKMPacket_UNIT_MISSION_REWARD_REQ nkmpacket_UNIT_MISSION_REWARD_REQ = new NKMPacket_UNIT_MISSION_REWARD_REQ();
			nkmpacket_UNIT_MISSION_REWARD_REQ.unitId = unitId;
			nkmpacket_UNIT_MISSION_REWARD_REQ.missionId = missionId;
			nkmpacket_UNIT_MISSION_REWARD_REQ.stepId = stepId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_MISSION_REWARD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A48 RID: 14920 RVA: 0x0012C944 File Offset: 0x0012AB44
		public static void Send_NKMPacket_UNIT_MISSION_REWARD_ALL_REQ(int unitId)
		{
			NKMPacket_UNIT_MISSION_REWARD_ALL_REQ nkmpacket_UNIT_MISSION_REWARD_ALL_REQ = new NKMPacket_UNIT_MISSION_REWARD_ALL_REQ();
			nkmpacket_UNIT_MISSION_REWARD_ALL_REQ.unitId = unitId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_MISSION_REWARD_ALL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x0012C974 File Offset: 0x0012AB74
		public static void Send_NKMPacket_EVENT_BAR_CREATE_COCKTAIL_REQ(int cocktailItemId, int count)
		{
			NKMPacket_EVENT_BAR_CREATE_COCKTAIL_REQ nkmpacket_EVENT_BAR_CREATE_COCKTAIL_REQ = new NKMPacket_EVENT_BAR_CREATE_COCKTAIL_REQ();
			nkmpacket_EVENT_BAR_CREATE_COCKTAIL_REQ.cocktailItemId = cocktailItemId;
			nkmpacket_EVENT_BAR_CREATE_COCKTAIL_REQ.count = count;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EVENT_BAR_CREATE_COCKTAIL_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x0012C9A8 File Offset: 0x0012ABA8
		public static void Send_NKMPacket_EVENT_BAR_GET_REWARD_REQ(int cocktailItemId)
		{
			NKMPacket_EVENT_BAR_GET_REWARD_REQ nkmpacket_EVENT_BAR_GET_REWARD_REQ = new NKMPacket_EVENT_BAR_GET_REWARD_REQ();
			nkmpacket_EVENT_BAR_GET_REWARD_REQ.cocktailItemId = cocktailItemId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EVENT_BAR_GET_REWARD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x0012C9D8 File Offset: 0x0012ABD8
		public static void Send_NKMPacket_AD_ITEM_REWARD_REQ(int adItemId)
		{
			NKMPacket_AD_ITEM_REWARD_REQ nkmpacket_AD_ITEM_REWARD_REQ = new NKMPacket_AD_ITEM_REWARD_REQ();
			nkmpacket_AD_ITEM_REWARD_REQ.aditemId = adItemId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_AD_ITEM_REWARD_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x0012CA08 File Offset: 0x0012AC08
		public static void Send_NKMPacket_AD_INVENTORY_EXPAND_REQ(NKM_INVENTORY_EXPAND_TYPE inventoryType)
		{
			NKMPacket_AD_INVENTORY_EXPAND_REQ nkmpacket_AD_INVENTORY_EXPAND_REQ = new NKMPacket_AD_INVENTORY_EXPAND_REQ();
			nkmpacket_AD_INVENTORY_EXPAND_REQ.inventoryExpandType = inventoryType;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_AD_INVENTORY_EXPAND_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x0012CA38 File Offset: 0x0012AC38
		public static void Send_NKMPacket_BSIDE_COUPON_USE_REQ(string code)
		{
			NKMPacket_BSIDE_COUPON_USE_REQ nkmpacket_BSIDE_COUPON_USE_REQ = new NKMPacket_BSIDE_COUPON_USE_REQ();
			nkmpacket_BSIDE_COUPON_USE_REQ.couponCode = code;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_BSIDE_COUPON_USE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x0012CA68 File Offset: 0x0012AC68
		public static void Send_NKMPacket_ASYNC_PVP_RANK_LIST_REQ(RANK_TYPE type, bool all)
		{
			NKMPacket_ASYNC_PVP_RANK_LIST_REQ nkmpacket_ASYNC_PVP_RANK_LIST_REQ = new NKMPacket_ASYNC_PVP_RANK_LIST_REQ();
			nkmpacket_ASYNC_PVP_RANK_LIST_REQ.rankType = type;
			nkmpacket_ASYNC_PVP_RANK_LIST_REQ.isAll = all;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_ASYNC_PVP_RANK_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x0012CA9C File Offset: 0x0012AC9C
		public static void Send_NKMPacket_ASYNC_PVP_TARGET_LIST_REQ()
		{
			NKMPacket_ASYNC_PVP_TARGET_LIST_REQ packet = new NKMPacket_ASYNC_PVP_TARGET_LIST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x0012CAC4 File Offset: 0x0012ACC4
		public static void Send_NKMPacket_REVENGE_PVP_TARGET_LIST_REQ()
		{
			NKMPacket_REVENGE_PVP_TARGET_LIST_REQ packet = new NKMPacket_REVENGE_PVP_TARGET_LIST_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x0012CAEC File Offset: 0x0012ACEC
		public static void Send_NKMPacket_NPC_PVP_TARGET_LIST_REQ(int _iTargetTier)
		{
			NKMPacket_NPC_PVP_TARGET_LIST_REQ nkmpacket_NPC_PVP_TARGET_LIST_REQ = new NKMPacket_NPC_PVP_TARGET_LIST_REQ();
			nkmpacket_NPC_PVP_TARGET_LIST_REQ.targetTier = _iTargetTier;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_NPC_PVP_TARGET_LIST_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x0012CB1C File Offset: 0x0012AD1C
		public static void Send_NKMPacket_TRIM_START_REQ(int trimId, int trimLevel, List<NKMEventDeckData> eventDeckList)
		{
			NKMPacket_TRIM_START_REQ nkmpacket_TRIM_START_REQ = new NKMPacket_TRIM_START_REQ();
			nkmpacket_TRIM_START_REQ.trimId = trimId;
			nkmpacket_TRIM_START_REQ.trimLevel = trimLevel;
			nkmpacket_TRIM_START_REQ.eventDeckList = eventDeckList;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_TRIM_START_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x0012CB58 File Offset: 0x0012AD58
		public static void Send_NKMPacket_TRIM_RETRY_REQ()
		{
			NKMPacket_TRIM_RETRY_REQ packet = new NKMPacket_TRIM_RETRY_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x0012CB80 File Offset: 0x0012AD80
		public static void Send_NKMPacket_TRIM_RESTORE_REQ(int trimIntervalId)
		{
			NKMPacket_TRIM_RESTORE_REQ nkmpacket_TRIM_RESTORE_REQ = new NKMPacket_TRIM_RESTORE_REQ();
			nkmpacket_TRIM_RESTORE_REQ.trimIntervalId = trimIntervalId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_TRIM_RESTORE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x0012CBB0 File Offset: 0x0012ADB0
		public static void Send_NKMPacket_TRIM_END_REQ(int trimId)
		{
			NKMPacket_TRIM_END_REQ nkmpacket_TRIM_END_REQ = new NKMPacket_TRIM_END_REQ();
			nkmpacket_TRIM_END_REQ.trimId = trimId;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_TRIM_END_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x0012CBE0 File Offset: 0x0012ADE0
		public static void Send_NKMPacket_TRIM_DUNGEON_SKIP_REQ(int trimId, int trimLevel, int skipCount, List<NKMEventDeckData> eventDeckList)
		{
			NKMPacket_TRIM_DUNGEON_SKIP_REQ nkmpacket_TRIM_DUNGEON_SKIP_REQ = new NKMPacket_TRIM_DUNGEON_SKIP_REQ();
			nkmpacket_TRIM_DUNGEON_SKIP_REQ.trimId = trimId;
			nkmpacket_TRIM_DUNGEON_SKIP_REQ.trimLevel = trimLevel;
			nkmpacket_TRIM_DUNGEON_SKIP_REQ.skipCount = skipCount;
			nkmpacket_TRIM_DUNGEON_SKIP_REQ.eventDeckList = eventDeckList;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_TRIM_DUNGEON_SKIP_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A57 RID: 14935 RVA: 0x0012CC24 File Offset: 0x0012AE24
		public static void Send_NKMPacket_EVENT_COLLECTION_MERGE_REQ(int mergeID, int groupID, List<long> lstConsumeUids)
		{
			NKMPacket_EVENT_COLLECTION_MERGE_REQ nkmpacket_EVENT_COLLECTION_MERGE_REQ = new NKMPacket_EVENT_COLLECTION_MERGE_REQ();
			nkmpacket_EVENT_COLLECTION_MERGE_REQ.collectionMergeId = mergeID;
			nkmpacket_EVENT_COLLECTION_MERGE_REQ.mergeRecipeGroupId = groupID;
			nkmpacket_EVENT_COLLECTION_MERGE_REQ.consumeTrophyUids = lstConsumeUids;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_EVENT_COLLECTION_MERGE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x0012CC60 File Offset: 0x0012AE60
		public static void Send_NKMPacket_ACCOUNT_UNLINK_REQ()
		{
			NKMPacket_ACCOUNT_UNLINK_REQ packet = new NKMPacket_ACCOUNT_UNLINK_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x0012CC88 File Offset: 0x0012AE88
		public static void Send_NKMPacket_UNIT_TACTIC_UPDATE_REQ(long unitUID, long consumeUnitUID)
		{
			NKMPacket_UNIT_TACTIC_UPDATE_REQ nkmpacket_UNIT_TACTIC_UPDATE_REQ = new NKMPacket_UNIT_TACTIC_UPDATE_REQ();
			nkmpacket_UNIT_TACTIC_UPDATE_REQ.unitUid = unitUID;
			nkmpacket_UNIT_TACTIC_UPDATE_REQ.consumeUnitUid = consumeUnitUID;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_UNIT_TACTIC_UPDATE_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x04003503 RID: 13571
		private static NKMPacket_CONNECT_CHECK_REQ m_NKMPacket_CONNECT_CHECK_REQ = new NKMPacket_CONNECT_CHECK_REQ();
	}
}

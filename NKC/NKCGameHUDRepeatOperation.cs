using System;
using ClientPacket.Warfare;
using NKC.UI;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC
{
	// Token: 0x02000624 RID: 1572
	public class NKCGameHUDRepeatOperation : MonoBehaviour
	{
		// Token: 0x06003088 RID: 12424 RVA: 0x000EFED4 File Offset: 0x000EE0D4
		public void InitUI()
		{
			this.m_csbtnOn.PointerClick.RemoveAllListeners();
			this.m_csbtnOn.PointerClick.AddListener(new UnityAction(this.OnClick));
			this.m_csbtnOff.PointerClick.RemoveAllListeners();
			this.m_csbtnOff.PointerClick.AddListener(new UnityAction(this.OnClick));
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x000EFF39 File Offset: 0x000EE139
		public void SetDisable()
		{
			this.DisableThisUI();
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000EFF41 File Offset: 0x000EE141
		private void DisableThisUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000EFF50 File Offset: 0x000EE150
		public void SetUI(NKMGameData cNKMGameData)
		{
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.OPERATION_REPEAT, 0, 0))
			{
				this.DisableThisUI();
				return;
			}
			NKM_GAME_TYPE gameType = cNKMGameData.GetGameType();
			if (gameType != NKM_GAME_TYPE.NGT_DUNGEON)
			{
				if (gameType != NKM_GAME_TYPE.NGT_WARFARE)
				{
					if (gameType != NKM_GAME_TYPE.NGT_PHASE)
					{
						this.DisableThisUI();
						return;
					}
					if (!NKCRepeatOperaion.CheckVisible(NKCPhaseManager.GetStageTemplet()))
					{
						this.DisableThisUI();
						return;
					}
					NKCUtil.SetGameobjectActive(base.gameObject, true);
					this.ResetBtnOnOffUI();
					return;
				}
				else
				{
					if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.WARFARE_REPEAT))
					{
						this.DisableThisUI();
						return;
					}
					NKMWarfareTemplet nkmwarfareTemplet = NKMWarfareTemplet.Find(cNKMGameData.m_WarfareID);
					if (nkmwarfareTemplet == null)
					{
						this.DisableThisUI();
						return;
					}
					NKMStageTempletV2 nkmstageTempletV = NKMEpisodeMgr.FindStageTempletByBattleStrID(nkmwarfareTemplet.m_WarfareStrID);
					if (nkmstageTempletV == null)
					{
						this.DisableThisUI();
						return;
					}
					if (nkmstageTempletV.m_bNoAutoRepeat)
					{
						this.DisableThisUI();
						return;
					}
					NKCUtil.SetGameobjectActive(base.gameObject, true);
					this.ResetBtnOnOffUI();
					return;
				}
			}
			else
			{
				NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(cNKMGameData.m_DungeonID);
				if (!NKCRepeatOperaion.CheckVisible((dungeonTempletBase != null) ? dungeonTempletBase.StageTemplet : null))
				{
					this.DisableThisUI();
					return;
				}
				NKCUtil.SetGameobjectActive(base.gameObject, true);
				this.ResetBtnOnOffUI();
				return;
			}
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x000F004D File Offset: 0x000EE24D
		public void ResetBtnOnOffUI()
		{
			this.SetBtnOnOff(NKCScenManager.GetScenManager().GetNKCRepeatOperaion().GetIsOnGoing());
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x000F0064 File Offset: 0x000EE264
		private void SetBtnOnOff(bool bOn)
		{
			NKCUtil.SetGameobjectActive(this.m_csbtnOn, bOn);
			NKCUtil.SetGameobjectActive(this.m_csbtnOff, !bOn);
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000F0084 File Offset: 0x000EE284
		private void OnClick()
		{
			NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
			if (gameClient == null || gameClient.GetGameData() == null)
			{
				return;
			}
			NKMGameData gameData = gameClient.GetGameData();
			NKM_GAME_TYPE gameType = gameData.GetGameType();
			if (gameType != NKM_GAME_TYPE.NGT_WARFARE)
			{
				if (gameType != NKM_GAME_TYPE.NGT_PHASE)
				{
					NKMDungeonTempletBase dungeonTempletBase = NKMDungeonManager.GetDungeonTempletBase(gameData.m_DungeonID);
					if (dungeonTempletBase == null)
					{
						return;
					}
					NKC_SCEN_DUNGEON_ATK_READY scen_DUNGEON_ATK_READY = NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY();
					if (scen_DUNGEON_ATK_READY != null && scen_DUNGEON_ATK_READY.GetLastMultiplyRewardCount() > 1)
					{
						NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_DP_DOUBLE_OPERATION_CANNOT_REPEAT", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
						return;
					}
					if (!NKCRepeatOperaion.CheckPossible((dungeonTempletBase.StageTemplet != null) ? dungeonTempletBase.StageTemplet.Key : 0, true))
					{
						return;
					}
				}
				else
				{
					if (NKCPhaseManager.GetStageTemplet() == null)
					{
						return;
					}
					NKC_SCEN_DUNGEON_ATK_READY scen_DUNGEON_ATK_READY2 = NKCScenManager.GetScenManager().Get_SCEN_DUNGEON_ATK_READY();
					if (scen_DUNGEON_ATK_READY2 != null && scen_DUNGEON_ATK_READY2.GetLastMultiplyRewardCount() > 1)
					{
						NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_DP_DOUBLE_OPERATION_CANNOT_REPEAT", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
						return;
					}
					if (!NKCRepeatOperaion.CheckPossible(NKCPhaseManager.PhaseModeState.stageId, true))
					{
						return;
					}
				}
			}
			else
			{
				if (!NKCRepeatOperaion.CheckPossibleForWarfare(NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetWarfareStrID(), true))
				{
					return;
				}
				if (NKCScenManager.GetScenManager().Get_NKC_SCEN_WARFARE_GAME().GetRetryData() == null)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_WARFARE_CANNOT_FIND_RETRY_DATA, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
				WarfareGameData warfareGameData = NKCScenManager.GetScenManager().WarfareGameData;
				if (warfareGameData != null && warfareGameData.rewardMultiply > 1)
				{
					NKCPopupMessageManager.AddPopupMessage(NKCStringTable.GetString("SI_DP_DOUBLE_OPERATION_CANNOT_REPEAT", false), NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
					return;
				}
			}
			gameClient.Send_Packet_GAME_PAUSE_REQ(true, false, NKC_OPEN_POPUP_TYPE_AFTER_PAUSE.NOPTATP_REPEAT_OPERATION_POPUP);
		}

		// Token: 0x04002FFA RID: 12282
		public NKCUIComStateButton m_csbtnOn;

		// Token: 0x04002FFB RID: 12283
		public NKCUIComStateButton m_csbtnOff;
	}
}

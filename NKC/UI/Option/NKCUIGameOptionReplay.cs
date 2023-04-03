using System;
using ClientPacket.Pvp;
using NKM;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Option
{
	// Token: 0x02000B94 RID: 2964
	public class NKCUIGameOptionReplay : NKCUIGameOptionContentBase
	{
		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x060088DA RID: 35034 RVA: 0x002E4BB8 File Offset: 0x002E2DB8
		private string REPLAY_LEAVE_POPUP_TITLE
		{
			get
			{
				return NKCUtilString.GET_STRING_REPLAY_OPTION_LEAVE_TITLE;
			}
		}

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x060088DB RID: 35035 RVA: 0x002E4BBF File Offset: 0x002E2DBF
		private string REPLAY_LEAVE_POPUP_DESC
		{
			get
			{
				return NKCUtilString.GET_STRING_REPLAY_OPTION_LEAVE_DESC;
			}
		}

		// Token: 0x060088DC RID: 35036 RVA: 0x002E4BC6 File Offset: 0x002E2DC6
		public override void Init()
		{
			this.m_csbtnExit.PointerClick.RemoveAllListeners();
			this.m_csbtnExit.PointerClick.AddListener(new UnityAction(this.LeaveReplay));
		}

		// Token: 0x060088DD RID: 35037 RVA: 0x002E4BF4 File Offset: 0x002E2DF4
		public override void SetContent()
		{
			NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_REPLAY_TEXT_TITLE, NKCUtilString.GET_STRING_GAUNTLET.ToUpper());
			NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_REPLAY_TEXT_SUB_TITLE, "");
			NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_REPLAY_TEXT_DESC, "");
			NKCGameClient gameClient = NKCScenManager.GetScenManager().GetGameClient();
			ReplayData currentReplayData = NKCReplayMgr.GetNKCReplaMgr().CurrentReplayData;
			if (currentReplayData != null && gameClient != null)
			{
				NKM_GAME_TYPE gameType = currentReplayData.gameData.GetGameType();
				if (gameType != NKM_GAME_TYPE.NGT_PVP_RANK)
				{
					if (gameType != NKM_GAME_TYPE.NGT_ASYNC_PVP)
					{
						switch (gameType)
						{
						case NKM_GAME_TYPE.NGT_PVP_PRIVATE:
							NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_REPLAY_TEXT_SUB_TITLE, NKCUtilString.GET_STRING_PRIVATE_PVP);
							break;
						case NKM_GAME_TYPE.NGT_PVP_LEAGUE:
							NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_REPLAY_TEXT_SUB_TITLE, NKCUtilString.GET_STRING_GAUNTLET_LEAGUE_GAME);
							break;
						case NKM_GAME_TYPE.NGT_PVP_STRATEGY:
							NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_REPLAY_TEXT_SUB_TITLE, NKCUtilString.GET_STRING_GAUNTLET_ASYNC_GAME);
							break;
						case NKM_GAME_TYPE.NGT_PVP_STRATEGY_REVENGE:
							NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_REPLAY_TEXT_SUB_TITLE, NKCUtilString.GET_STRING_GAUNTLET_ASYNC_GAME);
							break;
						case NKM_GAME_TYPE.NGT_PVP_STRATEGY_NPC:
							NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_REPLAY_TEXT_SUB_TITLE, NKCUtilString.GET_STRING_GAUNTLET_ASYNC_GAME);
							break;
						}
					}
					else
					{
						NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_REPLAY_TEXT_SUB_TITLE, NKCUtilString.GET_STRING_GAUNTLET_ASYNC_GAME);
					}
				}
				else
				{
					NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_REPLAY_TEXT_SUB_TITLE, NKCUtilString.GET_STRING_GAUNTLET_RANK_GAME);
				}
				string userNickname = NKCUtilString.GetUserNickname(currentReplayData.gameData.GetEnemyTeamData(gameClient.m_MyTeam).m_UserNickname, true);
				NKCUtil.SetLabelText(this.m_NKM_UI_GAME_OPTION_REPLAY_TEXT_DESC, NKCStringTable.GetString("SI_GAUNTLET_ASYNC_READY_VS", false) + " " + userNickname);
			}
		}

		// Token: 0x060088DE RID: 35038 RVA: 0x002E4D46 File Offset: 0x002E2F46
		public void LeaveReplay()
		{
			NKCPopupOKCancel.OpenOKCancelBox(this.REPLAY_LEAVE_POPUP_TITLE, this.REPLAY_LEAVE_POPUP_DESC, new NKCPopupOKCancel.OnButton(NKCUIGameOptionReplay.OnClickLeaveReplayOkButton), null, false);
		}

		// Token: 0x060088DF RID: 35039 RVA: 0x002E4D67 File Offset: 0x002E2F67
		private static void OnClickLeaveReplayOkButton()
		{
			if (!NKCReplayMgr.IsPlayingReplay())
			{
				return;
			}
			NKCReplayMgr.GetNKCReplaMgr().LeavePlaying();
		}

		// Token: 0x04007555 RID: 30037
		public Text m_NKM_UI_GAME_OPTION_REPLAY_TEXT_TITLE;

		// Token: 0x04007556 RID: 30038
		public Text m_NKM_UI_GAME_OPTION_REPLAY_TEXT_SUB_TITLE;

		// Token: 0x04007557 RID: 30039
		public Text m_NKM_UI_GAME_OPTION_REPLAY_TEXT_DESC;

		// Token: 0x04007558 RID: 30040
		public NKCUIComStateButton m_csbtnExit;
	}
}

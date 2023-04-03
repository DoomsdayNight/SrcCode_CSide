using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.HUD
{
	// Token: 0x02000C45 RID: 3141
	public class NKCGameHudReplay : MonoBehaviour
	{
		// Token: 0x0600927B RID: 37499 RVA: 0x00320020 File Offset: 0x0031E220
		public void InitUI(NKCGameHud hud)
		{
			this.m_NKCGameHUD = hud;
			if (this.m_NKCGameHUD == null)
			{
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetButtonClickDelegate(this.m_cbtnChangeTeam, new UnityAction(this.ChangeTeamDeck));
			NKCUtil.SetGameobjectActive(this.m_cbtnChangeTeam, false);
			if (NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.REPLAY_HIDE_NICKCNAME))
			{
				NKCUtil.SetToggleValueChangedDelegate(this.m_tglShowUserNickname, new UnityAction<bool>(this.ShowUserNickNames));
				if (this.m_tglShowUserNickname != null)
				{
					this.m_tglShowUserNickname.Select(this.m_bShowUserNickName, true, false);
				}
				NKCUtil.SetGameobjectActive(this.m_tglShowUserNickname, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_tglShowUserNickname, false);
		}

		// Token: 0x0600927C RID: 37500 RVA: 0x003200CC File Offset: 0x0031E2CC
		public void ChangeTeamDeck()
		{
			switch (this.m_currentViewTeamType)
			{
			case NKM_TEAM_TYPE.NTT_INVALID:
				this.m_currentViewTeamType = this.m_NKCGameHUD.GetGameClient().m_MyTeam;
				break;
			case NKM_TEAM_TYPE.NTT_A1:
			case NKM_TEAM_TYPE.NTT_A2:
				this.m_currentViewTeamType = NKM_TEAM_TYPE.NTT_B1;
				break;
			case NKM_TEAM_TYPE.NTT_B1:
			case NKM_TEAM_TYPE.NTT_B2:
				this.m_currentViewTeamType = NKM_TEAM_TYPE.NTT_A1;
				break;
			}
			NKMGameTeamData teamData = this.m_NKCGameHUD.GetGameClient().GetGameData().GetTeamData(this.m_currentViewTeamType);
			if (teamData != null)
			{
				this.m_NKCGameHUD.SetDeck(teamData);
				this.m_NKCGameHUD.SetAssistDeck(teamData);
				this.m_NKCGameHUD.SetShipSkillDeck(teamData.m_MainShip);
			}
		}

		// Token: 0x0600927D RID: 37501 RVA: 0x0032016C File Offset: 0x0031E36C
		public void ShowUserNickNames(bool value)
		{
			this.m_bShowUserNickName = !this.m_bShowUserNickName;
		}

		// Token: 0x04007F75 RID: 32629
		private NKM_TEAM_TYPE m_currentViewTeamType;

		// Token: 0x04007F76 RID: 32630
		public NKCUIComButton m_cbtnChangeTeam;

		// Token: 0x04007F77 RID: 32631
		public NKCUIComToggle m_tglShowUserNickname;

		// Token: 0x04007F78 RID: 32632
		private bool m_bShowUserNickName = true;

		// Token: 0x04007F79 RID: 32633
		private NKCGameHud m_NKCGameHUD;
	}
}

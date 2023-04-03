using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Guild
{
	// Token: 0x02000B57 RID: 2903
	public class NKCUIGuildLobbyTab : MonoBehaviour
	{
		// Token: 0x0600846F RID: 33903 RVA: 0x002CA8C1 File Offset: 0x002C8AC1
		public void InitUI(NKCUIGuildLobbyTab.OnToggle onToggle)
		{
			this.m_tgl.OnValueChanged.RemoveAllListeners();
			this.m_tgl.OnValueChanged.AddListener(new UnityAction<bool>(this.OnClickTab));
			this.m_dOnValueChanged = onToggle;
		}

		// Token: 0x06008470 RID: 33904 RVA: 0x002CA8F8 File Offset: 0x002C8AF8
		public void UpdateState()
		{
			switch (this.m_tabType)
			{
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.None:
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Info:
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Member:
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Manage:
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Invite:
				break;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Mission:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_MISSION, 0, 0))
				{
					this.m_tgl.Lock(false);
					return;
				}
				this.m_tgl.UnLock(false);
				return;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Point:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_POINT, 0, 0))
				{
					this.m_tgl.Lock(false);
					return;
				}
				this.m_tgl.UnLock(false);
				return;
			case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Ranking:
				if (!NKCContentManager.IsContentsUnlocked(ContentsType.GUILD_RANKING, 0, 0))
				{
					this.m_tgl.Lock(true);
					return;
				}
				this.m_tgl.UnLock(false);
				break;
			default:
				return;
			}
		}

		// Token: 0x06008471 RID: 33905 RVA: 0x002CA9A4 File Offset: 0x002C8BA4
		public void CheckRedDot()
		{
			bool bValue = false;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null)
			{
				switch (this.m_tabType)
				{
				case NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE.Mission:
				{
					NKMMissionTabTemplet missionTabTemplet = NKMMissionManager.GetMissionTabTemplet(NKM_MISSION_TYPE.GUILD);
					if (missionTabTemplet != null)
					{
						bValue = nkmuserData.m_MissionData.CheckCompletableMission(nkmuserData, missionTabTemplet.m_tabID, false);
					}
					break;
				}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objRedDot, bValue);
		}

		// Token: 0x06008472 RID: 33906 RVA: 0x002CAA0C File Offset: 0x002C8C0C
		public NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE GetTabType()
		{
			return this.m_tabType;
		}

		// Token: 0x06008473 RID: 33907 RVA: 0x002CAA14 File Offset: 0x002C8C14
		public void OnClickTab(bool bValue)
		{
			if (bValue)
			{
				this.m_dOnValueChanged(this.m_tabType, false);
			}
		}

		// Token: 0x0400709C RID: 28828
		public NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE m_tabType;

		// Token: 0x0400709D RID: 28829
		public NKCUIComToggle m_tgl;

		// Token: 0x0400709E RID: 28830
		public GameObject m_objRedDot;

		// Token: 0x0400709F RID: 28831
		public NKCUIGuildLobbyTab.OnToggle m_dOnValueChanged;

		// Token: 0x020018F1 RID: 6385
		// (Invoke) Token: 0x0600B739 RID: 46905
		public delegate void OnToggle(NKCUIGuildLobby.GUILD_LOBBY_UI_TYPE tabType, bool bForce = false);
	}
}

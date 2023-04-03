using System;
using NKC.UI.Guild;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C13 RID: 3091
	public class NKCUILobbyMenuGuild : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008F02 RID: 36610 RVA: 0x0030A2F0 File Offset: 0x003084F0
		public void Init(ContentsType contentsType = ContentsType.None)
		{
			if (this.m_csbtnMenu != null)
			{
				this.m_csbtnMenu.PointerClick.RemoveAllListeners();
				this.m_csbtnMenu.PointerClick.AddListener(new UnityAction(this.OnButton));
			}
			NKCUtil.SetLabelText(this.m_lbGuildName, string.Empty);
			NKCUtil.SetGameobjectActive(this.m_lbGuildName, false);
			NKCUtil.SetGameobjectActive(this.m_GuildBadgeUI, false);
			NKCUtil.SetGameobjectActive(this.m_objNone, false);
			this.m_ContentsType = contentsType;
		}

		// Token: 0x06008F03 RID: 36611 RVA: 0x0030A374 File Offset: 0x00308574
		protected override void ContentsUpdate(NKMUserData userData)
		{
			bool flag = NKCAlarmManager.CheckGuildNotify(userData);
			NKCUtil.SetGameobjectActive(this.m_objNotify, flag);
			this.SetNotify(flag);
			this.SetGuildName();
		}

		// Token: 0x06008F04 RID: 36612 RVA: 0x0030A3A1 File Offset: 0x003085A1
		private void OnButton()
		{
			if (this.m_bLocked)
			{
				NKCContentManager.ShowLockedMessagePopup(this.m_ContentsType, 0);
				return;
			}
			if (NKCGuildManager.HasGuild())
			{
				NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_LOBBY, true);
				return;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_GUILD_INTRO, true);
		}

		// Token: 0x06008F05 RID: 36613 RVA: 0x0030A3DC File Offset: 0x003085DC
		public void SetGuildName()
		{
			if (this.m_GuildBadgeUI != null)
			{
				NKCUtil.SetGameobjectActive(this.m_GuildBadgeUI, NKCGuildManager.HasGuild());
				if (this.m_GuildBadgeUI.gameObject.activeSelf)
				{
					this.m_GuildBadgeUI.SetData(NKCGuildManager.MyGuildData.badgeId, false);
				}
				NKCUtil.SetGameobjectActive(this.m_lbGuildName, NKCGuildManager.HasGuild());
				if (this.m_lbGuildName.gameObject.activeSelf)
				{
					NKCUtil.SetLabelText(this.m_lbGuildName, string.Format("[{0}]", NKCUtilString.GetUserGuildName(NKCGuildManager.MyGuildData.name, false)));
				}
				NKCUtil.SetGameobjectActive(this.m_objNone, !NKCGuildManager.HasGuild());
			}
		}

		// Token: 0x04007C01 RID: 31745
		public NKCUIComStateButton m_csbtnMenu;

		// Token: 0x04007C02 RID: 31746
		public GameObject m_objNotify;

		// Token: 0x04007C03 RID: 31747
		public NKCUIGuildBadge m_GuildBadgeUI;

		// Token: 0x04007C04 RID: 31748
		public Text m_lbGuildName;

		// Token: 0x04007C05 RID: 31749
		public GameObject m_objNone;
	}
}

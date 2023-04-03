using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C15 RID: 3093
	public class NKCUILobbyMenuLeaderBoard : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008F0F RID: 36623 RVA: 0x0030A764 File Offset: 0x00308964
		public void Init(ContentsType contentsType = ContentsType.None)
		{
			if (this.m_csbtnMenu != null)
			{
				this.m_csbtnMenu.PointerClick.RemoveAllListeners();
				this.m_csbtnMenu.PointerClick.AddListener(new UnityAction(this.OnButton));
			}
			this.m_ContentsType = contentsType;
		}

		// Token: 0x06008F10 RID: 36624 RVA: 0x0030A7B4 File Offset: 0x003089B4
		protected override void ContentsUpdate(NKMUserData userData)
		{
			bool flag = NKCAlarmManager.CheckleaderBoardNotify(userData);
			NKCUtil.SetGameobjectActive(this.m_objNotify, flag);
			this.SetNotify(flag);
		}

		// Token: 0x06008F11 RID: 36625 RVA: 0x0030A7DB File Offset: 0x003089DB
		protected override void SetNotify(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objNotify, value);
			base.SetNotify(value);
		}

		// Token: 0x06008F12 RID: 36626 RVA: 0x0030A7F0 File Offset: 0x003089F0
		private void OnButton()
		{
			if (this.m_bLocked)
			{
				NKCContentManager.ShowLockedMessagePopup(this.m_ContentsType, 0);
				return;
			}
			NKCUILeaderBoard.Instance.Open(null, true);
		}

		// Token: 0x04007C11 RID: 31761
		public NKCUIComStateButton m_csbtnMenu;

		// Token: 0x04007C12 RID: 31762
		public GameObject m_objNotify;
	}
}

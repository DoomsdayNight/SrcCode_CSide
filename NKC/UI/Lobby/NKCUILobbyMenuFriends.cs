using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C12 RID: 3090
	public class NKCUILobbyMenuFriends : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008EFE RID: 36606 RVA: 0x0030A210 File Offset: 0x00308410
		public void Init(ContentsType contentsType = ContentsType.None)
		{
			if (this.m_csbtnMenu != null)
			{
				this.m_csbtnMenu.PointerClick.RemoveAllListeners();
				this.m_csbtnMenu.PointerClick.AddListener(new UnityAction(this.OnButton));
			}
			this.m_ContentsType = contentsType;
		}

		// Token: 0x06008EFF RID: 36607 RVA: 0x0030A260 File Offset: 0x00308460
		protected override void ContentsUpdate(NKMUserData userData)
		{
			bool flag = NKCAlarmManager.CheckFriendNotify(userData);
			NKCUtil.SetGameobjectActive(this.m_objNotify, flag);
			NKCUtil.SetLabelText(this.m_lbFriendCount, NKCFriendManager.FriendList.Count.ToString());
			NKCUtil.SetLabelText(this.m_lbFriendReqCount, NKCFriendManager.ReceivedREQList.Count.ToString());
			this.SetNotify(flag);
		}

		// Token: 0x06008F00 RID: 36608 RVA: 0x0030A2C1 File Offset: 0x003084C1
		private void OnButton()
		{
			if (this.m_bLocked)
			{
				NKCContentManager.ShowLockedMessagePopup(this.m_ContentsType, 0);
				return;
			}
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_FRIEND, true);
		}

		// Token: 0x04007BFD RID: 31741
		public NKCUIComStateButton m_csbtnMenu;

		// Token: 0x04007BFE RID: 31742
		public GameObject m_objNotify;

		// Token: 0x04007BFF RID: 31743
		public Text m_lbFriendCount;

		// Token: 0x04007C00 RID: 31744
		public Text m_lbFriendReqCount;
	}
}

using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C16 RID: 3094
	public class NKCUILobbyMenuMail : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008F14 RID: 36628 RVA: 0x0030A81C File Offset: 0x00308A1C
		public void Init()
		{
			if (this.m_csbtnMenu != null)
			{
				this.m_csbtnMenu.PointerClick.RemoveAllListeners();
				this.m_csbtnMenu.PointerClick.AddListener(new UnityAction(this.OnButton));
			}
			NKCMailManager.dOnMailFlagChange = (NKCMailManager.OnMailFlagChange)Delegate.Combine(NKCMailManager.dOnMailFlagChange, new NKCMailManager.OnMailFlagChange(this.OnMailFlagChange));
		}

		// Token: 0x06008F15 RID: 36629 RVA: 0x0030A883 File Offset: 0x00308A83
		private void OnMailFlagChange(bool bHasNewMail)
		{
			this._UpdateData();
		}

		// Token: 0x06008F16 RID: 36630 RVA: 0x0030A88B File Offset: 0x00308A8B
		protected override void ContentsUpdate(NKMUserData userData)
		{
			this._UpdateData();
		}

		// Token: 0x06008F17 RID: 36631 RVA: 0x0030A894 File Offset: 0x00308A94
		private void _UpdateData()
		{
			int totalMailCount = NKCMailManager.GetTotalMailCount();
			bool flag = totalMailCount > 0;
			NKCUtil.SetGameobjectActive(this.m_objNotifyRoot, flag);
			if (flag)
			{
				NKCUtil.SetLabelText(this.m_lbMailCount, totalMailCount.ToString());
			}
			this.SetNotify(flag);
		}

		// Token: 0x06008F18 RID: 36632 RVA: 0x0030A8D4 File Offset: 0x00308AD4
		private void OnButton()
		{
			NKCUIMail.Instance.Open();
		}

		// Token: 0x06008F19 RID: 36633 RVA: 0x0030A8E0 File Offset: 0x00308AE0
		public override void CleanUp()
		{
			base.CleanUp();
			NKCMailManager.dOnMailFlagChange = (NKCMailManager.OnMailFlagChange)Delegate.Remove(NKCMailManager.dOnMailFlagChange, new NKCMailManager.OnMailFlagChange(this.OnMailFlagChange));
		}

		// Token: 0x04007C13 RID: 31763
		public NKCUIComStateButton m_csbtnMenu;

		// Token: 0x04007C14 RID: 31764
		public GameObject m_objNotifyRoot;

		// Token: 0x04007C15 RID: 31765
		public Text m_lbMailCount;
	}
}

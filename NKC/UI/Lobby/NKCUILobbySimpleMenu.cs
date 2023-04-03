using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C1C RID: 3100
	public class NKCUILobbySimpleMenu : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008F45 RID: 36677 RVA: 0x0030B9CC File Offset: 0x00309BCC
		public void Init(NKCUILobbySimpleMenu.DotEnableConditionFunction conditionFunc, NKCUILobbySimpleMenu.OnButton onButton, ContentsType contentsType = ContentsType.None)
		{
			this.dDotEnableConditionFunction = conditionFunc;
			this.dOnButton = onButton;
			this.m_ContentsType = contentsType;
			this.m_csbtnButton.PointerClick.RemoveAllListeners();
			this.m_csbtnButton.PointerClick.AddListener(new UnityAction(this.OnBtn));
		}

		// Token: 0x06008F46 RID: 36678 RVA: 0x0030BA1C File Offset: 0x00309C1C
		protected override void ContentsUpdate(NKMUserData userData)
		{
			bool notify = this.dDotEnableConditionFunction != null && this.dDotEnableConditionFunction(userData);
			this.SetNotify(notify);
		}

		// Token: 0x06008F47 RID: 36679 RVA: 0x0030BA48 File Offset: 0x00309C48
		protected override void UpdateLock()
		{
			this.m_bLocked = !NKCContentManager.IsContentsUnlocked(this.m_ContentsType, 0, 0);
			if (this.m_csbtnButton != null)
			{
				this.m_csbtnButton.SetLock(this.m_bLocked, false);
			}
		}

		// Token: 0x06008F48 RID: 36680 RVA: 0x0030BA80 File Offset: 0x00309C80
		protected override void SetNotify(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objReddot, value);
			base.SetNotify(value);
		}

		// Token: 0x06008F49 RID: 36681 RVA: 0x0030BA95 File Offset: 0x00309C95
		private void OnBtn()
		{
			if (this.m_bLocked)
			{
				NKCContentManager.ShowLockedMessagePopup(this.m_ContentsType, 0);
				return;
			}
			if (this.dOnButton != null)
			{
				this.dOnButton();
			}
		}

		// Token: 0x04007C5B RID: 31835
		public GameObject m_objReddot;

		// Token: 0x04007C5C RID: 31836
		public NKCUIComStateButton m_csbtnButton;

		// Token: 0x04007C5D RID: 31837
		private NKCUILobbySimpleMenu.DotEnableConditionFunction dDotEnableConditionFunction;

		// Token: 0x04007C5E RID: 31838
		private NKCUILobbySimpleMenu.OnButton dOnButton;

		// Token: 0x020019DA RID: 6618
		// (Invoke) Token: 0x0600BA5E RID: 47710
		public delegate bool DotEnableConditionFunction(NKMUserData userData);

		// Token: 0x020019DB RID: 6619
		// (Invoke) Token: 0x0600BA62 RID: 47714
		public delegate void OnButton();
	}
}

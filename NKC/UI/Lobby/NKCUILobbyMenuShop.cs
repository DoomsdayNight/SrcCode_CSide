using System;
using NKM;
using NKM.Shop;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Lobby
{
	// Token: 0x02000C19 RID: 3097
	public class NKCUILobbyMenuShop : NKCUILobbyMenuButtonBase
	{
		// Token: 0x06008F29 RID: 36649 RVA: 0x0030AE50 File Offset: 0x00309050
		public void Init(NKCUILobbyMenuShop.DotEnableConditionFunction conditionFunc, NKCUILobbyMenuShop.OnButton onButton, ContentsType contentsType = ContentsType.None)
		{
			this.dDotEnableConditionFunction = conditionFunc;
			this.dOnButton = onButton;
			this.m_ContentsType = contentsType;
			this.m_csbtnButton.PointerClick.RemoveAllListeners();
			this.m_csbtnButton.PointerClick.AddListener(new UnityAction(this.OnBtn));
		}

		// Token: 0x06008F2A RID: 36650 RVA: 0x0030AEA0 File Offset: 0x003090A0
		protected override void ContentsUpdate(NKMUserData userData)
		{
			ShopReddotType reddotType;
			int reddotCount = NKCShopManager.CheckTabReddotCount(out reddotType, "TAB_NONE", 0);
			NKCUtil.SetShopReddotImage(reddotType, this.m_objReddot, this.m_objReddot_RED, this.m_objReddot_YELLOW);
			NKCUtil.SetShopReddotLabel(reddotType, this.m_lbReddotCount, reddotCount);
		}

		// Token: 0x06008F2B RID: 36651 RVA: 0x0030AEE0 File Offset: 0x003090E0
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

		// Token: 0x04007C26 RID: 31782
		public GameObject m_objReddot;

		// Token: 0x04007C27 RID: 31783
		public GameObject m_objReddot_RED;

		// Token: 0x04007C28 RID: 31784
		public GameObject m_objReddot_YELLOW;

		// Token: 0x04007C29 RID: 31785
		public Text m_lbReddotCount;

		// Token: 0x04007C2A RID: 31786
		public NKCUIComStateButton m_csbtnButton;

		// Token: 0x04007C2B RID: 31787
		private NKCUILobbyMenuShop.DotEnableConditionFunction dDotEnableConditionFunction;

		// Token: 0x04007C2C RID: 31788
		private NKCUILobbyMenuShop.OnButton dOnButton;

		// Token: 0x020019D7 RID: 6615
		// (Invoke) Token: 0x0600BA51 RID: 47697
		public delegate bool DotEnableConditionFunction(NKMUserData userData);

		// Token: 0x020019D8 RID: 6616
		// (Invoke) Token: 0x0600BA55 RID: 47701
		public delegate void OnButton();
	}
}

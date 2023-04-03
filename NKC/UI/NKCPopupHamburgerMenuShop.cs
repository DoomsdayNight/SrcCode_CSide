using System;
using NKM;
using NKM.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A5F RID: 2655
	public class NKCPopupHamburgerMenuShop : NKCPopupHamburgerMenuSimpleButton
	{
		// Token: 0x060074CD RID: 29901 RVA: 0x0026D514 File Offset: 0x0026B714
		protected override void ContentsUpdate(NKMUserData userData)
		{
			ShopReddotType reddotType;
			int reddotCount = NKCShopManager.CheckTabReddotCount(out reddotType, "TAB_NONE", 0);
			NKCUtil.SetShopReddotImage(reddotType, this.m_objReddot, this.m_objReddot_RED, this.m_objReddot_YELLOW);
			NKCUtil.SetShopReddotLabel(reddotType, this.m_lbReddotCount, reddotCount);
		}

		// Token: 0x04006121 RID: 24865
		public GameObject m_objReddot_RED;

		// Token: 0x04006122 RID: 24866
		public GameObject m_objReddot_YELLOW;

		// Token: 0x04006123 RID: 24867
		public Text m_lbReddotCount;
	}
}

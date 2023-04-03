using System;
using NKM;
using NKM.Event;
using NKM.Shop;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Event
{
	// Token: 0x02000BD9 RID: 3033
	public class NKCUIEventSubUIAlipayPromo : NKCUIEventSubUIBase
	{
		// Token: 0x06008CA8 RID: 36008 RVA: 0x002FD62C File Offset: 0x002FB82C
		public override void Init()
		{
			base.Init();
			if (this.m_csbtnBuyPkg != null)
			{
				this.m_csbtnBuyPkg.SetLock(false, false);
				this.m_csbtnBuyPkg.PointerClick.RemoveAllListeners();
				this.m_csbtnBuyPkg.PointerClick.AddListener(new UnityAction(this.OnClickBuyPkg));
			}
			if (this.m_csbtnGetHongBao != null)
			{
				this.m_csbtnGetHongBao.SetLock(false, false);
				this.m_csbtnGetHongBao.PointerClick.RemoveAllListeners();
				this.m_csbtnGetHongBao.PointerClick.AddListener(new UnityAction(this.OnClickBuyHongbao));
			}
			if (this.m_csbtnEventHelp != null)
			{
				this.m_csbtnEventHelp.SetLock(false, false);
				this.m_csbtnEventHelp.PointerClick.RemoveAllListeners();
				this.m_csbtnEventHelp.PointerClick.AddListener(new UnityAction(this.OnClickEventHelp));
			}
		}

		// Token: 0x06008CA9 RID: 36009 RVA: 0x002FD714 File Offset: 0x002FB914
		public override void Open(NKMEventTabTemplet tabTemplet)
		{
			this.m_tabTemplet = tabTemplet;
			this.UpdateUI();
		}

		// Token: 0x06008CAA RID: 36010 RVA: 0x002FD724 File Offset: 0x002FB924
		private bool GetPossibleBuyPkg()
		{
			NKMShopData shopData = NKCScenManager.CurrentUserData().m_ShopData;
			if (shopData == null)
			{
				return false;
			}
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(160372);
			return shopItemTemplet == null || shopData.GetPurchasedCount(shopItemTemplet) <= 0;
		}

		// Token: 0x06008CAB RID: 36011 RVA: 0x002FD75C File Offset: 0x002FB95C
		private void UpdateUI()
		{
			if (this.m_csbtnBuyPkg != null)
			{
				this.m_csbtnBuyPkg.SetLock(!this.GetPossibleBuyPkg(), false);
			}
			if (this.m_csbtnGetHongBao != null)
			{
				this.m_csbtnGetHongBao.SetLock(!this.GetPossibleBuyPkg(), false);
			}
		}

		// Token: 0x06008CAC RID: 36012 RVA: 0x002FD7B0 File Offset: 0x002FB9B0
		private void OnClickBuyPkg()
		{
			if (!this.GetPossibleBuyPkg())
			{
				return;
			}
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(160372);
			if (shopItemTemplet != null)
			{
				NKCPacketSender.Send_NKMPacket_SHOP_FIX_SHOP_CASH_BUY_POSSIBLE_REQ(shopItemTemplet.m_MarketID, null);
			}
		}

		// Token: 0x06008CAD RID: 36013 RVA: 0x002FD7E0 File Offset: 0x002FB9E0
		private void OnClickBuyHongbao()
		{
			Application.OpenURL("https://render.alipay.com/p/c/18lg98p47vog/page_u748a05713109410089df61f17a7e0f54.html");
		}

		// Token: 0x06008CAE RID: 36014 RVA: 0x002FD7EC File Offset: 0x002FB9EC
		private void OnClickEventHelp()
		{
			NKCPopupEventHelp.Instance.Open(this.m_tabTemplet.m_EventID);
		}

		// Token: 0x06008CAF RID: 36015 RVA: 0x002FD803 File Offset: 0x002FBA03
		public override void Refresh()
		{
			this.UpdateUI();
		}

		// Token: 0x0400798D RID: 31117
		public NKCUIComStateButton m_csbtnBuyPkg;

		// Token: 0x0400798E RID: 31118
		public NKCUIComStateButton m_csbtnGetHongBao;

		// Token: 0x0400798F RID: 31119
		public NKCUIComStateButton m_csbtnEventHelp;

		// Token: 0x04007990 RID: 31120
		private const int ALIPAY_PROMO_PKG_PRODUCT_ID = 160372;
	}
}

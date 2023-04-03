using System;
using System.Collections.Generic;
using ClientPacket.Shop;
using NKC.Publisher;
using NKC.Templet;
using NKC.UI;
using NKC.UI.Shop;
using NKM;
using NKM.Shop;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200072C RID: 1836
	public class NKC_SCEN_SHOP : NKC_SCEN_BASIC
	{
		// Token: 0x06004915 RID: 18709 RVA: 0x001601C8 File Offset: 0x0015E3C8
		public NKC_SCEN_SHOP()
		{
			this.m_NKM_SCEN_ID = NKM_SCEN_ID.NSI_SHOP;
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x001601E3 File Offset: 0x0015E3E3
		public override void ScenLoadUIStart()
		{
			base.ScenLoadUIStart();
			if (!NKCEmoticonManager.m_bReceivedEmoticonData)
			{
				this.Send_NKMPacket_EMOTICON_DATA_REQ();
			}
			NKCShopManager.RequestShopItemList(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x00160200 File Offset: 0x0015E400
		public override void ScenLoadUIUpdate()
		{
			this.m_deltaTime += Time.deltaTime;
			if (this.m_deltaTime > 5f)
			{
				this.m_deltaTime = 0f;
				base.Set_NKC_SCEN_STATE(NKC_SCEN_STATE.NSS_FAIL);
				return;
			}
			if (NKCShopManager.IsShopItemListReady && NKCEmoticonManager.m_bReceivedEmoticonData)
			{
				this.m_deltaTime = 0f;
				base.ScenLoadUIUpdate();
			}
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x0016025F File Offset: 0x0015E45F
		public override void ScenLoadUIComplete()
		{
			base.ScenLoadUIComplete();
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x00160268 File Offset: 0x0015E468
		public override void ScenStart()
		{
			base.ScenStart();
			if (!NKCShopManager.IsShopItemListReady)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_SHOP_WAS_NOT_ABLE_TO_GET_PRODUCT_LIST_FROM_SERVER, new NKCPopupOKCancel.OnButton(this.ToHomeScene), "");
				return;
			}
			if (!NKCEmoticonManager.m_bReceivedEmoticonData)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_SHOP_WAS_NOT_ABLE_TO_GET_PRODUCT_LIST_FROM_SERVER, new NKCPopupOKCancel.OnButton(this.ToHomeScene), "");
				return;
			}
			NKCPublisherModule.InAppPurchase.BillingRestore(new NKCPublisherModule.OnComplete(NKCShopManager.OnBillingRestore));
			if (this.m_bReservedOpenTab != "TAB_NONE")
			{
				NKCShopCategoryTemplet categoryFromTab = NKCShopManager.GetCategoryFromTab(ShopTabTemplet.Find(this.m_bReservedOpenTab, this.m_ReservedOpenIndex).TabType);
				NKCShopManager.ShopTabCategory category = (categoryFromTab != null) ? categoryFromTab.m_eCategory : NKCShopManager.ShopTabCategory.PACKAGE;
				NKCUIShop.Instance.Open(category, this.m_bReservedOpenTab, this.m_ReservedOpenIndex, this.m_ReservedOpenProductID, NKCUIShop.eTabMode.Fold);
				this.m_bReservedOpenTab = "TAB_NONE";
				this.m_ReservedOpenIndex = 0;
				this.m_ReservedOpenProductID = 0;
			}
			else
			{
				NKCUIShop.Instance.Open(NKCShopManager.ShopTabCategory.PACKAGE, "TAB_MAIN", 0, 0, NKCUIShop.eTabMode.Fold);
			}
			NKCCamera.EnableBloom(false);
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x0016036E File Offset: 0x0015E56E
		public override void ScenEnd()
		{
			base.ScenEnd();
			NKCUIShop.CheckInstanceAndClose();
			this.UnloadUI();
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x00160381 File Offset: 0x0015E581
		public override void UnloadUI()
		{
			base.UnloadUI();
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x00160389 File Offset: 0x0015E589
		public override void ScenUpdate()
		{
			base.ScenUpdate();
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x00160391 File Offset: 0x0015E591
		public override bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x00160394 File Offset: 0x0015E594
		private void Send_NKMPacket_EMOTICON_DATA_REQ()
		{
			NKCPacketSender.Send_NKMPacket_EMOTICON_DATA_REQ(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID);
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x0016039C File Offset: 0x0015E59C
		public void OnRecvProductBuyCheck(string productMarketID, List<int> lstSelection)
		{
			NKC_SCEN_SHOP.<>c__DisplayClass12_0 CS$<>8__locals1 = new NKC_SCEN_SHOP.<>c__DisplayClass12_0();
			CS$<>8__locals1.productMarketID = productMarketID;
			CS$<>8__locals1.lstSelection = lstSelection;
			CS$<>8__locals1.productTemplet = NKCShopManager.GetShopTempletByMarketID(CS$<>8__locals1.productMarketID);
			if (!NKMOpenTagManager.IsSystemOpened(SystemOpenTagType.IGNORE_BILLING_RESTORE) && (NKCDefineManager.DEFINE_NXTOY() || NKCDefineManager.DEFINE_NXTOY_JP()))
			{
				NKCPublisherModule.InAppPurchase.BillingRestore(new NKCPublisherModule.OnComplete(CS$<>8__locals1.<OnRecvProductBuyCheck>g__BillingRestoreComplete|1));
				return;
			}
			CS$<>8__locals1.<OnRecvProductBuyCheck>g__InAppPurchase|0();
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x00160404 File Offset: 0x0015E604
		public void Send_NKMPacket_SHOP_RANDOM_SHOP_BUY_REQ(int slotIndex)
		{
			NKMPacket_SHOP_RANDOM_SHOP_BUY_REQ nkmpacket_SHOP_RANDOM_SHOP_BUY_REQ = new NKMPacket_SHOP_RANDOM_SHOP_BUY_REQ();
			nkmpacket_SHOP_RANDOM_SHOP_BUY_REQ.slotIndex = slotIndex;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHOP_RANDOM_SHOP_BUY_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x00160434 File Offset: 0x0015E634
		public void Send_NKMPacket_SHOP_REFRESH_REQ(bool bUseCash)
		{
			NKMPacket_SHOP_REFRESH_REQ nkmpacket_SHOP_REFRESH_REQ = new NKMPacket_SHOP_REFRESH_REQ();
			nkmpacket_SHOP_REFRESH_REQ.isUseCash = bUseCash;
			NKCScenManager.GetScenManager().GetConnectGame().Send(nkmpacket_SHOP_REFRESH_REQ, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x00160464 File Offset: 0x0015E664
		public void Send_NKMPacket_SHOP_CHAIN_TAB_RESET_TIME_REQ()
		{
			NKMPacket_SHOP_CHAIN_TAB_RESET_TIME_REQ packet = new NKMPacket_SHOP_CHAIN_TAB_RESET_TIME_REQ();
			NKCScenManager.GetScenManager().GetConnectGame().Send(packet, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, true);
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x0016048A File Offset: 0x0015E68A
		public void ToHomeScene()
		{
			NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_HOME, false);
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x00160498 File Offset: 0x0015E698
		public void SetReservedOpenTab(string shopType, int tabIndex = 0, int productID = 0)
		{
			this.m_bReservedOpenTab = shopType;
			this.m_ReservedOpenIndex = tabIndex;
			this.m_ReservedOpenProductID = productID;
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x001604B0 File Offset: 0x0015E6B0
		public void MoveToReservedTab()
		{
			if (this.m_bReservedOpenTab != "TAB_NONE")
			{
				NKCUIShop.ShopShortcut(this.m_bReservedOpenTab, this.m_ReservedOpenIndex, this.m_ReservedOpenProductID);
				this.m_bReservedOpenTab = "TAB_NONE";
				this.m_ReservedOpenIndex = 0;
				this.m_ReservedOpenProductID = 0;
			}
		}

		// Token: 0x04003880 RID: 14464
		private const float FIVE_SECONDS = 5f;

		// Token: 0x04003881 RID: 14465
		private float m_deltaTime;

		// Token: 0x04003882 RID: 14466
		private string m_bReservedOpenTab = "TAB_NONE";

		// Token: 0x04003883 RID: 14467
		private int m_ReservedOpenIndex;

		// Token: 0x04003884 RID: 14468
		private int m_ReservedOpenProductID;
	}
}

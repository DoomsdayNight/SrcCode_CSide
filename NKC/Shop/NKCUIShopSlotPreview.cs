using System;
using NKM;
using NKM.Shop;
using UnityEngine;

namespace NKC.UI.Shop
{
	// Token: 0x02000AE7 RID: 2791
	public class NKCUIShopSlotPreview : NKCUIShopSlotCard
	{
		// Token: 0x06007D50 RID: 32080 RVA: 0x002A0062 File Offset: 0x0029E262
		protected override bool IsProductAvailable(ShopItemTemplet shopTemplet, out bool bAdmin, bool bIncludeLockedItemWithReason)
		{
			bAdmin = false;
			return true;
		}

		// Token: 0x06007D51 RID: 32081 RVA: 0x002A0068 File Offset: 0x0029E268
		protected override void SetGoodsImage(ShopItemTemplet shopTemplet, bool bFirstBuy)
		{
			string assetName = string.IsNullOrEmpty(this.m_OverrideImageAsset) ? shopTemplet.m_CardImage : this.m_OverrideImageAsset;
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("AB_UI_NKM_UI_SHOP_IMG", assetName));
			NKCUtil.SetGameobjectActive(this.m_imgItem, orLoadAssetResource != null);
			NKCUtil.SetGameobjectActive(this.m_Slot, orLoadAssetResource == null);
			if (orLoadAssetResource != null)
			{
				this.m_imgItem.sprite = orLoadAssetResource;
			}
			else
			{
				NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeShopItemData(shopTemplet, bFirstBuy);
				bool bShowNumber = slotData.eType == NKCUISlot.eSlotMode.ItemMisc || slotData.eType == NKCUISlot.eSlotMode.Mold;
				this.m_Slot.SetData(slotData, false, bShowNumber, false, new NKCUISlot.OnClick(this.OnSlotClick));
			}
			if (this.m_lbDescription != null)
			{
				this.m_lbDescription.text = NKCUtilString.GetShopDescriptionText(shopTemplet.GetItemDesc(), bFirstBuy);
			}
		}

		// Token: 0x06007D52 RID: 32082 RVA: 0x002A013B File Offset: 0x0029E33B
		private void OnSlotClick(NKCUISlot.SlotData slotData, bool bLocked)
		{
			base.OnBtnBuy();
		}

		// Token: 0x04006A15 RID: 27157
		[Header("슬롯")]
		public NKCUISlot m_Slot;
	}
}

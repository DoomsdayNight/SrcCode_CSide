using System;
using NKM;
using NKM.Shop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000945 RID: 2373
	public class NKCUIComShopItem : MonoBehaviour
	{
		// Token: 0x06005EC7 RID: 24263 RVA: 0x001D6DA3 File Offset: 0x001D4FA3
		private void OnEnable()
		{
			this.SetData();
		}

		// Token: 0x06005EC8 RID: 24264 RVA: 0x001D6DAC File Offset: 0x001D4FAC
		public void SetData()
		{
			this.m_bUpdateTime = false;
			this.m_fDeltaTime = 0f;
			this.m_btnShopPackageItem.PointerClick.RemoveAllListeners();
			this.m_btnShopPackageItem.PointerClick.AddListener(new UnityAction(this.OnClickShopItem));
			ShopItemTemplet shopItemTemplet = ShopItemTemplet.Find(this.m_ShopPackageItemId);
			if (shopItemTemplet != null)
			{
				NKMItemMiscTemplet nkmitemMiscTemplet = NKMItemMiscTemplet.Find(shopItemTemplet.m_PriceItemID);
				if (nkmitemMiscTemplet == null)
				{
					return;
				}
				NKCUtil.SetLabelText(this.m_lbShopItemName, shopItemTemplet.GetItemName());
				if (this.m_sprShopItemIcon != null)
				{
					NKCUtil.SetImageSprite(this.m_imgShopItemIcon, this.m_sprShopItemIcon, false);
					NKCUtil.SetImageSprite(this.m_imgShopItemIconOff, this.m_sprShopItemIcon, false);
				}
				NKCUtil.SetGameobjectActive(base.gameObject, true);
				if (shopItemTemplet.m_bEnabled && shopItemTemplet.EnableByTag && shopItemTemplet.ItemEnableByTag)
				{
					NKCUtil.SetImageSprite(this.m_imgShopPackageItemCost, NKCResourceUtility.GetOrLoadMiscItemIcon(nkmitemMiscTemplet), false);
					NKCUtil.SetLabelText(this.m_lbShopPackageItemCost, shopItemTemplet.m_Price.ToString());
					if (NKCShopManager.GetBuyCountLeft(this.m_ShopPackageItemId) > 0)
					{
						this.m_btnShopPackageItem.UnLock(false);
						if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(shopItemTemplet.m_PriceItemID) >= (long)shopItemTemplet.m_Price)
						{
							NKCUtil.SetLabelTextColor(this.m_lbShopPackageItemCost, Color.white);
							return;
						}
						NKCUtil.SetLabelTextColor(this.m_lbShopPackageItemCost, Color.red);
						return;
					}
					else
					{
						NKMShopData shopData = NKCScenManager.CurrentUserData().m_ShopData;
						if (shopData.histories.ContainsKey(this.m_ShopPackageItemId) && !NKCSynchronizedTime.IsFinished(shopData.histories[this.m_ShopPackageItemId].nextResetDate))
						{
							NKCUtil.SetLabelText(this.m_ShopPackageRemainTime, NKCSynchronizedTime.GetTimeLeftString(shopData.histories[this.m_ShopPackageItemId].nextResetDate));
							this.m_ShopPackageNextResetTime = new DateTime(shopData.histories[this.m_ShopPackageItemId].nextResetDate);
							this.m_btnShopPackageItem.Lock(false);
							this.m_bUpdateTime = true;
							this.UpdateShopPackageRemainTime();
							return;
						}
						NKCUtil.SetGameobjectActive(base.gameObject, false);
					}
				}
			}
		}

		// Token: 0x06005EC9 RID: 24265 RVA: 0x001D6FB5 File Offset: 0x001D51B5
		private void UpdateShopPackageRemainTime()
		{
			NKCUtil.SetLabelText(this.m_ShopPackageRemainTime, NKCSynchronizedTime.GetTimeLeftString(this.m_ShopPackageNextResetTime));
			if (NKCSynchronizedTime.IsFinished(this.m_ShopPackageNextResetTime))
			{
				this.m_bUpdateTime = false;
				this.SetData();
			}
		}

		// Token: 0x06005ECA RID: 24266 RVA: 0x001D6FE8 File Offset: 0x001D51E8
		private void Update()
		{
			this.m_fDeltaTime += Time.deltaTime;
			if (this.m_fDeltaTime > 1f)
			{
				this.m_fDeltaTime -= 1f;
				if (this.m_bUpdateTime && this.m_btnShopPackageItem != null && this.m_btnShopPackageItem.m_bLock)
				{
					this.UpdateShopPackageRemainTime();
				}
			}
		}

		// Token: 0x06005ECB RID: 24267 RVA: 0x001D704F File Offset: 0x001D524F
		private void OnClickShopItem()
		{
			NKCShopManager.OnBtnProductBuy(this.m_ShopPackageItemId, false);
		}

		// Token: 0x06005ECC RID: 24268 RVA: 0x001D705E File Offset: 0x001D525E
		public void Refresh()
		{
			this.SetData();
		}

		// Token: 0x04004AE0 RID: 19168
		public NKCUIComStateButton m_btnShopPackageItem;

		// Token: 0x04004AE1 RID: 19169
		public Image m_imgShopItemIcon;

		// Token: 0x04004AE2 RID: 19170
		public Image m_imgShopItemIconOff;

		// Token: 0x04004AE3 RID: 19171
		public Text m_lbShopItemName;

		// Token: 0x04004AE4 RID: 19172
		public Image m_imgShopPackageItemCost;

		// Token: 0x04004AE5 RID: 19173
		public Text m_lbShopPackageItemCost;

		// Token: 0x04004AE6 RID: 19174
		public Text m_ShopPackageRemainTime;

		// Token: 0x04004AE7 RID: 19175
		[Header("실제 사용하는 프리팹에서 따로 세팅")]
		public int m_ShopPackageItemId;

		// Token: 0x04004AE8 RID: 19176
		public Sprite m_sprShopItemIcon;

		// Token: 0x04004AE9 RID: 19177
		private DateTime m_ShopPackageNextResetTime;

		// Token: 0x04004AEA RID: 19178
		private bool m_bUpdateTime;

		// Token: 0x04004AEB RID: 19179
		private float m_fDeltaTime;
	}
}

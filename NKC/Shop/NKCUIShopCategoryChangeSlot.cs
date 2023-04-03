using System;
using NKC.Templet;
using NKM.Shop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000ADA RID: 2778
	public class NKCUIShopCategoryChangeSlot : MonoBehaviour
	{
		// Token: 0x06007CB5 RID: 31925 RVA: 0x0029BEEC File Offset: 0x0029A0EC
		public void Init(NKCUIShopCategoryChangeSlot.OnSelectCategory onSelectCategory)
		{
			NKCUtil.SetButtonClickDelegate(this.m_Button, new UnityAction(this.OnButton));
			NKCShopCategoryTemplet data = NKCShopCategoryTemplet.Find(this.m_eCategory);
			this.dOnSelectCategory = onSelectCategory;
			this.SetData(data);
		}

		// Token: 0x06007CB6 RID: 31926 RVA: 0x0029BF2C File Offset: 0x0029A12C
		public void SetData(NKCShopCategoryTemplet templet)
		{
			if (templet == null)
			{
				NKCUtil.SetGameobjectActive(this, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this, true);
			NKCUtil.SetImageSprite(this.m_Image, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_shop_thumbnail", templet.m_ThumbnailImg, true), false);
			NKCUtil.SetLabelText(this.m_lbName, NKCStringTable.GetString(templet.m_TabCategoryName, false));
			this.CheckReddot();
		}

		// Token: 0x06007CB7 RID: 31927 RVA: 0x0029BF85 File Offset: 0x0029A185
		private void OnButton()
		{
			NKCUIShopCategoryChangeSlot.OnSelectCategory onSelectCategory = this.dOnSelectCategory;
			if (onSelectCategory == null)
			{
				return;
			}
			onSelectCategory(this.m_eCategory);
		}

		// Token: 0x06007CB8 RID: 31928 RVA: 0x0029BFA0 File Offset: 0x0029A1A0
		public void CheckReddot()
		{
			int num = 0;
			ShopReddotType shopReddotType = ShopReddotType.NONE;
			NKCShopCategoryTemplet nkcshopCategoryTemplet = NKCShopCategoryTemplet.Find(this.m_eCategory);
			for (int i = 0; i < nkcshopCategoryTemplet.m_UseTabID.Count; i++)
			{
				ShopReddotType shopReddotType2;
				num += NKCShopManager.CheckTabReddotCount(out shopReddotType2, nkcshopCategoryTemplet.m_UseTabID[i], 0);
				if (shopReddotType < shopReddotType2)
				{
					shopReddotType = shopReddotType2;
				}
			}
			NKCUtil.SetShopReddotImage(shopReddotType, this.m_objReddotRoot, this.m_objReddot_RED, this.m_objReddot_YELLOW);
			NKCUtil.SetShopReddotLabel(shopReddotType, this.m_lbReddotCount, num);
		}

		// Token: 0x0400696A RID: 26986
		public NKCShopManager.ShopTabCategory m_eCategory;

		// Token: 0x0400696B RID: 26987
		public NKCUIComStateButton m_Button;

		// Token: 0x0400696C RID: 26988
		public Image m_Image;

		// Token: 0x0400696D RID: 26989
		public Text m_lbName;

		// Token: 0x0400696E RID: 26990
		public GameObject m_objReddotRoot;

		// Token: 0x0400696F RID: 26991
		public GameObject m_objReddot_RED;

		// Token: 0x04006970 RID: 26992
		public GameObject m_objReddot_YELLOW;

		// Token: 0x04006971 RID: 26993
		public Text m_lbReddotCount;

		// Token: 0x04006972 RID: 26994
		private NKCUIShopCategoryChangeSlot.OnSelectCategory dOnSelectCategory;

		// Token: 0x02001851 RID: 6225
		// (Invoke) Token: 0x0600B5A3 RID: 46499
		public delegate void OnSelectCategory(NKCShopManager.ShopTabCategory category);
	}
}

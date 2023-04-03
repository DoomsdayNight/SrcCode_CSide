using System;
using System.Collections;
using NKM.Shop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000AEB RID: 2795
	public class NKCUIShopTabSlot : MonoBehaviour
	{
		// Token: 0x06007D75 RID: 32117 RVA: 0x002A0E34 File Offset: 0x0029F034
		public void SetData(ShopTabTemplet shopTabTemplet, NKCUIShopTabSlot.OnClicked clicked)
		{
			NKCUtil.SetLabelText(this.m_lbTitle, shopTabTemplet.GetTabName());
			this.m_tabTemplet = shopTabTemplet;
			this.m_bHasProduct = (NKCShopManager.GetItemTempletListByTab(shopTabTemplet, true).Count > 0);
			this.m_bTabSoldOut = NKCShopManager.IsTabSoldOut(shopTabTemplet);
			if (this.HideTabRequired())
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			if (this.m_LayoutElement != null)
			{
				this.m_fMaxHeight = this.m_LayoutElement.preferredHeight;
				this.m_LayoutElement.preferredHeight = 0f;
			}
			this.dOnClicked = clicked;
			NKCUtil.SetGameobjectActive(this.m_imgSelected, false);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUtil.SetBindFunction(this.m_btnContent, new UnityAction(this.OnBtnClick));
			if (!string.IsNullOrEmpty(shopTabTemplet.m_SpecialColorCode))
			{
				if (!shopTabTemplet.m_SpecialColorCode.Contains("#"))
				{
					shopTabTemplet.m_SpecialColorCode = "#" + shopTabTemplet.m_SpecialColorCode;
				}
				NKCUtil.SetImageColor(this.m_imgSelected, NKCUtil.GetColor(shopTabTemplet.m_SpecialColorCode));
				NKCUtil.SetImageColor(this.m_imgSpecialBG, NKCUtil.GetColor(shopTabTemplet.m_SpecialColorCode));
				NKCUtil.SetGameobjectActive(this.m_imgSpecialBG, true);
				return;
			}
			NKCUtil.SetImageColor(this.m_imgSelected, this.BASE_SELECTED_COLOR);
			NKCUtil.SetGameobjectActive(this.m_imgSpecialBG, false);
		}

		// Token: 0x06007D76 RID: 32118 RVA: 0x002A0F80 File Offset: 0x0029F180
		public void SetRedDot(ShopTabTemplet tabTemplet)
		{
			ShopReddotType reddotType;
			int reddotCount = NKCShopManager.CheckTabReddotCount(out reddotType, tabTemplet.TabType, tabTemplet.SubIndex);
			NKCUtil.SetShopReddotImage(reddotType, this.m_objRedDot, this.m_objReddot_RED, this.m_objReddot_YELLOW);
			NKCUtil.SetShopReddotLabel(reddotType, this.m_lbReddotCount, reddotCount);
		}

		// Token: 0x06007D77 RID: 32119 RVA: 0x002A0FC6 File Offset: 0x0029F1C6
		public bool HideTabRequired()
		{
			return this.m_tabTemplet == null || (this.m_tabTemplet.m_HideWhenSoldOut && (!this.m_bHasProduct || (this.m_bTabSoldOut && !NKCUtil.IsUsingSuperUserFunction())));
		}

		// Token: 0x06007D78 RID: 32120 RVA: 0x002A0FFD File Offset: 0x0029F1FD
		public bool HasProduct()
		{
			return this.m_bHasProduct;
		}

		// Token: 0x06007D79 RID: 32121 RVA: 0x002A1005 File Offset: 0x0029F205
		public bool TabSoldOut()
		{
			return this.m_bTabSoldOut;
		}

		// Token: 0x06007D7A RID: 32122 RVA: 0x002A1010 File Offset: 0x0029F210
		public void OnActive(bool Active, bool bAnimate)
		{
			if (this.HideTabRequired())
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			Color color = NKCUtil.GetColor("#FFFFFF");
			if (!Active)
			{
				color.a = 0.6f;
			}
			Active &= (this.m_tabTemplet != null && NKCSynchronizedTime.IsEventTime(this.m_tabTemplet.EventDateStartUtc, this.m_tabTemplet.EventDateEndUtc));
			NKCUtil.SetLabelTextColor(this.m_lbTitle, color);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.StopAllCoroutines();
			if (bAnimate && base.gameObject.activeInHierarchy)
			{
				base.StartCoroutine(this.ActiveButton(Active));
				return;
			}
			if (Active)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, true);
				this.m_LayoutElement.preferredHeight = this.m_fMaxHeight;
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_LayoutElement.preferredHeight = 0f;
		}

		// Token: 0x06007D7B RID: 32123 RVA: 0x002A10F0 File Offset: 0x0029F2F0
		public void OnBtnClick()
		{
			if (this.dOnClicked != null)
			{
				this.dOnClicked(this.m_tabTemplet.TabType, this.m_tabTemplet.SubIndex);
			}
		}

		// Token: 0x06007D7C RID: 32124 RVA: 0x002A111C File Offset: 0x0029F31C
		public bool OnSelected(string tabType, int subTabIndex)
		{
			bool flag = tabType == this.m_tabTemplet.TabType && subTabIndex == this.m_tabTemplet.SubIndex;
			this.m_btnContent.Select(flag, true, false);
			return flag;
		}

		// Token: 0x06007D7D RID: 32125 RVA: 0x002A115E File Offset: 0x0029F35E
		private IEnumerator ActiveButton(bool bActive)
		{
			if (bActive)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, true);
				while (this.m_LayoutElement.preferredHeight < this.m_fMaxHeight)
				{
					this.m_LayoutElement.preferredHeight += this.fActiveSpeed;
					yield return new WaitForEndOfFrame();
				}
			}
			else
			{
				while (this.m_LayoutElement.preferredHeight > 0f)
				{
					this.m_LayoutElement.preferredHeight -= this.fActiveSpeed;
					yield return new WaitForEndOfFrame();
				}
				NKCUtil.SetGameobjectActive(base.gameObject, false);
			}
			this.m_LayoutElement.preferredHeight = Mathf.Clamp(this.m_LayoutElement.preferredHeight, 0f, this.m_fMaxHeight);
			yield return null;
			yield break;
		}

		// Token: 0x06007D7E RID: 32126 RVA: 0x002A1174 File Offset: 0x0029F374
		public int GetSubIndex()
		{
			return this.m_tabTemplet.SubIndex;
		}

		// Token: 0x04006A5F RID: 27231
		public Text m_lbTitle;

		// Token: 0x04006A60 RID: 27232
		public Image m_imgSelected;

		// Token: 0x04006A61 RID: 27233
		public Image m_imgSpecialBG;

		// Token: 0x04006A62 RID: 27234
		public NKCUIComStateButton m_btnContent;

		// Token: 0x04006A63 RID: 27235
		public GameObject m_objRedDot;

		// Token: 0x04006A64 RID: 27236
		public GameObject m_objReddot_RED;

		// Token: 0x04006A65 RID: 27237
		public GameObject m_objReddot_YELLOW;

		// Token: 0x04006A66 RID: 27238
		public Text m_lbReddotCount;

		// Token: 0x04006A67 RID: 27239
		[Header("")]
		public LayoutElement m_LayoutElement;

		// Token: 0x04006A68 RID: 27240
		public float fActiveSpeed = 10f;

		// Token: 0x04006A69 RID: 27241
		private float m_fMaxHeight;

		// Token: 0x04006A6A RID: 27242
		public Color BASE_SELECTED_COLOR;

		// Token: 0x04006A6B RID: 27243
		private NKCUIShopTabSlot.OnClicked dOnClicked;

		// Token: 0x04006A6C RID: 27244
		private ShopTabTemplet m_tabTemplet;

		// Token: 0x04006A6D RID: 27245
		private bool m_bHasProduct = true;

		// Token: 0x04006A6E RID: 27246
		private bool m_bTabSoldOut;

		// Token: 0x0200185F RID: 6239
		// (Invoke) Token: 0x0600B5C3 RID: 46531
		public delegate void OnClicked(string tabType, int subTabIndex = 0);
	}
}

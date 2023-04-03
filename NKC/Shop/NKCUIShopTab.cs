using System;
using System.Collections.Generic;
using NKC.Templet;
using NKM.Shop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000AEA RID: 2794
	public class NKCUIShopTab : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x170014C5 RID: 5317
		// (get) Token: 0x06007D63 RID: 32099 RVA: 0x002A0774 File Offset: 0x0029E974
		private string m_eType
		{
			get
			{
				return this.m_tabTemplet.TabType;
			}
		}

		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x06007D64 RID: 32100 RVA: 0x002A0781 File Offset: 0x0029E981
		private int m_subIndex
		{
			get
			{
				return this.m_tabTemplet.SubIndex;
			}
		}

		// Token: 0x06007D65 RID: 32101 RVA: 0x002A0790 File Offset: 0x0029E990
		public void SetData(ShopTabTemplet tabTemplet, NKCUIShopTab.OnTabSelected onTabSelected, NKCUIComToggleGroup toggleGroup)
		{
			this.m_tabTemplet = tabTemplet;
			this.dOnTabSelected = onTabSelected;
			this.m_ctglTab.OnValueChanged.RemoveAllListeners();
			this.m_ctglTab.OnValueChanged.AddListener(new UnityAction<bool>(this.OnToggle));
			this.m_ctglTab.SetToggleGroup(toggleGroup);
			NKCUtil.SetImageSprite(this.m_imgIconUnSelected, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_shop_sprite", tabTemplet.m_TabImageSelect, false), false);
			NKCUtil.SetLabelText(this.m_lbTabName, tabTemplet.GetTabName());
			NKCUtil.SetLabelText(this.m_lbTabNameUnSelected, tabTemplet.GetTabName());
			this.SetRibbon(tabTemplet.m_TagImage);
			if (!string.IsNullOrEmpty(tabTemplet.m_ImgBGSelected) && !string.IsNullOrEmpty(tabTemplet.m_ImgBGUnSelected))
			{
				NKCUtil.SetImageSprite(this.m_imgBG, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_shop_sprite", tabTemplet.m_ImgBGSelected, false), false);
				NKCUtil.SetImageSprite(this.m_imgBGUnSelected, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_shop_sprite", tabTemplet.m_ImgBGUnSelected, false), false);
				if (this.m_imgBG != null && this.m_imgBGUnSelected != null)
				{
					NKCUtil.SetLabelTextColor(this.m_lbTabName, Color.white);
				}
				NKCUtil.SetGameobjectActive(this.m_imgIcon, false);
			}
			else
			{
				NKCUtil.SetImageSprite(this.m_imgBG, this.m_sprBaseBGSelected, false);
				NKCUtil.SetImageSprite(this.m_imgBGUnSelected, this.m_sprBaseBGUnSelected, false);
				NKCUtil.SetImageSprite(this.m_imgIcon, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_shop_sprite", tabTemplet.m_TabImageSelect, false), false);
				NKCUtil.SetGameobjectActive(this.m_imgIcon, true);
				if (this.m_imgBG != null && this.m_imgBGUnSelected != null)
				{
					NKCUtil.SetLabelTextColor(this.m_lbTabName, this.BASE_COLOR_SELECTED_TEXT);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objRemainTimeSelected, tabTemplet.HasDateLimit);
			NKCUtil.SetGameobjectActive(this.m_objRemainTimeUnSelected, tabTemplet.HasDateLimit);
			if (tabTemplet.HasDateLimit)
			{
				this.m_tEndTimeUtc = tabTemplet.EventDateEndUtc;
				this.SetTimeText(this.m_tEndTimeUtc);
				return;
			}
			this.m_tEndTimeUtc = DateTime.MinValue;
		}

		// Token: 0x06007D66 RID: 32102 RVA: 0x002A0988 File Offset: 0x0029EB88
		public void Clear()
		{
			foreach (NKCUIShopTabSlot nkcuishopTabSlot in this.m_lstSubSlot)
			{
				UnityEngine.Object.Destroy(nkcuishopTabSlot.gameObject);
			}
			this.m_lstSubSlot.Clear();
		}

		// Token: 0x06007D67 RID: 32103 RVA: 0x002A09E8 File Offset: 0x0029EBE8
		private void OnToggle(bool bChecked)
		{
			if (bChecked && this.dOnTabSelected != null)
			{
				this.dOnTabSelected(this.m_eType, this.m_subIndex);
			}
		}

		// Token: 0x06007D68 RID: 32104 RVA: 0x002A0A0C File Offset: 0x0029EC0C
		public void SetRedDot()
		{
			ShopTabTemplet shopTabTemplet = ShopTabTemplet.Find(this.m_eType, 0);
			ShopReddotType reddotType;
			int reddotCount = NKCShopManager.CheckTabReddotCount(out reddotType, shopTabTemplet.TabType, 0);
			NKCUtil.SetShopReddotImage(reddotType, this.m_objRedDot, this.m_objReddot_RED, this.m_objReddot_YELLOW);
			NKCUtil.SetShopReddotLabel(reddotType, this.m_lbReddotCount, reddotCount);
			for (int i = 0; i < this.m_lstSubSlot.Count; i++)
			{
				if (shopTabTemplet != null)
				{
					ShopTabTemplet redDot = ShopTabTemplet.Find(shopTabTemplet.TabType, this.m_lstSubSlot[i].GetSubIndex());
					this.m_lstSubSlot[i].SetRedDot(redDot);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstSubSlot[i].m_objRedDot, false);
				}
			}
		}

		// Token: 0x06007D69 RID: 32105 RVA: 0x002A0ABD File Offset: 0x0029ECBD
		public void Toggle(bool bChecked)
		{
			this.m_ctglTab.Select(bChecked, true, false);
		}

		// Token: 0x06007D6A RID: 32106 RVA: 0x002A0ACE File Offset: 0x0029ECCE
		private void SetTimeText(DateTime endDateUtc)
		{
			NKCUtil.SetLabelText(this.m_lbRemainTimeSelected, string.Format(NKCUtilString.GET_STRING_REMAIN_TIME_LEFT_ONE_PARAM, NKCUtilString.GetRemainTimeString(endDateUtc, 1)));
			NKCUtil.SetLabelText(this.m_lbRemainTimeUnSelected, string.Format(NKCUtilString.GET_STRING_REMAIN_TIME_LEFT_ONE_PARAM, NKCUtilString.GetRemainTimeString(endDateUtc, 1)));
		}

		// Token: 0x06007D6B RID: 32107 RVA: 0x002A0B08 File Offset: 0x0029ED08
		private void Update()
		{
			if (this.m_tEndTimeUtc > DateTime.MinValue)
			{
				this.m_tDeltaTime += Time.deltaTime;
				if (this.m_tDeltaTime > 1f)
				{
					this.SetTimeText(this.m_tEndTimeUtc);
					this.m_tDeltaTime = 0f;
				}
			}
		}

		// Token: 0x06007D6C RID: 32108 RVA: 0x002A0B5D File Offset: 0x0029ED5D
		public void OnPointerClick(PointerEventData eventData)
		{
			this.ChangeSubTabState();
		}

		// Token: 0x06007D6D RID: 32109 RVA: 0x002A0B65 File Offset: 0x0029ED65
		public void AddSubSlot(NKCUIShopTabSlot subSlot)
		{
			this.m_lstSubSlot.Add(subSlot);
		}

		// Token: 0x06007D6E RID: 32110 RVA: 0x002A0B74 File Offset: 0x0029ED74
		public GameObject GetSubSlotObject(int subTabIndex)
		{
			if (subTabIndex == 0)
			{
				return base.gameObject;
			}
			for (int i = 0; i < this.m_lstSubSlot.Count; i++)
			{
				if (this.m_lstSubSlot[i].GetSubIndex() == subTabIndex)
				{
					return this.m_lstSubSlot[i].gameObject;
				}
			}
			return null;
		}

		// Token: 0x06007D6F RID: 32111 RVA: 0x002A0BC8 File Offset: 0x0029EDC8
		public int GetSubSlotCount()
		{
			return this.m_lstSubSlot.Count;
		}

		// Token: 0x06007D70 RID: 32112 RVA: 0x002A0BD8 File Offset: 0x0029EDD8
		public bool HideTabRequired()
		{
			if (!this.m_tabTemplet.m_HideWhenSoldOut)
			{
				return false;
			}
			if (this.GetSubSlotCount() > 0)
			{
				for (int i = 0; i < this.m_lstSubSlot.Count; i++)
				{
					if (!this.m_lstSubSlot[i].HideTabRequired())
					{
						return false;
					}
				}
				return true;
			}
			if (!string.IsNullOrEmpty(this.m_tabTemplet.m_PackageGroupID))
			{
				List<NKCShopFeaturedTemplet> featuredList = NKCShopManager.GetFeaturedList(NKCScenManager.CurrentUserData(), this.m_tabTemplet.m_PackageGroupID, false);
				return featuredList == null || featuredList.Count == 0;
			}
			return NKCShopManager.GetItemTempletListByTab(this.m_tabTemplet, true).Count <= 0 || (!NKCUtil.IsUsingSuperUserFunction() && NKCShopManager.IsTabSoldOut(this.m_tabTemplet));
		}

		// Token: 0x06007D71 RID: 32113 RVA: 0x002A0C90 File Offset: 0x0029EE90
		public bool SelectSubSlot(string tabType, int subTabIndex, bool bAnimate)
		{
			bool flag = false;
			foreach (NKCUIShopTabSlot nkcuishopTabSlot in this.m_lstSubSlot)
			{
				bool flag2 = nkcuishopTabSlot.OnSelected(tabType, subTabIndex);
				if (!flag)
				{
					flag = flag2;
				}
			}
			bool flag3 = this.m_eType == tabType && subTabIndex == 0;
			this.m_ctglTab.Select(flag || flag3, true, false);
			foreach (NKCUIShopTabSlot nkcuishopTabSlot2 in this.m_lstSubSlot)
			{
				nkcuishopTabSlot2.OnActive(flag || flag3, bAnimate);
			}
			this.m_bSubTabOpened = (flag || flag3);
			return flag || flag3;
		}

		// Token: 0x06007D72 RID: 32114 RVA: 0x002A0D60 File Offset: 0x0029EF60
		public void ChangeSubTabState()
		{
			this.m_bSubTabOpened = !this.m_bSubTabOpened;
			foreach (NKCUIShopTabSlot nkcuishopTabSlot in this.m_lstSubSlot)
			{
				nkcuishopTabSlot.OnActive(this.m_bSubTabOpened, true);
			}
		}

		// Token: 0x06007D73 RID: 32115 RVA: 0x002A0DC8 File Offset: 0x0029EFC8
		protected void SetRibbon(ShopItemRibbon ribbonType)
		{
			NKCUtil.SetImageColor(this.m_imgRibbon, NKCShopManager.GetRibbonColor(ribbonType));
			NKCUtil.SetLabelText(this.m_lbRibbon, NKCShopManager.GetRibbonString(ribbonType));
			NKCUtil.SetGameobjectActive(this.m_lbRibbon, ribbonType > ShopItemRibbon.None);
			NKCUtil.SetGameobjectActive(this.m_imgRibbon, ribbonType > ShopItemRibbon.None);
		}

		// Token: 0x04006A44 RID: 27204
		private const float ONE_SECOND = 1f;

		// Token: 0x04006A45 RID: 27205
		public Sprite m_sprBaseBGSelected;

		// Token: 0x04006A46 RID: 27206
		public Sprite m_sprBaseBGUnSelected;

		// Token: 0x04006A47 RID: 27207
		public NKCUIComToggle m_ctglTab;

		// Token: 0x04006A48 RID: 27208
		public Image m_imgIcon;

		// Token: 0x04006A49 RID: 27209
		public Image m_imgBG;

		// Token: 0x04006A4A RID: 27210
		public Text m_lbTabName;

		// Token: 0x04006A4B RID: 27211
		public Image m_imgIconUnSelected;

		// Token: 0x04006A4C RID: 27212
		public Image m_imgBGUnSelected;

		// Token: 0x04006A4D RID: 27213
		public Text m_lbTabNameUnSelected;

		// Token: 0x04006A4E RID: 27214
		public GameObject m_objRemainTimeSelected;

		// Token: 0x04006A4F RID: 27215
		public Text m_lbRemainTimeSelected;

		// Token: 0x04006A50 RID: 27216
		public GameObject m_objRemainTimeUnSelected;

		// Token: 0x04006A51 RID: 27217
		public Text m_lbRemainTimeUnSelected;

		// Token: 0x04006A52 RID: 27218
		public GameObject m_objRedDot;

		// Token: 0x04006A53 RID: 27219
		public GameObject m_objReddot_RED;

		// Token: 0x04006A54 RID: 27220
		public GameObject m_objReddot_YELLOW;

		// Token: 0x04006A55 RID: 27221
		public Text m_lbReddotCount;

		// Token: 0x04006A56 RID: 27222
		public Color BASE_COLOR_SELECTED_TEXT;

		// Token: 0x04006A57 RID: 27223
		[Header("리본")]
		public Image m_imgRibbon;

		// Token: 0x04006A58 RID: 27224
		public Text m_lbRibbon;

		// Token: 0x04006A59 RID: 27225
		private List<NKCUIShopTabSlot> m_lstSubSlot = new List<NKCUIShopTabSlot>();

		// Token: 0x04006A5A RID: 27226
		private NKCUIShopTab.OnTabSelected dOnTabSelected;

		// Token: 0x04006A5B RID: 27227
		public ShopTabTemplet m_tabTemplet;

		// Token: 0x04006A5C RID: 27228
		private DateTime m_tEndTimeUtc = DateTime.MinValue;

		// Token: 0x04006A5D RID: 27229
		private bool m_bSubTabOpened;

		// Token: 0x04006A5E RID: 27230
		private float m_tDeltaTime;

		// Token: 0x0200185E RID: 6238
		// (Invoke) Token: 0x0600B5BF RID: 46527
		public delegate void OnTabSelected(string type, int subIndex = 0);
	}
}

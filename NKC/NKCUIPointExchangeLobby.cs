using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using NKM.Shop;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009CA RID: 2506
	public class NKCUIPointExchangeLobby : MonoBehaviour
	{
		// Token: 0x06006AEE RID: 27374 RVA: 0x0022BCAC File Offset: 0x00229EAC
		public void Init()
		{
			if (!this.IsEventTime())
			{
				this.SetAsEmpty();
				return;
			}
			NKMPointExchangeTemplet byTime = NKMPointExchangeTemplet.GetByTime(NKCSynchronizedTime.ServiceTime);
			if (byTime == null)
			{
				this.SetAsEmpty();
				return;
			}
			base.gameObject.SetActive(true);
			NKCUtil.SetGameobjectActive(this.m_buttonImage, true);
			NKCUtil.SetGameobjectActive(this.m_objEventClosed, false);
			Sprite orLoadAssetResource = NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("ab_ui_nkm_ui_lobby_texture", byTime.BannerId));
			NKCUtil.SetImageSprite(this.m_buttonImage, orLoadAssetResource, false);
		}

		// Token: 0x06006AEF RID: 27375 RVA: 0x0022BD24 File Offset: 0x00229F24
		public void SetData()
		{
			if (!this.IsEventTime())
			{
				this.SetAsEmpty();
				return;
			}
			NKMPointExchangeTemplet byTime = NKMPointExchangeTemplet.GetByTime(NKCSynchronizedTime.ServiceTime);
			if (byTime == null)
			{
				this.SetAsEmpty();
				return;
			}
			NKMIntervalTemplet eventRemainTime = NKMIntervalTemplet.Find(byTime.IntervalTag);
			this.SetEventRemainTime(eventRemainTime);
			NKMUserData userData = NKCScenManager.CurrentUserData();
			this.CheckRedDot(userData, byTime.ShopTabStrId, byTime.ShopTabSubIndex);
			NKCPopupPointExchange instance = NKCPopupPointExchange.Instance;
			NKCUtil.SetButtonClickDelegate(this.m_csbtnButton, new UnityAction(this.OnClickButton));
		}

		// Token: 0x06006AF0 RID: 27376 RVA: 0x0022BD9E File Offset: 0x00229F9E
		public static void OpenPtExchangePopup()
		{
			NKCUIPointExchangeTransition.MakeInstance(NKMPointExchangeTemplet.GetByTime(NKCSynchronizedTime.ServiceTime)).Open();
		}

		// Token: 0x06006AF1 RID: 27377 RVA: 0x0022BDB4 File Offset: 0x00229FB4
		private bool IsEventTime()
		{
			bool flag = false;
			foreach (NKMPointExchangeTemplet nkmpointExchangeTemplet in NKMTempletContainer<NKMPointExchangeTemplet>.Values)
			{
				if (nkmpointExchangeTemplet != null && nkmpointExchangeTemplet.EnableByTag)
				{
					if (string.IsNullOrEmpty(nkmpointExchangeTemplet.IntervalTag))
					{
						flag = true;
						break;
					}
					NKMIntervalTemplet nkmintervalTemplet = NKMIntervalTemplet.Find(nkmpointExchangeTemplet.IntervalTag);
					if (nkmintervalTemplet == null)
					{
						Log.Debug("IntervalTemplet with " + nkmpointExchangeTemplet.IntervalTag + " not exist", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIPointExchangeLobby.cs", 106);
					}
					else
					{
						flag = nkmintervalTemplet.IsValidTime(NKCSynchronizedTime.ServiceTime);
						if (flag)
						{
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06006AF2 RID: 27378 RVA: 0x0022BE5C File Offset: 0x0022A05C
		private void SetEventRemainTime(NKMIntervalTemplet intervalTemplet)
		{
			if (intervalTemplet == null)
			{
				NKCUtil.SetGameobjectActive(this.m_lbRemainTime, false);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_lbRemainTime, true);
			TimeSpan timeLeft = NKCSynchronizedTime.GetTimeLeft(NKCSynchronizedTime.ToUtcTime(intervalTemplet.EndDate));
			string msg;
			if (timeLeft.Days > 0)
			{
				msg = string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_DAYS", false), timeLeft.Days);
			}
			else if (timeLeft.Hours > 0)
			{
				msg = string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_HOURS", false), timeLeft.Hours);
			}
			else
			{
				msg = string.Format(NKCStringTable.GetString("SI_DP_REMAIN_TIME_STRING_EX_MINUTES", false), timeLeft.Minutes);
			}
			NKCUtil.SetLabelText(this.m_lbRemainTime, msg);
		}

		// Token: 0x06006AF3 RID: 27379 RVA: 0x0022BF18 File Offset: 0x0022A118
		private void CheckRedDot(NKMUserData userData, string shopTabStrId, int shopTabSubIndex)
		{
			bool bValue = false;
			List<ShopItemTemplet> itemTempletListByTab = NKCShopManager.GetItemTempletListByTab(ShopTabTemplet.Find(shopTabStrId, shopTabSubIndex), false);
			if (itemTempletListByTab == null || userData == null)
			{
				NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
				return;
			}
			int count = itemTempletListByTab.Count;
			for (int i = 0; i < count; i++)
			{
				if (itemTempletListByTab[i] != null)
				{
					NKMShopData shopData = userData.m_ShopData;
					bool flag = userData.m_InventoryData.GetCountMiscItem(itemTempletListByTab[i].m_PriceItemID) >= (long)itemTempletListByTab[i].m_Price;
					bool flag2 = true;
					if (shopData.histories.ContainsKey(itemTempletListByTab[i].m_ProductID))
					{
						flag2 = (shopData.histories[itemTempletListByTab[i].m_ProductID].purchaseCount < itemTempletListByTab[i].m_QuantityLimit);
					}
					if (flag2 && flag)
					{
						bValue = true;
						break;
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objRedDot, bValue);
		}

		// Token: 0x06006AF4 RID: 27380 RVA: 0x0022C001 File Offset: 0x0022A201
		private Sprite GetEmptyButtonImage()
		{
			return NKCResourceUtility.GetOrLoadAssetResource<Sprite>(NKMAssetName.ParseBundleName("AB_UI_LOBBY_THUMB_EVENT", "THUMB_EVENT_EMPTY"));
		}

		// Token: 0x06006AF5 RID: 27381 RVA: 0x0022C017 File Offset: 0x0022A217
		private void SetAsEmpty()
		{
			this.GetEmptyButtonImage();
			NKCUtil.SetGameobjectActive(this.m_buttonImage, false);
			NKCUtil.SetGameobjectActive(this.m_objEventClosed, true);
			NKCUIComStateButton csbtnButton = this.m_csbtnButton;
			if (csbtnButton == null)
			{
				return;
			}
			csbtnButton.PointerClick.RemoveAllListeners();
		}

		// Token: 0x06006AF6 RID: 27382 RVA: 0x0022C04D File Offset: 0x0022A24D
		private void OnClickButton()
		{
			NKCUIPointExchangeLobby.OpenPtExchangePopup();
		}

		// Token: 0x0400569B RID: 22171
		public Text m_lbRemainTime;

		// Token: 0x0400569C RID: 22172
		public Image m_buttonImage;

		// Token: 0x0400569D RID: 22173
		public Image m_buttonTitleImage;

		// Token: 0x0400569E RID: 22174
		public NKCUIComStateButton m_csbtnButton;

		// Token: 0x0400569F RID: 22175
		public GameObject m_objRedDot;

		// Token: 0x040056A0 RID: 22176
		public GameObject m_objEventClosed;
	}
}

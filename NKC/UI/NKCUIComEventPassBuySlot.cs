using System;
using System.Collections.Generic;
using NKM.EventPass;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000935 RID: 2357
	public class NKCUIComEventPassBuySlot : MonoBehaviour
	{
		// Token: 0x06005E2A RID: 24106 RVA: 0x001D24CC File Offset: 0x001D06CC
		public void Init()
		{
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager == null)
			{
				return;
			}
			NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(eventPassDataManager.EventPassId);
			if (nkmeventPassTemplet == null)
			{
				return;
			}
			int num = nkmeventPassTemplet.PassMaxLevel / this.m_iPassLevelInterval;
			for (int i = 0; i < num; i++)
			{
				NKCUISlot newInstance = NKCUISlot.GetNewInstance(this.m_slotContent);
				if (newInstance != null)
				{
					this.m_listCorePassRewardSlot.Add(newInstance);
					newInstance.SetActive(true);
				}
			}
			NKCUtil.SetButtonClickDelegate(this.m_purchaseButton, new UnityAction(this.OnClickBuy));
		}

		// Token: 0x06005E2B RID: 24107 RVA: 0x001D2558 File Offset: 0x001D0758
		public void SetData(string title, string desc, int userPassLevel, int addPassLevel, int priceId, int price, float discountPercent, int discountedPrice, NKCUIComEventPassBuySlot.ClickBuy onClickBuy)
		{
			NKCEventPassDataManager eventPassDataManager = NKCScenManager.GetScenManager().GetEventPassDataManager();
			if (eventPassDataManager == null)
			{
				return;
			}
			NKMEventPassTemplet nkmeventPassTemplet = NKMTempletContainer<NKMEventPassTemplet>.Find(eventPassDataManager.EventPassId);
			if (nkmeventPassTemplet == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_lbTitle, title);
			NKCUtil.SetLabelText(this.m_lbDesc, desc);
			int count = this.m_listCorePassRewardSlot.Count;
			for (int i = 0; i < count; i++)
			{
				int num = (i + 1) * this.m_iPassLevelInterval;
				NKMEventPassRewardTemplet rewardTemplet = NKMEventPassRewardTemplet.GetRewardTemplet(nkmeventPassTemplet.PassRewardGroupId, num);
				if (rewardTemplet != null)
				{
					NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeRewardTypeData(rewardTemplet.CoreRewardItemType, rewardTemplet.CoreRewardItemId, rewardTemplet.CoreRewardItemCount, 0);
					if (slotData.eType == NKCUISlot.eSlotMode.ItemMisc)
					{
						this.m_listCorePassRewardSlot[i].SetData(slotData, true, new NKCUISlot.OnClick(this.OnClickRewardIcon));
					}
					else
					{
						this.m_listCorePassRewardSlot[i].SetData(slotData, true, null);
					}
					this.m_listCorePassRewardSlot[i].OverrideName(string.Format("<size={0}>Lv {1}</size>", this.m_iSlotIconLabelFontSize, num), true, true);
					if (num <= userPassLevel + addPassLevel)
					{
						this.m_listCorePassRewardSlot[i].SetTopNotice(NKCUtilString.GET_STRING_EVENTPASS_REWARD_POSSIBLE, true);
					}
				}
			}
			Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(priceId);
			NKCUtil.SetImageSprite(this.m_imgPriceIcon, orLoadMiscItemSmallIcon, false);
			if (discountPercent > 0f)
			{
				NKCUtil.SetGameobjectActive(this.m_objDiscountRate, true);
				NKCUtil.SetLabelText(this.m_lbDiscountRate, string.Format(NKCUtilString.GET_STRING_EVENTPASS_COREPASS_DISCOUNT_RATE, string.Format("-{0:###%}", discountPercent)));
				NKCUtil.SetGameobjectActive(this.m_objOriginalPrice, true);
				NKCUtil.SetLabelText(this.m_lbOriginalPrice, string.Format("{0}", price));
				NKCUtil.SetLabelText(this.m_lbPrice, string.Format("{0}", discountedPrice));
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objDiscountRate, false);
				NKCUtil.SetGameobjectActive(this.m_objOriginalPrice, false);
				NKCUtil.SetLabelText(this.m_lbPrice, string.Format("{0}", price));
			}
			if (addPassLevel > 0)
			{
				this.m_purchaseButton.SetLock(userPassLevel >= nkmeventPassTemplet.PassMaxLevel, false);
			}
			this.m_onClickBuy = onClickBuy;
		}

		// Token: 0x06005E2C RID: 24108 RVA: 0x001D2780 File Offset: 0x001D0980
		private void OnClickRewardIcon(NKCUISlot.SlotData slotData, bool bLocked)
		{
			NKCPopupItemBox.Instance.Open(NKCPopupItemBox.eMode.Normal, slotData, null, false, false, false);
		}

		// Token: 0x06005E2D RID: 24109 RVA: 0x001D2792 File Offset: 0x001D0992
		private void OnClickBuy()
		{
			if (this.m_onClickBuy != null)
			{
				this.m_onClickBuy();
			}
		}

		// Token: 0x06005E2E RID: 24110 RVA: 0x001D27A8 File Offset: 0x001D09A8
		private void OnDestroy()
		{
			this.m_lbTitle = null;
			this.m_lbDesc = null;
			this.m_lbPrice = null;
			this.m_imgPriceIcon = null;
			this.m_slotContent = null;
			this.m_purchaseButton = null;
			List<NKCUISlot> listCorePassRewardSlot = this.m_listCorePassRewardSlot;
			if (listCorePassRewardSlot != null)
			{
				listCorePassRewardSlot.Clear();
			}
			this.m_listCorePassRewardSlot = null;
			this.m_onClickBuy = null;
		}

		// Token: 0x04004A54 RID: 19028
		public Text m_lbTitle;

		// Token: 0x04004A55 RID: 19029
		public Text m_lbDesc;

		// Token: 0x04004A56 RID: 19030
		public Text m_lbPrice;

		// Token: 0x04004A57 RID: 19031
		public Text m_lbDiscountRate;

		// Token: 0x04004A58 RID: 19032
		public GameObject m_objDiscountRate;

		// Token: 0x04004A59 RID: 19033
		public Text m_lbOriginalPrice;

		// Token: 0x04004A5A RID: 19034
		public GameObject m_objOriginalPrice;

		// Token: 0x04004A5B RID: 19035
		public Image m_imgPriceIcon;

		// Token: 0x04004A5C RID: 19036
		public Transform m_slotContent;

		// Token: 0x04004A5D RID: 19037
		public NKCUIComStateButton m_purchaseButton;

		// Token: 0x04004A5E RID: 19038
		public int m_iPassLevelInterval;

		// Token: 0x04004A5F RID: 19039
		public int m_iSlotIconLabelFontSize;

		// Token: 0x04004A60 RID: 19040
		private List<NKCUISlot> m_listCorePassRewardSlot = new List<NKCUISlot>();

		// Token: 0x04004A61 RID: 19041
		private NKCUIComEventPassBuySlot.ClickBuy m_onClickBuy;

		// Token: 0x020015BA RID: 5562
		// (Invoke) Token: 0x0600AE0E RID: 44558
		public delegate void ClickBuy();
	}
}

using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM.Guild;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B40 RID: 2880
	public class NKCPopupGuildDonationSlot : MonoBehaviour
	{
		// Token: 0x0600832D RID: 33581 RVA: 0x002C3BA4 File Offset: 0x002C1DA4
		public void InitUI(NKCPopupGuildDonationSlot.OnSlot onSlot)
		{
			this.m_dOnSlot = onSlot;
			this.m_btnSlot.PointerClick.RemoveAllListeners();
			this.m_btnSlot.PointerClick.AddListener(new UnityAction(this.OnClickSlot));
			this.m_btnResource.PointerClick.RemoveAllListeners();
			this.m_btnResource.PointerClick.AddListener(new UnityAction(this.OnClickSlot));
			for (int i = 0; i < this.m_lstRewardSlot.Count; i++)
			{
				this.m_lstRewardSlot[i].Init();
			}
		}

		// Token: 0x0600832E RID: 33582 RVA: 0x002C3C38 File Offset: 0x002C1E38
		public void SetData(GuildDonationTemplet templet)
		{
			if (templet == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_cGuildDonationTemplet = templet;
			NKCUtil.SetLabelText(this.m_lbTitle, NKCStringTable.GetString(templet.DonateText, false));
			NKCUtil.SetImageSprite(this.m_imgIcon, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_CONSORTIUM_Texture", templet.DonateImgFileName, false), false);
			for (int i = 0; i < this.m_lstRewardSlot.Count; i++)
			{
				if (i >= templet.m_DonationReward.Count)
				{
					NKCUtil.SetGameobjectActive(this.m_lstRewardSlot[i], false);
				}
				else if (templet.m_DonationReward[i].RewardType == NKM_REWARD_TYPE.RT_MISC)
				{
					NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(templet.m_DonationReward[i].RewardID, (long)templet.m_DonationReward[i].RewardValue, 0);
					this.m_lstRewardSlot[i].SetData(data, false, null);
				}
				else
				{
					Log.Error("기부 보상이 MISC 타입이 아님 - 해당 타입 작업 필요함", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/NKCPopupGuildDonationSlot.cs", 77);
				}
			}
			NKCUtil.SetImageSprite(this.m_imgUseResourceIcon, NKCResourceUtility.GetOrLoadMiscItemSmallIcon(templet.reqItemUnit.ItemId), false);
			NKCUtil.SetLabelText(this.m_lbUseResourceCount, templet.reqItemUnit.Count.ToString("N0"));
			this.CheckState(templet);
		}

		// Token: 0x0600832F RID: 33583 RVA: 0x002C3D7C File Offset: 0x002C1F7C
		public void CheckState(GuildDonationTemplet templet)
		{
			if (NKCGuildManager.GetRemainDonationCount() <= 0)
			{
				this.m_btnSlot.Lock(false);
				this.m_btnResource.Lock(false);
				NKCUtil.SetLabelTextColor(this.m_lbUseResourceCount, NKCUtil.GetColor("#212122"));
				return;
			}
			if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(templet.reqItemUnit.ItemId) < templet.reqItemUnit.Count)
			{
				NKCUtil.SetLabelTextColor(this.m_lbUseResourceCount, Color.red);
			}
			else
			{
				NKCUtil.SetLabelTextColor(this.m_lbUseResourceCount, NKCUtil.GetColor("#582817"));
			}
			this.m_btnSlot.UnLock(false);
			this.m_btnResource.UnLock(false);
		}

		// Token: 0x06008330 RID: 33584 RVA: 0x002C3E28 File Offset: 0x002C2028
		private void OnClickSlot()
		{
			if (this.m_btnSlot.m_bLock)
			{
				return;
			}
			if (NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_cGuildDonationTemplet.reqItemUnit.ItemId) < this.m_cGuildDonationTemplet.reqItemUnit.Count)
			{
				NKCShopManager.OpenItemLackPopup(this.m_cGuildDonationTemplet.reqItemUnit.ItemId, this.m_cGuildDonationTemplet.reqItemUnit.Count32);
				return;
			}
			NKCPopupGuildDonationSlot.OnSlot dOnSlot = this.m_dOnSlot;
			if (dOnSlot == null)
			{
				return;
			}
			dOnSlot(this.m_cGuildDonationTemplet.ID);
		}

		// Token: 0x04006F5A RID: 28506
		public Text m_lbTitle;

		// Token: 0x04006F5B RID: 28507
		public Image m_imgIcon;

		// Token: 0x04006F5C RID: 28508
		public List<NKCUISlot> m_lstRewardSlot = new List<NKCUISlot>();

		// Token: 0x04006F5D RID: 28509
		public Image m_imgUseResourceIcon;

		// Token: 0x04006F5E RID: 28510
		public Text m_lbUseResourceCount;

		// Token: 0x04006F5F RID: 28511
		public NKCUIComStateButton m_btnSlot;

		// Token: 0x04006F60 RID: 28512
		public NKCUIComStateButton m_btnResource;

		// Token: 0x04006F61 RID: 28513
		private NKCPopupGuildDonationSlot.OnSlot m_dOnSlot;

		// Token: 0x04006F62 RID: 28514
		private GuildDonationTemplet m_cGuildDonationTemplet;

		// Token: 0x020018DB RID: 6363
		// (Invoke) Token: 0x0600B6F1 RID: 46833
		public delegate void OnSlot(int slot);
	}
}

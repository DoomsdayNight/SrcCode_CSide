using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;
using NKM.Guild;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B3F RID: 2879
	public class NKCPopupGuildDonation : NKCUIBase
	{
		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x0600831E RID: 33566 RVA: 0x002C3854 File Offset: 0x002C1A54
		public static NKCPopupGuildDonation Instance
		{
			get
			{
				if (NKCPopupGuildDonation.m_Instance == null)
				{
					NKCPopupGuildDonation.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildDonation>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_POPUP_DONATION", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGuildDonation.CleanupInstance)).GetInstance<NKCPopupGuildDonation>();
					if (NKCPopupGuildDonation.m_Instance != null)
					{
						NKCPopupGuildDonation.m_Instance.InitUI();
					}
				}
				return NKCPopupGuildDonation.m_Instance;
			}
		}

		// Token: 0x0600831F RID: 33567 RVA: 0x002C38B5 File Offset: 0x002C1AB5
		private static void CleanupInstance()
		{
			NKCPopupGuildDonation.m_Instance = null;
		}

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x06008320 RID: 33568 RVA: 0x002C38BD File Offset: 0x002C1ABD
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildDonation.m_Instance != null && NKCPopupGuildDonation.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008321 RID: 33569 RVA: 0x002C38D8 File Offset: 0x002C1AD8
		private void OnDestroy()
		{
			NKCPopupGuildDonation.m_Instance = null;
		}

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x06008322 RID: 33570 RVA: 0x002C38E0 File Offset: 0x002C1AE0
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x06008323 RID: 33571 RVA: 0x002C38E3 File Offset: 0x002C1AE3
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06008324 RID: 33572 RVA: 0x002C38EA File Offset: 0x002C1AEA
		public override void CloseInternal()
		{
			this.m_lstTemplet.Clear();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008325 RID: 33573 RVA: 0x002C3903 File Offset: 0x002C1B03
		public override void UnHide()
		{
			base.UnHide();
			this.UpdateState();
		}

		// Token: 0x06008326 RID: 33574 RVA: 0x002C3914 File Offset: 0x002C1B14
		private void InitUI()
		{
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_btnBG.PointerClick.RemoveAllListeners();
			this.m_btnBG.PointerClick.AddListener(new UnityAction(base.Close));
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].InitUI(new NKCPopupGuildDonationSlot.OnSlot(this.OnClickSlot));
			}
		}

		// Token: 0x06008327 RID: 33575 RVA: 0x002C39AC File Offset: 0x002C1BAC
		public void Open()
		{
			if (this.m_lstSlot.Count != NKMTempletContainer<GuildDonationTemplet>.Values.Count<GuildDonationTemplet>())
			{
				Log.Error("기부 종류가 UI와 템플릿이 다름", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/NKCPopupGuildDonation.cs", 95);
			}
			this.m_lstTemplet = NKMTempletContainer<GuildDonationTemplet>.Values.ToList<GuildDonationTemplet>();
			this.UpdateState();
			base.UIOpened(true);
		}

		// Token: 0x06008328 RID: 33576 RVA: 0x002C3A00 File Offset: 0x002C1C00
		private void UpdateState()
		{
			NKCUtil.SetLabelText(this.m_lbRemainContibutionCount, string.Format(NKCUtilString.GET_STRING_CONTRACT_COUNT_ONE_PARAM, NKCGuildManager.GetRemainDonationCount()));
			if (NKCGuildManager.GetRemainDonationCount() > 0)
			{
				NKCUtil.SetLabelTextColor(this.m_lbRemainContibutionCount, NKCUtil.GetColor("#C3C3C3"));
			}
			else
			{
				NKCUtil.SetLabelTextColor(this.m_lbRemainContibutionCount, Color.red);
			}
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].SetData(this.m_lstTemplet[i]);
			}
		}

		// Token: 0x06008329 RID: 33577 RVA: 0x002C3A90 File Offset: 0x002C1C90
		private void OnClickSlot(int donationID)
		{
			GuildDonationTemplet guildDonationTemplet = GuildDonationTemplet.Find(donationID);
			if (guildDonationTemplet != null)
			{
				NKCPopupResourceConfirmBox.Instance.Open(NKCUtilString.GET_STRING_CONSORTIUM_POPUP_DONATION_COMFIRM_TITLE, NKCUtilString.GET_STRING_CONSORTIUM_POPUP_DONATION_COMFIRM_BODY, guildDonationTemplet.reqItemUnit.ItemId, guildDonationTemplet.reqItemUnit.Count32, delegate()
				{
					this.OnDonate(donationID);
				}, null, false);
				return;
			}
			Log.Error(string.Format("templet is null - ID : {0}", donationID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Guild/NKCPopupGuildDonation.cs", 131);
		}

		// Token: 0x0600832A RID: 33578 RVA: 0x002C3B1D File Offset: 0x002C1D1D
		private void OnDonate(int donationID)
		{
			NKCPacketSender.Send_NKMPacket_GUILD_DONATION_REQ(donationID);
		}

		// Token: 0x0600832B RID: 33579 RVA: 0x002C3B28 File Offset: 0x002C1D28
		public override void OnGuildDataChanged()
		{
			NKCUtil.SetLabelText(this.m_lbRemainContibutionCount, string.Format(NKCUtilString.GET_STRING_CONTRACT_COUNT_ONE_PARAM, NKCGuildManager.GetRemainDonationCount()));
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].CheckState(GuildDonationTemplet.Find(i + 1));
			}
		}

		// Token: 0x04006F52 RID: 28498
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM";

		// Token: 0x04006F53 RID: 28499
		private const string UI_ASSET_NAME = "NKM_UI_CONSORTIUM_POPUP_DONATION";

		// Token: 0x04006F54 RID: 28500
		private static NKCPopupGuildDonation m_Instance;

		// Token: 0x04006F55 RID: 28501
		public NKCUIComStateButton m_btnBG;

		// Token: 0x04006F56 RID: 28502
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04006F57 RID: 28503
		public Text m_lbRemainContibutionCount;

		// Token: 0x04006F58 RID: 28504
		public List<NKCPopupGuildDonationSlot> m_lstSlot = new List<NKCPopupGuildDonationSlot>();

		// Token: 0x04006F59 RID: 28505
		private List<GuildDonationTemplet> m_lstTemplet = new List<GuildDonationTemplet>();
	}
}

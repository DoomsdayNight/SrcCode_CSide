using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Shop
{
	// Token: 0x02000ACC RID: 2764
	public class NKCPopupShopCustomPackageSubstitude : NKCUIBase
	{
		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x06007BDB RID: 31707 RVA: 0x00295830 File Offset: 0x00293A30
		public static NKCPopupShopCustomPackageSubstitude Instance
		{
			get
			{
				if (NKCPopupShopCustomPackageSubstitude.m_Instance == null)
				{
					NKCPopupShopCustomPackageSubstitude.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupShopCustomPackageSubstitude>("ab_ui_nkm_ui_shop", "NKM_UI_POPUP_SHOP_BUY_PACKAGE_SUBSTITUDE", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupShopCustomPackageSubstitude.CleanupInstance)).GetInstance<NKCPopupShopCustomPackageSubstitude>();
					NKCPopupShopCustomPackageSubstitude.m_Instance.Init();
				}
				return NKCPopupShopCustomPackageSubstitude.m_Instance;
			}
		}

		// Token: 0x06007BDC RID: 31708 RVA: 0x0029587F File Offset: 0x00293A7F
		private static void CleanupInstance()
		{
			NKCPopupShopCustomPackageSubstitude.m_Instance = null;
		}

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x06007BDD RID: 31709 RVA: 0x00295887 File Offset: 0x00293A87
		public override NKCUIManager.eUIUnloadFlag UnloadFlag
		{
			get
			{
				return NKCUIManager.eUIUnloadFlag.DEFAULT;
			}
		}

		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x06007BDE RID: 31710 RVA: 0x0029588A File Offset: 0x00293A8A
		public static bool HasInstance
		{
			get
			{
				return NKCPopupShopCustomPackageSubstitude.m_Instance != null;
			}
		}

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x06007BDF RID: 31711 RVA: 0x00295897 File Offset: 0x00293A97
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupShopCustomPackageSubstitude.m_Instance != null && NKCPopupShopCustomPackageSubstitude.m_Instance.IsOpen;
			}
		}

		// Token: 0x06007BE0 RID: 31712 RVA: 0x002958B2 File Offset: 0x00293AB2
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupShopCustomPackageSubstitude.m_Instance != null && NKCPopupShopCustomPackageSubstitude.m_Instance.IsOpen)
			{
				NKCPopupShopCustomPackageSubstitude.m_Instance.Close();
			}
		}

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x06007BE1 RID: 31713 RVA: 0x002958D7 File Offset: 0x00293AD7
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x06007BE2 RID: 31714 RVA: 0x002958DA File Offset: 0x00293ADA
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06007BE3 RID: 31715 RVA: 0x002958E1 File Offset: 0x00293AE1
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06007BE4 RID: 31716 RVA: 0x002958F0 File Offset: 0x00293AF0
		private void Init()
		{
			this.m_srContent.dOnGetObject += this.GetObject;
			this.m_srContent.dOnReturnObject += this.ReturnObject;
			this.m_srContent.dOnProvideData += this.ProvideData;
			this.m_srContent.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_srContent, null);
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCancel, new UnityAction(base.Close));
			NKCUtil.SetButtonClickDelegate(this.m_csbtnConfirm, new UnityAction(this.OnOK));
			NKCUtil.SetHotkey(this.m_csbtnConfirm, HotkeyEventType.Confirm, null, false);
		}

		// Token: 0x06007BE5 RID: 31717 RVA: 0x00295998 File Offset: 0x00293B98
		public void Open(List<NKCShopManager.ShopRewardSubstituteData> lstSubstituteReward, NKCPopupShopCustomPackageSubstitude.OnClose onClose)
		{
			if (lstSubstituteReward != null && lstSubstituteReward.Count != 0)
			{
				this.m_lstSubstituteData = new List<NKCPopupShopCustomPackageSubstitude.SubstituteData>();
				for (int i = 0; i < lstSubstituteReward.Count; i++)
				{
					NKCUISlot.SlotData before = NKCUISlot.SlotData.MakeRewardTypeData(lstSubstituteReward[i].Before, 0);
					NKCUISlot.SlotData after = NKCUISlot.SlotData.MakeRewardTypeData(lstSubstituteReward[i].After, 0);
					this.m_lstSubstituteData.Add(new NKCPopupShopCustomPackageSubstitude.SubstituteData
					{
						Before = before,
						After = after
					});
				}
				this.dOnClose = onClose;
				base.UIOpened(true);
				this.m_srContent.TotalCount = this.m_lstSubstituteData.Count;
				this.m_srContent.SetIndexPosition(0);
				return;
			}
			base.gameObject.SetActive(false);
			NKCPopupShopCustomPackageSubstitude.OnClose onClose2 = this.dOnClose;
			if (onClose2 == null)
			{
				return;
			}
			onClose2();
		}

		// Token: 0x06007BE6 RID: 31718 RVA: 0x00295A63 File Offset: 0x00293C63
		private void OnOK()
		{
			base.Close();
			NKCPopupShopCustomPackageSubstitude.OnClose onClose = this.dOnClose;
			if (onClose != null)
			{
				onClose();
			}
			this.dOnClose = null;
			this.m_lstSubstituteData = null;
		}

		// Token: 0x06007BE7 RID: 31719 RVA: 0x00295A8A File Offset: 0x00293C8A
		private RectTransform GetObject(int index)
		{
			NKCPopupShopCustomPackageSubstitudeSlot nkcpopupShopCustomPackageSubstitudeSlot = UnityEngine.Object.Instantiate<NKCPopupShopCustomPackageSubstitudeSlot>(this.m_pfbSlot);
			nkcpopupShopCustomPackageSubstitudeSlot.Init();
			return nkcpopupShopCustomPackageSubstitudeSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06007BE8 RID: 31720 RVA: 0x00295AA2 File Offset: 0x00293CA2
		private void ReturnObject(Transform tr)
		{
			if (tr != null)
			{
				tr.SetParent(null);
			}
			tr.gameObject.SetActive(false);
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06007BE9 RID: 31721 RVA: 0x00295ACC File Offset: 0x00293CCC
		private void ProvideData(Transform transform, int idx)
		{
			if (idx >= this.m_lstSubstituteData.Count || idx < 0)
			{
				transform.gameObject.SetActive(false);
				return;
			}
			NKCPopupShopCustomPackageSubstitudeSlot component = transform.GetComponent<NKCPopupShopCustomPackageSubstitudeSlot>();
			if (component != null)
			{
				component.SetData(this.m_lstSubstituteData[idx].Before, this.m_lstSubstituteData[idx].After);
				return;
			}
			transform.gameObject.SetActive(false);
		}

		// Token: 0x0400689B RID: 26779
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_shop";

		// Token: 0x0400689C RID: 26780
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_SHOP_BUY_PACKAGE_SUBSTITUDE";

		// Token: 0x0400689D RID: 26781
		private static NKCPopupShopCustomPackageSubstitude m_Instance;

		// Token: 0x0400689E RID: 26782
		public NKCPopupShopCustomPackageSubstitudeSlot m_pfbSlot;

		// Token: 0x0400689F RID: 26783
		public LoopScrollRect m_srContent;

		// Token: 0x040068A0 RID: 26784
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x040068A1 RID: 26785
		public NKCUIComStateButton m_csbtnConfirm;

		// Token: 0x040068A2 RID: 26786
		private NKCPopupShopCustomPackageSubstitude.OnClose dOnClose;

		// Token: 0x040068A3 RID: 26787
		private List<NKCPopupShopCustomPackageSubstitude.SubstituteData> m_lstSubstituteData;

		// Token: 0x0200183F RID: 6207
		// (Invoke) Token: 0x0600B574 RID: 46452
		public delegate void OnClose();

		// Token: 0x02001840 RID: 6208
		public struct SubstituteData
		{
			// Token: 0x0400A86E RID: 43118
			public NKCUISlot.SlotData Before;

			// Token: 0x0400A86F RID: 43119
			public NKCUISlot.SlotData After;
		}
	}
}

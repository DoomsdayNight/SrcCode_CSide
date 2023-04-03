using System;
using ClientPacket.Account;
using Cs.Logging;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000919 RID: 2329
	public class NKCPopupAccountSelectConfirm : NKCUIBase
	{
		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x06005D3D RID: 23869 RVA: 0x001CC138 File Offset: 0x001CA338
		public static NKCPopupAccountSelectConfirm Instance
		{
			get
			{
				if (NKCPopupAccountSelectConfirm.m_Instance == null)
				{
					NKCPopupAccountSelectConfirm.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupAccountSelectConfirm>("AB_UI_NKM_UI_ACCOUNT_LINK", "NKM_UI_POPUP_ACCOUNT_SELECT_CONFIRM", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupAccountSelectConfirm.CleanupInstance)).GetInstance<NKCPopupAccountSelectConfirm>();
					NKCPopupAccountSelectConfirm.m_Instance.InitUI();
				}
				return NKCPopupAccountSelectConfirm.m_Instance;
			}
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x001CC187 File Offset: 0x001CA387
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupAccountSelectConfirm.m_Instance != null && NKCPopupAccountSelectConfirm.m_Instance.IsOpen)
			{
				NKCPopupAccountSelectConfirm.m_Instance.Close();
			}
		}

		// Token: 0x06005D3F RID: 23871 RVA: 0x001CC1AC File Offset: 0x001CA3AC
		private static void CleanupInstance()
		{
			NKCPopupAccountSelectConfirm.m_Instance = null;
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06005D40 RID: 23872 RVA: 0x001CC1B4 File Offset: 0x001CA3B4
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06005D41 RID: 23873 RVA: 0x001CC1B7 File Offset: 0x001CA3B7
		public override string MenuName
		{
			get
			{
				return "AccountLink";
			}
		}

		// Token: 0x06005D42 RID: 23874 RVA: 0x001CC1C0 File Offset: 0x001CA3C0
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_selectedSlot.InitData();
			NKCUIComStateButton cancel = this.m_cancel;
			if (cancel != null)
			{
				cancel.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton cancel2 = this.m_cancel;
			if (cancel2 == null)
			{
				return;
			}
			cancel2.PointerClick.AddListener(new UnityAction(this.OnClickClose));
		}

		// Token: 0x06005D43 RID: 23875 RVA: 0x001CC22C File Offset: 0x001CA42C
		public void OnClickClose()
		{
			Log.Debug("[SteamLink][SelectConfirm] OnClickClose", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCPopupAccountSelectConfirm.cs", 67);
			base.Close();
		}

		// Token: 0x06005D44 RID: 23876 RVA: 0x001CC248 File Offset: 0x001CA448
		public void Open(NKMAccountLinkUserProfile selectedUserProfile, UnityAction onClickConfirm)
		{
			Log.Debug("[SteamLink][SelectConfirm] Open", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCPopupAccountSelectConfirm.cs", 73);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCPopupAccountSelectSlot selectedSlot = this.m_selectedSlot;
			if (selectedSlot != null)
			{
				selectedSlot.SetData(selectedUserProfile, null);
			}
			NKCUIComStateButton ok = this.m_ok;
			if (ok != null)
			{
				ok.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton ok2 = this.m_ok;
			if (ok2 != null)
			{
				ok2.PointerClick.AddListener(onClickConfirm);
			}
			base.UIOpened(true);
		}

		// Token: 0x06005D45 RID: 23877 RVA: 0x001CC2C4 File Offset: 0x001CA4C4
		public void OpenSuccess(NKMAccountLinkUserProfile selectedUserProfile, UnityAction onClickConfirm)
		{
			Log.Debug("[SteamLink][SelectConfirm] OpenSuccess", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCPopupAccountSelectConfirm.cs", 87);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCPopupAccountSelectSlot selectedSlot = this.m_selectedSlot;
			if (selectedSlot != null)
			{
				selectedSlot.SetData(selectedUserProfile, null);
			}
			NKCUIComStateButton ok = this.m_ok;
			if (ok != null)
			{
				ok.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton ok2 = this.m_ok;
			if (ok2 != null)
			{
				ok2.PointerClick.AddListener(onClickConfirm);
			}
			base.UIOpened(true);
		}

		// Token: 0x06005D46 RID: 23878 RVA: 0x001CC340 File Offset: 0x001CA540
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x001CC355 File Offset: 0x001CA555
		public override void CloseInternal()
		{
			Log.Debug("[SteamLink][SelectConfirm] CloseInternal", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCPopupAccountSelectConfirm.cs", 109);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x04004950 RID: 18768
		private const string DEBUG_HEADER = "[SteamLink][SelectConfirm]";

		// Token: 0x04004951 RID: 18769
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_ACCOUNT_LINK";

		// Token: 0x04004952 RID: 18770
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_ACCOUNT_SELECT_CONFIRM";

		// Token: 0x04004953 RID: 18771
		private static NKCPopupAccountSelectConfirm m_Instance;

		// Token: 0x04004954 RID: 18772
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04004955 RID: 18773
		public NKCPopupAccountSelectSlot m_selectedSlot;

		// Token: 0x04004956 RID: 18774
		public NKCUIComStateButton m_ok;

		// Token: 0x04004957 RID: 18775
		public NKCUIComStateButton m_cancel;
	}
}

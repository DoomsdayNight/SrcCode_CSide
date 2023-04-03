using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC.PacketHandler;
using NKC.Publisher;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A39 RID: 2617
	public class NKCPopupCurrentLoginType : NKCUIBase
	{
		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x0600729D RID: 29341 RVA: 0x002612E0 File Offset: 0x0025F4E0
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x0600729E RID: 29342 RVA: 0x002612E3 File Offset: 0x0025F4E3
		public override string MenuName
		{
			get
			{
				return "Current Login Type";
			}
		}

		// Token: 0x0600729F RID: 29343 RVA: 0x002612EA File Offset: 0x0025F4EA
		public override void OnBackButton()
		{
			this.ClosePopup();
		}

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x060072A0 RID: 29344 RVA: 0x002612F4 File Offset: 0x0025F4F4
		public static NKCPopupCurrentLoginType Instance
		{
			get
			{
				if (NKCPopupCurrentLoginType._instance == null)
				{
					NKCPopupCurrentLoginType.m_loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupCurrentLoginType>("AB_UI_LOGIN_SELECT", "AB_UI_LOGIN_SELECT_CURRENT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupCurrentLoginType.CleanupInstance));
					if (NKCPopupCurrentLoginType.m_loadedUIData != null)
					{
						NKCPopupCurrentLoginType._instance = NKCPopupCurrentLoginType.m_loadedUIData.GetInstance<NKCPopupCurrentLoginType>();
					}
					NKCPopupCurrentLoginType._instance.InitUI();
					NKCUtil.SetGameobjectActive(NKCPopupCurrentLoginType._instance.gameObject, false);
				}
				return NKCPopupCurrentLoginType._instance;
			}
		}

		// Token: 0x060072A1 RID: 29345 RVA: 0x00261364 File Offset: 0x0025F564
		public static void CheckInstanceAndClose()
		{
			NKCUIManager.LoadedUIData loadedUIData = NKCPopupCurrentLoginType.m_loadedUIData;
			if (loadedUIData != null)
			{
				loadedUIData.CloseInstance();
			}
			NKCPopupCurrentLoginType.m_loadedUIData = null;
		}

		// Token: 0x060072A2 RID: 29346 RVA: 0x0026137C File Offset: 0x0025F57C
		private static void CleanupInstance()
		{
			NKCPopupCurrentLoginType._instance = null;
		}

		// Token: 0x060072A3 RID: 29347 RVA: 0x00261384 File Offset: 0x0025F584
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060072A4 RID: 29348 RVA: 0x00261394 File Offset: 0x0025F594
		public void InitUI()
		{
			NKCUIComStateButton facebook = this.m_Facebook;
			if (facebook != null)
			{
				facebook.Lock(false);
			}
			NKCUIComStateButton google = this.m_Google;
			if (google != null)
			{
				google.Lock(false);
			}
			NKCUIComStateButton twitter = this.m_Twitter;
			if (twitter != null)
			{
				twitter.Lock(false);
			}
			NKCUIComStateButton guest = this.m_Guest;
			if (guest != null)
			{
				guest.Lock(false);
			}
			NKCUIComStateButton apple = this.m_Apple;
			if (apple != null)
			{
				apple.Lock(false);
			}
			NKCUtil.SetGameobjectActive(this.m_Facebook, false);
			NKCUtil.SetGameobjectActive(this.m_Google, false);
			NKCUtil.SetGameobjectActive(this.m_Twitter, false);
			NKCUtil.SetGameobjectActive(this.m_Guest, false);
			NKCUtil.SetGameobjectActive(this.m_Apple, false);
			NKCUIComStateButton logout = this.m_Logout;
			if (logout != null)
			{
				logout.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton logout2 = this.m_Logout;
			if (logout2 != null)
			{
				logout2.PointerClick.AddListener(new UnityAction(this.OnClickLogout));
			}
			NKCUIComStateButton close = this.m_Close;
			if (close != null)
			{
				close.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton close2 = this.m_Close;
			if (close2 != null)
			{
				close2.PointerClick.AddListener(new UnityAction(this.ClosePopup));
			}
			NKCUIComStateButton back = this.m_Back;
			if (back != null)
			{
				back.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton back2 = this.m_Back;
			if (back2 != null)
			{
				back2.PointerClick.AddListener(new UnityAction(this.OnClickBack));
			}
			NKCUtil.SetGameobjectActive(this.m_Withdraw, NKCDefineManager.DEFINE_SELECT_SERVER());
			NKCUIComStateButton withdraw = this.m_Withdraw;
			if (withdraw != null)
			{
				withdraw.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton withdraw2 = this.m_Withdraw;
			if (withdraw2 == null)
			{
				return;
			}
			withdraw2.PointerClick.AddListener(new UnityAction(this.OnClickWithdraw));
		}

		// Token: 0x060072A5 RID: 29349 RVA: 0x00261526 File Offset: 0x0025F726
		public void Open(NKCPublisherModule.OnComplete dOnComplete, string providerName)
		{
			if (this.m_bOpen)
			{
				base.Close();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
			this.m_onComplete = dOnComplete;
			this.UpdateProviderBanner(providerName);
		}

		// Token: 0x060072A6 RID: 29350 RVA: 0x00261557 File Offset: 0x0025F757
		public static bool isOpen()
		{
			return NKCPopupCurrentLoginType.Instance != null && NKCPopupCurrentLoginType.Instance.IsOpen;
		}

		// Token: 0x060072A7 RID: 29351 RVA: 0x00261572 File Offset: 0x0025F772
		public void ClosePopup()
		{
			if (NKCPopupCurrentLoginType._instance == null)
			{
				return;
			}
			if (NKCPopupCurrentLoginType.isOpen())
			{
				NKCPopupCurrentLoginType.Instance.Close();
			}
			NKCPopupCurrentLoginType.CheckInstanceAndClose();
		}

		// Token: 0x060072A8 RID: 29352 RVA: 0x00261598 File Offset: 0x0025F798
		public void OnClickBack()
		{
			this.ClosePopup();
		}

		// Token: 0x060072A9 RID: 29353 RVA: 0x002615A0 File Offset: 0x0025F7A0
		public void OnClickLogout()
		{
			NKCPublisherModule.Auth.Logout(new NKCPublisherModule.OnComplete(NKCPacketHandlersLobby.OnLogoutComplete));
			this.ClosePopup();
		}

		// Token: 0x060072AA RID: 29354 RVA: 0x002615C0 File Offset: 0x0025F7C0
		private void OnClickWithdraw()
		{
			if (NKCPublisherModule.Auth.IsGuest())
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_OPTION_DROPOUT_WARNING_INSTANT, delegate()
				{
					NKCUIAccountWithdrawCheckPopup.Instance.OpenUI(false);
				}, null, false);
				return;
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_WARNING, NKCUtilString.GET_STRING_OPTION_DROPOUT_WARNING, delegate()
			{
				NKCUIAccountWithdrawCheckPopup.Instance.OpenUI(false);
			}, null, false);
		}

		// Token: 0x060072AB RID: 29355 RVA: 0x0026163C File Offset: 0x0025F83C
		public void UpdateProviderBanner(string providerName)
		{
			NKCUtil.SetGameobjectActive(this.m_Facebook, providerName.ToLower().Equals("facebook"));
			NKCUtil.SetGameobjectActive(this.m_Google, providerName.ToLower().Equals("google"));
			NKCUtil.SetGameobjectActive(this.m_Twitter, providerName.ToLower().Equals("twitter"));
			NKCUtil.SetGameobjectActive(this.m_Guest, providerName.ToLower().Equals("guest"));
			NKCUtil.SetGameobjectActive(this.m_Apple, providerName.ToLower().Equals("appleid"));
		}

		// Token: 0x060072AC RID: 29356 RVA: 0x002616D0 File Offset: 0x0025F8D0
		public void UpdateProviderProfile(Dictionary<string, object> providerProfileInformation)
		{
			foreach (KeyValuePair<string, object> keyValuePair in providerProfileInformation)
			{
				Log.Debug("[GameBase] authproviderInfo [" + keyValuePair.Key + "]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Popup/NKCPopupCurrentLoginType.cs", 183);
			}
		}

		// Token: 0x04005E86 RID: 24198
		public NKCUIComStateButton m_Facebook;

		// Token: 0x04005E87 RID: 24199
		public NKCUIComStateButton m_Google;

		// Token: 0x04005E88 RID: 24200
		public NKCUIComStateButton m_Twitter;

		// Token: 0x04005E89 RID: 24201
		public NKCUIComStateButton m_Apple;

		// Token: 0x04005E8A RID: 24202
		public NKCUIComStateButton m_Guest;

		// Token: 0x04005E8B RID: 24203
		public NKCUIComStateButton m_Logout;

		// Token: 0x04005E8C RID: 24204
		public NKCUIComStateButton m_Close;

		// Token: 0x04005E8D RID: 24205
		public NKCUIComStateButton m_Back;

		// Token: 0x04005E8E RID: 24206
		public NKCUIComStateButton m_Withdraw;

		// Token: 0x04005E8F RID: 24207
		private const string m_assetBundleName = "AB_UI_LOGIN_SELECT";

		// Token: 0x04005E90 RID: 24208
		private const string m_prefabName = "AB_UI_LOGIN_SELECT_CURRENT";

		// Token: 0x04005E91 RID: 24209
		private NKCPublisherModule.OnComplete m_onComplete;

		// Token: 0x04005E92 RID: 24210
		private static NKCUIManager.LoadedUIData m_loadedUIData;

		// Token: 0x04005E93 RID: 24211
		private static NKCPopupCurrentLoginType _instance;
	}
}

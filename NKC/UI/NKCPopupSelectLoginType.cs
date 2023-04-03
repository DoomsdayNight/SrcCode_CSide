using System;
using Cs.Logging;
using NKC.Publisher;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x02000A7D RID: 2685
	public class NKCPopupSelectLoginType : NKCUIBase
	{
		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x060076E3 RID: 30435 RVA: 0x00279016 File Offset: 0x00277216
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x060076E4 RID: 30436 RVA: 0x00279019 File Offset: 0x00277219
		public override string MenuName
		{
			get
			{
				return "Select Login Type";
			}
		}

		// Token: 0x060076E5 RID: 30437 RVA: 0x00279020 File Offset: 0x00277220
		public override void OnBackButton()
		{
			this.OnClickBack();
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x060076E6 RID: 30438 RVA: 0x00279028 File Offset: 0x00277228
		public static NKCPopupSelectLoginType Instance
		{
			get
			{
				if (NKCPopupSelectLoginType._instance == null)
				{
					NKCPopupSelectLoginType.m_loadedUIData = NKCUIManager.OpenNewInstance<NKCPopupSelectLoginType>("AB_UI_LOGIN_SELECT", "AB_UI_LOGIN_SOCIAL_POPUP", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupSelectLoginType.CleanupInstance));
					if (NKCPopupSelectLoginType.m_loadedUIData != null)
					{
						NKCPopupSelectLoginType._instance = NKCPopupSelectLoginType.m_loadedUIData.GetInstance<NKCPopupSelectLoginType>();
					}
					NKCPopupSelectLoginType._instance.InitUI();
					NKCUtil.SetGameobjectActive(NKCPopupSelectLoginType._instance.gameObject, false);
				}
				return NKCPopupSelectLoginType._instance;
			}
		}

		// Token: 0x060076E7 RID: 30439 RVA: 0x00279098 File Offset: 0x00277298
		public static void CheckInstanceAndClose()
		{
			NKCUIManager.LoadedUIData loadedUIData = NKCPopupSelectLoginType.m_loadedUIData;
			if (loadedUIData != null)
			{
				loadedUIData.CloseInstance();
			}
			NKCPopupSelectLoginType.m_loadedUIData = null;
		}

		// Token: 0x060076E8 RID: 30440 RVA: 0x002790B0 File Offset: 0x002772B0
		private static void CleanupInstance()
		{
			NKCPopupSelectLoginType._instance = null;
		}

		// Token: 0x060076E9 RID: 30441 RVA: 0x002790B8 File Offset: 0x002772B8
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x060076EA RID: 30442 RVA: 0x002790C8 File Offset: 0x002772C8
		public void InitUI()
		{
			NKCUIComStateButton facebook = this.m_Facebook;
			if (facebook != null)
			{
				facebook.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton facebook2 = this.m_Facebook;
			if (facebook2 != null)
			{
				facebook2.PointerClick.AddListener(new UnityAction(this.OnClickFacebookLogin));
			}
			NKCUIComStateButton google = this.m_Google;
			if (google != null)
			{
				google.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton google2 = this.m_Google;
			if (google2 != null)
			{
				google2.PointerClick.AddListener(new UnityAction(this.OnClickGoogleLogin));
			}
			NKCUIComStateButton twitter = this.m_Twitter;
			if (twitter != null)
			{
				twitter.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton twitter2 = this.m_Twitter;
			if (twitter2 != null)
			{
				twitter2.PointerClick.AddListener(new UnityAction(this.OnClickTwitterLogin));
			}
			NKCUIComStateButton guest = this.m_Guest;
			if (guest != null)
			{
				guest.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton guest2 = this.m_Guest;
			if (guest2 != null)
			{
				guest2.PointerClick.AddListener(new UnityAction(this.OnClickGuestLogin));
			}
			NKCUIComStateButton apple = this.m_Apple;
			if (apple != null)
			{
				apple.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton apple2 = this.m_Apple;
			if (apple2 != null)
			{
				apple2.PointerClick.AddListener(new UnityAction(this.OnClickAppleLogin));
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
			if (back2 == null)
			{
				return;
			}
			back2.PointerClick.AddListener(new UnityAction(this.OnClickBack));
		}

		// Token: 0x060076EB RID: 30443 RVA: 0x0027925C File Offset: 0x0027745C
		public void Open(NKCPublisherModule.OnComplete dOnComplete, bool isAddMappingState)
		{
			if (this.m_bOpen)
			{
				base.Close();
			}
			Log.Debug(string.Format("[GBLogin] PopupSelectLoginType mapping[{0}]", isAddMappingState), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/Popup/NKCPopupSelectLoginType.cs", 99);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
			this.m_onComplete = dOnComplete;
			this.m_isAddMappingState = isAddMappingState;
			NKCUtil.SetGameobjectActive(this.m_Guest.gameObject, !this.m_isAddMappingState);
			this.UpdateUI();
		}

		// Token: 0x060076EC RID: 30444 RVA: 0x002792D3 File Offset: 0x002774D3
		public static bool isOpen()
		{
			return NKCPopupSelectLoginType.Instance != null && NKCPopupSelectLoginType.Instance.IsOpen;
		}

		// Token: 0x060076ED RID: 30445 RVA: 0x002792EE File Offset: 0x002774EE
		public void ClosePopup()
		{
			if (NKCPopupSelectLoginType._instance == null)
			{
				return;
			}
			if (NKCPopupSelectLoginType.isOpen())
			{
				NKCPopupSelectLoginType.Instance.Close();
			}
			NKCPopupSelectLoginType.CheckInstanceAndClose();
		}

		// Token: 0x060076EE RID: 30446 RVA: 0x00279314 File Offset: 0x00277514
		public void OnClickBack()
		{
			this.ClosePopup();
		}

		// Token: 0x060076EF RID: 30447 RVA: 0x0027931C File Offset: 0x0027751C
		public void UpdateUI()
		{
		}

		// Token: 0x060076F0 RID: 30448 RVA: 0x0027931E File Offset: 0x0027751E
		public void OnClickGuestLogin()
		{
			this.DoRequest("GUEST");
		}

		// Token: 0x060076F1 RID: 30449 RVA: 0x0027932B File Offset: 0x0027752B
		public void OnClickGoogleLogin()
		{
			this.DoRequest("GOOGLE");
		}

		// Token: 0x060076F2 RID: 30450 RVA: 0x00279338 File Offset: 0x00277538
		public void OnClickTwitterLogin()
		{
			this.DoRequest("TWITTER");
		}

		// Token: 0x060076F3 RID: 30451 RVA: 0x00279345 File Offset: 0x00277545
		public void OnClickFacebookLogin()
		{
			this.DoRequest("FACEBOOK");
		}

		// Token: 0x060076F4 RID: 30452 RVA: 0x00279352 File Offset: 0x00277552
		public void OnClickAppleLogin()
		{
			this.DoRequest("APPLEID");
		}

		// Token: 0x060076F5 RID: 30453 RVA: 0x0027935F File Offset: 0x0027755F
		private void DoRequest(string providerName)
		{
			this.ClosePopup();
			if (this.m_isAddMappingState)
			{
				NKCPublisherModule.Auth.AddMapping(providerName, this.m_onComplete);
				return;
			}
			NKCPublisherModule.Auth.LoginToPublisherBy(providerName, this.m_onComplete);
		}

		// Token: 0x04006367 RID: 25447
		public NKCUIComStateButton m_Facebook;

		// Token: 0x04006368 RID: 25448
		public NKCUIComStateButton m_Google;

		// Token: 0x04006369 RID: 25449
		public NKCUIComStateButton m_Twitter;

		// Token: 0x0400636A RID: 25450
		public NKCUIComStateButton m_Apple;

		// Token: 0x0400636B RID: 25451
		public NKCUIComStateButton m_Guest;

		// Token: 0x0400636C RID: 25452
		public NKCUIComStateButton m_Close;

		// Token: 0x0400636D RID: 25453
		public NKCUIComStateButton m_Back;

		// Token: 0x0400636E RID: 25454
		private const string m_assetBundleName = "AB_UI_LOGIN_SELECT";

		// Token: 0x0400636F RID: 25455
		private const string m_prefabName = "AB_UI_LOGIN_SOCIAL_POPUP";

		// Token: 0x04006370 RID: 25456
		private NKCPublisherModule.OnComplete m_onComplete;

		// Token: 0x04006371 RID: 25457
		private bool m_isAddMappingState;

		// Token: 0x04006372 RID: 25458
		private static NKCUIManager.LoadedUIData m_loadedUIData;

		// Token: 0x04006373 RID: 25459
		private static NKCPopupSelectLoginType _instance;
	}
}

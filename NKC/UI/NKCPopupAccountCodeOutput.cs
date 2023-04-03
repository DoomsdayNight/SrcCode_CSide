using System;
using Cs.Logging;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000917 RID: 2327
	public class NKCPopupAccountCodeOutput : NKCUIBase
	{
		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06005D20 RID: 23840 RVA: 0x001CBB64 File Offset: 0x001C9D64
		public static NKCPopupAccountCodeOutput Instance
		{
			get
			{
				if (NKCPopupAccountCodeOutput.m_Instance == null)
				{
					NKCPopupAccountCodeOutput.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupAccountCodeOutput>("AB_UI_NKM_UI_ACCOUNT_LINK", "NKM_UI_POPUP_ACCOUNT_CODE_OUTPUT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupAccountCodeOutput.CleanupInstance)).GetInstance<NKCPopupAccountCodeOutput>();
					NKCPopupAccountCodeOutput.m_Instance.InitUI();
				}
				return NKCPopupAccountCodeOutput.m_Instance;
			}
		}

		// Token: 0x06005D21 RID: 23841 RVA: 0x001CBBB3 File Offset: 0x001C9DB3
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupAccountCodeOutput.m_Instance != null && NKCPopupAccountCodeOutput.m_Instance.IsOpen)
			{
				NKCPopupAccountCodeOutput.m_Instance.Close();
			}
		}

		// Token: 0x06005D22 RID: 23842 RVA: 0x001CBBD8 File Offset: 0x001C9DD8
		private static void CleanupInstance()
		{
			NKCPopupAccountCodeOutput.m_Instance = null;
		}

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x06005D23 RID: 23843 RVA: 0x001CBBE0 File Offset: 0x001C9DE0
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06005D24 RID: 23844 RVA: 0x001CBBE3 File Offset: 0x001C9DE3
		public override string MenuName
		{
			get
			{
				return "AccountLink";
			}
		}

		// Token: 0x06005D25 RID: 23845 RVA: 0x001CBBEA File Offset: 0x001C9DEA
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005D26 RID: 23846 RVA: 0x001CBC09 File Offset: 0x001C9E09
		public override void OnBackButton()
		{
			this.OnClickClose();
		}

		// Token: 0x06005D27 RID: 23847 RVA: 0x001CBC14 File Offset: 0x001C9E14
		public void Open(string privateLinkCode, float remainingTime)
		{
			Log.Debug("[SteamLink][CodeOutput] Open", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCPopupAccountCodeOutput.cs", 69);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			NKCUIComStateButton cancel = this.m_cancel;
			if (cancel != null)
			{
				cancel.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton cancel2 = this.m_cancel;
			if (cancel2 != null)
			{
				cancel2.PointerClick.AddListener(new UnityAction(this.OnClickClose));
			}
			NKCUtil.SetGameobjectActive(this.m_ok, false);
			if (this.m_privateLinkCodeText != null)
			{
				this.m_privateLinkCodeText.text = privateLinkCode;
			}
			this.m_remainingTime = remainingTime;
			this.m_prevTimeTextUpdate = this.m_remainingTime + 1f;
			this.UpdateTimerText();
			base.UIOpened(true);
		}

		// Token: 0x06005D28 RID: 23848 RVA: 0x001CBCD0 File Offset: 0x001C9ED0
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
				this.m_remainingTime -= Time.deltaTime;
				if (this.m_remainingTime < 0f)
				{
					this.m_remainingTime = 0f;
					this.m_ok.Lock(false);
				}
				if ((int)this.m_remainingTime != (int)this.m_prevTimeTextUpdate)
				{
					this.m_prevTimeTextUpdate = this.m_remainingTime;
					this.UpdateTimerText();
				}
			}
		}

		// Token: 0x06005D29 RID: 23849 RVA: 0x001CBD48 File Offset: 0x001C9F48
		public void OnClickClose()
		{
			Log.Debug("[SteamLink][CodeOutput] OnClickClose", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCPopupAccountCodeOutput.cs", 113);
			NKCAccountLinkMgr.CheckForCancelProcess();
		}

		// Token: 0x06005D2A RID: 23850 RVA: 0x001CBD60 File Offset: 0x001C9F60
		public override void CloseInternal()
		{
			Log.Debug("[SteamLink][CodeOutput] CloseInternal", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Steam/NKCPopupAccountCodeOutput.cs", 119);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06005D2B RID: 23851 RVA: 0x001CBD80 File Offset: 0x001C9F80
		public void UpdateTimerText()
		{
			string timeSpanStringMS = NKCUtilString.GetTimeSpanStringMS(TimeSpan.FromSeconds((double)this.m_remainingTime));
			if (this.m_timerText != null)
			{
				this.m_timerText.text = timeSpanStringMS;
			}
		}

		// Token: 0x04004936 RID: 18742
		private const string DEBUG_HEADER = "[SteamLink][CodeOutput]";

		// Token: 0x04004937 RID: 18743
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_ACCOUNT_LINK";

		// Token: 0x04004938 RID: 18744
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_ACCOUNT_CODE_OUTPUT";

		// Token: 0x04004939 RID: 18745
		private static NKCPopupAccountCodeOutput m_Instance;

		// Token: 0x0400493A RID: 18746
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x0400493B RID: 18747
		public Text m_privateLinkCodeText;

		// Token: 0x0400493C RID: 18748
		public Text m_timerText;

		// Token: 0x0400493D RID: 18749
		public NKCUIComStateButton m_ok;

		// Token: 0x0400493E RID: 18750
		public NKCUIComStateButton m_cancel;

		// Token: 0x0400493F RID: 18751
		private float m_prevTimeTextUpdate;

		// Token: 0x04004940 RID: 18752
		private float m_remainingTime;
	}
}

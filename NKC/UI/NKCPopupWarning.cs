using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A91 RID: 2705
	public class NKCPopupWarning : NKCUIBase
	{
		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x060077BF RID: 30655 RVA: 0x0027CCA7 File Offset: 0x0027AEA7
		public static NKCPopupWarning Instance
		{
			get
			{
				if (NKCPopupWarning.m_Instance == null)
				{
					NKCPopupWarning.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupWarning>("ab_ui_nkm_ui_world_map_renewal", "NKM_UI_POPUP_WARNING", NKCUIManager.eUIBaseRect.UIOverlay, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupWarning.CleanupInstance)).GetInstance<NKCPopupWarning>();
				}
				return NKCPopupWarning.m_Instance;
			}
		}

		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x060077C0 RID: 30656 RVA: 0x0027CCE1 File Offset: 0x0027AEE1
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupWarning.m_Instance != null && NKCPopupWarning.m_Instance.IsOpen;
			}
		}

		// Token: 0x060077C1 RID: 30657 RVA: 0x0027CCFC File Offset: 0x0027AEFC
		private static void CleanupInstance()
		{
			NKCPopupWarning.m_Instance = null;
		}

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x060077C2 RID: 30658 RVA: 0x0027CD04 File Offset: 0x0027AF04
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Overlay;
			}
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x060077C3 RID: 30659 RVA: 0x0027CD07 File Offset: 0x0027AF07
		public override string MenuName
		{
			get
			{
				return "Warning";
			}
		}

		// Token: 0x060077C4 RID: 30660 RVA: 0x0027CD10 File Offset: 0x0027AF10
		public void Open(string message, NKCPopupWarning.OnClose _dOnClose = null)
		{
			this.m_dOnClose = _dOnClose;
			NKCUtil.SetGameobjectActive(this.m_amtorWarning.gameObject, true);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_lbMessage.text = message;
			this.m_fElapsedTime = 0f;
			base.UIOpened(true);
		}

		// Token: 0x060077C5 RID: 30661 RVA: 0x0027CD6C File Offset: 0x0027AF6C
		private void Update()
		{
			this.m_fElapsedTime += Time.deltaTime;
			if (Input.anyKey && this.m_fElapsedTime > 1.1f)
			{
				base.Close();
				NKCPopupWarning.OnClose dOnClose = this.m_dOnClose;
				if (dOnClose == null)
				{
					return;
				}
				dOnClose(true);
				return;
			}
			else
			{
				if (this.m_amtorWarning.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
				{
					return;
				}
				base.Close();
				NKCPopupWarning.OnClose dOnClose2 = this.m_dOnClose;
				if (dOnClose2 == null)
				{
					return;
				}
				dOnClose2(false);
				return;
			}
		}

		// Token: 0x060077C6 RID: 30662 RVA: 0x0027CDEA File Offset: 0x0027AFEA
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0400645D RID: 25693
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_world_map_renewal";

		// Token: 0x0400645E RID: 25694
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_WARNING";

		// Token: 0x0400645F RID: 25695
		private static NKCPopupWarning m_Instance;

		// Token: 0x04006460 RID: 25696
		private NKCPopupWarning.OnClose m_dOnClose;

		// Token: 0x04006461 RID: 25697
		public Text m_lbMessage;

		// Token: 0x04006462 RID: 25698
		public Animator m_amtorWarning;

		// Token: 0x04006463 RID: 25699
		public NKCUIComStateButton m_csbtnBG;

		// Token: 0x04006464 RID: 25700
		private const float TIME_CAN_SKIP = 1.1f;

		// Token: 0x04006465 RID: 25701
		private float m_fElapsedTime;

		// Token: 0x020017ED RID: 6125
		// (Invoke) Token: 0x0600B4A0 RID: 46240
		public delegate void OnClose(bool bCanceled);
	}
}

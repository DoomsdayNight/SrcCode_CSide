using System;
using UnityEngine.Events;

namespace NKC.UI.Warfare
{
	// Token: 0x02000B00 RID: 2816
	public class NKCPopupWarfareInfo : NKCUIBase
	{
		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x06008009 RID: 32777 RVA: 0x002B2020 File Offset: 0x002B0220
		public static NKCPopupWarfareInfo Instance
		{
			get
			{
				if (NKCPopupWarfareInfo.m_Instance == null)
				{
					NKCPopupWarfareInfo.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupWarfareInfo>("ab_ui_nkm_ui_warfare", "NKM_UI_POPUP_WARFARE_INFOPOPUP", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupWarfareInfo.CleanupInstance)).GetInstance<NKCPopupWarfareInfo>();
					NKCPopupWarfareInfo.m_Instance.InitUI();
				}
				return NKCPopupWarfareInfo.m_Instance;
			}
		}

		// Token: 0x0600800A RID: 32778 RVA: 0x002B206F File Offset: 0x002B026F
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupWarfareInfo.m_Instance != null && NKCPopupWarfareInfo.m_Instance.IsOpen)
			{
				NKCPopupWarfareInfo.m_Instance.Close();
			}
		}

		// Token: 0x0600800B RID: 32779 RVA: 0x002B2094 File Offset: 0x002B0294
		private static void CleanupInstance()
		{
			NKCPopupWarfareInfo.m_Instance = null;
		}

		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x0600800C RID: 32780 RVA: 0x002B209C File Offset: 0x002B029C
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x0600800D RID: 32781 RVA: 0x002B209F File Offset: 0x002B029F
		public override string MenuName
		{
			get
			{
				return "WarfareInfo";
			}
		}

		// Token: 0x0600800E RID: 32782 RVA: 0x002B20A8 File Offset: 0x002B02A8
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			this.m_BtnClose.PointerClick.RemoveAllListeners();
			this.m_BtnClose.PointerClick.AddListener(new UnityAction(base.Close));
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600800F RID: 32783 RVA: 0x002B20FE File Offset: 0x002B02FE
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x06008010 RID: 32784 RVA: 0x002B211E File Offset: 0x002B031E
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06008011 RID: 32785 RVA: 0x002B2133 File Offset: 0x002B0333
		public void OK()
		{
			base.Close();
		}

		// Token: 0x06008012 RID: 32786 RVA: 0x002B213B File Offset: 0x002B033B
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x04006C34 RID: 27700
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_warfare";

		// Token: 0x04006C35 RID: 27701
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_WARFARE_INFOPOPUP";

		// Token: 0x04006C36 RID: 27702
		private static NKCPopupWarfareInfo m_Instance;

		// Token: 0x04006C37 RID: 27703
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04006C38 RID: 27704
		public NKCUIComStateButton m_BtnClose;
	}
}

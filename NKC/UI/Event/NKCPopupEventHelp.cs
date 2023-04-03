using System;
using NKM.Event;
using NKM.Templet.Base;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BC5 RID: 3013
	public class NKCPopupEventHelp : NKCUIBase
	{
		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x06008B6A RID: 35690 RVA: 0x002F6838 File Offset: 0x002F4A38
		public static NKCPopupEventHelp Instance
		{
			get
			{
				if (NKCPopupEventHelp.m_Instance == null)
				{
					NKCPopupEventHelp.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupEventHelp>("AB_UI_NKM_UI_EVENT", "NKM_UI_POPUP_EVENT_HELP", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupEventHelp.CleanupInstance)).GetInstance<NKCPopupEventHelp>();
					NKCPopupEventHelp.m_Instance.Init();
				}
				return NKCPopupEventHelp.m_Instance;
			}
		}

		// Token: 0x06008B6B RID: 35691 RVA: 0x002F6887 File Offset: 0x002F4A87
		private static void CleanupInstance()
		{
			NKCPopupEventHelp.m_Instance = null;
		}

		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x06008B6C RID: 35692 RVA: 0x002F688F File Offset: 0x002F4A8F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupEventHelp.m_Instance != null && NKCPopupEventHelp.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008B6D RID: 35693 RVA: 0x002F68AA File Offset: 0x002F4AAA
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupEventHelp.m_Instance != null && NKCPopupEventHelp.m_Instance.IsOpen)
			{
				NKCPopupEventHelp.m_Instance.Close();
			}
		}

		// Token: 0x06008B6E RID: 35694 RVA: 0x002F68CF File Offset: 0x002F4ACF
		private void OnDestroy()
		{
			NKCPopupEventHelp.m_Instance = null;
		}

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06008B6F RID: 35695 RVA: 0x002F68D7 File Offset: 0x002F4AD7
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06008B70 RID: 35696 RVA: 0x002F68DA File Offset: 0x002F4ADA
		public override string MenuName
		{
			get
			{
				return "Help";
			}
		}

		// Token: 0x06008B71 RID: 35697 RVA: 0x002F68E1 File Offset: 0x002F4AE1
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008B72 RID: 35698 RVA: 0x002F68F0 File Offset: 0x002F4AF0
		private void Init()
		{
			if (this.m_close != null)
			{
				this.m_close.PointerClick.RemoveAllListeners();
				this.m_close.PointerClick.AddListener(new UnityAction(base.Close));
			}
			if (this.m_back != null)
			{
				this.m_back.PointerClick.RemoveAllListeners();
				this.m_back.PointerClick.AddListener(new UnityAction(base.Close));
			}
		}

		// Token: 0x06008B73 RID: 35699 RVA: 0x002F6974 File Offset: 0x002F4B74
		public void Open(int eventID)
		{
			NKMEventTabTemplet nkmeventTabTemplet = NKMTempletContainer<NKMEventTabTemplet>.Find(eventID);
			if (nkmeventTabTemplet == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_desc, NKCStringTable.GetString(nkmeventTabTemplet.m_EventHelpDesc, false));
			base.UIOpened(true);
		}

		// Token: 0x04007843 RID: 30787
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_EVENT";

		// Token: 0x04007844 RID: 30788
		public const string UI_ASSET_NAME = "NKM_UI_POPUP_EVENT_HELP";

		// Token: 0x04007845 RID: 30789
		private static NKCPopupEventHelp m_Instance;

		// Token: 0x04007846 RID: 30790
		public Text m_desc;

		// Token: 0x04007847 RID: 30791
		public NKCUIComStateButton m_close;

		// Token: 0x04007848 RID: 30792
		public NKCUIComButton m_back;
	}
}

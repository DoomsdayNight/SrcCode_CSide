using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI
{
	// Token: 0x02000A5C RID: 2652
	public class NKCPopupGameGradeCheck : NKCUIBase
	{
		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06007475 RID: 29813 RVA: 0x0026B920 File Offset: 0x00269B20
		public static NKCPopupGameGradeCheck Instance
		{
			get
			{
				if (NKCPopupGameGradeCheck.m_Instance == null)
				{
					NKCPopupGameGradeCheck.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGameGradeCheck>("AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX", "NKM_UI_POPUP_GAME_GRADE_CHECK", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGameGradeCheck.CleanupInstance)).GetInstance<NKCPopupGameGradeCheck>();
					NKCPopupGameGradeCheck.m_Instance.InitUI();
				}
				return NKCPopupGameGradeCheck.m_Instance;
			}
		}

		// Token: 0x06007476 RID: 29814 RVA: 0x0026B96F File Offset: 0x00269B6F
		public static void CheckInstanceAndClose()
		{
			if (NKCPopupGameGradeCheck.m_Instance != null && NKCPopupGameGradeCheck.m_Instance.IsOpen)
			{
				NKCPopupGameGradeCheck.m_Instance.Close();
			}
		}

		// Token: 0x06007477 RID: 29815 RVA: 0x0026B994 File Offset: 0x00269B94
		private static void CleanupInstance()
		{
			NKCPopupGameGradeCheck.m_Instance = null;
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06007478 RID: 29816 RVA: 0x0026B99C File Offset: 0x00269B9C
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06007479 RID: 29817 RVA: 0x0026B99F File Offset: 0x00269B9F
		public override string MenuName
		{
			get
			{
				return "PopupGameGradeCheck";
			}
		}

		// Token: 0x0600747A RID: 29818 RVA: 0x0026B9A8 File Offset: 0x00269BA8
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			this.m_BtnClose.PointerClick.RemoveAllListeners();
			this.m_BtnClose.PointerClick.AddListener(new UnityAction(base.Close));
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData e)
			{
				base.Close();
			});
			this.m_etBG.triggers.Add(entry);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600747B RID: 29819 RVA: 0x0026BA33 File Offset: 0x00269C33
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x0600747C RID: 29820 RVA: 0x0026BA53 File Offset: 0x00269C53
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x0600747D RID: 29821 RVA: 0x0026BA68 File Offset: 0x00269C68
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x040060D4 RID: 24788
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_POPUP_OK_CANCEL_BOX";

		// Token: 0x040060D5 RID: 24789
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_GAME_GRADE_CHECK";

		// Token: 0x040060D6 RID: 24790
		private static NKCPopupGameGradeCheck m_Instance;

		// Token: 0x040060D7 RID: 24791
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x040060D8 RID: 24792
		public NKCUIComStateButton m_BtnClose;

		// Token: 0x040060D9 RID: 24793
		public EventTrigger m_etBG;
	}
}

using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Worldmap
{
	// Token: 0x02000AB6 RID: 2742
	public class NKCPopupWorldmapEventOKCancel : NKCUIBase
	{
		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x06007A0D RID: 31245 RVA: 0x0028A1D2 File Offset: 0x002883D2
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x06007A0E RID: 31246 RVA: 0x0028A1D5 File Offset: 0x002883D5
		public override string MenuName
		{
			get
			{
				return "PopupWorldmapEventOKCancel";
			}
		}

		// Token: 0x06007A0F RID: 31247 RVA: 0x0028A1DC File Offset: 0x002883DC
		public void InitUI()
		{
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			this.m_csbtnOK.PointerClick.RemoveAllListeners();
			this.m_csbtnOK.PointerClick.AddListener(new UnityAction(this.OnClickOK));
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
			this.m_csbtnCancel.PointerClick.RemoveAllListeners();
			this.m_csbtnCancel.PointerClick.AddListener(new UnityAction(this.OnClickCancel));
			if (this.m_evtBG != null)
			{
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerClick;
				entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnClickBG));
				this.m_evtBG.triggers.Add(entry);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06007A10 RID: 31248 RVA: 0x0028A2AF File Offset: 0x002884AF
		private void OnClickBG(BaseEventData cBaseEventData)
		{
			base.Close();
		}

		// Token: 0x06007A11 RID: 31249 RVA: 0x0028A2B7 File Offset: 0x002884B7
		private void OnClickOK()
		{
			if (this.m_dOnClickOK != null)
			{
				this.m_dOnClickOK();
			}
		}

		// Token: 0x06007A12 RID: 31250 RVA: 0x0028A2CC File Offset: 0x002884CC
		private void OnClickCancel()
		{
			if (this.m_dOnClickCancel != null)
			{
				this.m_dOnClickCancel();
			}
		}

		// Token: 0x06007A13 RID: 31251 RVA: 0x0028A2E1 File Offset: 0x002884E1
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06007A14 RID: 31252 RVA: 0x0028A2F8 File Offset: 0x002884F8
		public void Open(string desc, int cityLevel, float cityExpPercent, string cityName, NKM_WORLDMAP_EVENT_TYPE eventType, int eventLevel, NKCPopupWorldmapEventOKCancel.OnClickOKOrCancel dOnClickOK, NKCPopupWorldmapEventOKCancel.OnClickOKOrCancel dOnClickCancel = null)
		{
			this.m_dOnClickOK = dOnClickOK;
			this.m_dOnClickCancel = dOnClickCancel;
			NKCUtil.SetLabelText(this.m_lbDesc, desc);
			NKCUtil.SetLabelText(this.m_lbCityLevel, cityLevel.ToString());
			NKCUtil.SetLabelText(this.m_lbCityName, cityName);
			this.m_imgCityExp.fillAmount = cityExpPercent;
			if (eventType == NKM_WORLDMAP_EVENT_TYPE.WET_RAID)
			{
				this.m_imgEventType.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_RENEWAL_SPRITE", "NKM_UI_WORLD_MAP_RENEWAL_EVENT_TYPE_ICON_RAID", false);
				NKCUtil.SetLabelText(this.m_lbEventLevel, string.Format(NKCUtilString.GET_STRING_WORLDMAP_EVENT_POPUP_OK_CANCEL_RAID_LEVEL, eventLevel));
			}
			else if (eventType == NKM_WORLDMAP_EVENT_TYPE.WET_DIVE)
			{
				this.m_imgEventType.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_WORLD_MAP_RENEWAL_SPRITE", "NKM_UI_WORLD_MAP_RENEWAL_EVENT_TYPE_ICON_DIVE", false);
				NKCUtil.SetLabelText(this.m_lbEventLevel, string.Format(NKCUtilString.GET_STRING_WORLDMAP_EVENT_POPUP_OK_CANCEL_DIVE_LEVEL, eventLevel));
			}
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x06007A15 RID: 31253 RVA: 0x0028A3D7 File Offset: 0x002885D7
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x040066C0 RID: 26304
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_WORLD_MAP_RENEWAL";

		// Token: 0x040066C1 RID: 26305
		public const string UI_ASSET_NAME = "NKM_UI_WORLD_MAP_POPUP_EventOKCancel";

		// Token: 0x040066C2 RID: 26306
		public EventTrigger m_evtBG;

		// Token: 0x040066C3 RID: 26307
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x040066C4 RID: 26308
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x040066C5 RID: 26309
		public Text m_lbDesc;

		// Token: 0x040066C6 RID: 26310
		public Text m_lbCityLevel;

		// Token: 0x040066C7 RID: 26311
		public Image m_imgCityExp;

		// Token: 0x040066C8 RID: 26312
		public Text m_lbCityName;

		// Token: 0x040066C9 RID: 26313
		public Image m_imgEventType;

		// Token: 0x040066CA RID: 26314
		public Text m_lbEventLevel;

		// Token: 0x040066CB RID: 26315
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x040066CC RID: 26316
		private NKCPopupWorldmapEventOKCancel.OnClickOKOrCancel m_dOnClickOK;

		// Token: 0x040066CD RID: 26317
		private NKCPopupWorldmapEventOKCancel.OnClickOKOrCancel m_dOnClickCancel;

		// Token: 0x02001813 RID: 6163
		// (Invoke) Token: 0x0600B509 RID: 46345
		public delegate void OnClickOKOrCancel();
	}
}

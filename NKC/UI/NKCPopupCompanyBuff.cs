using System;
using System.Collections.Generic;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A36 RID: 2614
	public class NKCPopupCompanyBuff : NKCUIBase
	{
		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06007276 RID: 29302 RVA: 0x0026072C File Offset: 0x0025E92C
		public static NKCPopupCompanyBuff Instance
		{
			get
			{
				if (NKCPopupCompanyBuff.m_Instance == null)
				{
					NKCPopupCompanyBuff.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupCompanyBuff>("ab_ui_nkm_ui_popup_ok_cancel_box", "NKM_UI_POPUP_EVENTBUFF", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupCompanyBuff.CleanupInstance)).GetInstance<NKCPopupCompanyBuff>();
					NKCPopupCompanyBuff.m_Instance.InitUI();
				}
				return NKCPopupCompanyBuff.m_Instance;
			}
		}

		// Token: 0x06007277 RID: 29303 RVA: 0x0026077B File Offset: 0x0025E97B
		private static void CleanupInstance()
		{
			NKCPopupCompanyBuff.m_Instance = null;
		}

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06007278 RID: 29304 RVA: 0x00260783 File Offset: 0x0025E983
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06007279 RID: 29305 RVA: 0x00260786 File Offset: 0x0025E986
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x0600727A RID: 29306 RVA: 0x00260790 File Offset: 0x0025E990
		private void InitUI()
		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerDown;
			entry.callback = new EventTrigger.TriggerEvent();
			entry.callback.AddListener(new UnityAction<BaseEventData>(this.OnClickBG));
			EventTrigger eventTrigger = this.m_objBG.GetComponent<EventTrigger>();
			if (eventTrigger == null)
			{
				eventTrigger = this.m_objBG.AddComponent<EventTrigger>();
			}
			eventTrigger.triggers.Add(entry);
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_loopScoll.dOnGetObject += this.GetObject;
			this.m_loopScoll.dOnReturnObject += this.ReturnObject;
			this.m_loopScoll.dOnProvideData += this.ProvideData;
			this.m_loopScoll.ContentConstraintCount = 1;
			this.m_loopScoll.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loopScoll, null);
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
		}

		// Token: 0x0600727B RID: 29307 RVA: 0x0026089F File Offset: 0x0025EA9F
		public void Open()
		{
			this.RefreshList(true);
			this.m_NKCUIOpenAnimator.PlayOpenAni();
			base.UIOpened(true);
		}

		// Token: 0x0600727C RID: 29308 RVA: 0x002608BA File Offset: 0x0025EABA
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600727D RID: 29309 RVA: 0x002608C8 File Offset: 0x0025EAC8
		private int Compare(NKMCompanyBuffData buffA, NKMCompanyBuffData buffB)
		{
			return buffA.ExpireTicks.CompareTo(buffB.ExpireTicks);
		}

		// Token: 0x0600727E RID: 29310 RVA: 0x002608EC File Offset: 0x0025EAEC
		private void RefreshList(bool bOpen = false)
		{
			IReadOnlyList<NKMCompanyBuffData> companyBuffDataList = NKCScenManager.CurrentUserData().m_companyBuffDataList;
			this.m_lstBuff = new List<NKMCompanyBuffData>();
			this.m_lstBuff.AddRange(companyBuffDataList);
			this.m_lstBuff.Sort(new Comparison<NKMCompanyBuffData>(this.Compare));
			if (!bOpen && this.m_lstBuff.Count == 0)
			{
				base.Close();
				return;
			}
			this.m_loopScoll.TotalCount = this.m_lstBuff.Count;
			this.m_loopScoll.RefreshCells(true);
		}

		// Token: 0x0600727F RID: 29311 RVA: 0x0026096B File Offset: 0x0025EB6B
		private void OnClickBG(BaseEventData eventData)
		{
			base.Close();
		}

		// Token: 0x06007280 RID: 29312 RVA: 0x00260973 File Offset: 0x0025EB73
		public void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
			}
		}

		// Token: 0x06007281 RID: 29313 RVA: 0x00260988 File Offset: 0x0025EB88
		private RectTransform GetObject(int index)
		{
			NKCPopupCompanyBuffSlot newInstance = NKCPopupCompanyBuffSlot.GetNewInstance(this.m_trContent);
			if (newInstance == null)
			{
				return null;
			}
			newInstance.gameObject.transform.localPosition = Vector3.zero;
			newInstance.gameObject.transform.localScale = Vector3.one;
			return newInstance.GetComponent<RectTransform>();
		}

		// Token: 0x06007282 RID: 29314 RVA: 0x002609DC File Offset: 0x0025EBDC
		private void ReturnObject(Transform tr)
		{
			NKCPopupCompanyBuffSlot component = tr.GetComponent<NKCPopupCompanyBuffSlot>();
			tr.SetParent(base.transform);
			if (component != null)
			{
				component.DestoryInstance();
				return;
			}
			UnityEngine.Object.Destroy(tr.gameObject);
		}

		// Token: 0x06007283 RID: 29315 RVA: 0x00260A18 File Offset: 0x0025EC18
		private void ProvideData(Transform transform, int idx)
		{
			if (idx < 0)
			{
				NKCUtil.SetGameobjectActive(transform, false);
				return;
			}
			if (idx >= this.m_lstBuff.Count)
			{
				NKCUtil.SetGameobjectActive(transform, false);
				return;
			}
			NKCPopupCompanyBuffSlot component = transform.GetComponent<NKCPopupCompanyBuffSlot>();
			if (component != null)
			{
				component.SetData(this.m_lstBuff[idx], new NKCPopupCompanyBuffSlot.OnExpire(this.RefreshList));
			}
		}

		// Token: 0x04005E58 RID: 24152
		private const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_popup_ok_cancel_box";

		// Token: 0x04005E59 RID: 24153
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_EVENTBUFF";

		// Token: 0x04005E5A RID: 24154
		private static NKCPopupCompanyBuff m_Instance;

		// Token: 0x04005E5B RID: 24155
		public GameObject m_objBG;

		// Token: 0x04005E5C RID: 24156
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04005E5D RID: 24157
		public LoopScrollFlexibleRect m_loopScoll;

		// Token: 0x04005E5E RID: 24158
		public Transform m_trContent;

		// Token: 0x04005E5F RID: 24159
		private List<NKMCompanyBuffData> m_lstBuff = new List<NKMCompanyBuffData>();

		// Token: 0x04005E60 RID: 24160
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;
	}
}

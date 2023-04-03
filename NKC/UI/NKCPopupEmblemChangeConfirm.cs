using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A3F RID: 2623
	public class NKCPopupEmblemChangeConfirm : NKCUIBase
	{
		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x060072F6 RID: 29430 RVA: 0x0026389E File Offset: 0x00261A9E
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x060072F7 RID: 29431 RVA: 0x002638A1 File Offset: 0x00261AA1
		public override string MenuName
		{
			get
			{
				return "PopupEmblemChangeConfirm";
			}
		}

		// Token: 0x060072F8 RID: 29432 RVA: 0x002638A8 File Offset: 0x00261AA8
		public void InitUI()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCUtil.SetBindFunction(this.m_csbtnOK, new UnityAction(this.OnClickOK));
			NKCUtil.SetHotkey(this.m_csbtnOK, HotkeyEventType.Confirm, null, false);
			NKCUtil.SetBindFunction(this.m_csbtnCancel, new UnityAction(this.OnCloseBtn));
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(delegate(BaseEventData e)
			{
				this.OnCloseBtn();
			});
			this.m_etBG.triggers.Add(entry);
		}

		// Token: 0x060072F9 RID: 29433 RVA: 0x00263932 File Offset: 0x00261B32
		private void OnClickOK()
		{
			if (this.m_DestEmblemID != -1 && this.m_dOnClickOK != null)
			{
				this.m_dOnClickOK(this.m_DestEmblemID);
			}
			this.m_dOnClickOK = null;
			base.Close();
		}

		// Token: 0x060072FA RID: 29434 RVA: 0x00263964 File Offset: 0x00261B64
		public void Open(int srcEmblemID, int destEmblemID, NKCPopupEmblemChangeConfirm.dOnClickOK _dOnClickOK = null)
		{
			NKCUtil.SetGameobjectActive(this.m_NKCPopupEmblemBigSlotDest, true);
			if (destEmblemID == 0)
			{
				NKCUtil.SetGameobjectActive(this.m_NKCPopupEmblemBigSlotSrc, true);
				NKCUtil.SetGameobjectActive(this.m_objArrow, true);
				this.m_NKCPopupEmblemBigSlotSrc.SetData(srcEmblemID);
				this.m_NKCPopupEmblemBigSlotDest.SetEmpty(NKCUtilString.GET_STRING_EMBLEM_EQUIPPED_EMBLEM_UNEQUIP);
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_EMBLEM_EQUIPPED_EMBLEM_UNEQUIP_CONFIRM);
			}
			else if (srcEmblemID == 0 || srcEmblemID == -1)
			{
				NKCUtil.SetGameobjectActive(this.m_NKCPopupEmblemBigSlotSrc, false);
				NKCUtil.SetGameobjectActive(this.m_objArrow, false);
				this.m_NKCPopupEmblemBigSlotDest.SetData(destEmblemID);
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_EMBLEM_EQUIP_CONFIRM);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKCPopupEmblemBigSlotSrc, true);
				NKCUtil.SetGameobjectActive(this.m_objArrow, true);
				this.m_NKCPopupEmblemBigSlotSrc.SetData(srcEmblemID);
				this.m_NKCPopupEmblemBigSlotDest.SetData(destEmblemID);
				NKCUtil.SetLabelText(this.m_lbTitle, NKCUtilString.GET_STRING_EMBLEM_EQUIPPED_EMBLEM_CHANGE_CONFIRM);
			}
			this.m_DestEmblemID = destEmblemID;
			this.m_dOnClickOK = _dOnClickOK;
			base.UIOpened(true);
		}

		// Token: 0x060072FB RID: 29435 RVA: 0x00263A58 File Offset: 0x00261C58
		public void CloseEmblemChangeConfirmPopup()
		{
			base.Close();
		}

		// Token: 0x060072FC RID: 29436 RVA: 0x00263A60 File Offset: 0x00261C60
		public void OnCloseBtn()
		{
			base.Close();
		}

		// Token: 0x060072FD RID: 29437 RVA: 0x00263A68 File Offset: 0x00261C68
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060072FE RID: 29438 RVA: 0x00263A76 File Offset: 0x00261C76
		private void OnDestroy()
		{
		}

		// Token: 0x04005EF0 RID: 24304
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_FRIEND";

		// Token: 0x04005EF1 RID: 24305
		public const string UI_ASSET_NAME = "NKM_UI_POPUP_EMBLEM_CONFIRM";

		// Token: 0x04005EF2 RID: 24306
		public Text m_lbTitle;

		// Token: 0x04005EF3 RID: 24307
		public NKCUIComStateButton m_csbtnOK;

		// Token: 0x04005EF4 RID: 24308
		public NKCUIComStateButton m_csbtnCancel;

		// Token: 0x04005EF5 RID: 24309
		public EventTrigger m_etBG;

		// Token: 0x04005EF6 RID: 24310
		public NKCPopupEmblemBigSlot m_NKCPopupEmblemBigSlotSrc;

		// Token: 0x04005EF7 RID: 24311
		public NKCPopupEmblemBigSlot m_NKCPopupEmblemBigSlotDest;

		// Token: 0x04005EF8 RID: 24312
		public GameObject m_objArrow;

		// Token: 0x04005EF9 RID: 24313
		private int m_DestEmblemID = -1;

		// Token: 0x04005EFA RID: 24314
		private NKCPopupEmblemChangeConfirm.dOnClickOK m_dOnClickOK;

		// Token: 0x02001783 RID: 6019
		// (Invoke) Token: 0x0600B388 RID: 45960
		public delegate void dOnClickOK(int id);
	}
}

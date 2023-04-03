using System;
using System.Collections.Generic;
using System.Linq;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B3E RID: 2878
	public class NKCPopupGuildBadgeSetting : NKCUIBase
	{
		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x06008303 RID: 33539 RVA: 0x002C2F40 File Offset: 0x002C1140
		public static NKCPopupGuildBadgeSetting Instance
		{
			get
			{
				if (NKCPopupGuildBadgeSetting.m_Instance == null)
				{
					NKCPopupGuildBadgeSetting.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildBadgeSetting>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_POPUP_MARK_SETTING", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGuildBadgeSetting.CleanupInstance)).GetInstance<NKCPopupGuildBadgeSetting>();
					if (NKCPopupGuildBadgeSetting.m_Instance != null)
					{
						NKCPopupGuildBadgeSetting.m_Instance.InitUI();
					}
				}
				return NKCPopupGuildBadgeSetting.m_Instance;
			}
		}

		// Token: 0x06008304 RID: 33540 RVA: 0x002C2FA1 File Offset: 0x002C11A1
		private static void CleanupInstance()
		{
			NKCPopupGuildBadgeSetting.m_Instance = null;
		}

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x06008305 RID: 33541 RVA: 0x002C2FA9 File Offset: 0x002C11A9
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildBadgeSetting.m_Instance != null && NKCPopupGuildBadgeSetting.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008306 RID: 33542 RVA: 0x002C2FC4 File Offset: 0x002C11C4
		private void OnDestroy()
		{
			NKCPopupGuildBadgeSetting.m_Instance = null;
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x06008307 RID: 33543 RVA: 0x002C2FCC File Offset: 0x002C11CC
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x06008308 RID: 33544 RVA: 0x002C2FD3 File Offset: 0x002C11D3
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x06008309 RID: 33545 RVA: 0x002C2FD8 File Offset: 0x002C11D8
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			for (int i = 0; i < this.m_lstVisibleSlot.Count; i++)
			{
				this.m_stkSlot.Push(this.m_lstVisibleSlot[i]);
			}
			this.m_lstVisibleSlot.Clear();
		}

		// Token: 0x0600830A RID: 33546 RVA: 0x002C302C File Offset: 0x002C122C
		public RectTransform GetObjectFrame(int index)
		{
			NKCUIGuildBadgeSlot nkcuiguildBadgeSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcuiguildBadgeSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcuiguildBadgeSlot = NKCUIGuildBadgeSlot.GetNewInstance(this.m_trFrameContentsParent, this.m_tglGroupFrame, new NKCUIGuildBadgeSlot.OnSelectedSlot(this.OnChangeFrame));
			}
			this.m_lstVisibleSlot.Add(nkcuiguildBadgeSlot);
			NKCUtil.SetGameobjectActive(nkcuiguildBadgeSlot, false);
			if (nkcuiguildBadgeSlot == null)
			{
				return null;
			}
			return nkcuiguildBadgeSlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600830B RID: 33547 RVA: 0x002C3094 File Offset: 0x002C1294
		public void ReturnObjectFrame(Transform tr)
		{
			NKCUIGuildBadgeSlot component = tr.GetComponent<NKCUIGuildBadgeSlot>();
			this.m_lstVisibleSlot.Remove(component);
			this.m_stkSlot.Push(component);
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
		}

		// Token: 0x0600830C RID: 33548 RVA: 0x002C30D4 File Offset: 0x002C12D4
		public void ProvideDataFrame(Transform tr, int idx)
		{
			NKCUIGuildBadgeSlot component = tr.GetComponent<NKCUIGuildBadgeSlot>();
			if (component == null)
			{
				return;
			}
			component.SetData(NKCGuildManager.GetFrameTempletByIndex(idx));
			if (component.m_slotId == this.m_GuildBadgeInfo.FrameId)
			{
				component.m_tgl.Select(true, false, false);
				return;
			}
			component.m_tgl.Select(false, false, false);
		}

		// Token: 0x0600830D RID: 33549 RVA: 0x002C3130 File Offset: 0x002C1330
		public RectTransform GetObjectFrameColor(int index)
		{
			NKCUIGuildBadgeSlot nkcuiguildBadgeSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcuiguildBadgeSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcuiguildBadgeSlot = NKCUIGuildBadgeSlot.GetNewInstance(this.m_trFrameContentsParent, this.m_tglGroupFrameColor, new NKCUIGuildBadgeSlot.OnSelectedSlot(this.OnChangeFrameColor));
			}
			this.m_lstVisibleSlot.Add(nkcuiguildBadgeSlot);
			NKCUtil.SetGameobjectActive(nkcuiguildBadgeSlot, false);
			if (nkcuiguildBadgeSlot == null)
			{
				return null;
			}
			return nkcuiguildBadgeSlot.GetComponent<RectTransform>();
		}

		// Token: 0x0600830E RID: 33550 RVA: 0x002C3198 File Offset: 0x002C1398
		public void ReturnObjectFrameColor(Transform tr)
		{
			NKCUIGuildBadgeSlot component = tr.GetComponent<NKCUIGuildBadgeSlot>();
			this.m_lstVisibleSlot.Remove(component);
			this.m_stkSlot.Push(component);
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
		}

		// Token: 0x0600830F RID: 33551 RVA: 0x002C31D8 File Offset: 0x002C13D8
		public void ProvideDataFrameColor(Transform tr, int idx)
		{
			NKCUIGuildBadgeSlot component = tr.GetComponent<NKCUIGuildBadgeSlot>();
			if (component == null)
			{
				return;
			}
			component.SetData(NKCGuildManager.GetBadgeColorTempletByIndex(idx));
			if (component.m_slotId == this.m_GuildBadgeInfo.FrameColorId)
			{
				component.m_tgl.Select(true, false, false);
				return;
			}
			component.m_tgl.Select(false, false, false);
		}

		// Token: 0x06008310 RID: 33552 RVA: 0x002C3234 File Offset: 0x002C1434
		public RectTransform GetObjectMark(int index)
		{
			NKCUIGuildBadgeSlot nkcuiguildBadgeSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcuiguildBadgeSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcuiguildBadgeSlot = NKCUIGuildBadgeSlot.GetNewInstance(this.m_trFrameContentsParent, this.m_tglGroupMark, new NKCUIGuildBadgeSlot.OnSelectedSlot(this.OnChangeMark));
			}
			this.m_lstVisibleSlot.Add(nkcuiguildBadgeSlot);
			NKCUtil.SetGameobjectActive(nkcuiguildBadgeSlot, false);
			if (nkcuiguildBadgeSlot == null)
			{
				return null;
			}
			return nkcuiguildBadgeSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008311 RID: 33553 RVA: 0x002C329C File Offset: 0x002C149C
		public void ReturnObjectMark(Transform tr)
		{
			NKCUIGuildBadgeSlot component = tr.GetComponent<NKCUIGuildBadgeSlot>();
			this.m_lstVisibleSlot.Remove(component);
			this.m_stkSlot.Push(component);
		}

		// Token: 0x06008312 RID: 33554 RVA: 0x002C32CC File Offset: 0x002C14CC
		public void ProvideDataMark(Transform tr, int idx)
		{
			NKCUIGuildBadgeSlot component = tr.GetComponent<NKCUIGuildBadgeSlot>();
			if (component == null)
			{
				return;
			}
			component.SetData(NKCGuildManager.GetMarkTempletByIndex(idx));
			if (component.m_slotId == this.m_GuildBadgeInfo.MarkId)
			{
				component.m_tgl.Select(true, false, false);
				return;
			}
			component.m_tgl.Select(false, false, false);
		}

		// Token: 0x06008313 RID: 33555 RVA: 0x002C3328 File Offset: 0x002C1528
		public RectTransform GetObjectMarkColor(int index)
		{
			NKCUIGuildBadgeSlot nkcuiguildBadgeSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcuiguildBadgeSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcuiguildBadgeSlot = NKCUIGuildBadgeSlot.GetNewInstance(this.m_trFrameContentsParent, this.m_tglGroupMarkColor, new NKCUIGuildBadgeSlot.OnSelectedSlot(this.OnChangeMarkColor));
			}
			this.m_lstVisibleSlot.Add(nkcuiguildBadgeSlot);
			NKCUtil.SetGameobjectActive(nkcuiguildBadgeSlot, false);
			if (nkcuiguildBadgeSlot == null)
			{
				return null;
			}
			return nkcuiguildBadgeSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008314 RID: 33556 RVA: 0x002C3390 File Offset: 0x002C1590
		public void ReturnObjectMarkColor(Transform tr)
		{
			NKCUIGuildBadgeSlot component = tr.GetComponent<NKCUIGuildBadgeSlot>();
			this.m_lstVisibleSlot.Remove(component);
			this.m_stkSlot.Push(component);
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.transform);
		}

		// Token: 0x06008315 RID: 33557 RVA: 0x002C33D0 File Offset: 0x002C15D0
		public void ProvideDataMarkColor(Transform tr, int idx)
		{
			NKCUIGuildBadgeSlot component = tr.GetComponent<NKCUIGuildBadgeSlot>();
			if (component == null)
			{
				return;
			}
			component.SetData(NKCGuildManager.GetBadgeColorTempletByIndex(idx));
			if (component.m_slotId == this.m_GuildBadgeInfo.MarkColorId)
			{
				component.m_tgl.Select(true, false, false);
				return;
			}
			component.m_tgl.Select(false, false, false);
		}

		// Token: 0x06008316 RID: 33558 RVA: 0x002C342C File Offset: 0x002C162C
		public void InitUI()
		{
			this.m_BadgeUI.InitUI();
			this.m_btnOK.PointerClick.RemoveAllListeners();
			this.m_btnOK.PointerClick.AddListener(new UnityAction(this.OnClickOk));
			NKCUtil.SetHotkey(this.m_btnOK, HotkeyEventType.Confirm, null, false);
			this.m_btnCancel.PointerClick.RemoveAllListeners();
			this.m_btnCancel.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_loopFrame.dOnGetObject += this.GetObjectFrame;
			this.m_loopFrame.dOnReturnObject += this.ReturnObjectFrame;
			this.m_loopFrame.dOnProvideData += this.ProvideDataFrame;
			this.m_loopFrame.PrepareCells(0);
			this.m_loopFrameColor.dOnGetObject += this.GetObjectFrameColor;
			this.m_loopFrameColor.dOnReturnObject += this.ReturnObjectFrameColor;
			this.m_loopFrameColor.dOnProvideData += this.ProvideDataFrameColor;
			this.m_loopFrameColor.PrepareCells(0);
			this.m_loopMark.dOnGetObject += this.GetObjectMark;
			this.m_loopMark.dOnReturnObject += this.ReturnObjectMark;
			this.m_loopMark.dOnProvideData += this.ProvideDataMark;
			this.m_loopMark.PrepareCells(0);
			this.m_loopMarkColor.dOnGetObject += this.GetObjectMarkColor;
			this.m_loopMarkColor.dOnReturnObject += this.ReturnObjectMarkColor;
			this.m_loopMarkColor.dOnProvideData += this.ProvideDataMarkColor;
			this.m_loopMarkColor.PrepareCells(0);
			this.m_GuildBadgeInfo = new GuildBadgeInfo(0L);
		}

		// Token: 0x06008317 RID: 33559 RVA: 0x002C35FC File Offset: 0x002C17FC
		public void Open(NKCPopupGuildBadgeSetting.OnClose onClose, long badgeId = 0L)
		{
			this.m_dOnClose = onClose;
			this.m_GuildBadgeInfo = new GuildBadgeInfo(badgeId);
			this.m_BadgeUI.SetData(this.m_GuildBadgeInfo);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loopFrame.TotalCount = NKMTempletContainer<NKMGuildBadgeFrameTemplet>.Values.Count<NKMGuildBadgeFrameTemplet>();
			this.m_loopFrame.RefreshCells(false);
			this.m_loopFrameColor.TotalCount = NKMTempletContainer<NKMGuildBadgeColorTemplet>.Values.Count<NKMGuildBadgeColorTemplet>();
			this.m_loopFrameColor.RefreshCells(false);
			this.m_loopMark.TotalCount = NKMTempletContainer<NKMGuildBadgeMarkTemplet>.Values.Count<NKMGuildBadgeMarkTemplet>();
			this.m_loopMark.RefreshCells(false);
			this.m_loopMarkColor.TotalCount = NKMTempletContainer<NKMGuildBadgeColorTemplet>.Values.Count<NKMGuildBadgeColorTemplet>();
			this.m_loopMarkColor.RefreshCells(false);
			base.UIOpened(true);
		}

		// Token: 0x06008318 RID: 33560 RVA: 0x002C36C4 File Offset: 0x002C18C4
		private void OnChangeFrame(int frameId)
		{
			if (frameId > 0)
			{
				this.m_GuildBadgeInfo = new GuildBadgeInfo(frameId, this.m_GuildBadgeInfo.FrameColorId, this.m_GuildBadgeInfo.MarkId, this.m_GuildBadgeInfo.MarkColorId);
				this.m_BadgeUI.SetData(this.m_GuildBadgeInfo);
			}
		}

		// Token: 0x06008319 RID: 33561 RVA: 0x002C3714 File Offset: 0x002C1914
		private void OnChangeFrameColor(int frameColorId)
		{
			if (frameColorId > 0)
			{
				this.m_GuildBadgeInfo = new GuildBadgeInfo(this.m_GuildBadgeInfo.FrameId, frameColorId, this.m_GuildBadgeInfo.MarkId, this.m_GuildBadgeInfo.MarkColorId);
				this.m_BadgeUI.SetData(this.m_GuildBadgeInfo);
			}
		}

		// Token: 0x0600831A RID: 33562 RVA: 0x002C3764 File Offset: 0x002C1964
		private void OnChangeMark(int markId)
		{
			if (markId > 0)
			{
				this.m_GuildBadgeInfo = new GuildBadgeInfo(this.m_GuildBadgeInfo.FrameId, this.m_GuildBadgeInfo.FrameColorId, markId, this.m_GuildBadgeInfo.MarkColorId);
				this.m_BadgeUI.SetData(this.m_GuildBadgeInfo);
			}
		}

		// Token: 0x0600831B RID: 33563 RVA: 0x002C37B4 File Offset: 0x002C19B4
		private void OnChangeMarkColor(int markColorId)
		{
			if (markColorId > 0)
			{
				this.m_GuildBadgeInfo = new GuildBadgeInfo(this.m_GuildBadgeInfo.FrameId, this.m_GuildBadgeInfo.FrameColorId, this.m_GuildBadgeInfo.MarkId, markColorId);
				this.m_BadgeUI.SetData(this.m_GuildBadgeInfo);
			}
		}

		// Token: 0x0600831C RID: 33564 RVA: 0x002C3803 File Offset: 0x002C1A03
		public void OnClickOk()
		{
			if (this.m_GuildBadgeInfo.BadgeId > 0L)
			{
				NKCPopupGuildBadgeSetting.OnClose dOnClose = this.m_dOnClose;
				if (dOnClose != null)
				{
					dOnClose(this.m_GuildBadgeInfo.BadgeId);
				}
				base.Close();
			}
		}

		// Token: 0x04006F3C RID: 28476
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM";

		// Token: 0x04006F3D RID: 28477
		private const string UI_ASSET_NAME = "NKM_UI_CONSORTIUM_POPUP_MARK_SETTING";

		// Token: 0x04006F3E RID: 28478
		private static NKCPopupGuildBadgeSetting m_Instance;

		// Token: 0x04006F3F RID: 28479
		public NKCUIGuildBadge m_BadgeUI;

		// Token: 0x04006F40 RID: 28480
		public LoopScrollRect m_loopFrame;

		// Token: 0x04006F41 RID: 28481
		public Transform m_trFrameContentsParent;

		// Token: 0x04006F42 RID: 28482
		public NKCUIComToggleGroup m_tglGroupFrame;

		// Token: 0x04006F43 RID: 28483
		public LoopScrollRect m_loopFrameColor;

		// Token: 0x04006F44 RID: 28484
		public Transform m_trFrameColorContentsParent;

		// Token: 0x04006F45 RID: 28485
		public NKCUIComToggleGroup m_tglGroupFrameColor;

		// Token: 0x04006F46 RID: 28486
		public LoopScrollRect m_loopMark;

		// Token: 0x04006F47 RID: 28487
		public Transform m_trMarkContentsParent;

		// Token: 0x04006F48 RID: 28488
		public NKCUIComToggleGroup m_tglGroupMark;

		// Token: 0x04006F49 RID: 28489
		public LoopScrollRect m_loopMarkColor;

		// Token: 0x04006F4A RID: 28490
		public Transform m_trMarkColorContentsParent;

		// Token: 0x04006F4B RID: 28491
		public NKCUIComToggleGroup m_tglGroupMarkColor;

		// Token: 0x04006F4C RID: 28492
		public NKCUIComStateButton m_btnOK;

		// Token: 0x04006F4D RID: 28493
		public NKCUIComStateButton m_btnCancel;

		// Token: 0x04006F4E RID: 28494
		private Stack<NKCUIGuildBadgeSlot> m_stkSlot = new Stack<NKCUIGuildBadgeSlot>();

		// Token: 0x04006F4F RID: 28495
		private List<NKCUIGuildBadgeSlot> m_lstVisibleSlot = new List<NKCUIGuildBadgeSlot>();

		// Token: 0x04006F50 RID: 28496
		private NKCPopupGuildBadgeSetting.OnClose m_dOnClose;

		// Token: 0x04006F51 RID: 28497
		private GuildBadgeInfo m_GuildBadgeInfo;

		// Token: 0x020018D9 RID: 6361
		// (Invoke) Token: 0x0600B6EB RID: 46827
		public delegate void OnClose(long badgeId);
	}
}

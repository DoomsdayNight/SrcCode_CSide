using System;
using System.Collections.Generic;
using ClientPacket.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B32 RID: 2866
	public class NKCPopupGuildCoopStatus : NKCUIBase
	{
		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x0600827C RID: 33404 RVA: 0x002C06AC File Offset: 0x002BE8AC
		public static NKCPopupGuildCoopStatus Instance
		{
			get
			{
				if (NKCPopupGuildCoopStatus.m_Instance == null)
				{
					NKCPopupGuildCoopStatus.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildCoopStatus>("AB_UI_NKM_UI_CONSORTIUM_COOP", "NKM_UI_POPUP_CONSORTIUM_COOP_STATUS", NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null).GetInstance<NKCPopupGuildCoopStatus>();
					if (NKCPopupGuildCoopStatus.m_Instance != null)
					{
						NKCPopupGuildCoopStatus.m_Instance.InitUI();
					}
				}
				return NKCPopupGuildCoopStatus.m_Instance;
			}
		}

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x0600827D RID: 33405 RVA: 0x002C0702 File Offset: 0x002BE902
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildCoopStatus.m_Instance != null && NKCPopupGuildCoopStatus.m_Instance.IsOpen;
			}
		}

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x0600827E RID: 33406 RVA: 0x002C071D File Offset: 0x002BE91D
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x0600827F RID: 33407 RVA: 0x002C0720 File Offset: 0x002BE920
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06008280 RID: 33408 RVA: 0x002C0728 File Offset: 0x002BE928
		public void InitUI()
		{
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			this.m_loop.dOnGetObject += this.GetObject;
			this.m_loop.dOnReturnObject += this.ReturnObject;
			this.m_loop.dOnProvideData += this.ProvideData;
			this.m_loop.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loop, null);
		}

		// Token: 0x06008281 RID: 33409 RVA: 0x002C07BE File Offset: 0x002BE9BE
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008282 RID: 33410 RVA: 0x002C07CC File Offset: 0x002BE9CC
		private void OnDestroy()
		{
			NKCPopupGuildCoopStatus.m_Instance = null;
		}

		// Token: 0x06008283 RID: 33411 RVA: 0x002C07D4 File Offset: 0x002BE9D4
		private RectTransform GetObject(int idx)
		{
			NKCPopupGuildCoopStatusSlot nkcpopupGuildCoopStatusSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcpopupGuildCoopStatusSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcpopupGuildCoopStatusSlot = UnityEngine.Object.Instantiate<NKCPopupGuildCoopStatusSlot>(this.m_pfbSlot);
				nkcpopupGuildCoopStatusSlot.InitUI();
			}
			nkcpopupGuildCoopStatusSlot.transform.SetParent(this.m_trSlotParent);
			return nkcpopupGuildCoopStatusSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008284 RID: 33412 RVA: 0x002C0828 File Offset: 0x002BEA28
		private void ReturnObject(Transform tr)
		{
			NKCPopupGuildCoopStatusSlot component = tr.GetComponent<NKCPopupGuildCoopStatusSlot>();
			if (component == null)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			NKCUtil.SetGameobjectActive(component, false);
			component.transform.SetParent(base.transform);
			this.m_stkSlot.Push(component);
		}

		// Token: 0x06008285 RID: 33413 RVA: 0x002C0874 File Offset: 0x002BEA74
		private void ProvideData(Transform tr, int idx)
		{
			NKCPopupGuildCoopStatusSlot component = tr.GetComponent<NKCPopupGuildCoopStatusSlot>();
			if (component == null || this.m_lstMemberInfo.Count < idx)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			component.transform.SetParent(this.m_trSlotParent);
			NKCUtil.SetGameobjectActive(component, true);
			component.SetData(this.m_lstMemberInfo[idx], idx + 1);
		}

		// Token: 0x06008286 RID: 33414 RVA: 0x002C08D4 File Offset: 0x002BEAD4
		public void Open()
		{
			NKCUtil.SetImageSprite(this.m_imgTopBanner, null, false);
			this.m_lstMemberInfo = NKCGuildCoopManager.GetGuildMemberInfo();
			this.m_lstMemberInfo.Sort(new Comparison<GuildDungeonMemberInfo>(NKCGuildCoopManager.CompMember));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			base.UIOpened(true);
			this.RefreshUI(true);
		}

		// Token: 0x06008287 RID: 33415 RVA: 0x002C092A File Offset: 0x002BEB2A
		public void RefreshUI(bool bResetScroll = false)
		{
			this.m_loop.TotalCount = this.m_lstMemberInfo.Count;
			if (bResetScroll)
			{
				this.m_loop.SetIndexPosition(0);
				return;
			}
			this.m_loop.RefreshCells(false);
		}

		// Token: 0x04006EB4 RID: 28340
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM_COOP";

		// Token: 0x04006EB5 RID: 28341
		private const string UI_ASSET_NAME = "NKM_UI_POPUP_CONSORTIUM_COOP_STATUS";

		// Token: 0x04006EB6 RID: 28342
		private static NKCPopupGuildCoopStatus m_Instance;

		// Token: 0x04006EB7 RID: 28343
		public NKCPopupGuildCoopStatusSlot m_pfbSlot;

		// Token: 0x04006EB8 RID: 28344
		public Image m_imgTopBanner;

		// Token: 0x04006EB9 RID: 28345
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04006EBA RID: 28346
		public LoopScrollRect m_loop;

		// Token: 0x04006EBB RID: 28347
		public Transform m_trSlotParent;

		// Token: 0x04006EBC RID: 28348
		private Stack<NKCPopupGuildCoopStatusSlot> m_stkSlot = new Stack<NKCPopupGuildCoopStatusSlot>();

		// Token: 0x04006EBD RID: 28349
		private List<GuildDungeonMemberInfo> m_lstMemberInfo = new List<GuildDungeonMemberInfo>();
	}
}

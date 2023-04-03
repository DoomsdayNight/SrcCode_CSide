using System;
using System.Collections.Generic;
using Cs.Core.Util;
using NKM.Guild;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Guild
{
	// Token: 0x02000B46 RID: 2886
	public class NKCPopupGuildRankSeasonSelect : NKCUIBase
	{
		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06008371 RID: 33649 RVA: 0x002C4CD0 File Offset: 0x002C2ED0
		public static NKCPopupGuildRankSeasonSelect Instance
		{
			get
			{
				if (NKCPopupGuildRankSeasonSelect.m_Instance == null)
				{
					NKCPopupGuildRankSeasonSelect.m_Instance = NKCUIManager.OpenNewInstance<NKCPopupGuildRankSeasonSelect>("AB_UI_NKM_UI_CONSORTIUM", "NKM_UI_CONSORTIUM_POPUP_RANKING_SELECT", NKCUIManager.eUIBaseRect.UIFrontPopup, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCPopupGuildRankSeasonSelect.CleanupInstance)).GetInstance<NKCPopupGuildRankSeasonSelect>();
					NKCPopupGuildRankSeasonSelect.m_Instance.InitUI();
				}
				return NKCPopupGuildRankSeasonSelect.m_Instance;
			}
		}

		// Token: 0x06008372 RID: 33650 RVA: 0x002C4D1F File Offset: 0x002C2F1F
		private static void CleanupInstance()
		{
			NKCPopupGuildRankSeasonSelect.m_Instance = null;
		}

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06008373 RID: 33651 RVA: 0x002C4D27 File Offset: 0x002C2F27
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCPopupGuildRankSeasonSelect.m_Instance != null && NKCPopupGuildRankSeasonSelect.m_Instance.IsOpen;
			}
		}

		// Token: 0x06008374 RID: 33652 RVA: 0x002C4D42 File Offset: 0x002C2F42
		private void OnDestroy()
		{
			NKCPopupGuildRankSeasonSelect.m_Instance = null;
		}

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x06008375 RID: 33653 RVA: 0x002C4D4A File Offset: 0x002C2F4A
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x06008376 RID: 33654 RVA: 0x002C4D4D File Offset: 0x002C2F4D
		public override string MenuName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06008377 RID: 33655 RVA: 0x002C4D54 File Offset: 0x002C2F54
		public void InitUI()
		{
			this.m_btnClose.PointerClick.RemoveAllListeners();
			this.m_btnClose.PointerClick.AddListener(new UnityAction(base.Close));
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loop.dOnGetObject += this.GetObject;
			this.m_loop.dOnReturnObject += this.ReturnObject;
			this.m_loop.dOnProvideData += this.ProvideData;
			this.m_loop.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_loop, null);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06008378 RID: 33656 RVA: 0x002C4E04 File Offset: 0x002C3004
		private RectTransform GetObject(int idx)
		{
			NKCPopupGuildRankSeasonSelectSlot nkcpopupGuildRankSeasonSelectSlot;
			if (this.m_stkSlot.Count > 0)
			{
				nkcpopupGuildRankSeasonSelectSlot = this.m_stkSlot.Pop();
			}
			else
			{
				nkcpopupGuildRankSeasonSelectSlot = UnityEngine.Object.Instantiate<NKCPopupGuildRankSeasonSelectSlot>(this.m_pfbSlot);
				nkcpopupGuildRankSeasonSelectSlot.InitUI();
			}
			nkcpopupGuildRankSeasonSelectSlot.transform.SetParent(this.m_trSlotParent);
			NKCUtil.SetGameobjectActive(nkcpopupGuildRankSeasonSelectSlot, false);
			return nkcpopupGuildRankSeasonSelectSlot.GetComponent<RectTransform>();
		}

		// Token: 0x06008379 RID: 33657 RVA: 0x002C4E60 File Offset: 0x002C3060
		private void ReturnObject(Transform tr)
		{
			NKCUtil.SetGameobjectActive(tr, false);
			tr.SetParent(base.gameObject.transform);
			NKCPopupGuildRankSeasonSelectSlot component = tr.GetComponent<NKCPopupGuildRankSeasonSelectSlot>();
			this.m_stkSlot.Push(component);
		}

		// Token: 0x0600837A RID: 33658 RVA: 0x002C4E98 File Offset: 0x002C3098
		private void ProvideData(Transform tr, int idx)
		{
			NKCPopupGuildRankSeasonSelectSlot component = tr.GetComponent<NKCPopupGuildRankSeasonSelectSlot>();
			if (component == null)
			{
				NKCUtil.SetGameobjectActive(tr, false);
				return;
			}
			tr.SetParent(this.m_trSlotParent);
			NKCUtil.SetGameobjectActive(tr, true);
			component.SetData(this.m_lstTemplet[idx].Key, NKCStringTable.GetString(this.m_lstTemplet[idx].GetSeasonNameID(), false), this.m_lstTemplet[idx].Key == this.m_selectedSeasonId, new NKCPopupGuildRankSeasonSelectSlot.OnClick(this.OnClickSlot));
		}

		// Token: 0x0600837B RID: 33659 RVA: 0x002C4F24 File Offset: 0x002C3124
		public void Open(NKCPopupGuildRankSeasonSelect.OnSelectSeason dOnSelectSeason, int selectedSeasonId)
		{
			this.m_dOnSelectSeason = dOnSelectSeason;
			this.m_selectedSeasonId = selectedSeasonId;
			this.m_lstTemplet.Clear();
			foreach (GuildSeasonTemplet guildSeasonTemplet in GuildSeasonTemplet.Values)
			{
				if (guildSeasonTemplet.GetSeasonStartDate() < ServiceTime.Recent)
				{
					this.m_lstTemplet.Add(guildSeasonTemplet);
				}
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_loop.TotalCount = this.m_lstTemplet.Count;
			this.m_loop.SetIndexPosition(0);
			this.m_loop.RefreshCells(false);
			base.UIOpened(true);
		}

		// Token: 0x0600837C RID: 33660 RVA: 0x002C4FE4 File Offset: 0x002C31E4
		public override void CloseInternal()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x0600837D RID: 33661 RVA: 0x002C4FF2 File Offset: 0x002C31F2
		private void OnClickSlot(int seasonId)
		{
			base.Close();
			NKCPopupGuildRankSeasonSelect.OnSelectSeason dOnSelectSeason = this.m_dOnSelectSeason;
			if (dOnSelectSeason == null)
			{
				return;
			}
			dOnSelectSeason(seasonId);
		}

		// Token: 0x04006F99 RID: 28569
		private const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_CONSORTIUM";

		// Token: 0x04006F9A RID: 28570
		private const string UI_ASSET_NAME = "NKM_UI_CONSORTIUM_POPUP_RANKING_SELECT";

		// Token: 0x04006F9B RID: 28571
		private static NKCPopupGuildRankSeasonSelect m_Instance;

		// Token: 0x04006F9C RID: 28572
		public NKCPopupGuildRankSeasonSelectSlot m_pfbSlot;

		// Token: 0x04006F9D RID: 28573
		public NKCUIComStateButton m_btnClose;

		// Token: 0x04006F9E RID: 28574
		public LoopScrollRect m_loop;

		// Token: 0x04006F9F RID: 28575
		public Transform m_trSlotParent;

		// Token: 0x04006FA0 RID: 28576
		private Stack<NKCPopupGuildRankSeasonSelectSlot> m_stkSlot = new Stack<NKCPopupGuildRankSeasonSelectSlot>();

		// Token: 0x04006FA1 RID: 28577
		private List<GuildSeasonTemplet> m_lstTemplet = new List<GuildSeasonTemplet>();

		// Token: 0x04006FA2 RID: 28578
		private int m_selectedSeasonId;

		// Token: 0x04006FA3 RID: 28579
		private NKCPopupGuildRankSeasonSelect.OnSelectSeason m_dOnSelectSeason;

		// Token: 0x020018DD RID: 6365
		// (Invoke) Token: 0x0600B6F9 RID: 46841
		public delegate void OnSelectSeason(int seasonId);
	}
}

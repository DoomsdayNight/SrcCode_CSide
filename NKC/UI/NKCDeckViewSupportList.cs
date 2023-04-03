using System;
using System.Collections.Generic;
using ClientPacket.Community;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000965 RID: 2405
	public class NKCDeckViewSupportList : MonoBehaviour
	{
		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x0600602E RID: 24622 RVA: 0x001DFE66 File Offset: 0x001DE066
		// (set) Token: 0x0600602F RID: 24623 RVA: 0x001DFE6E File Offset: 0x001DE06E
		public bool IsOpen { get; private set; }

		// Token: 0x06006030 RID: 24624 RVA: 0x001DFE78 File Offset: 0x001DE078
		public void Init(NKCUIDeckViewer deckViewer, NKCDeckViewSupportList.OnSelectSlot onSelectSlot, NKCDeckViewSupportList.OnConfirmBtn onConfirmBtn)
		{
			this.IsOpen = false;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_scrollRect.dOnGetObject += this.GetSlot;
			this.m_scrollRect.dOnReturnObject += this.ReturnSlot;
			this.m_scrollRect.dOnProvideData += this.ProvideData;
			this.m_scrollRect.ContentConstraintCount = 1;
			this.m_scrollRect.PrepareCells(0);
			NKCUtil.SetScrollHotKey(this.m_scrollRect, deckViewer);
			this.dOnSelectSlot = onSelectSlot;
			this.dOnConfirmBtn = onConfirmBtn;
			this.m_btnSelect.PointerClick.RemoveAllListeners();
			this.m_btnSelect.PointerClick.AddListener(new UnityAction(this.OnConfirmButton));
			this.m_tgSortTypeMenu.OnValueChanged.RemoveAllListeners();
			this.m_tgSortTypeMenu.OnValueChanged.AddListener(new UnityAction<bool>(this.OnSortMenuOpen));
		}

		// Token: 0x06006031 RID: 24625 RVA: 0x001DFF68 File Offset: 0x001DE168
		public void Open(List<WarfareSupporterListData> list, NKCUIDeckViewer.DeckViewerOption.IsValidSupport validSupport)
		{
			if (this.IsOpen)
			{
				return;
			}
			this.IsOpen = true;
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.dIsValidSupport = validSupport;
			this.m_dataList.Clear();
			for (int i = 0; i < list.Count; i++)
			{
				WarfareSupporterListData warfareSupporterListData = list[i];
				bool isGuest = !NKCFriendManager.IsFriend(warfareSupporterListData.commonProfile.friendCode);
				bool isGuild = NKCGuildManager.IsGuildMember(warfareSupporterListData.commonProfile.friendCode);
				NKCDeckViewSupportList.SupportData item = new NKCDeckViewSupportList.SupportData
				{
					Data = warfareSupporterListData,
					isGuest = isGuest,
					isGuild = isGuild
				};
				this.m_dataList.Add(item);
			}
			this.m_scrollRect.TotalCount = this.m_dataList.Count;
			this.m_scrollRect.SetIndexPosition(0);
			this.m_scrollRect.RefreshCells(true);
			this.RefreshList();
			this.m_txtCount.text = string.Format("{0}/{1}", this.m_dataList.Count, 60);
			if (this.m_selectedCode == 0L && this.m_dataList.Count > 0)
			{
				this.m_selectedCode = this.m_dataList[0].Data.commonProfile.friendCode;
			}
		}

		// Token: 0x06006032 RID: 24626 RVA: 0x001E00A1 File Offset: 0x001DE2A1
		public void Close()
		{
			if (!this.IsOpen)
			{
				return;
			}
			this.IsOpen = false;
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006033 RID: 24627 RVA: 0x001E00BF File Offset: 0x001DE2BF
		public void Clear()
		{
			this.m_selectedCode = 0L;
			this.m_dataList.Clear();
		}

		// Token: 0x06006034 RID: 24628 RVA: 0x001E00D4 File Offset: 0x001DE2D4
		public WarfareSupporterListData GetSelectedData()
		{
			NKCDeckViewSupportList.SupportData supportData = this.m_dataList.Find((NKCDeckViewSupportList.SupportData v) => v.Data.commonProfile.friendCode == this.m_selectedCode);
			if (supportData != null)
			{
				return supportData.Data;
			}
			return null;
		}

		// Token: 0x06006035 RID: 24629 RVA: 0x001E0104 File Offset: 0x001DE304
		public void UpdateSelectUI()
		{
			for (int i = 0; i < this.m_slotList.Count; i++)
			{
				NKCUIDeckViewSupportSlot nkcuideckViewSupportSlot = this.m_slotList[i];
				nkcuideckViewSupportSlot.SelectUI(nkcuideckViewSupportSlot.SuppoterCode == this.m_selectedCode);
			}
			this.UpdateButtonUI();
		}

		// Token: 0x06006036 RID: 24630 RVA: 0x001E014C File Offset: 0x001DE34C
		private void UpdateButtonUI()
		{
			WarfareSupporterListData selectedData = this.GetSelectedData();
			bool flag = false;
			if (selectedData != null)
			{
				flag = !this.IsCooltime(selectedData);
			}
			bool flag2 = this.m_dataList.Count > 0;
			NKCUIDeckViewer.DeckViewerOption.IsValidSupport isValidSupport = this.dIsValidSupport;
			bool flag3 = (isValidSupport == null || isValidSupport(this.m_selectedCode)) && flag && flag2;
			NKCUtil.SetGameobjectActive(this.m_btnSelect, flag3);
			NKCUtil.SetGameobjectActive(this.m_btnSelect_off, !flag3);
		}

		// Token: 0x06006037 RID: 24631 RVA: 0x001E01B7 File Offset: 0x001DE3B7
		private void SelectSlot(long selectedCode)
		{
			this.m_selectedCode = selectedCode;
			NKCDeckViewSupportList.OnSelectSlot onSelectSlot = this.dOnSelectSlot;
			if (onSelectSlot == null)
			{
				return;
			}
			onSelectSlot();
		}

		// Token: 0x06006038 RID: 24632 RVA: 0x001E01D0 File Offset: 0x001DE3D0
		private void OnConfirmButton()
		{
			NKCDeckViewSupportList.OnConfirmBtn onConfirmBtn = this.dOnConfirmBtn;
			if (onConfirmBtn == null)
			{
				return;
			}
			onConfirmBtn(this.m_selectedCode);
		}

		// Token: 0x06006039 RID: 24633 RVA: 0x001E01E8 File Offset: 0x001DE3E8
		private bool IsCooltime(WarfareSupporterListData friend)
		{
			TimeSpan t = NKCSynchronizedTime.GetServerUTCTime(0.0) - friend.lastUsedDate;
			return (TimeSpan.FromHours(12.0) - t).TotalSeconds > 0.0;
		}

		// Token: 0x0600603A RID: 24634 RVA: 0x001E0238 File Offset: 0x001DE438
		private void RefreshList()
		{
			NKCUtil.SetLabelText(this.m_txtSortType, NKCUnitSortSystem.GetSortName(this.m_currentSortOption));
			NKCUtil.SetLabelText(this.m_txtSelectedSortType, NKCUnitSortSystem.GetSortName(this.m_currentSortOption));
			this.m_dataList.Sort(new Comparison<NKCDeckViewSupportList.SupportData>(this.Compare));
			this.m_scrollRect.RefreshCells(false);
		}

		// Token: 0x0600603B RID: 24635 RVA: 0x001E0294 File Offset: 0x001DE494
		private RectTransform GetSlot(int index)
		{
			if (this.m_slotPool.Count > 0)
			{
				NKCUIDeckViewSupportSlot nkcuideckViewSupportSlot = this.m_slotPool.Pop();
				NKCUtil.SetGameobjectActive(nkcuideckViewSupportSlot, true);
				RectTransform component = nkcuideckViewSupportSlot.GetComponent<RectTransform>();
				this.m_slotList.Add(nkcuideckViewSupportSlot);
				return component;
			}
			NKCUIDeckViewSupportSlot nkcuideckViewSupportSlot2 = UnityEngine.Object.Instantiate<NKCUIDeckViewSupportSlot>(this.m_slotPrefab);
			nkcuideckViewSupportSlot2.transform.SetParent(this.m_scrollRect.content);
			nkcuideckViewSupportSlot2.Init();
			NKCUtil.SetGameobjectActive(nkcuideckViewSupportSlot2, true);
			RectTransform component2 = nkcuideckViewSupportSlot2.GetComponent<RectTransform>();
			component2.localScale = Vector3.one;
			this.m_slotList.Add(nkcuideckViewSupportSlot2);
			return component2;
		}

		// Token: 0x0600603C RID: 24636 RVA: 0x001E0324 File Offset: 0x001DE524
		private void ReturnSlot(Transform trans)
		{
			NKCUtil.SetGameobjectActive(trans, false);
			NKCUIDeckViewSupportSlot component = trans.GetComponent<NKCUIDeckViewSupportSlot>();
			this.m_slotList.Remove(component);
			this.m_slotPool.Push(component);
			trans.SetParent(base.transform);
		}

		// Token: 0x0600603D RID: 24637 RVA: 0x001E0364 File Offset: 0x001DE564
		private void ProvideData(Transform trans, int index)
		{
			NKCUIDeckViewSupportSlot component = trans.GetComponent<NKCUIDeckViewSupportSlot>();
			NKCDeckViewSupportList.SupportData supportData = this.m_dataList[index];
			component.SetData(supportData.Data, supportData.isGuest, new NKCUIDeckViewSupportSlot.OnSelect(this.SelectSlot));
			component.SelectUI(supportData.Data.commonProfile.friendCode == this.m_selectedCode);
			NKCUtil.SetGameobjectActive(component, true);
		}

		// Token: 0x0600603E RID: 24638 RVA: 0x001E03C6 File Offset: 0x001DE5C6
		private void OnSortMenuOpen(bool bValue)
		{
			this.m_NKCPopupSort.OpenSquadSortMenu(this.m_currentSortOption, new NKCPopupSort.OnSortOption(this.OnSort), bValue);
			this.SetOpenSortingMenu(bValue, true);
		}

		// Token: 0x0600603F RID: 24639 RVA: 0x001E03EE File Offset: 0x001DE5EE
		private void OnSort(List<NKCUnitSortSystem.eSortOption> sortOptionList)
		{
			this.m_currentSortOption = sortOptionList[0];
			this.SetOpenSortingMenu(false, true);
			this.RefreshList();
			this.UpdateSelectUI();
		}

		// Token: 0x06006040 RID: 24640 RVA: 0x001E0411 File Offset: 0x001DE611
		public void SetOpenSortingMenu(bool bOpen, bool bAnimate = true)
		{
			this.m_tgSortTypeMenu.Select(bOpen, true, false);
			this.m_NKCPopupSort.StartRectMove(bOpen, bAnimate);
		}

		// Token: 0x06006041 RID: 24641 RVA: 0x001E0430 File Offset: 0x001DE630
		private int Compare(NKCDeckViewSupportList.SupportData spt_a, NKCDeckViewSupportList.SupportData spt_b)
		{
			if (spt_a.isGuest != spt_b.isGuest)
			{
				return spt_a.isGuest.CompareTo(spt_b.isGuest);
			}
			if (spt_a.isGuild != spt_b.isGuild)
			{
				return spt_b.isGuild.CompareTo(spt_a.isGuild);
			}
			WarfareSupporterListData data = spt_a.Data;
			WarfareSupporterListData data2 = spt_b.Data;
			bool flag = !this.IsCooltime(data);
			bool flag2 = !this.IsCooltime(data2);
			if (flag != flag2)
			{
				return flag2.CompareTo(flag);
			}
			if (this.m_currentSortOption == NKCUnitSortSystem.eSortOption.Player_Level_High || this.m_currentSortOption == NKCUnitSortSystem.eSortOption.Player_Level_Low)
			{
				if (data.commonProfile.level != data2.commonProfile.level)
				{
					return data2.commonProfile.level.CompareTo(data.commonProfile.level);
				}
			}
			else if ((this.m_currentSortOption == NKCUnitSortSystem.eSortOption.LoginTime_Latly || this.m_currentSortOption == NKCUnitSortSystem.eSortOption.LoginTime_Old) && data.lastLoginDate != data2.lastLoginDate)
			{
				return data2.lastLoginDate.CompareTo(data.lastLoginDate);
			}
			float num = (float)data.deckData.CalculateOperationPower();
			float num2 = (float)data2.deckData.CalculateOperationPower();
			if (num != num2)
			{
				return num2.CompareTo(num);
			}
			return data.commonProfile.friendCode.CompareTo(data2.commonProfile.friendCode);
		}

		// Token: 0x04004C93 RID: 19603
		public LoopVerticalScrollRect m_scrollRect;

		// Token: 0x04004C94 RID: 19604
		public NKCUIDeckViewSupportSlot m_slotPrefab;

		// Token: 0x04004C95 RID: 19605
		public NKCUIComButton m_btnSelect;

		// Token: 0x04004C96 RID: 19606
		public NKCUIComButton m_btnSelect_off;

		// Token: 0x04004C97 RID: 19607
		public Text m_txtCount;

		// Token: 0x04004C98 RID: 19608
		[Header("정렬 옵션")]
		public NKCUIComToggle m_tgSortTypeMenu;

		// Token: 0x04004C99 RID: 19609
		public GameObject m_objSortSelect;

		// Token: 0x04004C9A RID: 19610
		public NKCPopupSort m_NKCPopupSort;

		// Token: 0x04004C9B RID: 19611
		public Text m_txtSortType;

		// Token: 0x04004C9C RID: 19612
		public Text m_txtSelectedSortType;

		// Token: 0x04004C9D RID: 19613
		private long m_selectedCode;

		// Token: 0x04004C9F RID: 19615
		private Stack<NKCUIDeckViewSupportSlot> m_slotPool = new Stack<NKCUIDeckViewSupportSlot>();

		// Token: 0x04004CA0 RID: 19616
		private List<NKCUIDeckViewSupportSlot> m_slotList = new List<NKCUIDeckViewSupportSlot>();

		// Token: 0x04004CA1 RID: 19617
		private List<NKCDeckViewSupportList.SupportData> m_dataList = new List<NKCDeckViewSupportList.SupportData>();

		// Token: 0x04004CA2 RID: 19618
		private NKCDeckViewSupportList.OnSelectSlot dOnSelectSlot;

		// Token: 0x04004CA3 RID: 19619
		private NKCDeckViewSupportList.OnConfirmBtn dOnConfirmBtn;

		// Token: 0x04004CA4 RID: 19620
		private NKCUIDeckViewer.DeckViewerOption.IsValidSupport dIsValidSupport;

		// Token: 0x04004CA5 RID: 19621
		private NKCUnitSortSystem.eSortOption m_currentSortOption = NKCUnitSortSystem.eSortOption.Power_High;

		// Token: 0x020015EE RID: 5614
		public class SupportData
		{
			// Token: 0x0400A2C1 RID: 41665
			public WarfareSupporterListData Data;

			// Token: 0x0400A2C2 RID: 41666
			public bool isGuest;

			// Token: 0x0400A2C3 RID: 41667
			public bool isGuild;
		}

		// Token: 0x020015EF RID: 5615
		// (Invoke) Token: 0x0600AEB0 RID: 44720
		public delegate void OnSelectSlot();

		// Token: 0x020015F0 RID: 5616
		// (Invoke) Token: 0x0600AEB4 RID: 44724
		public delegate void OnConfirmBtn(long selectedCode);
	}
}

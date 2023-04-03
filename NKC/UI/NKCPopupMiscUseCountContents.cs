using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A6F RID: 2671
	public class NKCPopupMiscUseCountContents : MonoBehaviour, IScrollHandler, IEventSystemHandler
	{
		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x060075D0 RID: 30160 RVA: 0x00272DC7 File Offset: 0x00270FC7
		// (set) Token: 0x060075D1 RID: 30161 RVA: 0x00272DCF File Offset: 0x00270FCF
		public long m_useCount { get; private set; }

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x060075D2 RID: 30162 RVA: 0x00272DD8 File Offset: 0x00270FD8
		// (set) Token: 0x060075D3 RID: 30163 RVA: 0x00272DE0 File Offset: 0x00270FE0
		public long m_maxCount { get; private set; }

		// Token: 0x060075D4 RID: 30164 RVA: 0x00272DEC File Offset: 0x00270FEC
		public void Init()
		{
			NKCUIComStateButton btnCountMinus = this.m_btnCountMinus;
			if (btnCountMinus != null)
			{
				btnCountMinus.PointerDown.RemoveAllListeners();
			}
			NKCUIComStateButton btnCountMinus2 = this.m_btnCountMinus;
			if (btnCountMinus2 != null)
			{
				btnCountMinus2.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnMinusDown));
			}
			NKCUIComStateButton btnCountMinus3 = this.m_btnCountMinus;
			if (btnCountMinus3 != null)
			{
				btnCountMinus3.PointerUp.RemoveAllListeners();
			}
			NKCUIComStateButton btnCountMinus4 = this.m_btnCountMinus;
			if (btnCountMinus4 != null)
			{
				btnCountMinus4.PointerUp.AddListener(new UnityAction(this.OnButtonUp));
			}
			NKCUtil.SetHotkey(this.m_btnCountMinus, HotkeyEventType.Minus, null, true);
			NKCUIComStateButton btnCountPlus = this.m_btnCountPlus;
			if (btnCountPlus != null)
			{
				btnCountPlus.PointerDown.RemoveAllListeners();
			}
			NKCUIComStateButton btnCountPlus2 = this.m_btnCountPlus;
			if (btnCountPlus2 != null)
			{
				btnCountPlus2.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPlusDown));
			}
			NKCUIComStateButton btnCountPlus3 = this.m_btnCountPlus;
			if (btnCountPlus3 != null)
			{
				btnCountPlus3.PointerUp.RemoveAllListeners();
			}
			NKCUIComStateButton btnCountPlus4 = this.m_btnCountPlus;
			if (btnCountPlus4 != null)
			{
				btnCountPlus4.PointerUp.AddListener(new UnityAction(this.OnButtonUp));
			}
			NKCUtil.SetHotkey(this.m_btnCountPlus, HotkeyEventType.Plus, null, true);
			NKCUIComStateButton btnCountMax = this.m_btnCountMax;
			if (btnCountMax != null)
			{
				btnCountMax.PointerClick.RemoveAllListeners();
			}
			NKCUIComStateButton btnCountMax2 = this.m_btnCountMax;
			if (btnCountMax2 != null)
			{
				btnCountMax2.PointerClick.AddListener(new UnityAction(this.OnButtonMax));
			}
			NKCUISlot nkcuiitemSlot = this.m_NKCUIItemSlot;
			if (nkcuiitemSlot != null)
			{
				nkcuiitemSlot.Init();
			}
			NKCUtil.SetBindFunction(this.m_btnCountMinus, delegate()
			{
				this.OnChangeCount(false);
			});
			NKCUtil.SetBindFunction(this.m_btnCountPlus, delegate()
			{
				this.OnChangeCount(true);
			});
		}

		// Token: 0x060075D5 RID: 30165 RVA: 0x00272F70 File Offset: 0x00271170
		public void SetData(NKCPopupMiscUseCount.USE_ITEM_TYPE openType, int useItemID, NKCUISlot.SlotData slotData)
		{
			if (slotData == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			this.m_useMiscTemplet = NKMItemManager.GetItemMiscTempletByID(useItemID);
			if (this.m_useMiscTemplet == null)
			{
				return;
			}
			if (openType != NKCPopupMiscUseCount.USE_ITEM_TYPE.Common)
			{
				if (openType == NKCPopupMiscUseCount.USE_ITEM_TYPE.DailyTicket)
				{
					NKCUtil.SetLabelText(this.m_lbSlotItemInventoryCountDesc, NKCStringTable.GetString("SI_DP_POPUP_USE_COUNT_TEXT_MISC_REMAIN_COUNT", false));
					NKCUtil.SetLabelText(this.m_lbUseItemInventoryCountDesc, NKCStringTable.GetString("SI_DP_POPUP_USE_COUNT_HAVE_TEXT_MISC_DAILY_TICKET", false));
				}
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbSlotItemInventoryCountDesc, NKCStringTable.GetString("SI_DP_POPUP_USE_COUNT_TEXT_MISC_HAVE", false));
				NKCUtil.SetLabelText(this.m_lbUseItemInventoryCountDesc, NKCStringTable.GetString("SI_DP_POPUP_USE_COUNT_HAVE_TEXT_MISC_CHOICE", false));
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_currentItemData = slotData;
			this.m_NKCUIItemSlot.SetData(this.m_currentItemData, false, this.m_useMiscTemplet.IsChoiceItem(), false, null);
			this.m_NKCUIItemSlot.SetOnClickAction(new NKCUISlot.SlotClickType[]
			{
				NKCUISlot.SlotClickType.RatioList,
				NKCUISlot.SlotClickType.BoxList
			});
			long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(useItemID);
			this.m_useCount = 1L;
			if (this.m_useMiscTemplet.IsPackageItem)
			{
				this.m_maxCount = Math.Min(countMiscItem, 100L);
			}
			else
			{
				this.m_maxCount = Math.Min(countMiscItem, 10000L);
			}
			NKCUtil.SetGameobjectActive(this.m_objUseItemCount, openType == NKCPopupMiscUseCount.USE_ITEM_TYPE.DailyTicket || this.m_useMiscTemplet.IsChoiceItem());
			if (this.m_objUseItemCount.activeSelf)
			{
				NKCUtil.SetLabelText(this.m_lbUseItemInventoryCount, countMiscItem.ToString());
			}
			this.SetTextFromSlotData(this.m_currentItemData);
			this.UpdateCountInfo();
			this.SetIntervalItem(slotData);
		}

		// Token: 0x060075D6 RID: 30166 RVA: 0x002730F0 File Offset: 0x002712F0
		private void SetTextFromSlotData(NKCUISlot.SlotData data)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(data.ID);
			if (itemMiscTempletByID != null)
			{
				NKCUtil.SetLabelText(this.m_lbItemName, itemMiscTempletByID.GetItemName());
				NKCUtil.SetLabelText(this.m_lbItemDesc, itemMiscTempletByID.GetItemDesc());
				long countMiscItem = NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(data.ID);
				NKCUtil.SetLabelText(this.m_lbSlotItemInventoryCount, countMiscItem.ToString());
			}
		}

		// Token: 0x060075D7 RID: 30167 RVA: 0x00273156 File Offset: 0x00271356
		public void Update()
		{
			this.OnUpdateButtonHold();
		}

		// Token: 0x060075D8 RID: 30168 RVA: 0x00273160 File Offset: 0x00271360
		private void UpdateCountInfo()
		{
			if (this.m_useMiscTemplet.IsChoiceItem())
			{
				this.m_NKCUIItemSlot.SetSlotItemCount(this.m_currentItemData.Count * this.m_useCount);
			}
			NKCUtil.SetLabelText(this.m_lbCount, this.m_useCount.ToString());
		}

		// Token: 0x060075D9 RID: 30169 RVA: 0x002731B0 File Offset: 0x002713B0
		private void SetIntervalItem(NKCUISlot.SlotData slotData)
		{
			bool flag = false;
			if (slotData != null && slotData.eType == NKCUISlot.eSlotMode.ItemMisc)
			{
				NKMItemMiscTemplet nkmitemMiscTemplet = NKMItemMiscTemplet.Find(slotData.ID);
				flag = (nkmitemMiscTemplet != null && nkmitemMiscTemplet.IsTimeIntervalItem);
				if (flag)
				{
					string timeSpanStringDHM = NKCUtilString.GetTimeSpanStringDHM(nkmitemMiscTemplet.GetIntervalTimeSpanLeft());
					NKCUtil.SetLabelText(this.m_lbTimeLeft, timeSpanStringDHM);
				}
			}
			NKCUtil.SetGameobjectActive(this.m_objTimeInterval, flag);
		}

		// Token: 0x060075DA RID: 30170 RVA: 0x0027320C File Offset: 0x0027140C
		public void OnChangeCount(bool bPlus = true)
		{
			if (this.m_bWasHold)
			{
				this.m_bWasHold = false;
				return;
			}
			if (!bPlus && this.m_useCount == 1L)
			{
				if (this.m_maxCount > 0L)
				{
					this.m_useCount = this.m_maxCount;
					this.UpdateCountInfo();
				}
				this.OnButtonUp();
				return;
			}
			this.m_useCount += (bPlus ? 1L : -1L);
			if (!bPlus && this.m_useCount <= 1L)
			{
				this.m_useCount = 1L;
			}
			if (bPlus && this.m_useCount >= this.m_maxCount)
			{
				this.m_useCount = this.m_maxCount;
			}
			this.UpdateCountInfo();
		}

		// Token: 0x060075DB RID: 30171 RVA: 0x002732A6 File Offset: 0x002714A6
		private void OnMinusDown(PointerEventData eventData)
		{
			this.m_iChangeValue = -1;
			this.m_bPress = true;
			this.m_fDelay = 0.35f;
			this.m_fHoldTime = 0f;
			this.m_bWasHold = false;
		}

		// Token: 0x060075DC RID: 30172 RVA: 0x002732D3 File Offset: 0x002714D3
		private void OnPlusDown(PointerEventData eventData)
		{
			this.m_iChangeValue = 1;
			this.m_bPress = true;
			this.m_fDelay = 0.35f;
			this.m_fHoldTime = 0f;
			this.m_bWasHold = false;
		}

		// Token: 0x060075DD RID: 30173 RVA: 0x00273300 File Offset: 0x00271500
		private void OnButtonUp()
		{
			this.m_iChangeValue = 0;
			this.m_fDelay = 0.35f;
			this.m_bPress = false;
		}

		// Token: 0x060075DE RID: 30174 RVA: 0x0027331B File Offset: 0x0027151B
		private void OnButtonMax()
		{
			this.m_iChangeValue = 0;
			this.m_fDelay = 0.35f;
			this.m_bPress = false;
			this.m_useCount = this.m_maxCount;
			this.UpdateCountInfo();
		}

		// Token: 0x060075DF RID: 30175 RVA: 0x00273348 File Offset: 0x00271548
		private void OnUpdateButtonHold()
		{
			if (!this.m_bPress)
			{
				return;
			}
			this.m_fHoldTime += Time.deltaTime;
			if (this.m_fHoldTime >= this.m_fDelay)
			{
				this.m_fHoldTime = 0f;
				this.m_fDelay *= 0.8f;
				int num = (this.m_fDelay < 0.01f) ? 5 : 1;
				this.m_fDelay = Mathf.Clamp(this.m_fDelay, 0.01f, 0.35f);
				this.m_useCount += (long)(this.m_iChangeValue * num);
				this.m_bWasHold = true;
				if (this.m_iChangeValue < 0 && this.m_useCount < 1L)
				{
					this.m_useCount = 1L;
					this.m_bPress = false;
				}
				if (this.m_iChangeValue > 0 && this.m_useCount > this.m_maxCount)
				{
					this.m_useCount = this.m_maxCount;
					this.m_bPress = false;
				}
				this.UpdateCountInfo();
			}
		}

		// Token: 0x060075E0 RID: 30176 RVA: 0x0027343C File Offset: 0x0027163C
		public void OnScroll(PointerEventData eventData)
		{
			if (eventData.scrollDelta.y < 0f)
			{
				long useCount = this.m_useCount;
				this.m_useCount = useCount - 1L;
			}
			else if (eventData.scrollDelta.y > 0f)
			{
				long useCount = this.m_useCount;
				this.m_useCount = useCount + 1L;
			}
			Mathf.Clamp((float)this.m_useCount, 1f, (float)this.m_maxCount);
			this.UpdateCountInfo();
		}

		// Token: 0x04006235 RID: 25141
		public Text m_lbItemName;

		// Token: 0x04006236 RID: 25142
		public GameObject m_objItemInventoryCountParent;

		// Token: 0x04006237 RID: 25143
		public Text m_lbSlotItemInventoryCountDesc;

		// Token: 0x04006238 RID: 25144
		public Text m_lbSlotItemInventoryCount;

		// Token: 0x04006239 RID: 25145
		public Text m_lbItemDesc;

		// Token: 0x0400623A RID: 25146
		public NKCUISlot m_NKCUIItemSlot;

		// Token: 0x0400623B RID: 25147
		public GameObject m_objUseItemCount;

		// Token: 0x0400623C RID: 25148
		public Text m_lbUseItemInventoryCountDesc;

		// Token: 0x0400623D RID: 25149
		public Text m_lbUseItemInventoryCount;

		// Token: 0x0400623E RID: 25150
		public GameObject m_objCount;

		// Token: 0x0400623F RID: 25151
		public NKCUIComStateButton m_btnCountMinus;

		// Token: 0x04006240 RID: 25152
		public NKCUIComStateButton m_btnCountPlus;

		// Token: 0x04006241 RID: 25153
		public NKCUIComStateButton m_btnCountMax;

		// Token: 0x04006242 RID: 25154
		public Text m_lbCount;

		// Token: 0x04006243 RID: 25155
		[Header("기간제 아이템")]
		public GameObject m_objTimeInterval;

		// Token: 0x04006244 RID: 25156
		public Text m_lbTimeLeft;

		// Token: 0x04006245 RID: 25157
		private NKMItemMiscTemplet m_useMiscTemplet;

		// Token: 0x04006246 RID: 25158
		private NKCUISlot.SlotData m_currentItemData;

		// Token: 0x04006249 RID: 25161
		private const int MAX_USE_COUNT = 10000;

		// Token: 0x0400624A RID: 25162
		private const int PACKAGE_MAX_USE_COUNT = 100;

		// Token: 0x0400624B RID: 25163
		public const float PRESS_GAP_MAX = 0.35f;

		// Token: 0x0400624C RID: 25164
		public const float PRESS_GAP_MIN = 0.01f;

		// Token: 0x0400624D RID: 25165
		public const float DAMPING = 0.8f;

		// Token: 0x0400624E RID: 25166
		private float m_fDelay = 0.5f;

		// Token: 0x0400624F RID: 25167
		private float m_fHoldTime;

		// Token: 0x04006250 RID: 25168
		private int m_iChangeValue;

		// Token: 0x04006251 RID: 25169
		private bool m_bPress;

		// Token: 0x04006252 RID: 25170
		private bool m_bWasHold;
	}
}

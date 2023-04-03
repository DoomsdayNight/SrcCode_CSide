using System;
using NKC.UI.Guide;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009C4 RID: 2500
	public class NKCUIOperationMultiply : MonoBehaviour, IScrollHandler, IEventSystemHandler
	{
		// Token: 0x06006A71 RID: 27249 RVA: 0x00229284 File Offset: 0x00227484
		public void Init(NKCUIOperationMultiply.OnCountUpdated onCountUpdated = null, UnityAction closeEvent = null)
		{
			this.m_dOnCountUpdated = onCountUpdated;
			this.m_dOnClickEventClose = closeEvent;
			this.m_csbtnUp.PointerClick.RemoveAllListeners();
			this.m_csbtnUp.PointerClick.AddListener(new UnityAction(this.OnClickUp));
			this.m_csbtnUp.dOnPointerHoldPress = new NKCUIComStateButtonBase.OnPointerHoldPress(this.OnClickUp);
			this.m_csbtnUp.SetHotkey(HotkeyEventType.Plus, null, false);
			this.m_csbtnDown.PointerClick.RemoveAllListeners();
			this.m_csbtnDown.PointerClick.AddListener(new UnityAction(this.OnClickDown));
			this.m_csbtnDown.dOnPointerHoldPress = new NKCUIComStateButtonBase.OnPointerHoldPress(this.OnHoldDown);
			this.m_csbtnDown.SetHotkey(HotkeyEventType.Minus, null, false);
			if (this.m_btnInfo != null)
			{
				this.m_btnInfo.PointerClick.RemoveAllListeners();
				this.m_btnInfo.PointerClick.AddListener(new UnityAction(this.OnClickInfo));
			}
			if (this.m_btnClose != null)
			{
				this.m_btnClose.PointerClick.RemoveAllListeners();
				this.m_btnClose.PointerClick.AddListener(new UnityAction(this.OnClickClose));
			}
		}

		// Token: 0x06006A72 RID: 27250 RVA: 0x002293B7 File Offset: 0x002275B7
		private void OnClickDown()
		{
			this.m_currentCount--;
			if (!this.m_bWasHold && this.m_currentCount < this.m_MinCount)
			{
				this.m_currentCount = this.m_MaxCount;
			}
			this.m_bWasHold = false;
			this.OnValueChanged(true);
		}

		// Token: 0x06006A73 RID: 27251 RVA: 0x002293F7 File Offset: 0x002275F7
		private void OnClickUp()
		{
			this.m_currentCount++;
			this.OnValueChanged(true);
		}

		// Token: 0x06006A74 RID: 27252 RVA: 0x0022940E File Offset: 0x0022760E
		private void OnHoldDown()
		{
			this.m_bWasHold = true;
			this.m_currentCount--;
			this.OnValueChanged(true);
		}

		// Token: 0x06006A75 RID: 27253 RVA: 0x0022942C File Offset: 0x0022762C
		private void OnClickInfo()
		{
			NKCUIPopUpGuide.Instance.Open("ARTICLE_SYSTEM_SUPPORT_BATTLE", 1);
		}

		// Token: 0x06006A76 RID: 27254 RVA: 0x0022943E File Offset: 0x0022763E
		private void OnClickClose()
		{
			if (this.m_dOnClickEventClose != null)
			{
				this.m_dOnClickEventClose();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006A77 RID: 27255 RVA: 0x0022945F File Offset: 0x0022765F
		public void SetLockUI(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objUnlock, !bSet);
			NKCUtil.SetGameobjectActive(this.m_objLock, bSet);
		}

		// Token: 0x06006A78 RID: 27256 RVA: 0x0022947C File Offset: 0x0022767C
		private void OnValueChanged(bool bInvokeCallback)
		{
			this.m_currentCount = Mathf.Clamp(this.m_currentCount, this.m_MinCount, this.GetMaxCount());
			if (this.m_NKCUIItemCostSlot != null)
			{
				this.m_NKCUIItemCostSlot.SetData(this.m_MultiplyCostItemID, this.m_MultiplyCostItemCount * this.m_currentCount, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_MultiplyCostItemID), true, true, false);
			}
			NKCUtil.SetLabelText(this.m_lbCount, this.m_currentCount.ToString());
			if (bInvokeCallback)
			{
				NKCUIOperationMultiply.OnCountUpdated dOnCountUpdated = this.m_dOnCountUpdated;
				if (dOnCountUpdated == null)
				{
					return;
				}
				dOnCountUpdated(this.m_currentCount);
			}
		}

		// Token: 0x06006A79 RID: 27257 RVA: 0x0022951C File Offset: 0x0022771C
		private int GetMaxCount()
		{
			int num = 99;
			if (this.m_DungeonCostItemID != 0 && this.m_DungeonCostItemCount != 0)
			{
				num = (int)(NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_DungeonCostItemID) / (long)this.m_DungeonCostItemCount);
			}
			int num2 = 99;
			if (this.m_MultiplyCostItemID != 0 && this.m_MultiplyCostItemCount != 0)
			{
				num2 = (int)(NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_MultiplyCostItemID) / (long)this.m_MultiplyCostItemCount);
			}
			return Mathf.Min(new int[]
			{
				99,
				this.m_MaxCount,
				num,
				num2
			});
		}

		// Token: 0x06006A7A RID: 27258 RVA: 0x002295AE File Offset: 0x002277AE
		public void SetData(int multiplyCostItemID, int multiplyCostItemCount, int dungeonCostItemID, int dungeonCostItemCount, int currCount, int minCount = 2, int maxCount = 99)
		{
			this.m_MultiplyCostItemID = multiplyCostItemID;
			this.m_MultiplyCostItemCount = multiplyCostItemCount;
			this.m_DungeonCostItemID = dungeonCostItemID;
			this.m_DungeonCostItemCount = dungeonCostItemCount;
			this.m_MaxCount = maxCount;
			this.m_MinCount = minCount;
			this.m_currentCount = currCount;
			this.OnValueChanged(false);
		}

		// Token: 0x06006A7B RID: 27259 RVA: 0x002295EC File Offset: 0x002277EC
		public void OnScroll(PointerEventData eventData)
		{
			if (eventData.scrollDelta.y < 0f)
			{
				this.m_currentCount--;
			}
			else if (eventData.scrollDelta.y > 0f)
			{
				this.m_currentCount++;
			}
			this.OnValueChanged(true);
		}

		// Token: 0x04005636 RID: 22070
		public GameObject m_objUnlock;

		// Token: 0x04005637 RID: 22071
		public GameObject m_objLock;

		// Token: 0x04005638 RID: 22072
		public NKCUIItemCostSlot m_NKCUIItemCostSlot;

		// Token: 0x04005639 RID: 22073
		public Text m_lbCount;

		// Token: 0x0400563A RID: 22074
		public NKCUIComStateButton m_csbtnUp;

		// Token: 0x0400563B RID: 22075
		public NKCUIComStateButton m_csbtnDown;

		// Token: 0x0400563C RID: 22076
		public NKCUIComStateButton m_btnInfo;

		// Token: 0x0400563D RID: 22077
		public NKCUIComStateButton m_btnClose;

		// Token: 0x0400563E RID: 22078
		private UnityAction m_dOnClickEventClose;

		// Token: 0x0400563F RID: 22079
		private NKCUIOperationMultiply.OnCountUpdated m_dOnCountUpdated;

		// Token: 0x04005640 RID: 22080
		private int m_MultiplyCostItemID;

		// Token: 0x04005641 RID: 22081
		private int m_MultiplyCostItemCount = 1;

		// Token: 0x04005642 RID: 22082
		private int m_DungeonCostItemID;

		// Token: 0x04005643 RID: 22083
		private int m_DungeonCostItemCount = 1;

		// Token: 0x04005644 RID: 22084
		private int m_currentCount;

		// Token: 0x04005645 RID: 22085
		private int m_MaxCount = 99;

		// Token: 0x04005646 RID: 22086
		private int m_MinCount = 2;

		// Token: 0x04005647 RID: 22087
		private bool m_bWasHold;

		// Token: 0x020016C9 RID: 5833
		// (Invoke) Token: 0x0600B14E RID: 45390
		public delegate void OnCountUpdated(int count);
	}
}

using System;
using NKC.UI.Guide;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009C5 RID: 2501
	public class NKCUIOperationSkip : MonoBehaviour, IScrollHandler, IEventSystemHandler
	{
		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06006A7D RID: 27261 RVA: 0x00229667 File Offset: 0x00227867
		public int CurrentCount
		{
			get
			{
				return this.m_currentCount;
			}
		}

		// Token: 0x06006A7E RID: 27262 RVA: 0x00229670 File Offset: 0x00227870
		public void Init(NKCUIOperationSkip.OnCountUpdated onCountUpdated = null, UnityAction closeEvent = null)
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
			if (this.m_csbtnInfo != null)
			{
				this.m_csbtnInfo.PointerClick.RemoveAllListeners();
				this.m_csbtnInfo.PointerClick.AddListener(new UnityAction(this.OnClickInfo));
			}
			if (this.m_csbtnClose != null)
			{
				this.m_csbtnClose.PointerClick.RemoveAllListeners();
				this.m_csbtnClose.PointerClick.AddListener(new UnityAction(this.OnClickClose));
			}
		}

		// Token: 0x06006A7F RID: 27263 RVA: 0x002297A4 File Offset: 0x002279A4
		public void SetData(NKMStageTempletV2 stageTemplet, int currCount)
		{
			int num = 99;
			int num2 = (stageTemplet != null) ? stageTemplet.m_StageReqItemCount : 1;
			int num3 = (stageTemplet != null) ? stageTemplet.m_StageReqItemID : 0;
			if (stageTemplet != null)
			{
				if (stageTemplet.m_StageReqItemID == 2)
				{
					if (stageTemplet.WarfareTemplet != null)
					{
						NKCCompanyBuff.SetDiscountOfEterniumInEnteringWarfare(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref num2);
					}
					else if (stageTemplet.DungeonTempletBase != null)
					{
						NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref num2);
					}
					else if (stageTemplet.PhaseTemplet != null)
					{
						NKCCompanyBuff.SetDiscountOfEterniumInEnteringDungeon(NKCScenManager.CurrentUserData().m_companyBuffDataList, ref num2);
					}
				}
				if (stageTemplet.EnterLimit > 0)
				{
					int statePlayCnt = NKCScenManager.CurrentUserData().GetStatePlayCnt(stageTemplet.Key, false, false, false);
					num = stageTemplet.EnterLimit - statePlayCnt;
				}
				if (stageTemplet.IsUsingEventDeck())
				{
					NKCUtil.SetLabelText(this.m_lbUseDeckType, NKCStringTable.GetString("SI_PF_OPERATION_SKIP_EVENTDECK_INFO", false));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbUseDeckType, NKCStringTable.GetString("SI_PF_OPERATION_SKIP_INFO", false));
				}
			}
			int b = 1;
			if (num2 > 0)
			{
				NKMUserData myUserData = NKCScenManager.GetScenManager().GetMyUserData();
				if (myUserData != null)
				{
					b = (int)myUserData.m_InventoryData.GetCountMiscItem(num3) / num2;
				}
			}
			num = Mathf.Min(num, b);
			this.SetData(NKMCommonConst.SkipCostMiscItemId, NKMCommonConst.SkipCostMiscItemCount, num3, num2, currCount, 1, num);
			NKCUtil.SetGameobjectActive(this.m_lbUseDeckType, stageTemplet != null);
		}

		// Token: 0x06006A80 RID: 27264 RVA: 0x002298E0 File Offset: 0x00227AE0
		public void SetData(int skipCostItemID, int skipCostItemCount, int dungeonCostItemID, int dungeonCostItemCount, int currCount, int minCount = 1, int maxCount = 99)
		{
			this.m_SkipCostItemID = skipCostItemID;
			this.m_SkipCostItemCount = skipCostItemCount;
			this.m_DungeonCostItemID = dungeonCostItemID;
			this.m_DungeonCostItemCount = dungeonCostItemCount;
			this.m_MaxCount = maxCount;
			this.m_MinCount = minCount;
			this.m_currentCount = currCount;
			this.OnValueChanged(false);
			NKCUtil.SetGameobjectActive(this.m_lbUseDeckType, false);
		}

		// Token: 0x06006A81 RID: 27265 RVA: 0x00229935 File Offset: 0x00227B35
		public void Close()
		{
			this.OnClickClose();
		}

		// Token: 0x06006A82 RID: 27266 RVA: 0x0022993D File Offset: 0x00227B3D
		private void SetLockUI(bool bSet)
		{
			NKCUtil.SetGameobjectActive(this.m_objUnlock, !bSet);
			NKCUtil.SetGameobjectActive(this.m_objLock, bSet);
		}

		// Token: 0x06006A83 RID: 27267 RVA: 0x0022995C File Offset: 0x00227B5C
		private int GetMaxCount()
		{
			int num = 99;
			if (this.m_DungeonCostItemID != 0 && this.m_DungeonCostItemCount != 0)
			{
				num = (int)(NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_DungeonCostItemID) / (long)this.m_DungeonCostItemCount);
			}
			int num2 = 99;
			if (this.m_SkipCostItemID != 0 && this.m_SkipCostItemCount != 0)
			{
				num2 = (int)(NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_SkipCostItemID) / (long)this.m_SkipCostItemCount);
			}
			return Mathf.Min(new int[]
			{
				99,
				this.m_MaxCount,
				num,
				num2
			});
		}

		// Token: 0x06006A84 RID: 27268 RVA: 0x002299EE File Offset: 0x00227BEE
		private void OnClickUp()
		{
			this.m_currentCount++;
			this.OnValueChanged(true);
		}

		// Token: 0x06006A85 RID: 27269 RVA: 0x00229A05 File Offset: 0x00227C05
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

		// Token: 0x06006A86 RID: 27270 RVA: 0x00229A45 File Offset: 0x00227C45
		private void OnHoldDown()
		{
			this.m_bWasHold = true;
			this.m_currentCount--;
			this.OnValueChanged(true);
		}

		// Token: 0x06006A87 RID: 27271 RVA: 0x00229A64 File Offset: 0x00227C64
		private void OnValueChanged(bool bInvokeCallback)
		{
			this.m_currentCount = Mathf.Clamp(this.m_currentCount, this.m_MinCount, this.GetMaxCount());
			if (this.m_NKCUIItemCostSlot != null)
			{
				this.m_NKCUIItemCostSlot.SetData(this.m_SkipCostItemID, this.m_SkipCostItemCount * this.m_currentCount, NKCScenManager.CurrentUserData().m_InventoryData.GetCountMiscItem(this.m_SkipCostItemID), true, true, false);
			}
			NKCUtil.SetLabelText(this.m_lbCount, this.m_currentCount.ToString());
			if (bInvokeCallback)
			{
				NKCUIOperationSkip.OnCountUpdated dOnCountUpdated = this.m_dOnCountUpdated;
				if (dOnCountUpdated == null)
				{
					return;
				}
				dOnCountUpdated(this.m_currentCount);
			}
		}

		// Token: 0x06006A88 RID: 27272 RVA: 0x00229B01 File Offset: 0x00227D01
		private void OnClickInfo()
		{
			NKCUIPopUpGuide.Instance.Open("ARTICLE_SYSTEM_SUPPORT_BATTLE", 2);
		}

		// Token: 0x06006A89 RID: 27273 RVA: 0x00229B13 File Offset: 0x00227D13
		private void OnClickClose()
		{
			if (this.m_dOnClickEventClose != null)
			{
				this.m_dOnClickEventClose();
			}
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x06006A8A RID: 27274 RVA: 0x00229B34 File Offset: 0x00227D34
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

		// Token: 0x04005648 RID: 22088
		public GameObject m_objUnlock;

		// Token: 0x04005649 RID: 22089
		public GameObject m_objLock;

		// Token: 0x0400564A RID: 22090
		public NKCUIItemCostSlot m_NKCUIItemCostSlot;

		// Token: 0x0400564B RID: 22091
		public Text m_lbCount;

		// Token: 0x0400564C RID: 22092
		public NKCUIComStateButton m_csbtnUp;

		// Token: 0x0400564D RID: 22093
		public NKCUIComStateButton m_csbtnDown;

		// Token: 0x0400564E RID: 22094
		public NKCUIComStateButton m_csbtnInfo;

		// Token: 0x0400564F RID: 22095
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04005650 RID: 22096
		public Text m_lbUseDeckType;

		// Token: 0x04005651 RID: 22097
		private UnityAction m_dOnClickEventClose;

		// Token: 0x04005652 RID: 22098
		private NKCUIOperationSkip.OnCountUpdated m_dOnCountUpdated;

		// Token: 0x04005653 RID: 22099
		private int m_SkipCostItemID;

		// Token: 0x04005654 RID: 22100
		private int m_SkipCostItemCount = 1;

		// Token: 0x04005655 RID: 22101
		private int m_DungeonCostItemID;

		// Token: 0x04005656 RID: 22102
		private int m_DungeonCostItemCount = 1;

		// Token: 0x04005657 RID: 22103
		private int m_currentCount;

		// Token: 0x04005658 RID: 22104
		public const int MAX_COUNT_MULTIPLY_AND_SKIP = 99;

		// Token: 0x04005659 RID: 22105
		private int m_MaxCount = 99;

		// Token: 0x0400565A RID: 22106
		private int m_MinCount = 1;

		// Token: 0x0400565B RID: 22107
		private bool m_bWasHold;

		// Token: 0x020016CA RID: 5834
		// (Invoke) Token: 0x0600B152 RID: 45394
		public delegate void OnCountUpdated(int count);
	}
}

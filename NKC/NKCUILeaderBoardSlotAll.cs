using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200095B RID: 2395
	public class NKCUILeaderBoardSlotAll : MonoBehaviour
	{
		// Token: 0x06005FAB RID: 24491 RVA: 0x001DC747 File Offset: 0x001DA947
		public void InitUI()
		{
			NKCUILeaderBoardSlotTop3 slotTop = this.m_SlotTop3;
			if (slotTop != null)
			{
				slotTop.InitUI();
			}
			NKCUILeaderBoardSlotTop3 slotTop3Only = this.m_SlotTop3Only;
			if (slotTop3Only != null)
			{
				slotTop3Only.InitUI();
			}
			NKCUILeaderBoardSlot slot = this.m_Slot;
			if (slot == null)
			{
				return;
			}
			slot.InitUI();
		}

		// Token: 0x06005FAC RID: 24492 RVA: 0x001DC77C File Offset: 0x001DA97C
		public void SetData(LeaderBoardSlotData data1, LeaderBoardSlotData data2, LeaderBoardSlotData data3, int criteria, bool bTop3Only, NKCUILeaderBoardSlot.OnDragBegin onDragBegin)
		{
			NKCUtil.SetGameobjectActive(this.m_SlotTop3Only, bTop3Only);
			NKCUtil.SetGameobjectActive(this.m_SlotTop3, !bTop3Only);
			NKCUtil.SetGameobjectActive(this.m_Slot, false);
			this.m_layout.preferredHeight = (bTop3Only ? this.m_SlotTop3Only.GetComponent<LayoutElement>().preferredHeight : this.m_SlotTop3.GetComponent<LayoutElement>().preferredHeight);
			this.m_SlotTop3.SetData(data1, data2, data3, criteria, onDragBegin);
		}

		// Token: 0x06005FAD RID: 24493 RVA: 0x001DC7F8 File Offset: 0x001DA9F8
		public void SetData(LeaderBoardSlotData data, int criteria, NKCUILeaderBoardSlot.OnDragBegin onDragBegin)
		{
			NKCUtil.SetGameobjectActive(this.m_SlotTop3Only, false);
			NKCUtil.SetGameobjectActive(this.m_SlotTop3, false);
			NKCUtil.SetGameobjectActive(this.m_Slot, true);
			this.m_layout.preferredHeight = this.m_Slot.GetComponent<LayoutElement>().preferredHeight;
			this.m_Slot.SetData(data, criteria, onDragBegin, false, true);
		}

		// Token: 0x04004BCC RID: 19404
		public NKCUILeaderBoardSlotTop3 m_SlotTop3Only;

		// Token: 0x04004BCD RID: 19405
		public NKCUILeaderBoardSlotTop3 m_SlotTop3;

		// Token: 0x04004BCE RID: 19406
		public NKCUILeaderBoardSlot m_Slot;

		// Token: 0x04004BCF RID: 19407
		public LayoutElement m_layout;
	}
}

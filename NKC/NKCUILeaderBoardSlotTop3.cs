using System;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x0200095C RID: 2396
	public class NKCUILeaderBoardSlotTop3 : MonoBehaviour
	{
		// Token: 0x06005FAF RID: 24495 RVA: 0x001DC85C File Offset: 0x001DAA5C
		public void InitUI()
		{
			NKCUILeaderBoardSlot top = this.m_Top1;
			if (top != null)
			{
				top.InitUI();
			}
			NKCUILeaderBoardSlot top2 = this.m_Top2;
			if (top2 != null)
			{
				top2.InitUI();
			}
			NKCUILeaderBoardSlot top3 = this.m_Top3;
			if (top3 == null)
			{
				return;
			}
			top3.InitUI();
		}

		// Token: 0x06005FB0 RID: 24496 RVA: 0x001DC890 File Offset: 0x001DAA90
		public void SetData(LeaderBoardSlotData data1, LeaderBoardSlotData data2, LeaderBoardSlotData data3, int criteria, NKCUILeaderBoardSlot.OnDragBegin onDragBegin)
		{
			this.m_Top1.SetData(data1, criteria, onDragBegin, false, true);
			this.m_Top2.SetData(data2, criteria, onDragBegin, false, true);
			this.m_Top3.SetData(data3, criteria, onDragBegin, false, true);
		}

		// Token: 0x04004BD0 RID: 19408
		public NKCUILeaderBoardSlot m_Top1;

		// Token: 0x04004BD1 RID: 19409
		public NKCUILeaderBoardSlot m_Top2;

		// Token: 0x04004BD2 RID: 19410
		public NKCUILeaderBoardSlot m_Top3;
	}
}

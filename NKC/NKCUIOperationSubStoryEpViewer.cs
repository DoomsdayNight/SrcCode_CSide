using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000A0B RID: 2571
	public class NKCUIOperationSubStoryEpViewer : MonoBehaviour
	{
		// Token: 0x0600703E RID: 28734 RVA: 0x0025318A File Offset: 0x0025138A
		public List<NKCUIOperationSubStorySlot> GetEpList()
		{
			return this.m_lstSlot;
		}

		// Token: 0x0600703F RID: 28735 RVA: 0x00253194 File Offset: 0x00251394
		public void InitUI()
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].InitUI(null);
			}
		}

		// Token: 0x06007040 RID: 28736 RVA: 0x002531CC File Offset: 0x002513CC
		public void SetData()
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].SetData(false);
			}
		}

		// Token: 0x06007041 RID: 28737 RVA: 0x00253204 File Offset: 0x00251404
		public void Refresh()
		{
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].Refresh();
			}
		}

		// Token: 0x04005BF1 RID: 23537
		public List<NKCUIOperationSubStorySlot> m_lstSlot = new List<NKCUIOperationSubStorySlot>();
	}
}

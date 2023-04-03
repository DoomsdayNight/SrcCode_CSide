using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007E3 RID: 2019
	public class NKCWarfareGameLabelMgr
	{
		// Token: 0x06004FD0 RID: 20432 RVA: 0x00181FCC File Offset: 0x001801CC
		public NKCWarfareGameLabelMgr(Transform labelParent)
		{
			this.m_labelParent = labelParent;
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x00181FE8 File Offset: 0x001801E8
		public void SetLabel(int tileIndex, WARFARE_LABEL_TYPE labelType, Vector3 pos)
		{
			NKCWarfareGameLabel nkcwarfareGameLabel = this.getLabel(tileIndex);
			if (nkcwarfareGameLabel == null)
			{
				for (int i = 0; i < this.m_labelList.Count; i++)
				{
					if (!this.m_labelList[i].gameObject.activeSelf)
					{
						nkcwarfareGameLabel = this.m_labelList[i];
						break;
					}
				}
			}
			if (nkcwarfareGameLabel == null)
			{
				nkcwarfareGameLabel = NKCWarfareGameLabel.GetNewInstance(this.m_labelParent);
				this.m_labelList.Add(nkcwarfareGameLabel);
			}
			nkcwarfareGameLabel.SetWFLabelType(tileIndex, labelType);
			nkcwarfareGameLabel.transform.localPosition = pos;
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x00182078 File Offset: 0x00180278
		public void SetText(int index, int count)
		{
			NKCWarfareGameLabel label = this.getLabel(index);
			if (label != null)
			{
				label.SetWFLabelCount(count);
			}
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x001820A0 File Offset: 0x001802A0
		public void HideAll()
		{
			for (int i = 0; i < this.m_labelList.Count; i++)
			{
				this.m_labelList[i].Hide();
			}
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x001820D4 File Offset: 0x001802D4
		private NKCWarfareGameLabel getLabel(int index)
		{
			for (int i = 0; i < this.m_labelList.Count; i++)
			{
				if (this.m_labelList[i].Index == index)
				{
					return this.m_labelList[i];
				}
			}
			return null;
		}

		// Token: 0x04003FDB RID: 16347
		private Transform m_labelParent;

		// Token: 0x04003FDC RID: 16348
		private List<NKCWarfareGameLabel> m_labelList = new List<NKCWarfareGameLabel>();
	}
}

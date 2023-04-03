using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000755 RID: 1877
	public class NKCUIComDotList : MonoBehaviour
	{
		// Token: 0x06004B1A RID: 19226 RVA: 0x00167FC8 File Offset: 0x001661C8
		public void SetIndex(int index)
		{
			for (int i = 0; i < this.m_lstDots.Count; i++)
			{
				Image image = this.m_lstDots[i];
				if (!(image == null))
				{
					image.color = ((i == index) ? this.m_colSelected : this.m_colBase);
				}
			}
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x0016801C File Offset: 0x0016621C
		public void SetMaxCount(int value)
		{
			if (value < 1)
			{
				return;
			}
			if (value > this.m_lstDots.Count)
			{
				while (value > this.m_lstDots.Count)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_lstDots[0].gameObject);
					gameObject.transform.SetParent(this.m_lstDots[0].transform.parent);
					Image component = gameObject.GetComponent<Image>();
					this.m_lstDots.Add(component);
				}
				this.m_MaxCount = this.m_lstDots.Count;
			}
			else
			{
				this.m_MaxCount = value;
			}
			for (int i = 0; i < this.m_lstDots.Count; i++)
			{
				Image image = this.m_lstDots[i];
				if (!(image == null))
				{
					NKCUtil.SetGameobjectActive(image, i < this.m_MaxCount);
				}
			}
		}

		// Token: 0x040039C5 RID: 14789
		public List<Image> m_lstDots;

		// Token: 0x040039C6 RID: 14790
		public Color m_colSelected;

		// Token: 0x040039C7 RID: 14791
		public Color m_colBase;

		// Token: 0x040039C8 RID: 14792
		private int m_MaxCount = 1;
	}
}

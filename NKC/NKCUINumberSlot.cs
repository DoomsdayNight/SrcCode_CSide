using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200076D RID: 1901
	public class NKCUINumberSlot : MonoBehaviour
	{
		// Token: 0x06004BE0 RID: 19424 RVA: 0x0016B248 File Offset: 0x00169448
		public void SetValue(int value, bool bForceAni = false)
		{
			if (bForceAni || this.currentValue != value)
			{
				this.m_Animator.SetTrigger("Play");
				int num = 1;
				for (int i = 0; i < this.m_lstNumber.Count; i++)
				{
					if (this.m_lstNumber[i] != null)
					{
						this.m_lstNumber[i].SetEnable(value >= num);
						this.m_lstNumber[i].SetNumber(value / num % 10);
					}
					num *= 10;
				}
			}
			this.currentValue = value;
		}

		// Token: 0x04003A59 RID: 14937
		[Header("번호 오브젝트, 낮은 자리수부터 높은 자리수 순으로")]
		public List<NKCUINumberSlot.Number> m_lstNumber;

		// Token: 0x04003A5A RID: 14938
		public Animator m_Animator;

		// Token: 0x04003A5B RID: 14939
		private int currentValue = -1;

		// Token: 0x0200143B RID: 5179
		[Serializable]
		public class Number
		{
			// Token: 0x0600A830 RID: 43056 RVA: 0x0034BDB1 File Offset: 0x00349FB1
			public void SetEnable(bool value)
			{
				NKCUtil.SetGameobjectActive(this.m_objEnable, value);
				NKCUtil.SetGameobjectActive(this.m_objDisable, !value);
			}

			// Token: 0x0600A831 RID: 43057 RVA: 0x0034BDCE File Offset: 0x00349FCE
			public void SetNumber(int value)
			{
				NKCUtil.SetLabelText(this.m_lbCredit, value.ToString());
			}

			// Token: 0x04009DC2 RID: 40386
			public Text m_lbCredit;

			// Token: 0x04009DC3 RID: 40387
			public GameObject m_objEnable;

			// Token: 0x04009DC4 RID: 40388
			public GameObject m_objDisable;
		}
	}
}

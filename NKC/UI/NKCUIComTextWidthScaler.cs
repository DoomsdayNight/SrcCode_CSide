using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200094D RID: 2381
	[RequireComponent(typeof(Text))]
	public class NKCUIComTextWidthScaler : MonoBehaviour
	{
		// Token: 0x06005EF1 RID: 24305 RVA: 0x001D79F8 File Offset: 0x001D5BF8
		private void Start()
		{
			this.m_Text = base.GetComponent<Text>();
			if (this.m_Text == null)
			{
				base.enabled = false;
				return;
			}
			this.LastText = this.m_Text.text;
			NKCUtil.SetLabelWidthScale(ref this.m_Text);
		}

		// Token: 0x06005EF2 RID: 24306 RVA: 0x001D7A38 File Offset: 0x001D5C38
		private void Update()
		{
			if (this.m_Text != null && this.LastText != this.m_Text.text)
			{
				NKCUtil.SetLabelWidthScale(ref this.m_Text);
			}
		}

		// Token: 0x04004B13 RID: 19219
		private Text m_Text;

		// Token: 0x04004B14 RID: 19220
		private string LastText;
	}
}

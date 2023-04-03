using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Tooltip
{
	// Token: 0x02000B0C RID: 2828
	public class NKCUITooltipText : NKCUITooltipBase
	{
		// Token: 0x06008064 RID: 32868 RVA: 0x002B4B52 File Offset: 0x002B2D52
		public override void Init()
		{
		}

		// Token: 0x06008065 RID: 32869 RVA: 0x002B4B54 File Offset: 0x002B2D54
		public override void SetData(NKCUITooltip.Data data)
		{
			NKCUITooltip.TextData textData = data as NKCUITooltip.TextData;
			if (textData == null)
			{
				Debug.LogError("Tooltip textData is null");
				return;
			}
			this.m_desc.text = textData.Text;
		}

		// Token: 0x04006CA5 RID: 27813
		public Text m_desc;
	}
}

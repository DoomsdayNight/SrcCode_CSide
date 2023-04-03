using System;
using NKC;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000018 RID: 24
public class NKCPopupEquipOptionListSlot : MonoBehaviour
{
	// Token: 0x060000CF RID: 207 RVA: 0x00003F81 File Offset: 0x00002181
	public void SetData(string name, string value)
	{
		NKCUtil.SetLabelText(this.m_OPTION_NAME, name);
		NKCUtil.SetLabelText(this.m_OPTION_TEXT, value);
	}

	// Token: 0x0400005E RID: 94
	public Text m_OPTION_NAME;

	// Token: 0x0400005F RID: 95
	public Text m_OPTION_TEXT;
}

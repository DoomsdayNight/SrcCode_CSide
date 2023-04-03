using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A45 RID: 2629
	public class NKCPopupEquipChangeStatSlot : MonoBehaviour
	{
		// Token: 0x06007367 RID: 29543 RVA: 0x002663F1 File Offset: 0x002645F1
		public void SetData(string statShortName, string statValue, string changedValueStr, string changedValueColor)
		{
			NKCUtil.SetLabelText(this.m_lbName, statShortName);
			NKCUtil.SetLabelText(this.m_lbValue, statValue);
			NKCUtil.SetLabelText(this.m_lbChangeValue, changedValueStr);
			NKCUtil.SetLabelTextColor(this.m_lbChangeValue, NKCUtil.GetColor(changedValueColor));
		}

		// Token: 0x04005F61 RID: 24417
		public Text m_lbName;

		// Token: 0x04005F62 RID: 24418
		public Text m_lbValue;

		// Token: 0x04005F63 RID: 24419
		public Text m_lbChangeValue;
	}
}

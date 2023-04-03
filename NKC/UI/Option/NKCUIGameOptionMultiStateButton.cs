using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Option
{
	// Token: 0x02000B91 RID: 2961
	public class NKCUIGameOptionMultiStateButton : MonoBehaviour
	{
		// Token: 0x060088C9 RID: 35017 RVA: 0x002E4928 File Offset: 0x002E2B28
		public void Init(int min, int max, int value, string[] textTemplet = null, NKCUIGameOptionMultiStateButton.OnClicked onClicked = null)
		{
			this.m_Min = min;
			this.m_Max = max;
			this.m_TextTemplet = textTemplet;
			this.dOnClicked = onClicked;
			this.m_Value = value;
			NKCUIComStateButton button = this.m_Button;
			if (button != null)
			{
				button.PointerClick.AddListener(new UnityAction(this.OnClickButton));
			}
			this.UpdateButtonText();
		}

		// Token: 0x060088CA RID: 35018 RVA: 0x002E4985 File Offset: 0x002E2B85
		public int GetValue()
		{
			return this.m_Value;
		}

		// Token: 0x060088CB RID: 35019 RVA: 0x002E498D File Offset: 0x002E2B8D
		public void ChangeValue(int value)
		{
			this.m_Value = value;
			this.UpdateButtonText();
		}

		// Token: 0x060088CC RID: 35020 RVA: 0x002E499C File Offset: 0x002E2B9C
		private void UpdateButtonText()
		{
			if (this.m_TextTemplet != null)
			{
				this.m_Text.text = this.m_TextTemplet[this.m_Value];
				return;
			}
			this.m_Text.text = this.m_Value.ToString();
		}

		// Token: 0x060088CD RID: 35021 RVA: 0x002E49D8 File Offset: 0x002E2BD8
		private void OnClickButton()
		{
			int num = this.m_Value;
			num++;
			if (num > this.m_Max)
			{
				num = this.m_Min;
			}
			this.ChangeValue(num);
			if (this.dOnClicked != null)
			{
				this.dOnClicked();
			}
		}

		// Token: 0x04007548 RID: 30024
		private int m_Min;

		// Token: 0x04007549 RID: 30025
		private int m_Max;

		// Token: 0x0400754A RID: 30026
		private string[] m_TextTemplet;

		// Token: 0x0400754B RID: 30027
		private int m_Value;

		// Token: 0x0400754C RID: 30028
		public NKCUIComStateButton m_Button;

		// Token: 0x0400754D RID: 30029
		public Text m_Text;

		// Token: 0x0400754E RID: 30030
		private NKCUIGameOptionMultiStateButton.OnClicked dOnClicked;

		// Token: 0x02001938 RID: 6456
		// (Invoke) Token: 0x0600B7FB RID: 47099
		public delegate void OnClicked();
	}
}

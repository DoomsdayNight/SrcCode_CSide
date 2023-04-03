using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Option
{
	// Token: 0x02000B8E RID: 2958
	public class NKCUIGameOptionMenuButton : MonoBehaviour
	{
		// Token: 0x060088B1 RID: 34993 RVA: 0x002E3E08 File Offset: 0x002E2008
		public void Init(Color selectedIconColor, Color selectedTextColor, Color selectedSubTextColor, NKCUIGameOptionMenuButton.OnSelected onSelected = null)
		{
			this.m_OriginalIconColor = this.m_Icon.color;
			this.m_OriginalTextColor = this.m_Text.color;
			this.m_OriginalSubTextColor = this.m_SubText.color;
			this.m_SelectedIconColor = selectedIconColor;
			this.m_SelectedTextColor = selectedTextColor;
			this.m_SelectedSubTextColor = selectedSubTextColor;
			this.dOnSelected = onSelected;
			this.m_Toggle.OnValueChanged.AddListener(new UnityAction<bool>(this.Select));
		}

		// Token: 0x060088B2 RID: 34994 RVA: 0x002E3E84 File Offset: 0x002E2084
		private void Select(bool bSelect)
		{
			if (bSelect)
			{
				this.m_Icon.color = this.m_SelectedIconColor;
				this.m_Text.color = this.m_SelectedTextColor;
				this.m_SubText.color = this.m_SelectedSubTextColor;
				this.dOnSelected();
				return;
			}
			this.m_Icon.color = this.m_OriginalIconColor;
			this.m_Text.color = this.m_OriginalTextColor;
			this.m_SubText.color = this.m_OriginalSubTextColor;
		}

		// Token: 0x04007524 RID: 29988
		public NKCUIComToggle m_Toggle;

		// Token: 0x04007525 RID: 29989
		public Image m_Icon;

		// Token: 0x04007526 RID: 29990
		public Text m_Text;

		// Token: 0x04007527 RID: 29991
		public Text m_SubText;

		// Token: 0x04007528 RID: 29992
		private Color m_OriginalIconColor;

		// Token: 0x04007529 RID: 29993
		private Color m_OriginalTextColor;

		// Token: 0x0400752A RID: 29994
		private Color m_OriginalSubTextColor;

		// Token: 0x0400752B RID: 29995
		private Color m_SelectedIconColor;

		// Token: 0x0400752C RID: 29996
		private Color m_SelectedTextColor;

		// Token: 0x0400752D RID: 29997
		private Color m_SelectedSubTextColor;

		// Token: 0x0400752E RID: 29998
		private NKCUIGameOptionMenuButton.OnSelected dOnSelected;

		// Token: 0x02001935 RID: 6453
		// (Invoke) Token: 0x0600B7F7 RID: 47095
		public delegate void OnSelected();
	}
}

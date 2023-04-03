using System;
using TMPro;

namespace NKC.UI.Component
{
	// Token: 0x02000C4E RID: 3150
	public class NKCComTMPUIText : TextMeshProUGUI
	{
		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x060092C6 RID: 37574 RVA: 0x0032184E File Offset: 0x0031FA4E
		// (set) Token: 0x060092C7 RID: 37575 RVA: 0x00321856 File Offset: 0x0031FA56
		public override string text
		{
			get
			{
				return base.text;
			}
			set
			{
				base.text = value;
				if (this.m_IgnoreNewlineChar)
				{
					base.text = NKCUtil.RemoveLabelCharText(this.text, "\n");
				}
			}
		}

		// Token: 0x060092C8 RID: 37576 RVA: 0x0032187D File Offset: 0x0031FA7D
		protected override void Awake()
		{
			base.Awake();
			if (!string.IsNullOrWhiteSpace(this.m_StringKey))
			{
				this.text = NKCStringTable.GetString(this.m_StringKey, false);
			}
		}

		// Token: 0x04007FBB RID: 32699
		public string m_StringKey;

		// Token: 0x04007FBC RID: 32700
		public bool m_IgnoreNewlineChar;
	}
}

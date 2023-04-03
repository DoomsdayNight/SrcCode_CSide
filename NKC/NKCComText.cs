using System;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000620 RID: 1568
	public class NKCComText : Text
	{
		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x0600306D RID: 12397 RVA: 0x000EEFD2 File Offset: 0x000ED1D2
		// (set) Token: 0x0600306E RID: 12398 RVA: 0x000EEFDA File Offset: 0x000ED1DA
		public override string text
		{
			get
			{
				return base.text;
			}
			set
			{
				base.text = value;
				if (this.m_HideTextOutOfRect)
				{
					base.text = NKCUtil.LabelLongTextCut(this);
				}
				if (this.m_IgnoreNewlineChar)
				{
					base.text = NKCUtil.RemoveLabelCharText(this.text, "\n");
				}
			}
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000EF018 File Offset: 0x000ED218
		protected override void Awake()
		{
			base.Awake();
			if (!string.IsNullOrWhiteSpace(this.m_StringKey))
			{
				this.text = NKCStringTable.GetString(this.m_StringKey, false);
				return;
			}
			if (!string.IsNullOrWhiteSpace(this.text) && this.m_HideTextOutOfRect)
			{
				base.text = NKCUtil.LabelLongTextCut(this);
			}
		}

		// Token: 0x04002FD0 RID: 12240
		public string m_StringKey;

		// Token: 0x04002FD1 RID: 12241
		public bool m_HideTextOutOfRect;

		// Token: 0x04002FD2 RID: 12242
		public bool m_IgnoreNewlineChar;
	}
}

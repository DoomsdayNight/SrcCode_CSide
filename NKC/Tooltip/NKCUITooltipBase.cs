using System;
using UnityEngine;

namespace NKC.UI.Tooltip
{
	// Token: 0x02000B03 RID: 2819
	public abstract class NKCUITooltipBase : MonoBehaviour
	{
		// Token: 0x06008045 RID: 32837
		public abstract void Init();

		// Token: 0x06008046 RID: 32838
		public abstract void SetData(NKCUITooltip.Data data);

		// Token: 0x06008047 RID: 32839 RVA: 0x002B4459 File Offset: 0x002B2659
		public virtual void SetData(string title, string desc)
		{
		}
	}
}

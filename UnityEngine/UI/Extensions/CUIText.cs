using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002D6 RID: 726
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(Text))]
	[AddComponentMenu("UI/Effects/Extensions/Curly UI Text")]
	public class CUIText : CUIGraphic
	{
		// Token: 0x06000FEE RID: 4078 RVA: 0x0003464C File Offset: 0x0003284C
		public override void ReportSet()
		{
			if (this.uiGraphic == null)
			{
				this.uiGraphic = base.GetComponent<Text>();
			}
			base.ReportSet();
		}
	}
}

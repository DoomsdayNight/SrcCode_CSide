using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000305 RID: 773
	public abstract class FancyScrollRectCell<TItemData> : FancyScrollRectCell<TItemData, FancyScrollRectContext>
	{
		// Token: 0x0600112E RID: 4398 RVA: 0x0003C20A File Offset: 0x0003A40A
		public sealed override void SetContext(FancyScrollRectContext context)
		{
			base.SetContext(context);
		}
	}
}

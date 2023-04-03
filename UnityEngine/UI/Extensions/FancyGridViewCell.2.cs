using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002FE RID: 766
	public abstract class FancyGridViewCell<TItemData> : FancyGridViewCell<TItemData, FancyGridViewContext>
	{
		// Token: 0x060010F9 RID: 4345 RVA: 0x0003BB59 File Offset: 0x00039D59
		public sealed override void SetContext(FancyGridViewContext context)
		{
			base.SetContext(context);
		}
	}
}

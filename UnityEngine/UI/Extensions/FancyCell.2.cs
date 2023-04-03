using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002F6 RID: 758
	public abstract class FancyCell<TItemData> : FancyCell<TItemData, NullContext>
	{
		// Token: 0x060010CC RID: 4300 RVA: 0x0003B393 File Offset: 0x00039593
		public sealed override void SetContext(NullContext context)
		{
			base.SetContext(context);
		}
	}
}

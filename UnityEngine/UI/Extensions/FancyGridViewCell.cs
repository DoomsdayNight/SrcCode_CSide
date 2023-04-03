using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002FD RID: 765
	public abstract class FancyGridViewCell<TItemData, TContext> : FancyScrollRectCell<TItemData, TContext> where TContext : class, IFancyGridViewContext, new()
	{
		// Token: 0x060010F7 RID: 4343 RVA: 0x0003BAB4 File Offset: 0x00039CB4
		protected override void UpdatePosition(float normalizedPosition, float localPosition)
		{
			float num = base.Context.GetCellSize();
			float num2 = base.Context.GetStartAxisSpacing();
			int num3 = base.Context.GetGroupCount();
			int num4 = base.Index % num3;
			float num5 = (num + num2) * ((float)num4 - (float)(num3 - 1) * 0.5f);
			base.transform.localPosition = ((base.Context.ScrollDirection == ScrollDirection.Horizontal) ? new Vector2(-localPosition, -num5) : new Vector2(num5, localPosition));
		}
	}
}

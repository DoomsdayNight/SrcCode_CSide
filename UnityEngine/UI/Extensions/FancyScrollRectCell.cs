using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000304 RID: 772
	public abstract class FancyScrollRectCell<TItemData, TContext> : FancyCell<TItemData, TContext> where TContext : class, IFancyScrollRectContext, new()
	{
		// Token: 0x0600112B RID: 4395 RVA: 0x0003C160 File Offset: 0x0003A360
		public override void UpdatePosition(float position)
		{
			ValueTuple<float, float> valueTuple = base.Context.CalculateScrollSize();
			float item = valueTuple.Item1;
			float item2 = valueTuple.Item2;
			float normalizedPosition = (Mathf.Lerp(0f, item, position) - item2) / (item - item2 * 2f);
			float num = 0.5f * item;
			float b = -num;
			this.UpdatePosition(normalizedPosition, Mathf.Lerp(num, b, position));
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0003C1C4 File Offset: 0x0003A3C4
		protected virtual void UpdatePosition(float normalizedPosition, float localPosition)
		{
			base.transform.localPosition = ((base.Context.ScrollDirection == ScrollDirection.Horizontal) ? new Vector2(-localPosition, 0f) : new Vector2(0f, localPosition));
		}
	}
}

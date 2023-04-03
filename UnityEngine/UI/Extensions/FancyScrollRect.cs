using System;
using System.Collections.Generic;
using UnityEngine.UI.Extensions.EasingCore;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000302 RID: 770
	[RequireComponent(typeof(Scroller))]
	public abstract class FancyScrollRect<TItemData, TContext> : FancyScrollView<TItemData, TContext> where TContext : class, IFancyScrollRectContext, new()
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06001110 RID: 4368
		protected abstract float CellSize { get; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x0003BBD8 File Offset: 0x00039DD8
		protected virtual bool Scrollable
		{
			get
			{
				return this.MaxScrollPosition > 0f;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0003BBE8 File Offset: 0x00039DE8
		protected Scroller Scroller
		{
			get
			{
				Scroller result;
				if ((result = this.cachedScroller) == null)
				{
					result = (this.cachedScroller = base.GetComponent<Scroller>());
				}
				return result;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x0003BC0E File Offset: 0x00039E0E
		private float ScrollLength
		{
			get
			{
				return 1f / Mathf.Max(this.cellInterval, 0.01f) - 1f;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0003BC2C File Offset: 0x00039E2C
		private float ViewportLength
		{
			get
			{
				return this.ScrollLength - this.reuseCellMarginCount * 2f;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x0003BC41 File Offset: 0x00039E41
		private float PaddingHeadLength
		{
			get
			{
				return (this.paddingHead - this.spacing * 0.5f) / (this.CellSize + this.spacing);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0003BC64 File Offset: 0x00039E64
		private float MaxScrollPosition
		{
			get
			{
				return (float)base.ItemsSource.Count - this.ScrollLength + this.reuseCellMarginCount * 2f + (this.paddingHead + this.paddingTail - this.spacing) / (this.CellSize + this.spacing);
			}
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x0003BCB4 File Offset: 0x00039EB4
		protected override void Initialize()
		{
			base.Initialize();
			base.Context.ScrollDirection = this.Scroller.ScrollDirection;
			base.Context.CalculateScrollSize = delegate()
			{
				float num = this.CellSize + this.spacing;
				float num2 = num * this.reuseCellMarginCount;
				return new ValueTuple<float, float>(this.Scroller.ViewportSize + num + num2 * 2f, num2);
			};
			this.AdjustCellIntervalAndScrollOffset();
			this.Scroller.OnValueChanged(new Action<float>(this.OnScrollerValueChanged));
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0003BD1C File Offset: 0x00039F1C
		private void OnScrollerValueChanged(float p)
		{
			base.UpdatePosition(this.Scrollable ? this.ToFancyScrollViewPosition(p) : 0f);
			if (this.Scroller.Scrollbar)
			{
				if (p > (float)(base.ItemsSource.Count - 1))
				{
					this.ShrinkScrollbar(p - (float)(base.ItemsSource.Count - 1));
					return;
				}
				if (p < 0f)
				{
					this.ShrinkScrollbar(-p);
				}
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0003BD90 File Offset: 0x00039F90
		private void ShrinkScrollbar(float offset)
		{
			float num = 1f - this.ToFancyScrollViewPosition(offset) / (this.ViewportLength - this.PaddingHeadLength);
			this.UpdateScrollbarSize((this.ViewportLength - this.PaddingHeadLength) * num);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0003BDCE File Offset: 0x00039FCE
		protected override void Refresh()
		{
			this.AdjustCellIntervalAndScrollOffset();
			this.RefreshScroller();
			base.Refresh();
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0003BDE2 File Offset: 0x00039FE2
		protected override void Relayout()
		{
			this.AdjustCellIntervalAndScrollOffset();
			this.RefreshScroller();
			base.Relayout();
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0003BDF8 File Offset: 0x00039FF8
		protected void RefreshScroller()
		{
			this.Scroller.Draggable = this.Scrollable;
			this.Scroller.ScrollSensitivity = this.ToScrollerPosition(this.ViewportLength - this.PaddingHeadLength);
			this.Scroller.Position = this.ToScrollerPosition(this.currentPosition);
			if (this.Scroller.Scrollbar)
			{
				this.Scroller.Scrollbar.gameObject.SetActive(this.Scrollable);
				this.UpdateScrollbarSize(this.ViewportLength);
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0003BE84 File Offset: 0x0003A084
		protected override void UpdateContents(IList<TItemData> items)
		{
			this.AdjustCellIntervalAndScrollOffset();
			base.UpdateContents(items);
			this.Scroller.SetTotalCount(items.Count);
			this.RefreshScroller();
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0003BEAA File Offset: 0x0003A0AA
		protected new void UpdatePosition(float position)
		{
			this.Scroller.Position = this.ToScrollerPosition(position, 0.5f);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x0003BEC3 File Offset: 0x0003A0C3
		protected virtual void JumpTo(int itemIndex, float alignment = 0.5f)
		{
			this.Scroller.Position = this.ToScrollerPosition((float)itemIndex, alignment);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0003BED9 File Offset: 0x0003A0D9
		protected virtual void ScrollTo(int index, float duration, float alignment = 0.5f, Action onComplete = null)
		{
			this.Scroller.ScrollTo(this.ToScrollerPosition((float)index, alignment), duration, onComplete);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0003BEF2 File Offset: 0x0003A0F2
		protected virtual void ScrollTo(int index, float duration, Ease easing, float alignment = 0.5f, Action onComplete = null)
		{
			this.Scroller.ScrollTo(this.ToScrollerPosition((float)index, alignment), duration, easing, onComplete);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0003BF10 File Offset: 0x0003A110
		protected void UpdateScrollbarSize(float viewportLength)
		{
			float num = Mathf.Max((float)base.ItemsSource.Count + (this.paddingHead + this.paddingTail - this.spacing) / (this.CellSize + this.spacing), 1f);
			this.Scroller.Scrollbar.size = (this.Scrollable ? Mathf.Clamp01(viewportLength / num) : 1f);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0003BF7E File Offset: 0x0003A17E
		protected float ToFancyScrollViewPosition(float position)
		{
			return position / (float)Mathf.Max(base.ItemsSource.Count - 1, 1) * this.MaxScrollPosition - this.PaddingHeadLength;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0003BFA4 File Offset: 0x0003A1A4
		protected float ToScrollerPosition(float position)
		{
			return (position + this.PaddingHeadLength) / this.MaxScrollPosition * (float)Mathf.Max(base.ItemsSource.Count - 1, 1);
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0003BFCC File Offset: 0x0003A1CC
		protected float ToScrollerPosition(float position, float alignment = 0.5f)
		{
			float num = alignment * (this.ScrollLength - (1f + this.reuseCellMarginCount * 2f)) + (1f - alignment - 0.5f) * this.spacing / (this.CellSize + this.spacing);
			return this.ToScrollerPosition(Mathf.Clamp(position - num, 0f, this.MaxScrollPosition));
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0003C034 File Offset: 0x0003A234
		protected void AdjustCellIntervalAndScrollOffset()
		{
			float num = this.Scroller.ViewportSize + (this.CellSize + this.spacing) * (1f + this.reuseCellMarginCount * 2f);
			this.cellInterval = (this.CellSize + this.spacing) / num;
			this.scrollOffset = this.cellInterval * (1f + this.reuseCellMarginCount);
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0003C09C File Offset: 0x0003A29C
		protected virtual void OnValidate()
		{
			this.AdjustCellIntervalAndScrollOffset();
			if (this.loop)
			{
				this.loop = false;
				Debug.LogError("Loop is currently not supported in FancyScrollRect.");
			}
			if (this.Scroller.SnapEnabled)
			{
				this.Scroller.SnapEnabled = false;
				Debug.LogError("Snap is currently not supported in FancyScrollRect.");
			}
			if (this.Scroller.MovementType == MovementType.Unrestricted)
			{
				this.Scroller.MovementType = MovementType.Elastic;
				Debug.LogError("MovementType.Unrestricted is currently not supported in FancyScrollRect.");
			}
		}

		// Token: 0x04000BDF RID: 3039
		[SerializeField]
		protected float reuseCellMarginCount;

		// Token: 0x04000BE0 RID: 3040
		[SerializeField]
		protected float paddingHead;

		// Token: 0x04000BE1 RID: 3041
		[SerializeField]
		protected float paddingTail;

		// Token: 0x04000BE2 RID: 3042
		[SerializeField]
		protected float spacing;

		// Token: 0x04000BE3 RID: 3043
		private Scroller cachedScroller;
	}
}

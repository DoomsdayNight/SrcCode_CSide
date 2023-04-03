using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.UI.Extensions.EasingCore;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002FB RID: 763
	public abstract class FancyGridView<TItemData, TContext> : FancyScrollRect<TItemData[], TContext> where TContext : class, IFancyGridViewContext, new()
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x0003B807 File Offset: 0x00039A07
		protected sealed override GameObject CellPrefab
		{
			get
			{
				return this.cellGroupTemplate;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0003B80F File Offset: 0x00039A0F
		protected override float CellSize
		{
			get
			{
				if (base.Scroller.ScrollDirection != ScrollDirection.Horizontal)
				{
					return this.cellSize.y;
				}
				return this.cellSize.x;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0003B836 File Offset: 0x00039A36
		// (set) Token: 0x060010E9 RID: 4329 RVA: 0x0003B83E File Offset: 0x00039A3E
		public int DataCount { get; private set; }

		// Token: 0x060010EA RID: 4330 RVA: 0x0003B848 File Offset: 0x00039A48
		protected override void Initialize()
		{
			base.Initialize();
			base.Context.ScrollDirection = base.Scroller.ScrollDirection;
			base.Context.GetGroupCount = (() => this.startAxisCellCount);
			base.Context.GetStartAxisSpacing = (() => this.startAxisSpacing);
			base.Context.GetCellSize = delegate()
			{
				if (base.Scroller.ScrollDirection != ScrollDirection.Horizontal)
				{
					return this.cellSize.x;
				}
				return this.cellSize.y;
			};
			this.SetupCellTemplate();
		}

		// Token: 0x060010EB RID: 4331
		protected abstract void SetupCellTemplate();

		// Token: 0x060010EC RID: 4332 RVA: 0x0003B8D0 File Offset: 0x00039AD0
		protected virtual void Setup<TGroup>(FancyCell<TItemData, TContext> cellTemplate) where TGroup : FancyCell<TItemData[], TContext>
		{
			base.Context.CellTemplate = cellTemplate.gameObject;
			this.cellGroupTemplate = new GameObject("Group").AddComponent<TGroup>().gameObject;
			this.cellGroupTemplate.transform.SetParent(this.cellContainer, false);
			this.cellGroupTemplate.SetActive(false);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0003B938 File Offset: 0x00039B38
		public virtual void UpdateContents(IList<TItemData> items)
		{
			this.DataCount = items.Count;
			TItemData[][] itemsSource = (from x in items.Select((TItemData item, int index) => new ValueTuple<TItemData, int>(item, index))
			group x.Item1 by x.Item2 / this.startAxisCellCount into @group
			select @group.ToArray<TItemData>()).ToArray<TItemData[]>();
			this.UpdateContents(itemsSource);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0003B9D8 File Offset: 0x00039BD8
		protected override void JumpTo(int itemIndex, float alignment = 0.5f)
		{
			int itemIndex2 = itemIndex / this.startAxisCellCount;
			base.JumpTo(itemIndex2, alignment);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0003B9F8 File Offset: 0x00039BF8
		protected override void ScrollTo(int itemIndex, float duration, float alignment = 0.5f, Action onComplete = null)
		{
			int index = itemIndex / this.startAxisCellCount;
			base.ScrollTo(index, duration, alignment, onComplete);
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0003BA1C File Offset: 0x00039C1C
		protected override void ScrollTo(int itemIndex, float duration, Ease easing, float alignment = 0.5f, Action onComplete = null)
		{
			int index = itemIndex / this.startAxisCellCount;
			base.ScrollTo(index, duration, easing, alignment, onComplete);
		}

		// Token: 0x04000BD4 RID: 3028
		[SerializeField]
		protected float startAxisSpacing;

		// Token: 0x04000BD5 RID: 3029
		[SerializeField]
		protected int startAxisCellCount = 4;

		// Token: 0x04000BD6 RID: 3030
		[SerializeField]
		protected Vector2 cellSize = new Vector2(100f, 100f);

		// Token: 0x04000BD8 RID: 3032
		private GameObject cellGroupTemplate;

		// Token: 0x0200114D RID: 4429
		protected abstract class DefaultCellGroup : FancyCellGroup<TItemData, TContext>
		{
		}
	}
}

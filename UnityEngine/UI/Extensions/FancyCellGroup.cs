using System;
using System.Linq;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002FA RID: 762
	public abstract class FancyCellGroup<TItemData, TContext> : FancyCell<TItemData[], TContext> where TContext : class, IFancyCellGroupContext, new()
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x0003B66C File Offset: 0x0003986C
		// (set) Token: 0x060010DF RID: 4319 RVA: 0x0003B674 File Offset: 0x00039874
		private protected virtual FancyCell<TItemData, TContext>[] Cells { protected get; private set; }

		// Token: 0x060010E0 RID: 4320 RVA: 0x0003B680 File Offset: 0x00039880
		protected virtual FancyCell<TItemData, TContext>[] InstantiateCells()
		{
			return (from _ in Enumerable.Range(0, base.Context.GetGroupCount())
			select Object.Instantiate<GameObject>(base.Context.CellTemplate, base.transform) into x
			select x.GetComponent<FancyCell<TItemData, TContext>>()).ToArray<FancyCell<TItemData, TContext>>();
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0003B6E4 File Offset: 0x000398E4
		public override void Initialize()
		{
			this.Cells = this.InstantiateCells();
			for (int i = 0; i < this.Cells.Length; i++)
			{
				this.Cells[i].SetContext(base.Context);
				this.Cells[i].Initialize();
			}
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0003B730 File Offset: 0x00039930
		public override void UpdateContent(TItemData[] contents)
		{
			int num = base.Index * base.Context.GetGroupCount();
			for (int i = 0; i < this.Cells.Length; i++)
			{
				this.Cells[i].Index = i + num;
				this.Cells[i].SetVisible(i < contents.Length);
				if (this.Cells[i].IsVisible)
				{
					this.Cells[i].UpdateContent(contents[i]);
				}
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0003B7B4 File Offset: 0x000399B4
		public override void UpdatePosition(float position)
		{
			for (int i = 0; i < this.Cells.Length; i++)
			{
				this.Cells[i].UpdatePosition(position);
			}
		}
	}
}

using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002F7 RID: 759
	public abstract class FancyScrollView<TItemData, TContext> : MonoBehaviour where TContext : class, new()
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060010CE RID: 4302
		protected abstract GameObject CellPrefab { get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0003B3A4 File Offset: 0x000395A4
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x0003B3AC File Offset: 0x000395AC
		protected IList<TItemData> ItemsSource { get; set; } = new List<TItemData>();

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x0003B3B5 File Offset: 0x000395B5
		protected TContext Context { get; } = Activator.CreateInstance<TContext>();

		// Token: 0x060010D2 RID: 4306 RVA: 0x0003B3BD File Offset: 0x000395BD
		protected virtual void Initialize()
		{
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0003B3BF File Offset: 0x000395BF
		protected virtual void UpdateContents(IList<TItemData> itemsSource)
		{
			this.ItemsSource = itemsSource;
			this.Refresh();
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0003B3CE File Offset: 0x000395CE
		protected virtual void Relayout()
		{
			this.UpdatePosition(this.currentPosition, false);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0003B3DD File Offset: 0x000395DD
		protected virtual void Refresh()
		{
			this.UpdatePosition(this.currentPosition, true);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0003B3EC File Offset: 0x000395EC
		protected virtual void UpdatePosition(float position)
		{
			this.UpdatePosition(position, false);
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003B3F8 File Offset: 0x000395F8
		private void UpdatePosition(float position, bool forceRefresh)
		{
			if (!this.initialized)
			{
				this.Initialize();
				this.initialized = true;
			}
			this.currentPosition = position;
			float num = position - this.scrollOffset / this.cellInterval;
			int firstIndex = Mathf.CeilToInt(num);
			float num2 = (Mathf.Ceil(num) - num) * this.cellInterval;
			if (num2 + (float)this.pool.Count * this.cellInterval < 1f)
			{
				this.ResizePool(num2);
			}
			this.UpdateCells(num2, firstIndex, forceRefresh);
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0003B474 File Offset: 0x00039674
		private void ResizePool(float firstPosition)
		{
			int num = Mathf.CeilToInt((1f - firstPosition) / this.cellInterval) - this.pool.Count;
			for (int i = 0; i < num; i++)
			{
				FancyCell<TItemData, TContext> component = Object.Instantiate<GameObject>(this.CellPrefab, this.cellContainer).GetComponent<FancyCell<TItemData, TContext>>();
				if (component == null)
				{
					throw new MissingComponentException(string.Format("FancyCell<{0}, {1}> component not found in {2}.", typeof(TItemData).FullName, typeof(TContext).FullName, this.CellPrefab.name));
				}
				component.SetContext(this.Context);
				component.Initialize();
				component.SetVisible(false);
				this.pool.Add(component);
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0003B534 File Offset: 0x00039734
		private void UpdateCells(float firstPosition, int firstIndex, bool forceRefresh)
		{
			for (int i = 0; i < this.pool.Count; i++)
			{
				int num = firstIndex + i;
				float num2 = firstPosition + (float)i * this.cellInterval;
				FancyCell<TItemData, TContext> fancyCell = this.pool[this.CircularIndex(num, this.pool.Count)];
				if (this.loop)
				{
					num = this.CircularIndex(num, this.ItemsSource.Count);
				}
				if (num < 0 || num >= this.ItemsSource.Count || num2 > 1f)
				{
					fancyCell.SetVisible(false);
				}
				else
				{
					if (forceRefresh || fancyCell.Index != num || !fancyCell.IsVisible)
					{
						fancyCell.Index = num;
						fancyCell.SetVisible(true);
						fancyCell.UpdateContent(this.ItemsSource[num]);
					}
					fancyCell.UpdatePosition(num2);
				}
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0003B604 File Offset: 0x00039804
		private int CircularIndex(int i, int size)
		{
			if (size < 1)
			{
				return 0;
			}
			if (i >= 0)
			{
				return i % size;
			}
			return size - 1 + (i + 1) % size;
		}

		// Token: 0x04000BCA RID: 3018
		[SerializeField]
		[Range(0.01f, 1f)]
		protected float cellInterval = 0.2f;

		// Token: 0x04000BCB RID: 3019
		[SerializeField]
		[Range(0f, 1f)]
		protected float scrollOffset = 0.5f;

		// Token: 0x04000BCC RID: 3020
		[SerializeField]
		protected bool loop;

		// Token: 0x04000BCD RID: 3021
		[SerializeField]
		protected Transform cellContainer;

		// Token: 0x04000BCE RID: 3022
		private readonly IList<FancyCell<TItemData, TContext>> pool = new List<FancyCell<TItemData, TContext>>();

		// Token: 0x04000BCF RID: 3023
		protected bool initialized;

		// Token: 0x04000BD0 RID: 3024
		protected float currentPosition;
	}
}

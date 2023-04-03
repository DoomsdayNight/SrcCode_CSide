using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002F5 RID: 757
	public abstract class FancyCell<TItemData, TContext> : MonoBehaviour where TContext : class, new()
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0003B33C File Offset: 0x0003953C
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x0003B344 File Offset: 0x00039544
		public int Index { get; set; } = -1;

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x0003B34D File Offset: 0x0003954D
		public virtual bool IsVisible
		{
			get
			{
				return base.gameObject.activeSelf;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0003B35A File Offset: 0x0003955A
		// (set) Token: 0x060010C5 RID: 4293 RVA: 0x0003B362 File Offset: 0x00039562
		private protected TContext Context { protected get; private set; }

		// Token: 0x060010C6 RID: 4294 RVA: 0x0003B36B File Offset: 0x0003956B
		public virtual void SetContext(TContext context)
		{
			this.Context = context;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0003B374 File Offset: 0x00039574
		public virtual void Initialize()
		{
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0003B376 File Offset: 0x00039576
		public virtual void SetVisible(bool visible)
		{
			base.gameObject.SetActive(visible);
		}

		// Token: 0x060010C9 RID: 4297
		public abstract void UpdateContent(TItemData itemData);

		// Token: 0x060010CA RID: 4298
		public abstract void UpdatePosition(float position);
	}
}

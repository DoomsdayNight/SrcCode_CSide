using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000301 RID: 769
	public interface IFancyGridViewContext : IFancyScrollRectContext, IFancyCellGroupContext
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600110C RID: 4364
		// (set) Token: 0x0600110D RID: 4365
		Func<float> GetStartAxisSpacing { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600110E RID: 4366
		// (set) Token: 0x0600110F RID: 4367
		Func<float> GetCellSize { get; set; }
	}
}

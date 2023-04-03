using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000300 RID: 768
	public interface IFancyCellGroupContext
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06001108 RID: 4360
		// (set) Token: 0x06001109 RID: 4361
		GameObject CellTemplate { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600110A RID: 4362
		// (set) Token: 0x0600110B RID: 4363
		Func<int> GetGroupCount { get; set; }
	}
}

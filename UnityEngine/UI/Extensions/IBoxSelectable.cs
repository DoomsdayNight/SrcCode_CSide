using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x020002CA RID: 714
	public interface IBoxSelectable
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000F57 RID: 3927
		// (set) Token: 0x06000F58 RID: 3928
		bool selected { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000F59 RID: 3929
		// (set) Token: 0x06000F5A RID: 3930
		bool preSelected { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000F5B RID: 3931
		Transform transform { get; }
	}
}

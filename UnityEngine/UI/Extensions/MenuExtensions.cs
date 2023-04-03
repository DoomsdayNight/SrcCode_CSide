using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200031B RID: 795
	public static class MenuExtensions
	{
		// Token: 0x06001245 RID: 4677 RVA: 0x00041D48 File Offset: 0x0003FF48
		public static Menu GetMenu(this GameObject go)
		{
			return go.GetComponent<Menu>();
		}
	}
}

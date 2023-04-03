using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200031C RID: 796
	public abstract class SimpleMenu<T> : Menu<T> where T : SimpleMenu<T>
	{
		// Token: 0x06001246 RID: 4678 RVA: 0x00041D50 File Offset: 0x0003FF50
		public static void Show()
		{
			Menu<T>.Open();
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00041D57 File Offset: 0x0003FF57
		public static void Hide()
		{
			Menu<T>.Close();
		}
	}
}

using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200033D RID: 829
	public static class ScrollRectExtensions
	{
		// Token: 0x0600137A RID: 4986 RVA: 0x00049106 File Offset: 0x00047306
		public static void ScrollToTop(this ScrollRect scrollRect)
		{
			scrollRect.normalizedPosition = new Vector2(0f, 1f);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0004911D File Offset: 0x0004731D
		public static void ScrollToBottom(this ScrollRect scrollRect)
		{
			scrollRect.normalizedPosition = new Vector2(0f, 0f);
		}
	}
}

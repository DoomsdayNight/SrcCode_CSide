using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000319 RID: 793
	public abstract class Menu : MonoBehaviour
	{
		// Token: 0x06001233 RID: 4659
		public abstract void OnBackPressed();

		// Token: 0x04000C9A RID: 3226
		[Tooltip("Destroy the Game Object when menu is closed (reduces memory usage)")]
		public bool DestroyWhenClosed = true;

		// Token: 0x04000C9B RID: 3227
		[Tooltip("Disable menus that are under this one in the stack")]
		public bool DisableMenusUnderneath = true;
	}
}

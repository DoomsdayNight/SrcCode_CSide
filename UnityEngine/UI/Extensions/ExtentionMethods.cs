using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000335 RID: 821
	public static class ExtentionMethods
	{
		// Token: 0x06001353 RID: 4947 RVA: 0x00048800 File Offset: 0x00046A00
		public static T GetOrAddComponent<T>(this GameObject child) where T : Component
		{
			T t = child.GetComponent<T>();
			if (t == null)
			{
				t = child.AddComponent<T>();
			}
			return t;
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0004882C File Offset: 0x00046A2C
		public static bool IsPrefab(this GameObject gameObject)
		{
			if (gameObject == null)
			{
				throw new ArgumentNullException("gameObject");
			}
			return !gameObject.scene.IsValid() && !gameObject.scene.isLoaded && gameObject.GetInstanceID() >= 0 && !gameObject.hideFlags.HasFlag(HideFlags.HideInHierarchy);
		}
	}
}

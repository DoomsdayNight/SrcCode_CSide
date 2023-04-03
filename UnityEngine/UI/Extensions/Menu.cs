using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000318 RID: 792
	public abstract class Menu<T> : Menu where T : Menu<T>
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00041910 File Offset: 0x0003FB10
		// (set) Token: 0x0600122C RID: 4652 RVA: 0x00041917 File Offset: 0x0003FB17
		public static T Instance { get; private set; }

		// Token: 0x0600122D RID: 4653 RVA: 0x0004191F File Offset: 0x0003FB1F
		protected virtual void Awake()
		{
			Menu<T>.Instance = (T)((object)this);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0004192C File Offset: 0x0003FB2C
		protected virtual void OnDestroy()
		{
			Menu<T>.Instance = default(T);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00041948 File Offset: 0x0003FB48
		protected static void Open()
		{
			GameObject go = null;
			if (Menu<T>.Instance == null)
			{
				MenuManager.Instance.CreateInstance(typeof(T).Name, out go);
				MenuManager.Instance.OpenMenu(go.GetMenu());
				return;
			}
			Menu<T>.Instance.gameObject.SetActive(true);
			MenuManager.Instance.OpenMenu(Menu<T>.Instance);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x000419C0 File Offset: 0x0003FBC0
		protected static void Close()
		{
			if (Menu<T>.Instance == null)
			{
				Debug.LogErrorFormat("Trying to close menu {0} but Instance is null", new object[]
				{
					typeof(T)
				});
				return;
			}
			MenuManager.Instance.CloseMenu(Menu<T>.Instance);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00041A11 File Offset: 0x0003FC11
		public override void OnBackPressed()
		{
			Menu<T>.Close();
		}
	}
}

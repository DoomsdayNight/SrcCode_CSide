using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200031A RID: 794
	[AddComponentMenu("UI/Extensions/Menu Manager")]
	[DisallowMultipleComponent]
	public class MenuManager : MonoBehaviour
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x00041A36 File Offset: 0x0003FC36
		// (set) Token: 0x06001236 RID: 4662 RVA: 0x00041A3E File Offset: 0x0003FC3E
		public Menu[] MenuScreens
		{
			get
			{
				return this.menuScreens;
			}
			set
			{
				this.menuScreens = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x00041A47 File Offset: 0x0003FC47
		// (set) Token: 0x06001238 RID: 4664 RVA: 0x00041A4F File Offset: 0x0003FC4F
		public int StartScreen
		{
			get
			{
				return this.startScreen;
			}
			set
			{
				this.startScreen = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x00041A58 File Offset: 0x0003FC58
		// (set) Token: 0x0600123A RID: 4666 RVA: 0x00041A5F File Offset: 0x0003FC5F
		public static MenuManager Instance { get; set; }

		// Token: 0x0600123B RID: 4667 RVA: 0x00041A68 File Offset: 0x0003FC68
		private void Start()
		{
			MenuManager.Instance = this;
			if (this.MenuScreens.Length > this.StartScreen)
			{
				GameObject go = this.CreateInstance(this.MenuScreens[this.StartScreen].name);
				this.OpenMenu(go.GetMenu());
				return;
			}
			Debug.LogError("Not enough Menu Screens configured");
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00041ABB File Offset: 0x0003FCBB
		private void OnDestroy()
		{
			MenuManager.Instance = null;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00041AC3 File Offset: 0x0003FCC3
		public GameObject CreateInstance(string MenuName)
		{
			return Object.Instantiate<GameObject>(this.GetPrefab(MenuName), base.transform);
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00041AD8 File Offset: 0x0003FCD8
		public void CreateInstance(string MenuName, out GameObject menuInstance)
		{
			GameObject prefab = this.GetPrefab(MenuName);
			menuInstance = Object.Instantiate<GameObject>(prefab, base.transform);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00041AFC File Offset: 0x0003FCFC
		public void OpenMenu(Menu menuInstance)
		{
			if (this.menuStack.Count > 0)
			{
				if (menuInstance.DisableMenusUnderneath)
				{
					foreach (Menu menu in this.menuStack)
					{
						menu.gameObject.SetActive(false);
						if (menu.DisableMenusUnderneath)
						{
							break;
						}
					}
				}
				Canvas component = menuInstance.GetComponent<Canvas>();
				if (component != null)
				{
					Canvas component2 = this.menuStack.Peek().GetComponent<Canvas>();
					if (component2 != null)
					{
						component.sortingOrder = component2.sortingOrder + 1;
					}
				}
			}
			this.menuStack.Push(menuInstance);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00041BBC File Offset: 0x0003FDBC
		private GameObject GetPrefab(string PrefabName)
		{
			for (int i = 0; i < this.MenuScreens.Length; i++)
			{
				if (this.MenuScreens[i].name == PrefabName)
				{
					return this.MenuScreens[i].gameObject;
				}
			}
			throw new MissingReferenceException("Prefab not found for " + PrefabName);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00041C10 File Offset: 0x0003FE10
		public void CloseMenu(Menu menu)
		{
			if (this.menuStack.Count == 0)
			{
				Debug.LogErrorFormat(menu, "{0} cannot be closed because menu stack is empty", new object[]
				{
					menu.GetType()
				});
				return;
			}
			if (this.menuStack.Peek() != menu)
			{
				Debug.LogErrorFormat(menu, "{0} cannot be closed because it is not on top of stack", new object[]
				{
					menu.GetType()
				});
				return;
			}
			this.CloseTopMenu();
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00041C7C File Offset: 0x0003FE7C
		public void CloseTopMenu()
		{
			Menu menu = this.menuStack.Pop();
			if (menu.DestroyWhenClosed)
			{
				Object.Destroy(menu.gameObject);
			}
			else
			{
				menu.gameObject.SetActive(false);
			}
			foreach (Menu menu2 in this.menuStack)
			{
				menu2.gameObject.SetActive(true);
				if (menu2.DisableMenusUnderneath)
				{
					break;
				}
			}
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00041D0C File Offset: 0x0003FF0C
		private void Update()
		{
			if (UIExtensionsInputManager.GetKeyDown(KeyCode.Escape) && this.menuStack.Count > 0)
			{
				this.menuStack.Peek().OnBackPressed();
			}
		}

		// Token: 0x04000C9C RID: 3228
		[SerializeField]
		private Menu[] menuScreens;

		// Token: 0x04000C9D RID: 3229
		[SerializeField]
		private int startScreen;

		// Token: 0x04000C9E RID: 3230
		private Stack<Menu> menuStack = new Stack<Menu>();
	}
}

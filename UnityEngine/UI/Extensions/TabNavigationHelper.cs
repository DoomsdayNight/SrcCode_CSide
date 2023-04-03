using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x02000329 RID: 809
	[RequireComponent(typeof(EventSystem))]
	[AddComponentMenu("Event/Extensions/Tab Navigation Helper")]
	public class TabNavigationHelper : MonoBehaviour
	{
		// Token: 0x060012E4 RID: 4836 RVA: 0x00045D58 File Offset: 0x00043F58
		private void Start()
		{
			this._system = base.GetComponent<EventSystem>();
			if (this._system == null)
			{
				Debug.LogError("Needs to be attached to the Event System component in the scene");
			}
			if (this.NavigationMode == NavigationMode.Manual && this.NavigationPath.Length != 0)
			{
				this.StartingObject = this.NavigationPath[0].gameObject.GetComponent<Selectable>();
			}
			if (this.StartingObject == null && this.CircularNavigation)
			{
				this.SelectDefaultObject(out this.StartingObject);
			}
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00045DD8 File Offset: 0x00043FD8
		public void Update()
		{
			Selectable selectable = null;
			if (this.LastObject == null && this._system.currentSelectedGameObject != null)
			{
				selectable = this._system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
				while (selectable != null)
				{
					this.LastObject = selectable;
					selectable = selectable.FindSelectableOnDown();
				}
			}
			if (UIExtensionsInputManager.GetKeyDown(KeyCode.Tab) && UIExtensionsInputManager.GetKey(KeyCode.LeftShift))
			{
				if (this.NavigationMode == NavigationMode.Manual && this.NavigationPath.Length != 0)
				{
					for (int i = this.NavigationPath.Length - 1; i >= 0; i--)
					{
						if (!(this._system.currentSelectedGameObject != this.NavigationPath[i].gameObject))
						{
							selectable = ((i == 0) ? this.NavigationPath[this.NavigationPath.Length - 1] : this.NavigationPath[i - 1]);
							break;
						}
					}
				}
				else if (this._system.currentSelectedGameObject != null)
				{
					selectable = this._system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
					if (selectable == null && this.CircularNavigation)
					{
						selectable = this.LastObject;
					}
				}
				else
				{
					this.SelectDefaultObject(out selectable);
				}
			}
			else if (UIExtensionsInputManager.GetKeyDown(KeyCode.Tab))
			{
				if (this.NavigationMode == NavigationMode.Manual && this.NavigationPath.Length != 0)
				{
					for (int j = 0; j < this.NavigationPath.Length; j++)
					{
						if (!(this._system.currentSelectedGameObject != this.NavigationPath[j].gameObject))
						{
							selectable = ((j == this.NavigationPath.Length - 1) ? this.NavigationPath[0] : this.NavigationPath[j + 1]);
							break;
						}
					}
				}
				else if (this._system.currentSelectedGameObject != null)
				{
					selectable = this._system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
					if (selectable == null && this.CircularNavigation)
					{
						selectable = this.StartingObject;
					}
				}
				else
				{
					this.SelectDefaultObject(out selectable);
				}
			}
			else if (this._system.currentSelectedGameObject == null)
			{
				this.SelectDefaultObject(out selectable);
			}
			if (this.CircularNavigation && this.StartingObject == null)
			{
				this.StartingObject = selectable;
			}
			this.selectGameObject(selectable);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0004601E File Offset: 0x0004421E
		private void SelectDefaultObject(out Selectable next)
		{
			if (this._system.firstSelectedGameObject)
			{
				next = this._system.firstSelectedGameObject.GetComponent<Selectable>();
				return;
			}
			next = null;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00046048 File Offset: 0x00044248
		private void selectGameObject(Selectable selectable)
		{
			if (selectable != null)
			{
				InputField component = selectable.GetComponent<InputField>();
				if (component != null)
				{
					component.OnPointerClick(new PointerEventData(this._system));
				}
				this._system.SetSelectedGameObject(selectable.gameObject, new BaseEventData(this._system));
			}
		}

		// Token: 0x04000D10 RID: 3344
		private EventSystem _system;

		// Token: 0x04000D11 RID: 3345
		private Selectable StartingObject;

		// Token: 0x04000D12 RID: 3346
		private Selectable LastObject;

		// Token: 0x04000D13 RID: 3347
		[Tooltip("The path to take when user is tabbing through ui components.")]
		public Selectable[] NavigationPath;

		// Token: 0x04000D14 RID: 3348
		[Tooltip("Use the default Unity navigation system or a manual fixed order using Navigation Path")]
		public NavigationMode NavigationMode;

		// Token: 0x04000D15 RID: 3349
		[Tooltip("If True, this will loop the tab order from last to first automatically")]
		public bool CircularNavigation;
	}
}

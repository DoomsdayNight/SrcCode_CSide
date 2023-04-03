using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x020002AF RID: 687
	[AddComponentMenu("UI/Extensions/Extensions Toggle Group")]
	[DisallowMultipleComponent]
	public class ExtensionsToggleGroup : UIBehaviour
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00029B20 File Offset: 0x00027D20
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x00029B28 File Offset: 0x00027D28
		public bool AllowSwitchOff
		{
			get
			{
				return this.m_AllowSwitchOff;
			}
			set
			{
				this.m_AllowSwitchOff = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00029B31 File Offset: 0x00027D31
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x00029B39 File Offset: 0x00027D39
		public ExtensionsToggle SelectedToggle { get; private set; }

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00029B42 File Offset: 0x00027D42
		protected ExtensionsToggleGroup()
		{
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00029B6B File Offset: 0x00027D6B
		private void ValidateToggleIsInGroup(ExtensionsToggle toggle)
		{
			if (toggle == null || !this.m_Toggles.Contains(toggle))
			{
				throw new ArgumentException(string.Format("Toggle {0} is not part of ToggleGroup {1}", new object[]
				{
					toggle,
					this
				}));
			}
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00029BA4 File Offset: 0x00027DA4
		public void NotifyToggleOn(ExtensionsToggle toggle)
		{
			this.ValidateToggleIsInGroup(toggle);
			for (int i = 0; i < this.m_Toggles.Count; i++)
			{
				if (this.m_Toggles[i] == toggle)
				{
					this.SelectedToggle = toggle;
				}
				else
				{
					this.m_Toggles[i].IsOn = false;
				}
			}
			this.onToggleGroupChanged.Invoke(this.AnyTogglesOn());
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00029C0E File Offset: 0x00027E0E
		public void UnregisterToggle(ExtensionsToggle toggle)
		{
			if (this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Remove(toggle);
				toggle.onValueChanged.RemoveListener(new UnityAction<bool>(this.NotifyToggleChanged));
			}
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00029C42 File Offset: 0x00027E42
		private void NotifyToggleChanged(bool isOn)
		{
			this.onToggleGroupToggleChanged.Invoke(isOn);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00029C50 File Offset: 0x00027E50
		public void RegisterToggle(ExtensionsToggle toggle)
		{
			if (!this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Add(toggle);
				toggle.onValueChanged.AddListener(new UnityAction<bool>(this.NotifyToggleChanged));
			}
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00029C83 File Offset: 0x00027E83
		public bool AnyTogglesOn()
		{
			return this.m_Toggles.Find((ExtensionsToggle x) => x.IsOn) != null;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00029CB5 File Offset: 0x00027EB5
		public IEnumerable<ExtensionsToggle> ActiveToggles()
		{
			return from x in this.m_Toggles
			where x.IsOn
			select x;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00029CE4 File Offset: 0x00027EE4
		public void SetAllTogglesOff()
		{
			bool allowSwitchOff = this.m_AllowSwitchOff;
			this.m_AllowSwitchOff = true;
			for (int i = 0; i < this.m_Toggles.Count; i++)
			{
				this.m_Toggles[i].IsOn = false;
			}
			this.m_AllowSwitchOff = allowSwitchOff;
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00029D2E File Offset: 0x00027F2E
		public void HasTheGroupToggle(bool value)
		{
			Debug.Log("Testing, the group has toggled [" + value.ToString() + "]");
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00029D4B File Offset: 0x00027F4B
		public void HasAToggleFlipped(bool value)
		{
			Debug.Log("Testing, a toggle has toggled [" + value.ToString() + "]");
		}

		// Token: 0x040009A9 RID: 2473
		[SerializeField]
		private bool m_AllowSwitchOff;

		// Token: 0x040009AA RID: 2474
		private List<ExtensionsToggle> m_Toggles = new List<ExtensionsToggle>();

		// Token: 0x040009AB RID: 2475
		public ExtensionsToggleGroup.ToggleGroupEvent onToggleGroupChanged = new ExtensionsToggleGroup.ToggleGroupEvent();

		// Token: 0x040009AC RID: 2476
		public ExtensionsToggleGroup.ToggleGroupEvent onToggleGroupToggleChanged = new ExtensionsToggleGroup.ToggleGroupEvent();

		// Token: 0x0200111F RID: 4383
		[Serializable]
		public class ToggleGroupEvent : UnityEvent<bool>
		{
		}
	}
}

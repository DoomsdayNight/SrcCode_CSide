using System;
using System.Collections.Generic;
using NKC.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NKC
{
	// Token: 0x02000766 RID: 1894
	public class NKCUIComToggleGroup : MonoBehaviour
	{
		// Token: 0x06004B98 RID: 19352 RVA: 0x0016A196 File Offset: 0x00168396
		private void Start()
		{
			this.m_uiRoot = NKCUIManager.FindRootUIBase(base.transform);
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x0016A1A9 File Offset: 0x001683A9
		public void RegisterToggle(NKCUIComToggle toggle)
		{
			if (!this.m_lstComToggles.Contains(toggle))
			{
				this.m_lstComToggles.Add(toggle);
			}
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x0016A1C5 File Offset: 0x001683C5
		public void DeregisterToggle(NKCUIComToggle toggle)
		{
			if (this.m_lstComToggles.Contains(toggle))
			{
				this.m_lstComToggles.Remove(toggle);
			}
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x0016A1E4 File Offset: 0x001683E4
		public void OnCheckOneToggle(NKCUIComToggle SelectedToggle)
		{
			if (!this.m_bMultiSelect)
			{
				using (List<NKCUIComToggle>.Enumerator enumerator = this.m_lstComToggles.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						NKCUIComToggle nkcuicomToggle = enumerator.Current;
						if (nkcuicomToggle != SelectedToggle)
						{
							nkcuicomToggle.Select(false, !this.m_bCallbackOnUnSelect, false);
						}
					}
					return;
				}
			}
			int num = 0;
			using (List<NKCUIComToggle>.Enumerator enumerator = this.m_lstComToggles.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.m_bChecked)
					{
						num++;
					}
				}
			}
			if (num > this.m_MaxMultiCount)
			{
				SelectedToggle.Select(false, true, false);
			}
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x0016A2B0 File Offset: 0x001684B0
		public void SetAllToggleUnselected()
		{
			foreach (NKCUIComToggle nkcuicomToggle in this.m_lstComToggles)
			{
				nkcuicomToggle.Select(false, !this.m_bCallbackOnUnSelect, false);
			}
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x0016A30C File Offset: 0x0016850C
		public void SetHotkey(HotkeyEventType prev, HotkeyEventType next)
		{
			this.m_hotKeyPrev = prev;
			this.m_hotkeyNext = next;
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x0016A31C File Offset: 0x0016851C
		private void Update()
		{
			if (this.m_lstComToggles.Count <= 1)
			{
				return;
			}
			if (this.m_hotKeyPrev != HotkeyEventType.None || this.m_hotkeyNext != HotkeyEventType.None)
			{
				if (!NKCUIManager.IsTopmostUI(this.m_uiRoot))
				{
					return;
				}
				if (NKCInputManager.CheckHotKeyEvent(this.m_hotKeyPrev))
				{
					if (this.MoveSelection(-1))
					{
						NKCInputManager.ConsumeHotKeyEvent(this.m_hotKeyPrev);
					}
				}
				else if (NKCInputManager.CheckHotKeyEvent(this.m_hotkeyNext) && this.MoveSelection(1))
				{
					NKCInputManager.ConsumeHotKeyEvent(this.m_hotkeyNext);
				}
				if (NKCInputManager.CheckHotKeyEvent(HotkeyEventType.ShowHotkey))
				{
					NKCUIComHotkeyDisplay.OpenInstance(base.transform, new HotkeyEventType[]
					{
						this.m_hotKeyPrev,
						this.m_hotkeyNext
					});
					return;
				}
			}
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x0016A3C8 File Offset: 0x001685C8
		private bool MoveSelection(int delta)
		{
			int num = this.m_lstComToggles.FindIndex((NKCUIComToggle x) => x.m_bSelect);
			if (num < 0)
			{
				return false;
			}
			int index = (num + this.m_lstComToggles.Count + delta) % this.m_lstComToggles.Count;
			NKCUIComToggle nkcuicomToggle = this.m_lstComToggles[index];
			PointerEventData pointerEventData;
			if (!nkcuicomToggle.CanCastRaycast(out pointerEventData))
			{
				return false;
			}
			nkcuicomToggle.Select(true, false, false);
			return true;
		}

		// Token: 0x04003A34 RID: 14900
		public bool m_bAllowSwitchOff;

		// Token: 0x04003A35 RID: 14901
		private List<NKCUIComToggle> m_lstComToggles = new List<NKCUIComToggle>();

		// Token: 0x04003A36 RID: 14902
		public bool m_bCallbackOnUnSelect;

		// Token: 0x04003A37 RID: 14903
		public bool m_bMultiSelect;

		// Token: 0x04003A38 RID: 14904
		public int m_MaxMultiCount = 1;

		// Token: 0x04003A39 RID: 14905
		public HotkeyEventType m_hotKeyPrev;

		// Token: 0x04003A3A RID: 14906
		public HotkeyEventType m_hotkeyNext;

		// Token: 0x04003A3B RID: 14907
		private NKCUIBase m_uiRoot;
	}
}

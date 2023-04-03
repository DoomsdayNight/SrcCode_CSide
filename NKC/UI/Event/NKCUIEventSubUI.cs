using System;
using System.Collections.Generic;
using NKM;
using NKM.Event;
using UnityEngine;

namespace NKC.UI.Event
{
	// Token: 0x02000BD8 RID: 3032
	public class NKCUIEventSubUI : MonoBehaviour
	{
		// Token: 0x06008C9E RID: 35998 RVA: 0x002FD450 File Offset: 0x002FB650
		public void Init()
		{
			NKCUIEventSubUIBase[] components = base.gameObject.GetComponents<NKCUIEventSubUIBase>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].Init();
				this.m_listSubUI.Add(components[i]);
			}
		}

		// Token: 0x06008C9F RID: 35999 RVA: 0x002FD490 File Offset: 0x002FB690
		public void Open(NKMEventTabTemplet tabTemplet)
		{
			for (int i = 0; i < this.m_listSubUI.Count; i++)
			{
				this.m_listSubUI[i].Open(tabTemplet);
			}
		}

		// Token: 0x06008CA0 RID: 36000 RVA: 0x002FD4C8 File Offset: 0x002FB6C8
		public void Refresh()
		{
			for (int i = 0; i < this.m_listSubUI.Count; i++)
			{
				this.m_listSubUI[i].Refresh();
			}
		}

		// Token: 0x06008CA1 RID: 36001 RVA: 0x002FD4FC File Offset: 0x002FB6FC
		public void Close()
		{
			for (int i = 0; i < this.m_listSubUI.Count; i++)
			{
				this.m_listSubUI[i].Close();
			}
		}

		// Token: 0x06008CA2 RID: 36002 RVA: 0x002FD530 File Offset: 0x002FB730
		public void Hide()
		{
			for (int i = 0; i < this.m_listSubUI.Count; i++)
			{
				this.m_listSubUI[i].Hide();
			}
		}

		// Token: 0x06008CA3 RID: 36003 RVA: 0x002FD564 File Offset: 0x002FB764
		public void UnHide()
		{
			for (int i = 0; i < this.m_listSubUI.Count; i++)
			{
				this.m_listSubUI[i].UnHide();
			}
		}

		// Token: 0x06008CA4 RID: 36004 RVA: 0x002FD598 File Offset: 0x002FB798
		public bool OnBackButton()
		{
			bool flag = false;
			for (int i = 0; i < this.m_listSubUI.Count; i++)
			{
				flag |= this.m_listSubUI[i].OnBackButton();
			}
			return flag;
		}

		// Token: 0x06008CA5 RID: 36005 RVA: 0x002FD5D4 File Offset: 0x002FB7D4
		public void OnInventoryChange(NKMItemMiscData itemData)
		{
			for (int i = 0; i < this.m_listSubUI.Count; i++)
			{
				this.m_listSubUI[i].OnInventoryChange(itemData);
			}
		}

		// Token: 0x06008CA6 RID: 36006 RVA: 0x002FD609 File Offset: 0x002FB809
		public IEnumerable<T> GetSubUIs<T>() where T : NKCUIEventSubUIBase
		{
			int num;
			for (int i = 0; i < this.m_listSubUI.Count; i = num + 1)
			{
				if (this.m_listSubUI[i] is T)
				{
					yield return this.m_listSubUI[i] as T;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x0400798C RID: 31116
		private List<NKCUIEventSubUIBase> m_listSubUI = new List<NKCUIEventSubUIBase>();
	}
}

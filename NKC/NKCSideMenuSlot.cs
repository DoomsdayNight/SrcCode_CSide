using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x020007AF RID: 1967
	public class NKCSideMenuSlot : MonoBehaviour
	{
		// Token: 0x06004DB4 RID: 19892 RVA: 0x00177164 File Offset: 0x00175364
		public void Init(string title, NKCUIComToggleGroup toggleGroup, RectTransform rtParent)
		{
			base.gameObject.GetComponent<RectTransform>().SetParent(rtParent);
			this.m_Toggle.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChange));
			NKCUtil.SetLabelText(this.m_TEXT, title);
			this.m_Toggle.SetToggleGroup(toggleGroup);
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x001771B6 File Offset: 0x001753B6
		public void SetCallBackFunction(UnityAction callback)
		{
			this.dOnCallBack = callback;
		}

		// Token: 0x06004DB6 RID: 19894 RVA: 0x001771C0 File Offset: 0x001753C0
		public void OnValueChange(bool bVal)
		{
			Color col = bVal ? NKCUtil.GetColor("#011B3B") : NKCUtil.GetColor("#FFFFFF");
			NKCUtil.SetLabelTextColor(this.m_TEXT, col);
			this.OnActiveChild(bVal);
		}

		// Token: 0x06004DB7 RID: 19895 RVA: 0x001771FA File Offset: 0x001753FA
		public void AddSubSlot(NKCSideMenuSlotChild child)
		{
			child.m_Toggle.SetToggleGroup(base.gameObject.GetComponent<NKCUIComToggleGroup>());
			this.m_lstSubSlot.Add(child);
		}

		// Token: 0x06004DB8 RID: 19896 RVA: 0x0017721E File Offset: 0x0017541E
		public void AddSubSlot(NKCUISkinSlot child)
		{
			this.m_lstSubSlotSkin.Add(child);
		}

		// Token: 0x06004DB9 RID: 19897 RVA: 0x0017722C File Offset: 0x0017542C
		public bool SelectSubSlot(string ARTICLE_ID)
		{
			bool flag = false;
			foreach (NKCSideMenuSlotChild nkcsideMenuSlotChild in this.m_lstSubSlot)
			{
				bool flag2 = nkcsideMenuSlotChild.OnSelected(ARTICLE_ID);
				if (!flag)
				{
					flag = flag2;
				}
			}
			this.m_Toggle.Select(flag, false, false);
			return flag;
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x00177298 File Offset: 0x00175498
		private void OnActiveChild(bool bActive)
		{
			if (bActive && this.dOnCallBack != null)
			{
				this.dOnCallBack();
				return;
			}
			foreach (NKCSideMenuSlotChild nkcsideMenuSlotChild in this.m_lstSubSlot)
			{
				nkcsideMenuSlotChild.OnActive(bActive);
			}
		}

		// Token: 0x06004DBB RID: 19899 RVA: 0x00177300 File Offset: 0x00175500
		public bool HasChild(string key)
		{
			for (int i = 0; i < this.m_lstSubSlot.Count; i++)
			{
				if (string.Equals(key, this.m_lstSubSlot[i].KEY))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004DBC RID: 19900 RVA: 0x00177340 File Offset: 0x00175540
		public void NotifySelectID(string key)
		{
			for (int i = 0; i < this.m_lstSubSlot.Count; i++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstSubSlot[i].m_SELECTED, string.Equals(key, this.m_lstSubSlot[i].KEY));
				this.m_lstSubSlot[i].m_Toggle.Select(string.Equals(key, this.m_lstSubSlot[i].KEY), true, false);
			}
		}

		// Token: 0x06004DBD RID: 19901 RVA: 0x001773C0 File Offset: 0x001755C0
		public void Clear()
		{
			foreach (NKCSideMenuSlotChild nkcsideMenuSlotChild in this.m_lstSubSlot)
			{
				UnityEngine.Object.Destroy(nkcsideMenuSlotChild.gameObject);
			}
			this.m_lstSubSlot.Clear();
			foreach (NKCUISkinSlot nkcuiskinSlot in this.m_lstSubSlotSkin)
			{
				UnityEngine.Object.Destroy(nkcuiskinSlot.gameObject);
			}
			this.m_lstSubSlotSkin.Clear();
		}

		// Token: 0x06004DBE RID: 19902 RVA: 0x00177470 File Offset: 0x00175670
		public void Lock()
		{
			if (this.m_Toggle != null)
			{
				this.m_Toggle.Lock(false);
			}
			NKCUtil.SetLabelTextColor(this.m_TEXT, NKCUtil.GetColor("#676767"));
		}

		// Token: 0x06004DBF RID: 19903 RVA: 0x001774A1 File Offset: 0x001756A1
		public void UnLock()
		{
			if (this.m_Toggle != null)
			{
				this.m_Toggle.UnLock(false);
			}
			NKCUtil.SetLabelTextColor(this.m_TEXT, NKCUtil.GetColor("#FFFFFF"));
		}

		// Token: 0x06004DC0 RID: 19904 RVA: 0x001774D2 File Offset: 0x001756D2
		public void ForceSelect(bool select)
		{
			if (this.m_Toggle != null)
			{
				this.m_Toggle.Select(select, false, false);
			}
		}

		// Token: 0x04003D73 RID: 15731
		public Text m_TEXT;

		// Token: 0x04003D74 RID: 15732
		public NKCUIComToggle m_Toggle;

		// Token: 0x04003D75 RID: 15733
		public GameObject m_LOCK;

		// Token: 0x04003D76 RID: 15734
		private List<NKCSideMenuSlotChild> m_lstSubSlot = new List<NKCSideMenuSlotChild>();

		// Token: 0x04003D77 RID: 15735
		private List<NKCUISkinSlot> m_lstSubSlotSkin = new List<NKCUISkinSlot>();

		// Token: 0x04003D78 RID: 15736
		private UnityAction dOnCallBack;
	}
}

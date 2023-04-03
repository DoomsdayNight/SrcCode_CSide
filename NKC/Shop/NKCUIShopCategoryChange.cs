using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI.Shop
{
	// Token: 0x02000AD9 RID: 2777
	public class NKCUIShopCategoryChange : MonoBehaviour
	{
		// Token: 0x06007CB1 RID: 31921 RVA: 0x0029BE2C File Offset: 0x0029A02C
		public void Init(NKCUIShopCategoryChangeSlot.OnSelectCategory onSelectCategory)
		{
			foreach (NKCUIShopCategoryChangeSlot nkcuishopCategoryChangeSlot in this.m_lstSlot)
			{
				nkcuishopCategoryChangeSlot.Init(onSelectCategory);
			}
			NKCUtil.SetButtonClickDelegate(this.m_csbtnClose, new UnityAction(this.Close));
		}

		// Token: 0x06007CB2 RID: 31922 RVA: 0x0029BE94 File Offset: 0x0029A094
		public void Open()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			for (int i = 0; i < this.m_lstSlot.Count; i++)
			{
				this.m_lstSlot[i].CheckReddot();
			}
		}

		// Token: 0x06007CB3 RID: 31923 RVA: 0x0029BED4 File Offset: 0x0029A0D4
		private void Close()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x04006968 RID: 26984
		public NKCUIComStateButton m_csbtnClose;

		// Token: 0x04006969 RID: 26985
		public List<NKCUIShopCategoryChangeSlot> m_lstSlot;
	}
}

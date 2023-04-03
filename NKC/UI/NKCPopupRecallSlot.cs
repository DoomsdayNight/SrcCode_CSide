using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A76 RID: 2678
	public class NKCPopupRecallSlot : MonoBehaviour
	{
		// Token: 0x06007673 RID: 30323 RVA: 0x002767E4 File Offset: 0x002749E4
		public void SetData(string title, List<NKCUISlot.SlotData> lstSlotData)
		{
			NKCUtil.SetLabelText(this.m_lbTitle, title);
			if (lstSlotData == null || lstSlotData.Count <= 0)
			{
				for (int i = 0; i < this.m_lstSlot.Count; i++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstSlot[i].gameObject, false);
				}
				NKCUtil.SetGameobjectActive(this.m_objEmpty, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objEmpty, false);
			for (int j = 0; j < this.m_lstSlot.Count; j++)
			{
				if (j < lstSlotData.Count)
				{
					NKCUtil.SetGameobjectActive(this.m_lstSlot[j], true);
					this.m_lstSlot[j].SetMiscItemData(lstSlotData[j], false, true, false, null);
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstSlot[j], false);
				}
			}
		}

		// Token: 0x040062DB RID: 25307
		public Text m_lbTitle;

		// Token: 0x040062DC RID: 25308
		public GameObject m_objEmpty;

		// Token: 0x040062DD RID: 25309
		public List<NKCUISlot> m_lstSlot = new List<NKCUISlot>();
	}
}

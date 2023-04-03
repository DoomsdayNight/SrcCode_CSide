using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A3E RID: 2622
	public class NKCPopupEmblemBigSlot : MonoBehaviour
	{
		// Token: 0x060072F3 RID: 29427 RVA: 0x00263820 File Offset: 0x00261A20
		public void SetData(int miscItemID)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(miscItemID);
			if (itemMiscTempletByID == null)
			{
				return;
			}
			this.m_NKCUISlot.SetMiscItemData(miscItemID, 1L, false, false, true, null);
			NKCUtil.SetLabelText(this.m_lbEmblemName, itemMiscTempletByID.GetItemName());
			NKCUtil.SetLabelText(this.m_lbEmblemDesc, itemMiscTempletByID.GetItemDesc());
		}

		// Token: 0x060072F4 RID: 29428 RVA: 0x0026386C File Offset: 0x00261A6C
		public void SetEmpty(string desc = "")
		{
			this.m_NKCUISlot.SetEmpty(null);
			NKCUtil.SetLabelText(this.m_lbEmblemName, "");
			NKCUtil.SetLabelText(this.m_lbEmblemDesc, desc);
		}

		// Token: 0x04005EED RID: 24301
		public NKCUISlot m_NKCUISlot;

		// Token: 0x04005EEE RID: 24302
		public Text m_lbEmblemName;

		// Token: 0x04005EEF RID: 24303
		public Text m_lbEmblemDesc;
	}
}

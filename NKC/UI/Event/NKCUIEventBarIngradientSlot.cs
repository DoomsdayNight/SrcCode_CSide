using System;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BD3 RID: 3027
	public class NKCUIEventBarIngradientSlot : MonoBehaviour
	{
		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x06008C56 RID: 35926 RVA: 0x002FBD79 File Offset: 0x002F9F79
		public int ItemID
		{
			get
			{
				return this.m_itemID;
			}
		}

		// Token: 0x06008C57 RID: 35927 RVA: 0x002FBD81 File Offset: 0x002F9F81
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnSlot, new UnityAction(this.OnClickSlot));
		}

		// Token: 0x06008C58 RID: 35928 RVA: 0x002FBD9C File Offset: 0x002F9F9C
		public void SetData(int itemID, NKCUIEventBarIngradientSlot.OnSelectIngradient onSelect)
		{
			this.m_itemID = itemID;
			this.m_dOnSelect = onSelect;
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
			if (itemMiscTempletByID == null)
			{
				return;
			}
			NKCUtil.SetImageSprite(this.m_imgIcon, NKCResourceUtility.GetOrLoadMiscItemIcon(itemMiscTempletByID), false);
			NKCUtil.SetLabelText(this.m_lbName, itemMiscTempletByID.GetItemName());
			NKCUtil.SetLabelText(this.m_lbDesc, itemMiscTempletByID.GetItemDesc());
			NKCUIComStateButton csbtnSlot = this.m_csbtnSlot;
			if (csbtnSlot == null)
			{
				return;
			}
			csbtnSlot.Select(false, false, false);
		}

		// Token: 0x06008C59 RID: 35929 RVA: 0x002FBE0A File Offset: 0x002FA00A
		public bool IsSelected()
		{
			return !(this.m_csbtnSlot == null) && this.m_csbtnSlot.m_bSelect;
		}

		// Token: 0x06008C5A RID: 35930 RVA: 0x002FBE28 File Offset: 0x002FA028
		private void OnClickSlot()
		{
			if (this.m_dOnSelect != null)
			{
				bool bSelect = this.m_dOnSelect(this.m_itemID, !this.m_csbtnSlot.m_bSelect);
				this.m_csbtnSlot.Select(bSelect, false, false);
			}
		}

		// Token: 0x0400792C RID: 31020
		public Image m_imgIcon;

		// Token: 0x0400792D RID: 31021
		public Text m_lbName;

		// Token: 0x0400792E RID: 31022
		public Text m_lbDesc;

		// Token: 0x0400792F RID: 31023
		public GameObject m_objSelect;

		// Token: 0x04007930 RID: 31024
		public NKCUIComStateButton m_csbtnSlot;

		// Token: 0x04007931 RID: 31025
		private int m_itemID;

		// Token: 0x04007932 RID: 31026
		private NKCUIEventBarIngradientSlot.OnSelectIngradient m_dOnSelect;

		// Token: 0x020019AD RID: 6573
		// (Invoke) Token: 0x0600B9AE RID: 47534
		public delegate bool OnSelectIngradient(int itemID, bool select);
	}
}

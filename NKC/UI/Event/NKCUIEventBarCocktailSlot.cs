using System;
using NKC.UI.Tooltip;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI.Event
{
	// Token: 0x02000BD1 RID: 3025
	public class NKCUIEventBarCocktailSlot : MonoBehaviour
	{
		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x06008C34 RID: 35892 RVA: 0x002FB000 File Offset: 0x002F9200
		public int CockTailID
		{
			get
			{
				return this.m_cockTailID;
			}
		}

		// Token: 0x06008C35 RID: 35893 RVA: 0x002FB008 File Offset: 0x002F9208
		public void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnSlot, new UnityAction(this.OnClickSlot));
		}

		// Token: 0x06008C36 RID: 35894 RVA: 0x002FB024 File Offset: 0x002F9224
		public void SetData(int cocktailID, NKCUIEventBarCocktailSlot.OnSelectSlot onSelectSlot)
		{
			this.m_cockTailID = cocktailID;
			this.m_dOnSelectSlot = onSelectSlot;
			long num = 0L;
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(cocktailID);
			if (itemMiscTempletByID != null)
			{
				NKCUtil.SetLabelText(this.m_lbCockTailName, itemMiscTempletByID.GetItemName());
				Sprite orLoadMiscItemIcon = NKCResourceUtility.GetOrLoadMiscItemIcon(itemMiscTempletByID);
				NKCUtil.SetImageSprite(this.m_imgCockTailIcon, orLoadMiscItemIcon, false);
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					num = nkmuserData.m_InventoryData.GetCountMiscItem(itemMiscTempletByID.m_ItemMiscID);
				}
			}
			NKCUtil.SetLabelText(this.m_lbCockTailCount, num.ToString());
			NKCUtil.SetGameobjectActive(this.m_objNone, num <= 0L);
			Color white = Color.white;
			if (num <= 0L && !string.IsNullOrEmpty(this.m_strIconColorNone))
			{
				ColorUtility.TryParseHtmlString(this.m_strIconColorNone, out white);
			}
			NKCUtil.SetImageColor(this.m_imgCockTailIcon, white);
			NKCUIComStateButton csbtnSlot = this.m_csbtnSlot;
			if (csbtnSlot == null)
			{
				return;
			}
			csbtnSlot.Select(false, false, false);
		}

		// Token: 0x06008C37 RID: 35895 RVA: 0x002FB0FA File Offset: 0x002F92FA
		public void SetSelected(bool value)
		{
			NKCUIComStateButton csbtnSlot = this.m_csbtnSlot;
			if (csbtnSlot == null)
			{
				return;
			}
			csbtnSlot.Select(value, false, false);
		}

		// Token: 0x06008C38 RID: 35896 RVA: 0x002FB110 File Offset: 0x002F9310
		public bool Toggle()
		{
			if (this.m_csbtnSlot == null)
			{
				return false;
			}
			this.m_csbtnSlot.Select(!this.m_csbtnSlot.m_bSelect, false, false);
			return this.m_csbtnSlot.m_bSelect;
		}

		// Token: 0x06008C39 RID: 35897 RVA: 0x002FB149 File Offset: 0x002F9349
		private void OnClickSlot()
		{
			if (this.m_dOnSelectSlot != null)
			{
				this.m_dOnSelectSlot(base.gameObject);
			}
		}

		// Token: 0x06008C3A RID: 35898 RVA: 0x002FB164 File Offset: 0x002F9364
		private void OnClickPress(PointerEventData eventData)
		{
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(this.m_cockTailID);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			long count = 0L;
			if (nkmuserData != null && itemMiscTempletByID != null)
			{
				count = nkmuserData.m_InventoryData.GetCountMiscItem(itemMiscTempletByID.m_ItemMiscID);
			}
			NKCUISlot.SlotData slotData = NKCUISlot.SlotData.MakeMiscItemData(this.m_cockTailID, count, 0);
			NKCUITooltip.Instance.Open(slotData, new Vector2?(eventData.position));
		}

		// Token: 0x04007900 RID: 30976
		public Text m_lbCockTailName;

		// Token: 0x04007901 RID: 30977
		public Text m_lbCockTailCount;

		// Token: 0x04007902 RID: 30978
		public Image m_imgCockTailIcon;

		// Token: 0x04007903 RID: 30979
		public GameObject m_objNone;

		// Token: 0x04007904 RID: 30980
		public GameObject m_objSelect;

		// Token: 0x04007905 RID: 30981
		public string m_strIconColorNone;

		// Token: 0x04007906 RID: 30982
		public NKCUIComStateButton m_csbtnSlot;

		// Token: 0x04007907 RID: 30983
		private int m_cockTailID;

		// Token: 0x04007908 RID: 30984
		private float timer;

		// Token: 0x04007909 RID: 30985
		private NKCUIEventBarCocktailSlot.OnSelectSlot m_dOnSelectSlot;

		// Token: 0x020019A7 RID: 6567
		// (Invoke) Token: 0x0600B99A RID: 47514
		public delegate void OnSelectSlot(GameObject slot);
	}
}

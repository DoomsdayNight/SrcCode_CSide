using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x0200075C RID: 1884
	public class NKCUIComResourceButton : NKCUIComStateButton
	{
		// Token: 0x06004B39 RID: 19257 RVA: 0x001686F4 File Offset: 0x001668F4
		private void OnEnable()
		{
			this.Init();
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x001686FC File Offset: 0x001668FC
		public void Init()
		{
			if (!this.bInit)
			{
				this.bInit = true;
				if (this.m_ResourceCount != null)
				{
					this.TextOriginalColor = this.m_ResourceCount.color;
				}
			}
		}

		// Token: 0x06004B3B RID: 19259 RVA: 0x0016872C File Offset: 0x0016692C
		public int GetItemID()
		{
			return this.m_ItemID;
		}

		// Token: 0x06004B3C RID: 19260 RVA: 0x00168734 File Offset: 0x00166934
		public void OnShow(bool bShow)
		{
			NKCUtil.SetGameobjectActive(this.m_Obj, bShow);
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x00168744 File Offset: 0x00166944
		public void SetData(int itemID, int itemCount)
		{
			this.m_ItemID = itemID;
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
			if (itemMiscTempletByID == null)
			{
				return;
			}
			Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemMiscTempletByID);
			NKCUtil.SetLabelText(this.m_ResourceCount, itemCount.ToString());
			NKCUtil.SetImageSprite(this.m_ResourceIcon, orLoadMiscItemSmallIcon, false);
			bool bEnough = true;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null && nkmuserData.m_InventoryData.GetCountMiscItem(itemID) < (long)itemCount)
			{
				bEnough = false;
			}
			this.UpdateTextColor(bEnough);
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x001687B0 File Offset: 0x001669B0
		public void SetDataWithUseCount(int itemID, int itemUseCount, string format = "{0}/{1}")
		{
			this.m_ItemID = itemID;
			NKMItemMiscTemplet itemMiscTempletByID = NKMItemManager.GetItemMiscTempletByID(itemID);
			if (itemMiscTempletByID == null)
			{
				return;
			}
			Sprite orLoadMiscItemSmallIcon = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemMiscTempletByID);
			NKCUtil.SetImageSprite(this.m_ResourceIcon, orLoadMiscItemSmallIcon, false);
			bool flag = true;
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			long num = 0L;
			if (nkmuserData != null)
			{
				num = nkmuserData.m_InventoryData.GetCountMiscItem(itemID);
				if (num < (long)itemUseCount)
				{
					flag = false;
				}
			}
			this.UpdateTextColor(true);
			if (flag)
			{
				NKCUtil.SetLabelText(this.m_ResourceCount, string.Format(format, num, itemUseCount));
				return;
			}
			string arg = string.Format("<color=#CD2121>{0}</color>", num);
			NKCUtil.SetLabelText(this.m_ResourceCount, string.Format(format, arg, itemUseCount));
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x00168860 File Offset: 0x00166A60
		private void UpdateTextColor(bool bEnough = false)
		{
			Color textColor = this.TextOriginalColor;
			if (!bEnough)
			{
				textColor = NKCUtil.GetColor("#CD2121");
			}
			this.SetTextColor(textColor);
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x00168889 File Offset: 0x00166A89
		public void SetTextColor(Color col)
		{
			NKCUtil.SetLabelTextColor(this.m_ResourceCount, col);
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x00168897 File Offset: 0x00166A97
		public void SetIconColor(Color col)
		{
			NKCUtil.SetImageColor(this.m_ResourceIcon, col);
		}

		// Token: 0x06004B42 RID: 19266 RVA: 0x001688A5 File Offset: 0x00166AA5
		public void SetText(string newText)
		{
			NKCUtil.SetLabelText(this.m_ResourceCount, newText);
		}

		// Token: 0x06004B43 RID: 19267 RVA: 0x001688B3 File Offset: 0x00166AB3
		public void SetBackgroundSprite(Sprite newSprite)
		{
			NKCUtil.SetImageSprite(this.m_Background, newSprite, false);
		}

		// Token: 0x040039D5 RID: 14805
		public GameObject m_Obj;

		// Token: 0x040039D6 RID: 14806
		public Image m_ResourceIcon;

		// Token: 0x040039D7 RID: 14807
		public Text m_ResourceCount;

		// Token: 0x040039D8 RID: 14808
		public Image m_Background;

		// Token: 0x040039D9 RID: 14809
		private bool bInit;

		// Token: 0x040039DA RID: 14810
		private Color TextOriginalColor = Color.white;

		// Token: 0x040039DB RID: 14811
		private int m_ItemID;
	}
}

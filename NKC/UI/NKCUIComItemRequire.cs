using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200093B RID: 2363
	public class NKCUIComItemRequire : MonoBehaviour
	{
		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x06005E7C RID: 24188 RVA: 0x001D4E74 File Offset: 0x001D3074
		// (set) Token: 0x06005E7D RID: 24189 RVA: 0x001D4E7C File Offset: 0x001D307C
		public int ItemID { get; private set; }

		// Token: 0x06005E7E RID: 24190 RVA: 0x001D4E88 File Offset: 0x001D3088
		public void Init()
		{
			if (this.m_Slot != null)
			{
				this.m_Slot.Init();
			}
			Text insufficientLabel = this.GetInsufficientLabel();
			if (insufficientLabel != null)
			{
				this.m_colTextOrigColor = insufficientLabel.color;
			}
		}

		// Token: 0x06005E7F RID: 24191 RVA: 0x001D4ECC File Offset: 0x001D30CC
		public void SetItem(int itemID, int RequireCount, bool bQuantityCheck = false)
		{
			this.ItemID = itemID;
			if (this.m_Slot != null)
			{
				if (-1 == itemID)
				{
					this.m_Slot.SetEmpty(null);
				}
				else
				{
					this.m_Slot.SetData(NKCUISlot.SlotData.MakeMiscItemData(itemID, 0L, 0), false, false, true, null);
					this.m_Slot.SetOpenItemBoxOnClick();
				}
			}
			if (this.m_lbItemSmallIcon != null)
			{
				this.m_lbItemSmallIcon.sprite = NKCResourceUtility.GetOrLoadMiscItemSmallIcon(itemID);
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			long num = 0L;
			if (nkmuserData != null)
			{
				num = nkmuserData.m_InventoryData.GetCountMiscItem(itemID);
			}
			NKCUtil.SetLabelText(this.m_lbHasCount, num.ToString());
			if (!bQuantityCheck)
			{
				if (RequireCount >= 0)
				{
					NKCUtil.SetLabelText(this.m_lbRequireCount, RequireCount.ToString());
					Text insufficientLabel = this.GetInsufficientLabel();
					if (insufficientLabel != null)
					{
						if (num < (long)RequireCount)
						{
							insufficientLabel.color = Color.red;
							return;
						}
						insufficientLabel.color = this.m_colTextOrigColor;
						return;
					}
				}
				else
				{
					NKCUtil.SetLabelText(this.m_lbRequireCount, "0");
					Text insufficientLabel2 = this.GetInsufficientLabel();
					if (insufficientLabel2 != null)
					{
						insufficientLabel2.color = this.m_colTextOrigColor;
					}
				}
				return;
			}
			if ((long)RequireCount <= num)
			{
				NKCUtil.SetLabelText(this.m_lbRequireCount, string.Format("<color=#ffffff>{0}/{1}</color>", RequireCount, num));
				return;
			}
			NKCUtil.SetLabelText(this.m_lbRequireCount, string.Format("<color=#ffffff>{0}/</color><color=#ff0000ff>{1}</color>", RequireCount, num));
		}

		// Token: 0x06005E80 RID: 24192 RVA: 0x001D502C File Offset: 0x001D322C
		private Text GetInsufficientLabel()
		{
			NKCUIComItemRequire.InsufficientMarking eInsufficientMarking = this.m_eInsufficientMarking;
			if (eInsufficientMarking != NKCUIComItemRequire.InsufficientMarking.REQUIRE_COUNT)
			{
				return this.m_lbHasCount;
			}
			return this.m_lbRequireCount;
		}

		// Token: 0x04004A92 RID: 19090
		public NKCUISlot m_Slot;

		// Token: 0x04004A93 RID: 19091
		public Image m_lbItemSmallIcon;

		// Token: 0x04004A94 RID: 19092
		public Text m_lbRequireCount;

		// Token: 0x04004A95 RID: 19093
		public Text m_lbHasCount;

		// Token: 0x04004A96 RID: 19094
		[Header("부족할때 어떤 값이 빨간색으로 표시되나요?")]
		public NKCUIComItemRequire.InsufficientMarking m_eInsufficientMarking = NKCUIComItemRequire.InsufficientMarking.MY_COUNT;

		// Token: 0x04004A98 RID: 19096
		private Color m_colTextOrigColor;

		// Token: 0x020015C9 RID: 5577
		public enum InsufficientMarking
		{
			// Token: 0x0400A283 RID: 41603
			REQUIRE_COUNT,
			// Token: 0x0400A284 RID: 41604
			MY_COUNT
		}
	}
}

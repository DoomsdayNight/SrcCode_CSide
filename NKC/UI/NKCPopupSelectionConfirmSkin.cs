using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A83 RID: 2691
	public class NKCPopupSelectionConfirmSkin : MonoBehaviour
	{
		// Token: 0x06007712 RID: 30482 RVA: 0x00279B23 File Offset: 0x00277D23
		public void Init()
		{
			NKCUISlot iconSlot = this.m_iconSlot;
			if (iconSlot == null)
			{
				return;
			}
			iconSlot.Init();
		}

		// Token: 0x06007713 RID: 30483 RVA: 0x00279B38 File Offset: 0x00277D38
		public void SetData(int skinId, int selectMiscItemId)
		{
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinId);
			if (skinTemplet != null)
			{
				NKCUtil.SetLabelText(this.m_lbName, skinTemplet.GetTitle());
				NKCUtil.SetLabelText(this.m_lbDesc, skinTemplet.GetSkinDesc());
			}
			NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeSkinData(skinId);
			NKCUISlot iconSlot = this.m_iconSlot;
			if (iconSlot != null)
			{
				iconSlot.SetData(data, true, null);
			}
			NKCUtil.SetLabelText(this.m_lbOwn, NKCStringTable.GetString("SI_DP_POPUP_USE_COUNT_TEXT_MISC_HAVE", false));
			NKCUtil.SetLabelText(this.m_lbSelectItemOwnAmount, NKCStringTable.GetString("SI_DP_POPUP_USE_COUNT_HAVE_TEXT_MISC_CHOICE", false));
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			int num = 0;
			long num2 = 0L;
			if (nkmuserData != null)
			{
				num = (nkmuserData.m_InventoryData.HasItemSkin(skinId) ? 1 : 0);
				num2 = nkmuserData.m_InventoryData.GetCountMiscItem(selectMiscItemId);
			}
			NKCUtil.SetLabelText(this.m_lbOwnCount, num.ToString());
			NKCUtil.SetLabelText(this.m_lbSelectItemCount, num2.ToString());
		}

		// Token: 0x06007714 RID: 30484 RVA: 0x00279C0C File Offset: 0x00277E0C
		private string GetUnitName(int unitId)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitId);
			string result = "";
			if (unitTempletBase != null)
			{
				if (unitTempletBase.m_bAwaken)
				{
					result = NKCStringTable.GetString("SI_PF_SHOP_SKIN_AWAKEN", new object[]
					{
						unitTempletBase.GetUnitName()
					});
				}
				else
				{
					result = unitTempletBase.GetUnitName();
				}
			}
			return result;
		}

		// Token: 0x04006394 RID: 25492
		public NKCUISlot m_iconSlot;

		// Token: 0x04006395 RID: 25493
		public Text m_lbName;

		// Token: 0x04006396 RID: 25494
		public Text m_lbDesc;

		// Token: 0x04006397 RID: 25495
		public Text m_lbOwn;

		// Token: 0x04006398 RID: 25496
		public Text m_lbOwnCount;

		// Token: 0x04006399 RID: 25497
		public Text m_lbSelectItemOwnAmount;

		// Token: 0x0400639A RID: 25498
		public Text m_lbSelectItemCount;
	}
}

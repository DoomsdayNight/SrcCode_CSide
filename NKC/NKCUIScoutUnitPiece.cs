using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000AA2 RID: 2722
	public class NKCUIScoutUnitPiece : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x060078A9 RID: 30889 RVA: 0x00280F5C File Offset: 0x0027F15C
		public void SetData(NKMPieceTemplet templet)
		{
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData == null)
			{
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			long num = (long)(nkmuserData.m_ArmyData.IsCollectedUnit(templet.m_PieceGetUintId) ? templet.m_PieceReq : templet.m_PieceReqFirst);
			long countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(templet.m_PieceId);
			NKMItemMiscTemplet itemMiscTemplet = (templet == null) ? null : NKMItemManager.GetItemMiscTempletByID(templet.m_PieceId);
			NKCUtil.SetImageSprite(this.m_imgUnit, NKCResourceUtility.GetOrLoadMiscItemIcon(itemMiscTemplet), false);
			if (countMiscItem < num)
			{
				NKCUtil.SetLabelText(this.m_lbCount, string.Format("<color=#ff0000>{0}</color>/{1}", countMiscItem, num));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbCount, string.Format("{0}/{1}", countMiscItem, num));
			}
			if (this.m_slCount != null)
			{
				this.m_slCount.minValue = 0f;
				this.m_slCount.maxValue = (float)num;
				if (countMiscItem >= num)
				{
					this.m_slCount.normalizedValue = 1f;
				}
				else
				{
					this.m_slCount.value = (float)countMiscItem;
				}
			}
			this.m_miscItemID = templet.m_PieceId;
		}

		// Token: 0x060078AA RID: 30890 RVA: 0x0028107C File Offset: 0x0027F27C
		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.m_miscItemID != 0)
			{
				NKCUISlot.SlotData data = NKCUISlot.SlotData.MakeMiscItemData(this.m_miscItemID, 1L, 0);
				NKCPopupItemBox.Instance.Open(NKCPopupItemBox.eMode.Normal, data, null, false, false, true);
			}
		}

		// Token: 0x04006532 RID: 25906
		public Image m_imgUnit;

		// Token: 0x04006533 RID: 25907
		public Text m_lbCount;

		// Token: 0x04006534 RID: 25908
		public Slider m_slCount;

		// Token: 0x04006535 RID: 25909
		private int m_miscItemID;
	}
}

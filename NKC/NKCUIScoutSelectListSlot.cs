using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000AA1 RID: 2721
	[RequireComponent(typeof(NKCUIUnitSelectListSlot))]
	public class NKCUIScoutSelectListSlot : MonoBehaviour
	{
		// Token: 0x060078A6 RID: 30886 RVA: 0x00280D42 File Offset: 0x0027EF42
		public void Init()
		{
			this.m_UnitSlot = base.GetComponent<NKCUIUnitSelectListSlot>();
			this.m_UnitSlot.Init(false);
		}

		// Token: 0x060078A7 RID: 30887 RVA: 0x00280D5C File Offset: 0x0027EF5C
		public void SetData(NKMPieceTemplet templet, NKMUnitData fakeUnitData, bool bSelected, NKCUIUnitSelectListSlotBase.OnSelectThisSlot onSelectThisSlot)
		{
			if (templet == null)
			{
				this.m_UnitSlot.SetEmpty(true, onSelectThisSlot, null);
			}
			else
			{
				if (fakeUnitData == null)
				{
					NKMUnitTempletBase templetBase = NKMUnitTempletBase.Find(templet.m_PieceGetUintId);
					NKCUIUnitSelectListSlot unitSlot = this.m_UnitSlot;
					if (unitSlot != null)
					{
						unitSlot.SetData(templetBase, 1, true, onSelectThisSlot);
					}
				}
				else
				{
					NKCUIUnitSelectListSlot unitSlot2 = this.m_UnitSlot;
					if (unitSlot2 != null)
					{
						unitSlot2.SetDataForCollection(fakeUnitData, NKMDeckIndex.None, onSelectThisSlot, true);
					}
				}
				NKCUtil.SetGameobjectActive(this.m_UnitSlot.m_lbName, true);
				NKCUtil.SetGameobjectActive(this.m_UnitSlot.m_imgUnitRole, true);
				NKCUtil.SetGameobjectActive(this.m_UnitSlot.m_comStarRank, false);
				NKCUtil.SetGameobjectActive(this.m_UnitSlot.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_STAR_GRADE, false);
				NKCUtil.SetGameobjectActive(this.m_UnitSlot.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_COST, true);
				NKCUtil.SetGameobjectActive(this.m_UnitSlot.m_NKM_UI_UNIT_SELECT_LIST_UNIT_SLOT_BOTTOM, false);
			}
			NKCUtil.SetGameobjectActive(this.m_objSelected, bSelected);
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			bool flag = nkmuserData.m_ArmyData.IsCollectedUnit(templet.m_PieceGetUintId);
			long num = (long)(flag ? templet.m_PieceReq : templet.m_PieceReqFirst);
			long countMiscItem = nkmuserData.m_InventoryData.GetCountMiscItem(templet.m_PieceId);
			if (countMiscItem < num)
			{
				NKCUtil.SetLabelText(this.m_lbPieceCount, string.Format("<color=#ff0000>{0}</color>/{1}", countMiscItem, num));
			}
			else
			{
				NKCUtil.SetLabelText(this.m_lbPieceCount, string.Format("{0}/{1}", countMiscItem, num));
			}
			NKCUtil.SetGameobjectActive(this.m_objReddot, NKCUIScout.IsReddotNeeded(nkmuserData, templet.Key));
			bool flag2 = countMiscItem >= num;
			NKCUtil.SetGameobjectActive(this.m_objCanExchangeBarBG, flag2);
			NKCUtil.SetGameobjectActive(this.m_objNoExchangeBarBG, !flag2);
			NKCUtil.SetGameobjectActive(this.m_objUnitNotOwned, !flag);
			if (this.m_slPieceCountBar != null)
			{
				this.m_slPieceCountBar.minValue = 0f;
				this.m_slPieceCountBar.maxValue = (float)num;
				if (flag2)
				{
					this.m_slPieceCountBar.normalizedValue = 1f;
					return;
				}
				this.m_slPieceCountBar.value = (float)countMiscItem;
			}
		}

		// Token: 0x0400652A RID: 25898
		private NKCUIUnitSelectListSlot m_UnitSlot;

		// Token: 0x0400652B RID: 25899
		public GameObject m_objReddot;

		// Token: 0x0400652C RID: 25900
		public GameObject m_objUnitNotOwned;

		// Token: 0x0400652D RID: 25901
		public GameObject m_objSelected;

		// Token: 0x0400652E RID: 25902
		public Text m_lbPieceCount;

		// Token: 0x0400652F RID: 25903
		public Slider m_slPieceCountBar;

		// Token: 0x04006530 RID: 25904
		public GameObject m_objCanExchangeBarBG;

		// Token: 0x04006531 RID: 25905
		public GameObject m_objNoExchangeBarBG;
	}
}

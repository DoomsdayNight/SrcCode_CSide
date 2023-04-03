using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Contract
{
	// Token: 0x02000BF2 RID: 3058
	public class NKCUIContractPopupRateSlot : MonoBehaviour
	{
		// Token: 0x06008E07 RID: 36359 RVA: 0x003064CC File Offset: 0x003046CC
		public void SetData(ContractUnitSlotData unitData, bool bPickup = false)
		{
			if (unitData == null)
			{
				return;
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_RANK, this.GetGradleText(unitData.grade));
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_RANK_IMG_SSR, unitData.grade == NKM_UNIT_GRADE.NUG_SSR);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_RANK_IMG_SR, unitData.grade == NKM_UNIT_GRADE.NUG_SR);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_RANK_IMG_R, unitData.grade == NKM_UNIT_GRADE.NUG_R);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_RANK_IMG_N, unitData.grade == NKM_UNIT_GRADE.NUG_N);
			string name = unitData.Name;
			NKMUnitTempletBase nkmunitTempletBase = NKMUnitTempletBase.Find(unitData.UnitID);
			if (unitData.type == NKM_UNIT_STYLE_TYPE.NUST_OPERATOR)
			{
				if (nkmunitTempletBase != null)
				{
					NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_NAME, string.Format("[{0}] {1}", nkmunitTempletBase.GetUnitTitle(), name));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_NAME, name);
				}
				NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_CLASS, NKCUtilString.GET_STRING_OPERATOR_CONTRACT_STYLE);
				NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_TYPE, "");
			}
			else
			{
				if (nkmunitTempletBase != null && (nkmunitTempletBase.m_bAwaken || nkmunitTempletBase.IsRearmUnit))
				{
					NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_NAME, string.Format("[{0}] {1}", nkmunitTempletBase.GetUnitTitle(), name));
				}
				else
				{
					NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_NAME, name);
				}
				NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_TYPE, NKCUtilString.GetUnitStyleType(unitData.type));
				NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_CLASS, NKCUtilString.GetRoleText(unitData.role, false));
			}
			NKCUtil.SetLabelText(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_PERCENT, unitData.Percent.ToString("N3") + "%");
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_FEATURED, bPickup);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_PERCENTUP2, !bPickup && unitData.RatioUp);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_UP, false);
			NKCUtil.SetGameobjectActive(this.m_objNoGet, !NKCScenManager.CurrentUserData().m_ArmyData.IsCollectedUnit(unitData.UnitID));
			NKCUtil.SetGameobjectActive(this.m_objAwaken, unitData.Awaken);
			NKCUtil.SetGameobjectActive(this.m_objRearmment, unitData.Rearm);
		}

		// Token: 0x06008E08 RID: 36360 RVA: 0x003066B6 File Offset: 0x003048B6
		private string GetGradleText(NKM_UNIT_GRADE grade)
		{
			switch (grade)
			{
			case NKM_UNIT_GRADE.NUG_N:
				return "N";
			case NKM_UNIT_GRADE.NUG_R:
				return "R";
			case NKM_UNIT_GRADE.NUG_SR:
				return "SR";
			case NKM_UNIT_GRADE.NUG_SSR:
				return "SSR";
			default:
				return "";
			}
		}

		// Token: 0x04007B0F RID: 31503
		public Text m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_RANK;

		// Token: 0x04007B10 RID: 31504
		public GameObject m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_UP;

		// Token: 0x04007B11 RID: 31505
		public Text m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_NAME;

		// Token: 0x04007B12 RID: 31506
		public Text m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_TYPE;

		// Token: 0x04007B13 RID: 31507
		public Text m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_CLASS;

		// Token: 0x04007B14 RID: 31508
		public Text m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_PERCENT;

		// Token: 0x04007B15 RID: 31509
		public GameObject m_objNoGet;

		// Token: 0x04007B16 RID: 31510
		public GameObject m_objAwaken;

		// Token: 0x04007B17 RID: 31511
		public GameObject m_objRearmment;

		// Token: 0x04007B18 RID: 31512
		[Header("픽업")]
		public GameObject m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_FEATURED;

		// Token: 0x04007B19 RID: 31513
		[Header("최대 2개")]
		public GameObject m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_PERCENTUP1;

		// Token: 0x04007B1A RID: 31514
		public GameObject m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_PERCENTUP2;

		// Token: 0x04007B1B RID: 31515
		[Header("RATE IMG")]
		public GameObject m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_RANK_IMG_SSR;

		// Token: 0x04007B1C RID: 31516
		public GameObject m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_RANK_IMG_SR;

		// Token: 0x04007B1D RID: 31517
		public GameObject m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_RANK_IMG_R;

		// Token: 0x04007B1E RID: 31518
		public GameObject m_NKM_UI_POPUP_CONTRACT_RATE_SLOT_RANK_IMG_N;
	}
}

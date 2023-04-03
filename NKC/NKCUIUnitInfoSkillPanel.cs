using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009F4 RID: 2548
	public class NKCUIUnitInfoSkillPanel : MonoBehaviour
	{
		// Token: 0x06006E5E RID: 28254 RVA: 0x0024412B File Offset: 0x0024232B
		public void SetOpenPopupWhenSelected()
		{
			this.m_bOpenPopupWhenSelected = true;
		}

		// Token: 0x06006E5F RID: 28255 RVA: 0x00244134 File Offset: 0x00242334
		public void Init()
		{
			foreach (NKCUISkillSlot nkcuiskillSlot in this.m_lstSkillSlot)
			{
				nkcuiskillSlot.Init(new NKCUISkillSlot.OnClickSkillSlot(this.SelectSkill));
			}
		}

		// Token: 0x06006E60 RID: 28256 RVA: 0x00244190 File Offset: 0x00242390
		public void OpenSkillInfoPopup()
		{
			NKCPopupSkillFullInfo.UnitInstance.OpenForUnit(this.m_CurUnitData, this.m_unitName, this.m_unitStarGradeMax, this.m_unitLimitBreakLevel, this.m_CurUnitTempletBase.IsRearmUnit);
		}

		// Token: 0x06006E61 RID: 28257 RVA: 0x002441C0 File Offset: 0x002423C0
		public void SetData(NKMUnitData unitData, bool bDisplayEmptySlot = false)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			this.m_CurUnitTempletBase = unitTempletBase;
			this.m_unitName = unitTempletBase.GetUnitName();
			this.m_unitStarGradeMax = unitTempletBase.m_StarGradeMax;
			this.m_unitLimitBreakLevel = (int)unitData.m_LimitBreakLevel;
			List<NKMUnitSkillTemplet> unitAllSkillTemplets = NKMUnitSkillManager.GetUnitAllSkillTemplets(unitData);
			bool bValue = false;
			int num = 1;
			for (int i = 0; i < this.m_lstSkillSlot.Count; i++)
			{
				if (unitAllSkillTemplets.Count > i)
				{
					bool value = NKMUnitSkillManager.IsLockedSkill(unitAllSkillTemplets[i].m_ID, (int)unitData.m_LimitBreakLevel);
					NKMUnitSkillTemplet nkmunitSkillTemplet = unitAllSkillTemplets[i];
					if (nkmunitSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_LEADER)
					{
						bValue = true;
						this.m_lstSkillSlot[0].SetData(nkmunitSkillTemplet, false);
						this.m_lstSkillSlot[0].LockSkill(value);
						NKCUtil.SetLabelText(this.m_lstSkillTypeName[0], NKCUtilString.GetSkillTypeName(nkmunitSkillTemplet.m_NKM_SKILL_TYPE));
					}
					else
					{
						this.m_lstSkillSlot[num].SetData(nkmunitSkillTemplet, nkmunitSkillTemplet.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_HYPER);
						this.m_lstSkillSlot[num].LockSkill(value);
						NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[num], true);
						NKCUtil.SetLabelText(this.m_lstSkillTypeName[num], NKCUtilString.GetSkillTypeName(nkmunitSkillTemplet.m_NKM_SKILL_TYPE));
						num++;
					}
				}
			}
			for (int j = num; j < this.m_lstSkillSlot.Count; j++)
			{
				NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[j], false);
			}
			NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[0], bValue);
			NKCUtil.SetGameobjectActive(this.m_lstSkillTypeName[0], bValue);
			this.m_CurUnitData = unitData;
		}

		// Token: 0x06006E62 RID: 28258 RVA: 0x0024436D File Offset: 0x0024256D
		public void SelectSkill(NKMUnitSkillTemplet skillTemplet)
		{
			if (skillTemplet != null && this.m_bOpenPopupWhenSelected)
			{
				this.OpenSkillInfoPopup();
			}
		}

		// Token: 0x040059AF RID: 22959
		[Header("왼쪽 버튼")]
		public List<Text> m_lstSkillTypeName;

		// Token: 0x040059B0 RID: 22960
		public List<NKCUISkillSlot> m_lstSkillSlot;

		// Token: 0x040059B1 RID: 22961
		public GameObject m_goHyperSkillEffect;

		// Token: 0x040059B2 RID: 22962
		private bool m_bOpenPopupWhenSelected;

		// Token: 0x040059B3 RID: 22963
		private string m_unitName;

		// Token: 0x040059B4 RID: 22964
		private int m_unitStarGradeMax;

		// Token: 0x040059B5 RID: 22965
		private int m_unitLimitBreakLevel;

		// Token: 0x040059B6 RID: 22966
		private NKMUnitTempletBase m_CurUnitTempletBase;

		// Token: 0x040059B7 RID: 22967
		private NKMUnitData m_CurUnitData;
	}
}

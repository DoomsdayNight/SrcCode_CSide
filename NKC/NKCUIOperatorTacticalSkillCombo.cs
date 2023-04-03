using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC
{
	// Token: 0x020007ED RID: 2029
	public class NKCUIOperatorTacticalSkillCombo : MonoBehaviour
	{
		// Token: 0x06005080 RID: 20608 RVA: 0x00185AB8 File Offset: 0x00183CB8
		public void SetData(NKMOperator operatorData)
		{
			this.SetData(operatorData.id);
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x00185AC8 File Offset: 0x00183CC8
		public void SetData(int operatorID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(operatorID);
			if (unitTempletBase == null)
			{
				return;
			}
			if (unitTempletBase.m_NKM_UNIT_TYPE != NKM_UNIT_TYPE.NUT_OPERATOR)
			{
				return;
			}
			NKMOperatorSkillTemplet skillTemplet = NKCOperatorUtil.GetSkillTemplet(unitTempletBase.m_lstSkillStrID[0]);
			if (skillTemplet == null)
			{
				return;
			}
			if (skillTemplet.m_OperSkillType == OperatorSkillType.m_Tactical)
			{
				NKMTacticalCommandTemplet tacticalCommandTempletByStrID = NKMTacticalCommandManager.GetTacticalCommandTempletByStrID(skillTemplet.m_OperSkillTarget);
				if (tacticalCommandTempletByStrID.m_listComboType != null && tacticalCommandTempletByStrID.m_listComboType.Count > 0)
				{
					List<NKMTacticalCombo> listComboType = tacticalCommandTempletByStrID.m_listComboType;
					for (int i = 0; i < this.m_lstComboSlot.Count; i++)
					{
						if (listComboType.Count <= i)
						{
							NKCUtil.SetGameobjectActive(this.m_lstComboSlot[i].gameObject, false);
						}
						else
						{
							NKCUtil.SetGameobjectActive(this.m_lstComboSlot[i].gameObject, true);
							this.m_lstComboSlot[i].SetUI(listComboType[i], i < listComboType.Count - 1);
						}
					}
				}
			}
		}

		// Token: 0x04004076 RID: 16502
		public List<NKCGameHudComboSlot> m_lstComboSlot;
	}
}

using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;

namespace NKC.UI
{
	// Token: 0x020009E5 RID: 2533
	public class NKCUIShipInfoSkillPanel : MonoBehaviour
	{
		// Token: 0x06006D02 RID: 27906 RVA: 0x0023B8F4 File Offset: 0x00239AF4
		public void Init(UnityAction callback = null)
		{
			this.dOnCallSkillInfo = callback;
			foreach (NKCUIShipSkillSlot nkcuishipSkillSlot in this.m_lstSkillSlot)
			{
				nkcuishipSkillSlot.Init(new UnityAction(this.OnSelectSkill), false);
			}
		}

		// Token: 0x06006D03 RID: 27907 RVA: 0x0023B958 File Offset: 0x00239B58
		public void SetData(NKMUnitTempletBase cNKMShipTemplet)
		{
			if (cNKMShipTemplet != null)
			{
				int num = 0;
				for (int i = 0; i < cNKMShipTemplet.GetSkillCount(); i++)
				{
					NKMShipSkillTemplet shipSkillTempletByIndex = NKMShipSkillManager.GetShipSkillTempletByIndex(cNKMShipTemplet, i);
					if (shipSkillTempletByIndex != null && num < this.m_lstSkillSlot.Count && shipSkillTempletByIndex.m_NKM_SKILL_TYPE == NKM_SKILL_TYPE.NST_PASSIVE)
					{
						NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[num], true);
						this.m_lstSkillSlot[num].SetData(shipSkillTempletByIndex, NKCUIShipSkillSlot.eShipSkillSlotStatus.NONE);
						num++;
					}
				}
				for (int j = 0; j < cNKMShipTemplet.GetSkillCount(); j++)
				{
					NKMShipSkillTemplet shipSkillTempletByIndex2 = NKMShipSkillManager.GetShipSkillTempletByIndex(cNKMShipTemplet, j);
					if (shipSkillTempletByIndex2 != null && num < this.m_lstSkillSlot.Count && shipSkillTempletByIndex2.m_NKM_SKILL_TYPE != NKM_SKILL_TYPE.NST_PASSIVE)
					{
						NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[num], true);
						this.m_lstSkillSlot[num].SetData(shipSkillTempletByIndex2, NKCUIShipSkillSlot.eShipSkillSlotStatus.NONE);
						num++;
					}
				}
				for (int k = num; k < this.m_lstSkillSlot.Count; k++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstSkillSlot[k], false);
				}
				return;
			}
			foreach (NKCUIShipSkillSlot targetMono in this.m_lstSkillSlot)
			{
				NKCUtil.SetGameobjectActive(targetMono, false);
			}
		}

		// Token: 0x06006D04 RID: 27908 RVA: 0x0023BA9C File Offset: 0x00239C9C
		public void OnSelectSkill()
		{
			UnityAction unityAction = this.dOnCallSkillInfo;
			if (unityAction == null)
			{
				return;
			}
			unityAction();
		}

		// Token: 0x040058DE RID: 22750
		public List<NKCUIShipSkillSlot> m_lstSkillSlot;

		// Token: 0x040058DF RID: 22751
		private UnityAction dOnCallSkillInfo;
	}
}

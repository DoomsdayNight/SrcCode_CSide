using System;
using System.Collections.Generic;

namespace NKC.UI.Tooltip
{
	// Token: 0x02000B0F RID: 2831
	public class NKCUITooltipOperatorSkillCombo : NKCUITooltipBase
	{
		// Token: 0x0600806C RID: 32876 RVA: 0x002B4D2D File Offset: 0x002B2F2D
		public override void Init()
		{
		}

		// Token: 0x0600806D RID: 32877 RVA: 0x002B4D30 File Offset: 0x002B2F30
		public override void SetData(NKCUITooltip.Data data)
		{
			NKCUITooltip.OperatorSkillComboData operatorSkillComboData = data as NKCUITooltip.OperatorSkillComboData;
			if (operatorSkillComboData != null)
			{
				for (int i = 0; i < this.m_lstComboSlot.Count; i++)
				{
					if (operatorSkillComboData.skillCombo.Count <= i)
					{
						NKCUtil.SetGameobjectActive(this.m_lstComboSlot[i].gameObject, false);
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstComboSlot[i].gameObject, true);
						this.m_lstComboSlot[i].SetUI(operatorSkillComboData.skillCombo[i], i < operatorSkillComboData.skillCombo.Count - 1);
					}
				}
			}
		}

		// Token: 0x04006CB0 RID: 27824
		public List<NKCGameHudComboSlot> m_lstComboSlot;
	}
}

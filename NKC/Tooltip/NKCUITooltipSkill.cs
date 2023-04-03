using System;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Tooltip
{
	// Token: 0x02000B08 RID: 2824
	public class NKCUITooltipSkill : NKCUITooltipBase
	{
		// Token: 0x06008058 RID: 32856 RVA: 0x002B4895 File Offset: 0x002B2A95
		public override void Init()
		{
			this.m_slot.Init(null, false);
		}

		// Token: 0x06008059 RID: 32857 RVA: 0x002B48A4 File Offset: 0x002B2AA4
		public override void SetData(NKCUITooltip.Data data)
		{
			NKCUITooltip.ShipSkillData shipSkillData = data as NKCUITooltip.ShipSkillData;
			if (shipSkillData == null)
			{
				Debug.LogError("Tooltip ShipSkillData is null");
				return;
			}
			NKMShipSkillTemplet shipSkillTemplet = shipSkillData.ShipSkillTemplet;
			this.m_slot.SetData(shipSkillTemplet, NKCUIShipSkillSlot.eShipSkillSlotStatus.NONE);
			this.m_type.text = NKCUtilString.GetSkillTypeName(shipSkillTemplet.m_NKM_SKILL_TYPE);
			this.m_name.text = shipSkillTemplet.GetName();
		}

		// Token: 0x04006C96 RID: 27798
		public NKCUIShipSkillSlot m_slot;

		// Token: 0x04006C97 RID: 27799
		public Text m_type;

		// Token: 0x04006C98 RID: 27800
		public Text m_name;
	}
}

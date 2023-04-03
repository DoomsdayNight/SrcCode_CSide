using System;
using BehaviorDesigner.Runtime.Tasks;
using NKC.Office;

namespace NKC.BT.Office
{
	// Token: 0x02000817 RID: 2071
	public abstract class BTOfficeConditionBase : Conditional
	{
		// Token: 0x060051EC RID: 20972 RVA: 0x0018DFC2 File Offset: 0x0018C1C2
		public override void OnAwake()
		{
			this.m_Character = base.GetComponent<NKCOfficeCharacter>();
			NKCOfficeCharacter character = this.m_Character;
			this.m_OfficeBuilding = ((character != null) ? character.OfficeBuilding : null);
		}

		// Token: 0x04004226 RID: 16934
		protected NKCOfficeCharacter m_Character;

		// Token: 0x04004227 RID: 16935
		protected NKCOfficeBuildingBase m_OfficeBuilding;
	}
}

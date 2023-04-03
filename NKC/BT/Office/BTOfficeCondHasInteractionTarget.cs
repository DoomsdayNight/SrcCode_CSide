using System;
using BehaviorDesigner.Runtime.Tasks;

namespace NKC.BT.Office
{
	// Token: 0x02000815 RID: 2069
	public class BTOfficeCondHasInteractionTarget : BTOfficeConditionBase
	{
		// Token: 0x060051E6 RID: 20966 RVA: 0x0018DD87 File Offset: 0x0018BF87
		public override TaskStatus OnUpdate()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				return TaskStatus.Failure;
			}
			if (this.m_Character.HasInteractionTarget())
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}
	}
}

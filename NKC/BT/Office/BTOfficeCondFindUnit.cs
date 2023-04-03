using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace NKC.BT.Office
{
	// Token: 0x02000814 RID: 2068
	public class BTOfficeCondFindUnit : BTOfficeConditionBase
	{
		// Token: 0x060051E3 RID: 20963 RVA: 0x0018DCD8 File Offset: 0x0018BED8
		public override TaskStatus OnUpdate()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				return TaskStatus.Failure;
			}
			if (this.IsTargetInRoom())
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x060051E4 RID: 20964 RVA: 0x0018DD04 File Offset: 0x0018BF04
		private bool IsTargetInRoom()
		{
			Vector3 position = this.m_Character.transform.position;
			float num = this.m_fRange * this.m_fRange;
			if (this.FollowTarget.Value == null)
			{
				return false;
			}
			if (this.m_fRange < 0f)
			{
				return true;
			}
			Vector3 position2 = this.FollowTarget.Value.transform.position;
			return (position - position2).sqrMagnitude <= num;
		}

		// Token: 0x04004223 RID: 16931
		[Header("목표 유닛 (UnitSelector에서 지정됨)")]
		public BTSharedOfficeCharacter FollowTarget;

		// Token: 0x04004224 RID: 16932
		[Header("거리. 음수인 경우 유닛이 있기만 하면 True")]
		public float m_fRange;
	}
}

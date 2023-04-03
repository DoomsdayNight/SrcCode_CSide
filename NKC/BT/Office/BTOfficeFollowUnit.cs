using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace NKC.BT.Office
{
	// Token: 0x02000819 RID: 2073
	public class BTOfficeFollowUnit : BTOfficeActionBase
	{
		// Token: 0x060051F0 RID: 20976 RVA: 0x0018E09C File Offset: 0x0018C29C
		public override void OnStart()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			this.m_fTargetRangeSquared = this.FollowTargetRange * this.FollowTargetRange;
			if (this.FollowTarget.Value == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			this.bActionSuccessFlag = true;
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x0018E104 File Offset: 0x0018C304
		public override TaskStatus OnUpdate()
		{
			if (!this.bActionSuccessFlag)
			{
				return TaskStatus.Failure;
			}
			if (this.FollowTarget.Value == null)
			{
				return TaskStatus.Failure;
			}
			Vector3 vector = this.FollowTarget.Value.transform.position;
			vector = this.m_OfficeBuilding.trActorRoot.InverseTransformPoint(vector);
			if (!this.m_bFollowing)
			{
				this.m_bFollowing = true;
				if (this.m_Character.PlayAnimCompleted())
				{
					this.m_vMoveTargetPosition = vector;
					if (!base.Move(vector, false, this.MoveSpeed.Value, this.MoveAnimName.Value))
					{
						return TaskStatus.Failure;
					}
				}
				return TaskStatus.Running;
			}
			Vector3 vector2 = this.m_Character.transform.position;
			vector2 = this.m_OfficeBuilding.trActorRoot.InverseTransformPoint(vector2);
			if ((vector2 - vector).sqrMagnitude <= this.m_fTargetRangeSquared)
			{
				this.m_Character.StopAllAnimInstances();
				this.m_bFollowing = false;
				return TaskStatus.Success;
			}
			if (this.m_Character.PlayAnimCompleted())
			{
				this.m_vMoveTargetPosition = vector;
				if (!base.Move(vector, false, this.MoveSpeed.Value, this.MoveAnimName.Value))
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Running;
			}
			else
			{
				if ((vector - this.m_vMoveTargetPosition).sqrMagnitude <= this.m_fTargetRangeSquared)
				{
					return TaskStatus.Running;
				}
				this.m_Character.StopAllAnimInstances();
				this.m_vMoveTargetPosition = vector;
				if (!base.Move(vector, false, this.MoveSpeed.Value, this.MoveAnimName.Value))
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Running;
			}
		}

		// Token: 0x0400422C RID: 16940
		[Header("목표 유닛. TargetUnitSelector에서 지정됨")]
		public BTSharedOfficeCharacter FollowTarget;

		// Token: 0x0400422D RID: 16941
		[Header("이 거리 안까지 도달하면 완료")]
		public float FollowTargetRange = 200f;

		// Token: 0x0400422E RID: 16942
		private float m_fTargetRangeSquared;

		// Token: 0x0400422F RID: 16943
		private bool m_bFollowing;

		// Token: 0x04004230 RID: 16944
		private Vector3 m_vMoveTargetPosition;

		// Token: 0x04004231 RID: 16945
		[Header("따라갈때 사용할 애니")]
		public SharedString MoveAnimName;

		// Token: 0x04004232 RID: 16946
		[Header("따라갈때 이동속도")]
		public SharedFloat MoveSpeed = 450f;
	}
}

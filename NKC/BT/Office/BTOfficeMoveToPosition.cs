using System;
using BehaviorDesigner.Runtime.Tasks;
using Cs.Math;
using UnityEngine;

namespace NKC.BT.Office
{
	// Token: 0x02000820 RID: 2080
	public class BTOfficeMoveToPosition : BTOfficeActionBase
	{
		// Token: 0x06005204 RID: 20996 RVA: 0x0018EA8A File Offset: 0x0018CC8A
		public override void OnStart()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			this.bActionSuccessFlag = true;
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x0018EAB8 File Offset: 0x0018CCB8
		public override TaskStatus OnUpdate()
		{
			if (!this.bActionSuccessFlag)
			{
				return TaskStatus.Failure;
			}
			if (this.m_Character.PlayAnimCompleted())
			{
				if (this.transform.localPosition.x.IsNearlyEqual(this.TargetLocalPos.x, 1E-05f) && this.transform.localPosition.y.IsNearlyEqual(this.TargetLocalPos.y, 1E-05f) && this.transform.localPosition.z.IsNearlyEqual(this.TargetLocalPos.z, 1E-05f))
				{
					return TaskStatus.Success;
				}
				if (string.IsNullOrEmpty(this.AnimEventName))
				{
					this.m_Character.EnqueueAnimation(base.GetWalkInstance(this.transform.localPosition, this.TargetLocalPos, this.moveSpeed, this.AnimName));
				}
				else
				{
					this.m_Character.EnqueueAnimation(new NKCAnimationInstance(this.m_Character, this.m_OfficeBuilding.transform, NKCAnimationEventManager.Find(this.AnimEventName), this.transform.localPosition, this.TargetLocalPos));
				}
			}
			return TaskStatus.Running;
		}

		// Token: 0x0400423C RID: 16956
		public Vector3 TargetLocalPos;

		// Token: 0x0400423D RID: 16957
		public float moveSpeed = 200f;

		// Token: 0x0400423E RID: 16958
		public string AnimName;

		// Token: 0x0400423F RID: 16959
		[Header("이동시 사용할 이벤트. AnimEventName 있는 경우 AnimName, moveSpeed는 무시")]
		public string AnimEventName;
	}
}

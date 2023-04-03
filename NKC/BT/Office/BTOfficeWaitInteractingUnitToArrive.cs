using System;
using BehaviorDesigner.Runtime.Tasks;
using Cs.Math;
using UnityEngine;

namespace NKC.BT.Office
{
	// Token: 0x02000824 RID: 2084
	public class BTOfficeWaitInteractingUnitToArrive : BTOfficeActionBase
	{
		// Token: 0x06005211 RID: 21009 RVA: 0x0018EFC4 File Offset: 0x0018D1C4
		public override void OnStart()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			this.bActionSuccessFlag = true;
			if (this.m_Character.CurrentUnitInteractionTemplet != null && !this.m_Character.CurrentUnitInteractionTemplet.IsSoloAction && this.m_Character.CurrentUnitInteractionTarget != null)
			{
				Vector3 vector = this.m_Character.transform.parent.TransformPoint(this.m_Character.CurrentUnitInteractionTarget.CurrentUnitInteractionPosition);
				bool bLeft = this.m_Character.transform.position.x >= vector.x;
				this.m_Character.EnqueueAnimation(base.GetInvertDirectionInstance(bLeft));
				float animTime = this.m_Character.GetAnimTime("SD_IDLE");
				NKCAnimationInstance idleInstance = base.GetIdleInstance(animTime, this.m_Character.transform.localPosition, "");
				this.m_Character.EnqueueAnimation(idleInstance);
				this.bWaitRequired = true;
				return;
			}
			this.bWaitRequired = false;
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x0018F0DC File Offset: 0x0018D2DC
		public override TaskStatus OnUpdate()
		{
			if (!this.bActionSuccessFlag)
			{
				return TaskStatus.Failure;
			}
			if (!this.bWaitRequired)
			{
				return TaskStatus.Success;
			}
			if (this.m_Character.CurrentUnitInteractionTarget == null || !this.m_Character.CurrentUnitInteractionTarget.HasInteractionTarget())
			{
				this.m_Character.UnregisterInteraction();
				return TaskStatus.Failure;
			}
			Vector3 localPosition = this.m_Character.CurrentUnitInteractionTarget.transform.localPosition;
			Vector3 currentUnitInteractionPosition = this.m_Character.CurrentUnitInteractionTarget.CurrentUnitInteractionPosition;
			if (localPosition.x.IsNearlyEqual(currentUnitInteractionPosition.x, 0.0001f) && localPosition.y.IsNearlyEqual(currentUnitInteractionPosition.y, 0.0001f) && localPosition.z.IsNearlyEqual(currentUnitInteractionPosition.z, 0.0001f))
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x0018F1A1 File Offset: 0x0018D3A1
		public override void OnEnd()
		{
			this.m_Character.StopAllAnimInstances();
		}

		// Token: 0x04004247 RID: 16967
		private bool bWaitRequired;
	}
}

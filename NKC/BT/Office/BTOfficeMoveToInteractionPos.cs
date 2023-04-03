using System;
using BehaviorDesigner.Runtime.Tasks;
using Cs.Math;
using UnityEngine;

namespace NKC.BT.Office
{
	// Token: 0x0200081F RID: 2079
	public class BTOfficeMoveToInteractionPos : BTOfficeActionBase
	{
		// Token: 0x06005200 RID: 20992 RVA: 0x0018E914 File Offset: 0x0018CB14
		public override void OnStart()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			if (!this.m_Character.HasInteractionTarget())
			{
				this.bActionSuccessFlag = false;
				return;
			}
			this.TargetLocalPos = this.m_Character.GetInteractionPosition();
			this.bActionSuccessFlag = true;
			Vector3 localPosition = this.m_Character.transform.localPosition;
			if (!localPosition.x.IsNearlyEqual(this.TargetLocalPos.x, 0.0001f) || !localPosition.y.IsNearlyEqual(this.TargetLocalPos.y, 0.0001f) || !localPosition.z.IsNearlyEqual(this.TargetLocalPos.z, 0.0001f))
			{
				if (this.m_bRun)
				{
					this.m_Character.EnqueueAnimation(base.GetRunInstance(this.transform.localPosition, this.TargetLocalPos, 600f, ""));
					return;
				}
				base.Move(this.TargetLocalPos, false, 150f, "");
			}
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x0018EA27 File Offset: 0x0018CC27
		public override TaskStatus OnUpdate()
		{
			if (!this.bActionSuccessFlag)
			{
				return TaskStatus.Failure;
			}
			if (this.m_Character.PlayAnimCompleted())
			{
				this.m_Character.transform.localPosition = this.TargetLocalPos;
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x0018EA59 File Offset: 0x0018CC59
		public override void OnEnd()
		{
			base.OnEnd();
			if (!this.bActionSuccessFlag && this.m_Character != null)
			{
				this.m_Character.UnregisterInteraction();
			}
		}

		// Token: 0x0400423A RID: 16954
		public bool m_bRun;

		// Token: 0x0400423B RID: 16955
		private Vector3 TargetLocalPos;
	}
}

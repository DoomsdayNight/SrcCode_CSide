using System;
using BehaviorDesigner.Runtime.Tasks;

namespace NKC.BT.Office
{
	// Token: 0x02000825 RID: 2085
	public class BTOfficeWaitInteractingUnitToFinish : BTOfficeActionBase
	{
		// Token: 0x06005215 RID: 21013 RVA: 0x0018F1B8 File Offset: 0x0018D3B8
		public override void OnStart()
		{
			if (this.m_Character == null || this.m_OfficeBuilding == null)
			{
				this.bActionSuccessFlag = false;
				return;
			}
			this.bActionSuccessFlag = true;
			if (this.m_Character.CurrentUnitInteractionTarget != null && this.m_Character.CurrentUnitInteractionTarget.PlayingInteractionAnimation)
			{
				float animTime = this.m_Character.GetAnimTime("SD_IDLE");
				NKCAnimationInstance idleInstance = base.GetIdleInstance(animTime, this.m_Character.transform.localPosition, "");
				this.m_Character.EnqueueAnimation(idleInstance);
				this.bWaitRequired = true;
				return;
			}
			this.bWaitRequired = false;
		}

		// Token: 0x06005216 RID: 21014 RVA: 0x0018F260 File Offset: 0x0018D460
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
				return TaskStatus.Success;
			}
			if (!this.m_Character.CurrentUnitInteractionTarget.PlayingInteractionAnimation)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06005217 RID: 21015 RVA: 0x0018F2BD File Offset: 0x0018D4BD
		public override void OnEnd()
		{
			this.m_Character.StopAllAnimInstances();
			this.m_Character.UnregisterInteraction();
		}

		// Token: 0x04004248 RID: 16968
		private bool bWaitRequired;
	}
}

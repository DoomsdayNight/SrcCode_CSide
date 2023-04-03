using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000DD RID: 221
	[TaskDescription("Waits the specified duration after the child has completed before returning the child's status of success or failure.")]
	[TaskIcon("{SkinColor}CooldownIcon.png")]
	public class Cooldown : Decorator
	{
		// Token: 0x06000725 RID: 1829 RVA: 0x0001995A File Offset: 0x00017B5A
		public override bool CanExecute()
		{
			return this.cooldownTime == -1f || this.cooldownTime + this.duration.Value > Time.time;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00019984 File Offset: 0x00017B84
		public override int CurrentChildIndex()
		{
			if (this.cooldownTime == -1f)
			{
				return 0;
			}
			return -1;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00019996 File Offset: 0x00017B96
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
			if (this.executionStatus == TaskStatus.Failure || this.executionStatus == TaskStatus.Success)
			{
				this.cooldownTime = Time.time;
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000199BC File Offset: 0x00017BBC
		public override TaskStatus OverrideStatus()
		{
			if (!this.CanExecute())
			{
				return TaskStatus.Running;
			}
			return this.executionStatus;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000199CE File Offset: 0x00017BCE
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			if (status == TaskStatus.Running)
			{
				return status;
			}
			return this.executionStatus;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x000199DC File Offset: 0x00017BDC
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.cooldownTime = -1f;
		}

		// Token: 0x0400035B RID: 859
		public SharedFloat duration = 2f;

		// Token: 0x0400035C RID: 860
		private TaskStatus executionStatus;

		// Token: 0x0400035D RID: 861
		private float cooldownTime = -1f;
	}
}

using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E0 RID: 224
	[TaskDescription("The repeater task will repeat execution of its child task until the child task has been run a specified number of times. It has the option of continuing to execute the child task even if the child task returns a failure.")]
	[TaskIcon("{SkinColor}RepeaterIcon.png")]
	public class Repeater : Decorator
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x00019AB4 File Offset: 0x00017CB4
		public override bool CanExecute()
		{
			return (this.repeatForever.Value || this.executionCount < this.count.Value) && (!this.endOnFailure.Value || (this.endOnFailure.Value && this.executionStatus != TaskStatus.Failure));
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00019B0D File Offset: 0x00017D0D
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionCount++;
			this.executionStatus = childStatus;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00019B24 File Offset: 0x00017D24
		public override void OnEnd()
		{
			this.executionCount = 0;
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00019B34 File Offset: 0x00017D34
		public override void OnReset()
		{
			this.count = 0;
			this.endOnFailure = true;
		}

		// Token: 0x04000361 RID: 865
		[Tooltip("The number of times to repeat the execution of its child task")]
		public SharedInt count = 1;

		// Token: 0x04000362 RID: 866
		[Tooltip("Allows the repeater to repeat forever")]
		public SharedBool repeatForever;

		// Token: 0x04000363 RID: 867
		[Tooltip("Should the task return if the child task returns a failure")]
		public SharedBool endOnFailure;

		// Token: 0x04000364 RID: 868
		private int executionCount;

		// Token: 0x04000365 RID: 869
		private TaskStatus executionStatus;
	}
}

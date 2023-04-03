using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E5 RID: 229
	[TaskDescription("The until success task will keep executing its child task until the child task returns success.")]
	[TaskIcon("{SkinColor}UntilSuccessIcon.png")]
	public class UntilSuccess : Decorator
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x00019D07 File Offset: 0x00017F07
		public override bool CanExecute()
		{
			return this.executionStatus == TaskStatus.Failure || this.executionStatus == TaskStatus.Inactive;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00019D1D File Offset: 0x00017F1D
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00019D26 File Offset: 0x00017F26
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x0400036E RID: 878
		private TaskStatus executionStatus;
	}
}

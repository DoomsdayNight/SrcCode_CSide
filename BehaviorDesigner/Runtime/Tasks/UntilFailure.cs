using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E4 RID: 228
	[TaskDescription("The until failure task will keep executing its child task until the child task returns failure.")]
	[TaskIcon("{SkinColor}UntilFailureIcon.png")]
	public class UntilFailure : Decorator
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x00019CD7 File Offset: 0x00017ED7
		public override bool CanExecute()
		{
			return this.executionStatus == TaskStatus.Success || this.executionStatus == TaskStatus.Inactive;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00019CED File Offset: 0x00017EED
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00019CF6 File Offset: 0x00017EF6
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x0400036D RID: 877
		private TaskStatus executionStatus;
	}
}

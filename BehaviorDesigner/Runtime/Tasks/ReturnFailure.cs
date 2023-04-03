using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E1 RID: 225
	[TaskDescription("The return failure task will always return failure except when the child task is running.")]
	[TaskIcon("{SkinColor}ReturnFailureIcon.png")]
	public class ReturnFailure : Decorator
	{
		// Token: 0x0600073C RID: 1852 RVA: 0x00019B62 File Offset: 0x00017D62
		public override bool CanExecute()
		{
			return this.executionStatus == TaskStatus.Inactive || this.executionStatus == TaskStatus.Running;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00019B77 File Offset: 0x00017D77
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00019B80 File Offset: 0x00017D80
		public override TaskStatus Decorate(TaskStatus status)
		{
			if (status == TaskStatus.Success)
			{
				return TaskStatus.Failure;
			}
			return status;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00019B89 File Offset: 0x00017D89
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x04000366 RID: 870
		private TaskStatus executionStatus;
	}
}

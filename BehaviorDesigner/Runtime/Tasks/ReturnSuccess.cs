using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E2 RID: 226
	[TaskDescription("The return success task will always return success except when the child task is running.")]
	[TaskIcon("{SkinColor}ReturnSuccessIcon.png")]
	public class ReturnSuccess : Decorator
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x00019B9A File Offset: 0x00017D9A
		public override bool CanExecute()
		{
			return this.executionStatus == TaskStatus.Inactive || this.executionStatus == TaskStatus.Running;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00019BAF File Offset: 0x00017DAF
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00019BB8 File Offset: 0x00017DB8
		public override TaskStatus Decorate(TaskStatus status)
		{
			if (status == TaskStatus.Failure)
			{
				return TaskStatus.Success;
			}
			return status;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00019BC1 File Offset: 0x00017DC1
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x04000367 RID: 871
		private TaskStatus executionStatus;
	}
}

using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000DE RID: 222
	[TaskDescription("The interrupt task will stop all child tasks from running if it is interrupted. The interruption can be triggered by the perform interruption task. The interrupt task will keep running its child until this interruption is called. If no interruption happens and the child task completed its execution the interrupt task will return the value assigned by the child task.")]
	[TaskIcon("{SkinColor}InterruptIcon.png")]
	public class Interrupt : Decorator
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x00019A13 File Offset: 0x00017C13
		public override bool CanExecute()
		{
			return this.executionStatus == TaskStatus.Inactive || this.executionStatus == TaskStatus.Running;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00019A28 File Offset: 0x00017C28
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00019A31 File Offset: 0x00017C31
		public void DoInterrupt(TaskStatus status)
		{
			this.interruptStatus = status;
			BehaviorManager.instance.Interrupt(base.Owner, this, status);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00019A4C File Offset: 0x00017C4C
		public override TaskStatus OverrideStatus()
		{
			return this.interruptStatus;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00019A54 File Offset: 0x00017C54
		public override void OnEnd()
		{
			this.interruptStatus = TaskStatus.Failure;
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x0400035E RID: 862
		private TaskStatus interruptStatus = TaskStatus.Failure;

		// Token: 0x0400035F RID: 863
		private TaskStatus executionStatus;
	}
}

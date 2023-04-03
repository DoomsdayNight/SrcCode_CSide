using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000DF RID: 223
	[TaskDescription("The inverter task will invert the return value of the child task after it has finished executing. If the child returns success, the inverter task will return failure. If the child returns failure, the inverter task will return success.")]
	[TaskIcon("{SkinColor}InverterIcon.png")]
	public class Inverter : Decorator
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x00019A73 File Offset: 0x00017C73
		public override bool CanExecute()
		{
			return this.executionStatus == TaskStatus.Inactive || this.executionStatus == TaskStatus.Running;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00019A88 File Offset: 0x00017C88
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00019A91 File Offset: 0x00017C91
		public override TaskStatus Decorate(TaskStatus status)
		{
			if (status == TaskStatus.Success)
			{
				return TaskStatus.Failure;
			}
			if (status == TaskStatus.Failure)
			{
				return TaskStatus.Success;
			}
			return status;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00019AA0 File Offset: 0x00017CA0
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x04000360 RID: 864
		private TaskStatus executionStatus;
	}
}

using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000B9 RID: 185
	[TaskDescription("Perform the actual interruption. This will immediately stop the specified tasks from running and will return success or failure depending on the value of interrupt success.")]
	[TaskIcon("{SkinColor}PerformInterruptionIcon.png")]
	public class PerformInterruption : Action
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x00016D34 File Offset: 0x00014F34
		public override TaskStatus OnUpdate()
		{
			for (int i = 0; i < this.interruptTasks.Length; i++)
			{
				this.interruptTasks[i].DoInterrupt(this.interruptSuccess.Value ? TaskStatus.Success : TaskStatus.Failure);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00016D73 File Offset: 0x00014F73
		public override void OnReset()
		{
			this.interruptTasks = null;
			this.interruptSuccess = false;
		}

		// Token: 0x040002D1 RID: 721
		[Tooltip("The list of tasks to interrupt. Can be any number of tasks")]
		public Interrupt[] interruptTasks;

		// Token: 0x040002D2 RID: 722
		[Tooltip("When we interrupt the task should we return a task status of success?")]
		public SharedBool interruptSuccess;
	}
}

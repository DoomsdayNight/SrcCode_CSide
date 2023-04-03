using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000E3 RID: 227
	[TaskDescription("The task guard task is similar to a semaphore in multithreaded programming. The task guard task is there to ensure a limited resource is not being overused. \n\nFor example, you may place a task guard above a task that plays an animation. Elsewhere within your behavior tree you may also have another task that plays a different animation but uses the same bones for that animation. Because of this you don't want that animation to play twice at the same time. Placing a task guard will let you specify how many times a particular task can be accessed at the same time.\n\nIn the previous animation task example you would specify an access count of 1. With this setup the animation task can be only controlled by one task at a time. If the first task is playing the animation and a second task wants to control the animation as well, it will either have to wait or skip over the task completely.")]
	[TaskIcon("{SkinColor}TaskGuardIcon.png")]
	public class TaskGuard : Decorator
	{
		// Token: 0x06000746 RID: 1862 RVA: 0x00019BD2 File Offset: 0x00017DD2
		public override bool CanExecute()
		{
			return this.executingTasks < this.maxTaskAccessCount.Value && !this.executing;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00019BF4 File Offset: 0x00017DF4
		public override void OnChildStarted()
		{
			this.executingTasks++;
			this.executing = true;
			for (int i = 0; i < this.linkedTaskGuards.Length; i++)
			{
				this.linkedTaskGuards[i].taskExecuting(true);
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00019C37 File Offset: 0x00017E37
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			if (this.executing || !this.waitUntilTaskAvailable.Value)
			{
				return status;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00019C51 File Offset: 0x00017E51
		public void taskExecuting(bool increase)
		{
			this.executingTasks += (increase ? 1 : -1);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00019C68 File Offset: 0x00017E68
		public override void OnEnd()
		{
			if (this.executing)
			{
				this.executingTasks--;
				for (int i = 0; i < this.linkedTaskGuards.Length; i++)
				{
					this.linkedTaskGuards[i].taskExecuting(false);
				}
				this.executing = false;
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00019CB3 File Offset: 0x00017EB3
		public override void OnReset()
		{
			this.maxTaskAccessCount = null;
			this.linkedTaskGuards = null;
			this.waitUntilTaskAvailable = true;
		}

		// Token: 0x04000368 RID: 872
		[Tooltip("The number of times the child tasks can be accessed by parallel tasks at once")]
		public SharedInt maxTaskAccessCount;

		// Token: 0x04000369 RID: 873
		[Tooltip("The linked tasks that also guard a task. If the task guard is not linked against any other tasks it doesn't have much purpose. Marked as LinkedTask to ensure all tasks linked are linked to the same set of tasks")]
		[LinkedTask]
		public TaskGuard[] linkedTaskGuards;

		// Token: 0x0400036A RID: 874
		[Tooltip("If true the task will wait until the child task is available. If false then any unavailable child tasks will be skipped over")]
		public SharedBool waitUntilTaskAvailable;

		// Token: 0x0400036B RID: 875
		private int executingTasks;

		// Token: 0x0400036C RID: 876
		private bool executing;
	}
}

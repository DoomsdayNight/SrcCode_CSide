using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000C5 RID: 197
	[TaskDescription("Similar to the sequence task, the parallel task will run each child task until a child task returns failure. The difference is that the parallel task will run all of its children tasks simultaneously versus running each task one at a time. Like the sequence class, the parallel task will return success once all of its children tasks have return success. If one tasks returns failure the parallel task will end all of the child tasks and return failure.")]
	[TaskIcon("{SkinColor}ParallelIcon.png")]
	public class Parallel : Composite
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x00017D67 File Offset: 0x00015F67
		public override void OnAwake()
		{
			this.executionStatus = new TaskStatus[this.children.Count];
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00017D7F File Offset: 0x00015F7F
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus[childIndex] = TaskStatus.Running;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00017D98 File Offset: 0x00015F98
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00017D9B File Offset: 0x00015F9B
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00017DA3 File Offset: 0x00015FA3
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00017DB8 File Offset: 0x00015FB8
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			this.executionStatus[childIndex] = childStatus;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00017DC4 File Offset: 0x00015FC4
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			bool flag = true;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				if (this.executionStatus[i] == TaskStatus.Running)
				{
					flag = false;
				}
				else if (this.executionStatus[i] == TaskStatus.Failure)
				{
					return TaskStatus.Failure;
				}
			}
			if (!flag)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00017E08 File Offset: 0x00016008
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = 0;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = TaskStatus.Inactive;
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00017E38 File Offset: 0x00016038
		public override void OnEnd()
		{
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = TaskStatus.Inactive;
			}
			this.currentChildIndex = 0;
		}

		// Token: 0x04000309 RID: 777
		private int currentChildIndex;

		// Token: 0x0400030A RID: 778
		private TaskStatus[] executionStatus;
	}
}

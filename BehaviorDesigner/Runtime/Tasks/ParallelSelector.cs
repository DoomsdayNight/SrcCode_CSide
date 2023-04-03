using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000C7 RID: 199
	[TaskDescription("Similar to the selector task, the parallel selector task will return success as soon as a child task returns success. The difference is that the parallel task will run all of its children tasks simultaneously versus running each task one at a time. If one tasks returns success the parallel selector task will end all of the child tasks and return success. If every child task returns failure then the parallel selector task will return failure.")]
	[TaskIcon("{SkinColor}ParallelSelectorIcon.png")]
	public class ParallelSelector : Composite
	{
		// Token: 0x06000682 RID: 1666 RVA: 0x00017F7C File Offset: 0x0001617C
		public override void OnAwake()
		{
			this.executionStatus = new TaskStatus[this.children.Count];
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00017F94 File Offset: 0x00016194
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus[childIndex] = TaskStatus.Running;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00017FAD File Offset: 0x000161AD
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00017FB0 File Offset: 0x000161B0
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00017FB8 File Offset: 0x000161B8
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00017FCD File Offset: 0x000161CD
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			this.executionStatus[childIndex] = childStatus;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00017FD8 File Offset: 0x000161D8
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = 0;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = TaskStatus.Inactive;
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00018008 File Offset: 0x00016208
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			bool flag = true;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				if (this.executionStatus[i] == TaskStatus.Running)
				{
					flag = false;
				}
				else if (this.executionStatus[i] == TaskStatus.Success)
				{
					return TaskStatus.Success;
				}
			}
			if (!flag)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001804C File Offset: 0x0001624C
		public override void OnEnd()
		{
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = TaskStatus.Inactive;
			}
			this.currentChildIndex = 0;
		}

		// Token: 0x0400030D RID: 781
		private int currentChildIndex;

		// Token: 0x0400030E RID: 782
		private TaskStatus[] executionStatus;
	}
}

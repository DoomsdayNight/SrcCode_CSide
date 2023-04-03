using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000C6 RID: 198
	[TaskDescription("Similar to the parallel selector task, except the parallel complete task will return the child status as soon as the child returns success or failure.The child tasks are executed simultaneously.")]
	[TaskIcon("{SkinColor}ParallelCompleteIcon.png")]
	public class ParallelComplete : Composite
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x00017E70 File Offset: 0x00016070
		public override void OnAwake()
		{
			this.executionStatus = new TaskStatus[this.children.Count];
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00017E88 File Offset: 0x00016088
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus[childIndex] = TaskStatus.Running;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00017EA1 File Offset: 0x000160A1
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00017EA4 File Offset: 0x000160A4
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00017EAC File Offset: 0x000160AC
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00017EC1 File Offset: 0x000160C1
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			this.executionStatus[childIndex] = childStatus;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00017ECC File Offset: 0x000160CC
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = 0;
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = TaskStatus.Inactive;
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00017EFC File Offset: 0x000160FC
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			if (this.currentChildIndex == 0)
			{
				return TaskStatus.Success;
			}
			for (int i = 0; i < this.currentChildIndex; i++)
			{
				if (this.executionStatus[i] == TaskStatus.Success || this.executionStatus[i] == TaskStatus.Failure)
				{
					return this.executionStatus[i];
				}
			}
			return TaskStatus.Running;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00017F44 File Offset: 0x00016144
		public override void OnEnd()
		{
			for (int i = 0; i < this.executionStatus.Length; i++)
			{
				this.executionStatus[i] = TaskStatus.Inactive;
			}
			this.currentChildIndex = 0;
		}

		// Token: 0x0400030B RID: 779
		private int currentChildIndex;

		// Token: 0x0400030C RID: 780
		private TaskStatus[] executionStatus;
	}
}

using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000C8 RID: 200
	[TaskDescription("Similar to the selector task, the priority selector task will return success as soon as a child task returns success. Instead of running the tasks sequentially from left to right within the tree, the priority selector will ask the task what its priority is to determine the order. The higher priority tasks have a higher chance at being run first.")]
	[TaskIcon("{SkinColor}PrioritySelectorIcon.png")]
	public class PrioritySelector : Composite
	{
		// Token: 0x0600068C RID: 1676 RVA: 0x00018084 File Offset: 0x00016284
		public override void OnStart()
		{
			this.childrenExecutionOrder.Clear();
			for (int i = 0; i < this.children.Count; i++)
			{
				float priority = this.children[i].GetPriority();
				int index = this.childrenExecutionOrder.Count;
				for (int j = 0; j < this.childrenExecutionOrder.Count; j++)
				{
					if (this.children[this.childrenExecutionOrder[j]].GetPriority() < priority)
					{
						index = j;
						break;
					}
				}
				this.childrenExecutionOrder.Insert(index, i);
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00018116 File Offset: 0x00016316
		public override int CurrentChildIndex()
		{
			return this.childrenExecutionOrder[this.currentChildIndex];
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00018129 File Offset: 0x00016329
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count && this.executionStatus != TaskStatus.Success;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001814C File Offset: 0x0001634C
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.currentChildIndex++;
			this.executionStatus = childStatus;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00018163 File Offset: 0x00016363
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00018173 File Offset: 0x00016373
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.currentChildIndex = 0;
		}

		// Token: 0x0400030F RID: 783
		private int currentChildIndex;

		// Token: 0x04000310 RID: 784
		private TaskStatus executionStatus;

		// Token: 0x04000311 RID: 785
		private List<int> childrenExecutionOrder = new List<int>();
	}
}

using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000CB RID: 203
	[TaskDescription("The selector task is similar to an \"or\" operation. It will return success as soon as one of its child tasks return success. If a child task returns failure then it will sequentially run the next task. If no child task returns success then it will return failure.")]
	[TaskIcon("{SkinColor}SelectorIcon.png")]
	public class Selector : Composite
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x00018477 File Offset: 0x00016677
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001847F File Offset: 0x0001667F
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count && this.executionStatus != TaskStatus.Success;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000184A2 File Offset: 0x000166A2
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.currentChildIndex++;
			this.executionStatus = childStatus;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000184B9 File Offset: 0x000166B9
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000184C9 File Offset: 0x000166C9
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.currentChildIndex = 0;
		}

		// Token: 0x0400031C RID: 796
		private int currentChildIndex;

		// Token: 0x0400031D RID: 797
		private TaskStatus executionStatus;
	}
}

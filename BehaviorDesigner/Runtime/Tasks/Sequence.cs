using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000CD RID: 205
	[TaskDescription("The sequence task is similar to an \"and\" operation. It will return failure as soon as one of its child tasks return failure. If a child task returns success then it will sequentially run the next task. If all child tasks return success then it will return success.")]
	[TaskIcon("{SkinColor}SequenceIcon.png")]
	public class Sequence : Composite
	{
		// Token: 0x060006B9 RID: 1721 RVA: 0x00018656 File Offset: 0x00016856
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001865E File Offset: 0x0001685E
		public override bool CanExecute()
		{
			return this.currentChildIndex < this.children.Count && this.executionStatus != TaskStatus.Failure;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00018681 File Offset: 0x00016881
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.currentChildIndex++;
			this.executionStatus = childStatus;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00018698 File Offset: 0x00016898
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x000186A8 File Offset: 0x000168A8
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.currentChildIndex = 0;
		}

		// Token: 0x04000322 RID: 802
		private int currentChildIndex;

		// Token: 0x04000323 RID: 803
		private TaskStatus executionStatus;
	}
}

using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000CC RID: 204
	[TaskDescription("The selector evaluator is a selector task which reevaluates its children every tick. It will run the lowest priority child which returns a task status of running. This is done each tick. If a higher priority child is running and the next frame a lower priority child wants to run it will interrupt the higher priority child. The selector evaluator will return success as soon as the first child returns success otherwise it will keep trying higher priority children. This task mimics the conditional abort functionality except the child tasks don't always have to be conditional tasks.")]
	[TaskIcon("{SkinColor}SelectorEvaluatorIcon.png")]
	public class SelectorEvaluator : Composite
	{
		// Token: 0x060006AD RID: 1709 RVA: 0x000184E1 File Offset: 0x000166E1
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000184E9 File Offset: 0x000166E9
		public override void OnChildStarted(int childIndex)
		{
			this.currentChildIndex++;
			this.executionStatus = TaskStatus.Running;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00018500 File Offset: 0x00016700
		public override bool CanExecute()
		{
			if (this.executionStatus == TaskStatus.Success || this.executionStatus == TaskStatus.Running)
			{
				return false;
			}
			if (this.storedCurrentChildIndex != -1)
			{
				return this.currentChildIndex < this.storedCurrentChildIndex - 1;
			}
			return this.currentChildIndex < this.children.Count;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001854E File Offset: 0x0001674E
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			if (childStatus == TaskStatus.Inactive && this.children[childIndex].Disabled)
			{
				this.executionStatus = TaskStatus.Failure;
			}
			if (childStatus != TaskStatus.Inactive && childStatus != TaskStatus.Running)
			{
				this.executionStatus = childStatus;
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001857B File Offset: 0x0001677B
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001858B File Offset: 0x0001678B
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.currentChildIndex = 0;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001859B File Offset: 0x0001679B
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			return this.executionStatus;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000185A3 File Offset: 0x000167A3
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000185A6 File Offset: 0x000167A6
		public override bool CanReevaluate()
		{
			return true;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000185A9 File Offset: 0x000167A9
		public override bool OnReevaluationStarted()
		{
			if (this.executionStatus == TaskStatus.Inactive)
			{
				return false;
			}
			this.storedCurrentChildIndex = this.currentChildIndex;
			this.storedExecutionStatus = this.executionStatus;
			this.currentChildIndex = 0;
			this.executionStatus = TaskStatus.Inactive;
			return true;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000185DC File Offset: 0x000167DC
		public override void OnReevaluationEnded(TaskStatus status)
		{
			if (this.executionStatus != TaskStatus.Failure && this.executionStatus != TaskStatus.Inactive)
			{
				BehaviorManager.instance.Interrupt(base.Owner, this.children[this.storedCurrentChildIndex - 1], this, TaskStatus.Inactive);
			}
			else
			{
				this.currentChildIndex = this.storedCurrentChildIndex;
				this.executionStatus = this.storedExecutionStatus;
			}
			this.storedCurrentChildIndex = -1;
			this.storedExecutionStatus = TaskStatus.Inactive;
		}

		// Token: 0x0400031E RID: 798
		private int currentChildIndex;

		// Token: 0x0400031F RID: 799
		private TaskStatus executionStatus;

		// Token: 0x04000320 RID: 800
		private int storedCurrentChildIndex = -1;

		// Token: 0x04000321 RID: 801
		private TaskStatus storedExecutionStatus;
	}
}

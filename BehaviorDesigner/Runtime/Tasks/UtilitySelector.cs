using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000CE RID: 206
	[TaskDescription("The utility selector task evaluates the child tasks using Utility Theory AI. The child task can override the GetUtility method and return the utility value at that particular time. The task with the highest utility value will be selected and the existing running task will be aborted. The utility selector task reevaluates its children every tick.")]
	[TaskIcon("{SkinColor}UtilitySelectorIcon.png")]
	public class UtilitySelector : Composite
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x000186C0 File Offset: 0x000168C0
		public override void OnStart()
		{
			this.highestUtility = float.MinValue;
			this.availableChildren.Clear();
			for (int i = 0; i < this.children.Count; i++)
			{
				float utility = this.children[i].GetUtility();
				if (utility > this.highestUtility)
				{
					this.highestUtility = utility;
					this.currentChildIndex = i;
				}
				this.availableChildren.Add(i);
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001872E File Offset: 0x0001692E
		public override int CurrentChildIndex()
		{
			return this.currentChildIndex;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00018736 File Offset: 0x00016936
		public override void OnChildStarted(int childIndex)
		{
			this.executionStatus = TaskStatus.Running;
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001873F File Offset: 0x0001693F
		public override bool CanExecute()
		{
			return this.executionStatus != TaskStatus.Success && this.executionStatus != TaskStatus.Running && !this.reevaluating && this.availableChildren.Count > 0;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001876C File Offset: 0x0001696C
		public override void OnChildExecuted(int childIndex, TaskStatus childStatus)
		{
			if (childStatus != TaskStatus.Inactive && childStatus != TaskStatus.Running)
			{
				this.executionStatus = childStatus;
				if (this.executionStatus == TaskStatus.Failure)
				{
					this.availableChildren.Remove(childIndex);
					this.highestUtility = float.MinValue;
					for (int i = 0; i < this.availableChildren.Count; i++)
					{
						float utility = this.children[this.availableChildren[i]].GetUtility();
						if (utility > this.highestUtility)
						{
							this.highestUtility = utility;
							this.currentChildIndex = this.availableChildren[i];
						}
					}
				}
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00018800 File Offset: 0x00016A00
		public override void OnConditionalAbort(int childIndex)
		{
			this.currentChildIndex = childIndex;
			this.executionStatus = TaskStatus.Inactive;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00018810 File Offset: 0x00016A10
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.currentChildIndex = 0;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00018820 File Offset: 0x00016A20
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			return this.executionStatus;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00018828 File Offset: 0x00016A28
		public override bool CanRunParallelChildren()
		{
			return true;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001882B File Offset: 0x00016A2B
		public override bool CanReevaluate()
		{
			return true;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001882E File Offset: 0x00016A2E
		public override bool OnReevaluationStarted()
		{
			if (this.executionStatus == TaskStatus.Inactive)
			{
				return false;
			}
			this.reevaluating = true;
			return true;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00018844 File Offset: 0x00016A44
		public override void OnReevaluationEnded(TaskStatus status)
		{
			this.reevaluating = false;
			int num = this.currentChildIndex;
			this.highestUtility = float.MinValue;
			for (int i = 0; i < this.availableChildren.Count; i++)
			{
				float utility = this.children[this.availableChildren[i]].GetUtility();
				if (utility > this.highestUtility)
				{
					this.highestUtility = utility;
					this.currentChildIndex = this.availableChildren[i];
				}
			}
			if (num != this.currentChildIndex)
			{
				BehaviorManager.instance.Interrupt(base.Owner, this.children[num], this, TaskStatus.Failure);
				this.executionStatus = TaskStatus.Inactive;
			}
		}

		// Token: 0x04000324 RID: 804
		private int currentChildIndex;

		// Token: 0x04000325 RID: 805
		private float highestUtility;

		// Token: 0x04000326 RID: 806
		private TaskStatus executionStatus;

		// Token: 0x04000327 RID: 807
		private bool reevaluating;

		// Token: 0x04000328 RID: 808
		private List<int> availableChildren = new List<int>();
	}
}

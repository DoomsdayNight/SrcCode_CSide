using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000C9 RID: 201
	[TaskDescription("Similar to the selector task, the random selector task will return success as soon as a child task returns success.  The difference is that the random selector class will run its children in a random order. The selector task is deterministic in that it will always run the tasks from left to right within the tree. The random selector task shuffles the child tasks up and then begins execution in a random order. Other than that the random selector class is the same as the selector class. It will continue running tasks until a task completes successfully. If no child tasks return success then it will return failure.")]
	[TaskIcon("{SkinColor}RandomSelectorIcon.png")]
	public class RandomSelector : Composite
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x00018198 File Offset: 0x00016398
		public override void OnAwake()
		{
			if (this.useSeed)
			{
				UnityEngine.Random.InitState(this.seed);
			}
			this.childIndexList.Clear();
			for (int i = 0; i < this.children.Count; i++)
			{
				this.childIndexList.Add(i);
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000181E5 File Offset: 0x000163E5
		public override void OnStart()
		{
			this.ShuffleChilden();
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000181ED File Offset: 0x000163ED
		public override int CurrentChildIndex()
		{
			return this.childrenExecutionOrder.Peek();
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000181FA File Offset: 0x000163FA
		public override bool CanExecute()
		{
			return this.childrenExecutionOrder.Count > 0 && this.executionStatus != TaskStatus.Success;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00018218 File Offset: 0x00016418
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			if (this.childrenExecutionOrder.Count > 0)
			{
				this.childrenExecutionOrder.Pop();
			}
			this.executionStatus = childStatus;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001823B File Offset: 0x0001643B
		public override void OnConditionalAbort(int childIndex)
		{
			this.childrenExecutionOrder.Clear();
			this.executionStatus = TaskStatus.Inactive;
			this.ShuffleChilden();
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00018255 File Offset: 0x00016455
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.childrenExecutionOrder.Clear();
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00018269 File Offset: 0x00016469
		public override void OnReset()
		{
			this.seed = 0;
			this.useSeed = false;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001827C File Offset: 0x0001647C
		private void ShuffleChilden()
		{
			for (int i = this.childIndexList.Count; i > 0; i--)
			{
				int index = UnityEngine.Random.Range(0, i);
				int num = this.childIndexList[index];
				this.childrenExecutionOrder.Push(num);
				this.childIndexList[index] = this.childIndexList[i - 1];
				this.childIndexList[i - 1] = num;
			}
		}

		// Token: 0x04000312 RID: 786
		[Tooltip("Seed the random number generator to make things easier to debug")]
		public int seed;

		// Token: 0x04000313 RID: 787
		[Tooltip("Do we want to use the seed?")]
		public bool useSeed;

		// Token: 0x04000314 RID: 788
		private List<int> childIndexList = new List<int>();

		// Token: 0x04000315 RID: 789
		private Stack<int> childrenExecutionOrder = new Stack<int>();

		// Token: 0x04000316 RID: 790
		private TaskStatus executionStatus;
	}
}

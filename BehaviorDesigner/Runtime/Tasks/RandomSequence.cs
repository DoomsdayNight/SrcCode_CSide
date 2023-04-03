using System;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000CA RID: 202
	[TaskDescription("Similar to the sequence task, the random sequence task will return success as soon as every child task returns success.  The difference is that the random sequence class will run its children in a random order. The sequence task is deterministic in that it will always run the tasks from left to right within the tree. The random sequence task shuffles the child tasks up and then begins execution in a random order. Other than that the random sequence class is the same as the sequence class. It will stop running tasks as soon as a single task ends in failure. On a task failure it will stop executing all of the child tasks and return failure. If no child returns failure then it will return success.")]
	[TaskIcon("{SkinColor}RandomSequenceIcon.png")]
	public class RandomSequence : Composite
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x00018308 File Offset: 0x00016508
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

		// Token: 0x0600069E RID: 1694 RVA: 0x00018355 File Offset: 0x00016555
		public override void OnStart()
		{
			this.ShuffleChilden();
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001835D File Offset: 0x0001655D
		public override int CurrentChildIndex()
		{
			return this.childrenExecutionOrder.Peek();
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001836A File Offset: 0x0001656A
		public override bool CanExecute()
		{
			return this.childrenExecutionOrder.Count > 0 && this.executionStatus != TaskStatus.Failure;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00018388 File Offset: 0x00016588
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			if (this.childrenExecutionOrder.Count > 0)
			{
				this.childrenExecutionOrder.Pop();
			}
			this.executionStatus = childStatus;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x000183AB File Offset: 0x000165AB
		public override void OnConditionalAbort(int childIndex)
		{
			this.childrenExecutionOrder.Clear();
			this.executionStatus = TaskStatus.Inactive;
			this.ShuffleChilden();
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x000183C5 File Offset: 0x000165C5
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.childrenExecutionOrder.Clear();
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000183D9 File Offset: 0x000165D9
		public override void OnReset()
		{
			this.seed = 0;
			this.useSeed = false;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000183EC File Offset: 0x000165EC
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

		// Token: 0x04000317 RID: 791
		[Tooltip("Seed the random number generator to make things easier to debug")]
		public int seed;

		// Token: 0x04000318 RID: 792
		[Tooltip("Do we want to use the seed?")]
		public bool useSeed;

		// Token: 0x04000319 RID: 793
		private List<int> childIndexList = new List<int>();

		// Token: 0x0400031A RID: 794
		private Stack<int> childrenExecutionOrder = new Stack<int>();

		// Token: 0x0400031B RID: 795
		private TaskStatus executionStatus;
	}
}

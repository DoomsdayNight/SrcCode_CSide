using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000DC RID: 220
	[TaskDescription("Evaluates the specified conditional task. If the conditional task returns success then the child task is run and the child status is returned. If the conditional task does not return success then the child task is not run and a failure status is immediately returned.")]
	[TaskIcon("{SkinColor}ConditionalEvaluatorIcon.png")]
	public class ConditionalEvaluator : Decorator
	{
		// Token: 0x06000719 RID: 1817 RVA: 0x000197FC File Offset: 0x000179FC
		public override void OnAwake()
		{
			if (this.conditionalTask != null)
			{
				this.conditionalTask.Owner = base.Owner;
				this.conditionalTask.GameObject = this.gameObject;
				this.conditionalTask.Transform = this.transform;
				this.conditionalTask.OnAwake();
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001984F File Offset: 0x00017A4F
		public override void OnStart()
		{
			if (this.conditionalTask != null)
			{
				this.conditionalTask.OnStart();
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00019864 File Offset: 0x00017A64
		public override bool CanExecute()
		{
			if (this.checkConditionalTask)
			{
				this.checkConditionalTask = false;
				this.OnUpdate();
			}
			return !this.conditionalTaskFailed && (this.executionStatus == TaskStatus.Inactive || this.executionStatus == TaskStatus.Running);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00019899 File Offset: 0x00017A99
		public override bool CanReevaluate()
		{
			return this.reevaluate.Value;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000198A8 File Offset: 0x00017AA8
		public override TaskStatus OnUpdate()
		{
			TaskStatus taskStatus = this.conditionalTask.OnUpdate();
			this.conditionalTaskFailed = (this.conditionalTask == null || taskStatus == TaskStatus.Failure);
			return taskStatus;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000198D7 File Offset: 0x00017AD7
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000198E0 File Offset: 0x00017AE0
		public override TaskStatus OverrideStatus()
		{
			return TaskStatus.Failure;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000198E3 File Offset: 0x00017AE3
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			if (this.conditionalTaskFailed)
			{
				return TaskStatus.Failure;
			}
			return status;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000198F0 File Offset: 0x00017AF0
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.checkConditionalTask = true;
			this.conditionalTaskFailed = false;
			if (this.conditionalTask != null)
			{
				this.conditionalTask.OnEnd();
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001991A File Offset: 0x00017B1A
		public override string OnDrawNodeText()
		{
			if (this.conditionalTask == null || !this.graphLabel)
			{
				return string.Empty;
			}
			return this.conditionalTask.GetType().Name;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00019942 File Offset: 0x00017B42
		public override void OnReset()
		{
			this.conditionalTask = null;
		}

		// Token: 0x04000355 RID: 853
		[Tooltip("Should the conditional task be reevaluated every tick?")]
		public SharedBool reevaluate;

		// Token: 0x04000356 RID: 854
		[InspectTask]
		[Tooltip("The conditional task to evaluate")]
		public Conditional conditionalTask;

		// Token: 0x04000357 RID: 855
		[Tooltip("Should the inspected conditional task be labeled within the graph?")]
		public bool graphLabel;

		// Token: 0x04000358 RID: 856
		private TaskStatus executionStatus;

		// Token: 0x04000359 RID: 857
		private bool checkConditionalTask = true;

		// Token: 0x0400035A RID: 858
		private bool conditionalTaskFailed;
	}
}

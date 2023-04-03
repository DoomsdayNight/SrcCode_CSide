using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001F6 RID: 502
	[TaskCategory("Unity/Math")]
	[TaskDescription("Performs comparison between two integers: less than, less than or equal to, equal to, not equal to, greater than or equal to, or greater than.")]
	public class IntComparison : Conditional
	{
		// Token: 0x06000B1B RID: 2843 RVA: 0x00022A0C File Offset: 0x00020C0C
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case IntComparison.Operation.LessThan:
				if (this.integer1.Value >= this.integer2.Value)
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Success;
			case IntComparison.Operation.LessThanOrEqualTo:
				if (this.integer1.Value > this.integer2.Value)
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Success;
			case IntComparison.Operation.EqualTo:
				if (this.integer1.Value != this.integer2.Value)
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Success;
			case IntComparison.Operation.NotEqualTo:
				if (this.integer1.Value == this.integer2.Value)
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Success;
			case IntComparison.Operation.GreaterThanOrEqualTo:
				if (this.integer1.Value < this.integer2.Value)
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Success;
			case IntComparison.Operation.GreaterThan:
				if (this.integer1.Value <= this.integer2.Value)
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Success;
			default:
				return TaskStatus.Failure;
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00022AEC File Offset: 0x00020CEC
		public override void OnReset()
		{
			this.operation = IntComparison.Operation.LessThan;
			this.integer1.Value = 0;
			this.integer2.Value = 0;
		}

		// Token: 0x04000702 RID: 1794
		[Tooltip("The operation to perform")]
		public IntComparison.Operation operation;

		// Token: 0x04000703 RID: 1795
		[Tooltip("The first integer")]
		public SharedInt integer1;

		// Token: 0x04000704 RID: 1796
		[Tooltip("The second integer")]
		public SharedInt integer2;

		// Token: 0x02001114 RID: 4372
		public enum Operation
		{
			// Token: 0x04009174 RID: 37236
			LessThan,
			// Token: 0x04009175 RID: 37237
			LessThanOrEqualTo,
			// Token: 0x04009176 RID: 37238
			EqualTo,
			// Token: 0x04009177 RID: 37239
			NotEqualTo,
			// Token: 0x04009178 RID: 37240
			GreaterThanOrEqualTo,
			// Token: 0x04009179 RID: 37241
			GreaterThan
		}
	}
}

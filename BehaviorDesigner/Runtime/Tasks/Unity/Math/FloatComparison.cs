using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001F2 RID: 498
	[TaskCategory("Unity/Math")]
	[TaskDescription("Performs comparison between two floats: less than, less than or equal to, equal to, not equal to, greater than or equal to, or greater than.")]
	public class FloatComparison : Conditional
	{
		// Token: 0x06000B0F RID: 2831 RVA: 0x000226D0 File Offset: 0x000208D0
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case FloatComparison.Operation.LessThan:
				if (this.float1.Value >= this.float2.Value)
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Success;
			case FloatComparison.Operation.LessThanOrEqualTo:
				if (this.float1.Value > this.float2.Value)
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Success;
			case FloatComparison.Operation.EqualTo:
				if (!Mathf.Approximately(this.float1.Value, this.float2.Value))
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Success;
			case FloatComparison.Operation.NotEqualTo:
				if (Mathf.Approximately(this.float1.Value, this.float2.Value))
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Success;
			case FloatComparison.Operation.GreaterThanOrEqualTo:
				if (this.float1.Value < this.float2.Value)
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Success;
			case FloatComparison.Operation.GreaterThan:
				if (this.float1.Value <= this.float2.Value)
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Success;
			default:
				return TaskStatus.Failure;
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000227BA File Offset: 0x000209BA
		public override void OnReset()
		{
			this.operation = FloatComparison.Operation.LessThan;
			this.float1.Value = 0f;
			this.float2.Value = 0f;
		}

		// Token: 0x040006F7 RID: 1783
		[Tooltip("The operation to perform")]
		public FloatComparison.Operation operation;

		// Token: 0x040006F8 RID: 1784
		[Tooltip("The first float")]
		public SharedFloat float1;

		// Token: 0x040006F9 RID: 1785
		[Tooltip("The second float")]
		public SharedFloat float2;

		// Token: 0x02001112 RID: 4370
		public enum Operation
		{
			// Token: 0x04009165 RID: 37221
			LessThan,
			// Token: 0x04009166 RID: 37222
			LessThanOrEqualTo,
			// Token: 0x04009167 RID: 37223
			EqualTo,
			// Token: 0x04009168 RID: 37224
			NotEqualTo,
			// Token: 0x04009169 RID: 37225
			GreaterThanOrEqualTo,
			// Token: 0x0400916A RID: 37226
			GreaterThan
		}
	}
}

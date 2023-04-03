using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001F3 RID: 499
	[TaskCategory("Unity/Math")]
	[TaskDescription("Performs a math operation on two floats: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class FloatOperator : Action
	{
		// Token: 0x06000B12 RID: 2834 RVA: 0x000227EC File Offset: 0x000209EC
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case FloatOperator.Operation.Add:
				this.storeResult.Value = this.float1.Value + this.float2.Value;
				break;
			case FloatOperator.Operation.Subtract:
				this.storeResult.Value = this.float1.Value - this.float2.Value;
				break;
			case FloatOperator.Operation.Multiply:
				this.storeResult.Value = this.float1.Value * this.float2.Value;
				break;
			case FloatOperator.Operation.Divide:
				this.storeResult.Value = this.float1.Value / this.float2.Value;
				break;
			case FloatOperator.Operation.Min:
				this.storeResult.Value = Mathf.Min(this.float1.Value, this.float2.Value);
				break;
			case FloatOperator.Operation.Max:
				this.storeResult.Value = Mathf.Max(this.float1.Value, this.float2.Value);
				break;
			case FloatOperator.Operation.Modulo:
				this.storeResult.Value = this.float1.Value % this.float2.Value;
				break;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00022933 File Offset: 0x00020B33
		public override void OnReset()
		{
			this.operation = FloatOperator.Operation.Add;
			this.float1 = 0f;
			this.float2 = 0f;
			this.storeResult = 0f;
		}

		// Token: 0x040006FA RID: 1786
		[Tooltip("The operation to perform")]
		public FloatOperator.Operation operation;

		// Token: 0x040006FB RID: 1787
		[Tooltip("The first float")]
		public SharedFloat float1;

		// Token: 0x040006FC RID: 1788
		[Tooltip("The second float")]
		public SharedFloat float2;

		// Token: 0x040006FD RID: 1789
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;

		// Token: 0x02001113 RID: 4371
		public enum Operation
		{
			// Token: 0x0400916C RID: 37228
			Add,
			// Token: 0x0400916D RID: 37229
			Subtract,
			// Token: 0x0400916E RID: 37230
			Multiply,
			// Token: 0x0400916F RID: 37231
			Divide,
			// Token: 0x04009170 RID: 37232
			Min,
			// Token: 0x04009171 RID: 37233
			Max,
			// Token: 0x04009172 RID: 37234
			Modulo
		}
	}
}

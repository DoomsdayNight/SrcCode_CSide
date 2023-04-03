using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001F7 RID: 503
	[TaskCategory("Unity/Math")]
	[TaskDescription("Performs a math operation on two integers: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class IntOperator : Action
	{
		// Token: 0x06000B1E RID: 2846 RVA: 0x00022B18 File Offset: 0x00020D18
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case IntOperator.Operation.Add:
				this.storeResult.Value = this.integer1.Value + this.integer2.Value;
				break;
			case IntOperator.Operation.Subtract:
				this.storeResult.Value = this.integer1.Value - this.integer2.Value;
				break;
			case IntOperator.Operation.Multiply:
				this.storeResult.Value = this.integer1.Value * this.integer2.Value;
				break;
			case IntOperator.Operation.Divide:
				this.storeResult.Value = this.integer1.Value / this.integer2.Value;
				break;
			case IntOperator.Operation.Min:
				this.storeResult.Value = Mathf.Min(this.integer1.Value, this.integer2.Value);
				break;
			case IntOperator.Operation.Max:
				this.storeResult.Value = Mathf.Max(this.integer1.Value, this.integer2.Value);
				break;
			case IntOperator.Operation.Modulo:
				this.storeResult.Value = this.integer1.Value % this.integer2.Value;
				break;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00022C5F File Offset: 0x00020E5F
		public override void OnReset()
		{
			this.operation = IntOperator.Operation.Add;
			this.integer1 = 0;
			this.integer2 = 0;
			this.storeResult = 0;
		}

		// Token: 0x04000705 RID: 1797
		[Tooltip("The operation to perform")]
		public IntOperator.Operation operation;

		// Token: 0x04000706 RID: 1798
		[Tooltip("The first integer")]
		public SharedInt integer1;

		// Token: 0x04000707 RID: 1799
		[Tooltip("The second integer")]
		public SharedInt integer2;

		// Token: 0x04000708 RID: 1800
		[RequiredField]
		[Tooltip("The variable to store the result")]
		public SharedInt storeResult;

		// Token: 0x02001115 RID: 4373
		public enum Operation
		{
			// Token: 0x0400917B RID: 37243
			Add,
			// Token: 0x0400917C RID: 37244
			Subtract,
			// Token: 0x0400917D RID: 37245
			Multiply,
			// Token: 0x0400917E RID: 37246
			Divide,
			// Token: 0x0400917F RID: 37247
			Min,
			// Token: 0x04009180 RID: 37248
			Max,
			// Token: 0x04009181 RID: 37249
			Modulo
		}
	}
}

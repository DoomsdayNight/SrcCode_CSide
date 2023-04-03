using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001EF RID: 495
	[TaskCategory("Unity/Math")]
	[TaskDescription("Performs a math operation on two bools: AND, OR, NAND, or XOR.")]
	public class BoolOperator : Action
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x00022534 File Offset: 0x00020734
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case BoolOperator.Operation.AND:
				this.storeResult.Value = (this.bool1.Value && this.bool2.Value);
				break;
			case BoolOperator.Operation.OR:
				this.storeResult.Value = (this.bool1.Value || this.bool2.Value);
				break;
			case BoolOperator.Operation.NAND:
				this.storeResult.Value = (!this.bool1.Value || !this.bool2.Value);
				break;
			case BoolOperator.Operation.XOR:
				this.storeResult.Value = (this.bool1.Value ^ this.bool2.Value);
				break;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00022601 File Offset: 0x00020801
		public override void OnReset()
		{
			this.operation = BoolOperator.Operation.AND;
			this.bool1 = false;
			this.bool2 = false;
		}

		// Token: 0x040006EF RID: 1775
		[Tooltip("The operation to perform")]
		public BoolOperator.Operation operation;

		// Token: 0x040006F0 RID: 1776
		[Tooltip("The first bool")]
		public SharedBool bool1;

		// Token: 0x040006F1 RID: 1777
		[Tooltip("The second bool")]
		public SharedBool bool2;

		// Token: 0x040006F2 RID: 1778
		[Tooltip("The variable to store the result")]
		public SharedBool storeResult;

		// Token: 0x02001111 RID: 4369
		public enum Operation
		{
			// Token: 0x04009160 RID: 37216
			AND,
			// Token: 0x04009161 RID: 37217
			OR,
			// Token: 0x04009162 RID: 37218
			NAND,
			// Token: 0x04009163 RID: 37219
			XOR
		}
	}
}

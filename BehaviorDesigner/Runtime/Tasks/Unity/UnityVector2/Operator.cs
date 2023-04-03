using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x02000107 RID: 263
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Performs a math operation on two Vector2s: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class Operator : Action
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x0001AA34 File Offset: 0x00018C34
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case Operator.Operation.Add:
				this.storeResult.Value = this.firstVector2.Value + this.secondVector2.Value;
				break;
			case Operator.Operation.Subtract:
				this.storeResult.Value = this.firstVector2.Value - this.secondVector2.Value;
				break;
			case Operator.Operation.Scale:
				this.storeResult.Value = Vector2.Scale(this.firstVector2.Value, this.secondVector2.Value);
				break;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001AAD3 File Offset: 0x00018CD3
		public override void OnReset()
		{
			this.operation = Operator.Operation.Add;
			this.firstVector2 = Vector2.zero;
			this.secondVector2 = Vector2.zero;
			this.storeResult = Vector2.zero;
		}

		// Token: 0x040003C5 RID: 965
		[Tooltip("The operation to perform")]
		public Operator.Operation operation;

		// Token: 0x040003C6 RID: 966
		[Tooltip("The first Vector2")]
		public SharedVector2 firstVector2;

		// Token: 0x040003C7 RID: 967
		[Tooltip("The second Vector2")]
		public SharedVector2 secondVector2;

		// Token: 0x040003C8 RID: 968
		[Tooltip("The variable to store the result")]
		public SharedVector2 storeResult;

		// Token: 0x02001110 RID: 4368
		public enum Operation
		{
			// Token: 0x0400915C RID: 37212
			Add,
			// Token: 0x0400915D RID: 37213
			Subtract,
			// Token: 0x0400915E RID: 37214
			Scale
		}
	}
}

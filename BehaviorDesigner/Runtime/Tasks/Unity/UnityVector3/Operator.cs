using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000F6 RID: 246
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Performs a math operation on two Vector3s: Add, Subtract, Multiply, Divide, Min, or Max.")]
	public class Operator : Action
	{
		// Token: 0x06000784 RID: 1924 RVA: 0x0001A2BC File Offset: 0x000184BC
		public override TaskStatus OnUpdate()
		{
			switch (this.operation)
			{
			case Operator.Operation.Add:
				this.storeResult.Value = this.firstVector3.Value + this.secondVector3.Value;
				break;
			case Operator.Operation.Subtract:
				this.storeResult.Value = this.firstVector3.Value - this.secondVector3.Value;
				break;
			case Operator.Operation.Scale:
				this.storeResult.Value = Vector3.Scale(this.firstVector3.Value, this.secondVector3.Value);
				break;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001A35B File Offset: 0x0001855B
		public override void OnReset()
		{
			this.operation = Operator.Operation.Add;
			this.firstVector3 = Vector3.zero;
			this.secondVector3 = Vector3.zero;
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04000395 RID: 917
		[Tooltip("The operation to perform")]
		public Operator.Operation operation;

		// Token: 0x04000396 RID: 918
		[Tooltip("The first Vector3")]
		public SharedVector3 firstVector3;

		// Token: 0x04000397 RID: 919
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x04000398 RID: 920
		[Tooltip("The variable to store the result")]
		public SharedVector3 storeResult;

		// Token: 0x0200110F RID: 4367
		public enum Operation
		{
			// Token: 0x04009158 RID: 37208
			Add,
			// Token: 0x04009159 RID: 37209
			Subtract,
			// Token: 0x0400915A RID: 37210
			Scale
		}
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001AC RID: 428
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the quaternion of a forward vector.")]
	public class LookRotation : Action
	{
		// Token: 0x06000A11 RID: 2577 RVA: 0x0001FEF3 File Offset: 0x0001E0F3
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.LookRotation(this.forwardVector.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001FF11 File Offset: 0x0001E111
		public override void OnReset()
		{
			this.forwardVector = Vector3.zero;
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x040005F4 RID: 1524
		[Tooltip("The forward vector")]
		public SharedVector3 forwardVector;

		// Token: 0x040005F5 RID: 1525
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x040005F6 RID: 1526
		[Tooltip("The stored quaternion")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}

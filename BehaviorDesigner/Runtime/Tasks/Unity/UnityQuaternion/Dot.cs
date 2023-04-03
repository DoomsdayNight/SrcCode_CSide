using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001A6 RID: 422
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the dot product between two rotations.")]
	public class Dot : Action
	{
		// Token: 0x060009FF RID: 2559 RVA: 0x0001FCE1 File Offset: 0x0001DEE1
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Dot(this.leftRotation.Value, this.rightRotation.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0001FD0C File Offset: 0x0001DF0C
		public override void OnReset()
		{
			this.leftRotation = (this.rightRotation = Quaternion.identity);
			this.storeResult = 0f;
		}

		// Token: 0x040005E5 RID: 1509
		[Tooltip("The first rotation")]
		public SharedQuaternion leftRotation;

		// Token: 0x040005E6 RID: 1510
		[Tooltip("The second rotation")]
		public SharedQuaternion rightRotation;

		// Token: 0x040005E7 RID: 1511
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001A7 RID: 423
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the quaternion of a euler vector.")]
	public class Euler : Action
	{
		// Token: 0x06000A02 RID: 2562 RVA: 0x0001FD4A File Offset: 0x0001DF4A
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Euler(this.eulerVector.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0001FD68 File Offset: 0x0001DF68
		public override void OnReset()
		{
			this.eulerVector = Vector3.zero;
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x040005E8 RID: 1512
		[Tooltip("The euler vector")]
		public SharedVector3 eulerVector;

		// Token: 0x040005E9 RID: 1513
		[Tooltip("The stored quaternion")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}

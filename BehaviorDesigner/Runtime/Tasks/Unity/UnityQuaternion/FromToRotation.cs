using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001A8 RID: 424
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores a rotation which rotates from the first direction to the second.")]
	public class FromToRotation : Action
	{
		// Token: 0x06000A05 RID: 2565 RVA: 0x0001FD92 File Offset: 0x0001DF92
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.FromToRotation(this.fromDirection.Value, this.toDirection.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		public override void OnReset()
		{
			this.fromDirection = (this.toDirection = Vector3.zero);
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x040005EA RID: 1514
		[Tooltip("The from rotation")]
		public SharedVector3 fromDirection;

		// Token: 0x040005EB RID: 1515
		[Tooltip("The to rotation")]
		public SharedVector3 toDirection;

		// Token: 0x040005EC RID: 1516
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}

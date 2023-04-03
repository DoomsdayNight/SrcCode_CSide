using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001AD RID: 429
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the quaternion after a rotation.")]
	public class RotateTowards : Action
	{
		// Token: 0x06000A14 RID: 2580 RVA: 0x0001FF3B File Offset: 0x0001E13B
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.RotateTowards(this.fromQuaternion.Value, this.toQuaternion.Value, this.maxDeltaDegrees.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001FF70 File Offset: 0x0001E170
		public override void OnReset()
		{
			this.fromQuaternion = (this.toQuaternion = (this.storeResult = Quaternion.identity));
			this.maxDeltaDegrees = 0f;
		}

		// Token: 0x040005F7 RID: 1527
		[Tooltip("The from rotation")]
		public SharedQuaternion fromQuaternion;

		// Token: 0x040005F8 RID: 1528
		[Tooltip("The to rotation")]
		public SharedQuaternion toQuaternion;

		// Token: 0x040005F9 RID: 1529
		[Tooltip("The maximum degrees delta")]
		public SharedFloat maxDeltaDegrees;

		// Token: 0x040005FA RID: 1530
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001AE RID: 430
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Spherically lerp between two quaternions.")]
	public class Slerp : Action
	{
		// Token: 0x06000A17 RID: 2583 RVA: 0x0001FFB7 File Offset: 0x0001E1B7
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Slerp(this.fromQuaternion.Value, this.toQuaternion.Value, this.amount.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0001FFEC File Offset: 0x0001E1EC
		public override void OnReset()
		{
			this.fromQuaternion = (this.toQuaternion = (this.storeResult = Quaternion.identity));
			this.amount = 0f;
		}

		// Token: 0x040005FB RID: 1531
		[Tooltip("The from rotation")]
		public SharedQuaternion fromQuaternion;

		// Token: 0x040005FC RID: 1532
		[Tooltip("The to rotation")]
		public SharedQuaternion toQuaternion;

		// Token: 0x040005FD RID: 1533
		[Tooltip("The amount to lerp")]
		public SharedFloat amount;

		// Token: 0x040005FE RID: 1534
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}

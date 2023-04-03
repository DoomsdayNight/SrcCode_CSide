using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001AA RID: 426
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the inverse of the specified quaternion.")]
	public class Inverse : Action
	{
		// Token: 0x06000A0B RID: 2571 RVA: 0x0001FE27 File Offset: 0x0001E027
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Inverse(this.targetQuaternion.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0001FE48 File Offset: 0x0001E048
		public override void OnReset()
		{
			this.targetQuaternion = (this.storeResult = Quaternion.identity);
		}

		// Token: 0x040005EE RID: 1518
		[Tooltip("The target quaternion")]
		public SharedQuaternion targetQuaternion;

		// Token: 0x040005EF RID: 1519
		[Tooltip("The stored quaternion")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}

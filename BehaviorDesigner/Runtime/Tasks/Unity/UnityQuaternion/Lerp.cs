using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001AB RID: 427
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Lerps between two quaternions.")]
	public class Lerp : Action
	{
		// Token: 0x06000A0E RID: 2574 RVA: 0x0001FE76 File Offset: 0x0001E076
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Lerp(this.fromQuaternion.Value, this.toQuaternion.Value, this.amount.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001FEAC File Offset: 0x0001E0AC
		public override void OnReset()
		{
			this.fromQuaternion = (this.toQuaternion = (this.storeResult = Quaternion.identity));
			this.amount = 0f;
		}

		// Token: 0x040005F0 RID: 1520
		[Tooltip("The from rotation")]
		public SharedQuaternion fromQuaternion;

		// Token: 0x040005F1 RID: 1521
		[Tooltip("The to rotation")]
		public SharedQuaternion toQuaternion;

		// Token: 0x040005F2 RID: 1522
		[Tooltip("The amount to lerp")]
		public SharedFloat amount;

		// Token: 0x040005F3 RID: 1523
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}

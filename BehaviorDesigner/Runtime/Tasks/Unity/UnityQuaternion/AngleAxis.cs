using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001A5 RID: 421
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the rotation which rotates the specified degrees around the specified axis.")]
	public class AngleAxis : Action
	{
		// Token: 0x060009FC RID: 2556 RVA: 0x0001FC7E File Offset: 0x0001DE7E
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.AngleAxis(this.degrees.Value, this.axis.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0001FCA7 File Offset: 0x0001DEA7
		public override void OnReset()
		{
			this.degrees = 0f;
			this.axis = Vector3.zero;
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x040005E2 RID: 1506
		[Tooltip("The number of degrees")]
		public SharedFloat degrees;

		// Token: 0x040005E3 RID: 1507
		[Tooltip("The axis direction")]
		public SharedVector3 axis;

		// Token: 0x040005E4 RID: 1508
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}

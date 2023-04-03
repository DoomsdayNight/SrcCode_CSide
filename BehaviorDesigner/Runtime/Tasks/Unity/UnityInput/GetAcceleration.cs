using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000216 RID: 534
	[TaskCategory("Unity/Input")]
	[TaskDescription("Stores the acceleration value.")]
	public class GetAcceleration : Action
	{
		// Token: 0x06000B8C RID: 2956 RVA: 0x00023AFD File Offset: 0x00021CFD
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.acceleration;
			return TaskStatus.Success;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00023B10 File Offset: 0x00021D10
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x0400076E RID: 1902
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedVector3 storeResult;
	}
}

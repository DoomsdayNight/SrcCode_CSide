using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000EA RID: 234
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the dot product of two Vector3 values.")]
	public class Dot : Action
	{
		// Token: 0x06000760 RID: 1888 RVA: 0x00019E6B File Offset: 0x0001806B
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Dot(this.leftHandSide.Value, this.rightHandSide.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00019E94 File Offset: 0x00018094
		public override void OnReset()
		{
			this.leftHandSide = Vector3.zero;
			this.rightHandSide = Vector3.zero;
			this.storeResult = 0f;
		}

		// Token: 0x04000378 RID: 888
		[Tooltip("The left hand side of the dot product")]
		public SharedVector3 leftHandSide;

		// Token: 0x04000379 RID: 889
		[Tooltip("The right hand side of the dot product")]
		public SharedVector3 rightHandSide;

		// Token: 0x0400037A RID: 890
		[Tooltip("The dot product result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}

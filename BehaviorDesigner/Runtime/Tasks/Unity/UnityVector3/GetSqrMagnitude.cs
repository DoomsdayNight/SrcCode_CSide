using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000EE RID: 238
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the square magnitude of the Vector3.")]
	public class GetSqrMagnitude : Action
	{
		// Token: 0x0600076C RID: 1900 RVA: 0x00019F80 File Offset: 0x00018180
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value.sqrMagnitude;
			return TaskStatus.Success;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00019FAC File Offset: 0x000181AC
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = 0f;
		}

		// Token: 0x0400037F RID: 895
		[Tooltip("The Vector3 to get the square magnitude of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04000380 RID: 896
		[Tooltip("The square magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}

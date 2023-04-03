using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000EC RID: 236
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the magnitude of the Vector3.")]
	public class GetMagnitude : Action
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x00019EFC File Offset: 0x000180FC
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value.magnitude;
			return TaskStatus.Success;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00019F28 File Offset: 0x00018128
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = 0f;
		}

		// Token: 0x0400037C RID: 892
		[Tooltip("The Vector3 to get the magnitude of")]
		public SharedVector3 vector3Variable;

		// Token: 0x0400037D RID: 893
		[Tooltip("The magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}

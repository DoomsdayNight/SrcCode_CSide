using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000F5 RID: 245
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Normalize the Vector3.")]
	public class Normalize : Action
	{
		// Token: 0x06000781 RID: 1921 RVA: 0x0001A274 File Offset: 0x00018474
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Normalize(this.vector3Variable.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001A292 File Offset: 0x00018492
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04000393 RID: 915
		[Tooltip("The Vector3 to normalize")]
		public SharedVector3 vector3Variable;

		// Token: 0x04000394 RID: 916
		[Tooltip("The normalized resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}

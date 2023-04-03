using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000E9 RID: 233
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Returns the distance between two Vector3s.")]
	public class Distance : Action
	{
		// Token: 0x0600075D RID: 1885 RVA: 0x00019E08 File Offset: 0x00018008
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Distance(this.firstVector3.Value, this.secondVector3.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00019E31 File Offset: 0x00018031
		public override void OnReset()
		{
			this.firstVector3 = Vector3.zero;
			this.secondVector3 = Vector3.zero;
			this.storeResult = 0f;
		}

		// Token: 0x04000375 RID: 885
		[Tooltip("The first Vector3")]
		public SharedVector3 firstVector3;

		// Token: 0x04000376 RID: 886
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x04000377 RID: 887
		[Tooltip("The distance")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020000FF RID: 255
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Stores the square magnitude of the Vector2.")]
	public class GetSqrMagnitude : Action
	{
		// Token: 0x0600079F RID: 1951 RVA: 0x0001A714 File Offset: 0x00018914
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value.sqrMagnitude;
			return TaskStatus.Success;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001A740 File Offset: 0x00018940
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.storeResult = 0f;
		}

		// Token: 0x040003B0 RID: 944
		[Tooltip("The Vector2 to get the square magnitude of")]
		public SharedVector2 vector2Variable;

		// Token: 0x040003B1 RID: 945
		[Tooltip("The square magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}

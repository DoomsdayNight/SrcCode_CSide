using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020000FD RID: 253
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Stores the magnitude of the Vector2.")]
	public class GetMagnitude : Action
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x0001A690 File Offset: 0x00018890
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value.magnitude;
			return TaskStatus.Success;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001A6BC File Offset: 0x000188BC
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.storeResult = 0f;
		}

		// Token: 0x040003AD RID: 941
		[Tooltip("The Vector2 to get the magnitude of")]
		public SharedVector2 vector2Variable;

		// Token: 0x040003AE RID: 942
		[Tooltip("The magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}

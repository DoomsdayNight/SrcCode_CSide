using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000F0 RID: 240
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the Vector2 value of the Vector3.")]
	public class GetVector2 : Action
	{
		// Token: 0x06000772 RID: 1906 RVA: 0x0001A003 File Offset: 0x00018203
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001A021 File Offset: 0x00018221
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = Vector2.zero;
		}

		// Token: 0x04000382 RID: 898
		[Tooltip("The Vector3 to get the Vector2 value of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04000383 RID: 899
		[Tooltip("The Vector2 value")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}

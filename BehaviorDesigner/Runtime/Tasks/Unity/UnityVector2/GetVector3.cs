using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x02000101 RID: 257
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Stores the Vector3 value of the Vector2.")]
	public class GetVector3 : Action
	{
		// Token: 0x060007A5 RID: 1957 RVA: 0x0001A797 File Offset: 0x00018997
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001A7B5 File Offset: 0x000189B5
		public override void OnReset()
		{
			this.vector3Variable = Vector2.zero;
			this.storeResult = Vector3.zero;
		}

		// Token: 0x040003B3 RID: 947
		[Tooltip("The Vector2 to get the Vector3 value of")]
		public SharedVector2 vector3Variable;

		// Token: 0x040003B4 RID: 948
		[Tooltip("The Vector3 value")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}

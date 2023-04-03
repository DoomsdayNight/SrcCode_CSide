using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000F4 RID: 244
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Multiply the Vector3 by a float.")]
	public class Multiply : Action
	{
		// Token: 0x0600077E RID: 1918 RVA: 0x0001A211 File Offset: 0x00018411
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value * this.multiplyBy.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001A23A File Offset: 0x0001843A
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = Vector3.zero;
			this.multiplyBy = 0f;
		}

		// Token: 0x04000390 RID: 912
		[Tooltip("The Vector3 to multiply of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04000391 RID: 913
		[Tooltip("The value to multiply the Vector3 of")]
		public SharedFloat multiplyBy;

		// Token: 0x04000392 RID: 914
		[Tooltip("The multiplication resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}

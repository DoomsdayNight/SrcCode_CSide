using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000F8 RID: 248
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Sets the value of the Vector3.")]
	public class SetValue : Action
	{
		// Token: 0x0600078A RID: 1930 RVA: 0x0001A459 File Offset: 0x00018659
		public override TaskStatus OnUpdate()
		{
			this.vector3Variable.Value = this.vector3Value.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001A472 File Offset: 0x00018672
		public override void OnReset()
		{
			this.vector3Value = Vector3.zero;
			this.vector3Variable = Vector3.zero;
		}

		// Token: 0x0400039E RID: 926
		[Tooltip("The Vector3 to get the values of")]
		public SharedVector3 vector3Value;

		// Token: 0x0400039F RID: 927
		[Tooltip("The Vector3 to set the values of")]
		public SharedVector3 vector3Variable;
	}
}

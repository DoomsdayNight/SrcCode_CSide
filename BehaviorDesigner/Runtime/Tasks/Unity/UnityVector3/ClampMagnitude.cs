using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000E8 RID: 232
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Clamps the magnitude of the Vector3.")]
	public class ClampMagnitude : Action
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x00019DA5 File Offset: 0x00017FA5
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.ClampMagnitude(this.vector3Variable.Value, this.maxLength.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00019DCE File Offset: 0x00017FCE
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = Vector3.zero;
			this.maxLength = 0f;
		}

		// Token: 0x04000372 RID: 882
		[Tooltip("The Vector3 to clamp the magnitude of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04000373 RID: 883
		[Tooltip("The max length of the magnitude")]
		public SharedFloat maxLength;

		// Token: 0x04000374 RID: 884
		[Tooltip("The clamp magnitude resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000159 RID: 345
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedQuaternion variable to the specified object. Returns Success.")]
	public class SetSharedQuaternion : Action
	{
		// Token: 0x060008D7 RID: 2263 RVA: 0x0001D3FA File Offset: 0x0001B5FA
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001D413 File Offset: 0x0001B613
		public override void OnReset()
		{
			this.targetValue = Quaternion.identity;
			this.targetVariable = Quaternion.identity;
		}

		// Token: 0x040004CA RID: 1226
		[Tooltip("The value to set the SharedQuaternion to")]
		public SharedQuaternion targetValue;

		// Token: 0x040004CB RID: 1227
		[RequiredField]
		[Tooltip("The SharedQuaternion to set")]
		public SharedQuaternion targetVariable;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200015F RID: 351
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedVector3 variable to the specified object. Returns Success.")]
	public class SetSharedVector3 : Action
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x0001D598 File Offset: 0x0001B798
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001D5B1 File Offset: 0x0001B7B1
		public override void OnReset()
		{
			this.targetValue = Vector3.zero;
			this.targetVariable = Vector3.zero;
		}

		// Token: 0x040004D6 RID: 1238
		[Tooltip("The value to set the SharedVector3 to")]
		public SharedVector3 targetValue;

		// Token: 0x040004D7 RID: 1239
		[RequiredField]
		[Tooltip("The SharedVector3 to set")]
		public SharedVector3 targetVariable;
	}
}

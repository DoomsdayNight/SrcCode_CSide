using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000160 RID: 352
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedVector4 variable to the specified object. Returns Success.")]
	public class SetSharedVector4 : Action
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x0001D5DB File Offset: 0x0001B7DB
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001D5F4 File Offset: 0x0001B7F4
		public override void OnReset()
		{
			this.targetValue = Vector4.zero;
			this.targetVariable = Vector4.zero;
		}

		// Token: 0x040004D8 RID: 1240
		[Tooltip("The value to set the SharedVector4 to")]
		public SharedVector4 targetValue;

		// Token: 0x040004D9 RID: 1241
		[RequiredField]
		[Tooltip("The SharedVector4 to set")]
		public SharedVector4 targetVariable;
	}
}

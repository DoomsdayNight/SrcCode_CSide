using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200015E RID: 350
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedVector2 variable to the specified object. Returns Success.")]
	public class SetSharedVector2 : Action
	{
		// Token: 0x060008E6 RID: 2278 RVA: 0x0001D555 File Offset: 0x0001B755
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001D56E File Offset: 0x0001B76E
		public override void OnReset()
		{
			this.targetValue = Vector2.zero;
			this.targetVariable = Vector2.zero;
		}

		// Token: 0x040004D4 RID: 1236
		[Tooltip("The value to set the SharedVector2 to")]
		public SharedVector2 targetValue;

		// Token: 0x040004D5 RID: 1237
		[RequiredField]
		[Tooltip("The SharedVector2 to set")]
		public SharedVector2 targetVariable;
	}
}

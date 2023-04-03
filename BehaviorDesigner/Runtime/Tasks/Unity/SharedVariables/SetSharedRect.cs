using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200015A RID: 346
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedRect variable to the specified object. Returns Success.")]
	public class SetSharedRect : Action
	{
		// Token: 0x060008DA RID: 2266 RVA: 0x0001D43D File Offset: 0x0001B63D
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001D458 File Offset: 0x0001B658
		public override void OnReset()
		{
			this.targetValue = default(Rect);
			this.targetVariable = default(Rect);
		}

		// Token: 0x040004CC RID: 1228
		[Tooltip("The value to set the SharedRect to")]
		public SharedRect targetValue;

		// Token: 0x040004CD RID: 1229
		[RequiredField]
		[Tooltip("The SharedRect to set")]
		public SharedRect targetVariable;
	}
}

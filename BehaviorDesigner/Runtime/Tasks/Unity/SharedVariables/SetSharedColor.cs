using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000152 RID: 338
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedColor variable to the specified object. Returns Success.")]
	public class SetSharedColor : Action
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x0001D235 File Offset: 0x0001B435
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001D24E File Offset: 0x0001B44E
		public override void OnReset()
		{
			this.targetValue = Color.black;
			this.targetVariable = Color.black;
		}

		// Token: 0x040004BB RID: 1211
		[Tooltip("The value to set the SharedColor to")]
		public SharedColor targetValue;

		// Token: 0x040004BC RID: 1212
		[RequiredField]
		[Tooltip("The SharedColor to set")]
		public SharedColor targetVariable;
	}
}

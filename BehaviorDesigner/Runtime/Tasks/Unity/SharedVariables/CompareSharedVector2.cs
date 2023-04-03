using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200014E RID: 334
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedVector2 : Conditional
	{
		// Token: 0x060008B6 RID: 2230 RVA: 0x0001D0E8 File Offset: 0x0001B2E8
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001D118 File Offset: 0x0001B318
		public override void OnReset()
		{
			this.variable = Vector2.zero;
			this.compareTo = Vector2.zero;
		}

		// Token: 0x040004B3 RID: 1203
		[Tooltip("The first variable to compare")]
		public SharedVector2 variable;

		// Token: 0x040004B4 RID: 1204
		[Tooltip("The variable to compare to")]
		public SharedVector2 compareTo;
	}
}

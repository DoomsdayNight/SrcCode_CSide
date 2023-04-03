using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200014A RID: 330
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedRect : Conditional
	{
		// Token: 0x060008AA RID: 2218 RVA: 0x0001CECC File Offset: 0x0001B0CC
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001CEFC File Offset: 0x0001B0FC
		public override void OnReset()
		{
			this.variable = default(Rect);
			this.compareTo = default(Rect);
		}

		// Token: 0x040004AB RID: 1195
		[Tooltip("The first variable to compare")]
		public SharedRect variable;

		// Token: 0x040004AC RID: 1196
		[Tooltip("The variable to compare to")]
		public SharedRect compareTo;
	}
}

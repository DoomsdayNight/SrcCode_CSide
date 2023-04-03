using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200014F RID: 335
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedVector3 : Conditional
	{
		// Token: 0x060008B9 RID: 2233 RVA: 0x0001D144 File Offset: 0x0001B344
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001D174 File Offset: 0x0001B374
		public override void OnReset()
		{
			this.variable = Vector3.zero;
			this.compareTo = Vector3.zero;
		}

		// Token: 0x040004B5 RID: 1205
		[Tooltip("The first variable to compare")]
		public SharedVector3 variable;

		// Token: 0x040004B6 RID: 1206
		[Tooltip("The variable to compare to")]
		public SharedVector3 compareTo;
	}
}

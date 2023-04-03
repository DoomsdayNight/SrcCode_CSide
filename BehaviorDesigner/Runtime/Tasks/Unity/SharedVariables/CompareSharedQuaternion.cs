using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000149 RID: 329
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedQuaternion : Conditional
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x0001CE70 File Offset: 0x0001B070
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001CEA0 File Offset: 0x0001B0A0
		public override void OnReset()
		{
			this.variable = Quaternion.identity;
			this.compareTo = Quaternion.identity;
		}

		// Token: 0x040004A9 RID: 1193
		[Tooltip("The first variable to compare")]
		public SharedQuaternion variable;

		// Token: 0x040004AA RID: 1194
		[Tooltip("The variable to compare to")]
		public SharedQuaternion compareTo;
	}
}

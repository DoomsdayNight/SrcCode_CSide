using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000150 RID: 336
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedVector4 : Conditional
	{
		// Token: 0x060008BC RID: 2236 RVA: 0x0001D1A0 File Offset: 0x0001B3A0
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001D1D0 File Offset: 0x0001B3D0
		public override void OnReset()
		{
			this.variable = Vector4.zero;
			this.compareTo = Vector4.zero;
		}

		// Token: 0x040004B7 RID: 1207
		[Tooltip("The first variable to compare")]
		public SharedVector4 variable;

		// Token: 0x040004B8 RID: 1208
		[Tooltip("The variable to compare to")]
		public SharedVector4 compareTo;
	}
}

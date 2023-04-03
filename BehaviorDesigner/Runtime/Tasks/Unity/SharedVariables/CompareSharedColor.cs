using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000142 RID: 322
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedColor : Conditional
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x0001CAA4 File Offset: 0x0001ACA4
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001CAD4 File Offset: 0x0001ACD4
		public override void OnReset()
		{
			this.variable = Color.black;
			this.compareTo = Color.black;
		}

		// Token: 0x0400049B RID: 1179
		[Tooltip("The first variable to compare")]
		public SharedColor variable;

		// Token: 0x0400049C RID: 1180
		[Tooltip("The variable to compare to")]
		public SharedColor compareTo;
	}
}

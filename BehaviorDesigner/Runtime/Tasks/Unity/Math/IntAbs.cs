using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001F4 RID: 500
	[TaskCategory("Unity/Math")]
	[TaskDescription("Stores the absolute value of the int.")]
	public class IntAbs : Action
	{
		// Token: 0x06000B15 RID: 2837 RVA: 0x00022974 File Offset: 0x00020B74
		public override TaskStatus OnUpdate()
		{
			this.intVariable.Value = Mathf.Abs(this.intVariable.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00022992 File Offset: 0x00020B92
		public override void OnReset()
		{
			this.intVariable = 0;
		}

		// Token: 0x040006FE RID: 1790
		[Tooltip("The int to return the absolute value of")]
		public SharedInt intVariable;
	}
}

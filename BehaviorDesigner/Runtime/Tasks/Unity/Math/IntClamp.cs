using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001F5 RID: 501
	[TaskCategory("Unity/Math")]
	[TaskDescription("Clamps the int between two values.")]
	public class IntClamp : Action
	{
		// Token: 0x06000B18 RID: 2840 RVA: 0x000229A8 File Offset: 0x00020BA8
		public override TaskStatus OnUpdate()
		{
			this.intVariable.Value = Mathf.Clamp(this.intVariable.Value, this.minValue.Value, this.maxValue.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000229DC File Offset: 0x00020BDC
		public override void OnReset()
		{
			this.intVariable = 0;
			this.minValue = 0;
			this.maxValue = 0;
		}

		// Token: 0x040006FF RID: 1791
		[Tooltip("The int to clamp")]
		public SharedInt intVariable;

		// Token: 0x04000700 RID: 1792
		[Tooltip("The maximum value of the int")]
		public SharedInt minValue;

		// Token: 0x04000701 RID: 1793
		[Tooltip("The maximum value of the int")]
		public SharedInt maxValue;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001F1 RID: 497
	[TaskCategory("Unity/Math")]
	[TaskDescription("Clamps the float between two values.")]
	public class FloatClamp : Action
	{
		// Token: 0x06000B0C RID: 2828 RVA: 0x00022662 File Offset: 0x00020862
		public override TaskStatus OnUpdate()
		{
			this.floatVariable.Value = Mathf.Clamp(this.floatVariable.Value, this.minValue.Value, this.maxValue.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00022696 File Offset: 0x00020896
		public override void OnReset()
		{
			this.floatVariable = 0f;
			this.minValue = 0f;
			this.maxValue = 0f;
		}

		// Token: 0x040006F4 RID: 1780
		[Tooltip("The float to clamp")]
		public SharedFloat floatVariable;

		// Token: 0x040006F5 RID: 1781
		[Tooltip("The maximum value of the float")]
		public SharedFloat minValue;

		// Token: 0x040006F6 RID: 1782
		[Tooltip("The maximum value of the float")]
		public SharedFloat maxValue;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001F0 RID: 496
	[TaskCategory("Unity/Math")]
	[TaskDescription("Stores the absolute value of the float.")]
	public class FloatAbs : Action
	{
		// Token: 0x06000B09 RID: 2825 RVA: 0x0002262A File Offset: 0x0002082A
		public override TaskStatus OnUpdate()
		{
			this.floatVariable.Value = Mathf.Abs(this.floatVariable.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00022648 File Offset: 0x00020848
		public override void OnReset()
		{
			this.floatVariable = 0f;
		}

		// Token: 0x040006F3 RID: 1779
		[Tooltip("The float to return the absolute value of")]
		public SharedFloat floatVariable;
	}
}

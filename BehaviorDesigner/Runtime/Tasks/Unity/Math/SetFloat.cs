using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x02000200 RID: 512
	[TaskCategory("Unity/Math")]
	[TaskDescription("Sets a float value")]
	public class SetFloat : Action
	{
		// Token: 0x06000B38 RID: 2872 RVA: 0x00022F9E File Offset: 0x0002119E
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.floatValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00022FB7 File Offset: 0x000211B7
		public override void OnReset()
		{
			this.floatValue = 0f;
			this.storeResult = 0f;
		}

		// Token: 0x0400071E RID: 1822
		[Tooltip("The float value to set")]
		public SharedFloat floatValue;

		// Token: 0x0400071F RID: 1823
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}

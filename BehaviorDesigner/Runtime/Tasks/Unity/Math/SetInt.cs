using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x02000201 RID: 513
	[TaskCategory("Unity/Math")]
	[TaskDescription("Sets an int value")]
	public class SetInt : Action
	{
		// Token: 0x06000B3B RID: 2875 RVA: 0x00022FE1 File Offset: 0x000211E1
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.intValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00022FFA File Offset: 0x000211FA
		public override void OnReset()
		{
			this.intValue = 0;
			this.storeResult = 0;
		}

		// Token: 0x04000720 RID: 1824
		[Tooltip("The int value to set")]
		public SharedInt intValue;

		// Token: 0x04000721 RID: 1825
		[Tooltip("The variable to store the result")]
		public SharedInt storeResult;
	}
}

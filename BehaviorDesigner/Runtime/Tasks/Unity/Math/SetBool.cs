using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001FF RID: 511
	[TaskCategory("Unity/Math")]
	[TaskDescription("Sets a bool value")]
	public class SetBool : Action
	{
		// Token: 0x06000B35 RID: 2869 RVA: 0x00022F6F File Offset: 0x0002116F
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.boolValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00022F88 File Offset: 0x00021188
		public override void OnReset()
		{
			this.boolValue = false;
		}

		// Token: 0x0400071C RID: 1820
		[Tooltip("The bool value to set")]
		public SharedBool boolValue;

		// Token: 0x0400071D RID: 1821
		[Tooltip("The variable to store the result")]
		public SharedBool storeResult;
	}
}

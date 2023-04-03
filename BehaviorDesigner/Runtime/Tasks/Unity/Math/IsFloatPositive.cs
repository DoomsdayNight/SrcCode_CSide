using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001F8 RID: 504
	[TaskCategory("Unity/Math")]
	[TaskDescription("Is the float a positive value?")]
	public class IsFloatPositive : Conditional
	{
		// Token: 0x06000B21 RID: 2849 RVA: 0x00022C94 File Offset: 0x00020E94
		public override TaskStatus OnUpdate()
		{
			if (this.floatVariable.Value <= 0f)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00022CAB File Offset: 0x00020EAB
		public override void OnReset()
		{
			this.floatVariable = 0f;
		}

		// Token: 0x04000709 RID: 1801
		[Tooltip("The float to check if positive")]
		public SharedFloat floatVariable;
	}
}

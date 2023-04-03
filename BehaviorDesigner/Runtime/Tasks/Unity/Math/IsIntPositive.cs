using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001F9 RID: 505
	[TaskCategory("Unity/Math")]
	[TaskDescription("Is the int a positive value?")]
	public class IsIntPositive : Conditional
	{
		// Token: 0x06000B24 RID: 2852 RVA: 0x00022CC5 File Offset: 0x00020EC5
		public override TaskStatus OnUpdate()
		{
			if (this.intVariable.Value <= 0)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00022CD8 File Offset: 0x00020ED8
		public override void OnReset()
		{
			this.intVariable = 0;
		}

		// Token: 0x0400070A RID: 1802
		[Tooltip("The int to check if positive")]
		public SharedInt intVariable;
	}
}

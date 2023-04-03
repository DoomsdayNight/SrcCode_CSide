using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001ED RID: 493
	[TaskCategory("Unity/Math")]
	[TaskDescription("Performs a comparison between two bools.")]
	public class BoolComparison : Conditional
	{
		// Token: 0x06000B00 RID: 2816 RVA: 0x000224C1 File Offset: 0x000206C1
		public override TaskStatus OnUpdate()
		{
			if (this.bool1.Value != this.bool2.Value)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000224DE File Offset: 0x000206DE
		public override void OnReset()
		{
			this.bool1 = false;
			this.bool2 = false;
		}

		// Token: 0x040006EC RID: 1772
		[Tooltip("The first bool")]
		public SharedBool bool1;

		// Token: 0x040006ED RID: 1773
		[Tooltip("The second bool")]
		public SharedBool bool2;
	}
}

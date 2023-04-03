using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001EE RID: 494
	[TaskCategory("Unity/Math")]
	[TaskDescription("Flips the value of the bool.")]
	public class BoolFlip : Action
	{
		// Token: 0x06000B03 RID: 2819 RVA: 0x00022500 File Offset: 0x00020700
		public override TaskStatus OnUpdate()
		{
			this.boolVariable.Value = !this.boolVariable.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0002251C File Offset: 0x0002071C
		public override void OnReset()
		{
			this.boolVariable.Value = false;
		}

		// Token: 0x040006EE RID: 1774
		[Tooltip("The bool to flip the value of")]
		public SharedBool boolVariable;
	}
}

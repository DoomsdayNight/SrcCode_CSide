using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000141 RID: 321
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedBool : Conditional
	{
		// Token: 0x0600088F RID: 2191 RVA: 0x0001CA50 File Offset: 0x0001AC50
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001CA80 File Offset: 0x0001AC80
		public override void OnReset()
		{
			this.variable = false;
			this.compareTo = false;
		}

		// Token: 0x04000499 RID: 1177
		[Tooltip("The first variable to compare")]
		public SharedBool variable;

		// Token: 0x0400049A RID: 1178
		[Tooltip("The variable to compare to")]
		public SharedBool compareTo;
	}
}

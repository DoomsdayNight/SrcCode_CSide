using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200014C RID: 332
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedTransform : Conditional
	{
		// Token: 0x060008B0 RID: 2224 RVA: 0x0001CF88 File Offset: 0x0001B188
		public override TaskStatus OnUpdate()
		{
			if (this.variable.Value == null && this.compareTo.Value != null)
			{
				return TaskStatus.Failure;
			}
			if (this.variable.Value == null && this.compareTo.Value == null)
			{
				return TaskStatus.Success;
			}
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001D005 File Offset: 0x0001B205
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x040004AF RID: 1199
		[Tooltip("The first variable to compare")]
		public SharedTransform variable;

		// Token: 0x040004B0 RID: 1200
		[Tooltip("The variable to compare to")]
		public SharedTransform compareTo;
	}
}

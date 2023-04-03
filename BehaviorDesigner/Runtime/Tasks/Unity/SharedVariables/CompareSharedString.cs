using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200014B RID: 331
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedString : Conditional
	{
		// Token: 0x060008AD RID: 2221 RVA: 0x0001CF39 File Offset: 0x0001B139
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001CF5B File Offset: 0x0001B15B
		public override void OnReset()
		{
			this.variable = "";
			this.compareTo = "";
		}

		// Token: 0x040004AD RID: 1197
		[Tooltip("The first variable to compare")]
		public SharedString variable;

		// Token: 0x040004AE RID: 1198
		[Tooltip("The variable to compare to")]
		public SharedString compareTo;
	}
}

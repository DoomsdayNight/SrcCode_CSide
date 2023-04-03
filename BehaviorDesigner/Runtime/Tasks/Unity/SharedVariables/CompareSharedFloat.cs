using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000143 RID: 323
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedFloat : Conditional
	{
		// Token: 0x06000895 RID: 2197 RVA: 0x0001CB00 File Offset: 0x0001AD00
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001CB30 File Offset: 0x0001AD30
		public override void OnReset()
		{
			this.variable = 0f;
			this.compareTo = 0f;
		}

		// Token: 0x0400049D RID: 1181
		[Tooltip("The first variable to compare")]
		public SharedFloat variable;

		// Token: 0x0400049E RID: 1182
		[Tooltip("The variable to compare to")]
		public SharedFloat compareTo;
	}
}

using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000147 RID: 327
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedObject : Conditional
	{
		// Token: 0x060008A1 RID: 2209 RVA: 0x0001CD10 File Offset: 0x0001AF10
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

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001CD8D File Offset: 0x0001AF8D
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x040004A5 RID: 1189
		[Tooltip("The first variable to compare")]
		public SharedObject variable;

		// Token: 0x040004A6 RID: 1190
		[Tooltip("The variable to compare to")]
		public SharedObject compareTo;
	}
}

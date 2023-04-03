using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000144 RID: 324
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedGameObject : Conditional
	{
		// Token: 0x06000898 RID: 2200 RVA: 0x0001CB5C File Offset: 0x0001AD5C
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

		// Token: 0x06000899 RID: 2201 RVA: 0x0001CBD9 File Offset: 0x0001ADD9
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x0400049F RID: 1183
		[Tooltip("The first variable to compare")]
		public SharedGameObject variable;

		// Token: 0x040004A0 RID: 1184
		[Tooltip("The variable to compare to")]
		public SharedGameObject compareTo;
	}
}

using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000145 RID: 325
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedGameObjectList : Conditional
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x0001CBF4 File Offset: 0x0001ADF4
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
			if (this.variable.Value.Count != this.compareTo.Value.Count)
			{
				return TaskStatus.Failure;
			}
			for (int i = 0; i < this.variable.Value.Count; i++)
			{
				if (this.variable.Value[i] != this.compareTo.Value[i])
				{
					return TaskStatus.Failure;
				}
			}
			return TaskStatus.Success;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001CCA4 File Offset: 0x0001AEA4
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x040004A1 RID: 1185
		[Tooltip("The first variable to compare")]
		public SharedGameObjectList variable;

		// Token: 0x040004A2 RID: 1186
		[Tooltip("The variable to compare to")]
		public SharedGameObjectList compareTo;
	}
}

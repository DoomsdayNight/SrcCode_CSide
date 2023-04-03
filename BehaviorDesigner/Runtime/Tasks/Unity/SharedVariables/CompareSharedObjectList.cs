using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000148 RID: 328
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedObjectList : Conditional
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x0001CDA8 File Offset: 0x0001AFA8
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

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001CE58 File Offset: 0x0001B058
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x040004A7 RID: 1191
		[Tooltip("The first variable to compare")]
		public SharedObjectList variable;

		// Token: 0x040004A8 RID: 1192
		[Tooltip("The variable to compare to")]
		public SharedObjectList compareTo;
	}
}

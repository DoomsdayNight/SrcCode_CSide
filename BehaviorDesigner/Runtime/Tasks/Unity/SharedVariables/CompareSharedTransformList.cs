using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200014D RID: 333
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedTransformList : Conditional
	{
		// Token: 0x060008B3 RID: 2227 RVA: 0x0001D020 File Offset: 0x0001B220
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

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001D0D0 File Offset: 0x0001B2D0
		public override void OnReset()
		{
			this.variable = null;
			this.compareTo = null;
		}

		// Token: 0x040004B1 RID: 1201
		[Tooltip("The first variable to compare")]
		public SharedTransformList variable;

		// Token: 0x040004B2 RID: 1202
		[Tooltip("The variable to compare to")]
		public SharedTransformList compareTo;
	}
}

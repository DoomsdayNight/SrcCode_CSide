using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000146 RID: 326
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedInt : Conditional
	{
		// Token: 0x0600089E RID: 2206 RVA: 0x0001CCBC File Offset: 0x0001AEBC
		public override TaskStatus OnUpdate()
		{
			if (!this.variable.Value.Equals(this.compareTo.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001CCEC File Offset: 0x0001AEEC
		public override void OnReset()
		{
			this.variable = 0;
			this.compareTo = 0;
		}

		// Token: 0x040004A3 RID: 1187
		[Tooltip("The first variable to compare")]
		public SharedInt variable;

		// Token: 0x040004A4 RID: 1188
		[Tooltip("The variable to compare to")]
		public SharedInt compareTo;
	}
}

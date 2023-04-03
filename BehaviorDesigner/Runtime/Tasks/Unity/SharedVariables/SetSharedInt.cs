using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000156 RID: 342
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedInt variable to the specified object. Returns Success.")]
	public class SetSharedInt : Action
	{
		// Token: 0x060008CE RID: 2254 RVA: 0x0001D35D File Offset: 0x0001B55D
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0001D376 File Offset: 0x0001B576
		public override void OnReset()
		{
			this.targetValue = 0;
			this.targetVariable = 0;
		}

		// Token: 0x040004C4 RID: 1220
		[Tooltip("The value to set the SharedInt to")]
		public SharedInt targetValue;

		// Token: 0x040004C5 RID: 1221
		[RequiredField]
		[Tooltip("The SharedInt to set")]
		public SharedInt targetVariable;
	}
}

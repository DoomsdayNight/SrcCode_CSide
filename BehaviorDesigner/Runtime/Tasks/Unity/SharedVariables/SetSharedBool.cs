using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000151 RID: 337
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedBool variable to the specified object. Returns Success.")]
	public class SetSharedBool : Action
	{
		// Token: 0x060008BF RID: 2239 RVA: 0x0001D1FA File Offset: 0x0001B3FA
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001D213 File Offset: 0x0001B413
		public override void OnReset()
		{
			this.targetValue = false;
			this.targetVariable = false;
		}

		// Token: 0x040004B9 RID: 1209
		[Tooltip("The value to set the SharedBool to")]
		public SharedBool targetValue;

		// Token: 0x040004BA RID: 1210
		[RequiredField]
		[Tooltip("The SharedBool to set")]
		public SharedBool targetVariable;
	}
}

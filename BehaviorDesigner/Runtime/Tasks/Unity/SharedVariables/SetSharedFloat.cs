using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000153 RID: 339
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedFloat variable to the specified object. Returns Success.")]
	public class SetSharedFloat : Action
	{
		// Token: 0x060008C5 RID: 2245 RVA: 0x0001D278 File Offset: 0x0001B478
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001D291 File Offset: 0x0001B491
		public override void OnReset()
		{
			this.targetValue = 0f;
			this.targetVariable = 0f;
		}

		// Token: 0x040004BD RID: 1213
		[Tooltip("The value to set the SharedFloat to")]
		public SharedFloat targetValue;

		// Token: 0x040004BE RID: 1214
		[RequiredField]
		[Tooltip("The SharedFloat to set")]
		public SharedFloat targetVariable;
	}
}

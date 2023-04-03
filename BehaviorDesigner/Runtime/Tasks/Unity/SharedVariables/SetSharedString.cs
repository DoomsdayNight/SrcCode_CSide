using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200015B RID: 347
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedString variable to the specified object. Returns Success.")]
	public class SetSharedString : Action
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x0001D495 File Offset: 0x0001B695
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001D4AE File Offset: 0x0001B6AE
		public override void OnReset()
		{
			this.targetValue = "";
			this.targetVariable = "";
		}

		// Token: 0x040004CE RID: 1230
		[Tooltip("The value to set the SharedString to")]
		public SharedString targetValue;

		// Token: 0x040004CF RID: 1231
		[RequiredField]
		[Tooltip("The SharedString to set")]
		public SharedString targetVariable;
	}
}

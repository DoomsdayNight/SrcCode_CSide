using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200015C RID: 348
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedTransform variable to the specified object. Returns Success.")]
	public class SetSharedTransform : Action
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = ((this.targetValue.Value != null) ? this.targetValue.Value : this.transform);
			return TaskStatus.Success;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001D50C File Offset: 0x0001B70C
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x040004D0 RID: 1232
		[Tooltip("The value to set the SharedTransform to. If null the variable will be set to the current Transform")]
		public SharedTransform targetValue;

		// Token: 0x040004D1 RID: 1233
		[RequiredField]
		[Tooltip("The SharedTransform to set")]
		public SharedTransform targetVariable;
	}
}

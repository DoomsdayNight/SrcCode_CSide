using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200015D RID: 349
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedTransformList variable to the specified object. Returns Success.")]
	public class SetSharedTransformList : Action
	{
		// Token: 0x060008E3 RID: 2275 RVA: 0x0001D524 File Offset: 0x0001B724
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001D53D File Offset: 0x0001B73D
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x040004D2 RID: 1234
		[Tooltip("The value to set the SharedTransformList to.")]
		public SharedTransformList targetValue;

		// Token: 0x040004D3 RID: 1235
		[RequiredField]
		[Tooltip("The SharedTransformList to set")]
		public SharedTransformList targetVariable;
	}
}

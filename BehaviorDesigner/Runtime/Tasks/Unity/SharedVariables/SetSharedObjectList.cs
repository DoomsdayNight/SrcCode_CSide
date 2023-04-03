using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000158 RID: 344
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedObjectList variable to the specified object. Returns Success.")]
	public class SetSharedObjectList : Action
	{
		// Token: 0x060008D4 RID: 2260 RVA: 0x0001D3C9 File Offset: 0x0001B5C9
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001D3E2 File Offset: 0x0001B5E2
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x040004C8 RID: 1224
		[Tooltip("The value to set the SharedObjectList to.")]
		public SharedObjectList targetValue;

		// Token: 0x040004C9 RID: 1225
		[RequiredField]
		[Tooltip("The SharedObjectList to set")]
		public SharedObjectList targetVariable;
	}
}

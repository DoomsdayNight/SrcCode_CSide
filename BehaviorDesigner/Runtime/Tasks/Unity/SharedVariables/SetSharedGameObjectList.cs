using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000155 RID: 341
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedGameObjectList variable to the specified object. Returns Success.")]
	public class SetSharedGameObjectList : Action
	{
		// Token: 0x060008CB RID: 2251 RVA: 0x0001D32C File Offset: 0x0001B52C
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0001D345 File Offset: 0x0001B545
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x040004C2 RID: 1218
		[Tooltip("The value to set the SharedGameObjectList to.")]
		public SharedGameObjectList targetValue;

		// Token: 0x040004C3 RID: 1219
		[RequiredField]
		[Tooltip("The SharedGameObjectList to set")]
		public SharedGameObjectList targetVariable;
	}
}

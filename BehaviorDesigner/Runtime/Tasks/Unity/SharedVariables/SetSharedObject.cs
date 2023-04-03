using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000157 RID: 343
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedObject variable to the specified object. Returns Success.")]
	public class SetSharedObject : Action
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x0001D398 File Offset: 0x0001B598
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0001D3B1 File Offset: 0x0001B5B1
		public override void OnReset()
		{
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x040004C6 RID: 1222
		[Tooltip("The value to set the SharedObject to")]
		public SharedObject targetValue;

		// Token: 0x040004C7 RID: 1223
		[RequiredField]
		[Tooltip("The SharedTransform to set")]
		public SharedObject targetVariable;
	}
}

using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000154 RID: 340
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedGameObject variable to the specified object. Returns Success.")]
	public class SetSharedGameObject : Action
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x0001D2BC File Offset: 0x0001B4BC
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = ((this.targetValue.Value != null || this.valueCanBeNull.Value) ? this.targetValue.Value : this.gameObject);
			return TaskStatus.Success;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0001D308 File Offset: 0x0001B508
		public override void OnReset()
		{
			this.valueCanBeNull = false;
			this.targetValue = null;
			this.targetVariable = null;
		}

		// Token: 0x040004BF RID: 1215
		[Tooltip("The value to set the SharedGameObject to. If null the variable will be set to the current GameObject")]
		public SharedGameObject targetValue;

		// Token: 0x040004C0 RID: 1216
		[RequiredField]
		[Tooltip("The SharedGameObject to set")]
		public SharedGameObject targetVariable;

		// Token: 0x040004C1 RID: 1217
		[Tooltip("Can the target value be null?")]
		public SharedBool valueCanBeNull;
	}
}

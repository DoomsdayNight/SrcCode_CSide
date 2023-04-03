using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x0200013C RID: 316
	[TaskCategory("Unity/String")]
	[TaskDescription("Sets the variable string to the value string.")]
	public class SetString : Action
	{
		// Token: 0x0600087C RID: 2172 RVA: 0x0001C7C1 File Offset: 0x0001A9C1
		public override TaskStatus OnUpdate()
		{
			this.variable.Value = this.value.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001C7DA File Offset: 0x0001A9DA
		public override void OnReset()
		{
			this.variable = "";
			this.value = "";
		}

		// Token: 0x04000487 RID: 1159
		[Tooltip("The target string")]
		[RequiredField]
		public SharedString variable;

		// Token: 0x04000488 RID: 1160
		[Tooltip("The value string")]
		public SharedString value;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityDebug
{
	// Token: 0x02000235 RID: 565
	[TaskCategory("Unity/Debug")]
	[TaskDescription("Log a variable value.")]
	public class LogValue : Action
	{
		// Token: 0x06000BEB RID: 3051 RVA: 0x000245CD File Offset: 0x000227CD
		public override TaskStatus OnUpdate()
		{
			Debug.Log(this.variable.Value.value.GetValue());
			return TaskStatus.Success;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x000245EA File Offset: 0x000227EA
		public override void OnReset()
		{
			this.variable = null;
		}

		// Token: 0x040007B0 RID: 1968
		[Tooltip("The variable to output")]
		public SharedGenericVariable variable;
	}
}

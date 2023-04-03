using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001B6 RID: 438
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Sets the value with the specified key from the PlayerPrefs.")]
	public class SetFloat : Action
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x000201D2 File Offset: 0x0001E3D2
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.SetFloat(this.key.Value, this.value.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x000201F0 File Offset: 0x0001E3F0
		public override void OnReset()
		{
			this.key = "";
			this.value = 0f;
		}

		// Token: 0x0400060A RID: 1546
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x0400060B RID: 1547
		[Tooltip("The value to set")]
		public SharedFloat value;
	}
}

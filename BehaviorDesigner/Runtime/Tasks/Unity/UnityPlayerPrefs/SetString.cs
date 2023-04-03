using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001B8 RID: 440
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Sets the value with the specified key from the PlayerPrefs.")]
	public class SetString : Action
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x0002025E File Offset: 0x0001E45E
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.SetString(this.key.Value, this.value.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002027C File Offset: 0x0001E47C
		public override void OnReset()
		{
			this.key = "";
			this.value = "";
		}

		// Token: 0x0400060E RID: 1550
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x0400060F RID: 1551
		[Tooltip("The value to set")]
		public SharedString value;
	}
}

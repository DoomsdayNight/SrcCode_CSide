using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001B7 RID: 439
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Sets the value with the specified key from the PlayerPrefs.")]
	public class SetInt : Action
	{
		// Token: 0x06000A30 RID: 2608 RVA: 0x0002021A File Offset: 0x0001E41A
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.SetInt(this.key.Value, this.value.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00020238 File Offset: 0x0001E438
		public override void OnReset()
		{
			this.key = "";
			this.value = 0;
		}

		// Token: 0x0400060C RID: 1548
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x0400060D RID: 1549
		[Tooltip("The value to set")]
		public SharedInt value;
	}
}

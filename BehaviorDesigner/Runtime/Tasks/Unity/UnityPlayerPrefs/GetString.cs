using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001B3 RID: 435
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Stores the value with the specified key from the PlayerPrefs.")]
	public class GetString : Action
	{
		// Token: 0x06000A25 RID: 2597 RVA: 0x0002012E File Offset: 0x0001E32E
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = PlayerPrefs.GetString(this.key.Value, this.defaultValue.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00020157 File Offset: 0x0001E357
		public override void OnReset()
		{
			this.key = "";
			this.defaultValue = "";
			this.storeResult = "";
		}

		// Token: 0x04000606 RID: 1542
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04000607 RID: 1543
		[Tooltip("The default value")]
		public SharedString defaultValue;

		// Token: 0x04000608 RID: 1544
		[Tooltip("The value retrieved from the PlayerPrefs")]
		[RequiredField]
		public SharedString storeResult;
	}
}

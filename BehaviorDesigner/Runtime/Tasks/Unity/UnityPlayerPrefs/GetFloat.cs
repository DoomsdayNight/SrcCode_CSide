using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001B1 RID: 433
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Stores the value with the specified key from the PlayerPrefs.")]
	public class GetFloat : Action
	{
		// Token: 0x06000A1F RID: 2591 RVA: 0x00020070 File Offset: 0x0001E270
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = PlayerPrefs.GetFloat(this.key.Value, this.defaultValue.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00020099 File Offset: 0x0001E299
		public override void OnReset()
		{
			this.key = "";
			this.defaultValue = 0f;
			this.storeResult = 0f;
		}

		// Token: 0x04000600 RID: 1536
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04000601 RID: 1537
		[Tooltip("The default value")]
		public SharedFloat defaultValue;

		// Token: 0x04000602 RID: 1538
		[Tooltip("The value retrieved from the PlayerPrefs")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}

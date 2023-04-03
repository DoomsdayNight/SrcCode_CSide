using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001B2 RID: 434
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Stores the value with the specified key from the PlayerPrefs.")]
	public class GetInt : Action
	{
		// Token: 0x06000A22 RID: 2594 RVA: 0x000200D3 File Offset: 0x0001E2D3
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = PlayerPrefs.GetInt(this.key.Value, this.defaultValue.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000200FC File Offset: 0x0001E2FC
		public override void OnReset()
		{
			this.key = "";
			this.defaultValue = 0;
			this.storeResult = 0;
		}

		// Token: 0x04000603 RID: 1539
		[Tooltip("The key to store")]
		public SharedString key;

		// Token: 0x04000604 RID: 1540
		[Tooltip("The default value")]
		public SharedInt defaultValue;

		// Token: 0x04000605 RID: 1541
		[Tooltip("The value retrieved from the PlayerPrefs")]
		[RequiredField]
		public SharedInt storeResult;
	}
}

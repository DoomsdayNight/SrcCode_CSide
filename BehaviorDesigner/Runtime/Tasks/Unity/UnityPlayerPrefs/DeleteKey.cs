using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001B0 RID: 432
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Deletes the specified key from the PlayerPrefs.")]
	public class DeleteKey : Action
	{
		// Token: 0x06000A1C RID: 2588 RVA: 0x00020043 File Offset: 0x0001E243
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.DeleteKey(this.key.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00020056 File Offset: 0x0001E256
		public override void OnReset()
		{
			this.key = "";
		}

		// Token: 0x040005FF RID: 1535
		[Tooltip("The key to delete")]
		public SharedString key;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001AF RID: 431
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Deletes all entries from the PlayerPrefs.")]
	public class DeleteAll : Action
	{
		// Token: 0x06000A1A RID: 2586 RVA: 0x00020033 File Offset: 0x0001E233
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.DeleteAll();
			return TaskStatus.Success;
		}
	}
}

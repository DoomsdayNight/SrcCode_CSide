using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001B5 RID: 437
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Saves the PlayerPrefs.")]
	public class Save : Action
	{
		// Token: 0x06000A2B RID: 2603 RVA: 0x000201C2 File Offset: 0x0001E3C2
		public override TaskStatus OnUpdate()
		{
			PlayerPrefs.Save();
			return TaskStatus.Success;
		}
	}
}

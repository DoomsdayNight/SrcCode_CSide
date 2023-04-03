using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs
{
	// Token: 0x020001B4 RID: 436
	[TaskCategory("Unity/PlayerPrefs")]
	[TaskDescription("Retruns success if the specified key exists.")]
	public class HasKey : Conditional
	{
		// Token: 0x06000A28 RID: 2600 RVA: 0x00020191 File Offset: 0x0001E391
		public override TaskStatus OnUpdate()
		{
			if (!PlayerPrefs.HasKey(this.key.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000201A8 File Offset: 0x0001E3A8
		public override void OnReset()
		{
			this.key = "";
		}

		// Token: 0x04000609 RID: 1545
		[Tooltip("The key to check")]
		public SharedString key;
	}
}

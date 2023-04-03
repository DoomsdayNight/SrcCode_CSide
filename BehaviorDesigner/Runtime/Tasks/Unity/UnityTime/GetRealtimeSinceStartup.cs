using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTime
{
	// Token: 0x02000130 RID: 304
	[TaskCategory("Unity/Time")]
	[TaskDescription("Returns the real time in seconds since the game started.")]
	public class GetRealtimeSinceStartup : Action
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x0001C37A File Offset: 0x0001A57A
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.realtimeSinceStartup;
			return TaskStatus.Success;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0001C38D File Offset: 0x0001A58D
		public override void OnReset()
		{
			this.storeResult = 0f;
		}

		// Token: 0x0400046D RID: 1133
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}

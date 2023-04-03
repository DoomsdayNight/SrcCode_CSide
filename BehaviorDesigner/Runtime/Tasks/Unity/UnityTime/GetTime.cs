using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTime
{
	// Token: 0x02000131 RID: 305
	[TaskCategory("Unity/Time")]
	[TaskDescription("Returns the time in second since the start of the game.")]
	public class GetTime : Action
	{
		// Token: 0x0600085A RID: 2138 RVA: 0x0001C3A7 File Offset: 0x0001A5A7
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.time;
			return TaskStatus.Success;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0001C3BA File Offset: 0x0001A5BA
		public override void OnReset()
		{
			this.storeResult = 0f;
		}

		// Token: 0x0400046E RID: 1134
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}

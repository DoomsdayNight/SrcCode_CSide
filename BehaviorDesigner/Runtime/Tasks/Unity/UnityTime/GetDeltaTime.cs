using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTime
{
	// Token: 0x0200012F RID: 303
	[TaskCategory("Unity/Time")]
	[TaskDescription("Returns the time in seconds it took to complete the last frame.")]
	public class GetDeltaTime : Action
	{
		// Token: 0x06000854 RID: 2132 RVA: 0x0001C34D File Offset: 0x0001A54D
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.deltaTime;
			return TaskStatus.Success;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001C360 File Offset: 0x0001A560
		public override void OnReset()
		{
			this.storeResult = 0f;
		}

		// Token: 0x0400046C RID: 1132
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}

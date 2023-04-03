using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTime
{
	// Token: 0x02000132 RID: 306
	[TaskCategory("Unity/Time")]
	[TaskDescription("Returns the scale at which time is passing.")]
	public class GetTimeScale : Action
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x0001C3D4 File Offset: 0x0001A5D4
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Time.timeScale;
			return TaskStatus.Success;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0001C3E7 File Offset: 0x0001A5E7
		public override void OnReset()
		{
			this.storeResult = 0f;
		}

		// Token: 0x0400046F RID: 1135
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}

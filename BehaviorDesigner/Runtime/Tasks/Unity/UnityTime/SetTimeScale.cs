using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTime
{
	// Token: 0x02000133 RID: 307
	[TaskCategory("Unity/Time")]
	[TaskDescription("Sets the scale at which time is passing.")]
	public class SetTimeScale : Action
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x0001C401 File Offset: 0x0001A601
		public override TaskStatus OnUpdate()
		{
			Time.timeScale = this.timeScale.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001C414 File Offset: 0x0001A614
		public override void OnReset()
		{
			this.timeScale.Value = 0f;
		}

		// Token: 0x04000470 RID: 1136
		[Tooltip("The timescale")]
		public SharedFloat timeScale;
	}
}

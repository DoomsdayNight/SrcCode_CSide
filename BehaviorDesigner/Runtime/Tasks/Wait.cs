using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000C4 RID: 196
	[TaskDescription("Wait a specified amount of time. The task will return running until the task is done waiting. It will return success after the wait time has elapsed.")]
	[TaskIcon("{SkinColor}WaitIcon.png")]
	public class Wait : Action
	{
		// Token: 0x06000669 RID: 1641 RVA: 0x00017C3C File Offset: 0x00015E3C
		public override void OnStart()
		{
			this.startTime = Time.time;
			if (this.randomWait.Value)
			{
				this.waitDuration = UnityEngine.Random.Range(this.randomWaitMin.Value, this.randomWaitMax.Value);
				return;
			}
			this.waitDuration = this.waitTime.Value;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00017C94 File Offset: 0x00015E94
		public override TaskStatus OnUpdate()
		{
			if (this.startTime + this.waitDuration < Time.time)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00017CAD File Offset: 0x00015EAD
		public override void OnPause(bool paused)
		{
			if (paused)
			{
				this.pauseTime = Time.time;
				return;
			}
			this.startTime += Time.time - this.pauseTime;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00017CD7 File Offset: 0x00015ED7
		public override void OnReset()
		{
			this.waitTime = 1f;
			this.randomWait = false;
			this.randomWaitMin = 1f;
			this.randomWaitMax = 1f;
		}

		// Token: 0x04000302 RID: 770
		[Tooltip("The amount of time to wait")]
		public SharedFloat waitTime = 1f;

		// Token: 0x04000303 RID: 771
		[Tooltip("Should the wait be randomized?")]
		public SharedBool randomWait = false;

		// Token: 0x04000304 RID: 772
		[Tooltip("The minimum wait time if random wait is enabled")]
		public SharedFloat randomWaitMin = 1f;

		// Token: 0x04000305 RID: 773
		[Tooltip("The maximum wait time if random wait is enabled")]
		public SharedFloat randomWaitMax = 1f;

		// Token: 0x04000306 RID: 774
		private float waitDuration;

		// Token: 0x04000307 RID: 775
		private float startTime;

		// Token: 0x04000308 RID: 776
		private float pauseTime;
	}
}

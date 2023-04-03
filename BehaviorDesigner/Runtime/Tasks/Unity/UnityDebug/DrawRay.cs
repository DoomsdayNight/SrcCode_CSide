using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityDebug
{
	// Token: 0x02000233 RID: 563
	[TaskCategory("Unity/Debug")]
	[TaskDescription("Draws a debug ray")]
	public class DrawRay : Action
	{
		// Token: 0x06000BE3 RID: 3043 RVA: 0x000243C5 File Offset: 0x000225C5
		public override TaskStatus OnUpdate()
		{
			Debug.DrawRay(this.start.Value, this.direction.Value, this.color.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000243EE File Offset: 0x000225EE
		public override void OnReset()
		{
			this.start = Vector3.zero;
			this.direction = Vector3.zero;
			this.color = Color.white;
		}

		// Token: 0x040007A7 RID: 1959
		[Tooltip("The position")]
		public SharedVector3 start;

		// Token: 0x040007A8 RID: 1960
		[Tooltip("The direction")]
		public SharedVector3 direction;

		// Token: 0x040007A9 RID: 1961
		[Tooltip("The color")]
		public SharedColor color = Color.white;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityDebug
{
	// Token: 0x02000232 RID: 562
	[TaskCategory("Unity/Debug")]
	[TaskDescription("Draws a debug line")]
	public class DrawLine : Action
	{
		// Token: 0x06000BE0 RID: 3040 RVA: 0x00024307 File Offset: 0x00022507
		public override TaskStatus OnUpdate()
		{
			Debug.DrawLine(this.start.Value, this.end.Value, this.color.Value, this.duration.Value, this.depthTest.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00024348 File Offset: 0x00022548
		public override void OnReset()
		{
			this.start = Vector3.zero;
			this.end = Vector3.zero;
			this.color = Color.white;
			this.duration = 0f;
			this.depthTest = true;
		}

		// Token: 0x040007A2 RID: 1954
		[Tooltip("The start position")]
		public SharedVector3 start;

		// Token: 0x040007A3 RID: 1955
		[Tooltip("The end position")]
		public SharedVector3 end;

		// Token: 0x040007A4 RID: 1956
		[Tooltip("The color")]
		public SharedColor color = Color.white;

		// Token: 0x040007A5 RID: 1957
		[Tooltip("Duration the line will be visible for in seconds.\nDefault: 0 means 1 frame.")]
		public SharedFloat duration;

		// Token: 0x040007A6 RID: 1958
		[Tooltip("Whether the line should show through world geometry.")]
		public SharedBool depthTest = true;
	}
}

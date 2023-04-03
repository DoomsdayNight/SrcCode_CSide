using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020000FB RID: 251
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Returns the distance between two Vector2s.")]
	public class Distance : Action
	{
		// Token: 0x06000793 RID: 1939 RVA: 0x0001A5CA File Offset: 0x000187CA
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.Distance(this.firstVector2.Value, this.secondVector2.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001A5F3 File Offset: 0x000187F3
		public override void OnReset()
		{
			this.firstVector2 = Vector2.zero;
			this.secondVector2 = Vector2.zero;
			this.storeResult = 0f;
		}

		// Token: 0x040003A7 RID: 935
		[Tooltip("The first Vector2")]
		public SharedVector2 firstVector2;

		// Token: 0x040003A8 RID: 936
		[Tooltip("The second Vector2")]
		public SharedVector2 secondVector2;

		// Token: 0x040003A9 RID: 937
		[Tooltip("The distance")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}

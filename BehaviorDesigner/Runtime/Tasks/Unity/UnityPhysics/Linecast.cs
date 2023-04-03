using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPhysics
{
	// Token: 0x020001BC RID: 444
	[TaskCategory("Unity/Physics")]
	[TaskDescription("Returns success if there is any collider intersecting the line between start and end")]
	public class Linecast : Action
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x0002067E File Offset: 0x0001E87E
		public override TaskStatus OnUpdate()
		{
			if (!Physics.Linecast(this.startPosition.Value, this.endPosition.Value, this.layerMask))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000206AB File Offset: 0x0001E8AB
		public override void OnReset()
		{
			this.startPosition = Vector3.zero;
			this.endPosition = Vector3.zero;
			this.layerMask = -1;
		}

		// Token: 0x04000628 RID: 1576
		[Tooltip("The starting position of the linecast")]
		public SharedVector3 startPosition;

		// Token: 0x04000629 RID: 1577
		[Tooltip("The ending position of the linecast")]
		public SharedVector3 endPosition;

		// Token: 0x0400062A RID: 1578
		[Tooltip("Selectively ignore colliders.")]
		public LayerMask layerMask = -1;
	}
}

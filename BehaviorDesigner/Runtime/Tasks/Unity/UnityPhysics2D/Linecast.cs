using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPhysics2D
{
	// Token: 0x020001BA RID: 442
	[TaskCategory("Unity/Physics2D")]
	[TaskDescription("Returns success if there is any collider intersecting the line between start and end")]
	public class Linecast : Action
	{
		// Token: 0x06000A39 RID: 2617 RVA: 0x00020466 File Offset: 0x0001E666
		public override TaskStatus OnUpdate()
		{
			if (!Physics2D.Linecast(this.startPosition.Value, this.endPosition.Value, this.layerMask))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00020498 File Offset: 0x0001E698
		public override void OnReset()
		{
			this.startPosition = Vector2.zero;
			this.endPosition = Vector2.zero;
			this.layerMask = -1;
		}

		// Token: 0x0400061B RID: 1563
		[Tooltip("The starting position of the linecast.")]
		public SharedVector2 startPosition;

		// Token: 0x0400061C RID: 1564
		[Tooltip("The ending position of the linecast.")]
		public SharedVector2 endPosition;

		// Token: 0x0400061D RID: 1565
		[Tooltip("Selectively ignore colliders.")]
		public LayerMask layerMask = -1;
	}
}

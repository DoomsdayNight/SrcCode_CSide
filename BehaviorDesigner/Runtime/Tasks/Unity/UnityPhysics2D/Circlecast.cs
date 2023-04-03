using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPhysics2D
{
	// Token: 0x020001B9 RID: 441
	[TaskCategory("Unity/Physics2D")]
	[TaskDescription("Casts a circle against all colliders in the scene. Returns success if a collider was hit.")]
	public class Circlecast : Action
	{
		// Token: 0x06000A36 RID: 2614 RVA: 0x000202A8 File Offset: 0x0001E4A8
		public override TaskStatus OnUpdate()
		{
			Vector2 vector = this.direction.Value;
			Vector2 origin;
			if (this.originGameObject.Value != null)
			{
				origin = this.originGameObject.Value.transform.position;
				if (this.space == Space.Self)
				{
					vector = this.originGameObject.Value.transform.TransformDirection(this.direction.Value);
				}
			}
			else
			{
				origin = this.originPosition.Value;
			}
			RaycastHit2D raycastHit2D = Physics2D.CircleCast(origin, this.radius.Value, vector, (this.distance.Value == -1f) ? float.PositiveInfinity : this.distance.Value, this.layerMask);
			if (raycastHit2D.collider != null)
			{
				this.storeHitObject.Value = raycastHit2D.collider.gameObject;
				this.storeHitPoint.Value = raycastHit2D.point;
				this.storeHitNormal.Value = raycastHit2D.normal;
				this.storeHitDistance.Value = raycastHit2D.distance;
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000203D4 File Offset: 0x0001E5D4
		public override void OnReset()
		{
			this.originGameObject = null;
			this.originPosition = Vector2.zero;
			this.direction = Vector2.zero;
			this.radius = 0f;
			this.distance = -1f;
			this.layerMask = -1;
			this.space = Space.Self;
		}

		// Token: 0x04000610 RID: 1552
		[Tooltip("Starts the circlecast at the GameObject's position. If null the originPosition will be used.")]
		public SharedGameObject originGameObject;

		// Token: 0x04000611 RID: 1553
		[Tooltip("Starts the circlecast at the position. Only used if originGameObject is null.")]
		public SharedVector2 originPosition;

		// Token: 0x04000612 RID: 1554
		[Tooltip("The radius of the circlecast")]
		public SharedFloat radius;

		// Token: 0x04000613 RID: 1555
		[Tooltip("The direction of the circlecast")]
		public SharedVector2 direction;

		// Token: 0x04000614 RID: 1556
		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public SharedFloat distance = -1f;

		// Token: 0x04000615 RID: 1557
		[Tooltip("Selectively ignore colliders.")]
		public LayerMask layerMask = -1;

		// Token: 0x04000616 RID: 1558
		[Tooltip("Use world or local space. The direction is in world space if no GameObject is specified.")]
		public Space space = Space.Self;

		// Token: 0x04000617 RID: 1559
		[SharedRequired]
		[Tooltip("Stores the hit object of the circlecast.")]
		public SharedGameObject storeHitObject;

		// Token: 0x04000618 RID: 1560
		[SharedRequired]
		[Tooltip("Stores the hit point of the circlecast.")]
		public SharedVector2 storeHitPoint;

		// Token: 0x04000619 RID: 1561
		[SharedRequired]
		[Tooltip("Stores the hit normal of the circlecast.")]
		public SharedVector2 storeHitNormal;

		// Token: 0x0400061A RID: 1562
		[SharedRequired]
		[Tooltip("Stores the hit distance of the circlecast.")]
		public SharedFloat storeHitDistance;
	}
}

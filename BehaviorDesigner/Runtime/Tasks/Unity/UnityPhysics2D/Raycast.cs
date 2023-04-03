using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPhysics2D
{
	// Token: 0x020001BB RID: 443
	[TaskCategory("Unity/Physics2D")]
	[TaskDescription("Casts a ray against all colliders in the scene. Returns success if a collider was hit.")]
	public class Raycast : Action
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x000204DC File Offset: 0x0001E6DC
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
			RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, vector, (this.distance.Value == -1f) ? float.PositiveInfinity : this.distance.Value, this.layerMask);
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

		// Token: 0x06000A3D RID: 2621 RVA: 0x000205FC File Offset: 0x0001E7FC
		public override void OnReset()
		{
			this.originGameObject = null;
			this.originPosition = Vector2.zero;
			this.direction = Vector2.zero;
			this.distance = -1f;
			this.layerMask = -1;
			this.space = Space.Self;
		}

		// Token: 0x0400061E RID: 1566
		[Tooltip("Starts the ray at the GameObject's position. If null the originPosition will be used.")]
		public SharedGameObject originGameObject;

		// Token: 0x0400061F RID: 1567
		[Tooltip("Starts the ray at the position. Only used if originGameObject is null.")]
		public SharedVector2 originPosition;

		// Token: 0x04000620 RID: 1568
		[Tooltip("The direction of the ray")]
		public SharedVector2 direction;

		// Token: 0x04000621 RID: 1569
		[Tooltip("The length of the ray. Set to -1 for infinity.")]
		public SharedFloat distance = -1f;

		// Token: 0x04000622 RID: 1570
		[Tooltip("Selectively ignore colliders.")]
		public LayerMask layerMask = -1;

		// Token: 0x04000623 RID: 1571
		[Tooltip("Cast the ray in world or local space. The direction is in world space if no GameObject is specified.")]
		public Space space = Space.Self;

		// Token: 0x04000624 RID: 1572
		[SharedRequired]
		[Tooltip("Stores the hit object of the raycast.")]
		public SharedGameObject storeHitObject;

		// Token: 0x04000625 RID: 1573
		[SharedRequired]
		[Tooltip("Stores the hit point of the raycast.")]
		public SharedVector2 storeHitPoint;

		// Token: 0x04000626 RID: 1574
		[SharedRequired]
		[Tooltip("Stores the hit normal of the raycast.")]
		public SharedVector2 storeHitNormal;

		// Token: 0x04000627 RID: 1575
		[SharedRequired]
		[Tooltip("Stores the hit distance of the raycast.")]
		public SharedFloat storeHitDistance;
	}
}

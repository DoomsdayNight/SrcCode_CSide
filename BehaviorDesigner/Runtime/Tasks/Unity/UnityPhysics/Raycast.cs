using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPhysics
{
	// Token: 0x020001BD RID: 445
	[TaskCategory("Unity/Physics")]
	[TaskDescription("Casts a ray against all colliders in the scene. Returns success if a collider was hit.")]
	public class Raycast : Action
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x000206F0 File Offset: 0x0001E8F0
		public override TaskStatus OnUpdate()
		{
			Vector3 vector = this.direction.Value;
			Vector3 origin;
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
			RaycastHit raycastHit;
			if (Physics.Raycast(origin, vector, out raycastHit, (this.distance.Value == -1f) ? float.PositiveInfinity : this.distance.Value, this.layerMask))
			{
				this.storeHitObject.Value = raycastHit.collider.gameObject;
				this.storeHitPoint.Value = raycastHit.point;
				this.storeHitNormal.Value = raycastHit.normal;
				this.storeHitDistance.Value = raycastHit.distance;
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000207F4 File Offset: 0x0001E9F4
		public override void OnReset()
		{
			this.originGameObject = null;
			this.originPosition = Vector3.zero;
			this.direction = Vector3.zero;
			this.distance = -1f;
			this.layerMask = -1;
			this.space = Space.Self;
		}

		// Token: 0x0400062B RID: 1579
		[Tooltip("Starts the ray at the GameObject's position. If null the originPosition will be used")]
		public SharedGameObject originGameObject;

		// Token: 0x0400062C RID: 1580
		[Tooltip("Starts the ray at the position. Only used if originGameObject is null")]
		public SharedVector3 originPosition;

		// Token: 0x0400062D RID: 1581
		[Tooltip("The direction of the ray")]
		public SharedVector3 direction;

		// Token: 0x0400062E RID: 1582
		[Tooltip("The length of the ray. Set to -1 for infinity")]
		public SharedFloat distance = -1f;

		// Token: 0x0400062F RID: 1583
		[Tooltip("Selectively ignore colliders")]
		public LayerMask layerMask = -1;

		// Token: 0x04000630 RID: 1584
		[Tooltip("Cast the ray in world or local space. The direction is in world space if no GameObject is specified")]
		public Space space = Space.Self;

		// Token: 0x04000631 RID: 1585
		[SharedRequired]
		[Tooltip("Stores the hit object of the raycast")]
		public SharedGameObject storeHitObject;

		// Token: 0x04000632 RID: 1586
		[SharedRequired]
		[Tooltip("Stores the hit point of the raycast")]
		public SharedVector3 storeHitPoint;

		// Token: 0x04000633 RID: 1587
		[SharedRequired]
		[Tooltip("Stores the hit normal of the raycast")]
		public SharedVector3 storeHitNormal;

		// Token: 0x04000634 RID: 1588
		[SharedRequired]
		[Tooltip("Stores the hit distance of the raycast")]
		public SharedFloat storeHitDistance;
	}
}

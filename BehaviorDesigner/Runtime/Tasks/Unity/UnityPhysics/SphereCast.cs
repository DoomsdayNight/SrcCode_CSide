using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityPhysics
{
	// Token: 0x020001BE RID: 446
	[TaskCategory("Unity/Physics")]
	[TaskDescription("Casts a sphere against all colliders in the scene. Returns success if a collider was hit.")]
	public class SphereCast : Action
	{
		// Token: 0x06000A45 RID: 2629 RVA: 0x00020878 File Offset: 0x0001EA78
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
			if (Physics.SphereCast(origin, this.radius.Value, vector, out raycastHit, (this.distance.Value == -1f) ? float.PositiveInfinity : this.distance.Value, this.layerMask))
			{
				this.storeHitObject.Value = raycastHit.collider.gameObject;
				this.storeHitPoint.Value = raycastHit.point;
				this.storeHitNormal.Value = raycastHit.normal;
				this.storeHitDistance.Value = raycastHit.distance;
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00020988 File Offset: 0x0001EB88
		public override void OnReset()
		{
			this.originGameObject = null;
			this.originPosition = Vector3.zero;
			this.radius = 0f;
			this.direction = Vector3.zero;
			this.distance = -1f;
			this.layerMask = -1;
			this.space = Space.Self;
		}

		// Token: 0x04000635 RID: 1589
		[Tooltip("Starts the spherecast at the GameObject's position. If null the originPosition will be used")]
		public SharedGameObject originGameObject;

		// Token: 0x04000636 RID: 1590
		[Tooltip("Starts the sherecast at the position. Only used if originGameObject is null")]
		public SharedVector3 originPosition;

		// Token: 0x04000637 RID: 1591
		[Tooltip("The radius of the spherecast")]
		public SharedFloat radius;

		// Token: 0x04000638 RID: 1592
		[Tooltip("The direction of the spherecast")]
		public SharedVector3 direction;

		// Token: 0x04000639 RID: 1593
		[Tooltip("The length of the spherecast. Set to -1 for infinity")]
		public SharedFloat distance = -1f;

		// Token: 0x0400063A RID: 1594
		[Tooltip("Selectively ignore colliders")]
		public LayerMask layerMask = -1;

		// Token: 0x0400063B RID: 1595
		[Tooltip("Use world or local space. The direction is in world space if no GameObject is specified")]
		public Space space = Space.Self;

		// Token: 0x0400063C RID: 1596
		[SharedRequired]
		[Tooltip("Stores the hit object of the spherecast")]
		public SharedGameObject storeHitObject;

		// Token: 0x0400063D RID: 1597
		[SharedRequired]
		[Tooltip("Stores the hit point of the spherecast")]
		public SharedVector3 storeHitPoint;

		// Token: 0x0400063E RID: 1598
		[SharedRequired]
		[Tooltip("Stores the hit normal of the spherecast")]
		public SharedVector3 storeHitNormal;

		// Token: 0x0400063F RID: 1599
		[SharedRequired]
		[Tooltip("Stores the hit distance of the spherecast")]
		public SharedFloat storeHitDistance;
	}
}

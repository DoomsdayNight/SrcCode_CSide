using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200017E RID: 382
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Applies a force to the rigidbody that simulates explosion effects. Returns Success.")]
	public class AddExplosionForce : Action
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x0001E600 File Offset: 0x0001C800
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0001E640 File Offset: 0x0001C840
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.AddExplosionForce(this.explosionForce.Value, this.explosionPosition.Value, this.explosionRadius.Value, this.upwardsModifier, this.forceMode);
			return TaskStatus.Success;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001E6A0 File Offset: 0x0001C8A0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.explosionForce = 0f;
			this.explosionPosition = Vector3.zero;
			this.explosionRadius = 0f;
			this.upwardsModifier = 0f;
			this.forceMode = ForceMode.Force;
		}

		// Token: 0x04000543 RID: 1347
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000544 RID: 1348
		[Tooltip("The force of the explosion")]
		public SharedFloat explosionForce;

		// Token: 0x04000545 RID: 1349
		[Tooltip("The position of the explosion")]
		public SharedVector3 explosionPosition;

		// Token: 0x04000546 RID: 1350
		[Tooltip("The radius of the explosion")]
		public SharedFloat explosionRadius;

		// Token: 0x04000547 RID: 1351
		[Tooltip("Applies the force as if it was applied from beneath the object")]
		public float upwardsModifier;

		// Token: 0x04000548 RID: 1352
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x04000549 RID: 1353
		private Rigidbody rigidbody;

		// Token: 0x0400054A RID: 1354
		private GameObject prevGameObject;
	}
}

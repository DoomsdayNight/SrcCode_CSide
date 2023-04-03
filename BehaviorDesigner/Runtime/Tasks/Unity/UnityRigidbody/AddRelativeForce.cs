using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000181 RID: 385
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Applies a force to the rigidbody relative to its coordinate system. Returns Success.")]
	public class AddRelativeForce : Action
	{
		// Token: 0x0600096D RID: 2413 RVA: 0x0001E874 File Offset: 0x0001CA74
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0001E8B4 File Offset: 0x0001CAB4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.AddRelativeForce(this.force.Value, this.forceMode);
			return TaskStatus.Success;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0001E8ED File Offset: 0x0001CAED
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector3.zero;
			this.forceMode = ForceMode.Force;
		}

		// Token: 0x04000556 RID: 1366
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000557 RID: 1367
		[Tooltip("The amount of force to apply")]
		public SharedVector3 force;

		// Token: 0x04000558 RID: 1368
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x04000559 RID: 1369
		private Rigidbody rigidbody;

		// Token: 0x0400055A RID: 1370
		private GameObject prevGameObject;
	}
}

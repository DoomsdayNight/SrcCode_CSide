using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200018E RID: 398
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the velocity of the Rigidbody. Returns Success.")]
	public class GetVelocity : Action
	{
		// Token: 0x060009A1 RID: 2465 RVA: 0x0001F000 File Offset: 0x0001D200
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001F040 File Offset: 0x0001D240
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.velocity;
			return TaskStatus.Success;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0001F073 File Offset: 0x0001D273
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x0400058D RID: 1421
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400058E RID: 1422
		[Tooltip("The velocity of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x0400058F RID: 1423
		private Rigidbody rigidbody;

		// Token: 0x04000590 RID: 1424
		private GameObject prevGameObject;
	}
}

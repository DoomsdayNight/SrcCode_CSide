using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000195 RID: 405
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the center of mass of the Rigidbody. Returns Success.")]
	public class SetCenterOfMass : Action
	{
		// Token: 0x060009BD RID: 2493 RVA: 0x0001F3E4 File Offset: 0x0001D5E4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0001F424 File Offset: 0x0001D624
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.centerOfMass = this.centerOfMass.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0001F457 File Offset: 0x0001D657
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.centerOfMass = Vector3.zero;
		}

		// Token: 0x040005A7 RID: 1447
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005A8 RID: 1448
		[Tooltip("The center of mass of the Rigidbody")]
		public SharedVector3 centerOfMass;

		// Token: 0x040005A9 RID: 1449
		private Rigidbody rigidbody;

		// Token: 0x040005AA RID: 1450
		private GameObject prevGameObject;
	}
}

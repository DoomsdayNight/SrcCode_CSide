using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000194 RID: 404
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the angular velocity of the Rigidbody. Returns Success.")]
	public class SetAngularVelocity : Action
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x0001F350 File Offset: 0x0001D550
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0001F390 File Offset: 0x0001D590
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.angularVelocity = this.angularVelocity.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0001F3C3 File Offset: 0x0001D5C3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularVelocity = Vector3.zero;
		}

		// Token: 0x040005A3 RID: 1443
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005A4 RID: 1444
		[Tooltip("The angular velocity of the Rigidbody")]
		public SharedVector3 angularVelocity;

		// Token: 0x040005A5 RID: 1445
		private Rigidbody rigidbody;

		// Token: 0x040005A6 RID: 1446
		private GameObject prevGameObject;
	}
}

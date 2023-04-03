using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000199 RID: 409
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the is kinematic value of the Rigidbody. Returns Success.")]
	public class SetIsKinematic : Action
	{
		// Token: 0x060009CD RID: 2509 RVA: 0x0001F624 File Offset: 0x0001D824
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0001F664 File Offset: 0x0001D864
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.isKinematic = this.isKinematic.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0001F697 File Offset: 0x0001D897
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.isKinematic = false;
		}

		// Token: 0x040005B7 RID: 1463
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005B8 RID: 1464
		[Tooltip("The is kinematic value of the Rigidbody")]
		public SharedBool isKinematic;

		// Token: 0x040005B9 RID: 1465
		private Rigidbody rigidbody;

		// Token: 0x040005BA RID: 1466
		private GameObject prevGameObject;
	}
}

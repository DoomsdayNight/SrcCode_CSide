using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000179 RID: 377
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Sets the is kinematic value of the Rigidbody2D. Returns Success.")]
	public class SetIsKinematic : Action
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x0001E350 File Offset: 0x0001C550
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001E390 File Offset: 0x0001C590
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.rigidbody2D.isKinematic = this.isKinematic.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001E3C3 File Offset: 0x0001C5C3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.isKinematic = false;
		}

		// Token: 0x04000531 RID: 1329
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000532 RID: 1330
		[Tooltip("The is kinematic value of the Rigidbody2D")]
		public SharedBool isKinematic;

		// Token: 0x04000533 RID: 1331
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000534 RID: 1332
		private GameObject prevGameObject;
	}
}

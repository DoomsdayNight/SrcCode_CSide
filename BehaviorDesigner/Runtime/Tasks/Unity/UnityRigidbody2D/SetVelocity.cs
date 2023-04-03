using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200017B RID: 379
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Sets the velocity of the Rigidbody2D. Returns Success.")]
	public class SetVelocity : Action
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x0001E474 File Offset: 0x0001C674
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001E4B4 File Offset: 0x0001C6B4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.rigidbody2D.velocity = this.velocity.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001E4E7 File Offset: 0x0001C6E7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.velocity = Vector2.zero;
		}

		// Token: 0x04000539 RID: 1337
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400053A RID: 1338
		[Tooltip("The velocity of the Rigidbody2D")]
		public SharedVector2 velocity;

		// Token: 0x0400053B RID: 1339
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400053C RID: 1340
		private GameObject prevGameObject;
	}
}

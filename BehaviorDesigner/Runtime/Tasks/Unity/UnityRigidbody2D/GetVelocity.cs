using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000170 RID: 368
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the velocity of the Rigidbody2D. Returns Success.")]
	public class GetVelocity : Action
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x0001DE44 File Offset: 0x0001C044
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0001DE84 File Offset: 0x0001C084
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.velocity;
			return TaskStatus.Success;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0001DEB7 File Offset: 0x0001C0B7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector2.zero;
		}

		// Token: 0x0400050F RID: 1295
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000510 RID: 1296
		[Tooltip("The velocity of the Rigidbody2D")]
		[RequiredField]
		public SharedVector2 storeValue;

		// Token: 0x04000511 RID: 1297
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000512 RID: 1298
		private GameObject prevGameObject;
	}
}

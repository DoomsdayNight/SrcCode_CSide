using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000178 RID: 376
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Sets the gravity scale of the Rigidbody2D. Returns Success.")]
	public class SetGravityScale : Action
	{
		// Token: 0x06000949 RID: 2377 RVA: 0x0001E2BC File Offset: 0x0001C4BC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0001E2FC File Offset: 0x0001C4FC
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.rigidbody2D.gravityScale = this.gravityScale.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001E32F File Offset: 0x0001C52F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.gravityScale = 0f;
		}

		// Token: 0x0400052D RID: 1325
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400052E RID: 1326
		[Tooltip("The gravity scale of the Rigidbody2D")]
		public SharedFloat gravityScale;

		// Token: 0x0400052F RID: 1327
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000530 RID: 1328
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000174 RID: 372
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Rotates the Rigidbody2D to the specified rotation. Returns Success.")]
	public class MoveRotation : Action
	{
		// Token: 0x06000939 RID: 2361 RVA: 0x0001E06C File Offset: 0x0001C26C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0001E0AC File Offset: 0x0001C2AC
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody2D.MoveRotation(this.rotation.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0001E0DF File Offset: 0x0001C2DF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = 0f;
		}

		// Token: 0x0400051D RID: 1309
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400051E RID: 1310
		[Tooltip("The new rotation of the Rigidbody")]
		public SharedFloat rotation;

		// Token: 0x0400051F RID: 1311
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000520 RID: 1312
		private GameObject prevGameObject;
	}
}

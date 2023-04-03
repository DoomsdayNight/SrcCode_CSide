using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000173 RID: 371
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Moves the Rigidbody2D to the specified position. Returns Success.")]
	public class MovePosition : Action
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x0001DFD8 File Offset: 0x0001C1D8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0001E018 File Offset: 0x0001C218
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody2D.MovePosition(this.position.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0001E04B File Offset: 0x0001C24B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector2.zero;
		}

		// Token: 0x04000519 RID: 1305
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400051A RID: 1306
		[Tooltip("The new position of the Rigidbody")]
		public SharedVector2 position;

		// Token: 0x0400051B RID: 1307
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400051C RID: 1308
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000177 RID: 375
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Sets the drag of the Rigidbody2D. Returns Success.")]
	public class SetDrag : Action
	{
		// Token: 0x06000945 RID: 2373 RVA: 0x0001E228 File Offset: 0x0001C428
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001E268 File Offset: 0x0001C468
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.rigidbody2D.drag = this.drag.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001E29B File Offset: 0x0001C49B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.drag = 0f;
		}

		// Token: 0x04000529 RID: 1321
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400052A RID: 1322
		[Tooltip("The drag of the Rigidbody2D")]
		public SharedFloat drag;

		// Token: 0x0400052B RID: 1323
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400052C RID: 1324
		private GameObject prevGameObject;
	}
}

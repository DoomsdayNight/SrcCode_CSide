using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200017C RID: 380
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Forces the Rigidbody2D to sleep at least one frame. Returns Success.")]
	public class Sleep : Conditional
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x0001E508 File Offset: 0x0001C708
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0001E548 File Offset: 0x0001C748
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.rigidbody2D.Sleep();
			return TaskStatus.Success;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0001E570 File Offset: 0x0001C770
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400053D RID: 1341
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400053E RID: 1342
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400053F RID: 1343
		private GameObject prevGameObject;
	}
}

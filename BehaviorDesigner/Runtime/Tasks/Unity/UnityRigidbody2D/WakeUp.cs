using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200017D RID: 381
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Forces the Rigidbody2D to wake up. Returns Success.")]
	public class WakeUp : Conditional
	{
		// Token: 0x0600095D RID: 2397 RVA: 0x0001E584 File Offset: 0x0001C784
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0001E5C4 File Offset: 0x0001C7C4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.rigidbody2D.WakeUp();
			return TaskStatus.Success;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001E5EC File Offset: 0x0001C7EC
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000540 RID: 1344
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000541 RID: 1345
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000542 RID: 1346
		private GameObject prevGameObject;
	}
}

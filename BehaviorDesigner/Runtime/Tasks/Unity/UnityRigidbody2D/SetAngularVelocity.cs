using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000176 RID: 374
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Sets the angular velocity of the Rigidbody2D. Returns Success.")]
	public class SetAngularVelocity : Action
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x0001E194 File Offset: 0x0001C394
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0001E1D4 File Offset: 0x0001C3D4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.rigidbody2D.angularVelocity = this.angularVelocity.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0001E207 File Offset: 0x0001C407
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularVelocity = 0f;
		}

		// Token: 0x04000525 RID: 1317
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000526 RID: 1318
		[Tooltip("The angular velocity of the Rigidbody2D")]
		public SharedFloat angularVelocity;

		// Token: 0x04000527 RID: 1319
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000528 RID: 1320
		private GameObject prevGameObject;
	}
}

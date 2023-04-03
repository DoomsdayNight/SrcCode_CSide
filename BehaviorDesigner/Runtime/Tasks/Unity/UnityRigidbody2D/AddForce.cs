using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000165 RID: 357
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Applies a force to the Rigidbody2D. Returns Success.")]
	public class AddForce : Action
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x0001D7D0 File Offset: 0x0001B9D0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0001D810 File Offset: 0x0001BA10
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.rigidbody2D.AddForce(this.force.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0001D843 File Offset: 0x0001BA43
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector2.zero;
		}

		// Token: 0x040004E2 RID: 1250
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004E3 RID: 1251
		[Tooltip("The amount of force to apply")]
		public SharedVector2 force;

		// Token: 0x040004E4 RID: 1252
		private Rigidbody2D rigidbody2D;

		// Token: 0x040004E5 RID: 1253
		private GameObject prevGameObject;
	}
}

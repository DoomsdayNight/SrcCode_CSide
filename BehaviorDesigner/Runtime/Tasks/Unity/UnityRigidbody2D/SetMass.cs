using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200017A RID: 378
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Sets the mass of the Rigidbody2D. Returns Success.")]
	public class SetMass : Action
	{
		// Token: 0x06000951 RID: 2385 RVA: 0x0001E3E0 File Offset: 0x0001C5E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001E420 File Offset: 0x0001C620
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.rigidbody2D.mass = this.mass.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001E453 File Offset: 0x0001C653
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.mass = 0f;
		}

		// Token: 0x04000535 RID: 1333
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000536 RID: 1334
		[Tooltip("The mass of the Rigidbody2D")]
		public SharedFloat mass;

		// Token: 0x04000537 RID: 1335
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000538 RID: 1336
		private GameObject prevGameObject;
	}
}

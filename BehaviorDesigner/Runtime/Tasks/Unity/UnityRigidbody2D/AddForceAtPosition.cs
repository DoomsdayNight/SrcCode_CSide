using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000166 RID: 358
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Applies a force at the specified position to the Rigidbody2D. Returns Success.")]
	public class AddForceAtPosition : Action
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x0001D864 File Offset: 0x0001BA64
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x0001D8A4 File Offset: 0x0001BAA4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.rigidbody2D.AddForceAtPosition(this.force.Value, this.position.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0001D8E2 File Offset: 0x0001BAE2
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector2.zero;
			this.position = Vector2.zero;
		}

		// Token: 0x040004E6 RID: 1254
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004E7 RID: 1255
		[Tooltip("The amount of force to apply")]
		public SharedVector2 force;

		// Token: 0x040004E8 RID: 1256
		[Tooltip("The position of the force")]
		public SharedVector2 position;

		// Token: 0x040004E9 RID: 1257
		private Rigidbody2D rigidbody2D;

		// Token: 0x040004EA RID: 1258
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200018F RID: 399
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Returns Success if the Rigidbody is kinematic, otherwise Failure.")]
	public class IsKinematic : Conditional
	{
		// Token: 0x060009A5 RID: 2469 RVA: 0x0001F094 File Offset: 0x0001D294
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0001F0D4 File Offset: 0x0001D2D4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			if (!this.rigidbody.isKinematic)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001F100 File Offset: 0x0001D300
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000591 RID: 1425
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000592 RID: 1426
		private Rigidbody rigidbody;

		// Token: 0x04000593 RID: 1427
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000190 RID: 400
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Returns Success if the Rigidbody is sleeping, otherwise Failure.")]
	public class IsSleeping : Conditional
	{
		// Token: 0x060009A9 RID: 2473 RVA: 0x0001F114 File Offset: 0x0001D314
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001F154 File Offset: 0x0001D354
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			if (!this.rigidbody.IsSleeping())
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001F180 File Offset: 0x0001D380
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000594 RID: 1428
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000595 RID: 1429
		private Rigidbody rigidbody;

		// Token: 0x04000596 RID: 1430
		private GameObject prevGameObject;
	}
}

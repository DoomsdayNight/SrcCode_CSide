using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x020001A0 RID: 416
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Returns Success if the Rigidbody is using gravity, otherwise Failure.")]
	public class UseGravity : Conditional
	{
		// Token: 0x060009E9 RID: 2537 RVA: 0x0001FA10 File Offset: 0x0001DC10
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0001FA50 File Offset: 0x0001DC50
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			if (!this.rigidbody.useGravity)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0001FA7C File Offset: 0x0001DC7C
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040005D2 RID: 1490
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005D3 RID: 1491
		private Rigidbody rigidbody;

		// Token: 0x040005D4 RID: 1492
		private GameObject prevGameObject;
	}
}

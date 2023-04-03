using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200019A RID: 410
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the mass of the Rigidbody. Returns Success.")]
	public class SetMass : Action
	{
		// Token: 0x060009D1 RID: 2513 RVA: 0x0001F6B4 File Offset: 0x0001D8B4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0001F6F4 File Offset: 0x0001D8F4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.mass = this.mass.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0001F727 File Offset: 0x0001D927
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.mass = 0f;
		}

		// Token: 0x040005BB RID: 1467
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005BC RID: 1468
		[Tooltip("The mass of the Rigidbody")]
		public SharedFloat mass;

		// Token: 0x040005BD RID: 1469
		private Rigidbody rigidbody;

		// Token: 0x040005BE RID: 1470
		private GameObject prevGameObject;
	}
}

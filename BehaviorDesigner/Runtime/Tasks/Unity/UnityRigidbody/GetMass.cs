using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200018A RID: 394
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the mass of the Rigidbody. Returns Success.")]
	public class GetMass : Action
	{
		// Token: 0x06000991 RID: 2449 RVA: 0x0001EDB4 File Offset: 0x0001CFB4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0001EDF4 File Offset: 0x0001CFF4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.mass;
			return TaskStatus.Success;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0001EE27 File Offset: 0x0001D027
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400057D RID: 1405
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400057E RID: 1406
		[Tooltip("The mass of the Rigidbody")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400057F RID: 1407
		private Rigidbody rigidbody;

		// Token: 0x04000580 RID: 1408
		private GameObject prevGameObject;
	}
}

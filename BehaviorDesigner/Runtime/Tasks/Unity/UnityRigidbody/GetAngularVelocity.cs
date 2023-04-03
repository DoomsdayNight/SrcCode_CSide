using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000185 RID: 389
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the angular velocity of the Rigidbody. Returns Success.")]
	public class GetAngularVelocity : Action
	{
		// Token: 0x0600097D RID: 2429 RVA: 0x0001EAD8 File Offset: 0x0001CCD8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0001EB18 File Offset: 0x0001CD18
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.angularVelocity;
			return TaskStatus.Success;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0001EB4B File Offset: 0x0001CD4B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000569 RID: 1385
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400056A RID: 1386
		[Tooltip("The angular velocity of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x0400056B RID: 1387
		private Rigidbody rigidbody;

		// Token: 0x0400056C RID: 1388
		private GameObject prevGameObject;
	}
}

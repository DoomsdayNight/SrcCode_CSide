using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200018D RID: 397
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the use gravity value of the Rigidbody. Returns Success.")]
	public class GetUseGravity : Action
	{
		// Token: 0x0600099D RID: 2461 RVA: 0x0001EF70 File Offset: 0x0001D170
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.useGravity;
			return TaskStatus.Success;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001EFE3 File Offset: 0x0001D1E3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04000589 RID: 1417
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400058A RID: 1418
		[Tooltip("The use gravity value of the Rigidbody")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x0400058B RID: 1419
		private Rigidbody rigidbody;

		// Token: 0x0400058C RID: 1420
		private GameObject prevGameObject;
	}
}

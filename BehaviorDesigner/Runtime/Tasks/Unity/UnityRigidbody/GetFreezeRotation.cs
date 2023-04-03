using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000188 RID: 392
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the freeze rotation value of the Rigidbody. Returns Success.")]
	public class GetFreezeRotation : Action
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x0001EC94 File Offset: 0x0001CE94
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0001ECD4 File Offset: 0x0001CED4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.freezeRotation;
			return TaskStatus.Success;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0001ED07 File Offset: 0x0001CF07
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04000575 RID: 1397
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000576 RID: 1398
		[Tooltip("The freeze rotation value of the Rigidbody")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04000577 RID: 1399
		private Rigidbody rigidbody;

		// Token: 0x04000578 RID: 1400
		private GameObject prevGameObject;
	}
}

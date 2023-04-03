using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200018B RID: 395
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the position of the Rigidbody. Returns Success.")]
	public class GetPosition : Action
	{
		// Token: 0x06000995 RID: 2453 RVA: 0x0001EE48 File Offset: 0x0001D048
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0001EE88 File Offset: 0x0001D088
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.position;
			return TaskStatus.Success;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001EEBB File Offset: 0x0001D0BB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000581 RID: 1409
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000582 RID: 1410
		[Tooltip("The position of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000583 RID: 1411
		private Rigidbody rigidbody;

		// Token: 0x04000584 RID: 1412
		private GameObject prevGameObject;
	}
}

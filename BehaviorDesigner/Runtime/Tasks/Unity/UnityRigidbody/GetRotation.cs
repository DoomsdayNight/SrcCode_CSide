using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200018C RID: 396
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the rotation of the Rigidbody. Returns Success.")]
	public class GetRotation : Action
	{
		// Token: 0x06000999 RID: 2457 RVA: 0x0001EEDC File Offset: 0x0001D0DC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001EF1C File Offset: 0x0001D11C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.rotation;
			return TaskStatus.Success;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001EF4F File Offset: 0x0001D14F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Quaternion.identity;
		}

		// Token: 0x04000585 RID: 1413
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000586 RID: 1414
		[Tooltip("The rotation of the Rigidbody")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x04000587 RID: 1415
		private Rigidbody rigidbody;

		// Token: 0x04000588 RID: 1416
		private GameObject prevGameObject;
	}
}

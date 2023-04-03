using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000192 RID: 402
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Rotates the Rigidbody to the specified rotation. Returns Success.")]
	public class MoveRotation : Action
	{
		// Token: 0x060009B1 RID: 2481 RVA: 0x0001F228 File Offset: 0x0001D428
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0001F268 File Offset: 0x0001D468
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.MoveRotation(this.rotation.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0001F29B File Offset: 0x0001D49B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x0400059B RID: 1435
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400059C RID: 1436
		[Tooltip("The new rotation of the Rigidbody")]
		public SharedQuaternion rotation;

		// Token: 0x0400059D RID: 1437
		private Rigidbody rigidbody;

		// Token: 0x0400059E RID: 1438
		private GameObject prevGameObject;
	}
}

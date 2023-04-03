using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000180 RID: 384
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Applies a force at the specified position to the rigidbody. Returns Success.")]
	public class AddForceAtPosition : Action
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x0001E7AC File Offset: 0x0001C9AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0001E7EC File Offset: 0x0001C9EC
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.AddForceAtPosition(this.force.Value, this.position.Value, this.forceMode);
			return TaskStatus.Success;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0001E83B File Offset: 0x0001CA3B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector3.zero;
			this.position = Vector3.zero;
			this.forceMode = ForceMode.Force;
		}

		// Token: 0x04000550 RID: 1360
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000551 RID: 1361
		[Tooltip("The amount of force to apply")]
		public SharedVector3 force;

		// Token: 0x04000552 RID: 1362
		[Tooltip("The position of the force")]
		public SharedVector3 position;

		// Token: 0x04000553 RID: 1363
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x04000554 RID: 1364
		private Rigidbody rigidbody;

		// Token: 0x04000555 RID: 1365
		private GameObject prevGameObject;
	}
}

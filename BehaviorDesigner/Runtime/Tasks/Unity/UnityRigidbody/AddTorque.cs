using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000183 RID: 387
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Applies a torque to the rigidbody. Returns Success.")]
	public class AddTorque : Action
	{
		// Token: 0x06000975 RID: 2421 RVA: 0x0001E9A0 File Offset: 0x0001CBA0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0001E9E0 File Offset: 0x0001CBE0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.AddTorque(this.torque.Value, this.forceMode);
			return TaskStatus.Success;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001EA19 File Offset: 0x0001CC19
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.torque = Vector3.zero;
			this.forceMode = ForceMode.Force;
		}

		// Token: 0x04000560 RID: 1376
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000561 RID: 1377
		[Tooltip("The amount of torque to apply")]
		public SharedVector3 torque;

		// Token: 0x04000562 RID: 1378
		[Tooltip("The type of torque")]
		public ForceMode forceMode;

		// Token: 0x04000563 RID: 1379
		private Rigidbody rigidbody;

		// Token: 0x04000564 RID: 1380
		private GameObject prevGameObject;
	}
}

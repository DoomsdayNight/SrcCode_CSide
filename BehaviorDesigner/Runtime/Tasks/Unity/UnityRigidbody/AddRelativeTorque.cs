using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000182 RID: 386
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Applies a torque to the rigidbody relative to its coordinate system. Returns Success.")]
	public class AddRelativeTorque : Action
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x0001E918 File Offset: 0x0001CB18
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001E958 File Offset: 0x0001CB58
		public override TaskStatus OnUpdate()
		{
			this.rigidbody.AddRelativeTorque(this.torque.Value, this.forceMode);
			return TaskStatus.Success;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001E977 File Offset: 0x0001CB77
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.torque = Vector3.zero;
			this.forceMode = ForceMode.Force;
		}

		// Token: 0x0400055B RID: 1371
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400055C RID: 1372
		[Tooltip("The amount of torque to apply")]
		public SharedVector3 torque;

		// Token: 0x0400055D RID: 1373
		[Tooltip("The type of torque")]
		public ForceMode forceMode;

		// Token: 0x0400055E RID: 1374
		private Rigidbody rigidbody;

		// Token: 0x0400055F RID: 1375
		private GameObject prevGameObject;
	}
}

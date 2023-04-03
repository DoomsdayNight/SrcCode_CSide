using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200019D RID: 413
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the use gravity value of the Rigidbody. Returns Success.")]
	public class SetUseGravity : Action
	{
		// Token: 0x060009DD RID: 2525 RVA: 0x0001F870 File Offset: 0x0001DA70
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001F8B0 File Offset: 0x0001DAB0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.useGravity = this.useGravity.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001F8E3 File Offset: 0x0001DAE3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.useGravity = false;
		}

		// Token: 0x040005C7 RID: 1479
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005C8 RID: 1480
		[Tooltip("The use gravity value of the Rigidbody")]
		public SharedBool useGravity;

		// Token: 0x040005C9 RID: 1481
		private Rigidbody rigidbody;

		// Token: 0x040005CA RID: 1482
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x020001A1 RID: 417
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Forces the Rigidbody to wake up. Returns Success.")]
	public class WakeUp : Conditional
	{
		// Token: 0x060009ED RID: 2541 RVA: 0x0001FA90 File Offset: 0x0001DC90
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0001FAD0 File Offset: 0x0001DCD0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.WakeUp();
			return TaskStatus.Success;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0001FAF8 File Offset: 0x0001DCF8
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040005D5 RID: 1493
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005D6 RID: 1494
		private Rigidbody rigidbody;

		// Token: 0x040005D7 RID: 1495
		private GameObject prevGameObject;
	}
}

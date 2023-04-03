using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200019F RID: 415
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Forces the Rigidbody to sleep at least one frame. Returns Success.")]
	public class Sleep : Conditional
	{
		// Token: 0x060009E5 RID: 2533 RVA: 0x0001F994 File Offset: 0x0001DB94
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0001F9D4 File Offset: 0x0001DBD4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.Sleep();
			return TaskStatus.Success;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0001F9FC File Offset: 0x0001DBFC
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040005CF RID: 1487
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005D0 RID: 1488
		private Rigidbody rigidbody;

		// Token: 0x040005D1 RID: 1489
		private GameObject prevGameObject;
	}
}

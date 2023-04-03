using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200016C RID: 364
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the is kinematic value of the Rigidbody2D. Returns Success.")]
	public class GetIsKinematic : Action
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x0001DBF8 File Offset: 0x0001BDF8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001DC38 File Offset: 0x0001BE38
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.isKinematic;
			return TaskStatus.Success;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0001DC6B File Offset: 0x0001BE6B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x040004FF RID: 1279
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000500 RID: 1280
		[Tooltip("The is kinematic value of the Rigidbody2D")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04000501 RID: 1281
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000502 RID: 1282
		private GameObject prevGameObject;
	}
}

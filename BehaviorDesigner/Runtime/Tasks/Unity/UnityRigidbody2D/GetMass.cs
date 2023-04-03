using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200016D RID: 365
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the mass of the Rigidbody2D. Returns Success.")]
	public class GetMass : Action
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x0001DC88 File Offset: 0x0001BE88
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0001DCC8 File Offset: 0x0001BEC8
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.mass;
			return TaskStatus.Success;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0001DCFB File Offset: 0x0001BEFB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000503 RID: 1283
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000504 RID: 1284
		[Tooltip("The mass of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000505 RID: 1285
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000506 RID: 1286
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000169 RID: 361
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the angular velocity of the Rigidbody2D. Returns Success.")]
	public class GetAngularVelocity : Action
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x0001DA3C File Offset: 0x0001BC3C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0001DA7C File Offset: 0x0001BC7C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.angularVelocity;
			return TaskStatus.Success;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0001DAAF File Offset: 0x0001BCAF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040004F3 RID: 1267
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004F4 RID: 1268
		[Tooltip("The angular velocity of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040004F5 RID: 1269
		private Rigidbody2D rigidbody2D;

		// Token: 0x040004F6 RID: 1270
		private GameObject prevGameObject;
	}
}

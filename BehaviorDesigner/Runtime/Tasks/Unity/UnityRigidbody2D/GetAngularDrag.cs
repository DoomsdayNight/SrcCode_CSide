using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000168 RID: 360
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the angular drag of the Rigidbody2D. Returns Success.")]
	public class GetAngularDrag : Action
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x0001D9A8 File Offset: 0x0001BBA8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0001D9E8 File Offset: 0x0001BBE8
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.angularDrag;
			return TaskStatus.Success;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0001DA1B File Offset: 0x0001BC1B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040004EF RID: 1263
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004F0 RID: 1264
		[Tooltip("The angular drag of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040004F1 RID: 1265
		private Rigidbody2D rigidbody2D;

		// Token: 0x040004F2 RID: 1266
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200016A RID: 362
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the drag of the Rigidbody2D. Returns Success.")]
	public class GetDrag : Action
	{
		// Token: 0x06000911 RID: 2321 RVA: 0x0001DAD0 File Offset: 0x0001BCD0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0001DB10 File Offset: 0x0001BD10
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.drag;
			return TaskStatus.Success;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0001DB43 File Offset: 0x0001BD43
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040004F7 RID: 1271
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004F8 RID: 1272
		[Tooltip("The drag of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040004F9 RID: 1273
		private Rigidbody2D rigidbody2D;

		// Token: 0x040004FA RID: 1274
		private GameObject prevGameObject;
	}
}

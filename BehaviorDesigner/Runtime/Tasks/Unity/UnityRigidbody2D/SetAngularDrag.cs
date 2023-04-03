using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000175 RID: 373
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Sets the angular drag of the Rigidbody2D. Returns Success.")]
	public class SetAngularDrag : Action
	{
		// Token: 0x0600093D RID: 2365 RVA: 0x0001E100 File Offset: 0x0001C300
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0001E140 File Offset: 0x0001C340
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.rigidbody2D.angularDrag = this.angularDrag.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0001E173 File Offset: 0x0001C373
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularDrag = 0f;
		}

		// Token: 0x04000521 RID: 1313
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000522 RID: 1314
		[Tooltip("The angular drag of the Rigidbody2D")]
		public SharedFloat angularDrag;

		// Token: 0x04000523 RID: 1315
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000524 RID: 1316
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000191 RID: 401
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Moves the Rigidbody to the specified position. Returns Success.")]
	public class MovePosition : Action
	{
		// Token: 0x060009AD RID: 2477 RVA: 0x0001F194 File Offset: 0x0001D394
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001F1D4 File Offset: 0x0001D3D4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.MovePosition(this.position.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0001F207 File Offset: 0x0001D407
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04000597 RID: 1431
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000598 RID: 1432
		[Tooltip("The new position of the Rigidbody")]
		public SharedVector3 position;

		// Token: 0x04000599 RID: 1433
		private Rigidbody rigidbody;

		// Token: 0x0400059A RID: 1434
		private GameObject prevGameObject;
	}
}

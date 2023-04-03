using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000193 RID: 403
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the angular drag of the Rigidbody. Returns Success.")]
	public class SetAngularDrag : Action
	{
		// Token: 0x060009B5 RID: 2485 RVA: 0x0001F2BC File Offset: 0x0001D4BC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0001F2FC File Offset: 0x0001D4FC
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.angularDrag = this.angularDrag.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0001F32F File Offset: 0x0001D52F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularDrag = 0f;
		}

		// Token: 0x0400059F RID: 1439
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005A0 RID: 1440
		[Tooltip("The angular drag of the Rigidbody")]
		public SharedFloat angularDrag;

		// Token: 0x040005A1 RID: 1441
		private Rigidbody rigidbody;

		// Token: 0x040005A2 RID: 1442
		private GameObject prevGameObject;
	}
}

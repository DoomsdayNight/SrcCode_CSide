using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200019C RID: 412
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the rotation of the Rigidbody. Returns Success.")]
	public class SetRotation : Action
	{
		// Token: 0x060009D9 RID: 2521 RVA: 0x0001F7DC File Offset: 0x0001D9DC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001F81C File Offset: 0x0001DA1C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.rotation = this.rotation.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001F84F File Offset: 0x0001DA4F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x040005C3 RID: 1475
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005C4 RID: 1476
		[Tooltip("The rotation of the Rigidbody")]
		public SharedQuaternion rotation;

		// Token: 0x040005C5 RID: 1477
		private Rigidbody rigidbody;

		// Token: 0x040005C6 RID: 1478
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200019B RID: 411
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the position of the Rigidbody. Returns Success.")]
	public class SetPosition : Action
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x0001F748 File Offset: 0x0001D948
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0001F788 File Offset: 0x0001D988
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.position = this.position.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0001F7BB File Offset: 0x0001D9BB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x040005BF RID: 1471
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005C0 RID: 1472
		[Tooltip("The position of the Rigidbody")]
		public SharedVector3 position;

		// Token: 0x040005C1 RID: 1473
		private Rigidbody rigidbody;

		// Token: 0x040005C2 RID: 1474
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200019E RID: 414
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the velocity of the Rigidbody. Returns Success.")]
	public class SetVelocity : Action
	{
		// Token: 0x060009E1 RID: 2529 RVA: 0x0001F900 File Offset: 0x0001DB00
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0001F940 File Offset: 0x0001DB40
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.velocity = this.velocity.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0001F973 File Offset: 0x0001DB73
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.velocity = Vector3.zero;
		}

		// Token: 0x040005CB RID: 1483
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005CC RID: 1484
		[Tooltip("The velocity of the Rigidbody")]
		public SharedVector3 velocity;

		// Token: 0x040005CD RID: 1485
		private Rigidbody rigidbody;

		// Token: 0x040005CE RID: 1486
		private GameObject prevGameObject;
	}
}

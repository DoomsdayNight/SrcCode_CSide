using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D0 RID: 208
	[TaskDescription("Returns success when a collision starts. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasEnteredCollision : Conditional
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x00018C31 File Offset: 0x00016E31
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredCollision)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00018C3E File Offset: 0x00016E3E
		public override void OnEnd()
		{
			this.enteredCollision = false;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00018C48 File Offset: 0x00016E48
		public override void OnCollisionEnter(Collision collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || collision.gameObject.CompareTag(this.tag.Value))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.enteredCollision = true;
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00018C97 File Offset: 0x00016E97
		public override void OnReset()
		{
			this.tag = "";
			this.collidedGameObject = null;
		}

		// Token: 0x0400032F RID: 815
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x04000330 RID: 816
		[Tooltip("The object that started the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x04000331 RID: 817
		private bool enteredCollision;
	}
}

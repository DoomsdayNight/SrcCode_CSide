using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D1 RID: 209
	[TaskDescription("Returns success when a 2D collision starts. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasEnteredCollision2D : Conditional
	{
		// Token: 0x060006DB RID: 1755 RVA: 0x00018CC8 File Offset: 0x00016EC8
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredCollision)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00018CD5 File Offset: 0x00016ED5
		public override void OnEnd()
		{
			this.enteredCollision = false;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00018CE0 File Offset: 0x00016EE0
		public override void OnCollisionEnter2D(Collision2D collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || collision.gameObject.CompareTag(this.tag.Value))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.enteredCollision = true;
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00018D2F File Offset: 0x00016F2F
		public override void OnReset()
		{
			this.tag = "";
			this.collidedGameObject = null;
		}

		// Token: 0x04000332 RID: 818
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x04000333 RID: 819
		[Tooltip("The object that started the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x04000334 RID: 820
		private bool enteredCollision;
	}
}

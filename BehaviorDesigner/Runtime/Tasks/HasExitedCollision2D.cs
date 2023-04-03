using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D5 RID: 213
	[TaskDescription("Returns success when a 2D collision ends. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasExitedCollision2D : Conditional
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x00018F18 File Offset: 0x00017118
		public override TaskStatus OnUpdate()
		{
			if (!this.exitedCollision)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00018F25 File Offset: 0x00017125
		public override void OnEnd()
		{
			this.exitedCollision = false;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00018F30 File Offset: 0x00017130
		public override void OnCollisionExit2D(Collision2D collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || collision.gameObject.CompareTag(this.tag.Value))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.exitedCollision = true;
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00018F7F File Offset: 0x0001717F
		public override void OnReset()
		{
			this.tag = "";
			this.collidedGameObject = null;
		}

		// Token: 0x0400033E RID: 830
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x0400033F RID: 831
		[Tooltip("The object that exited the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x04000340 RID: 832
		private bool exitedCollision;
	}
}

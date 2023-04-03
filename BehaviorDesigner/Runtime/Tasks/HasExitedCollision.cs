using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D4 RID: 212
	[TaskDescription("Returns success when a collision ends. This task will only receive the physics callback if it is being reevaluated (with a conditional abort or under a parallel task).")]
	[TaskCategory("Physics")]
	public class HasExitedCollision : Conditional
	{
		// Token: 0x060006EA RID: 1770 RVA: 0x00018E90 File Offset: 0x00017090
		public override TaskStatus OnUpdate()
		{
			if (!this.exitedCollision)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00018E9D File Offset: 0x0001709D
		public override void OnEnd()
		{
			this.exitedCollision = false;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00018EA8 File Offset: 0x000170A8
		public override void OnCollisionExit(Collision collision)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || collision.gameObject.CompareTag(this.tag.Value))
			{
				this.collidedGameObject.Value = collision.gameObject;
				this.exitedCollision = true;
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00018EF7 File Offset: 0x000170F7
		public override void OnReset()
		{
			this.collidedGameObject = null;
		}

		// Token: 0x0400033B RID: 827
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x0400033C RID: 828
		[Tooltip("The object that exited the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x0400033D RID: 829
		private bool exitedCollision;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCircleCollider2D
{
	// Token: 0x0200023B RID: 571
	[TaskCategory("Unity/CircleCollider2D")]
	[TaskDescription("Sets the radius of the CircleCollider2D. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x06000C00 RID: 3072 RVA: 0x00024870 File Offset: 0x00022A70
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.circleCollider2D = defaultGameObject.GetComponent<CircleCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000248B0 File Offset: 0x00022AB0
		public override TaskStatus OnUpdate()
		{
			if (this.circleCollider2D == null)
			{
				Debug.LogWarning("CircleCollider2D is null");
				return TaskStatus.Failure;
			}
			this.circleCollider2D.radius = this.radius.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x000248E3 File Offset: 0x00022AE3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x040007C1 RID: 1985
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007C2 RID: 1986
		[Tooltip("The radius of the CircleCollider2D")]
		public SharedFloat radius;

		// Token: 0x040007C3 RID: 1987
		private CircleCollider2D circleCollider2D;

		// Token: 0x040007C4 RID: 1988
		private GameObject prevGameObject;
	}
}

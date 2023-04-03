using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCircleCollider2D
{
	// Token: 0x02000239 RID: 569
	[TaskCategory("Unity/CircleCollider2D")]
	[TaskDescription("Stores the radius of the CircleCollider2D. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x06000BF8 RID: 3064 RVA: 0x00024740 File Offset: 0x00022940
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.circleCollider2D = defaultGameObject.GetComponent<CircleCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00024780 File Offset: 0x00022980
		public override TaskStatus OnUpdate()
		{
			if (this.circleCollider2D == null)
			{
				Debug.LogWarning("CircleCollider2D is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.circleCollider2D.radius;
			return TaskStatus.Success;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x000247B3 File Offset: 0x000229B3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040007B9 RID: 1977
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007BA RID: 1978
		[Tooltip("The radius of the CircleCollider2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040007BB RID: 1979
		private CircleCollider2D circleCollider2D;

		// Token: 0x040007BC RID: 1980
		private GameObject prevGameObject;
	}
}

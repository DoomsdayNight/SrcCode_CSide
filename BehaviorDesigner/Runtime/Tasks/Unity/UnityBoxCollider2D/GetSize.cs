using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBoxCollider2D
{
	// Token: 0x02000253 RID: 595
	[TaskCategory("Unity/BoxCollider2D")]
	[TaskDescription("Stores the size of the BoxCollider2D. Returns Success.")]
	public class GetSize : Action
	{
		// Token: 0x06000C61 RID: 3169 RVA: 0x0002564C File Offset: 0x0002384C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider2D = defaultGameObject.GetComponent<BoxCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0002568C File Offset: 0x0002388C
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider2D == null)
			{
				Debug.LogWarning("BoxCollider2D is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.boxCollider2D.size;
			return TaskStatus.Success;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000256BF File Offset: 0x000238BF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector2.zero;
		}

		// Token: 0x04000820 RID: 2080
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000821 RID: 2081
		[Tooltip("The size of the BoxCollider2D")]
		[RequiredField]
		public SharedVector2 storeValue;

		// Token: 0x04000822 RID: 2082
		private BoxCollider2D boxCollider2D;

		// Token: 0x04000823 RID: 2083
		private GameObject prevGameObject;
	}
}

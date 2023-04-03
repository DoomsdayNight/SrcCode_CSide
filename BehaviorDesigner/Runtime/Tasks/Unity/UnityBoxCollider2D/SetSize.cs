using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBoxCollider2D
{
	// Token: 0x02000254 RID: 596
	[TaskCategory("Unity/BoxCollider2D")]
	[TaskDescription("Sets the size of the BoxCollider2D. Returns Success.")]
	public class SetSize : Action
	{
		// Token: 0x06000C65 RID: 3173 RVA: 0x000256E0 File Offset: 0x000238E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider2D = defaultGameObject.GetComponent<BoxCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00025720 File Offset: 0x00023920
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider2D == null)
			{
				Debug.LogWarning("BoxCollider2D is null");
				return TaskStatus.Failure;
			}
			this.boxCollider2D.size = this.size.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00025753 File Offset: 0x00023953
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.size = Vector2.zero;
		}

		// Token: 0x04000824 RID: 2084
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000825 RID: 2085
		[Tooltip("The size of the BoxCollider2D")]
		public SharedVector2 size;

		// Token: 0x04000826 RID: 2086
		private BoxCollider2D boxCollider2D;

		// Token: 0x04000827 RID: 2087
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCircleCollider2D
{
	// Token: 0x0200023A RID: 570
	[TaskCategory("Unity/CircleCollider2D")]
	[TaskDescription("Sets the offset of the CircleCollider2D. Returns Success.")]
	public class SetOffset : Action
	{
		// Token: 0x06000BFC RID: 3068 RVA: 0x000247D4 File Offset: 0x000229D4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.circleCollider2D = defaultGameObject.GetComponent<CircleCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00024814 File Offset: 0x00022A14
		public override TaskStatus OnUpdate()
		{
			if (this.circleCollider2D == null)
			{
				Debug.LogWarning("CircleCollider2D is null");
				return TaskStatus.Failure;
			}
			this.circleCollider2D.offset = this.offset.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002484C File Offset: 0x00022A4C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.offset = Vector3.zero;
		}

		// Token: 0x040007BD RID: 1981
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007BE RID: 1982
		[Tooltip("The offset of the CircleCollider2D")]
		public SharedVector3 offset;

		// Token: 0x040007BF RID: 1983
		private CircleCollider2D circleCollider2D;

		// Token: 0x040007C0 RID: 1984
		private GameObject prevGameObject;
	}
}

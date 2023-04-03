using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x02000252 RID: 594
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Sets the radius of the CapsuleCollider. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x06000C5D RID: 3165 RVA: 0x000255B8 File Offset: 0x000237B8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x000255F8 File Offset: 0x000237F8
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return TaskStatus.Failure;
			}
			this.capsuleCollider.radius = this.radius.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002562B File Offset: 0x0002382B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x0400081C RID: 2076
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400081D RID: 2077
		[Tooltip("The radius of the CapsuleCollider")]
		public SharedFloat radius;

		// Token: 0x0400081E RID: 2078
		private CapsuleCollider capsuleCollider;

		// Token: 0x0400081F RID: 2079
		private GameObject prevGameObject;
	}
}

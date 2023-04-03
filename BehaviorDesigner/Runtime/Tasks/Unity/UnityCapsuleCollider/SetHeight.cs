using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x02000251 RID: 593
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Sets the height of the CapsuleCollider. Returns Success.")]
	public class SetHeight : Action
	{
		// Token: 0x06000C59 RID: 3161 RVA: 0x00025524 File Offset: 0x00023724
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00025564 File Offset: 0x00023764
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return TaskStatus.Failure;
			}
			this.capsuleCollider.height = this.direction.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00025597 File Offset: 0x00023797
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.direction = 0f;
		}

		// Token: 0x04000818 RID: 2072
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000819 RID: 2073
		[Tooltip("The height of the CapsuleCollider")]
		public SharedFloat direction;

		// Token: 0x0400081A RID: 2074
		private CapsuleCollider capsuleCollider;

		// Token: 0x0400081B RID: 2075
		private GameObject prevGameObject;
	}
}

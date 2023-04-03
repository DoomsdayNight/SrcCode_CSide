using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x0200024D RID: 589
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Gets the height of the CapsuleCollider. Returns Success.")]
	public class GetHeight : Action
	{
		// Token: 0x06000C49 RID: 3145 RVA: 0x000252D8 File Offset: 0x000234D8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00025318 File Offset: 0x00023518
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.capsuleCollider.height;
			return TaskStatus.Success;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002534B File Offset: 0x0002354B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000808 RID: 2056
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000809 RID: 2057
		[Tooltip("The height of the CapsuleCollider")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400080A RID: 2058
		private CapsuleCollider capsuleCollider;

		// Token: 0x0400080B RID: 2059
		private GameObject prevGameObject;
	}
}

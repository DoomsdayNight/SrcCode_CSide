using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x02000250 RID: 592
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Sets the direction of the CapsuleCollider. Returns Success.")]
	public class SetDirection : Action
	{
		// Token: 0x06000C55 RID: 3157 RVA: 0x00025494 File Offset: 0x00023694
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x000254D4 File Offset: 0x000236D4
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return TaskStatus.Failure;
			}
			this.capsuleCollider.direction = this.direction.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00025507 File Offset: 0x00023707
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.direction = 0;
		}

		// Token: 0x04000814 RID: 2068
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000815 RID: 2069
		[Tooltip("The direction of the CapsuleCollider")]
		public SharedInt direction;

		// Token: 0x04000816 RID: 2070
		private CapsuleCollider capsuleCollider;

		// Token: 0x04000817 RID: 2071
		private GameObject prevGameObject;
	}
}

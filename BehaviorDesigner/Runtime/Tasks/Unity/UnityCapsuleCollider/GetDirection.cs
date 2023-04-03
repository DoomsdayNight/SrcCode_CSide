using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x0200024C RID: 588
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Stores the direction of the CapsuleCollider. Returns Success.")]
	public class GetDirection : Action
	{
		// Token: 0x06000C45 RID: 3141 RVA: 0x00025248 File Offset: 0x00023448
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00025288 File Offset: 0x00023488
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.capsuleCollider.direction;
			return TaskStatus.Success;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000252BB File Offset: 0x000234BB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0;
		}

		// Token: 0x04000804 RID: 2052
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000805 RID: 2053
		[Tooltip("The direction of the CapsuleCollider")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x04000806 RID: 2054
		private CapsuleCollider capsuleCollider;

		// Token: 0x04000807 RID: 2055
		private GameObject prevGameObject;
	}
}

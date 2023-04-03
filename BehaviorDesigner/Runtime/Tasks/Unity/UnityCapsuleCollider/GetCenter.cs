using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x0200024B RID: 587
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Stores the center of the CapsuleCollider. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x06000C41 RID: 3137 RVA: 0x000251B4 File Offset: 0x000233B4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x000251F4 File Offset: 0x000233F4
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.capsuleCollider.center;
			return TaskStatus.Success;
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00025227 File Offset: 0x00023427
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000800 RID: 2048
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000801 RID: 2049
		[Tooltip("The center of the CapsuleCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000802 RID: 2050
		private CapsuleCollider capsuleCollider;

		// Token: 0x04000803 RID: 2051
		private GameObject prevGameObject;
	}
}

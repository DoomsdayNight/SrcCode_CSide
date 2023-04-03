using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x0200024F RID: 591
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Sets the center of the CapsuleCollider. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x06000C51 RID: 3153 RVA: 0x00025400 File Offset: 0x00023600
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00025440 File Offset: 0x00023640
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return TaskStatus.Failure;
			}
			this.capsuleCollider.center = this.center.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00025473 File Offset: 0x00023673
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x04000810 RID: 2064
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000811 RID: 2065
		[Tooltip("The center of the CapsuleCollider")]
		public SharedVector3 center;

		// Token: 0x04000812 RID: 2066
		private CapsuleCollider capsuleCollider;

		// Token: 0x04000813 RID: 2067
		private GameObject prevGameObject;
	}
}

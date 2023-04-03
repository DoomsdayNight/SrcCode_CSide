using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBoxCollider
{
	// Token: 0x02000257 RID: 599
	[TaskCategory("Unity/BoxCollider")]
	[TaskDescription("Sets the center of the BoxCollider. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x06000C71 RID: 3185 RVA: 0x0002589C File Offset: 0x00023A9C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x000258DC File Offset: 0x00023ADC
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider == null)
			{
				Debug.LogWarning("BoxCollider is null");
				return TaskStatus.Failure;
			}
			this.boxCollider.center = this.center.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0002590F File Offset: 0x00023B0F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x04000830 RID: 2096
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000831 RID: 2097
		[Tooltip("The center of the BoxCollider")]
		public SharedVector3 center;

		// Token: 0x04000832 RID: 2098
		private BoxCollider boxCollider;

		// Token: 0x04000833 RID: 2099
		private GameObject prevGameObject;
	}
}

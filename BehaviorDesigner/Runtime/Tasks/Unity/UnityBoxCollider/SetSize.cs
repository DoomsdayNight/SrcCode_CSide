using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBoxCollider
{
	// Token: 0x02000258 RID: 600
	[TaskCategory("Unity/BoxCollider")]
	[TaskDescription("Sets the size of the BoxCollider. Returns Success.")]
	public class SetSize : Action
	{
		// Token: 0x06000C75 RID: 3189 RVA: 0x00025930 File Offset: 0x00023B30
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00025970 File Offset: 0x00023B70
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider == null)
			{
				Debug.LogWarning("BoxCollider is null");
				return TaskStatus.Failure;
			}
			this.boxCollider.size = this.size.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x000259A3 File Offset: 0x00023BA3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.size = Vector3.zero;
		}

		// Token: 0x04000834 RID: 2100
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000835 RID: 2101
		[Tooltip("The size of the BoxCollider")]
		public SharedVector3 size;

		// Token: 0x04000836 RID: 2102
		private BoxCollider boxCollider;

		// Token: 0x04000837 RID: 2103
		private GameObject prevGameObject;
	}
}

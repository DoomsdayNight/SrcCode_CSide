using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBoxCollider
{
	// Token: 0x02000256 RID: 598
	[TaskCategory("Unity/BoxCollider")]
	[TaskDescription("Stores the size of the BoxCollider. Returns Success.")]
	public class GetSize : Action
	{
		// Token: 0x06000C6D RID: 3181 RVA: 0x00025808 File Offset: 0x00023A08
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00025848 File Offset: 0x00023A48
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider == null)
			{
				Debug.LogWarning("BoxCollider is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.boxCollider.size;
			return TaskStatus.Success;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002587B File Offset: 0x00023A7B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x0400082C RID: 2092
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400082D RID: 2093
		[Tooltip("The size of the BoxCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x0400082E RID: 2094
		private BoxCollider boxCollider;

		// Token: 0x0400082F RID: 2095
		private GameObject prevGameObject;
	}
}

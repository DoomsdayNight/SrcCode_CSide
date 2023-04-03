using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBoxCollider
{
	// Token: 0x02000255 RID: 597
	[TaskCategory("Unity/BoxCollider")]
	[TaskDescription("Stores the center of the BoxCollider. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x06000C69 RID: 3177 RVA: 0x00025774 File Offset: 0x00023974
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.boxCollider = defaultGameObject.GetComponent<BoxCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x000257B4 File Offset: 0x000239B4
		public override TaskStatus OnUpdate()
		{
			if (this.boxCollider == null)
			{
				Debug.LogWarning("BoxCollider is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.boxCollider.center;
			return TaskStatus.Success;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x000257E7 File Offset: 0x000239E7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000828 RID: 2088
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000829 RID: 2089
		[Tooltip("The center of the BoxCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x0400082A RID: 2090
		private BoxCollider boxCollider;

		// Token: 0x0400082B RID: 2091
		private GameObject prevGameObject;
	}
}

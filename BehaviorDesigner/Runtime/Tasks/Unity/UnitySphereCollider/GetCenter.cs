using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnitySphereCollider
{
	// Token: 0x0200013D RID: 317
	[TaskCategory("Unity/SphereCollider")]
	[TaskDescription("Stores the center of the SphereCollider. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x0600087F RID: 2175 RVA: 0x0001C804 File Offset: 0x0001AA04
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0001C844 File Offset: 0x0001AA44
		public override TaskStatus OnUpdate()
		{
			if (this.sphereCollider == null)
			{
				Debug.LogWarning("SphereCollider is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.sphereCollider.center;
			return TaskStatus.Success;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001C877 File Offset: 0x0001AA77
		public override void OnReset()
		{
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000489 RID: 1161
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400048A RID: 1162
		[Tooltip("The center of the SphereCollider")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x0400048B RID: 1163
		private SphereCollider sphereCollider;

		// Token: 0x0400048C RID: 1164
		private GameObject prevGameObject;
	}
}

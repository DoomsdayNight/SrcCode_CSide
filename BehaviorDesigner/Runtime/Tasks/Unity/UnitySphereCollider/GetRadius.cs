using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnitySphereCollider
{
	// Token: 0x0200013E RID: 318
	[TaskCategory("Unity/SphereCollider")]
	[TaskDescription("Stores the radius of the SphereCollider. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x06000883 RID: 2179 RVA: 0x0001C894 File Offset: 0x0001AA94
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001C8D4 File Offset: 0x0001AAD4
		public override TaskStatus OnUpdate()
		{
			if (this.sphereCollider == null)
			{
				Debug.LogWarning("SphereCollider is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.sphereCollider.radius;
			return TaskStatus.Success;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0001C907 File Offset: 0x0001AB07
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400048D RID: 1165
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400048E RID: 1166
		[Tooltip("The radius of the SphereCollider")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400048F RID: 1167
		private SphereCollider sphereCollider;

		// Token: 0x04000490 RID: 1168
		private GameObject prevGameObject;
	}
}

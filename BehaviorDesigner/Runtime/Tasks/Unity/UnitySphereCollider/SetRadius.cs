using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnitySphereCollider
{
	// Token: 0x02000140 RID: 320
	[TaskCategory("Unity/SphereCollider")]
	[TaskDescription("Sets the radius of the SphereCollider. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x0600088B RID: 2187 RVA: 0x0001C9BC File Offset: 0x0001ABBC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001C9FC File Offset: 0x0001ABFC
		public override TaskStatus OnUpdate()
		{
			if (this.sphereCollider == null)
			{
				Debug.LogWarning("SphereCollider is null");
				return TaskStatus.Failure;
			}
			this.sphereCollider.radius = this.radius.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001CA2F File Offset: 0x0001AC2F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x04000495 RID: 1173
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000496 RID: 1174
		[Tooltip("The radius of the SphereCollider")]
		public SharedFloat radius;

		// Token: 0x04000497 RID: 1175
		private SphereCollider sphereCollider;

		// Token: 0x04000498 RID: 1176
		private GameObject prevGameObject;
	}
}

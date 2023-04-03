using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnitySphereCollider
{
	// Token: 0x0200013F RID: 319
	[TaskCategory("Unity/SphereCollider")]
	[TaskDescription("Sets the center of the SphereCollider. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x06000887 RID: 2183 RVA: 0x0001C928 File Offset: 0x0001AB28
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.sphereCollider = defaultGameObject.GetComponent<SphereCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001C968 File Offset: 0x0001AB68
		public override TaskStatus OnUpdate()
		{
			if (this.sphereCollider == null)
			{
				Debug.LogWarning("SphereCollider is null");
				return TaskStatus.Failure;
			}
			this.sphereCollider.center = this.center.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001C99B File Offset: 0x0001AB9B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x04000491 RID: 1169
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000492 RID: 1170
		[Tooltip("The center of the SphereCollider")]
		public SharedVector3 center;

		// Token: 0x04000493 RID: 1171
		private SphereCollider sphereCollider;

		// Token: 0x04000494 RID: 1172
		private GameObject prevGameObject;
	}
}

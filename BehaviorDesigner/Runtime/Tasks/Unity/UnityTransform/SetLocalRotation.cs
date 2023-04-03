using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000121 RID: 289
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the local rotation of the Transform. Returns Success.")]
	public class SetLocalRotation : Action
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x0001BAD4 File Offset: 0x00019CD4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001BB14 File Offset: 0x00019D14
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.targetTransform.localRotation = this.localRotation.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001BB47 File Offset: 0x00019D47
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localRotation = Quaternion.identity;
		}

		// Token: 0x04000434 RID: 1076
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000435 RID: 1077
		[Tooltip("The local rotation of the Transform")]
		public SharedQuaternion localRotation;

		// Token: 0x04000436 RID: 1078
		private Transform targetTransform;

		// Token: 0x04000437 RID: 1079
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200011E RID: 286
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the forward vector of the Transform. Returns Success.")]
	public class SetForwardVector : Action
	{
		// Token: 0x06000810 RID: 2064 RVA: 0x0001B918 File Offset: 0x00019B18
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001B958 File Offset: 0x00019B58
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.targetTransform.forward = this.position.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001B98B File Offset: 0x00019B8B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04000428 RID: 1064
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000429 RID: 1065
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x0400042A RID: 1066
		private Transform targetTransform;

		// Token: 0x0400042B RID: 1067
		private GameObject prevGameObject;
	}
}

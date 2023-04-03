using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200011B RID: 283
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Applies a rotation. Returns Success.")]
	public class Rotate : Action
	{
		// Token: 0x06000804 RID: 2052 RVA: 0x0001B704 File Offset: 0x00019904
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001B744 File Offset: 0x00019944
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.targetTransform.Rotate(this.eulerAngles.Value, this.relativeTo);
			return TaskStatus.Success;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001B77D File Offset: 0x0001997D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.eulerAngles = Vector3.zero;
			this.relativeTo = Space.Self;
		}

		// Token: 0x04000419 RID: 1049
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400041A RID: 1050
		[Tooltip("Amount to rotate")]
		public SharedVector3 eulerAngles;

		// Token: 0x0400041B RID: 1051
		[Tooltip("Specifies which axis the rotation is relative to")]
		public Space relativeTo = Space.Self;

		// Token: 0x0400041C RID: 1052
		private Transform targetTransform;

		// Token: 0x0400041D RID: 1053
		private GameObject prevGameObject;
	}
}

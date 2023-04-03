using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000120 RID: 288
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the local position of the Transform. Returns Success.")]
	public class SetLocalPosition : Action
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x0001BA40 File Offset: 0x00019C40
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001BA80 File Offset: 0x00019C80
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.targetTransform.localPosition = this.localPosition.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001BAB3 File Offset: 0x00019CB3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localPosition = Vector3.zero;
		}

		// Token: 0x04000430 RID: 1072
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000431 RID: 1073
		[Tooltip("The local position of the Transform")]
		public SharedVector3 localPosition;

		// Token: 0x04000432 RID: 1074
		private Transform targetTransform;

		// Token: 0x04000433 RID: 1075
		private GameObject prevGameObject;
	}
}

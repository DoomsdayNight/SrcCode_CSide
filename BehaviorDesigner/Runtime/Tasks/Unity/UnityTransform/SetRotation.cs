using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000126 RID: 294
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the rotation of the Transform. Returns Success.")]
	public class SetRotation : Action
	{
		// Token: 0x06000830 RID: 2096 RVA: 0x0001BDB0 File Offset: 0x00019FB0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001BDF0 File Offset: 0x00019FF0
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.targetTransform.rotation = this.rotation.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001BE23 File Offset: 0x0001A023
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x04000448 RID: 1096
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000449 RID: 1097
		[Tooltip("The rotation of the Transform")]
		public SharedQuaternion rotation;

		// Token: 0x0400044A RID: 1098
		private Transform targetTransform;

		// Token: 0x0400044B RID: 1099
		private GameObject prevGameObject;
	}
}

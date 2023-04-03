using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200011C RID: 284
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Applies a rotation. Returns Success.")]
	public class RotateAround : Action
	{
		// Token: 0x06000808 RID: 2056 RVA: 0x0001B7AC File Offset: 0x000199AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001B7EC File Offset: 0x000199EC
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.targetTransform.RotateAround(this.point.Value, this.axis.Value, this.angle.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001B840 File Offset: 0x00019A40
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.point = Vector3.zero;
			this.axis = Vector3.zero;
			this.angle = 0f;
		}

		// Token: 0x0400041E RID: 1054
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400041F RID: 1055
		[Tooltip("Point to rotate around")]
		public SharedVector3 point;

		// Token: 0x04000420 RID: 1056
		[Tooltip("Axis to rotate around")]
		public SharedVector3 axis;

		// Token: 0x04000421 RID: 1057
		[Tooltip("Amount to rotate")]
		public SharedFloat angle;

		// Token: 0x04000422 RID: 1058
		private Transform targetTransform;

		// Token: 0x04000423 RID: 1059
		private GameObject prevGameObject;
	}
}

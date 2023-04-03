using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200011D RID: 285
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the euler angles of the Transform. Returns Success.")]
	public class SetEulerAngles : Action
	{
		// Token: 0x0600080C RID: 2060 RVA: 0x0001B884 File Offset: 0x00019A84
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001B8C4 File Offset: 0x00019AC4
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.targetTransform.eulerAngles = this.eulerAngles.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001B8F7 File Offset: 0x00019AF7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.eulerAngles = Vector3.zero;
		}

		// Token: 0x04000424 RID: 1060
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000425 RID: 1061
		[Tooltip("The euler angles of the Transform")]
		public SharedVector3 eulerAngles;

		// Token: 0x04000426 RID: 1062
		private Transform targetTransform;

		// Token: 0x04000427 RID: 1063
		private GameObject prevGameObject;
	}
}

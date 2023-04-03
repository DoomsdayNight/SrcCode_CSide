using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000125 RID: 293
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the right vector of the Transform. Returns Success.")]
	public class SetRightVector : Action
	{
		// Token: 0x0600082C RID: 2092 RVA: 0x0001BD1C File Offset: 0x00019F1C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001BD5C File Offset: 0x00019F5C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.targetTransform.right = this.position.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001BD8F File Offset: 0x00019F8F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04000444 RID: 1092
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000445 RID: 1093
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x04000446 RID: 1094
		private Transform targetTransform;

		// Token: 0x04000447 RID: 1095
		private GameObject prevGameObject;
	}
}

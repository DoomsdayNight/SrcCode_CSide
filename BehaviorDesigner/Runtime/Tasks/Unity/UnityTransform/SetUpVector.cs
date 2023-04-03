using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000127 RID: 295
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the up vector of the Transform. Returns Success.")]
	public class SetUpVector : Action
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x0001BE44 File Offset: 0x0001A044
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001BE84 File Offset: 0x0001A084
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.targetTransform.up = this.position.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001BEB7 File Offset: 0x0001A0B7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x0400044C RID: 1100
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400044D RID: 1101
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x0400044E RID: 1102
		private Transform targetTransform;

		// Token: 0x0400044F RID: 1103
		private GameObject prevGameObject;
	}
}

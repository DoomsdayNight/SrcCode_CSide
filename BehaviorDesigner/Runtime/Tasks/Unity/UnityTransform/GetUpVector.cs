using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000118 RID: 280
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the up vector of the Transform. Returns Success.")]
	public class GetUpVector : Action
	{
		// Token: 0x060007F8 RID: 2040 RVA: 0x0001B4F8 File Offset: 0x000196F8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001B538 File Offset: 0x00019738
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.up;
			return TaskStatus.Success;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001B56B File Offset: 0x0001976B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x0400040B RID: 1035
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400040C RID: 1036
		[Tooltip("The position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x0400040D RID: 1037
		private Transform targetTransform;

		// Token: 0x0400040E RID: 1038
		private GameObject prevGameObject;
	}
}

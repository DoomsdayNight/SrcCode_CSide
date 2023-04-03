using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000115 RID: 277
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the position of the Transform. Returns Success.")]
	public class GetPosition : Action
	{
		// Token: 0x060007EC RID: 2028 RVA: 0x0001B33C File Offset: 0x0001953C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001B37C File Offset: 0x0001957C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.position;
			return TaskStatus.Success;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001B3AF File Offset: 0x000195AF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040003FF RID: 1023
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000400 RID: 1024
		[Tooltip("Can the target GameObject be empty?")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000401 RID: 1025
		private Transform targetTransform;

		// Token: 0x04000402 RID: 1026
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000116 RID: 278
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the right vector of the Transform. Returns Success.")]
	public class GetRightVector : Action
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x0001B3D0 File Offset: 0x000195D0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001B410 File Offset: 0x00019610
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.right;
			return TaskStatus.Success;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001B443 File Offset: 0x00019643
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000403 RID: 1027
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000404 RID: 1028
		[Tooltip("The position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000405 RID: 1029
		private Transform targetTransform;

		// Token: 0x04000406 RID: 1030
		private GameObject prevGameObject;
	}
}

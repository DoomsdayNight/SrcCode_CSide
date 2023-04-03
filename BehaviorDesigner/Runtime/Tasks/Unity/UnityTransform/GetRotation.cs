using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000117 RID: 279
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the rotation of the Transform. Returns Success.")]
	public class GetRotation : Action
	{
		// Token: 0x060007F4 RID: 2036 RVA: 0x0001B464 File Offset: 0x00019664
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001B4A4 File Offset: 0x000196A4
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.rotation;
			return TaskStatus.Success;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001B4D7 File Offset: 0x000196D7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Quaternion.identity;
		}

		// Token: 0x04000407 RID: 1031
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000408 RID: 1032
		[Tooltip("The rotation of the Transform")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x04000409 RID: 1033
		private Transform targetTransform;

		// Token: 0x0400040A RID: 1034
		private GameObject prevGameObject;
	}
}

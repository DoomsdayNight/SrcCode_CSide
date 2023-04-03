using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000112 RID: 274
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the local rotation of the Transform. Returns Success.")]
	public class GetLocalRotation : Action
	{
		// Token: 0x060007E0 RID: 2016 RVA: 0x0001B188 File Offset: 0x00019388
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001B1C8 File Offset: 0x000193C8
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.localRotation;
			return TaskStatus.Success;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001B1FB File Offset: 0x000193FB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Quaternion.identity;
		}

		// Token: 0x040003F3 RID: 1011
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040003F4 RID: 1012
		[Tooltip("The local rotation of the Transform")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x040003F5 RID: 1013
		private Transform targetTransform;

		// Token: 0x040003F6 RID: 1014
		private GameObject prevGameObject;
	}
}

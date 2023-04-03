using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200010F RID: 271
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the forward vector of the Transform. Returns Success.")]
	public class GetForwardVector : Action
	{
		// Token: 0x060007D4 RID: 2004 RVA: 0x0001AFCC File Offset: 0x000191CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001B00C File Offset: 0x0001920C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.forward;
			return TaskStatus.Success;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001B03F File Offset: 0x0001923F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040003E7 RID: 999
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040003E8 RID: 1000
		[Tooltip("The position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040003E9 RID: 1001
		private Transform targetTransform;

		// Token: 0x040003EA RID: 1002
		private GameObject prevGameObject;
	}
}

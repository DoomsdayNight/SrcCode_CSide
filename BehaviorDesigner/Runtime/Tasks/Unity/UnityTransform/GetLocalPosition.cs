using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000111 RID: 273
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the local position of the Transform. Returns Success.")]
	public class GetLocalPosition : Action
	{
		// Token: 0x060007DC RID: 2012 RVA: 0x0001B0F4 File Offset: 0x000192F4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001B134 File Offset: 0x00019334
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.localPosition;
			return TaskStatus.Success;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001B167 File Offset: 0x00019367
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040003EF RID: 1007
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040003F0 RID: 1008
		[Tooltip("The local position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040003F1 RID: 1009
		private Transform targetTransform;

		// Token: 0x040003F2 RID: 1010
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000110 RID: 272
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the local euler angles of the Transform. Returns Success.")]
	public class GetLocalEulerAngles : Action
	{
		// Token: 0x060007D8 RID: 2008 RVA: 0x0001B060 File Offset: 0x00019260
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001B0A0 File Offset: 0x000192A0
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.localEulerAngles;
			return TaskStatus.Success;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001B0D3 File Offset: 0x000192D3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040003EB RID: 1003
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040003EC RID: 1004
		[Tooltip("The local euler angles of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040003ED RID: 1005
		private Transform targetTransform;

		// Token: 0x040003EE RID: 1006
		private GameObject prevGameObject;
	}
}

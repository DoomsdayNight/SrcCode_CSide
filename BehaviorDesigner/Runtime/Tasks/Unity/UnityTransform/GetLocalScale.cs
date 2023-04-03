using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000113 RID: 275
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the local scale of the Transform. Returns Success.")]
	public class GetLocalScale : Action
	{
		// Token: 0x060007E4 RID: 2020 RVA: 0x0001B21C File Offset: 0x0001941C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001B25C File Offset: 0x0001945C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.localScale;
			return TaskStatus.Success;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001B28F File Offset: 0x0001948F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040003F7 RID: 1015
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040003F8 RID: 1016
		[Tooltip("The local scale of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040003F9 RID: 1017
		private Transform targetTransform;

		// Token: 0x040003FA RID: 1018
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000114 RID: 276
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the parent of the Transform. Returns Success.")]
	public class GetParent : Action
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x0001B2B0 File Offset: 0x000194B0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001B2F0 File Offset: 0x000194F0
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.parent;
			return TaskStatus.Success;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001B323 File Offset: 0x00019523
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = null;
		}

		// Token: 0x040003FB RID: 1019
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040003FC RID: 1020
		[Tooltip("The parent of the Transform")]
		[RequiredField]
		public SharedTransform storeValue;

		// Token: 0x040003FD RID: 1021
		private Transform targetTransform;

		// Token: 0x040003FE RID: 1022
		private GameObject prevGameObject;
	}
}

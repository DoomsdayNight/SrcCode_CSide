using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200010D RID: 269
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the number of children a Transform has. Returns Success.")]
	public class GetChildCount : Action
	{
		// Token: 0x060007CC RID: 1996 RVA: 0x0001AEA8 File Offset: 0x000190A8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001AEE8 File Offset: 0x000190E8
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.childCount;
			return TaskStatus.Success;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001AF1B File Offset: 0x0001911B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0;
		}

		// Token: 0x040003DF RID: 991
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040003E0 RID: 992
		[Tooltip("The number of children")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x040003E1 RID: 993
		private Transform targetTransform;

		// Token: 0x040003E2 RID: 994
		private GameObject prevGameObject;
	}
}

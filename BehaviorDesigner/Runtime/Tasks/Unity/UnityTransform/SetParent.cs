using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000123 RID: 291
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the parent of the Transform. Returns Success.")]
	public class SetParent : Action
	{
		// Token: 0x06000824 RID: 2084 RVA: 0x0001BBFC File Offset: 0x00019DFC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001BC3C File Offset: 0x00019E3C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.targetTransform.parent = this.parent.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001BC6F File Offset: 0x00019E6F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.parent = null;
		}

		// Token: 0x0400043C RID: 1084
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400043D RID: 1085
		[Tooltip("The parent of the Transform")]
		public SharedTransform parent;

		// Token: 0x0400043E RID: 1086
		private Transform targetTransform;

		// Token: 0x0400043F RID: 1087
		private GameObject prevGameObject;
	}
}

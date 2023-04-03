using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200010C RID: 268
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the transform child at the specified index. Returns Success.")]
	public class GetChild : Action
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x0001AE04 File Offset: 0x00019004
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001AE44 File Offset: 0x00019044
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				Debug.LogWarning("Transform is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.GetChild(this.index.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001AE82 File Offset: 0x00019082
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.storeValue = null;
		}

		// Token: 0x040003DA RID: 986
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040003DB RID: 987
		[Tooltip("The index of the child")]
		public SharedInt index;

		// Token: 0x040003DC RID: 988
		[Tooltip("The child of the Transform")]
		[RequiredField]
		public SharedTransform storeValue;

		// Token: 0x040003DD RID: 989
		private Transform targetTransform;

		// Token: 0x040003DE RID: 990
		private GameObject prevGameObject;
	}
}

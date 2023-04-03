using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000283 RID: 643
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Gets the avatar delta rotation for the last evaluated frame. Returns Success.")]
	public class GetDeltaRotation : Action
	{
		// Token: 0x06000D1E RID: 3358 RVA: 0x000271AC File Offset: 0x000253AC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x000271EC File Offset: 0x000253EC
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.deltaRotation;
			return TaskStatus.Success;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0002721F File Offset: 0x0002541F
		public override void OnReset()
		{
			if (this.storeValue != null)
			{
				this.storeValue.Value = Quaternion.identity;
			}
		}

		// Token: 0x040008DA RID: 2266
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008DB RID: 2267
		[Tooltip("The avatar delta rotation")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x040008DC RID: 2268
		private Animator animator;

		// Token: 0x040008DD RID: 2269
		private GameObject prevGameObject;
	}
}

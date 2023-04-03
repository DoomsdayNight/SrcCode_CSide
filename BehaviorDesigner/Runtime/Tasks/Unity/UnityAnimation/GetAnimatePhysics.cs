using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x020002A1 RID: 673
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Stores the animate physics value. Returns Success.")]
	public class GetAnimatePhysics : Action
	{
		// Token: 0x06000D9B RID: 3483 RVA: 0x00028760 File Offset: 0x00026960
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x000287A0 File Offset: 0x000269A0
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animation.animatePhysics;
			return TaskStatus.Success;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x000287D3 File Offset: 0x000269D3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue.Value = false;
		}

		// Token: 0x0400096F RID: 2415
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000970 RID: 2416
		[Tooltip("Are the if animations are executed in the physics loop?")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x04000971 RID: 2417
		private Animation animation;

		// Token: 0x04000972 RID: 2418
		private GameObject prevGameObject;
	}
}

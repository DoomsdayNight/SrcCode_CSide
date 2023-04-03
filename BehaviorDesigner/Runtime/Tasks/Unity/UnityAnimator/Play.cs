using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200028F RID: 655
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Plays an animator state. Returns Success.")]
	public class Play : Action
	{
		// Token: 0x06000D4D RID: 3405 RVA: 0x00027960 File Offset: 0x00025B60
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000279A0 File Offset: 0x00025BA0
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.animator.Play(this.stateName.Value, this.layer, this.normalizedTime);
			return TaskStatus.Success;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000279DF File Offset: 0x00025BDF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stateName = "";
			this.layer = -1;
			this.normalizedTime = float.NegativeInfinity;
		}

		// Token: 0x04000912 RID: 2322
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000913 RID: 2323
		[Tooltip("The name of the state")]
		public SharedString stateName;

		// Token: 0x04000914 RID: 2324
		[Tooltip("The layer where the state is")]
		public int layer = -1;

		// Token: 0x04000915 RID: 2325
		[Tooltip("The normalized time at which the state will play")]
		public float normalizedTime = float.NegativeInfinity;

		// Token: 0x04000916 RID: 2326
		private Animator animator;

		// Token: 0x04000917 RID: 2327
		private GameObject prevGameObject;
	}
}

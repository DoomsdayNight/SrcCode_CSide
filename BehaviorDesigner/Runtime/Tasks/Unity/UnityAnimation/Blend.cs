using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x0200029E RID: 670
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Blends the animation. Returns Success.")]
	public class Blend : Action
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x00028444 File Offset: 0x00026644
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00028484 File Offset: 0x00026684
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return TaskStatus.Failure;
			}
			this.animation.Blend(this.animationName.Value, this.targetWeight, this.fadeLength);
			return TaskStatus.Success;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x000284C3 File Offset: 0x000266C3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName = "";
			this.targetWeight = 1f;
			this.fadeLength = 0.3f;
		}

		// Token: 0x0400095B RID: 2395
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400095C RID: 2396
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x0400095D RID: 2397
		[Tooltip("The weight the animation should blend to")]
		public float targetWeight = 1f;

		// Token: 0x0400095E RID: 2398
		[Tooltip("The amount of time it takes to blend")]
		public float fadeLength = 0.3f;

		// Token: 0x0400095F RID: 2399
		private Animation animation;

		// Token: 0x04000960 RID: 2400
		private GameObject prevGameObject;
	}
}

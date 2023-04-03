using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x020002A0 RID: 672
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Cross fades an animation after previous animations has finished playing. Returns Success.")]
	public class CrossFadeQueued : Action
	{
		// Token: 0x06000D97 RID: 3479 RVA: 0x00028688 File Offset: 0x00026888
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x000286C8 File Offset: 0x000268C8
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return TaskStatus.Failure;
			}
			this.animation.CrossFadeQueued(this.animationName.Value, this.fadeLength, this.queue, this.playMode);
			return TaskStatus.Success;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00028719 File Offset: 0x00026919
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
			this.fadeLength = 0.3f;
			this.queue = QueueMode.CompleteOthers;
			this.playMode = PlayMode.StopSameLayer;
		}

		// Token: 0x04000968 RID: 2408
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000969 RID: 2409
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x0400096A RID: 2410
		[Tooltip("The amount of time it takes to blend")]
		public float fadeLength = 0.3f;

		// Token: 0x0400096B RID: 2411
		[Tooltip("Specifies when the animation should start playing")]
		public QueueMode queue;

		// Token: 0x0400096C RID: 2412
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x0400096D RID: 2413
		private Animation animation;

		// Token: 0x0400096E RID: 2414
		private GameObject prevGameObject;
	}
}

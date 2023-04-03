using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x020002A4 RID: 676
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Plays an animation after previous animations has finished playing. Returns Success.")]
	public class PlayQueued : Action
	{
		// Token: 0x06000DA7 RID: 3495 RVA: 0x00028988 File Offset: 0x00026B88
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x000289C8 File Offset: 0x00026BC8
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return TaskStatus.Failure;
			}
			this.animation.PlayQueued(this.animationName.Value, this.queue, this.playMode);
			return TaskStatus.Success;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00028A08 File Offset: 0x00026C08
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
			this.queue = QueueMode.CompleteOthers;
			this.playMode = PlayMode.StopSameLayer;
		}

		// Token: 0x0400097C RID: 2428
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400097D RID: 2429
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x0400097E RID: 2430
		[Tooltip("Specifies when the animation should start playing")]
		public QueueMode queue;

		// Token: 0x0400097F RID: 2431
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x04000980 RID: 2432
		private Animation animation;

		// Token: 0x04000981 RID: 2433
		private GameObject prevGameObject;
	}
}

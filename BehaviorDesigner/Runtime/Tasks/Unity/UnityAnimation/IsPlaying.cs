using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x020002A2 RID: 674
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Returns Success if the animation is currently playing.")]
	public class IsPlaying : Conditional
	{
		// Token: 0x06000D9F RID: 3487 RVA: 0x000287F0 File Offset: 0x000269F0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00028830 File Offset: 0x00026A30
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return TaskStatus.Failure;
			}
			if (string.IsNullOrEmpty(this.animationName.Value))
			{
				if (!this.animation.isPlaying)
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Success;
			}
			else
			{
				if (!this.animation.IsPlaying(this.animationName.Value))
				{
					return TaskStatus.Failure;
				}
				return TaskStatus.Success;
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00028895 File Offset: 0x00026A95
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
		}

		// Token: 0x04000973 RID: 2419
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000974 RID: 2420
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x04000975 RID: 2421
		private Animation animation;

		// Token: 0x04000976 RID: 2422
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x020002A3 RID: 675
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Plays animation without any blending. Returns Success.")]
	public class Play : Action
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x000288B8 File Offset: 0x00026AB8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000288F8 File Offset: 0x00026AF8
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return TaskStatus.Failure;
			}
			if (string.IsNullOrEmpty(this.animationName.Value))
			{
				this.animation.Play();
			}
			else
			{
				this.animation.Play(this.animationName.Value, this.playMode);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0002895D File Offset: 0x00026B5D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
			this.playMode = PlayMode.StopSameLayer;
		}

		// Token: 0x04000977 RID: 2423
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000978 RID: 2424
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x04000979 RID: 2425
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x0400097A RID: 2426
		private Animation animation;

		// Token: 0x0400097B RID: 2427
		private GameObject prevGameObject;
	}
}

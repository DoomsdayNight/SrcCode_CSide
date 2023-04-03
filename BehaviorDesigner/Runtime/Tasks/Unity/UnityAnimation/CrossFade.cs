using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x0200029F RID: 671
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Fades the animation over a period of time and fades other animations out. Returns Success.")]
	public class CrossFade : Action
	{
		// Token: 0x06000D93 RID: 3475 RVA: 0x00028510 File Offset: 0x00026710
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00028550 File Offset: 0x00026750
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return TaskStatus.Failure;
			}
			this.animation[this.animationName.Value].speed = this.animationSpeed.Value;
			if (this.animation[this.animationName.Value].speed < 0f)
			{
				this.animation[this.animationName.Value].time = this.animation[this.animationName.Value].length;
			}
			this.animation.CrossFade(this.animationName.Value, this.fadeLength.Value, this.playMode);
			return TaskStatus.Success;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0002861D File Offset: 0x0002681D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
			this.animationSpeed = 1f;
			this.fadeLength = 0.3f;
			this.playMode = PlayMode.StopSameLayer;
		}

		// Token: 0x04000961 RID: 2401
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000962 RID: 2402
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x04000963 RID: 2403
		[Tooltip("The speed of the animation. Use a negative value to play the animation backwards")]
		public SharedFloat animationSpeed = 1f;

		// Token: 0x04000964 RID: 2404
		[Tooltip("The amount of time it takes to blend")]
		public SharedFloat fadeLength = 0.3f;

		// Token: 0x04000965 RID: 2405
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x04000966 RID: 2406
		private Animation animation;

		// Token: 0x04000967 RID: 2407
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x020002A8 RID: 680
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Sets the wrap mode to the specified value. Returns Success.")]
	public class SetWrapMode : Action
	{
		// Token: 0x06000DB7 RID: 3511 RVA: 0x00028C04 File Offset: 0x00026E04
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00028C44 File Offset: 0x00026E44
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return TaskStatus.Failure;
			}
			this.animation.wrapMode = this.wrapMode;
			return TaskStatus.Success;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00028C72 File Offset: 0x00026E72
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.wrapMode = WrapMode.Default;
		}

		// Token: 0x0400098D RID: 2445
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400098E RID: 2446
		[Tooltip("How should time beyond the playback range of the clip be treated?")]
		public WrapMode wrapMode;

		// Token: 0x0400098F RID: 2447
		private Animation animation;

		// Token: 0x04000990 RID: 2448
		private GameObject prevGameObject;
	}
}

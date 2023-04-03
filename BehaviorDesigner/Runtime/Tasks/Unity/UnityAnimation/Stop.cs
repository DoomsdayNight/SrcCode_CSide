using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x020002A9 RID: 681
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Stops an animation. Stops all animations if animationName is blank. Returns Success.")]
	public class Stop : Action
	{
		// Token: 0x06000DBB RID: 3515 RVA: 0x00028C8C File Offset: 0x00026E8C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00028CCC File Offset: 0x00026ECC
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return TaskStatus.Failure;
			}
			if (string.IsNullOrEmpty(this.animationName.Value))
			{
				this.animation.Stop();
			}
			else
			{
				this.animation.Stop(this.animationName.Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00028D29 File Offset: 0x00026F29
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName = "";
		}

		// Token: 0x04000991 RID: 2449
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000992 RID: 2450
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x04000993 RID: 2451
		private Animation animation;

		// Token: 0x04000994 RID: 2452
		private GameObject prevGameObject;
	}
}

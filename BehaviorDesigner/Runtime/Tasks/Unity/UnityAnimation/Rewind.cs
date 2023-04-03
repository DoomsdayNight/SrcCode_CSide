using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x020002A5 RID: 677
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Rewinds an animation. Rewinds all animations if animationName is blank. Returns Success.")]
	public class Rewind : Action
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x00028A38 File Offset: 0x00026C38
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00028A78 File Offset: 0x00026C78
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return TaskStatus.Failure;
			}
			if (string.IsNullOrEmpty(this.animationName.Value))
			{
				this.animation.Rewind();
			}
			else
			{
				this.animation.Rewind(this.animationName.Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00028AD5 File Offset: 0x00026CD5
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = "";
		}

		// Token: 0x04000982 RID: 2434
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000983 RID: 2435
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x04000984 RID: 2436
		private Animation animation;

		// Token: 0x04000985 RID: 2437
		private GameObject prevGameObject;
	}
}

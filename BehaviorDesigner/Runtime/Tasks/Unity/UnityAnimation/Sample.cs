using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x020002A6 RID: 678
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Samples animations at the current state. Returns Success.")]
	public class Sample : Action
	{
		// Token: 0x06000DAF RID: 3503 RVA: 0x00028AF8 File Offset: 0x00026CF8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00028B38 File Offset: 0x00026D38
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return TaskStatus.Failure;
			}
			this.animation.Sample();
			return TaskStatus.Success;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00028B60 File Offset: 0x00026D60
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000986 RID: 2438
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000987 RID: 2439
		private Animation animation;

		// Token: 0x04000988 RID: 2440
		private GameObject prevGameObject;
	}
}

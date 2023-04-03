using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x020002A7 RID: 679
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Sets animate physics to the specified value. Returns Success.")]
	public class SetAnimatePhysics : Action
	{
		// Token: 0x06000DB3 RID: 3507 RVA: 0x00028B74 File Offset: 0x00026D74
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00028BB4 File Offset: 0x00026DB4
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				Debug.LogWarning("Animation is null");
				return TaskStatus.Failure;
			}
			this.animation.animatePhysics = this.animatePhysics.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00028BE7 File Offset: 0x00026DE7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animatePhysics.Value = false;
		}

		// Token: 0x04000989 RID: 2441
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400098A RID: 2442
		[Tooltip("Are animations executed in the physics loop?")]
		public SharedBool animatePhysics;

		// Token: 0x0400098B RID: 2443
		private Animation animation;

		// Token: 0x0400098C RID: 2444
		private GameObject prevGameObject;
	}
}

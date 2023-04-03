using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200027F RID: 639
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Creates a dynamic transition between the current state and the destination state. Returns Success.")]
	public class CrossFade : Action
	{
		// Token: 0x06000D0E RID: 3342 RVA: 0x00026EF0 File Offset: 0x000250F0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00026F30 File Offset: 0x00025130
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.animator.CrossFade(this.stateName.Value, this.transitionDuration.Value, this.layer, this.normalizedTime);
			return TaskStatus.Success;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00026F85 File Offset: 0x00025185
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stateName = "";
			this.transitionDuration = 0f;
			this.layer = -1;
			this.normalizedTime = float.NegativeInfinity;
		}

		// Token: 0x040008C6 RID: 2246
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008C7 RID: 2247
		[Tooltip("The name of the state")]
		public SharedString stateName;

		// Token: 0x040008C8 RID: 2248
		[Tooltip("The duration of the transition. Value is in source state normalized time")]
		public SharedFloat transitionDuration;

		// Token: 0x040008C9 RID: 2249
		[Tooltip("The layer where the state is")]
		public int layer = -1;

		// Token: 0x040008CA RID: 2250
		[Tooltip("The normalized time at which the state will play")]
		public float normalizedTime = float.NegativeInfinity;

		// Token: 0x040008CB RID: 2251
		private Animator animator;

		// Token: 0x040008CC RID: 2252
		private GameObject prevGameObject;
	}
}

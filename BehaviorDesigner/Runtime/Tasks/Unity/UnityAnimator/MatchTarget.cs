using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200028E RID: 654
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Automatically adjust the gameobject position and rotation so that the AvatarTarget reaches the matchPosition when the current state is at the specified progress. Returns Success.")]
	public class MatchTarget : Action
	{
		// Token: 0x06000D49 RID: 3401 RVA: 0x00027838 File Offset: 0x00025A38
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x00027878 File Offset: 0x00025A78
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.animator.MatchTarget(this.matchPosition.Value, this.matchRotation.Value, this.targetBodyPart, new MatchTargetWeightMask(this.weightMaskPosition, this.weightMaskRotation), this.startNormalizedTime, this.targetNormalizedTime);
			return TaskStatus.Success;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x000278E4 File Offset: 0x00025AE4
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.matchPosition = Vector3.zero;
			this.matchRotation = Quaternion.identity;
			this.targetBodyPart = AvatarTarget.Root;
			this.weightMaskPosition = Vector3.zero;
			this.weightMaskRotation = 0f;
			this.startNormalizedTime = 0f;
			this.targetNormalizedTime = 1f;
		}

		// Token: 0x04000908 RID: 2312
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000909 RID: 2313
		[Tooltip("The position we want the body part to reach")]
		public SharedVector3 matchPosition;

		// Token: 0x0400090A RID: 2314
		[Tooltip("The rotation in which we want the body part to be")]
		public SharedQuaternion matchRotation;

		// Token: 0x0400090B RID: 2315
		[Tooltip("The body part that is involved in the match")]
		public AvatarTarget targetBodyPart;

		// Token: 0x0400090C RID: 2316
		[Tooltip("Weights for matching position")]
		public Vector3 weightMaskPosition;

		// Token: 0x0400090D RID: 2317
		[Tooltip("Weights for matching rotation")]
		public float weightMaskRotation;

		// Token: 0x0400090E RID: 2318
		[Tooltip("Start time within the animation clip")]
		public float startNormalizedTime;

		// Token: 0x0400090F RID: 2319
		[Tooltip("End time within the animation clip")]
		public float targetNormalizedTime = 1f;

		// Token: 0x04000910 RID: 2320
		private Animator animator;

		// Token: 0x04000911 RID: 2321
		private GameObject prevGameObject;
	}
}

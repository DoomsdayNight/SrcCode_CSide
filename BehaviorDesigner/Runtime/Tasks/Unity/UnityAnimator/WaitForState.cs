using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200029D RID: 669
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Waits for the Animator to reach the specified state.")]
	public class WaitForState : Action
	{
		// Token: 0x06000D8A RID: 3466 RVA: 0x0002830D File Offset: 0x0002650D
		public override void OnAwake()
		{
			this.stateHash = Animator.StringToHash(this.stateName.Value);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00028328 File Offset: 0x00026528
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
				if (!this.animator.HasState(this.layer.Value, this.stateHash))
				{
					Debug.LogError("Error: The Animator does not have the state " + this.stateName.Value + " on layer " + this.layer.Value.ToString());
				}
			}
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x000283B8 File Offset: 0x000265B8
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			if (this.animator.GetCurrentAnimatorStateInfo(this.layer.Value).shortNameHash == this.stateHash)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00028408 File Offset: 0x00026608
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stateName = "";
			this.layer = -1;
		}

		// Token: 0x04000955 RID: 2389
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000956 RID: 2390
		[Tooltip("The name of the state")]
		public SharedString stateName;

		// Token: 0x04000957 RID: 2391
		[Tooltip("The layer where the state is")]
		public SharedInt layer = -1;

		// Token: 0x04000958 RID: 2392
		private Animator animator;

		// Token: 0x04000959 RID: 2393
		private GameObject prevGameObject;

		// Token: 0x0400095A RID: 2394
		private int stateHash;
	}
}

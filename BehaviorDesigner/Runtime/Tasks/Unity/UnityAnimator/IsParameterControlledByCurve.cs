using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200028D RID: 653
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Returns success if the specified parameter is controlled by an additional curve on an animation.")]
	public class IsParameterControlledByCurve : Conditional
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x000277A0 File Offset: 0x000259A0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000277E0 File Offset: 0x000259E0
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			if (!this.animator.IsParameterControlledByCurve(this.paramaterName.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00027817 File Offset: 0x00025A17
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
		}

		// Token: 0x04000904 RID: 2308
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000905 RID: 2309
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04000906 RID: 2310
		private Animator animator;

		// Token: 0x04000907 RID: 2311
		private GameObject prevGameObject;
	}
}

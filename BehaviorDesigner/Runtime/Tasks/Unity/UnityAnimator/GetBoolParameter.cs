using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000281 RID: 641
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stores the bool parameter on an animator. Returns Success.")]
	public class GetBoolParameter : Action
	{
		// Token: 0x06000D16 RID: 3350 RVA: 0x0002706C File Offset: 0x0002526C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x000270AC File Offset: 0x000252AC
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.GetBool(this.paramaterName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x000270EA File Offset: 0x000252EA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = "";
			this.storeValue = false;
		}

		// Token: 0x040008D1 RID: 2257
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008D2 RID: 2258
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x040008D3 RID: 2259
		[Tooltip("The value of the bool parameter")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040008D4 RID: 2260
		private Animator animator;

		// Token: 0x040008D5 RID: 2261
		private GameObject prevGameObject;
	}
}

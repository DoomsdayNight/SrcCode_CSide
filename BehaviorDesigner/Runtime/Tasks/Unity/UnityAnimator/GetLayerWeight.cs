using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000287 RID: 647
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stores the layer's weight. Returns Success.")]
	public class GetLayerWeight : Action
	{
		// Token: 0x06000D2E RID: 3374 RVA: 0x00027434 File Offset: 0x00025634
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00027474 File Offset: 0x00025674
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				Debug.LogWarning("Animator is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.GetLayerWeight(this.index.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x000274B2 File Offset: 0x000256B2
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.storeValue = 0f;
		}

		// Token: 0x040008EC RID: 2284
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008ED RID: 2285
		[Tooltip("The index of the layer")]
		public SharedInt index;

		// Token: 0x040008EE RID: 2286
		[Tooltip("The value of the float parameter")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040008EF RID: 2287
		private Animator animator;

		// Token: 0x040008F0 RID: 2288
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000249 RID: 585
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Sets the step offset of the CharacterController. Returns Success.")]
	public class SetStepOffset : Action
	{
		// Token: 0x06000C39 RID: 3129 RVA: 0x00025088 File Offset: 0x00023288
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x000250C8 File Offset: 0x000232C8
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return TaskStatus.Failure;
			}
			this.characterController.stepOffset = this.stepOffset.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000250FB File Offset: 0x000232FB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stepOffset = 0f;
		}

		// Token: 0x040007F8 RID: 2040
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007F9 RID: 2041
		[Tooltip("The step offset of the CharacterController")]
		public SharedFloat stepOffset;

		// Token: 0x040007FA RID: 2042
		private CharacterController characterController;

		// Token: 0x040007FB RID: 2043
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000243 RID: 579
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Returns Success if the character is grounded, otherwise Failure.")]
	public class IsGrounded : Conditional
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x00024D20 File Offset: 0x00022F20
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00024D60 File Offset: 0x00022F60
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return TaskStatus.Failure;
			}
			if (!this.characterController.isGrounded)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00024D8C File Offset: 0x00022F8C
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040007E1 RID: 2017
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007E2 RID: 2018
		private CharacterController characterController;

		// Token: 0x040007E3 RID: 2019
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000247 RID: 583
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Sets the radius of the CharacterController. Returns Success.")]
	public class SetRadius : Action
	{
		// Token: 0x06000C31 RID: 3121 RVA: 0x00024F60 File Offset: 0x00023160
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00024FA0 File Offset: 0x000231A0
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return TaskStatus.Failure;
			}
			this.characterController.radius = this.radius.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00024FD3 File Offset: 0x000231D3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.radius = 0f;
		}

		// Token: 0x040007F0 RID: 2032
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007F1 RID: 2033
		[Tooltip("The radius of the CharacterController")]
		public SharedFloat radius;

		// Token: 0x040007F2 RID: 2034
		private CharacterController characterController;

		// Token: 0x040007F3 RID: 2035
		private GameObject prevGameObject;
	}
}

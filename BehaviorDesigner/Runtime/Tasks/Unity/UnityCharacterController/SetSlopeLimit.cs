using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000248 RID: 584
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Sets the slope limit of the CharacterController. Returns Success.")]
	public class SetSlopeLimit : Action
	{
		// Token: 0x06000C35 RID: 3125 RVA: 0x00024FF4 File Offset: 0x000231F4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00025034 File Offset: 0x00023234
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return TaskStatus.Failure;
			}
			this.characterController.slopeLimit = this.slopeLimit.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00025067 File Offset: 0x00023267
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.slopeLimit = 0f;
		}

		// Token: 0x040007F4 RID: 2036
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007F5 RID: 2037
		[Tooltip("The slope limit of the CharacterController")]
		public SharedFloat slopeLimit;

		// Token: 0x040007F6 RID: 2038
		private CharacterController characterController;

		// Token: 0x040007F7 RID: 2039
		private GameObject prevGameObject;
	}
}

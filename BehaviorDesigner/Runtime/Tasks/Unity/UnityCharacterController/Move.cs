using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000244 RID: 580
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("A more complex move function taking absolute movement deltas. Returns Success.")]
	public class Move : Action
	{
		// Token: 0x06000C25 RID: 3109 RVA: 0x00024DA0 File Offset: 0x00022FA0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00024DE0 File Offset: 0x00022FE0
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return TaskStatus.Failure;
			}
			this.characterController.Move(this.motion.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00024E14 File Offset: 0x00023014
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.motion = Vector3.zero;
		}

		// Token: 0x040007E4 RID: 2020
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007E5 RID: 2021
		[Tooltip("The amount to move")]
		public SharedVector3 motion;

		// Token: 0x040007E6 RID: 2022
		private CharacterController characterController;

		// Token: 0x040007E7 RID: 2023
		private GameObject prevGameObject;
	}
}

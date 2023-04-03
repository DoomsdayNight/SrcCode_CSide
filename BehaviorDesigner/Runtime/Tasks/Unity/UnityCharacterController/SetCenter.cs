using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000245 RID: 581
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Sets the center of the CharacterController. Returns Success.")]
	public class SetCenter : Action
	{
		// Token: 0x06000C29 RID: 3113 RVA: 0x00024E38 File Offset: 0x00023038
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00024E78 File Offset: 0x00023078
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return TaskStatus.Failure;
			}
			this.characterController.center = this.center.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00024EAB File Offset: 0x000230AB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.center = Vector3.zero;
		}

		// Token: 0x040007E8 RID: 2024
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007E9 RID: 2025
		[Tooltip("The center of the CharacterController")]
		public SharedVector3 center;

		// Token: 0x040007EA RID: 2026
		private CharacterController characterController;

		// Token: 0x040007EB RID: 2027
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000246 RID: 582
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Sets the height of the CharacterController. Returns Success.")]
	public class SetHeight : Action
	{
		// Token: 0x06000C2D RID: 3117 RVA: 0x00024ECC File Offset: 0x000230CC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00024F0C File Offset: 0x0002310C
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return TaskStatus.Failure;
			}
			this.characterController.height = this.height.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00024F3F File Offset: 0x0002313F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.height = 0f;
		}

		// Token: 0x040007EC RID: 2028
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007ED RID: 2029
		[Tooltip("The height of the CharacterController")]
		public SharedFloat height;

		// Token: 0x040007EE RID: 2030
		private CharacterController characterController;

		// Token: 0x040007EF RID: 2031
		private GameObject prevGameObject;
	}
}

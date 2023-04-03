using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x0200023C RID: 572
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Stores the center of the CharacterController. Returns Success.")]
	public class GetCenter : Action
	{
		// Token: 0x06000C04 RID: 3076 RVA: 0x00024904 File Offset: 0x00022B04
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00024944 File Offset: 0x00022B44
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.characterController.center;
			return TaskStatus.Success;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00024977 File Offset: 0x00022B77
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040007C5 RID: 1989
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007C6 RID: 1990
		[Tooltip("The center of the CharacterController")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040007C7 RID: 1991
		private CharacterController characterController;

		// Token: 0x040007C8 RID: 1992
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x0200023F RID: 575
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Stores the slope limit of the CharacterController. Returns Success.")]
	public class GetSlopeLimit : Action
	{
		// Token: 0x06000C10 RID: 3088 RVA: 0x00024AC0 File Offset: 0x00022CC0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00024B00 File Offset: 0x00022D00
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.characterController.slopeLimit;
			return TaskStatus.Success;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00024B33 File Offset: 0x00022D33
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040007D1 RID: 2001
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007D2 RID: 2002
		[Tooltip("The slope limit of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040007D3 RID: 2003
		private CharacterController characterController;

		// Token: 0x040007D4 RID: 2004
		private GameObject prevGameObject;
	}
}

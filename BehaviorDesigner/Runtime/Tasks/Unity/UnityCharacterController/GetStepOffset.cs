using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000240 RID: 576
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Stores the step offset of the CharacterController. Returns Success.")]
	public class GetStepOffset : Action
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x00024B54 File Offset: 0x00022D54
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00024B94 File Offset: 0x00022D94
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.characterController.stepOffset;
			return TaskStatus.Success;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00024BC7 File Offset: 0x00022DC7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040007D5 RID: 2005
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007D6 RID: 2006
		[Tooltip("The step offset of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040007D7 RID: 2007
		private CharacterController characterController;

		// Token: 0x040007D8 RID: 2008
		private GameObject prevGameObject;
	}
}

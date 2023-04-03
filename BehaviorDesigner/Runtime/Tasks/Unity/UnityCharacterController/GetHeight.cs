using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x0200023D RID: 573
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Stores the height of the CharacterController. Returns Success.")]
	public class GetHeight : Action
	{
		// Token: 0x06000C08 RID: 3080 RVA: 0x00024998 File Offset: 0x00022B98
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x000249D8 File Offset: 0x00022BD8
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.characterController.height;
			return TaskStatus.Success;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00024A0B File Offset: 0x00022C0B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040007C9 RID: 1993
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007CA RID: 1994
		[Tooltip("The height of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040007CB RID: 1995
		private CharacterController characterController;

		// Token: 0x040007CC RID: 1996
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x0200023E RID: 574
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Stores the radius of the CharacterController. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x06000C0C RID: 3084 RVA: 0x00024A2C File Offset: 0x00022C2C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00024A6C File Offset: 0x00022C6C
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.characterController.radius;
			return TaskStatus.Success;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00024A9F File Offset: 0x00022C9F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040007CD RID: 1997
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007CE RID: 1998
		[Tooltip("The radius of the CharacterController")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040007CF RID: 1999
		private CharacterController characterController;

		// Token: 0x040007D0 RID: 2000
		private GameObject prevGameObject;
	}
}

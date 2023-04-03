using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000241 RID: 577
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Stores the velocity of the CharacterController. Returns Success.")]
	public class GetVelocity : Action
	{
		// Token: 0x06000C18 RID: 3096 RVA: 0x00024BE8 File Offset: 0x00022DE8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00024C28 File Offset: 0x00022E28
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.characterController.velocity;
			return TaskStatus.Success;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00024C5B File Offset: 0x00022E5B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040007D9 RID: 2009
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007DA RID: 2010
		[Tooltip("The velocity of the CharacterController")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040007DB RID: 2011
		private CharacterController characterController;

		// Token: 0x040007DC RID: 2012
		private GameObject prevGameObject;
	}
}

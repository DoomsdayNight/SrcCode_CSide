using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x02000242 RID: 578
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Returns Success if the collider hit another object, otherwise Failure.")]
	public class HasColliderHit : Conditional
	{
		// Token: 0x06000C1C RID: 3100 RVA: 0x00024C7C File Offset: 0x00022E7C
		public override TaskStatus OnUpdate()
		{
			if (!this.enteredCollision)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00024C89 File Offset: 0x00022E89
		public override void OnEnd()
		{
			this.enteredCollision = false;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00024C94 File Offset: 0x00022E94
		public override void OnControllerColliderHit(ControllerColliderHit hit)
		{
			if (string.IsNullOrEmpty(this.tag.Value) || this.tag.Value.Equals(hit.gameObject.tag))
			{
				this.collidedGameObject.Value = hit.gameObject;
				this.enteredCollision = true;
			}
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00024CE8 File Offset: 0x00022EE8
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.tag = "";
			this.collidedGameObject = null;
		}

		// Token: 0x040007DD RID: 2013
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007DE RID: 2014
		[Tooltip("The tag of the GameObject to check for a collision against")]
		public SharedString tag = "";

		// Token: 0x040007DF RID: 2015
		[Tooltip("The object that started the collision")]
		public SharedGameObject collidedGameObject;

		// Token: 0x040007E0 RID: 2016
		private bool enteredCollision;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController
{
	// Token: 0x0200024A RID: 586
	[TaskCategory("Unity/CharacterController")]
	[TaskDescription("Moves the character with speed. Returns Success.")]
	public class SimpleMove : Action
	{
		// Token: 0x06000C3D RID: 3133 RVA: 0x0002511C File Offset: 0x0002331C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.characterController = defaultGameObject.GetComponent<CharacterController>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0002515C File Offset: 0x0002335C
		public override TaskStatus OnUpdate()
		{
			if (this.characterController == null)
			{
				Debug.LogWarning("CharacterController is null");
				return TaskStatus.Failure;
			}
			this.characterController.SimpleMove(this.speed.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00025190 File Offset: 0x00023390
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.speed = Vector3.zero;
		}

		// Token: 0x040007FC RID: 2044
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007FD RID: 2045
		[Tooltip("The speed of the movement")]
		public SharedVector3 speed;

		// Token: 0x040007FE RID: 2046
		private CharacterController characterController;

		// Token: 0x040007FF RID: 2047
		private GameObject prevGameObject;
	}
}

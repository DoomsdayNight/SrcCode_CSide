using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000189 RID: 393
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the is kinematic value of the Rigidbody. Returns Success.")]
	public class GetIsKinematic : Action
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x0001ED24 File Offset: 0x0001CF24
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0001ED64 File Offset: 0x0001CF64
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.isKinematic;
			return TaskStatus.Success;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0001ED97 File Offset: 0x0001CF97
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = false;
		}

		// Token: 0x04000579 RID: 1401
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400057A RID: 1402
		[Tooltip("The is kinematic value of the Rigidbody")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x0400057B RID: 1403
		private Rigidbody rigidbody;

		// Token: 0x0400057C RID: 1404
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000171 RID: 369
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Returns Success if the Rigidbody2D is kinematic, otherwise Failure.")]
	public class IsKinematic : Conditional
	{
		// Token: 0x0600092D RID: 2349 RVA: 0x0001DED8 File Offset: 0x0001C0D8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0001DF18 File Offset: 0x0001C118
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			if (!this.rigidbody2D.isKinematic)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0001DF44 File Offset: 0x0001C144
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000513 RID: 1299
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000514 RID: 1300
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000515 RID: 1301
		private GameObject prevGameObject;
	}
}

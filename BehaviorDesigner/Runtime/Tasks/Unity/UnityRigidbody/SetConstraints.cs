using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000196 RID: 406
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the constraints of the Rigidbody. Returns Success.")]
	public class SetConstraints : Action
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x0001F478 File Offset: 0x0001D678
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0001F4B8 File Offset: 0x0001D6B8
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.constraints = this.constraints;
			return TaskStatus.Success;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0001F4E6 File Offset: 0x0001D6E6
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.constraints = RigidbodyConstraints.None;
		}

		// Token: 0x040005AB RID: 1451
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005AC RID: 1452
		[Tooltip("The constraints of the Rigidbody")]
		public RigidbodyConstraints constraints;

		// Token: 0x040005AD RID: 1453
		private Rigidbody rigidbody;

		// Token: 0x040005AE RID: 1454
		private GameObject prevGameObject;
	}
}

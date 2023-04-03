using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000198 RID: 408
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the freeze rotation value of the Rigidbody. Returns Success.")]
	public class SetFreezeRotation : Action
	{
		// Token: 0x060009C9 RID: 2505 RVA: 0x0001F594 File Offset: 0x0001D794
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0001F5D4 File Offset: 0x0001D7D4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.freezeRotation = this.freezeRotation.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0001F607 File Offset: 0x0001D807
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.freezeRotation = false;
		}

		// Token: 0x040005B3 RID: 1459
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005B4 RID: 1460
		[Tooltip("The freeze rotation value of the Rigidbody")]
		public SharedBool freezeRotation;

		// Token: 0x040005B5 RID: 1461
		private Rigidbody rigidbody;

		// Token: 0x040005B6 RID: 1462
		private GameObject prevGameObject;
	}
}

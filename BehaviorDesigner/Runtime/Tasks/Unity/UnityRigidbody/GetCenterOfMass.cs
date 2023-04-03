using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000186 RID: 390
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the center of mass of the Rigidbody. Returns Success.")]
	public class GetCenterOfMass : Action
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x0001EB6C File Offset: 0x0001CD6C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001EBAC File Offset: 0x0001CDAC
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.centerOfMass;
			return TaskStatus.Success;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001EBDF File Offset: 0x0001CDDF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x0400056D RID: 1389
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400056E RID: 1390
		[Tooltip("The center of mass of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x0400056F RID: 1391
		private Rigidbody rigidbody;

		// Token: 0x04000570 RID: 1392
		private GameObject prevGameObject;
	}
}

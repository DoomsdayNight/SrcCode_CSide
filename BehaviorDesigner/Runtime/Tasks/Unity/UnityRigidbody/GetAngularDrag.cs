using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000184 RID: 388
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the angular drag of the Rigidbody. Returns Success.")]
	public class GetAngularDrag : Action
	{
		// Token: 0x06000979 RID: 2425 RVA: 0x0001EA44 File Offset: 0x0001CC44
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0001EA84 File Offset: 0x0001CC84
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.angularDrag;
			return TaskStatus.Success;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0001EAB7 File Offset: 0x0001CCB7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000565 RID: 1381
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000566 RID: 1382
		[Tooltip("The angular drag of the Rigidbody")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000567 RID: 1383
		private Rigidbody rigidbody;

		// Token: 0x04000568 RID: 1384
		private GameObject prevGameObject;
	}
}

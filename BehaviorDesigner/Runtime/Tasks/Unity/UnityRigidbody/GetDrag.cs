using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000187 RID: 391
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the drag of the Rigidbody. Returns Success.")]
	public class GetDrag : Action
	{
		// Token: 0x06000985 RID: 2437 RVA: 0x0001EC00 File Offset: 0x0001CE00
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001EC40 File Offset: 0x0001CE40
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.drag;
			return TaskStatus.Success;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001EC73 File Offset: 0x0001CE73
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000571 RID: 1393
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000572 RID: 1394
		[Tooltip("The drag of the Rigidbody")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000573 RID: 1395
		private Rigidbody rigidbody;

		// Token: 0x04000574 RID: 1396
		private GameObject prevGameObject;
	}
}

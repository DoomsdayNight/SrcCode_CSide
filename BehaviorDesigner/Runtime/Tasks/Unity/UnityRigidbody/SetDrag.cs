using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000197 RID: 407
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the drag of the Rigidbody. Returns Success.")]
	public class SetDrag : Action
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x0001F500 File Offset: 0x0001D700
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0001F540 File Offset: 0x0001D740
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				Debug.LogWarning("Rigidbody is null");
				return TaskStatus.Failure;
			}
			this.rigidbody.drag = this.drag.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0001F573 File Offset: 0x0001D773
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.drag = 0f;
		}

		// Token: 0x040005AF RID: 1455
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040005B0 RID: 1456
		[Tooltip("The drag of the Rigidbody")]
		public SharedFloat drag;

		// Token: 0x040005B1 RID: 1457
		private Rigidbody rigidbody;

		// Token: 0x040005B2 RID: 1458
		private GameObject prevGameObject;
	}
}

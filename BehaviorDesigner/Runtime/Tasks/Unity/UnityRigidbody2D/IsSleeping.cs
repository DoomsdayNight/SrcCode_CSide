using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000172 RID: 370
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Returns Success if the Rigidbody2D is sleeping, otherwise Failure.")]
	public class IsSleeping : Conditional
	{
		// Token: 0x06000931 RID: 2353 RVA: 0x0001DF58 File Offset: 0x0001C158
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0001DF98 File Offset: 0x0001C198
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			if (!this.rigidbody2D.IsSleeping())
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0001DFC4 File Offset: 0x0001C1C4
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000516 RID: 1302
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000517 RID: 1303
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000518 RID: 1304
		private GameObject prevGameObject;
	}
}

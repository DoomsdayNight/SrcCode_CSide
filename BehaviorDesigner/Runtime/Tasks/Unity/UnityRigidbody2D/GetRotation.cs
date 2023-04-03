using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200016F RID: 367
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the rotation of the Rigidbody2D. Returns Success.")]
	public class GetRotation : Action
	{
		// Token: 0x06000925 RID: 2341 RVA: 0x0001DDB0 File Offset: 0x0001BFB0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0001DDF0 File Offset: 0x0001BFF0
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.rotation;
			return TaskStatus.Success;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0001DE23 File Offset: 0x0001C023
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400050B RID: 1291
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400050C RID: 1292
		[Tooltip("The rotation of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400050D RID: 1293
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400050E RID: 1294
		private GameObject prevGameObject;
	}
}

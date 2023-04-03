using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200016E RID: 366
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the position of the Rigidbody2D. Returns Success.")]
	public class GetPosition : Action
	{
		// Token: 0x06000921 RID: 2337 RVA: 0x0001DD1C File Offset: 0x0001BF1C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001DD5C File Offset: 0x0001BF5C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.position;
			return TaskStatus.Success;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001DD8F File Offset: 0x0001BF8F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector2.zero;
		}

		// Token: 0x04000507 RID: 1287
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000508 RID: 1288
		[Tooltip("The velocity of the Rigidbody2D")]
		[RequiredField]
		public SharedVector2 storeValue;

		// Token: 0x04000509 RID: 1289
		private Rigidbody2D rigidbody2D;

		// Token: 0x0400050A RID: 1290
		private GameObject prevGameObject;
	}
}

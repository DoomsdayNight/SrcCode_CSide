using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000167 RID: 359
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Applies a torque to the Rigidbody2D. Returns Success.")]
	public class AddTorque : Action
	{
		// Token: 0x06000905 RID: 2309 RVA: 0x0001D914 File Offset: 0x0001BB14
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0001D954 File Offset: 0x0001BB54
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.rigidbody2D.AddTorque(this.torque.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0001D987 File Offset: 0x0001BB87
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.torque = 0f;
		}

		// Token: 0x040004EB RID: 1259
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004EC RID: 1260
		[Tooltip("The amount of torque to apply")]
		public SharedFloat torque;

		// Token: 0x040004ED RID: 1261
		private Rigidbody2D rigidbody2D;

		// Token: 0x040004EE RID: 1262
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x0200016B RID: 363
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the gravity scale of the Rigidbody2D. Returns Success.")]
	public class GetGravityScale : Action
	{
		// Token: 0x06000915 RID: 2325 RVA: 0x0001DB64 File Offset: 0x0001BD64
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0001DBA4 File Offset: 0x0001BDA4
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				Debug.LogWarning("Rigidbody2D is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.gravityScale;
			return TaskStatus.Success;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0001DBD7 File Offset: 0x0001BDD7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040004FB RID: 1275
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004FC RID: 1276
		[Tooltip("The gravity scale of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040004FD RID: 1277
		private Rigidbody2D rigidbody2D;

		// Token: 0x040004FE RID: 1278
		private GameObject prevGameObject;
	}
}

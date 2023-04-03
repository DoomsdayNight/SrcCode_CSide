using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCircleCollider2D
{
	// Token: 0x02000238 RID: 568
	[TaskCategory("Unity/CircleCollider2D")]
	[TaskDescription("Stores the offset of the CircleCollider2D. Returns Success.")]
	public class GetOffset : Action
	{
		// Token: 0x06000BF4 RID: 3060 RVA: 0x000246A4 File Offset: 0x000228A4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.circleCollider2D = defaultGameObject.GetComponent<CircleCollider2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x000246E4 File Offset: 0x000228E4
		public override TaskStatus OnUpdate()
		{
			if (this.circleCollider2D == null)
			{
				Debug.LogWarning("CircleCollider2D is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.circleCollider2D.offset;
			return TaskStatus.Success;
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002471C File Offset: 0x0002291C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x040007B5 RID: 1973
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040007B6 RID: 1974
		[Tooltip("The offset of the CircleCollider2D")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x040007B7 RID: 1975
		private CircleCollider2D circleCollider2D;

		// Token: 0x040007B8 RID: 1976
		private GameObject prevGameObject;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityCapsuleCollider
{
	// Token: 0x0200024E RID: 590
	[TaskCategory("Unity/CapsuleCollider")]
	[TaskDescription("Stores the radius of the CapsuleCollider. Returns Success.")]
	public class GetRadius : Action
	{
		// Token: 0x06000C4D RID: 3149 RVA: 0x0002536C File Offset: 0x0002356C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.capsuleCollider = defaultGameObject.GetComponent<CapsuleCollider>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x000253AC File Offset: 0x000235AC
		public override TaskStatus OnUpdate()
		{
			if (this.capsuleCollider == null)
			{
				Debug.LogWarning("CapsuleCollider is null");
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.capsuleCollider.radius;
			return TaskStatus.Success;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x000253DF File Offset: 0x000235DF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400080C RID: 2060
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400080D RID: 2061
		[Tooltip("The radius of the CapsuleCollider")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x0400080E RID: 2062
		private CapsuleCollider capsuleCollider;

		// Token: 0x0400080F RID: 2063
		private GameObject prevGameObject;
	}
}

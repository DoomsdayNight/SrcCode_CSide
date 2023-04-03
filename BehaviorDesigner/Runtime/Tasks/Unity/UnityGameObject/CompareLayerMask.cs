using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000225 RID: 549
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Returns Success if the layermasks match, otherwise Failure.")]
	public class CompareLayerMask : Conditional
	{
		// Token: 0x06000BB9 RID: 3001 RVA: 0x00023E71 File Offset: 0x00022071
		public override TaskStatus OnUpdate()
		{
			if ((1 << base.GetDefaultGameObject(this.targetGameObject.Value).layer & this.layermask.value) == 0)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00023E9F File Offset: 0x0002209F
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000784 RID: 1924
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000785 RID: 1925
		[Tooltip("The layermask to compare against")]
		public LayerMask layermask;
	}
}

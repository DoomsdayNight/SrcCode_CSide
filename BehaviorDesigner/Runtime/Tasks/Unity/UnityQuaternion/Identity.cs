using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001A9 RID: 425
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the quaternion identity.")]
	public class Identity : Action
	{
		// Token: 0x06000A08 RID: 2568 RVA: 0x0001FDFA File Offset: 0x0001DFFA
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.identity;
			return TaskStatus.Success;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0001FE0D File Offset: 0x0001E00D
		public override void OnReset()
		{
			this.storeResult = Quaternion.identity;
		}

		// Token: 0x040005ED RID: 1517
		[Tooltip("The identity")]
		[RequiredField]
		public SharedQuaternion storeResult;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000ED RID: 237
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the right vector value.")]
	public class GetRightVector : Action
	{
		// Token: 0x06000769 RID: 1897 RVA: 0x00019F52 File Offset: 0x00018152
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.right;
			return TaskStatus.Success;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00019F65 File Offset: 0x00018165
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x0400037E RID: 894
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}

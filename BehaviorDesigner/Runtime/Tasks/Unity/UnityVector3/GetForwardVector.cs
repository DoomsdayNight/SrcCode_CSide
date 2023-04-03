using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000EB RID: 235
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the forward vector value.")]
	public class GetForwardVector : Action
	{
		// Token: 0x06000763 RID: 1891 RVA: 0x00019ECE File Offset: 0x000180CE
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.forward;
			return TaskStatus.Success;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00019EE1 File Offset: 0x000180E1
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x0400037B RID: 891
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}

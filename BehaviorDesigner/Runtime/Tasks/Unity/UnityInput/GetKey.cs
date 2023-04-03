using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x0200021A RID: 538
	[TaskCategory("Unity/Input")]
	[TaskDescription("Stores the pressed state of the specified key.")]
	public class GetKey : Action
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x00023C70 File Offset: 0x00021E70
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.GetKey(this.key);
			return TaskStatus.Success;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00023C89 File Offset: 0x00021E89
		public override void OnReset()
		{
			this.key = KeyCode.None;
			this.storeResult = false;
		}

		// Token: 0x04000777 RID: 1911
		[Tooltip("The key to test.")]
		public KeyCode key;

		// Token: 0x04000778 RID: 1912
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedBool storeResult;
	}
}

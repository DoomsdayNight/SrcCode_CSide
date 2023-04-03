using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x0200021B RID: 539
	[TaskCategory("Unity/Input")]
	[TaskDescription("Stores the state of the specified mouse button.")]
	public class GetMouseButton : Action
	{
		// Token: 0x06000B9B RID: 2971 RVA: 0x00023CA6 File Offset: 0x00021EA6
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.GetMouseButton(this.buttonIndex.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00023CC4 File Offset: 0x00021EC4
		public override void OnReset()
		{
			this.buttonIndex = 0;
			this.storeResult = false;
		}

		// Token: 0x04000779 RID: 1913
		[Tooltip("The index of the button")]
		public SharedInt buttonIndex;

		// Token: 0x0400077A RID: 1914
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedBool storeResult;
	}
}

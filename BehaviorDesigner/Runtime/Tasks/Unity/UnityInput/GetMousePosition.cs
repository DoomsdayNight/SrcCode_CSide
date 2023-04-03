using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x0200021C RID: 540
	[TaskCategory("Unity/Input")]
	[TaskDescription("Stores the mouse position.")]
	public class GetMousePosition : Action
	{
		// Token: 0x06000B9E RID: 2974 RVA: 0x00023CE6 File Offset: 0x00021EE6
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.mousePosition;
			return TaskStatus.Success;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00023CF9 File Offset: 0x00021EF9
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x0400077B RID: 1915
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedVector3 storeResult;
	}
}

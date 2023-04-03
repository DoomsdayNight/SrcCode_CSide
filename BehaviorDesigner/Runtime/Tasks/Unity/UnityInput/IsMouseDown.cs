using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000221 RID: 545
	[TaskCategory("Unity/Input")]
	[TaskDescription("Returns success when the specified mouse button is pressed.")]
	public class IsMouseDown : Conditional
	{
		// Token: 0x06000BAD RID: 2989 RVA: 0x00023DBB File Offset: 0x00021FBB
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetMouseButtonDown(this.buttonIndex.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00023DD2 File Offset: 0x00021FD2
		public override void OnReset()
		{
			this.buttonIndex = 0;
		}

		// Token: 0x04000780 RID: 1920
		[Tooltip("The button index")]
		public SharedInt buttonIndex;
	}
}

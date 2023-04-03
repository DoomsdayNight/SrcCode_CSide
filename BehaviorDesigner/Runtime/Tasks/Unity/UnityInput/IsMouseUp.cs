using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000222 RID: 546
	[TaskCategory("Unity/Input")]
	[TaskDescription("Returns success when the specified mouse button is pressed.")]
	public class IsMouseUp : Conditional
	{
		// Token: 0x06000BB0 RID: 2992 RVA: 0x00023DE8 File Offset: 0x00021FE8
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetMouseButtonUp(this.buttonIndex.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00023DFF File Offset: 0x00021FFF
		public override void OnReset()
		{
			this.buttonIndex = 0;
		}

		// Token: 0x04000781 RID: 1921
		[Tooltip("The button index")]
		public SharedInt buttonIndex;
	}
}

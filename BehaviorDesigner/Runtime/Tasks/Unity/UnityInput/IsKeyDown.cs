using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x0200021F RID: 543
	[TaskCategory("Unity/Input")]
	[TaskDescription("Returns success when the specified key is pressed.")]
	public class IsKeyDown : Conditional
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x00023D75 File Offset: 0x00021F75
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetKeyDown(this.key))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00023D87 File Offset: 0x00021F87
		public override void OnReset()
		{
			this.key = KeyCode.None;
		}

		// Token: 0x0400077E RID: 1918
		[Tooltip("The key to test")]
		public KeyCode key;
	}
}

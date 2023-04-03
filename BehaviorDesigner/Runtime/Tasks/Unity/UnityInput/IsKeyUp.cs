using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000220 RID: 544
	[TaskCategory("Unity/Input")]
	[TaskDescription("Returns success when the specified key is released.")]
	public class IsKeyUp : Conditional
	{
		// Token: 0x06000BAA RID: 2986 RVA: 0x00023D98 File Offset: 0x00021F98
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetKeyUp(this.key))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00023DAA File Offset: 0x00021FAA
		public override void OnReset()
		{
			this.key = KeyCode.None;
		}

		// Token: 0x0400077F RID: 1919
		[Tooltip("The key to test")]
		public KeyCode key;
	}
}

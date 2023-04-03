using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x0200021D RID: 541
	[TaskCategory("Unity/Input")]
	[TaskDescription("Returns success when the specified button is pressed.")]
	public class IsButtonDown : Conditional
	{
		// Token: 0x06000BA1 RID: 2977 RVA: 0x00023D13 File Offset: 0x00021F13
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetButtonDown(this.buttonName.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00023D2A File Offset: 0x00021F2A
		public override void OnReset()
		{
			this.buttonName = "Fire1";
		}

		// Token: 0x0400077C RID: 1916
		[Tooltip("The name of the button")]
		public SharedString buttonName;
	}
}

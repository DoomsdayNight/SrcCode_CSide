using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x0200021E RID: 542
	[TaskCategory("Unity/Input")]
	[TaskDescription("Returns success when the specified button is released.")]
	public class IsButtonUp : Conditional
	{
		// Token: 0x06000BA4 RID: 2980 RVA: 0x00023D44 File Offset: 0x00021F44
		public override TaskStatus OnUpdate()
		{
			if (!Input.GetButtonUp(this.buttonName.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00023D5B File Offset: 0x00021F5B
		public override void OnReset()
		{
			this.buttonName = "Fire1";
		}

		// Token: 0x0400077D RID: 1917
		[Tooltip("The name of the button")]
		public SharedString buttonName;
	}
}

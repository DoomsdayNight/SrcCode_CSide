using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000219 RID: 537
	[TaskCategory("Unity/Input")]
	[TaskDescription("Stores the state of the specified button.")]
	public class GetButton : Action
	{
		// Token: 0x06000B95 RID: 2965 RVA: 0x00023C2C File Offset: 0x00021E2C
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Input.GetButton(this.buttonName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00023C4A File Offset: 0x00021E4A
		public override void OnReset()
		{
			this.buttonName = "Fire1";
			this.storeResult = false;
		}

		// Token: 0x04000775 RID: 1909
		[Tooltip("The name of the button")]
		public SharedString buttonName;

		// Token: 0x04000776 RID: 1910
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedBool storeResult;
	}
}

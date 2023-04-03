using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001FA RID: 506
	[TaskCategory("Unity/Math")]
	[TaskDescription("Lerp the float by an amount.")]
	public class Lerp : Action
	{
		// Token: 0x06000B27 RID: 2855 RVA: 0x00022CEE File Offset: 0x00020EEE
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Mathf.Lerp(this.fromValue.Value, this.toValue.Value, this.lerpAmount.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00022D24 File Offset: 0x00020F24
		public override void OnReset()
		{
			this.fromValue = 0f;
			this.toValue = 0f;
			this.lerpAmount = 0f;
			this.storeResult = 0f;
		}

		// Token: 0x0400070B RID: 1803
		[Tooltip("The from value")]
		public SharedFloat fromValue;

		// Token: 0x0400070C RID: 1804
		[Tooltip("The to value")]
		public SharedFloat toValue;

		// Token: 0x0400070D RID: 1805
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x0400070E RID: 1806
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}

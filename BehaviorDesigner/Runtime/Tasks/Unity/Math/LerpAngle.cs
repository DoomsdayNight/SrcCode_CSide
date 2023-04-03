using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001FB RID: 507
	[TaskCategory("Unity/Math")]
	[TaskDescription("Lerp the angle by an amount.")]
	public class LerpAngle : Action
	{
		// Token: 0x06000B2A RID: 2858 RVA: 0x00022D79 File Offset: 0x00020F79
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Mathf.LerpAngle(this.fromValue.Value, this.toValue.Value, this.lerpAmount.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00022DB0 File Offset: 0x00020FB0
		public override void OnReset()
		{
			this.fromValue = 0f;
			this.toValue = 0f;
			this.lerpAmount = 0f;
			this.storeResult = 0f;
		}

		// Token: 0x0400070F RID: 1807
		[Tooltip("The from value")]
		public SharedFloat fromValue;

		// Token: 0x04000710 RID: 1808
		[Tooltip("The to value")]
		public SharedFloat toValue;

		// Token: 0x04000711 RID: 1809
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x04000712 RID: 1810
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}

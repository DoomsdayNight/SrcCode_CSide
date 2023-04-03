using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000218 RID: 536
	[TaskCategory("Unity/Input")]
	[TaskDescription("Stores the raw value of the specified axis and stores it in a float.")]
	public class GetAxisRaw : Action
	{
		// Token: 0x06000B92 RID: 2962 RVA: 0x00023BAC File Offset: 0x00021DAC
		public override TaskStatus OnUpdate()
		{
			float num = Input.GetAxis(this.axisName.Value);
			if (!this.multiplier.IsNone)
			{
				num *= this.multiplier.Value;
			}
			this.storeResult.Value = num;
			return TaskStatus.Success;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x00023BF2 File Offset: 0x00021DF2
		public override void OnReset()
		{
			this.axisName = "";
			this.multiplier = 1f;
			this.storeResult = 0f;
		}

		// Token: 0x04000772 RID: 1906
		[Tooltip("The name of the axis")]
		public SharedString axisName;

		// Token: 0x04000773 RID: 1907
		[Tooltip("Axis values are in the range -1 to 1. Use the multiplier to set a larger range")]
		public SharedFloat multiplier;

		// Token: 0x04000774 RID: 1908
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedFloat storeResult;
	}
}

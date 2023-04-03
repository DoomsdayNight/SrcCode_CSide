using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityInput
{
	// Token: 0x02000217 RID: 535
	[TaskCategory("Unity/Input")]
	[TaskDescription("Stores the value of the specified axis and stores it in a float.")]
	public class GetAxis : Action
	{
		// Token: 0x06000B8F RID: 2959 RVA: 0x00023B2C File Offset: 0x00021D2C
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

		// Token: 0x06000B90 RID: 2960 RVA: 0x00023B72 File Offset: 0x00021D72
		public override void OnReset()
		{
			this.axisName = "";
			this.multiplier = 1f;
			this.storeResult = 0f;
		}

		// Token: 0x0400076F RID: 1903
		[Tooltip("The name of the axis")]
		public SharedString axisName;

		// Token: 0x04000770 RID: 1904
		[Tooltip("Axis values are in the range -1 to 1. Use the multiplier to set a larger range")]
		public SharedFloat multiplier;

		// Token: 0x04000771 RID: 1905
		[RequiredField]
		[Tooltip("The stored result")]
		public SharedFloat storeResult;
	}
}

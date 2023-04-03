using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001FD RID: 509
	[TaskCategory("Unity/Math")]
	[TaskDescription("Sets a random float value")]
	public class RandomFloat : Action
	{
		// Token: 0x06000B2F RID: 2863 RVA: 0x00022E28 File Offset: 0x00021028
		public override TaskStatus OnUpdate()
		{
			if (this.inclusive)
			{
				this.storeResult.Value = UnityEngine.Random.Range(this.min.Value, this.max.Value);
			}
			else
			{
				this.storeResult.Value = UnityEngine.Random.Range(this.min.Value, this.max.Value - 1E-05f);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00022E92 File Offset: 0x00021092
		public override void OnReset()
		{
			this.min.Value = 0f;
			this.max.Value = 0f;
			this.inclusive = false;
			this.storeResult = 0f;
		}

		// Token: 0x04000714 RID: 1812
		[Tooltip("The minimum amount")]
		public SharedFloat min;

		// Token: 0x04000715 RID: 1813
		[Tooltip("The maximum amount")]
		public SharedFloat max;

		// Token: 0x04000716 RID: 1814
		[Tooltip("Is the maximum value inclusive?")]
		public bool inclusive;

		// Token: 0x04000717 RID: 1815
		[Tooltip("The variable to store the result")]
		public SharedFloat storeResult;
	}
}

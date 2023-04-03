using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001FE RID: 510
	[TaskCategory("Unity/Math")]
	[TaskDescription("Sets a random int value")]
	public class RandomInt : Action
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x00022ED4 File Offset: 0x000210D4
		public override TaskStatus OnUpdate()
		{
			if (this.inclusive)
			{
				this.storeResult.Value = UnityEngine.Random.Range(this.min.Value, this.max.Value + 1);
			}
			else
			{
				this.storeResult.Value = UnityEngine.Random.Range(this.min.Value, this.max.Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00022F3A File Offset: 0x0002113A
		public override void OnReset()
		{
			this.min.Value = 0;
			this.max.Value = 0;
			this.inclusive = false;
			this.storeResult = 0;
		}

		// Token: 0x04000718 RID: 1816
		[Tooltip("The minimum amount")]
		public SharedInt min;

		// Token: 0x04000719 RID: 1817
		[Tooltip("The maximum amount")]
		public SharedInt max;

		// Token: 0x0400071A RID: 1818
		[Tooltip("Is the maximum value inclusive?")]
		public bool inclusive;

		// Token: 0x0400071B RID: 1819
		[Tooltip("The variable to store the result")]
		public SharedInt storeResult;
	}
}

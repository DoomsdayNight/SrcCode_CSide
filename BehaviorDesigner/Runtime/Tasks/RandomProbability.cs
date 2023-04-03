using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D8 RID: 216
	[TaskDescription("The random probability task will return success when the random probability is below the succeed probability. It will otherwise return failure.")]
	public class RandomProbability : Conditional
	{
		// Token: 0x060006FE RID: 1790 RVA: 0x000190E0 File Offset: 0x000172E0
		public override void OnAwake()
		{
			if (this.useSeed.Value)
			{
				UnityEngine.Random.InitState(this.seed.Value);
			}
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000190FF File Offset: 0x000172FF
		public override TaskStatus OnUpdate()
		{
			if (UnityEngine.Random.value < this.successProbability.Value)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00019116 File Offset: 0x00017316
		public override void OnReset()
		{
			this.successProbability = 0.5f;
			this.seed = 0;
			this.useSeed = false;
		}

		// Token: 0x04000347 RID: 839
		[Tooltip("The chance that the task will return success")]
		public SharedFloat successProbability = 0.5f;

		// Token: 0x04000348 RID: 840
		[Tooltip("Seed the random number generator to make things easier to debug")]
		public SharedInt seed;

		// Token: 0x04000349 RID: 841
		[Tooltip("Do we want to use the seed?")]
		public SharedBool useSeed;
	}
}

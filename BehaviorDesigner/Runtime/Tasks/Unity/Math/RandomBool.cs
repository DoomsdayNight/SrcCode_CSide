using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001FC RID: 508
	[TaskCategory("Unity/Math")]
	[TaskDescription("Sets a random bool value")]
	public class RandomBool : Action
	{
		// Token: 0x06000B2D RID: 2861 RVA: 0x00022E05 File Offset: 0x00021005
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = (UnityEngine.Random.value < 0.5f);
			return TaskStatus.Success;
		}

		// Token: 0x04000713 RID: 1811
		[Tooltip("The variable to store the result")]
		public SharedBool storeResult;
	}
}

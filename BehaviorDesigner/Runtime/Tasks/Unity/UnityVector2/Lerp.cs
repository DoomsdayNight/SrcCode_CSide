using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x02000103 RID: 259
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Lerp the Vector2 by an amount.")]
	public class Lerp : Action
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x0001A856 File Offset: 0x00018A56
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.Lerp(this.fromVector2.Value, this.toVector2.Value, this.lerpAmount.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001A88C File Offset: 0x00018A8C
		public override void OnReset()
		{
			this.fromVector2 = Vector2.zero;
			this.toVector2 = Vector2.zero;
			this.storeResult = Vector2.zero;
			this.lerpAmount = 0f;
		}

		// Token: 0x040003B8 RID: 952
		[Tooltip("The from value")]
		public SharedVector2 fromVector2;

		// Token: 0x040003B9 RID: 953
		[Tooltip("The to value")]
		public SharedVector2 toVector2;

		// Token: 0x040003BA RID: 954
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x040003BB RID: 955
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}

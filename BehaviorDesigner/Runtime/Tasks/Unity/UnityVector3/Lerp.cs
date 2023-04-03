using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000F2 RID: 242
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Lerp the Vector3 by an amount.")]
	public class Lerp : Action
	{
		// Token: 0x06000778 RID: 1912 RVA: 0x0001A101 File Offset: 0x00018301
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Lerp(this.fromVector3.Value, this.toVector3.Value, this.lerpAmount.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001A138 File Offset: 0x00018338
		public override void OnReset()
		{
			this.fromVector3 = (this.toVector3 = (this.storeResult = Vector3.zero));
			this.lerpAmount = 0f;
		}

		// Token: 0x04000388 RID: 904
		[Tooltip("The from value")]
		public SharedVector3 fromVector3;

		// Token: 0x04000389 RID: 905
		[Tooltip("The to value")]
		public SharedVector3 toVector3;

		// Token: 0x0400038A RID: 906
		[Tooltip("The amount to lerp")]
		public SharedFloat lerpAmount;

		// Token: 0x0400038B RID: 907
		[Tooltip("The lerp resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}

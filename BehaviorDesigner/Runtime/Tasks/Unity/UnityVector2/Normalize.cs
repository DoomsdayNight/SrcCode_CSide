using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x02000106 RID: 262
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Normalize the Vector2.")]
	public class Normalize : Action
	{
		// Token: 0x060007B4 RID: 1972 RVA: 0x0001A9DC File Offset: 0x00018BDC
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value.normalized;
			return TaskStatus.Success;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001AA08 File Offset: 0x00018C08
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.storeResult = Vector2.zero;
		}

		// Token: 0x040003C3 RID: 963
		[Tooltip("The Vector2 to normalize")]
		public SharedVector2 vector2Variable;

		// Token: 0x040003C4 RID: 964
		[Tooltip("The normalized resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}

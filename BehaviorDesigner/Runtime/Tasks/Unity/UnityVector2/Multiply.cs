using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x02000105 RID: 261
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Multiply the Vector2 by a float.")]
	public class Multiply : Action
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x0001A971 File Offset: 0x00018B71
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value * this.multiplyBy.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001A99C File Offset: 0x00018B9C
		public override void OnReset()
		{
			this.vector2Variable = (this.storeResult = Vector2.zero);
			this.multiplyBy = 0f;
		}

		// Token: 0x040003C0 RID: 960
		[Tooltip("The Vector2 to multiply of")]
		public SharedVector2 vector2Variable;

		// Token: 0x040003C1 RID: 961
		[Tooltip("The value to multiply the Vector2 of")]
		public SharedFloat multiplyBy;

		// Token: 0x040003C2 RID: 962
		[Tooltip("The multiplication resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}

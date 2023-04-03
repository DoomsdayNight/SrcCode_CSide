using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x02000108 RID: 264
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Sets the value of the Vector2.")]
	public class SetValue : Action
	{
		// Token: 0x060007BA RID: 1978 RVA: 0x0001AB14 File Offset: 0x00018D14
		public override TaskStatus OnUpdate()
		{
			this.vector2Variable.Value = this.vector2Value.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001AB2D File Offset: 0x00018D2D
		public override void OnReset()
		{
			this.vector2Value = Vector2.zero;
			this.vector2Variable = Vector2.zero;
		}

		// Token: 0x040003C9 RID: 969
		[Tooltip("The Vector2 to get the values of")]
		public SharedVector2 vector2Value;

		// Token: 0x040003CA RID: 970
		[Tooltip("The Vector2 to set the values of")]
		public SharedVector2 vector2Variable;
	}
}

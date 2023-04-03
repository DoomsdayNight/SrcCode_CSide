using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x02000100 RID: 256
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Stores the up vector value.")]
	public class GetUpVector : Action
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x0001A76A File Offset: 0x0001896A
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.up;
			return TaskStatus.Success;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001A77D File Offset: 0x0001897D
		public override void OnReset()
		{
			this.storeResult = Vector2.zero;
		}

		// Token: 0x040003B2 RID: 946
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020000FE RID: 254
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Stores the right vector value.")]
	public class GetRightVector : Action
	{
		// Token: 0x0600079C RID: 1948 RVA: 0x0001A6E6 File Offset: 0x000188E6
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.right;
			return TaskStatus.Success;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001A6F9 File Offset: 0x000188F9
		public override void OnReset()
		{
			this.storeResult = Vector2.zero;
		}

		// Token: 0x040003AF RID: 943
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x02000104 RID: 260
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Move from the current position to the target position.")]
	public class MoveTowards : Action
	{
		// Token: 0x060007AE RID: 1966 RVA: 0x0001A8E1 File Offset: 0x00018AE1
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.MoveTowards(this.currentPosition.Value, this.targetPosition.Value, this.speed.Value * Time.deltaTime);
			return TaskStatus.Success;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001A91C File Offset: 0x00018B1C
		public override void OnReset()
		{
			this.currentPosition = Vector2.zero;
			this.targetPosition = Vector2.zero;
			this.storeResult = Vector2.zero;
			this.speed = 0f;
		}

		// Token: 0x040003BC RID: 956
		[Tooltip("The current position")]
		public SharedVector2 currentPosition;

		// Token: 0x040003BD RID: 957
		[Tooltip("The target position")]
		public SharedVector2 targetPosition;

		// Token: 0x040003BE RID: 958
		[Tooltip("The movement speed")]
		public SharedFloat speed;

		// Token: 0x040003BF RID: 959
		[Tooltip("The move resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}

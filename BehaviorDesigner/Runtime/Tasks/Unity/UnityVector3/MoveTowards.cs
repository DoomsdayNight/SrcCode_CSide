using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000F3 RID: 243
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Move from the current position to the target position.")]
	public class MoveTowards : Action
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x0001A17F File Offset: 0x0001837F
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.MoveTowards(this.currentPosition.Value, this.targetPosition.Value, this.speed.Value * Time.deltaTime);
			return TaskStatus.Success;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001A1BC File Offset: 0x000183BC
		public override void OnReset()
		{
			this.currentPosition = Vector3.zero;
			this.targetPosition = Vector3.zero;
			this.storeResult = Vector3.zero;
			this.speed = 0f;
		}

		// Token: 0x0400038C RID: 908
		[Tooltip("The current position")]
		public SharedVector3 currentPosition;

		// Token: 0x0400038D RID: 909
		[Tooltip("The target position")]
		public SharedVector3 targetPosition;

		// Token: 0x0400038E RID: 910
		[Tooltip("The movement speed")]
		public SharedFloat speed;

		// Token: 0x0400038F RID: 911
		[Tooltip("The move resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000F7 RID: 247
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Rotate the current rotation to the target rotation.")]
	public class RotateTowards : Action
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x0001A39C File Offset: 0x0001859C
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.RotateTowards(this.currentRotation.Value, this.targetRotation.Value, this.maxDegreesDelta.Value * 0.017453292f * Time.deltaTime, this.maxMagnitudeDelta.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001A3F4 File Offset: 0x000185F4
		public override void OnReset()
		{
			this.currentRotation = Vector3.zero;
			this.targetRotation = Vector3.zero;
			this.storeResult = Vector3.zero;
			this.maxDegreesDelta = 0f;
			this.maxMagnitudeDelta = 0f;
		}

		// Token: 0x04000399 RID: 921
		[Tooltip("The current rotation in euler angles")]
		public SharedVector3 currentRotation;

		// Token: 0x0400039A RID: 922
		[Tooltip("The target rotation in euler angles")]
		public SharedVector3 targetRotation;

		// Token: 0x0400039B RID: 923
		[Tooltip("The maximum delta of the degrees")]
		public SharedFloat maxDegreesDelta;

		// Token: 0x0400039C RID: 924
		[Tooltip("The maximum delta of the magnitude")]
		public SharedFloat maxMagnitudeDelta;

		// Token: 0x0400039D RID: 925
		[Tooltip("The rotation resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}

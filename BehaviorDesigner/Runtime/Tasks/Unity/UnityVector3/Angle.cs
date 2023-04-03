using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000E7 RID: 231
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Returns the angle between two Vector3s.")]
	public class Angle : Action
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x00019D42 File Offset: 0x00017F42
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Angle(this.firstVector3.Value, this.secondVector3.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00019D6B File Offset: 0x00017F6B
		public override void OnReset()
		{
			this.firstVector3 = Vector3.zero;
			this.secondVector3 = Vector3.zero;
			this.storeResult = 0f;
		}

		// Token: 0x0400036F RID: 879
		[Tooltip("The first Vector3")]
		public SharedVector3 firstVector3;

		// Token: 0x04000370 RID: 880
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x04000371 RID: 881
		[Tooltip("The angle")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}

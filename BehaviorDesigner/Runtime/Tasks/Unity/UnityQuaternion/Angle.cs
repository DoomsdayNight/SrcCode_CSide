using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityQuaternion
{
	// Token: 0x020001A4 RID: 420
	[TaskCategory("Unity/Quaternion")]
	[TaskDescription("Stores the angle in degrees between two rotations.")]
	public class Angle : Action
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x0001FC17 File Offset: 0x0001DE17
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Quaternion.Angle(this.firstRotation.Value, this.secondRotation.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0001FC40 File Offset: 0x0001DE40
		public override void OnReset()
		{
			this.firstRotation = (this.secondRotation = Quaternion.identity);
			this.storeResult = 0f;
		}

		// Token: 0x040005DF RID: 1503
		[Tooltip("The first rotation")]
		public SharedQuaternion firstRotation;

		// Token: 0x040005E0 RID: 1504
		[Tooltip("The second rotation")]
		public SharedQuaternion secondRotation;

		// Token: 0x040005E1 RID: 1505
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}

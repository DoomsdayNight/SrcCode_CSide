using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020000FC RID: 252
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Stores the dot product of two Vector2 values.")]
	public class Dot : Action
	{
		// Token: 0x06000796 RID: 1942 RVA: 0x0001A62D File Offset: 0x0001882D
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.Dot(this.leftHandSide.Value, this.rightHandSide.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001A656 File Offset: 0x00018856
		public override void OnReset()
		{
			this.leftHandSide = Vector2.zero;
			this.rightHandSide = Vector2.zero;
			this.storeResult = 0f;
		}

		// Token: 0x040003AA RID: 938
		[Tooltip("The left hand side of the dot product")]
		public SharedVector2 leftHandSide;

		// Token: 0x040003AB RID: 939
		[Tooltip("The right hand side of the dot product")]
		public SharedVector2 rightHandSide;

		// Token: 0x040003AC RID: 940
		[Tooltip("The dot product result")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x02000102 RID: 258
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Stores the X and Y values of the Vector2.")]
	public class GetXY : Action
	{
		// Token: 0x060007A8 RID: 1960 RVA: 0x0001A7DF File Offset: 0x000189DF
		public override TaskStatus OnUpdate()
		{
			this.storeX.Value = this.vector2Variable.Value.x;
			this.storeY.Value = this.vector2Variable.Value.y;
			return TaskStatus.Success;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001A818 File Offset: 0x00018A18
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.storeX = (this.storeY = 0f);
		}

		// Token: 0x040003B5 RID: 949
		[Tooltip("The Vector2 to get the values of")]
		public SharedVector2 vector2Variable;

		// Token: 0x040003B6 RID: 950
		[Tooltip("The X value")]
		[RequiredField]
		public SharedFloat storeX;

		// Token: 0x040003B7 RID: 951
		[Tooltip("The Y value")]
		[RequiredField]
		public SharedFloat storeY;
	}
}

using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x02000109 RID: 265
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Sets the X and Y values of the Vector2.")]
	public class SetXY : Action
	{
		// Token: 0x060007BD RID: 1981 RVA: 0x0001AB58 File Offset: 0x00018D58
		public override TaskStatus OnUpdate()
		{
			Vector2 value = this.vector2Variable.Value;
			if (!this.xValue.IsNone)
			{
				value.x = this.xValue.Value;
			}
			if (!this.yValue.IsNone)
			{
				value.y = this.yValue.Value;
			}
			this.vector2Variable.Value = value;
			return TaskStatus.Success;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001ABBC File Offset: 0x00018DBC
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.xValue = 0f;
			this.yValue = 0f;
		}

		// Token: 0x040003CB RID: 971
		[Tooltip("The Vector2 to set the values of")]
		public SharedVector2 vector2Variable;

		// Token: 0x040003CC RID: 972
		[Tooltip("The X value. Set to None to have the value ignored")]
		public SharedFloat xValue;

		// Token: 0x040003CD RID: 973
		[Tooltip("The Y value. Set to None to have the value ignored")]
		public SharedFloat yValue;
	}
}

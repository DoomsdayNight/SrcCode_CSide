using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000F9 RID: 249
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Sets the X, Y, and Z values of the Vector3.")]
	public class SetXYZ : Action
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x0001A49C File Offset: 0x0001869C
		public override TaskStatus OnUpdate()
		{
			Vector3 value = this.vector3Variable.Value;
			if (!this.xValue.IsNone)
			{
				value.x = this.xValue.Value;
			}
			if (!this.yValue.IsNone)
			{
				value.y = this.yValue.Value;
			}
			if (!this.zValue.IsNone)
			{
				value.z = this.zValue.Value;
			}
			this.vector3Variable.Value = value;
			return TaskStatus.Success;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001A520 File Offset: 0x00018720
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.xValue = (this.yValue = (this.zValue = 0f));
		}

		// Token: 0x040003A0 RID: 928
		[Tooltip("The Vector3 to set the values of")]
		public SharedVector3 vector3Variable;

		// Token: 0x040003A1 RID: 929
		[Tooltip("The X value. Set to None to have the value ignored")]
		public SharedFloat xValue;

		// Token: 0x040003A2 RID: 930
		[Tooltip("The Y value. Set to None to have the value ignored")]
		public SharedFloat yValue;

		// Token: 0x040003A3 RID: 931
		[Tooltip("The Z value. Set to None to have the value ignored")]
		public SharedFloat zValue;
	}
}

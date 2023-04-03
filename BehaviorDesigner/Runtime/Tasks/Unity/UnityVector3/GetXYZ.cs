using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000F1 RID: 241
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the X, Y, and Z values of the Vector3.")]
	public class GetXYZ : Action
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x0001A04C File Offset: 0x0001824C
		public override TaskStatus OnUpdate()
		{
			this.storeX.Value = this.vector3Variable.Value.x;
			this.storeY.Value = this.vector3Variable.Value.y;
			this.storeZ.Value = this.vector3Variable.Value.z;
			return TaskStatus.Success;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001A0AC File Offset: 0x000182AC
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeX = 0f;
			this.storeY = 0f;
			this.storeZ = 0f;
		}

		// Token: 0x04000384 RID: 900
		[Tooltip("The Vector3 to get the values of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04000385 RID: 901
		[Tooltip("The X value")]
		[RequiredField]
		public SharedFloat storeX;

		// Token: 0x04000386 RID: 902
		[Tooltip("The Y value")]
		[RequiredField]
		public SharedFloat storeY;

		// Token: 0x04000387 RID: 903
		[Tooltip("The Z value")]
		[RequiredField]
		public SharedFloat storeZ;
	}
}

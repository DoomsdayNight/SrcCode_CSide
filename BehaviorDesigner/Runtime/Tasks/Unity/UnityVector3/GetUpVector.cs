using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020000EF RID: 239
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the up vector value.")]
	public class GetUpVector : Action
	{
		// Token: 0x0600076F RID: 1903 RVA: 0x00019FD6 File Offset: 0x000181D6
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.up;
			return TaskStatus.Success;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00019FE9 File Offset: 0x000181E9
		public override void OnReset()
		{
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04000381 RID: 897
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}

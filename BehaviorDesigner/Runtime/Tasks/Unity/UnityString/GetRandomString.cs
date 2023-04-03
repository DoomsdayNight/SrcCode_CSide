using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x02000138 RID: 312
	[TaskCategory("Unity/String")]
	[TaskDescription("Randomly selects a string from the array of strings.")]
	public class GetRandomString : Action
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x0001C5F8 File Offset: 0x0001A7F8
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.source[UnityEngine.Random.Range(0, this.source.Length)].Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001C620 File Offset: 0x0001A820
		public override void OnReset()
		{
			this.source = null;
			this.storeResult = null;
		}

		// Token: 0x0400047C RID: 1148
		[Tooltip("The array of strings")]
		public SharedString[] source;

		// Token: 0x0400047D RID: 1149
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}

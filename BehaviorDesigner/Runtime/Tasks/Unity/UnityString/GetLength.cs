using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x02000137 RID: 311
	[TaskCategory("Unity/String")]
	[TaskDescription("Stores the length of the string")]
	public class GetLength : Action
	{
		// Token: 0x0600086D RID: 2157 RVA: 0x0001C5B4 File Offset: 0x0001A7B4
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.targetString.Value.Length;
			return TaskStatus.Success;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0001C5D2 File Offset: 0x0001A7D2
		public override void OnReset()
		{
			this.targetString = "";
			this.storeResult = 0;
		}

		// Token: 0x0400047A RID: 1146
		[Tooltip("The target string")]
		public SharedString targetString;

		// Token: 0x0400047B RID: 1147
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedInt storeResult;
	}
}

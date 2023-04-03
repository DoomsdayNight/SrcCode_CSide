using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x02000139 RID: 313
	[TaskCategory("Unity/String")]
	[TaskDescription("Stores a substring of the target string")]
	public class GetSubstring : Action
	{
		// Token: 0x06000873 RID: 2163 RVA: 0x0001C638 File Offset: 0x0001A838
		public override TaskStatus OnUpdate()
		{
			if (this.length.Value != -1)
			{
				this.storeResult.Value = this.targetString.Value.Substring(this.startIndex.Value, this.length.Value);
			}
			else
			{
				this.storeResult.Value = this.targetString.Value.Substring(this.startIndex.Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001C6AD File Offset: 0x0001A8AD
		public override void OnReset()
		{
			this.targetString = "";
			this.startIndex = 0;
			this.length = -1;
			this.storeResult = "";
		}

		// Token: 0x0400047E RID: 1150
		[Tooltip("The target string")]
		public SharedString targetString;

		// Token: 0x0400047F RID: 1151
		[Tooltip("The start substring index")]
		public SharedInt startIndex = 0;

		// Token: 0x04000480 RID: 1152
		[Tooltip("The length of the substring. Don't use if -1")]
		public SharedInt length = -1;

		// Token: 0x04000481 RID: 1153
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}

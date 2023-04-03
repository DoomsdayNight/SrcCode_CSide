using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x0200013B RID: 315
	[TaskCategory("Unity/String")]
	[TaskDescription("Replaces a string with the new string")]
	public class Replace : Action
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x0001C738 File Offset: 0x0001A938
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.targetString.Value.Replace(this.oldString.Value, this.newString.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001C76C File Offset: 0x0001A96C
		public override void OnReset()
		{
			this.targetString = "";
			this.oldString = "";
			this.newString = "";
			this.storeResult = "";
		}

		// Token: 0x04000483 RID: 1155
		[Tooltip("The target string")]
		public SharedString targetString;

		// Token: 0x04000484 RID: 1156
		[Tooltip("The old replace")]
		public SharedString oldString;

		// Token: 0x04000485 RID: 1157
		[Tooltip("The new string")]
		public SharedString newString;

		// Token: 0x04000486 RID: 1158
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}

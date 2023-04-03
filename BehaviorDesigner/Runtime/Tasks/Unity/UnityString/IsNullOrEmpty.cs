using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x0200013A RID: 314
	[TaskCategory("Unity/String")]
	[TaskDescription("Returns success if the string is null or empty")]
	public class IsNullOrEmpty : Conditional
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x0001C707 File Offset: 0x0001A907
		public override TaskStatus OnUpdate()
		{
			if (!string.IsNullOrEmpty(this.targetString.Value))
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001C71E File Offset: 0x0001A91E
		public override void OnReset()
		{
			this.targetString = "";
		}

		// Token: 0x04000482 RID: 1154
		[Tooltip("The target string")]
		public SharedString targetString;
	}
}

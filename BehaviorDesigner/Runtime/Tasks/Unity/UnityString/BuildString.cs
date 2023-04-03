using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x02000134 RID: 308
	[TaskCategory("Unity/String")]
	[TaskDescription("Creates a string from multiple other strings.")]
	public class BuildString : Action
	{
		// Token: 0x06000863 RID: 2147 RVA: 0x0001C430 File Offset: 0x0001A630
		public override TaskStatus OnUpdate()
		{
			for (int i = 0; i < this.source.Length; i++)
			{
				SharedString sharedString = this.storeResult;
				string value = sharedString.Value;
				SharedString sharedString2 = this.source[i];
				sharedString.Value = value + ((sharedString2 != null) ? sharedString2.ToString() : null);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001C47B File Offset: 0x0001A67B
		public override void OnReset()
		{
			this.source = null;
			this.storeResult = null;
		}

		// Token: 0x04000471 RID: 1137
		[Tooltip("The array of strings")]
		public SharedString[] source;

		// Token: 0x04000472 RID: 1138
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}

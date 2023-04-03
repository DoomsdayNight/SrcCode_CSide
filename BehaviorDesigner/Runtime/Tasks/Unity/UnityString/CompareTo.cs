using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x02000135 RID: 309
	[TaskCategory("Unity/String")]
	[TaskDescription("Compares the first string to the second string. Returns an int which indicates whether the first string precedes, matches, or follows the second string.")]
	public class CompareTo : Action
	{
		// Token: 0x06000866 RID: 2150 RVA: 0x0001C493 File Offset: 0x0001A693
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.firstString.Value.CompareTo(this.secondString.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001C4BC File Offset: 0x0001A6BC
		public override void OnReset()
		{
			this.firstString = "";
			this.secondString = "";
			this.storeResult = 0;
		}

		// Token: 0x04000473 RID: 1139
		[Tooltip("The string to compare")]
		public SharedString firstString;

		// Token: 0x04000474 RID: 1140
		[Tooltip("The string to compare to")]
		public SharedString secondString;

		// Token: 0x04000475 RID: 1141
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedInt storeResult;
	}
}

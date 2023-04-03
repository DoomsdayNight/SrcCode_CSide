using System;
using System.Collections;

namespace NKC.Patcher
{
	// Token: 0x02000877 RID: 2167
	public interface IPatchProcessStrategy : IEnumerable
	{
		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06005664 RID: 22116
		IPatchProcessStrategy.ExecutionStatus Status { get; }

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06005665 RID: 22117
		string ReasonOfFailure { get; }

		// Token: 0x02001543 RID: 5443
		public enum ExecutionStatus
		{
			// Token: 0x0400A066 RID: 41062
			Success,
			// Token: 0x0400A067 RID: 41063
			Fail,
			// Token: 0x0400A068 RID: 41064
			SkipNextProcess
		}
	}
}

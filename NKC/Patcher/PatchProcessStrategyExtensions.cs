using System;

namespace NKC.Patcher
{
	// Token: 0x02000878 RID: 2168
	public static class PatchProcessStrategyExtensions
	{
		// Token: 0x06005666 RID: 22118 RVA: 0x001A1592 File Offset: 0x0019F792
		public static bool ErrorOccurred(this IPatchProcessStrategy process)
		{
			return process.Status == IPatchProcessStrategy.ExecutionStatus.Fail;
		}

		// Token: 0x06005667 RID: 22119 RVA: 0x001A159D File Offset: 0x0019F79D
		public static bool SkipNextProcess(this IPatchProcessStrategy process)
		{
			return process.Status == IPatchProcessStrategy.ExecutionStatus.SkipNextProcess;
		}
	}
}

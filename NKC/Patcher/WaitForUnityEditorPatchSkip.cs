using System;
using System.Collections;

namespace NKC.Patcher
{
	// Token: 0x0200087B RID: 2171
	public class WaitForUnityEditorPatchSkip : IPatchProcessStrategy, IEnumerable
	{
		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x0600566F RID: 22127 RVA: 0x001A15F5 File Offset: 0x0019F7F5
		// (set) Token: 0x0600566E RID: 22126 RVA: 0x001A15EC File Offset: 0x0019F7EC
		public IPatchProcessStrategy.ExecutionStatus Status { get; private set; }

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06005671 RID: 22129 RVA: 0x001A1606 File Offset: 0x0019F806
		// (set) Token: 0x06005670 RID: 22128 RVA: 0x001A15FD File Offset: 0x0019F7FD
		public string ReasonOfFailure { get; private set; } = string.Empty;

		// Token: 0x06005672 RID: 22130 RVA: 0x001A160E File Offset: 0x0019F80E
		public IEnumerator GetEnumerator()
		{
			this.Status = IPatchProcessStrategy.ExecutionStatus.Success;
			this.ReasonOfFailure = string.Empty;
			yield break;
		}
	}
}

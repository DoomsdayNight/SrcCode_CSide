using System;
using System.Collections;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x0200087A RID: 2170
	public class WaitForInternetConnection : IPatchProcessStrategy, IEnumerable
	{
		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06005669 RID: 22121 RVA: 0x001A15B1 File Offset: 0x0019F7B1
		// (set) Token: 0x06005668 RID: 22120 RVA: 0x001A15A8 File Offset: 0x0019F7A8
		public IPatchProcessStrategy.ExecutionStatus Status { get; private set; }

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x0600566B RID: 22123 RVA: 0x001A15C2 File Offset: 0x0019F7C2
		// (set) Token: 0x0600566A RID: 22122 RVA: 0x001A15B9 File Offset: 0x0019F7B9
		public string ReasonOfFailure { get; private set; } = string.Empty;

		// Token: 0x0600566C RID: 22124 RVA: 0x001A15CA File Offset: 0x0019F7CA
		public IEnumerator GetEnumerator()
		{
			this.Status = IPatchProcessStrategy.ExecutionStatus.Success;
			this.ReasonOfFailure = string.Empty;
			NKCPatcherUI patcherUI = NKCPatchChecker.PatcherUI;
			if (patcherUI != null)
			{
				patcherUI.SetProgressText(NKCUtilString.GET_STRING_PATCHER_CHECKING_VERSION_INFORMATION);
			}
			NKCPatcherUI patcherUI2 = NKCPatchChecker.PatcherUI;
			if (patcherUI2 != null)
			{
				patcherUI2.Progress();
			}
			while (Application.internetReachability == NetworkReachability.NotReachable)
			{
				yield return NKCPatcherManager.GetPatcherManager().WaitForOKBox(NKCUtilString.GET_STRING_PATCHER_WARNING, NKCUtilString.GET_STRING_DECONNECT_INTERNET, NKCUtilString.GET_STRING_RETRY);
			}
			yield break;
		}
	}
}

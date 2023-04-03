using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace NKC.Patcher
{
	// Token: 0x02000880 RID: 2176
	public class WaitForTouch : IPatchProcessStrategy, IEnumerable
	{
		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x0600568E RID: 22158 RVA: 0x001A1766 File Offset: 0x0019F966
		// (set) Token: 0x0600568D RID: 22157 RVA: 0x001A175D File Offset: 0x0019F95D
		public IPatchProcessStrategy.ExecutionStatus Status { get; private set; }

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x06005690 RID: 22160 RVA: 0x001A1777 File Offset: 0x0019F977
		// (set) Token: 0x0600568F RID: 22159 RVA: 0x001A176E File Offset: 0x0019F96E
		public string ReasonOfFailure { get; private set; } = string.Empty;

		// Token: 0x06005691 RID: 22161 RVA: 0x001A177F File Offset: 0x0019F97F
		private bool IsNullBackGroundText()
		{
			return NKCPatchChecker.PatcherUI != null && NKCPatchChecker.PatcherUI.BackGroundTextIsNull();
		}

		// Token: 0x06005692 RID: 22162 RVA: 0x001A179A File Offset: 0x0019F99A
		public IEnumerator GetEnumerator()
		{
			this.Status = IPatchProcessStrategy.ExecutionStatus.Success;
			this.ReasonOfFailure = string.Empty;
			if (!NKCPatchDownloader.Instance.ProloguePlay && WaitForTouch.<GetEnumerator>g__IsDownloading|9_0() && !this.IsNullBackGroundText())
			{
				NKCPatcherUI patcherUI = NKCPatchChecker.PatcherUI;
				if (patcherUI != null)
				{
					patcherUI.SetForTouchWait();
				}
				yield return NKCPatchChecker.PatcherUI.WaitForTouch();
			}
			NKCPatcherUI patcherUI2 = NKCPatchChecker.PatcherUI;
			if (patcherUI2 != null)
			{
				patcherUI2.SetProgressText(NKCUtilString.GET_STRING_PATCHER_INITIALIZING);
			}
			NKCPatcherUI patcherUI3 = NKCPatchChecker.PatcherUI;
			if (patcherUI3 != null)
			{
				patcherUI3.Progress();
			}
			yield return null;
			yield break;
		}

		// Token: 0x06005694 RID: 22164 RVA: 0x001A17BC File Offset: 0x0019F9BC
		[CompilerGenerated]
		internal static bool <GetEnumerator>g__IsDownloading|9_0()
		{
			return NKCPatchDownloader.Instance.DownloadStatus == NKCPatchDownloader.PatchDownloadStatus.Finished;
		}
	}
}

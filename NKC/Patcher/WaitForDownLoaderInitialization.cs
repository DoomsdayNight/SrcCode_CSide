using System;
using System.Collections;

namespace NKC.Patcher
{
	// Token: 0x0200087C RID: 2172
	public class WaitForDownLoaderInitialization : IPatchProcessStrategy, IEnumerable
	{
		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06005675 RID: 22133 RVA: 0x001A1639 File Offset: 0x0019F839
		// (set) Token: 0x06005674 RID: 22132 RVA: 0x001A1630 File Offset: 0x0019F830
		public IPatchProcessStrategy.ExecutionStatus Status { get; private set; }

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06005677 RID: 22135 RVA: 0x001A164A File Offset: 0x0019F84A
		// (set) Token: 0x06005676 RID: 22134 RVA: 0x001A1641 File Offset: 0x0019F841
		public string ReasonOfFailure { get; private set; } = string.Empty;

		// Token: 0x06005678 RID: 22136 RVA: 0x001A1652 File Offset: 0x0019F852
		public IEnumerator GetEnumerator()
		{
			this.Status = IPatchProcessStrategy.ExecutionStatus.Success;
			this.ReasonOfFailure = string.Empty;
			if (NKCPatchDownloader.Instance == null)
			{
				if (NKCDefineManager.DEFINE_EXTRA_ASSET() || NKCDefineManager.DEFINE_ZLONG_CHN())
				{
					LegacyPatchDownloader.InitInstance(new NKCPatchDownloader.OnError(NKCPatcherManager.GetPatcherManager().ShowError));
				}
				else
				{
					NKCPatchParallelDownloader.InitInstance(new NKCPatchDownloader.OnError(NKCPatcherManager.GetPatcherManager().ShowError));
				}
			}
			NKCPatchDownloader.Instance.StopBackgroundDownload();
			if (NKCPatchChecker.PatcherVideoPlayer != null)
			{
				NKCPatchChecker.PatcherVideoPlayer.PlayVideo();
			}
			if (NKCDefineManager.DEFINE_USE_CHEAT())
			{
				NKCPatchUtility.ProcessPatchSkipTest(NKCPatchDownloader.Instance.LocalDownloadPath);
			}
			NKCPatcherUI patcherUI = NKCPatchChecker.PatcherUI;
			if (patcherUI != null)
			{
				patcherUI.SetIntegrityCheckProgress();
			}
			NKCPatcherUI patcherUI2 = NKCPatchChecker.PatcherUI;
			if (patcherUI2 != null)
			{
				patcherUI2.Progress();
			}
			while (!NKCPatchDownloader.Instance.IsInit)
			{
				yield return null;
			}
			yield break;
		}
	}
}

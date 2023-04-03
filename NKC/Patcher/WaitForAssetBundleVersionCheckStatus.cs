using System;
using System.Collections;
using NKC.Publisher;
using NKC.UI;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x0200087E RID: 2174
	public class WaitForAssetBundleVersionCheckStatus : IPatchProcessStrategy, IEnumerable
	{
		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06005681 RID: 22145 RVA: 0x001A16C1 File Offset: 0x0019F8C1
		// (set) Token: 0x06005680 RID: 22144 RVA: 0x001A16B8 File Offset: 0x0019F8B8
		public IPatchProcessStrategy.ExecutionStatus Status { get; private set; }

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06005683 RID: 22147 RVA: 0x001A16D2 File Offset: 0x0019F8D2
		// (set) Token: 0x06005682 RID: 22146 RVA: 0x001A16C9 File Offset: 0x0019F8C9
		public string ReasonOfFailure { get; private set; } = string.Empty;

		// Token: 0x06005684 RID: 22148 RVA: 0x001A16DA File Offset: 0x0019F8DA
		public IEnumerator GetEnumerator()
		{
			this.Status = IPatchProcessStrategy.ExecutionStatus.Success;
			this.ReasonOfFailure = string.Empty;
			while (NKCPatchDownloader.Instance.VersionCheckStatus == NKCPatchDownloader.VersionStatus.Unchecked)
			{
				yield return null;
			}
			NKCPatchDownloader.Instance.ProloguePlay = false;
			switch (NKCPatchDownloader.Instance.VersionCheckStatus)
			{
			case NKCPatchDownloader.VersionStatus.UpToDate:
				if (NKCDefineManager.DEFINE_SEMI_FULL_BUILD())
				{
					NKCPatchDownloader.Instance.DoWhenEndDownload();
				}
				break;
			case NKCPatchDownloader.VersionStatus.RequireDownload:
			{
				WaitForAssetBundleVersionCheckStatus.<>c__DisplayClass8_0 CS$<>8__locals1 = new WaitForAssetBundleVersionCheckStatus.<>c__DisplayClass8_0();
				NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_DownloadAvailable, 0, null);
				float num = (float)NKCPatchDownloader.Instance.TotalSize / 1048576f;
				string message = string.Format(NKCUtilString.GET_STRING_NOTICE_DOWNLOAD_ONE_PARAM, num);
				CS$<>8__locals1.m_bUserPermission = false;
				while (!CS$<>8__locals1.m_bUserPermission)
				{
					yield return NKCPatcherManager.GetPatcherManager().WaitForOKCancel(NKCUtilString.GET_STRING_PATCHER_WARNING, message, "", "", delegate
					{
						CS$<>8__locals1.m_bUserPermission = true;
					});
					if (!CS$<>8__locals1.m_bUserPermission)
					{
						yield return NKCPatcherManager.GetPatcherManager().WaitForOKCancel(NKCUtilString.GET_STRING_PATCHER_WARNING, NKCStringTable.GetString("SI_DP_PATCHER_QUIT_CONFIRM", false), "", "", new NKCPopupOKCancel.OnButton(Application.Quit));
					}
				}
				NKCAdjustManager.OnCustomEvent("02_downLoad_start");
				NKCPatchDownloader.Instance.StartFileDownload();
				NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_DownloadStart, 0, null);
				CS$<>8__locals1 = null;
				message = null;
				break;
			}
			case NKCPatchDownloader.VersionStatus.Error:
				NKCPatcherManager.GetPatcherManager().ShowError(NKCPatchDownloader.Instance.ErrorString);
				this.Status = IPatchProcessStrategy.ExecutionStatus.Fail;
				this.ReasonOfFailure = "[PatchProcess] " + NKCPatchDownloader.Instance.ErrorString;
				yield break;
			}
			yield break;
		}
	}
}

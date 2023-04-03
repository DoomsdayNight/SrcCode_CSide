using System;
using System.Collections;
using System.Collections.Generic;
using AssetBundles;
using NKC.Publisher;
using NKC.UI;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x0200087D RID: 2173
	public class WaitForAppVersionCheckStatus : IPatchProcessStrategy, IEnumerable
	{
		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x0600567B RID: 22139 RVA: 0x001A167D File Offset: 0x0019F87D
		// (set) Token: 0x0600567A RID: 22138 RVA: 0x001A1674 File Offset: 0x0019F874
		public IPatchProcessStrategy.ExecutionStatus Status { get; private set; }

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x0600567D RID: 22141 RVA: 0x001A168E File Offset: 0x0019F88E
		// (set) Token: 0x0600567C RID: 22140 RVA: 0x001A1685 File Offset: 0x0019F885
		public string ReasonOfFailure { get; private set; } = string.Empty;

		// Token: 0x0600567E RID: 22142 RVA: 0x001A1696 File Offset: 0x0019F896
		public IEnumerator GetEnumerator()
		{
			this.Status = IPatchProcessStrategy.ExecutionStatus.Success;
			this.ReasonOfFailure = string.Empty;
			if (PlayerPrefsContainer.GetBoolean("PatchIntegrityCheck"))
			{
				NKCPatcherUI patcherUI = NKCPatchChecker.PatcherUI;
				if (patcherUI != null)
				{
					patcherUI.SetProgressText(NKCStringTable.GetString("SI_DP_PATCHER_INTEGRITY_CHECK", false));
				}
				NKCPatcherUI patcherUI2 = NKCPatchChecker.PatcherUI;
				if (patcherUI2 != null)
				{
					patcherUI2.Progress();
				}
				yield return null;
			}
			NKCPatcherUI patcherUI3 = NKCPatchChecker.PatcherUI;
			if (patcherUI3 != null)
			{
				patcherUI3.Progress();
			}
			Debug.Log("IPatchProcessStrategy Begin WaitForVersionCheckStatus");
			NKCPatchDownloader.Instance.InitCheckTime();
			NKCPatchDownloader.Instance.CheckVersion(new List<string>(AssetBundleManager.ActiveVariants), PlayerPrefsContainer.GetBoolean("PatchIntegrityCheck"));
			PlayerPrefsContainer.Set("PatchIntegrityCheck", false);
			while (NKCPatchDownloader.Instance.BuildCheckStatus == NKCPatchDownloader.BuildStatus.Unchecked)
			{
				yield return null;
			}
			switch (NKCPatchDownloader.Instance.BuildCheckStatus)
			{
			case NKCPatchDownloader.BuildStatus.UpdateAvailable:
				yield return NKCPatcherManager.GetPatcherManager().WaitForOKCancel(NKCUtilString.GET_STRING_PATCHER_NOTICE, NKCUtilString.GET_STRING_PATCHER_CAN_UPDATE, NKCUtilString.GET_STRING_PATCHER_MOVE_TO_MARKET, NKCUtilString.GET_STRING_PATCHER_CONTINUE, new NKCPopupOKCancel.OnButton(NKCPatcherManager.GetPatcherManager().MoveToMarket));
				break;
			case NKCPatchDownloader.BuildStatus.RequireAppUpdate:
				NKCPatcherManager.GetPatcherManager().ShowUpdate();
				break;
			case NKCPatchDownloader.BuildStatus.Error:
				NKCPatcherManager.GetPatcherManager().ShowError(NKCPatchDownloader.Instance.ErrorString);
				this.Status = IPatchProcessStrategy.ExecutionStatus.Fail;
				this.ReasonOfFailure = "[PatchProcess] " + NKCPatchDownloader.Instance.ErrorString;
				yield break;
			}
			NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_VersionCheckComplete, 0, null);
			NKCPatcherUI patcherUI4 = NKCPatchChecker.PatcherUI;
			if (patcherUI4 != null)
			{
				patcherUI4.Progress();
			}
			yield break;
		}
	}
}

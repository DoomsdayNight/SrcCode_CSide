using System;
using System.Collections.Generic;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x0200086A RID: 2154
	public class DownloadHistoryController
	{
		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x0600559A RID: 21914 RVA: 0x0019F136 File Offset: 0x0019D336
		// (set) Token: 0x06005599 RID: 21913 RVA: 0x0019F12D File Offset: 0x0019D32D
		public long CurrentDownloadedCompletedSize { get; private set; }

		// Token: 0x0600559B RID: 21915 RVA: 0x0019F13E File Offset: 0x0019D33E
		public void SetDownloadHistoryPatchInfo(NKCPatchInfo downloadHistoryPatchInfo)
		{
			this._downloadHistoryPatchInfo = downloadHistoryPatchInfo;
		}

		// Token: 0x0600559C RID: 21916 RVA: 0x0019F147 File Offset: 0x0019D347
		public void CleanUp()
		{
			this._lstDownloadCompletedThisFrame.Clear();
			this.CurrentDownloadedCompletedSize = 0L;
		}

		// Token: 0x0600559D RID: 21917 RVA: 0x0019F15C File Offset: 0x0019D35C
		public void AddTo(NKCPatchInfo.PatchFileInfo patchFile)
		{
			if (this._lstDownloadCompletedThisFrame.Contains(patchFile))
			{
				return;
			}
			this._lstDownloadCompletedThisFrame.Add(patchFile);
		}

		// Token: 0x0600559E RID: 21918 RVA: 0x0019F17C File Offset: 0x0019D37C
		public void UpdateDownloadHistoryPatchInfo()
		{
			if (this._downloadHistoryPatchInfo == null)
			{
				Debug.LogWarning("[UpdateDownloadHistoryPatchInfo] downloadHistoryInfo is null");
				return;
			}
			foreach (NKCPatchInfo.PatchFileInfo patchFileInfo in this._lstDownloadCompletedThisFrame)
			{
				this._downloadHistoryPatchInfo.AddPatchFileInfo(patchFileInfo);
				this.CurrentDownloadedCompletedSize += patchFileInfo.Size;
			}
			this._downloadHistoryPatchInfo.SaveAsJSON();
			this._lstDownloadCompletedThisFrame.Clear();
		}

		// Token: 0x04004469 RID: 17513
		private readonly List<NKCPatchInfo.PatchFileInfo> _lstDownloadCompletedThisFrame = new List<NKCPatchInfo.PatchFileInfo>();

		// Token: 0x0400446A RID: 17514
		private NKCPatchInfo _downloadHistoryPatchInfo;
	}
}

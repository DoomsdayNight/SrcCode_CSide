using System;
using System.Collections;
using Cs.Engine.Util;
using Cs.Logging;
using NKC.Publisher;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x02000882 RID: 2178
	public class WaitForCheckVersion : IPatchProcessStrategy, IEnumerable
	{
		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x0600569A RID: 22170 RVA: 0x001A17EE File Offset: 0x0019F9EE
		// (set) Token: 0x06005699 RID: 22169 RVA: 0x001A17E5 File Offset: 0x0019F9E5
		public IPatchProcessStrategy.ExecutionStatus Status { get; private set; }

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x0600569C RID: 22172 RVA: 0x001A17FF File Offset: 0x0019F9FF
		// (set) Token: 0x0600569B RID: 22171 RVA: 0x001A17F6 File Offset: 0x0019F9F6
		public string ReasonOfFailure { get; private set; } = string.Empty;

		// Token: 0x0600569D RID: 22173 RVA: 0x001A1807 File Offset: 0x0019FA07
		private void StopBg()
		{
			if (NKCPatchChecker.PatcherVideoPlayer != null)
			{
				NKCPatchChecker.PatcherVideoPlayer.StopBG();
			}
		}

		// Token: 0x0600569E RID: 22174 RVA: 0x001A1820 File Offset: 0x0019FA20
		public IEnumerator GetEnumerator()
		{
			this.Status = IPatchProcessStrategy.ExecutionStatus.Success;
			this.ReasonOfFailure = string.Empty;
			Log.Debug("[PatcherManager] Stop BGM", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchProcessStrategy/PatcherProcessStrategies.cs", 385);
			if (NKCDefineManager.DEFINE_CHECKVERSION())
			{
				if (ContentsVersionChecker.VersionAckReceived)
				{
					this.StopBg();
				}
				else
				{
					float versionReqRetryTime = 0f;
					int versionRequestRetryCount = 0;
					while (!ContentsVersionChecker.VersionAckReceived)
					{
						versionReqRetryTime -= Time.deltaTime;
						if (versionReqRetryTime <= 0f)
						{
							NKCPatcherManager.GetPatcherManager().ShowRequestTimer(true);
							string serviceIP = NKCConnectionInfo.ServiceIP;
							Log.Debug("IPatchProcessStrategy Trying to retrieve server tag from " + serviceIP, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/NKCPatchProcessStrategy/PatcherProcessStrategies.cs", 409);
							yield return ContentsVersionChecker.GetVersion(serviceIP, -1, false);
							versionReqRetryTime = ContentsVersionChecker.RetryInterval;
							versionRequestRetryCount++;
						}
						if (versionRequestRetryCount >= 1)
						{
							break;
						}
						yield return null;
					}
					if (ContentsVersionChecker.Ack != null)
					{
						NKCPublisherModule.Statistics.LogClientAction(NKCPublisherModule.NKCPMStatistics.eClientAction.Patch_TagProvided, 0, null);
					}
					NKCPatcherManager.GetPatcherManager().ShowRequestTimer(false);
					this.StopBg();
				}
			}
			else
			{
				this.StopBg();
			}
			yield break;
		}
	}
}

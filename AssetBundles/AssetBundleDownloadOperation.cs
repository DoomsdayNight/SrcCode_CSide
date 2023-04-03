using System;

namespace AssetBundles
{
	// Token: 0x0200004D RID: 77
	public abstract class AssetBundleDownloadOperation : AssetBundleLoadOperation
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000A5F2 File Offset: 0x000087F2
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000A5FA File Offset: 0x000087FA
		public string assetBundleName { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000A603 File Offset: 0x00008803
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000A60B File Offset: 0x0000880B
		public LoadedAssetBundle assetBundle { get; protected set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000A614 File Offset: 0x00008814
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000A61C File Offset: 0x0000881C
		public string error { get; protected set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600025C RID: 604
		protected abstract bool downloadIsDone { get; }

		// Token: 0x0600025D RID: 605
		protected abstract void FinishDownload();

		// Token: 0x0600025E RID: 606 RVA: 0x0000A625 File Offset: 0x00008825
		public override bool Update()
		{
			if (!this.done && this.downloadIsDone)
			{
				this.FinishDownload();
				this.done = true;
			}
			return !this.done;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A64D File Offset: 0x0000884D
		public override bool IsDone()
		{
			return this.done;
		}

		// Token: 0x06000260 RID: 608
		public abstract string GetSourceURL();

		// Token: 0x06000261 RID: 609 RVA: 0x0000A655 File Offset: 0x00008855
		public AssetBundleDownloadOperation(string assetBundleName)
		{
			this.assetBundleName = assetBundleName;
		}

		// Token: 0x040001B8 RID: 440
		private bool done;

		// Token: 0x040001BC RID: 444
		public bool bForceFlush;
	}
}

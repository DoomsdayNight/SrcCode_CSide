using System;
using System.Collections;
using AssetBundles;

namespace NKC.Patcher
{
	// Token: 0x02000881 RID: 2177
	public class WaitForAssetBundleInitialize : IPatchProcessStrategy, IEnumerable
	{
		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x06005695 RID: 22165 RVA: 0x001A17CB File Offset: 0x0019F9CB
		public IPatchProcessStrategy.ExecutionStatus Status
		{
			get
			{
				return IPatchProcessStrategy.ExecutionStatus.Success;
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x06005696 RID: 22166 RVA: 0x001A17CE File Offset: 0x0019F9CE
		public string ReasonOfFailure
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06005697 RID: 22167 RVA: 0x001A17D5 File Offset: 0x0019F9D5
		public IEnumerator GetEnumerator()
		{
			yield return AssetBundleManager.Initialize();
			yield break;
		}
	}
}

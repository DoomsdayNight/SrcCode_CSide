using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC;
using SimpleJSON;
using UnityEngine;

namespace NKM
{
	// Token: 0x02000425 RID: 1061
	internal sealed class NKMJson
	{
		// Token: 0x06001C9E RID: 7326 RVA: 0x00084FF4 File Offset: 0x000831F4
		public bool LoadCommonPath(string bundleName, string fileName)
		{
			if (this.stack.Count > 0)
			{
				throw new Exception(string.Format("try to using uninitialized instance stackCount:{0}", this.stack.Count));
			}
			NKCAssetResourceData nkcassetResourceData = null;
			bool result;
			try
			{
				nkcassetResourceData = NKCAssetResourceManager.OpenResource<TextAsset>(bundleName, fileName, false, null);
				TextAsset asset = nkcassetResourceData.GetAsset<TextAsset>();
				if (asset == null)
				{
					Log.Error("Resources.Load null: " + fileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMJson.cs", 37);
					result = false;
				}
				else
				{
					this.stack.Push(JSON.Parse(asset.ToString()));
					NKCAssetResourceManager.CloseResource(nkcassetResourceData);
					result = true;
				}
			}
			catch (Exception ex)
			{
				Log.ErrorAndExit("루아 로딩 에러 FileName : " + fileName + ", BundleName : " + bundleName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMJson.cs", 47);
				Log.ErrorAndExit(ex.Message, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMJson.cs", 48);
				NKCAssetResourceManager.CloseResource(nkcassetResourceData);
				result = false;
			}
			return result;
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x000850D4 File Offset: 0x000832D4
		private void PushStack(JSONNode node)
		{
			this.stack.Push(node);
		}

		// Token: 0x04001BE7 RID: 7143
		private readonly Stack<JSONNode> stack = new Stack<JSONNode>();
	}
}

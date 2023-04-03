using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x0200088B RID: 2187
	public static class PatchLogContainer
	{
		// Token: 0x0600570E RID: 22286 RVA: 0x001A2F10 File Offset: 0x001A1110
		public static void AddToLog(string key, NKCPatchInfo.PatchFileInfo newInfo, string path)
		{
			if (!NKCDefineManager.DEFINE_SAVE_LOG())
			{
				return;
			}
			List<PatchLogContainer.DownloadResult> list;
			if (!PatchLogContainer._downloadListContainer.TryGetValue(key, out list))
			{
				list = new List<PatchLogContainer.DownloadResult>();
				PatchLogContainer._downloadListContainer.Add(key, list);
			}
			PatchLogContainer.DownloadResult item = new PatchLogContainer.DownloadResult(newInfo, path);
			list.Add(item);
		}

		// Token: 0x0600570F RID: 22287 RVA: 0x001A2F58 File Offset: 0x001A1158
		public static void DownloadListLogOutPut()
		{
			if (!NKCDefineManager.DEFINE_SAVE_LOG())
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DownloadListLogOutPut]");
			foreach (KeyValuePair<string, List<PatchLogContainer.DownloadResult>> keyValuePair in PatchLogContainer._downloadListContainer)
			{
				double totalSize = 0.0;
				keyValuePair.Value.ForEach(delegate(PatchLogContainer.DownloadResult _)
				{
					totalSize += (double)_.FileInfo.Size;
				});
				stringBuilder.Append(string.Format(":: [{0}][Count:{1}][Size:{2}mb] ::", keyValuePair.Key, keyValuePair.Value.Count, (double)((float)totalSize) / 1048576.0));
				foreach (PatchLogContainer.DownloadResult downloadResult in keyValuePair.Value)
				{
					stringBuilder.Append(string.Concat(new string[]
					{
						" - [",
						keyValuePair.Key,
						"][Path:",
						downloadResult.Path,
						"][FileName:",
						downloadResult.FileInfo.FileName,
						"]"
					}));
				}
			}
			Debug.Log(stringBuilder);
			PatchLogContainer.Clear();
		}

		// Token: 0x06005710 RID: 22288 RVA: 0x001A30E8 File Offset: 0x001A12E8
		public static void Clear()
		{
			PatchLogContainer._downloadListContainer.Clear();
		}

		// Token: 0x040044FE RID: 17662
		public const string NotExistInCurrentManifest = "Not exist in currentManifest";

		// Token: 0x040044FF RID: 17663
		public const string IsOldFile = "Is old file";

		// Token: 0x04004500 RID: 17664
		public const string DifferentSize = "Different size";

		// Token: 0x04004501 RID: 17665
		public const string CheckIntegrity = "Check integrity";

		// Token: 0x04004502 RID: 17666
		public const string NotRequiredUpdate = "Not required update";

		// Token: 0x04004503 RID: 17667
		public const string Inner_NotExist = "[Inner] Not exist";

		// Token: 0x04004504 RID: 17668
		public const string Inner_DifferentSize = "[Inner] Deferent size";

		// Token: 0x04004505 RID: 17669
		public const string Inner_CheckIntegrity = "[Inner] Check integrity";

		// Token: 0x04004506 RID: 17670
		private static readonly Dictionary<string, List<PatchLogContainer.DownloadResult>> _downloadListContainer = new Dictionary<string, List<PatchLogContainer.DownloadResult>>();

		// Token: 0x02001559 RID: 5465
		private class DownloadResult
		{
			// Token: 0x0600ACB1 RID: 44209 RVA: 0x00356171 File Offset: 0x00354371
			public DownloadResult(NKCPatchInfo.PatchFileInfo fileInfo, string path)
			{
				this.FileInfo = fileInfo;
				this.Path = path;
			}

			// Token: 0x0400A0AA RID: 41130
			public readonly NKCPatchInfo.PatchFileInfo FileInfo;

			// Token: 0x0400A0AB RID: 41131
			public readonly string Path;
		}
	}
}

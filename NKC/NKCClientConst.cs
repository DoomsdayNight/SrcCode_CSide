using System;
using Cs.Logging;
using NKM;

namespace NKC
{
	// Token: 0x02000649 RID: 1609
	public static class NKCClientConst
	{
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x000FB7A4 File Offset: 0x000F99A4
		// (set) Token: 0x0600327A RID: 12922 RVA: 0x000FB7AB File Offset: 0x000F99AB
		public static float DiveAutoSpeed { get; private set; } = 1f;

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x000FB7B3 File Offset: 0x000F99B3
		// (set) Token: 0x0600327C RID: 12924 RVA: 0x000FB7BA File Offset: 0x000F99BA
		public static float NextTalkChangeSpeedWhenAuto_Fast { get; private set; } = 1f;

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x000FB7C2 File Offset: 0x000F99C2
		// (set) Token: 0x0600327E RID: 12926 RVA: 0x000FB7C9 File Offset: 0x000F99C9
		public static float NextTalkChangeSpeedWhenAuto_Normal { get; private set; } = 1.2f;

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000FB7D1 File Offset: 0x000F99D1
		// (set) Token: 0x06003280 RID: 12928 RVA: 0x000FB7D8 File Offset: 0x000F99D8
		public static float NextTalkChangeSpeedWhenAuto_Slow { get; private set; } = 1.4f;

		// Token: 0x06003281 RID: 12929 RVA: 0x000FB7E0 File Offset: 0x000F99E0
		public static void LoadFromLUA(string fileName)
		{
			bool flag = true;
			using (NKMLua nkmlua = new NKMLua())
			{
				if (!nkmlua.LoadCommonPath("AB_SCRIPT", fileName, true))
				{
					Log.ErrorAndExit("fail loading lua file:" + fileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCClientConst.cs", 24);
					return;
				}
				using (nkmlua.OpenTable("Dive", "[ClientConst] loading Dive table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCClientConst.cs", 28))
				{
					float diveAutoSpeed = 1f;
					flag &= nkmlua.GetData("fAutoSpeed", ref diveAutoSpeed);
					NKCClientConst.DiveAutoSpeed = diveAutoSpeed;
				}
				using (nkmlua.OpenTable("Cutscen", "[ClientConst] loading Cutscen table failed.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCClientConst.cs", 36))
				{
					float num = 1f;
					flag &= nkmlua.GetData("fNextTalkChangeSpeedWhenAuto_Fast", ref num);
					NKCClientConst.NextTalkChangeSpeedWhenAuto_Fast = num;
					flag &= nkmlua.GetData("fNextTalkChangeSpeedWhenAuto_Normal", ref num);
					NKCClientConst.NextTalkChangeSpeedWhenAuto_Normal = num;
					flag &= nkmlua.GetData("fNextTalkChangeSpeedWhenAuto_Slow", ref num);
					NKCClientConst.NextTalkChangeSpeedWhenAuto_Slow = num;
				}
			}
			if (!flag)
			{
				Log.ErrorAndExit("fail loading lua file:" + fileName, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCClientConst.cs", 53);
			}
		}
	}
}

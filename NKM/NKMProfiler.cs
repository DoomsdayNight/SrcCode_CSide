using System;

namespace NKM
{
	// Token: 0x0200045A RID: 1114
	internal static class NKMProfiler
	{
		// Token: 0x06001E3A RID: 7738 RVA: 0x0008FAA5 File Offset: 0x0008DCA5
		internal static void SetProvider(IProfilerProvider provider)
		{
			NKMProfiler.provider = provider;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0008FAAD File Offset: 0x0008DCAD
		internal static void BeginSample(string name)
		{
			NKMProfiler.provider.BeginSample(name);
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0008FABA File Offset: 0x0008DCBA
		internal static void EndSample()
		{
			NKMProfiler.provider.EndSample();
		}

		// Token: 0x04001EEA RID: 7914
		private static IProfilerProvider provider = new NKMProfiler.NullProvider();

		// Token: 0x020011FB RID: 4603
		private sealed class NullProvider : IProfilerProvider
		{
			// Token: 0x0600A163 RID: 41315 RVA: 0x0033FEE8 File Offset: 0x0033E0E8
			public void BeginSample(string name)
			{
			}

			// Token: 0x0600A164 RID: 41316 RVA: 0x0033FEEA File Offset: 0x0033E0EA
			public void EndSample()
			{
			}
		}
	}
}

using System;

namespace NKM
{
	// Token: 0x02000459 RID: 1113
	internal interface IProfilerProvider
	{
		// Token: 0x06001E38 RID: 7736
		void BeginSample(string name);

		// Token: 0x06001E39 RID: 7737
		void EndSample();
	}
}

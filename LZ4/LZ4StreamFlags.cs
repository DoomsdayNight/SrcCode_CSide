using System;

namespace LZ4
{
	// Token: 0x02000612 RID: 1554
	[Flags]
	public enum LZ4StreamFlags
	{
		// Token: 0x04002F8D RID: 12173
		None = 0,
		// Token: 0x04002F8E RID: 12174
		InteractiveRead = 1,
		// Token: 0x04002F8F RID: 12175
		HighCompression = 2,
		// Token: 0x04002F90 RID: 12176
		IsolateInnerStream = 4,
		// Token: 0x04002F91 RID: 12177
		Default = 0
	}
}

using System;
using System.Threading;

namespace NKM
{
	// Token: 0x0200041A RID: 1050
	public static class NpcUid
	{
		// Token: 0x06001B62 RID: 7010 RVA: 0x00078106 File Offset: 0x00076306
		public static long Get()
		{
			return Interlocked.Increment(ref NpcUid.NpcUidIndex);
		}

		// Token: 0x04001B49 RID: 6985
		public const long NpcUidStart = 1000000000L;

		// Token: 0x04001B4A RID: 6986
		private static long NpcUidIndex = 1000000000L;
	}
}

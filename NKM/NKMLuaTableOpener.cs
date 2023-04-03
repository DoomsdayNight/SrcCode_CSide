using System;

namespace NKM
{
	// Token: 0x02000427 RID: 1063
	public sealed class NKMLuaTableOpener : IDisposable
	{
		// Token: 0x06001CF7 RID: 7415 RVA: 0x0008706E File Offset: 0x0008526E
		public NKMLuaTableOpener(NKMLua lua)
		{
			this.lua = lua;
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0008707D File Offset: 0x0008527D
		public void Dispose()
		{
			this.lua.CloseTable();
		}

		// Token: 0x04001BF0 RID: 7152
		private readonly NKMLua lua;
	}
}

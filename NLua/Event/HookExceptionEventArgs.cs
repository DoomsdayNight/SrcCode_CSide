using System;

namespace NLua.Event
{
	// Token: 0x0200007E RID: 126
	public class HookExceptionEventArgs : EventArgs
	{
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00014CED File Offset: 0x00012EED
		public Exception Exception { get; }

		// Token: 0x0600045B RID: 1115 RVA: 0x00014CF5 File Offset: 0x00012EF5
		public HookExceptionEventArgs(Exception ex)
		{
			this.Exception = ex;
		}
	}
}

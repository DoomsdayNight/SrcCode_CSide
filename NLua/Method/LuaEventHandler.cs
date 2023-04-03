using System;

namespace NLua.Method
{
	// Token: 0x02000072 RID: 114
	public class LuaEventHandler
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x00013ED4 File Offset: 0x000120D4
		public void HandleEvent(object[] args)
		{
			this.Handler.Call(args);
		}

		// Token: 0x04000247 RID: 583
		public LuaFunction Handler;
	}
}

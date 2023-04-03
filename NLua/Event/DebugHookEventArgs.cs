using System;
using KeraLua;

namespace NLua.Event
{
	// Token: 0x0200007D RID: 125
	public class DebugHookEventArgs : EventArgs
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x00014CD6 File Offset: 0x00012ED6
		public DebugHookEventArgs(LuaDebug luaDebug)
		{
			this.LuaDebug = luaDebug;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00014CE5 File Offset: 0x00012EE5
		public LuaDebug LuaDebug { get; }
	}
}

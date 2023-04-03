using System;
using System.Runtime.InteropServices;

namespace KeraLua
{
	// Token: 0x0200008E RID: 142
	public struct LuaRegister
	{
		// Token: 0x040002A0 RID: 672
		public string name;

		// Token: 0x040002A1 RID: 673
		[MarshalAs(UnmanagedType.FunctionPtr)]
		public LuaFunction function;
	}
}

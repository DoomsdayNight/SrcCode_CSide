using System;
using System.Runtime.InteropServices;
using System.Security;

namespace KeraLua
{
	// Token: 0x02000080 RID: 128
	// (Invoke) Token: 0x0600046B RID: 1131
	[SuppressUnmanagedCodeSecurity]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int LuaFunction(IntPtr luaState);
}

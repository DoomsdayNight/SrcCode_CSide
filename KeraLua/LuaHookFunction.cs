using System;
using System.Runtime.InteropServices;
using System.Security;

namespace KeraLua
{
	// Token: 0x02000081 RID: 129
	// (Invoke) Token: 0x0600046F RID: 1135
	[SuppressUnmanagedCodeSecurity]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void LuaHookFunction(IntPtr luaState, IntPtr ar);
}

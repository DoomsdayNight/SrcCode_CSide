using System;
using System.Runtime.InteropServices;
using System.Security;

namespace KeraLua
{
	// Token: 0x02000082 RID: 130
	// (Invoke) Token: 0x06000473 RID: 1139
	[SuppressUnmanagedCodeSecurity]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int LuaKFunction(IntPtr L, int status, IntPtr ctx);
}

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace KeraLua
{
	// Token: 0x02000086 RID: 134
	// (Invoke) Token: 0x06000483 RID: 1155
	[SuppressUnmanagedCodeSecurity]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void LuaWarnFunction(IntPtr ud, IntPtr msg, int tocont);
}

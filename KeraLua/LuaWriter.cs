using System;
using System.Runtime.InteropServices;
using System.Security;

namespace KeraLua
{
	// Token: 0x02000084 RID: 132
	// (Invoke) Token: 0x0600047B RID: 1147
	[SuppressUnmanagedCodeSecurity]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate int LuaWriter(IntPtr L, IntPtr p, UIntPtr size, IntPtr ud);
}

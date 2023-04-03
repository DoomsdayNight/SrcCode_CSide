using System;
using System.Runtime.InteropServices;
using System.Security;

namespace KeraLua
{
	// Token: 0x02000083 RID: 131
	// (Invoke) Token: 0x06000477 RID: 1143
	[SuppressUnmanagedCodeSecurity]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate IntPtr LuaReader(IntPtr L, IntPtr ud, ref UIntPtr sz);
}

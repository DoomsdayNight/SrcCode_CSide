using System;
using System.Runtime.InteropServices;
using System.Security;

namespace KeraLua
{
	// Token: 0x02000085 RID: 133
	// (Invoke) Token: 0x0600047F RID: 1151
	[SuppressUnmanagedCodeSecurity]
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate IntPtr LuaAlloc(IntPtr ud, IntPtr ptr, UIntPtr osize, UIntPtr nsize);
}

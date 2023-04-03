using System;
using System.Runtime.InteropServices;

namespace KeraLua
{
	// Token: 0x0200007F RID: 127
	internal static class DelegateExtensions
	{
		// Token: 0x0600045C RID: 1116 RVA: 0x00014D04 File Offset: 0x00012F04
		public static LuaFunction ToLuaFunction(this IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.GetDelegateForFunctionPointer<LuaFunction>(ptr);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00014D1B File Offset: 0x00012F1B
		public static IntPtr ToFunctionPointer(this LuaFunction d)
		{
			if (d == null)
			{
				return IntPtr.Zero;
			}
			return Marshal.GetFunctionPointerForDelegate<LuaFunction>(d);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00014D2C File Offset: 0x00012F2C
		public static LuaHookFunction ToLuaHookFunction(this IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.GetDelegateForFunctionPointer<LuaHookFunction>(ptr);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00014D43 File Offset: 0x00012F43
		public static IntPtr ToFunctionPointer(this LuaHookFunction d)
		{
			if (d == null)
			{
				return IntPtr.Zero;
			}
			return Marshal.GetFunctionPointerForDelegate<LuaHookFunction>(d);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00014D54 File Offset: 0x00012F54
		public static LuaKFunction ToLuaKFunction(this IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.GetDelegateForFunctionPointer<LuaKFunction>(ptr);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00014D6B File Offset: 0x00012F6B
		public static IntPtr ToFunctionPointer(this LuaKFunction d)
		{
			if (d == null)
			{
				return IntPtr.Zero;
			}
			return Marshal.GetFunctionPointerForDelegate<LuaKFunction>(d);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00014D7C File Offset: 0x00012F7C
		public static LuaReader ToLuaReader(this IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.GetDelegateForFunctionPointer<LuaReader>(ptr);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00014D93 File Offset: 0x00012F93
		public static IntPtr ToFunctionPointer(this LuaReader d)
		{
			if (d == null)
			{
				return IntPtr.Zero;
			}
			return Marshal.GetFunctionPointerForDelegate<LuaReader>(d);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00014DA4 File Offset: 0x00012FA4
		public static LuaWriter ToLuaWriter(this IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.GetDelegateForFunctionPointer<LuaWriter>(ptr);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00014DBB File Offset: 0x00012FBB
		public static IntPtr ToFunctionPointer(this LuaWriter d)
		{
			if (d == null)
			{
				return IntPtr.Zero;
			}
			return Marshal.GetFunctionPointerForDelegate<LuaWriter>(d);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00014DCC File Offset: 0x00012FCC
		public static LuaAlloc ToLuaAlloc(this IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.GetDelegateForFunctionPointer<LuaAlloc>(ptr);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00014DE3 File Offset: 0x00012FE3
		public static IntPtr ToFunctionPointer(this LuaAlloc d)
		{
			if (d == null)
			{
				return IntPtr.Zero;
			}
			return Marshal.GetFunctionPointerForDelegate<LuaAlloc>(d);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00014DF4 File Offset: 0x00012FF4
		public static LuaWarnFunction ToLuaWarning(this IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.GetDelegateForFunctionPointer<LuaWarnFunction>(ptr);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00014E0B File Offset: 0x0001300B
		public static IntPtr ToFunctionPointer(this LuaWarnFunction d)
		{
			if (d == null)
			{
				return IntPtr.Zero;
			}
			return Marshal.GetFunctionPointerForDelegate<LuaWarnFunction>(d);
		}
	}
}

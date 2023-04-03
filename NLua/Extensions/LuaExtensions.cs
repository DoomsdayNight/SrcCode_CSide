using System;
using System.Runtime.InteropServices;
using KeraLua;

namespace NLua.Extensions
{
	// Token: 0x02000078 RID: 120
	internal static class LuaExtensions
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x00014782 File Offset: 0x00012982
		public static bool CheckMetaTable(this Lua state, int index, IntPtr tag)
		{
			if (!state.GetMetaTable(index))
			{
				return false;
			}
			state.PushLightUserData(tag);
			state.RawGet(-2);
			bool result = !state.IsNil(-1);
			state.SetTop(-3);
			return result;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000147B1 File Offset: 0x000129B1
		public static void PopGlobalTable(this Lua luaState)
		{
			luaState.RawSetInteger(LuaRegistry.Index, 2L);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000147C0 File Offset: 0x000129C0
		public static void GetRef(this Lua luaState, int reference)
		{
			luaState.RawGetInteger(LuaRegistry.Index, (long)reference);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000147D0 File Offset: 0x000129D0
		public static void Unref(this Lua luaState, int reference)
		{
			luaState.Unref(LuaRegistry.Index, reference);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000147DE File Offset: 0x000129DE
		public static bool AreEqual(this Lua luaState, int ref1, int ref2)
		{
			return luaState.Compare(ref1, ref2, LuaCompare.Equal);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x000147EC File Offset: 0x000129EC
		public static IntPtr CheckUData(this Lua state, int ud, string name)
		{
			IntPtr intPtr = state.ToUserData(ud);
			if (intPtr == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			if (!state.GetMetaTable(ud))
			{
				return IntPtr.Zero;
			}
			state.GetField(LuaRegistry.Index, name);
			bool flag = state.RawEqual(-1, -2);
			state.Pop(2);
			if (flag)
			{
				return intPtr;
			}
			return IntPtr.Zero;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001484C File Offset: 0x00012A4C
		public static int ToNetObject(this Lua state, int index, IntPtr tag)
		{
			if (state.Type(index) != LuaType.UserData)
			{
				return -1;
			}
			IntPtr intPtr;
			if (state.CheckMetaTable(index, tag))
			{
				intPtr = state.ToUserData(index);
				if (intPtr != IntPtr.Zero)
				{
					return Marshal.ReadInt32(intPtr);
				}
			}
			intPtr = state.CheckUData(index, "luaNet_class");
			if (intPtr != IntPtr.Zero)
			{
				return Marshal.ReadInt32(intPtr);
			}
			intPtr = state.CheckUData(index, "luaNet_searchbase");
			if (intPtr != IntPtr.Zero)
			{
				return Marshal.ReadInt32(intPtr);
			}
			intPtr = state.CheckUData(index, "luaNet_function");
			if (intPtr != IntPtr.Zero)
			{
				return Marshal.ReadInt32(intPtr);
			}
			return -1;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000148EF File Offset: 0x00012AEF
		public static void NewUData(this Lua state, int val)
		{
			Marshal.WriteInt32(state.NewUserData(Marshal.SizeOf(typeof(int))), val);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001490C File Offset: 0x00012B0C
		public static int RawNetObj(this Lua state, int index)
		{
			IntPtr intPtr = state.ToUserData(index);
			if (intPtr == IntPtr.Zero)
			{
				return -1;
			}
			return Marshal.ReadInt32(intPtr);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00014938 File Offset: 0x00012B38
		public static int CheckUObject(this Lua state, int index, string name)
		{
			IntPtr intPtr = state.CheckUData(index, name);
			if (intPtr == IntPtr.Zero)
			{
				return -1;
			}
			return Marshal.ReadInt32(intPtr);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00014963 File Offset: 0x00012B63
		public static bool IsNumericType(this Lua state, int index)
		{
			return state.Type(index) == LuaType.Number;
		}
	}
}

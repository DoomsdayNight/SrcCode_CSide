using System;

namespace NLua.Method
{
	// Token: 0x02000070 RID: 112
	public class LuaClassHelper
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x00013DD0 File Offset: 0x00011FD0
		public static LuaFunction GetTableFunction(LuaTable luaTable, string name)
		{
			if (luaTable == null)
			{
				return null;
			}
			LuaFunction luaFunction = luaTable.RawGet(name) as LuaFunction;
			if (luaFunction != null)
			{
				return luaFunction;
			}
			return null;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00013DF8 File Offset: 0x00011FF8
		public static object CallFunction(LuaFunction function, object[] args, Type[] returnTypes, object[] inArgs, int[] outArgs)
		{
			object[] array = function.Call(inArgs, returnTypes);
			if (array == null || returnTypes.Length == 0)
			{
				return null;
			}
			object result;
			int num;
			if (returnTypes[0] == typeof(void))
			{
				result = null;
				num = 0;
			}
			else
			{
				result = array[0];
				num = 1;
			}
			for (int i = 0; i < outArgs.Length; i++)
			{
				args[outArgs[i]] = array[num];
				num++;
			}
			return result;
		}
	}
}

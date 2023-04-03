using System;
using System.Reflection;

namespace NLua
{
	// Token: 0x02000067 RID: 103
	public static class LuaRegistrationHelper
	{
		// Token: 0x06000365 RID: 869 RVA: 0x0000FE7C File Offset: 0x0000E07C
		public static void TaggedInstanceMethods(Lua lua, object o)
		{
			if (lua == null)
			{
				throw new ArgumentNullException("lua");
			}
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			foreach (MethodInfo methodInfo in o.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
			{
				foreach (LuaGlobalAttribute luaGlobalAttribute in methodInfo.GetCustomAttributes(typeof(LuaGlobalAttribute), true))
				{
					if (string.IsNullOrEmpty(luaGlobalAttribute.Name))
					{
						lua.RegisterFunction(methodInfo.Name, o, methodInfo);
					}
					else
					{
						lua.RegisterFunction(luaGlobalAttribute.Name, o, methodInfo);
					}
				}
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000FF24 File Offset: 0x0000E124
		public static void TaggedStaticMethods(Lua lua, Type type)
		{
			if (lua == null)
			{
				throw new ArgumentNullException("lua");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsClass)
			{
				throw new ArgumentException("The type must be a class!", "type");
			}
			foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.Public))
			{
				foreach (LuaGlobalAttribute luaGlobalAttribute in methodInfo.GetCustomAttributes(typeof(LuaGlobalAttribute), false))
				{
					if (string.IsNullOrEmpty(luaGlobalAttribute.Name))
					{
						lua.RegisterFunction(methodInfo.Name, null, methodInfo);
					}
					else
					{
						lua.RegisterFunction(luaGlobalAttribute.Name, null, methodInfo);
					}
				}
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000FFE4 File Offset: 0x0000E1E4
		public static void Enumeration<T>(Lua lua)
		{
			if (lua == null)
			{
				throw new ArgumentNullException("lua");
			}
			Type typeFromHandle = typeof(T);
			if (!typeFromHandle.IsEnum)
			{
				throw new ArgumentException("The type must be an enumeration!");
			}
			string[] names = Enum.GetNames(typeFromHandle);
			T[] array = (T[])Enum.GetValues(typeFromHandle);
			lua.NewTable(typeFromHandle.Name);
			for (int i = 0; i < names.Length; i++)
			{
				string fullPath = typeFromHandle.Name + "." + names[i];
				lua.SetObjectToPath(fullPath, array[i]);
			}
		}
	}
}

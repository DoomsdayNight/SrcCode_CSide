using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using AOT;
using KeraLua;
using NLua.Exceptions;
using NLua.Extensions;
using NLua.Method;

namespace NLua
{
	// Token: 0x0200006C RID: 108
	public class ObjectTranslator
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0001248C File Offset: 0x0001068C
		public MetaFunctions MetaFunctionsInstance
		{
			get
			{
				return this.metaFunctions;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00012494 File Offset: 0x00010694
		public Lua Interpreter
		{
			get
			{
				return this.interpreter;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0001249C File Offset: 0x0001069C
		public IntPtr Tag
		{
			get
			{
				return this._tagPtr;
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000124A4 File Offset: 0x000106A4
		public ObjectTranslator(Lua interpreter, Lua luaState)
		{
			this._tagPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
			this.interpreter = interpreter;
			this.typeChecker = new CheckType(this);
			this.metaFunctions = new MetaFunctions(this);
			this.assemblies = new List<Assembly>();
			this.CreateLuaObjectList(luaState);
			this.CreateIndexingMetaFunction(luaState);
			this.CreateBaseClassMetatable(luaState);
			this.CreateClassMetatable(luaState);
			this.CreateFunctionMetatable(luaState);
			this.SetGlobalFunctions(luaState);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00012558 File Offset: 0x00010758
		private void CreateLuaObjectList(Lua luaState)
		{
			luaState.PushString("luaNet_objects");
			luaState.NewTable();
			luaState.NewTable();
			luaState.PushString("__mode");
			luaState.PushString("v");
			luaState.SetTable(-3);
			luaState.SetMetaTable(-2);
			luaState.SetTable(-1001000);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000125AD File Offset: 0x000107AD
		private void CreateIndexingMetaFunction(Lua luaState)
		{
			luaState.PushString("luaNet_indexfunction");
			luaState.DoString("local a={}local function b(c,d)local e=getmetatable(c)local f=e.cache[d]if f~=nil then if f==a then return nil end;return f else local g,h=get_object_member(c,d)if h then if g==nil then e.cache[d]=a else e.cache[d]=g end end;return g end end;return b");
			luaState.RawSet(LuaRegistry.Index);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000125D4 File Offset: 0x000107D4
		private void CreateBaseClassMetatable(Lua luaState)
		{
			luaState.NewMetaTable("luaNet_searchbase");
			luaState.PushString("__gc");
			luaState.PushCFunction(MetaFunctions.GcFunction);
			luaState.SetTable(-3);
			luaState.PushString("__tostring");
			luaState.PushCFunction(MetaFunctions.ToStringFunction);
			luaState.SetTable(-3);
			luaState.PushString("__index");
			luaState.PushCFunction(MetaFunctions.BaseIndexFunction);
			luaState.SetTable(-3);
			luaState.PushString("__newindex");
			luaState.PushCFunction(MetaFunctions.NewIndexFunction);
			luaState.SetTable(-3);
			luaState.SetTop(-2);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00012670 File Offset: 0x00010870
		private void CreateClassMetatable(Lua luaState)
		{
			luaState.NewMetaTable("luaNet_class");
			luaState.PushString("__gc");
			luaState.PushCFunction(MetaFunctions.GcFunction);
			luaState.SetTable(-3);
			luaState.PushString("__tostring");
			luaState.PushCFunction(MetaFunctions.ToStringFunction);
			luaState.SetTable(-3);
			luaState.PushString("__index");
			luaState.PushCFunction(MetaFunctions.ClassIndexFunction);
			luaState.SetTable(-3);
			luaState.PushString("__newindex");
			luaState.PushCFunction(MetaFunctions.ClassNewIndexFunction);
			luaState.SetTable(-3);
			luaState.PushString("__call");
			luaState.PushCFunction(MetaFunctions.CallConstructorFunction);
			luaState.SetTable(-3);
			luaState.SetTop(-2);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00012728 File Offset: 0x00010928
		private void SetGlobalFunctions(Lua luaState)
		{
			luaState.PushCFunction(MetaFunctions.IndexFunction);
			luaState.SetGlobal("get_object_member");
			luaState.PushCFunction(ObjectTranslator._importTypeFunction);
			luaState.SetGlobal("import_type");
			luaState.PushCFunction(ObjectTranslator._loadAssemblyFunction);
			luaState.SetGlobal("load_assembly");
			luaState.PushCFunction(ObjectTranslator._registerTableFunction);
			luaState.SetGlobal("make_object");
			luaState.PushCFunction(ObjectTranslator._unregisterTableFunction);
			luaState.SetGlobal("free_object");
			luaState.PushCFunction(ObjectTranslator._getMethodSigFunction);
			luaState.SetGlobal("get_method_bysig");
			luaState.PushCFunction(ObjectTranslator._getConstructorSigFunction);
			luaState.SetGlobal("get_constructor_bysig");
			luaState.PushCFunction(ObjectTranslator._ctypeFunction);
			luaState.SetGlobal("ctype");
			luaState.PushCFunction(ObjectTranslator._enumFromIntFunction);
			luaState.SetGlobal("enum");
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000127FC File Offset: 0x000109FC
		private void CreateFunctionMetatable(Lua luaState)
		{
			luaState.NewMetaTable("luaNet_function");
			luaState.PushString("__gc");
			luaState.PushCFunction(MetaFunctions.GcFunction);
			luaState.SetTable(-3);
			luaState.PushString("__call");
			luaState.PushCFunction(MetaFunctions.ExecuteDelegateFunction);
			luaState.SetTable(-3);
			luaState.SetTop(-2);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001285C File Offset: 0x00010A5C
		internal void ThrowError(Lua luaState, object e)
		{
			int top = luaState.GetTop();
			luaState.Where(1);
			object[] array = this.PopValues(luaState, top);
			string source = string.Empty;
			if (array.Length != 0)
			{
				source = array[0].ToString();
			}
			string text = e as string;
			if (text != null)
			{
				if (this.interpreter.UseTraceback)
				{
					text = text + Environment.NewLine + this.interpreter.GetDebugTraceback();
				}
				e = new LuaScriptException(text, source);
			}
			else
			{
				Exception ex = e as Exception;
				if (ex != null)
				{
					if (this.interpreter.UseTraceback)
					{
						ex.Data["Traceback"] = this.interpreter.GetDebugTraceback();
					}
					e = new LuaScriptException(ex, source);
				}
			}
			this.Push(luaState, e);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00012914 File Offset: 0x00010B14
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int LoadAssembly(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = objectTranslator.LoadAssemblyInternal(lua);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00012954 File Offset: 0x00010B54
		private int LoadAssemblyInternal(Lua luaState)
		{
			try
			{
				string text = luaState.ToString(1, false);
				Assembly assembly = null;
				Exception ex = null;
				try
				{
					assembly = Assembly.Load(text);
				}
				catch (BadImageFormatException)
				{
				}
				catch (FileNotFoundException ex)
				{
				}
				if (assembly == null)
				{
					try
					{
						assembly = Assembly.Load(AssemblyName.GetAssemblyName(text));
					}
					catch (FileNotFoundException ex)
					{
					}
					if (assembly == null)
					{
						AssemblyName name = this.assemblies[0].GetName();
						AssemblyName assemblyName = new AssemblyName();
						assemblyName.Name = text;
						assemblyName.CultureInfo = name.CultureInfo;
						assemblyName.Version = name.Version;
						assemblyName.SetPublicKeyToken(name.GetPublicKeyToken());
						assemblyName.SetPublicKey(name.GetPublicKey());
						assembly = Assembly.Load(assemblyName);
						if (assembly != null)
						{
							ex = null;
						}
					}
					if (ex != null)
					{
						this.ThrowError(luaState, ex);
						return 1;
					}
				}
				if (assembly != null && !this.assemblies.Contains(assembly))
				{
					this.assemblies.Add(assembly);
				}
			}
			catch (Exception e)
			{
				this.ThrowError(luaState, e);
				return 1;
			}
			return 0;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00012A80 File Offset: 0x00010C80
		internal Type FindType(string className)
		{
			foreach (Assembly assembly in this.assemblies)
			{
				Type type = assembly.GetType(className);
				if (type != null)
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00012AE4 File Offset: 0x00010CE4
		public bool TryGetExtensionMethod(Type type, string name, out MethodInfo method)
		{
			method = this.GetExtensionMethod(type, name);
			return method != null;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00012AF8 File Offset: 0x00010CF8
		public MethodInfo GetExtensionMethod(Type type, string name)
		{
			return type.GetExtensionMethod(name, this.assemblies);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00012B08 File Offset: 0x00010D08
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int ImportType(IntPtr luaState)
		{
			Lua luaState2 = Lua.FromIntPtr(luaState);
			return ObjectTranslatorPool.Instance.Find(luaState2).ImportTypeInternal(luaState2);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00012B30 File Offset: 0x00010D30
		private int ImportTypeInternal(Lua luaState)
		{
			string className = luaState.ToString(1, false);
			Type type = this.FindType(className);
			if (type != null)
			{
				this.PushType(luaState, type);
			}
			else
			{
				luaState.PushNil();
			}
			return 1;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00012B68 File Offset: 0x00010D68
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int RegisterTable(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = objectTranslator.RegisterTableInternal(lua);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00012BA8 File Offset: 0x00010DA8
		private int RegisterTableInternal(Lua luaState)
		{
			if (luaState.Type(1) != LuaType.Table)
			{
				this.ThrowError(luaState, "register_table: first arg is not a table");
				return 1;
			}
			LuaTable table = this.GetTable(luaState, 1);
			string text = luaState.ToString(2, false);
			if (string.IsNullOrEmpty(text))
			{
				this.ThrowError(luaState, "register_table: superclass name can not be null");
				return 1;
			}
			Type type = this.FindType(text);
			if (type == null)
			{
				this.ThrowError(luaState, "register_table: can not find superclass '" + text + "'");
				return 1;
			}
			object classInstance = CodeGeneration.Instance.GetClassInstance(type, table);
			this.PushObject(luaState, classInstance, "luaNet_metatable");
			luaState.NewTable();
			luaState.PushString("__index");
			luaState.PushCopy(-3);
			luaState.SetTable(-3);
			luaState.PushString("__newindex");
			luaState.PushCopy(-3);
			luaState.SetTable(-3);
			luaState.SetMetaTable(1);
			luaState.PushString("base");
			int index = this.AddObject(classInstance);
			this.PushNewObject(luaState, classInstance, index, "luaNet_searchbase");
			luaState.RawSet(1);
			return 0;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00012CA8 File Offset: 0x00010EA8
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int UnregisterTable(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = objectTranslator.UnregisterTableInternal(lua);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00012CE8 File Offset: 0x00010EE8
		private int UnregisterTableInternal(Lua luaState)
		{
			if (!luaState.GetMetaTable(1))
			{
				this.ThrowError(luaState, "unregister_table: arg is not valid table");
				return 1;
			}
			luaState.PushString("__index");
			luaState.GetTable(-2);
			object rawNetObject = this.GetRawNetObject(luaState, -1);
			if (rawNetObject == null)
			{
				this.ThrowError(luaState, "unregister_table: arg is not valid table");
				return 1;
			}
			FieldInfo field = rawNetObject.GetType().GetField("__luaInterface_luaTable");
			if (field == null)
			{
				this.ThrowError(luaState, "unregister_table: arg is not valid table");
				return 1;
			}
			field.SetValue(rawNetObject, null);
			luaState.PushNil();
			luaState.SetMetaTable(1);
			luaState.PushString("base");
			luaState.PushNil();
			luaState.SetTable(1);
			return 0;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00012D90 File Offset: 0x00010F90
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int GetMethodSignature(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int methodSignatureInternal = objectTranslator.GetMethodSignatureInternal(lua);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return methodSignatureInternal;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00012DD0 File Offset: 0x00010FD0
		private int GetMethodSignatureInternal(Lua luaState)
		{
			int num = luaState.CheckUObject(1, "luaNet_class");
			ProxyType proxyType;
			object obj;
			if (num != -1)
			{
				proxyType = (ProxyType)this._objects[num];
				obj = null;
			}
			else
			{
				obj = this.GetRawNetObject(luaState, 1);
				if (obj == null)
				{
					this.ThrowError(luaState, "get_method_bysig: first arg is not type or object reference");
					return 1;
				}
				proxyType = new ProxyType(obj.GetType());
			}
			string name = luaState.ToString(2, false);
			Type[] array = new Type[luaState.GetTop() - 2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.FindType(luaState.ToString(i + 3, false));
			}
			try
			{
				MethodInfo method = proxyType.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, array);
				LuaFunction invokeFunction = new LuaMethodWrapper(this, obj, proxyType, method).InvokeFunction;
				this.PushFunction(luaState, invokeFunction);
			}
			catch (Exception e)
			{
				this.ThrowError(luaState, e);
			}
			return 1;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00012EB4 File Offset: 0x000110B4
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int GetConstructorSignature(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int constructorSignatureInternal = objectTranslator.GetConstructorSignatureInternal(lua);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return constructorSignatureInternal;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00012EF4 File Offset: 0x000110F4
		private int GetConstructorSignatureInternal(Lua luaState)
		{
			ProxyType proxyType = null;
			int num = luaState.CheckUObject(1, "luaNet_class");
			if (num != -1)
			{
				proxyType = (ProxyType)this._objects[num];
			}
			if (proxyType == null)
			{
				this.ThrowError(luaState, "get_constructor_bysig: first arg is invalid type reference");
				return 1;
			}
			Type[] array = new Type[luaState.GetTop() - 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.FindType(luaState.ToString(i + 2, false));
			}
			try
			{
				ConstructorInfo constructor = proxyType.UnderlyingSystemType.GetConstructor(array);
				LuaFunction invokeFunction = new LuaMethodWrapper(this, null, proxyType, constructor).InvokeFunction;
				this.PushFunction(luaState, invokeFunction);
			}
			catch (Exception e)
			{
				this.ThrowError(luaState, e);
			}
			return 1;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00012FB0 File Offset: 0x000111B0
		internal void PushType(Lua luaState, Type t)
		{
			this.PushObject(luaState, new ProxyType(t), "luaNet_class");
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00012FC4 File Offset: 0x000111C4
		internal void PushFunction(Lua luaState, LuaFunction func)
		{
			this.PushObject(luaState, func, "luaNet_function");
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00012FD4 File Offset: 0x000111D4
		internal void PushObject(Lua luaState, object o, string metatable)
		{
			int num = -1;
			if (o == null)
			{
				luaState.PushNil();
				return;
			}
			if ((!o.GetType().IsValueType || o.GetType().IsEnum) && this._objectsBackMap.TryGetValue(o, out num))
			{
				luaState.GetMetaTable("luaNet_objects");
				luaState.RawGetInteger(-1, (long)num);
				if (luaState.Type(-1) != LuaType.Nil)
				{
					luaState.Remove(-2);
					return;
				}
				luaState.Remove(-1);
				luaState.Remove(-1);
				this.CollectObject(o, num);
			}
			num = this.AddObject(o);
			this.PushNewObject(luaState, o, num, metatable);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001306C File Offset: 0x0001126C
		private void PushNewObject(Lua luaState, object o, int index, string metatable)
		{
			if (metatable == "luaNet_metatable")
			{
				luaState.GetMetaTable(o.GetType().AssemblyQualifiedName);
				if (luaState.IsNil(-1))
				{
					luaState.SetTop(-2);
					luaState.NewMetaTable(o.GetType().AssemblyQualifiedName);
					luaState.PushString("cache");
					luaState.NewTable();
					luaState.RawSet(-3);
					luaState.PushLightUserData(this._tagPtr);
					luaState.PushNumber(1.0);
					luaState.RawSet(-3);
					luaState.PushString("__index");
					luaState.PushString("luaNet_indexfunction");
					luaState.RawGet(LuaRegistry.Index);
					luaState.RawSet(-3);
					luaState.PushString("__gc");
					luaState.PushCFunction(MetaFunctions.GcFunction);
					luaState.RawSet(-3);
					luaState.PushString("__tostring");
					luaState.PushCFunction(MetaFunctions.ToStringFunction);
					luaState.RawSet(-3);
					luaState.PushString("__newindex");
					luaState.PushCFunction(MetaFunctions.NewIndexFunction);
					luaState.RawSet(-3);
					this.RegisterOperatorsFunctions(luaState, o.GetType());
					this.RegisterCallMethodForDelegate(luaState, o);
				}
			}
			else
			{
				luaState.GetMetaTable(metatable);
			}
			luaState.GetMetaTable("luaNet_objects");
			luaState.NewUData(index);
			luaState.PushCopy(-3);
			luaState.Remove(-4);
			luaState.SetMetaTable(-2);
			luaState.PushCopy(-1);
			luaState.RawSetInteger(-3, (long)index);
			luaState.Remove(-2);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000131E6 File Offset: 0x000113E6
		private void RegisterCallMethodForDelegate(Lua luaState, object o)
		{
			if (!(o is Delegate))
			{
				return;
			}
			luaState.PushString("__call");
			luaState.PushCFunction(MetaFunctions.CallDelegateFunction);
			luaState.RawSet(-3);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00013210 File Offset: 0x00011410
		private void RegisterOperatorsFunctions(Lua luaState, Type type)
		{
			if (type.HasAdditionOperator())
			{
				luaState.PushString("__add");
				luaState.PushCFunction(MetaFunctions.AddFunction);
				luaState.RawSet(-3);
			}
			if (type.HasSubtractionOperator())
			{
				luaState.PushString("__sub");
				luaState.PushCFunction(MetaFunctions.SubtractFunction);
				luaState.RawSet(-3);
			}
			if (type.HasMultiplyOperator())
			{
				luaState.PushString("__mul");
				luaState.PushCFunction(MetaFunctions.MultiplyFunction);
				luaState.RawSet(-3);
			}
			if (type.HasDivisionOperator())
			{
				luaState.PushString("__div");
				luaState.PushCFunction(MetaFunctions.DivisionFunction);
				luaState.RawSet(-3);
			}
			if (type.HasModulusOperator())
			{
				luaState.PushString("__mod");
				luaState.PushCFunction(MetaFunctions.ModulosFunction);
				luaState.RawSet(-3);
			}
			if (type.HasUnaryNegationOperator())
			{
				luaState.PushString("__unm");
				luaState.PushCFunction(MetaFunctions.UnaryNegationFunction);
				luaState.RawSet(-3);
			}
			if (type.HasEqualityOperator())
			{
				luaState.PushString("__eq");
				luaState.PushCFunction(MetaFunctions.EqualFunction);
				luaState.RawSet(-3);
			}
			if (type.HasLessThanOperator())
			{
				luaState.PushString("__lt");
				luaState.PushCFunction(MetaFunctions.LessThanFunction);
				luaState.RawSet(-3);
			}
			if (type.HasLessThanOrEqualOperator())
			{
				luaState.PushString("__le");
				luaState.PushCFunction(MetaFunctions.LessThanOrEqualFunction);
				luaState.RawSet(-3);
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00013374 File Offset: 0x00011574
		internal object GetAsType(Lua luaState, int stackPos, Type paramType)
		{
			ExtractValue extractValue = this.typeChecker.CheckLuaType(luaState, stackPos, paramType);
			if (extractValue == null)
			{
				return null;
			}
			return extractValue(luaState, stackPos);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000133A0 File Offset: 0x000115A0
		internal void CollectObject(int udata)
		{
			object o;
			if (this._objects.TryGetValue(udata, out o))
			{
				this.CollectObject(o, udata);
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000133C5 File Offset: 0x000115C5
		private void CollectObject(object o, int udata)
		{
			this._objects.Remove(udata);
			if (!o.GetType().IsValueType || o.GetType().IsEnum)
			{
				this._objectsBackMap.Remove(o);
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000133FC File Offset: 0x000115FC
		private int AddObject(object obj)
		{
			int nextObj = this._nextObj;
			this._nextObj = nextObj + 1;
			int num = nextObj;
			this._objects[num] = obj;
			if (!obj.GetType().IsValueType || obj.GetType().IsEnum)
			{
				this._objectsBackMap[obj] = num;
			}
			return num;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00013450 File Offset: 0x00011650
		internal object GetObject(Lua luaState, int index)
		{
			switch (luaState.Type(index))
			{
			case LuaType.Boolean:
				return luaState.ToBoolean(index);
			case LuaType.Number:
				if (luaState.IsInteger(index))
				{
					return luaState.ToInteger(index);
				}
				return luaState.ToNumber(index);
			case LuaType.String:
				return luaState.ToString(index, false);
			case LuaType.Table:
				return this.GetTable(luaState, index);
			case LuaType.Function:
				return this.GetFunction(luaState, index);
			case LuaType.UserData:
			{
				int num = luaState.ToNetObject(index, this.Tag);
				if (num == -1)
				{
					return this.GetUserData(luaState, index);
				}
				return this._objects[num];
			}
			case LuaType.Thread:
				return this.GetThread(luaState, index);
			}
			return null;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001350C File Offset: 0x0001170C
		internal LuaTable GetTable(Lua luaState, int index)
		{
			this.CleanFinalizedReferences(luaState);
			luaState.PushCopy(index);
			int num = luaState.Ref(LuaRegistry.Index);
			if (num == -1)
			{
				return null;
			}
			return new LuaTable(num, this.interpreter);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00013548 File Offset: 0x00011748
		internal LuaThread GetThread(Lua luaState, int index)
		{
			this.CleanFinalizedReferences(luaState);
			luaState.PushCopy(index);
			int num = luaState.Ref(LuaRegistry.Index);
			if (num == -1)
			{
				return null;
			}
			return new LuaThread(num, this.interpreter);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00013584 File Offset: 0x00011784
		internal LuaUserData GetUserData(Lua luaState, int index)
		{
			this.CleanFinalizedReferences(luaState);
			luaState.PushCopy(index);
			int num = luaState.Ref(LuaRegistry.Index);
			if (num == -1)
			{
				return null;
			}
			return new LuaUserData(num, this.interpreter);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000135C0 File Offset: 0x000117C0
		internal LuaFunction GetFunction(Lua luaState, int index)
		{
			this.CleanFinalizedReferences(luaState);
			luaState.PushCopy(index);
			int num = luaState.Ref(LuaRegistry.Index);
			if (num == -1)
			{
				return null;
			}
			return new LuaFunction(num, this.interpreter);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x000135FC File Offset: 0x000117FC
		internal object GetNetObject(Lua luaState, int index)
		{
			int num = luaState.ToNetObject(index, this.Tag);
			if (num == -1)
			{
				return null;
			}
			return this._objects[num];
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001362C File Offset: 0x0001182C
		internal object GetRawNetObject(Lua luaState, int index)
		{
			int num = luaState.RawNetObj(index);
			if (num == -1)
			{
				return null;
			}
			return this._objects[num];
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00013654 File Offset: 0x00011854
		internal object[] PopValues(Lua luaState, int oldTop)
		{
			int top = luaState.GetTop();
			if (oldTop == top)
			{
				return new object[0];
			}
			List<object> list = new List<object>();
			for (int i = oldTop + 1; i <= top; i++)
			{
				list.Add(this.GetObject(luaState, i));
			}
			luaState.SetTop(oldTop);
			return list.ToArray();
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000136A4 File Offset: 0x000118A4
		internal object[] PopValues(Lua luaState, int oldTop, Type[] popTypes)
		{
			int top = luaState.GetTop();
			if (oldTop == top)
			{
				return new object[0];
			}
			List<object> list = new List<object>();
			int num;
			if (popTypes[0] == typeof(void))
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			for (int i = oldTop + 1; i <= top; i++)
			{
				list.Add(this.GetAsType(luaState, i, popTypes[num]));
				num++;
			}
			luaState.SetTop(oldTop);
			return list.ToArray();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00013713 File Offset: 0x00011913
		private static bool IsILua(object o)
		{
			return o is ILuaGeneratedType && o.GetType().GetInterface("ILuaGeneratedType", true) != null;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00013738 File Offset: 0x00011938
		internal void Push(Lua luaState, object o)
		{
			if (o == null)
			{
				luaState.PushNil();
				return;
			}
			if (o is sbyte)
			{
				sbyte b = (sbyte)o;
				luaState.PushInteger((long)b);
				return;
			}
			if (o is byte)
			{
				byte b2 = (byte)o;
				luaState.PushInteger((long)((ulong)b2));
				return;
			}
			if (o is short)
			{
				short num = (short)o;
				luaState.PushInteger((long)num);
				return;
			}
			if (o is ushort)
			{
				ushort num2 = (ushort)o;
				luaState.PushInteger((long)((ulong)num2));
				return;
			}
			if (o is int)
			{
				int num3 = (int)o;
				luaState.PushInteger((long)num3);
				return;
			}
			if (o is uint)
			{
				uint num4 = (uint)o;
				luaState.PushInteger((long)((ulong)num4));
				return;
			}
			if (o is long)
			{
				long n = (long)o;
				luaState.PushInteger(n);
				return;
			}
			if (o is ulong)
			{
				ulong n2 = (ulong)o;
				luaState.PushInteger((long)n2);
				return;
			}
			if (o is char)
			{
				char c = (char)o;
				luaState.PushInteger((long)((ulong)c));
				return;
			}
			if (o is float)
			{
				float num5 = (float)o;
				luaState.PushNumber((double)num5);
				return;
			}
			if (o is decimal)
			{
				decimal value = (decimal)o;
				luaState.PushNumber((double)value);
				return;
			}
			if (o is double)
			{
				double number = (double)o;
				luaState.PushNumber(number);
				return;
			}
			string text = o as string;
			if (text != null)
			{
				luaState.PushString(text);
				return;
			}
			if (o is bool)
			{
				bool b3 = (bool)o;
				luaState.PushBoolean(b3);
				return;
			}
			if (ObjectTranslator.IsILua(o))
			{
				((ILuaGeneratedType)o).LuaInterfaceGetLuaTable().Push(luaState);
				return;
			}
			LuaTable luaTable = o as LuaTable;
			if (luaTable != null)
			{
				luaTable.Push(luaState);
				return;
			}
			LuaThread luaThread = o as LuaThread;
			if (luaThread != null)
			{
				luaThread.Push(luaState);
				return;
			}
			LuaFunction luaFunction = o as LuaFunction;
			if (luaFunction != null)
			{
				this.PushFunction(luaState, luaFunction);
				return;
			}
			LuaFunction luaFunction2 = o as LuaFunction;
			if (luaFunction2 != null)
			{
				luaFunction2.Push(luaState);
				return;
			}
			LuaUserData luaUserData = o as LuaUserData;
			if (luaUserData != null)
			{
				luaUserData.Push(luaState);
				return;
			}
			this.PushObject(luaState, o, "luaNet_metatable");
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00013940 File Offset: 0x00011B40
		internal bool MatchParameters(Lua luaState, MethodBase method, MethodCache methodCache, int skipParam)
		{
			return this.metaFunctions.MatchParameters(luaState, method, methodCache, skipParam);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00013952 File Offset: 0x00011B52
		internal Array TableToArray(Lua luaState, ExtractValue extractValue, Type paramArrayType, int startIndex, int count)
		{
			return this.metaFunctions.TableToArray(luaState, extractValue, paramArrayType, ref startIndex, count);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00013968 File Offset: 0x00011B68
		private Type TypeOf(Lua luaState, int idx)
		{
			int num = luaState.CheckUObject(idx, "luaNet_class");
			if (num == -1)
			{
				return null;
			}
			return ((ProxyType)this._objects[num]).UnderlyingSystemType;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001399E File Offset: 0x00011B9E
		private static int PushError(Lua luaState, string msg)
		{
			luaState.PushNil();
			luaState.PushString(msg);
			return 2;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000139B0 File Offset: 0x00011BB0
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int CType(IntPtr luaState)
		{
			Lua luaState2 = Lua.FromIntPtr(luaState);
			return ObjectTranslatorPool.Instance.Find(luaState2).CTypeInternal(luaState2);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000139D8 File Offset: 0x00011BD8
		private int CTypeInternal(Lua luaState)
		{
			Type type = this.TypeOf(luaState, 1);
			if (type == null)
			{
				return ObjectTranslator.PushError(luaState, "Not a CLR Class");
			}
			this.PushObject(luaState, type, "luaNet_metatable");
			return 1;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00013A14 File Offset: 0x00011C14
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int EnumFromInt(IntPtr luaState)
		{
			Lua luaState2 = Lua.FromIntPtr(luaState);
			return ObjectTranslatorPool.Instance.Find(luaState2).EnumFromIntInternal(luaState2);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00013A3C File Offset: 0x00011C3C
		private int EnumFromIntInternal(Lua luaState)
		{
			Type type = this.TypeOf(luaState, 1);
			if (type == null || !type.IsEnum)
			{
				return ObjectTranslator.PushError(luaState, "Not an Enum.");
			}
			object o = null;
			LuaType luaType = luaState.Type(2);
			if (luaType == LuaType.Number)
			{
				int value = (int)luaState.ToNumber(2);
				o = Enum.ToObject(type, value);
			}
			else
			{
				if (luaType != LuaType.String)
				{
					return ObjectTranslator.PushError(luaState, "Second argument must be a integer or a string.");
				}
				string value2 = luaState.ToString(2, false);
				string text = null;
				try
				{
					o = Enum.Parse(type, value2, true);
				}
				catch (ArgumentException ex)
				{
					text = ex.Message;
				}
				if (text != null)
				{
					return ObjectTranslator.PushError(luaState, text);
				}
			}
			this.PushObject(luaState, o, "luaNet_metatable");
			return 1;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00013AF0 File Offset: 0x00011CF0
		internal void AddFinalizedReference(int reference)
		{
			this.finalizedReferences.Enqueue(reference);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00013B00 File Offset: 0x00011D00
		private void CleanFinalizedReferences(Lua state)
		{
			if (this.finalizedReferences.Count == 0)
			{
				return;
			}
			int reference;
			while (this.finalizedReferences.TryDequeue(out reference))
			{
				state.Unref(LuaRegistry.Index, reference);
			}
		}

		// Token: 0x0400022F RID: 559
		private static readonly LuaFunction _registerTableFunction = new LuaFunction(ObjectTranslator.RegisterTable);

		// Token: 0x04000230 RID: 560
		private static readonly LuaFunction _unregisterTableFunction = new LuaFunction(ObjectTranslator.UnregisterTable);

		// Token: 0x04000231 RID: 561
		private static readonly LuaFunction _getMethodSigFunction = new LuaFunction(ObjectTranslator.GetMethodSignature);

		// Token: 0x04000232 RID: 562
		private static readonly LuaFunction _getConstructorSigFunction = new LuaFunction(ObjectTranslator.GetConstructorSignature);

		// Token: 0x04000233 RID: 563
		private static readonly LuaFunction _importTypeFunction = new LuaFunction(ObjectTranslator.ImportType);

		// Token: 0x04000234 RID: 564
		private static readonly LuaFunction _loadAssemblyFunction = new LuaFunction(ObjectTranslator.LoadAssembly);

		// Token: 0x04000235 RID: 565
		private static readonly LuaFunction _ctypeFunction = new LuaFunction(ObjectTranslator.CType);

		// Token: 0x04000236 RID: 566
		private static readonly LuaFunction _enumFromIntFunction = new LuaFunction(ObjectTranslator.EnumFromInt);

		// Token: 0x04000237 RID: 567
		private readonly Dictionary<object, int> _objectsBackMap = new Dictionary<object, int>(new ObjectTranslator.ReferenceComparer());

		// Token: 0x04000238 RID: 568
		private readonly Dictionary<int, object> _objects = new Dictionary<int, object>();

		// Token: 0x04000239 RID: 569
		private readonly ConcurrentQueue<int> finalizedReferences = new ConcurrentQueue<int>();

		// Token: 0x0400023A RID: 570
		internal EventHandlerContainer PendingEvents = new EventHandlerContainer();

		// Token: 0x0400023B RID: 571
		private MetaFunctions metaFunctions;

		// Token: 0x0400023C RID: 572
		private List<Assembly> assemblies;

		// Token: 0x0400023D RID: 573
		internal CheckType typeChecker;

		// Token: 0x0400023E RID: 574
		internal Lua interpreter;

		// Token: 0x0400023F RID: 575
		private int _nextObj;

		// Token: 0x04000240 RID: 576
		private readonly IntPtr _tagPtr;

		// Token: 0x02001106 RID: 4358
		private class ReferenceComparer : IEqualityComparer<object>
		{
			// Token: 0x06009EF6 RID: 40694 RVA: 0x0033BCFC File Offset: 0x00339EFC
			public bool Equals(object x, object y)
			{
				if (x != null && y != null && x.GetType() == y.GetType() && x.GetType().IsValueType && y.GetType().IsValueType)
				{
					return x.Equals(y);
				}
				return x == y;
			}

			// Token: 0x06009EF7 RID: 40695 RVA: 0x0033BD48 File Offset: 0x00339F48
			public int GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}

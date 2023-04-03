using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using AOT;
using KeraLua;
using NLua.Event;
using NLua.Exceptions;
using NLua.Extensions;
using NLua.Method;

namespace NLua
{
	// Token: 0x02000062 RID: 98
	public class Lua : IDisposable
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060002F7 RID: 759 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		// (remove) Token: 0x060002F8 RID: 760 RVA: 0x0000E334 File Offset: 0x0000C534
		public event EventHandler<HookExceptionEventArgs> HookException;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060002F9 RID: 761 RVA: 0x0000E36C File Offset: 0x0000C56C
		// (remove) Token: 0x060002FA RID: 762 RVA: 0x0000E3A4 File Offset: 0x0000C5A4
		public event EventHandler<DebugHookEventArgs> DebugHook;

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000E3D9 File Offset: 0x0000C5D9
		public bool IsExecuting
		{
			get
			{
				return this._executing;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000E3E1 File Offset: 0x0000C5E1
		public Lua State
		{
			get
			{
				return this._luaState;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000E3E9 File Offset: 0x0000C5E9
		internal ObjectTranslator Translator
		{
			get
			{
				return this._translator;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000E3F1 File Offset: 0x0000C5F1
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000E3F9 File Offset: 0x0000C5F9
		public bool UseTraceback { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000E402 File Offset: 0x0000C602
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000E40A File Offset: 0x0000C60A
		public int MaximumRecursion { get; set; } = 2;

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000E413 File Offset: 0x0000C613
		public IEnumerable<string> Globals
		{
			get
			{
				if (!this._globalsSorted)
				{
					this._globals.Sort();
					this._globalsSorted = true;
				}
				return this._globals;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000E438 File Offset: 0x0000C638
		public LuaThread Thread
		{
			get
			{
				int top = this._luaState.GetTop();
				this._luaState.PushThread();
				object @object = this._translator.GetObject(this._luaState, -1);
				this._luaState.SetTop(top);
				return (LuaThread)@object;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000E480 File Offset: 0x0000C680
		public LuaThread MainThread
		{
			get
			{
				Lua mainThread = this._luaState.MainThread;
				int top = mainThread.GetTop();
				mainThread.PushThread();
				object @object = this._translator.GetObject(mainThread, -1);
				mainThread.SetTop(top);
				return (LuaThread)@object;
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000E4C0 File Offset: 0x0000C6C0
		public Lua(bool openLibs = true)
		{
			this._luaState = new Lua(openLibs);
			this.Init();
			this._luaState.AtPanic(new LuaFunction(Lua.PanicCallback));
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000E510 File Offset: 0x0000C710
		public Lua(Lua luaState)
		{
			luaState.PushString("NLua_Loaded");
			luaState.GetTable(-1001000);
			if (luaState.ToBoolean(-1))
			{
				luaState.SetTop(-2);
				throw new LuaException("There is already a NLua.Lua instance associated with this Lua state");
			}
			this._luaState = luaState;
			this._StatePassed = true;
			luaState.SetTop(-2);
			this.Init();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000E584 File Offset: 0x0000C784
		private void Init()
		{
			this._luaState.PushString("NLua_Loaded");
			this._luaState.PushBoolean(true);
			this._luaState.SetTable(-1001000);
			if (!this._StatePassed)
			{
				this._luaState.NewTable();
				this._luaState.SetGlobal("luanet");
			}
			this._luaState.PushGlobalTable();
			this._luaState.GetGlobal("luanet");
			this._luaState.PushString("getmetatable");
			this._luaState.GetGlobal("getmetatable");
			this._luaState.SetTable(-3);
			this._luaState.PopGlobalTable();
			this._translator = new ObjectTranslator(this, this._luaState);
			ObjectTranslatorPool.Instance.Add(this._luaState, this._translator);
			this._luaState.PopGlobalTable();
			this._luaState.DoString("local a={}local rawget=rawget;local b=luanet.import_type;local c=luanet.load_assembly;luanet.error,luanet.type=error,type;function a:__index(d)local e=rawget(self,'.fqn')e=(e and e..'.'or'')..d;local f=rawget(luanet,d)or b(e)if f==nil then pcall(c,e)f={['.fqn']=e}setmetatable(f,a)end;rawset(self,d,f)return f end;function a:__call(...)error('No such type: '..rawget(self,'.fqn'),2)end;luanet['.fqn']=false;setmetatable(luanet,a)luanet.load_assembly('mscorlib')");
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000E679 File Offset: 0x0000C879
		public void Close()
		{
			if (this._StatePassed || this._luaState == null)
			{
				return;
			}
			this._luaState.Close();
			ObjectTranslatorPool.Instance.Remove(this._luaState);
			this._luaState = null;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int PanicCallback(IntPtr state)
		{
			Lua lua = Lua.FromIntPtr(state);
			throw new LuaException(string.Format("Unprotected error in call to Lua API ({0})", lua.ToString(-1, false)));
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		private void ThrowExceptionFromError(int oldTop)
		{
			object obj = this._translator.GetObject(this._luaState, -1);
			this._luaState.SetTop(oldTop);
			LuaScriptException ex = obj as LuaScriptException;
			if (ex != null)
			{
				throw ex;
			}
			if (obj == null)
			{
				obj = "Unknown Lua Error";
			}
			throw new LuaScriptException(obj.ToString(), string.Empty);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000E730 File Offset: 0x0000C930
		private static int PushDebugTraceback(Lua luaState, int argCount)
		{
			luaState.GetGlobal("debug");
			luaState.GetField(-1, "traceback");
			luaState.Remove(-2);
			int num = -argCount - 2;
			luaState.Insert(num);
			return num;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000E76C File Offset: 0x0000C96C
		public string GetDebugTraceback()
		{
			int top = this._luaState.GetTop();
			this._luaState.GetGlobal("debug");
			this._luaState.GetField(-1, "traceback");
			this._luaState.Remove(-2);
			this._luaState.PCall(0, -1, 0);
			return this._translator.PopValues(this._luaState, top)[0] as string;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
		internal int SetPendingException(Exception e)
		{
			if (e == null)
			{
				return 0;
			}
			this._translator.ThrowError(this._luaState, e);
			return 1;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000E808 File Offset: 0x0000CA08
		public LuaFunction LoadString(string chunk, string name)
		{
			int top = this._luaState.GetTop();
			this._executing = true;
			try
			{
				if (this._luaState.LoadString(chunk, name) != LuaStatus.OK)
				{
					this.ThrowExceptionFromError(top);
				}
			}
			finally
			{
				this._executing = false;
			}
			LuaFunction function = this._translator.GetFunction(this._luaState, -1);
			this._translator.PopValues(this._luaState, top);
			return function;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000E880 File Offset: 0x0000CA80
		public LuaFunction LoadString(byte[] chunk, string name)
		{
			int top = this._luaState.GetTop();
			this._executing = true;
			try
			{
				if (this._luaState.LoadBuffer(chunk, name) != LuaStatus.OK)
				{
					this.ThrowExceptionFromError(top);
				}
			}
			finally
			{
				this._executing = false;
			}
			LuaFunction function = this._translator.GetFunction(this._luaState, -1);
			this._translator.PopValues(this._luaState, top);
			return function;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000E8F8 File Offset: 0x0000CAF8
		public LuaFunction LoadFile(string fileName)
		{
			int top = this._luaState.GetTop();
			if (this._luaState.LoadFile(fileName) != LuaStatus.OK)
			{
				this.ThrowExceptionFromError(top);
			}
			LuaFunction function = this._translator.GetFunction(this._luaState, -1);
			this._translator.PopValues(this._luaState, top);
			return function;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000E94C File Offset: 0x0000CB4C
		public object[] DoString(byte[] chunk, string chunkName = "chunk", string mode = "t")
		{
			int num = this._luaState.GetTop();
			this._executing = true;
			if (this._luaState.LoadBuffer(chunk, chunkName, mode) != LuaStatus.OK)
			{
				this.ThrowExceptionFromError(num);
			}
			int errorFunctionIndex = 0;
			if (this.UseTraceback)
			{
				errorFunctionIndex = Lua.PushDebugTraceback(this._luaState, 0);
				num++;
			}
			object[] result;
			try
			{
				if (this._luaState.PCall(0, -1, errorFunctionIndex) != LuaStatus.OK)
				{
					this.ThrowExceptionFromError(num);
				}
				result = this._translator.PopValues(this._luaState, num);
			}
			finally
			{
				this._executing = false;
			}
			return result;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		public object[] DoString(string chunk, string chunkName = "chunk")
		{
			int num = this._luaState.GetTop();
			this._executing = true;
			if (this._luaState.LoadString(chunk, chunkName) != LuaStatus.OK)
			{
				this.ThrowExceptionFromError(num);
			}
			int errorFunctionIndex = 0;
			if (this.UseTraceback)
			{
				errorFunctionIndex = Lua.PushDebugTraceback(this._luaState, 0);
				num++;
			}
			object[] result;
			try
			{
				if (this._luaState.PCall(0, -1, errorFunctionIndex) != LuaStatus.OK)
				{
					this.ThrowExceptionFromError(num);
				}
				result = this._translator.PopValues(this._luaState, num);
			}
			finally
			{
				this._executing = false;
			}
			return result;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000EA7C File Offset: 0x0000CC7C
		public object[] DoFile(string fileName)
		{
			int num = this._luaState.GetTop();
			if (this._luaState.LoadFile(fileName) != LuaStatus.OK)
			{
				this.ThrowExceptionFromError(num);
			}
			this._executing = true;
			int errorFunctionIndex = 0;
			if (this.UseTraceback)
			{
				errorFunctionIndex = Lua.PushDebugTraceback(this._luaState, 0);
				num++;
			}
			object[] result;
			try
			{
				if (this._luaState.PCall(0, -1, errorFunctionIndex) != LuaStatus.OK)
				{
					this.ThrowExceptionFromError(num);
				}
				result = this._translator.PopValues(this._luaState, num);
			}
			finally
			{
				this._executing = false;
			}
			return result;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000EB14 File Offset: 0x0000CD14
		public object GetObjectFromPath(string fullPath)
		{
			int top = this._luaState.GetTop();
			string[] array = this.FullPathToArray(fullPath);
			this._luaState.GetGlobal(array[0]);
			object @object = this._translator.GetObject(this._luaState, -1);
			if (array.Length > 1)
			{
				LuaBase luaBase = @object as LuaBase;
				string[] array2 = new string[array.Length - 1];
				Array.Copy(array, 1, array2, 0, array.Length - 1);
				@object = this.GetObject(array2);
				if (luaBase != null)
				{
					luaBase.Dispose();
				}
			}
			this._luaState.SetTop(top);
			return @object;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000EB9C File Offset: 0x0000CD9C
		public void SetObjectToPath(string fullPath, object value)
		{
			int top = this._luaState.GetTop();
			string[] array = this.FullPathToArray(fullPath);
			if (array.Length == 1)
			{
				this._translator.Push(this._luaState, value);
				this._luaState.SetGlobal(fullPath);
			}
			else
			{
				this._luaState.GetGlobal(array[0]);
				string[] array2 = new string[array.Length - 1];
				Array.Copy(array, 1, array2, 0, array.Length - 1);
				this.SetObject(array2, value);
			}
			this._luaState.SetTop(top);
			if (value == null)
			{
				this._globals.Remove(fullPath);
				return;
			}
			if (!this._globals.Contains(fullPath))
			{
				this.RegisterGlobal(fullPath, value.GetType(), 0);
			}
		}

		// Token: 0x17000090 RID: 144
		public object this[string fullPath]
		{
			get
			{
				object objectFromPath = this.GetObjectFromPath(fullPath);
				if (objectFromPath is long)
				{
					long num = (long)objectFromPath;
					return (double)num;
				}
				return objectFromPath;
			}
			set
			{
				this.SetObjectToPath(fullPath, value);
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000EC84 File Offset: 0x0000CE84
		private void RegisterGlobal(string path, Type type, int recursionCounter)
		{
			if (type == typeof(LuaFunction))
			{
				this._globals.Add(path + "(");
			}
			else if ((type.IsClass || type.IsInterface) && type != typeof(string) && recursionCounter < this.MaximumRecursion)
			{
				foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public))
				{
					string name = methodInfo.Name;
					if (!methodInfo.GetCustomAttributes(typeof(LuaHideAttribute), false).Any<object>() && !methodInfo.GetCustomAttributes(typeof(LuaGlobalAttribute), false).Any<object>() && name != "GetType" && name != "GetHashCode" && name != "Equals" && name != "ToString" && name != "Clone" && name != "Dispose" && name != "GetEnumerator" && name != "CopyTo" && !name.StartsWith("get_", StringComparison.Ordinal) && !name.StartsWith("set_", StringComparison.Ordinal) && !name.StartsWith("add_", StringComparison.Ordinal) && !name.StartsWith("remove_", StringComparison.Ordinal))
					{
						string text = path + ":" + name + "(";
						if (methodInfo.GetParameters().Length == 0)
						{
							text += ")";
						}
						this._globals.Add(text);
					}
				}
				foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
				{
					if (!fieldInfo.GetCustomAttributes(typeof(LuaHideAttribute), false).Any<object>() && !fieldInfo.GetCustomAttributes(typeof(LuaGlobalAttribute), false).Any<object>())
					{
						this.RegisterGlobal(path + "." + fieldInfo.Name, fieldInfo.FieldType, recursionCounter + 1);
					}
				}
				foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
				{
					if (!propertyInfo.GetCustomAttributes(typeof(LuaHideAttribute), false).Any<object>() && !propertyInfo.GetCustomAttributes(typeof(LuaGlobalAttribute), false).Any<object>() && propertyInfo.Name != "Item")
					{
						this.RegisterGlobal(path + "." + propertyInfo.Name, propertyInfo.PropertyType, recursionCounter + 1);
					}
				}
			}
			else
			{
				this._globals.Add(path);
			}
			this._globalsSorted = false;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000EF48 File Offset: 0x0000D148
		private object GetObject(string[] remainingPath)
		{
			object obj = null;
			for (int i = 0; i < remainingPath.Length; i++)
			{
				this._luaState.PushString(remainingPath[i]);
				this._luaState.GetTable(-2);
				obj = this._translator.GetObject(this._luaState, -1);
				if (obj == null)
				{
					break;
				}
			}
			return obj;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000EF98 File Offset: 0x0000D198
		public double GetNumber(string fullPath)
		{
			object objectFromPath = this.GetObjectFromPath(fullPath);
			if (objectFromPath is long)
			{
				long num = (long)objectFromPath;
				return (double)num;
			}
			return (double)objectFromPath;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000EFC8 File Offset: 0x0000D1C8
		public int GetInteger(string fullPath)
		{
			object objectFromPath = this.GetObjectFromPath(fullPath);
			if (objectFromPath == null)
			{
				return 0;
			}
			return (int)((long)objectFromPath);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000EFEC File Offset: 0x0000D1EC
		public long GetLong(string fullPath)
		{
			object objectFromPath = this.GetObjectFromPath(fullPath);
			if (objectFromPath == null)
			{
				return 0L;
			}
			return (long)objectFromPath;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000F010 File Offset: 0x0000D210
		public string GetString(string fullPath)
		{
			object objectFromPath = this.GetObjectFromPath(fullPath);
			if (objectFromPath == null)
			{
				return null;
			}
			return objectFromPath.ToString();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000F030 File Offset: 0x0000D230
		public LuaTable GetTable(string fullPath)
		{
			return (LuaTable)this.GetObjectFromPath(fullPath);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000F03E File Offset: 0x0000D23E
		public object GetTable(Type interfaceType, string fullPath)
		{
			return CodeGeneration.Instance.GetClassInstance(interfaceType, this.GetTable(fullPath));
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000F052 File Offset: 0x0000D252
		public LuaThread GetThread(string fullPath)
		{
			return (LuaThread)this.GetObjectFromPath(fullPath);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000F060 File Offset: 0x0000D260
		public LuaFunction GetFunction(string fullPath)
		{
			object objectFromPath = this.GetObjectFromPath(fullPath);
			LuaFunction luaFunction = objectFromPath as LuaFunction;
			if (luaFunction != null)
			{
				return luaFunction;
			}
			return new LuaFunction((LuaFunction)objectFromPath, this);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000F08F File Offset: 0x0000D28F
		public void RegisterLuaDelegateType(Type delegateType, Type luaDelegateType)
		{
			CodeGeneration.Instance.RegisterLuaDelegateType(delegateType, luaDelegateType);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000F09D File Offset: 0x0000D29D
		public void RegisterLuaClassType(Type klass, Type luaClass)
		{
			CodeGeneration.Instance.RegisterLuaClassType(klass, luaClass);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000F0AB File Offset: 0x0000D2AB
		public void LoadCLRPackage()
		{
			this._luaState.DoString("if not luanet then require'luanet'end;local a,b=luanet.import_type,luanet.load_assembly;local c={__index=function(d,e)local f=rawget(d,e)if f==nil then f=a(d.packageName..\".\"..e)if f==nil then f=a(e)end;d[e]=f end;return f end}function luanet.namespace(g)if type(g)=='table'then local h={}for i=1,#g do h[i]=luanet.namespace(g[i])end;return unpack(h)end;local j={packageName=g}setmetatable(j,c)return j end;local k,l;local function m()l={}k={__index=function(n,e)for i,d in ipairs(l)do local f=d[e]if f then _G[e]=f;return f end end end}setmetatable(_G,k)end;function CLRPackage(o,p)p=p or o;local q=pcall(b,o)return luanet.namespace(p)end;function import(o,p)if not k then m()end;if not p then local i=o:find('%.dll$')if i then p=o:sub(1,i-1)else p=o end end;local j=CLRPackage(o,p)table.insert(l,j)return j end;function luanet.make_array(r,s)local t=r[#s]for i,u in ipairs(s)do t:SetValue(u,i-1)end;return t end;function luanet.each(v)local w=v:GetEnumerator()return function()if w:MoveNext()then return w.Current end end end");
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000F0BE File Offset: 0x0000D2BE
		public Delegate GetFunction(Type delegateType, string fullPath)
		{
			return CodeGeneration.Instance.GetDelegate(delegateType, this.GetFunction(fullPath));
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000F0D2 File Offset: 0x0000D2D2
		internal object[] CallFunction(object function, object[] args)
		{
			return this.CallFunction(function, args, null);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000F0E0 File Offset: 0x0000D2E0
		internal object[] CallFunction(object function, object[] args, Type[] returnTypes)
		{
			int num = 0;
			int num2 = this._luaState.GetTop();
			if (!this._luaState.CheckStack(args.Length + 6))
			{
				throw new LuaException("Lua stack overflow");
			}
			this._translator.Push(this._luaState, function);
			if (args.Length != 0)
			{
				num = args.Length;
				for (int i = 0; i < args.Length; i++)
				{
					this._translator.Push(this._luaState, args[i]);
				}
			}
			this._executing = true;
			try
			{
				int errorFunctionIndex = 0;
				if (this.UseTraceback)
				{
					errorFunctionIndex = Lua.PushDebugTraceback(this._luaState, num);
					num2++;
				}
				if (this._luaState.PCall(num, -1, errorFunctionIndex) != LuaStatus.OK)
				{
					this.ThrowExceptionFromError(num2);
				}
			}
			finally
			{
				this._executing = false;
			}
			if (returnTypes != null)
			{
				return this._translator.PopValues(this._luaState, num2, returnTypes);
			}
			return this._translator.PopValues(this._luaState, num2);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000F1D0 File Offset: 0x0000D3D0
		private void SetObject(string[] remainingPath, object val)
		{
			for (int i = 0; i < remainingPath.Length - 1; i++)
			{
				this._luaState.PushString(remainingPath[i]);
				this._luaState.GetTable(-2);
			}
			this._luaState.PushString(remainingPath[remainingPath.Length - 1]);
			this._translator.Push(this._luaState, val);
			this._luaState.SetTable(-3);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000F23A File Offset: 0x0000D43A
		private string[] FullPathToArray(string fullPath)
		{
			return fullPath.SplitWithEscape('.', '\\').ToArray<string>();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000F24C File Offset: 0x0000D44C
		public void NewTable(string fullPath)
		{
			string[] array = this.FullPathToArray(fullPath);
			int top = this._luaState.GetTop();
			if (array.Length == 1)
			{
				this._luaState.NewTable();
				this._luaState.SetGlobal(fullPath);
			}
			else
			{
				this._luaState.GetGlobal(array[0]);
				for (int i = 1; i < array.Length - 1; i++)
				{
					this._luaState.PushString(array[i]);
					this._luaState.GetTable(-2);
				}
				this._luaState.PushString(array[array.Length - 1]);
				this._luaState.NewTable();
				this._luaState.SetTable(-3);
			}
			this._luaState.SetTop(top);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000F300 File Offset: 0x0000D500
		public Dictionary<object, object> GetTableDict(LuaTable table)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			Dictionary<object, object> dictionary = new Dictionary<object, object>();
			int top = this._luaState.GetTop();
			this._translator.Push(this._luaState, table);
			this._luaState.PushNil();
			while (this._luaState.Next(-2))
			{
				dictionary[this._translator.GetObject(this._luaState, -2)] = this._translator.GetObject(this._luaState, -1);
				this._luaState.SetTop(-2);
			}
			this._luaState.SetTop(top);
			return dictionary;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		public int SetDebugHook(LuaHookMask mask, int count)
		{
			if (this._hookCallback == null)
			{
				this._hookCallback = new LuaHookFunction(Lua.DebugHookCallback);
				this._luaState.SetHook(this._hookCallback, mask, count);
			}
			return -1;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
		public void RemoveDebugHook()
		{
			this._hookCallback = null;
			this._luaState.SetHook(null, LuaHookMask.Disabled, 0);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000F3E7 File Offset: 0x0000D5E7
		public LuaHookMask GetHookMask()
		{
			return this._luaState.HookMask;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000F3F4 File Offset: 0x0000D5F4
		public int GetHookCount()
		{
			return this._luaState.HookCount;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000F401 File Offset: 0x0000D601
		public string GetLocal(LuaDebug luaDebug, int n)
		{
			return this._luaState.GetLocal(luaDebug, n);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000F410 File Offset: 0x0000D610
		public string SetLocal(LuaDebug luaDebug, int n)
		{
			return this._luaState.SetLocal(luaDebug, n);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000F41F File Offset: 0x0000D61F
		public int GetStack(int level, ref LuaDebug ar)
		{
			return this._luaState.GetStack(level, ref ar);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000F42E File Offset: 0x0000D62E
		public bool GetInfo(string what, ref LuaDebug ar)
		{
			return this._luaState.GetInfo(what, ref ar);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000F43D File Offset: 0x0000D63D
		public string GetUpValue(int funcindex, int n)
		{
			return this._luaState.GetUpValue(funcindex, n);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000F44C File Offset: 0x0000D64C
		public string SetUpValue(int funcindex, int n)
		{
			return this._luaState.SetUpValue(funcindex, n);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000F45C File Offset: 0x0000D65C
		[MonoPInvokeCallback(typeof(LuaHookFunction))]
		private static void DebugHookCallback(IntPtr luaState, IntPtr luaDebug)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			lua.GetStack(0, luaDebug);
			if (!lua.GetInfo("Snlu", luaDebug))
			{
				return;
			}
			LuaDebug luaDebug2 = LuaDebug.FromIntPtr(luaDebug);
			ObjectTranslatorPool.Instance.Find(lua).Interpreter.DebugHookCallbackInternal(luaDebug2);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
		private void DebugHookCallbackInternal(LuaDebug luaDebug)
		{
			try
			{
				EventHandler<DebugHookEventArgs> debugHook = this.DebugHook;
				if (debugHook != null)
				{
					debugHook(this, new DebugHookEventArgs(luaDebug));
				}
			}
			catch (Exception ex)
			{
				this.OnHookException(new HookExceptionEventArgs(ex));
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		private void OnHookException(HookExceptionEventArgs e)
		{
			EventHandler<HookExceptionEventArgs> hookException = this.HookException;
			if (hookException != null)
			{
				hookException(this, e);
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000F510 File Offset: 0x0000D710
		public object Pop()
		{
			int top = this._luaState.GetTop();
			return this._translator.PopValues(this._luaState, top - 1)[0];
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000F53F File Offset: 0x0000D73F
		public void Push(object value)
		{
			this._translator.Push(this._luaState, value);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000F553 File Offset: 0x0000D753
		internal void DisposeInternal(int reference, bool finalized)
		{
			if (finalized && this._translator != null)
			{
				this._translator.AddFinalizedReference(reference);
				return;
			}
			if (this._luaState != null && !finalized)
			{
				this._luaState.Unref(reference);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000F584 File Offset: 0x0000D784
		internal object RawGetObject(int reference, string field)
		{
			int top = this._luaState.GetTop();
			this._luaState.GetRef(reference);
			this._luaState.PushString(field);
			this._luaState.RawGet(-2);
			object @object = this._translator.GetObject(this._luaState, -1);
			this._luaState.SetTop(top);
			return @object;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000F5E4 File Offset: 0x0000D7E4
		internal object GetObject(int reference, string field)
		{
			int top = this._luaState.GetTop();
			this._luaState.GetRef(reference);
			object @object = this.GetObject(this.FullPathToArray(field));
			this._luaState.SetTop(top);
			return @object;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000F624 File Offset: 0x0000D824
		internal object GetObject(int reference, object field)
		{
			int top = this._luaState.GetTop();
			this._luaState.GetRef(reference);
			this._translator.Push(this._luaState, field);
			this._luaState.GetTable(-2);
			object @object = this._translator.GetObject(this._luaState, -1);
			this._luaState.SetTop(top);
			return @object;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000F688 File Offset: 0x0000D888
		internal void SetObject(int reference, string field, object val)
		{
			int top = this._luaState.GetTop();
			this._luaState.GetRef(reference);
			this.SetObject(this.FullPathToArray(field), val);
			this._luaState.SetTop(top);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000F6C8 File Offset: 0x0000D8C8
		internal void SetObject(int reference, object field, object val)
		{
			int top = this._luaState.GetTop();
			this._luaState.GetRef(reference);
			this._translator.Push(this._luaState, field);
			this._translator.Push(this._luaState, val);
			this._luaState.SetTable(-3);
			this._luaState.SetTop(top);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000F72C File Offset: 0x0000D92C
		internal Lua GetThreadState(int reference)
		{
			int top = this._luaState.GetTop();
			this._luaState.GetRef(reference);
			Lua result = this._luaState.ToThread(-1);
			this._luaState.SetTop(top);
			return result;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000F76C File Offset: 0x0000D96C
		public void XMove(Lua to, object val, int index = 1)
		{
			int top = this._luaState.GetTop();
			this._translator.Push(this._luaState, val);
			this._luaState.XMove(to, index);
			this._luaState.SetTop(top);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000F7B0 File Offset: 0x0000D9B0
		public void XMove(Lua to, object val, int index = 1)
		{
			int top = this._luaState.GetTop();
			this._translator.Push(this._luaState, val);
			this._luaState.XMove(to._luaState, index);
			this._luaState.SetTop(top);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000F7FC File Offset: 0x0000D9FC
		public void XMove(LuaThread thread, object val, int index = 1)
		{
			int top = this._luaState.GetTop();
			this._translator.Push(this._luaState, val);
			this._luaState.XMove(thread.State, index);
			this._luaState.SetTop(top);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000F848 File Offset: 0x0000DA48
		public Lua NewThread(out LuaThread thread)
		{
			int top = this._luaState.GetTop();
			Lua result = this._luaState.NewThread();
			thread = (LuaThread)this._translator.GetObject(this._luaState, -1);
			this._luaState.SetTop(top);
			return result;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000F894 File Offset: 0x0000DA94
		public Lua NewThread(string fullPath)
		{
			string[] array = this.FullPathToArray(fullPath);
			int top = this._luaState.GetTop();
			Lua result;
			if (array.Length == 1)
			{
				result = this._luaState.NewThread();
				this._luaState.SetGlobal(fullPath);
			}
			else
			{
				this._luaState.GetGlobal(array[0]);
				for (int i = 1; i < array.Length - 1; i++)
				{
					this._luaState.PushString(array[i]);
					this._luaState.GetTable(-2);
				}
				this._luaState.PushString(array[array.Length - 1]);
				result = this._luaState.NewThread();
				this._luaState.SetTable(-3);
			}
			this._luaState.SetTop(top);
			return result;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000F948 File Offset: 0x0000DB48
		public Lua NewThread(LuaFunction function, out LuaThread thread)
		{
			int top = this._luaState.GetTop();
			Lua lua = this._luaState.NewThread();
			thread = (LuaThread)this._translator.GetObject(this._luaState, -1);
			this._translator.Push(this._luaState, function);
			this._luaState.XMove(lua, 1);
			this._luaState.SetTop(top);
			return lua;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000F9B4 File Offset: 0x0000DBB4
		public void NewThread(string fullPath, LuaFunction function)
		{
			string[] array = this.FullPathToArray(fullPath);
			int top = this._luaState.GetTop();
			Lua to;
			if (array.Length == 1)
			{
				to = this._luaState.NewThread();
				this._luaState.SetGlobal(fullPath);
			}
			else
			{
				this._luaState.GetGlobal(array[0]);
				for (int i = 1; i < array.Length - 1; i++)
				{
					this._luaState.PushString(array[i]);
					this._luaState.GetTable(-2);
				}
				this._luaState.PushString(array[array.Length - 1]);
				to = this._luaState.NewThread();
				this._luaState.SetTable(-3);
			}
			this._translator.Push(this._luaState, function);
			this._luaState.XMove(to, 1);
			this._luaState.SetTop(top);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000FA86 File Offset: 0x0000DC86
		public LuaFunction RegisterFunction(string path, MethodBase function)
		{
			return this.RegisterFunction(path, null, function);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000FA94 File Offset: 0x0000DC94
		public LuaFunction RegisterFunction(string path, object target, MethodBase function)
		{
			int top = this._luaState.GetTop();
			LuaMethodWrapper luaMethodWrapper = new LuaMethodWrapper(this._translator, target, new ProxyType(function.DeclaringType), function);
			this._translator.Push(this._luaState, new LuaFunction(luaMethodWrapper.InvokeFunction.Invoke));
			object @object = this._translator.GetObject(this._luaState, -1);
			this.SetObjectToPath(path, @object);
			LuaFunction function2 = this.GetFunction(path);
			this._luaState.SetTop(top);
			return function2;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000FB18 File Offset: 0x0000DD18
		internal bool CompareRef(int ref1, int ref2)
		{
			int top = this._luaState.GetTop();
			this._luaState.GetRef(ref1);
			this._luaState.GetRef(ref2);
			bool result = this._luaState.AreEqual(-1, -2);
			this._luaState.SetTop(top);
			return result;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000FB63 File Offset: 0x0000DD63
		internal void PushCSFunction(LuaFunction function)
		{
			this._translator.PushFunction(this._luaState, function);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000FB78 File Offset: 0x0000DD78
		~Lua()
		{
			this.Dispose();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
		public virtual void Dispose()
		{
			if (this._translator != null)
			{
				this._translator.PendingEvents.Dispose();
				if (this._translator.Tag != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this._translator.Tag);
				}
				this._translator = null;
			}
			this.Close();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000206 RID: 518
		private LuaHookFunction _hookCallback;

		// Token: 0x04000207 RID: 519
		private readonly List<string> _globals = new List<string>();

		// Token: 0x04000208 RID: 520
		private bool _globalsSorted;

		// Token: 0x04000209 RID: 521
		private Lua _luaState;

		// Token: 0x0400020A RID: 522
		private ObjectTranslator _translator;

		// Token: 0x0400020B RID: 523
		private bool _StatePassed;

		// Token: 0x0400020C RID: 524
		private bool _executing;

		// Token: 0x0400020D RID: 525
		private const string InitLuanet = "local a={}local rawget=rawget;local b=luanet.import_type;local c=luanet.load_assembly;luanet.error,luanet.type=error,type;function a:__index(d)local e=rawget(self,'.fqn')e=(e and e..'.'or'')..d;local f=rawget(luanet,d)or b(e)if f==nil then pcall(c,e)f={['.fqn']=e}setmetatable(f,a)end;rawset(self,d,f)return f end;function a:__call(...)error('No such type: '..rawget(self,'.fqn'),2)end;luanet['.fqn']=false;setmetatable(luanet,a)luanet.load_assembly('mscorlib')";

		// Token: 0x0400020E RID: 526
		private const string ClrPackage = "if not luanet then require'luanet'end;local a,b=luanet.import_type,luanet.load_assembly;local c={__index=function(d,e)local f=rawget(d,e)if f==nil then f=a(d.packageName..\".\"..e)if f==nil then f=a(e)end;d[e]=f end;return f end}function luanet.namespace(g)if type(g)=='table'then local h={}for i=1,#g do h[i]=luanet.namespace(g[i])end;return unpack(h)end;local j={packageName=g}setmetatable(j,c)return j end;local k,l;local function m()l={}k={__index=function(n,e)for i,d in ipairs(l)do local f=d[e]if f then _G[e]=f;return f end end end}setmetatable(_G,k)end;function CLRPackage(o,p)p=p or o;local q=pcall(b,o)return luanet.namespace(p)end;function import(o,p)if not k then m()end;if not p then local i=o:find('%.dll$')if i then p=o:sub(1,i-1)else p=o end end;local j=CLRPackage(o,p)table.insert(l,j)return j end;function luanet.make_array(r,s)local t=r[#s]for i,u in ipairs(s)do t:SetValue(u,i-1)end;return t end;function luanet.each(v)local w=v:GetEnumerator()return function()if w:MoveNext()then return w.Current end end end";
	}
}

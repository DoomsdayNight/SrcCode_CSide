using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AOT;
using KeraLua;
using NLua.Exceptions;
using NLua.Extensions;
using NLua.Method;

namespace NLua
{
	// Token: 0x0200006B RID: 107
	public class MetaFunctions
	{
		// Token: 0x06000395 RID: 917 RVA: 0x00010582 File Offset: 0x0000E782
		public MetaFunctions(ObjectTranslator translator)
		{
			this._translator = translator;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001059C File Offset: 0x0000E79C
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int RunFunctionDelegate(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			LuaFunction luaFunction = (LuaFunction)objectTranslator.GetRawNetObject(lua, 1);
			if (luaFunction == null)
			{
				return lua.Error();
			}
			lua.Remove(1);
			int result = luaFunction(luaState);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000105FC File Offset: 0x0000E7FC
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int CollectObject(IntPtr state)
		{
			Lua luaState = Lua.FromIntPtr(state);
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(luaState);
			return MetaFunctions.CollectObject(luaState, translator);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00010624 File Offset: 0x0000E824
		private static int CollectObject(Lua luaState, ObjectTranslator translator)
		{
			int num = luaState.RawNetObj(1);
			if (num != -1)
			{
				translator.CollectObject(num);
			}
			return 0;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00010648 File Offset: 0x0000E848
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int ToStringLua(IntPtr state)
		{
			Lua luaState = Lua.FromIntPtr(state);
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(luaState);
			return MetaFunctions.ToStringLua(luaState, translator);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00010670 File Offset: 0x0000E870
		private static int ToStringLua(Lua luaState, ObjectTranslator translator)
		{
			object rawNetObject = translator.GetRawNetObject(luaState, 1);
			if (rawNetObject != null)
			{
				translator.Push(luaState, ((rawNetObject != null) ? rawNetObject.ToString() : null) + ": " + rawNetObject.GetHashCode().ToString());
			}
			else
			{
				luaState.PushNil();
			}
			return 1;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000106C0 File Offset: 0x0000E8C0
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int AddLua(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = MetaFunctions.MatchOperator(lua, "op_Addition", objectTranslator);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00010704 File Offset: 0x0000E904
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int SubtractLua(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = MetaFunctions.MatchOperator(lua, "op_Subtraction", objectTranslator);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00010748 File Offset: 0x0000E948
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int MultiplyLua(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = MetaFunctions.MatchOperator(lua, "op_Multiply", objectTranslator);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001078C File Offset: 0x0000E98C
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int DivideLua(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = MetaFunctions.MatchOperator(lua, "op_Division", objectTranslator);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000107D0 File Offset: 0x0000E9D0
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int ModLua(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = MetaFunctions.MatchOperator(lua, "op_Modulus", objectTranslator);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00010814 File Offset: 0x0000EA14
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int UnaryNegationLua(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = MetaFunctions.UnaryNegationLua(lua, objectTranslator);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00010854 File Offset: 0x0000EA54
		private static int UnaryNegationLua(Lua luaState, ObjectTranslator translator)
		{
			object obj = translator.GetRawNetObject(luaState, 1);
			if (obj == null)
			{
				translator.ThrowError(luaState, "Cannot negate a nil object");
				return 1;
			}
			Type type = obj.GetType();
			MethodInfo method = type.GetMethod("op_UnaryNegation");
			if (method == null)
			{
				translator.ThrowError(luaState, "Cannot negate object (" + type.Name + " does not overload the operator -)");
				return 1;
			}
			obj = method.Invoke(obj, new object[]
			{
				obj
			});
			translator.Push(luaState, obj);
			return 1;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000108D0 File Offset: 0x0000EAD0
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int EqualLua(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = MetaFunctions.MatchOperator(lua, "op_Equality", objectTranslator);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00010914 File Offset: 0x0000EB14
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int LessThanLua(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = MetaFunctions.MatchOperator(lua, "op_LessThan", objectTranslator);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00010958 File Offset: 0x0000EB58
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int LessThanOrEqualLua(IntPtr luaState)
		{
			Lua lua = Lua.FromIntPtr(luaState);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = MetaFunctions.MatchOperator(lua, "op_LessThanOrEqual", objectTranslator);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001099C File Offset: 0x0000EB9C
		public static void DumpStack(ObjectTranslator translator, Lua luaState)
		{
			int top = luaState.GetTop();
			for (int i = 1; i <= top; i++)
			{
				LuaType luaType = luaState.Type(i);
				if (luaType != LuaType.Table)
				{
					luaState.TypeName(luaType);
				}
				luaState.ToString(i, false);
				if (luaType == LuaType.UserData)
				{
					object rawNetObject = translator.GetRawNetObject(luaState, i);
					if (rawNetObject != null)
					{
						rawNetObject.ToString();
					}
				}
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000109F0 File Offset: 0x0000EBF0
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int GetMethod(IntPtr state)
		{
			Lua lua = Lua.FromIntPtr(state);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int methodInternal = objectTranslator.MetaFunctionsInstance.GetMethodInternal(lua);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return methodInternal;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00010A34 File Offset: 0x0000EC34
		private int GetMethodInternal(Lua luaState)
		{
			object rawNetObject = this._translator.GetRawNetObject(luaState, 1);
			if (rawNetObject == null)
			{
				this._translator.ThrowError(luaState, "Trying to index an invalid object reference");
				return 1;
			}
			object @object = this._translator.GetObject(luaState, 2);
			string text = @object as string;
			Type type = rawNetObject.GetType();
			ProxyType objType = new ProxyType(type);
			if (!string.IsNullOrEmpty(text) && this.IsMemberPresent(objType, text))
			{
				return this.GetMember(luaState, objType, rawNetObject, text, BindingFlags.Instance);
			}
			if (this.TryAccessByArray(luaState, type, rawNetObject, @object))
			{
				return 1;
			}
			int methodFallback = this.GetMethodFallback(luaState, type, rawNetObject, text);
			if (methodFallback != 0)
			{
				return methodFallback;
			}
			if (!string.IsNullOrEmpty(text) || @object != null)
			{
				if (string.IsNullOrEmpty(text))
				{
					text = @object.ToString();
				}
				return this.PushInvalidMethodCall(luaState, type, text);
			}
			luaState.PushBoolean(false);
			return 2;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00010AF6 File Offset: 0x0000ECF6
		private int PushInvalidMethodCall(Lua luaState, Type type, string name)
		{
			this.SetMemberCache(type, name, null);
			this._translator.Push(luaState, null);
			this._translator.Push(luaState, false);
			return 2;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00010B24 File Offset: 0x0000ED24
		private bool TryAccessByArray(Lua luaState, Type objType, object obj, object index)
		{
			if (!objType.IsArray)
			{
				return false;
			}
			int num = -1;
			if (index is long)
			{
				long num2 = (long)index;
				num = (int)num2;
			}
			else if (index is double)
			{
				double num3 = (double)index;
				num = (int)num3;
			}
			if (num == -1)
			{
				return false;
			}
			Type underlyingSystemType = objType.UnderlyingSystemType;
			if (underlyingSystemType == typeof(long[]))
			{
				long[] array = (long[])obj;
				this._translator.Push(luaState, array[num]);
				return true;
			}
			if (underlyingSystemType == typeof(float[]))
			{
				float[] array2 = (float[])obj;
				this._translator.Push(luaState, array2[num]);
				return true;
			}
			if (underlyingSystemType == typeof(double[]))
			{
				double[] array3 = (double[])obj;
				this._translator.Push(luaState, array3[num]);
				return true;
			}
			if (underlyingSystemType == typeof(int[]))
			{
				int[] array4 = (int[])obj;
				this._translator.Push(luaState, array4[num]);
				return true;
			}
			if (underlyingSystemType == typeof(byte[]))
			{
				byte[] array5 = (byte[])obj;
				this._translator.Push(luaState, array5[num]);
				return true;
			}
			if (underlyingSystemType == typeof(short[]))
			{
				short[] array6 = (short[])obj;
				this._translator.Push(luaState, array6[num]);
				return true;
			}
			if (underlyingSystemType == typeof(ushort[]))
			{
				ushort[] array7 = (ushort[])obj;
				this._translator.Push(luaState, array7[num]);
				return true;
			}
			if (underlyingSystemType == typeof(ulong[]))
			{
				ulong[] array8 = (ulong[])obj;
				this._translator.Push(luaState, array8[num]);
				return true;
			}
			if (underlyingSystemType == typeof(uint[]))
			{
				uint[] array9 = (uint[])obj;
				this._translator.Push(luaState, array9[num]);
				return true;
			}
			if (underlyingSystemType == typeof(sbyte[]))
			{
				sbyte[] array10 = (sbyte[])obj;
				this._translator.Push(luaState, array10[num]);
				return true;
			}
			object value = ((Array)obj).GetValue(num);
			this._translator.Push(luaState, value);
			return true;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00010D7C File Offset: 0x0000EF7C
		private int GetMethodFallback(Lua luaState, Type objType, object obj, string methodName)
		{
			object method;
			if (!string.IsNullOrEmpty(methodName) && this.TryGetExtensionMethod(objType, methodName, out method))
			{
				return this.PushExtensionMethod(luaState, objType, obj, methodName, method);
			}
			MethodInfo[] methods = objType.GetMethods();
			int num = this.TryIndexMethods(luaState, methods, obj);
			if (num != 0)
			{
				return num;
			}
			methods = objType.GetRuntimeMethods().ToArray<MethodInfo>();
			num = this.TryIndexMethods(luaState, methods, obj);
			if (num != 0)
			{
				return num;
			}
			num = this.TryGetValueForKeyMethods(luaState, methods, obj);
			if (num != 0)
			{
				return num;
			}
			MethodInfo methodInfo = objType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault((MethodInfo m) => m.Name == methodName && m.IsPrivate && m.IsVirtual && m.IsFinal);
			if (methodInfo != null)
			{
				ProxyType proxyType = new ProxyType(objType);
				LuaFunction luaFunction = new LuaFunction(new LuaMethodWrapper(this._translator, obj, proxyType, methodInfo).InvokeFunction.Invoke);
				this.SetMemberCache(proxyType, methodName, luaFunction);
				this._translator.PushFunction(luaState, luaFunction);
				this._translator.Push(luaState, true);
				return 2;
			}
			return 0;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00010E88 File Offset: 0x0000F088
		private int TryGetValueForKeyMethods(Lua luaState, MethodInfo[] methods, object obj)
		{
			foreach (MethodInfo methodInfo in methods)
			{
				if (!(methodInfo.Name != "TryGetValueForKey") && methodInfo.GetParameters().Length == 2)
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					object asType = this._translator.GetAsType(luaState, 2, parameters[0].ParameterType);
					if (asType == null)
					{
						break;
					}
					object[] array = new object[2];
					array[0] = asType;
					try
					{
						if (!(bool)methodInfo.Invoke(obj, array))
						{
							this._translator.ThrowError(luaState, "key not found: " + ((asType != null) ? asType.ToString() : null));
							return 1;
						}
						this._translator.Push(luaState, array[1]);
						return 1;
					}
					catch (TargetInvocationException ex)
					{
						if (ex.InnerException is KeyNotFoundException)
						{
							this._translator.ThrowError(luaState, "key '" + ((asType != null) ? asType.ToString() : null) + "' not found ");
						}
						else
						{
							this._translator.ThrowError(luaState, "exception indexing '" + ((asType != null) ? asType.ToString() : null) + "' " + ex.Message);
						}
						return 1;
					}
				}
			}
			return 0;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00010FDC File Offset: 0x0000F1DC
		private int TryIndexMethods(Lua luaState, MethodInfo[] methods, object obj)
		{
			foreach (MethodInfo methodInfo in methods)
			{
				if (!(methodInfo.Name != "get_Item") && methodInfo.GetParameters().Length == 1)
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					object asType = this._translator.GetAsType(luaState, 2, parameters[0].ParameterType);
					if (asType != null)
					{
						object[] parameters2 = new object[]
						{
							asType
						};
						try
						{
							object o = methodInfo.Invoke(obj, parameters2);
							this._translator.Push(luaState, o);
							return 1;
						}
						catch (TargetInvocationException ex)
						{
							if (ex.InnerException is KeyNotFoundException)
							{
								this._translator.ThrowError(luaState, "key '" + ((asType != null) ? asType.ToString() : null) + "' not found ");
							}
							else
							{
								this._translator.ThrowError(luaState, "exception indexing '" + ((asType != null) ? asType.ToString() : null) + "' " + ex.Message);
							}
							return 1;
						}
					}
				}
			}
			return 0;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000110FC File Offset: 0x0000F2FC
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int GetBaseMethod(IntPtr state)
		{
			Lua lua = Lua.FromIntPtr(state);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int baseMethodInternal = objectTranslator.MetaFunctionsInstance.GetBaseMethodInternal(lua);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return baseMethodInternal;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00011140 File Offset: 0x0000F340
		private int GetBaseMethodInternal(Lua luaState)
		{
			object rawNetObject = this._translator.GetRawNetObject(luaState, 1);
			if (rawNetObject == null)
			{
				this._translator.ThrowError(luaState, "Trying to index an invalid object reference");
				return 1;
			}
			string text = luaState.ToString(2, false);
			if (string.IsNullOrEmpty(text))
			{
				luaState.PushNil();
				luaState.PushBoolean(false);
				return 2;
			}
			this.GetMember(luaState, new ProxyType(rawNetObject.GetType()), rawNetObject, "__luaInterface_base_" + text, BindingFlags.Instance);
			luaState.SetTop(-2);
			if (luaState.Type(-1) == LuaType.Nil)
			{
				luaState.SetTop(-2);
				return this.GetMember(luaState, new ProxyType(rawNetObject.GetType()), rawNetObject, text, BindingFlags.Instance);
			}
			luaState.PushBoolean(false);
			return 2;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000111E8 File Offset: 0x0000F3E8
		private bool IsMemberPresent(ProxyType objType, string methodName)
		{
			return this.CheckMemberCache(objType, methodName) != null || objType.GetMember(methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public).Length != 0;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00011204 File Offset: 0x0000F404
		private bool TryGetExtensionMethod(Type type, string name, out object method)
		{
			object obj = this.CheckMemberCache(type, name);
			if (obj != null)
			{
				method = obj;
				return true;
			}
			MethodInfo methodInfo;
			bool result = this._translator.TryGetExtensionMethod(type, name, out methodInfo);
			method = methodInfo;
			return result;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00011234 File Offset: 0x0000F434
		private int PushExtensionMethod(Lua luaState, Type type, object obj, string name, object method)
		{
			LuaFunction luaFunction = method as LuaFunction;
			if (luaFunction != null)
			{
				this._translator.PushFunction(luaState, luaFunction);
				this._translator.Push(luaState, true);
				return 2;
			}
			MethodInfo method2 = (MethodInfo)method;
			LuaFunction luaFunction2 = new LuaFunction(new LuaMethodWrapper(this._translator, obj, new ProxyType(type), method2).InvokeFunction.Invoke);
			this.SetMemberCache(type, name, luaFunction2);
			this._translator.PushFunction(luaState, luaFunction2);
			this._translator.Push(luaState, true);
			return 2;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000112C4 File Offset: 0x0000F4C4
		private int GetMember(Lua luaState, ProxyType objType, object obj, string methodName, BindingFlags bindingType)
		{
			bool flag = false;
			MemberInfo memberInfo = null;
			object obj2 = this.CheckMemberCache(objType, methodName);
			if (obj2 is LuaFunction)
			{
				this._translator.PushFunction(luaState, (LuaFunction)obj2);
				this._translator.Push(luaState, true);
				return 2;
			}
			if (obj2 != null)
			{
				memberInfo = (MemberInfo)obj2;
			}
			else
			{
				MemberInfo[] member = objType.GetMember(methodName, bindingType | BindingFlags.Public);
				if (member.Length != 0)
				{
					memberInfo = member[0];
				}
				else
				{
					member = objType.GetMember(methodName, bindingType | BindingFlags.Static | BindingFlags.Public);
					if (member.Length != 0)
					{
						memberInfo = member[0];
						flag = true;
					}
				}
			}
			if (memberInfo != null)
			{
				if (memberInfo.MemberType == MemberTypes.Field)
				{
					FieldInfo fieldInfo = (FieldInfo)memberInfo;
					if (obj2 == null)
					{
						this.SetMemberCache(objType, methodName, memberInfo);
					}
					try
					{
						object value = fieldInfo.GetValue(obj);
						this._translator.Push(luaState, value);
						goto IL_2CD;
					}
					catch
					{
						luaState.PushNil();
						goto IL_2CD;
					}
				}
				if (memberInfo.MemberType == MemberTypes.Property)
				{
					PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
					if (obj2 == null)
					{
						this.SetMemberCache(objType, methodName, memberInfo);
					}
					try
					{
						object value2 = propertyInfo.GetValue(obj, null);
						this._translator.Push(luaState, value2);
						goto IL_2CD;
					}
					catch (ArgumentException)
					{
						if (objType.UnderlyingSystemType != typeof(object))
						{
							return this.GetMember(luaState, new ProxyType(objType.UnderlyingSystemType.BaseType), obj, methodName, bindingType);
						}
						luaState.PushNil();
						goto IL_2CD;
					}
					catch (TargetInvocationException e)
					{
						this.ThrowError(luaState, e);
						luaState.PushNil();
						goto IL_2CD;
					}
				}
				if (memberInfo.MemberType == MemberTypes.Event)
				{
					EventInfo eventInfo = (EventInfo)memberInfo;
					if (obj2 == null)
					{
						this.SetMemberCache(objType, methodName, memberInfo);
					}
					this._translator.Push(luaState, new RegisterEventHandler(this._translator.PendingEvents, obj, eventInfo));
				}
				else
				{
					if (flag)
					{
						this._translator.ThrowError(luaState, "Can't pass instance to static method " + methodName);
						return 1;
					}
					if (memberInfo.MemberType != MemberTypes.NestedType || !(memberInfo.DeclaringType != null))
					{
						LuaFunction invokeFunction = new LuaMethodWrapper(this._translator, objType, methodName, bindingType).InvokeFunction;
						if (obj2 == null)
						{
							this.SetMemberCache(objType, methodName, invokeFunction);
						}
						this._translator.PushFunction(luaState, invokeFunction);
						this._translator.Push(luaState, true);
						return 2;
					}
					if (obj2 == null)
					{
						this.SetMemberCache(objType, methodName, memberInfo);
					}
					string name = memberInfo.Name;
					string className = memberInfo.DeclaringType.FullName + "+" + name;
					Type t = this._translator.FindType(className);
					this._translator.PushType(luaState, t);
				}
				IL_2CD:
				this._translator.Push(luaState, false);
				return 2;
			}
			if (objType.UnderlyingSystemType != typeof(object))
			{
				return this.GetMember(luaState, new ProxyType(objType.UnderlyingSystemType.BaseType), obj, methodName, bindingType);
			}
			this._translator.ThrowError(luaState, "Unknown member name " + methodName);
			return 1;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000115DC File Offset: 0x0000F7DC
		private object CheckMemberCache(Type objType, string memberName)
		{
			return this.CheckMemberCache(new ProxyType(objType), memberName);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000115EC File Offset: 0x0000F7EC
		private object CheckMemberCache(ProxyType objType, string memberName)
		{
			Dictionary<object, object> dictionary;
			if (!this._memberCache.TryGetValue(objType, out dictionary))
			{
				return null;
			}
			object result;
			if (dictionary == null || !dictionary.TryGetValue(memberName, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001161C File Offset: 0x0000F81C
		private void SetMemberCache(Type objType, string memberName, object member)
		{
			this.SetMemberCache(new ProxyType(objType), memberName, member);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001162C File Offset: 0x0000F82C
		private void SetMemberCache(ProxyType objType, string memberName, object member)
		{
			Dictionary<object, object> dictionary;
			Dictionary<object, object> dictionary2;
			if (this._memberCache.TryGetValue(objType, out dictionary))
			{
				dictionary2 = dictionary;
			}
			else
			{
				dictionary2 = new Dictionary<object, object>();
				this._memberCache[objType] = dictionary2;
			}
			dictionary2[memberName] = member;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00011668 File Offset: 0x0000F868
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int SetFieldOrProperty(IntPtr state)
		{
			Lua lua = Lua.FromIntPtr(state);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = objectTranslator.MetaFunctionsInstance.SetFieldOrPropertyInternal(lua);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000116AC File Offset: 0x0000F8AC
		private int SetFieldOrPropertyInternal(Lua luaState)
		{
			object rawNetObject = this._translator.GetRawNetObject(luaState, 1);
			if (rawNetObject == null)
			{
				this._translator.ThrowError(luaState, "trying to index and invalid object reference");
				return 1;
			}
			Type type = rawNetObject.GetType();
			string e;
			if (this.TrySetMember(luaState, new ProxyType(type), rawNetObject, BindingFlags.Instance, out e))
			{
				return 0;
			}
			try
			{
				if (type.IsArray && luaState.IsNumber(2))
				{
					int index = (int)luaState.ToNumber(2);
					Array array = (Array)rawNetObject;
					object asType = this._translator.GetAsType(luaState, 3, array.GetType().GetElementType());
					array.SetValue(asType, index);
				}
				else
				{
					MethodInfo method = type.GetMethod("set_Item");
					if (!(method != null))
					{
						this._translator.ThrowError(luaState, e);
						return 1;
					}
					ParameterInfo[] parameters = method.GetParameters();
					Type parameterType = parameters[1].ParameterType;
					object asType2 = this._translator.GetAsType(luaState, 3, parameterType);
					Type parameterType2 = parameters[0].ParameterType;
					object asType3 = this._translator.GetAsType(luaState, 2, parameterType2);
					method.Invoke(rawNetObject, new object[]
					{
						asType3,
						asType2
					});
				}
			}
			catch (Exception e2)
			{
				this.ThrowError(luaState, e2);
				return 1;
			}
			return 0;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000117F4 File Offset: 0x0000F9F4
		private bool TrySetMember(Lua luaState, ProxyType targetType, object target, BindingFlags bindingType, out string detailMessage)
		{
			detailMessage = null;
			if (luaState.Type(2) != LuaType.String)
			{
				detailMessage = "property names must be strings";
				return false;
			}
			string text = luaState.ToString(2, false);
			if (string.IsNullOrEmpty(text) || (!char.IsLetter(text[0]) && text[0] != '_'))
			{
				detailMessage = "Invalid property name";
				return false;
			}
			MemberInfo memberInfo = (MemberInfo)this.CheckMemberCache(targetType, text);
			if (memberInfo == null)
			{
				MemberInfo[] member = targetType.GetMember(text, bindingType | BindingFlags.Public);
				if (member.Length == 0)
				{
					detailMessage = "field or property '" + text + "' does not exist";
					return false;
				}
				memberInfo = member[0];
				this.SetMemberCache(targetType, text, memberInfo);
			}
			if (memberInfo.MemberType == MemberTypes.Field)
			{
				FieldInfo fieldInfo = (FieldInfo)memberInfo;
				object asType = this._translator.GetAsType(luaState, 3, fieldInfo.FieldType);
				try
				{
					fieldInfo.SetValue(target, asType);
				}
				catch (Exception ex)
				{
					detailMessage = "Error setting field: " + ex.Message;
					return false;
				}
				return true;
			}
			if (memberInfo.MemberType == MemberTypes.Property)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				object asType2 = this._translator.GetAsType(luaState, 3, propertyInfo.PropertyType);
				try
				{
					propertyInfo.SetValue(target, asType2, null);
				}
				catch (Exception ex2)
				{
					detailMessage = "Error setting property: " + ex2.Message;
					return false;
				}
				return true;
			}
			detailMessage = "'" + text + "' is not a .net field or property";
			return false;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001196C File Offset: 0x0000FB6C
		private int SetMember(Lua luaState, ProxyType targetType, object target, BindingFlags bindingType)
		{
			string e;
			if (!this.TrySetMember(luaState, targetType, target, bindingType, out e))
			{
				this._translator.ThrowError(luaState, e);
				return 1;
			}
			return 0;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00011998 File Offset: 0x0000FB98
		private void ThrowError(Lua luaState, Exception e)
		{
			TargetInvocationException ex = e as TargetInvocationException;
			if (ex != null)
			{
				e = ex.InnerException;
			}
			this._translator.ThrowError(luaState, e);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000119C4 File Offset: 0x0000FBC4
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int GetClassMethod(IntPtr state)
		{
			Lua lua = Lua.FromIntPtr(state);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int classMethodInternal = objectTranslator.MetaFunctionsInstance.GetClassMethodInternal(lua);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return classMethodInternal;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00011A08 File Offset: 0x0000FC08
		private int GetClassMethodInternal(Lua luaState)
		{
			ProxyType proxyType = this._translator.GetRawNetObject(luaState, 1) as ProxyType;
			if (proxyType == null)
			{
				this._translator.ThrowError(luaState, "Trying to index an invalid type reference");
				return 1;
			}
			if (luaState.IsNumber(2))
			{
				int length = (int)luaState.ToNumber(2);
				this._translator.Push(luaState, Array.CreateInstance(proxyType.UnderlyingSystemType, length));
				return 1;
			}
			string text = luaState.ToString(2, false);
			if (string.IsNullOrEmpty(text))
			{
				luaState.PushNil();
				return 1;
			}
			return this.GetMember(luaState, proxyType, null, text, BindingFlags.Static);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00011A90 File Offset: 0x0000FC90
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int SetClassFieldOrProperty(IntPtr state)
		{
			Lua lua = Lua.FromIntPtr(state);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = objectTranslator.MetaFunctionsInstance.SetClassFieldOrPropertyInternal(lua);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00011AD4 File Offset: 0x0000FCD4
		private int SetClassFieldOrPropertyInternal(Lua luaState)
		{
			ProxyType proxyType = this._translator.GetRawNetObject(luaState, 1) as ProxyType;
			if (proxyType == null)
			{
				this._translator.ThrowError(luaState, "trying to index an invalid type reference");
				return 1;
			}
			return this.SetMember(luaState, proxyType, null, BindingFlags.Static);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00011B14 File Offset: 0x0000FD14
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int CallDelegate(IntPtr state)
		{
			Lua lua = Lua.FromIntPtr(state);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = objectTranslator.MetaFunctionsInstance.CallDelegateInternal(lua);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00011B58 File Offset: 0x0000FD58
		private int CallDelegateInternal(Lua luaState)
		{
			Delegate @delegate = this._translator.GetRawNetObject(luaState, 1) as Delegate;
			if (@delegate == null)
			{
				this._translator.ThrowError(luaState, "Trying to invoke a not delegate or callable value");
				return 1;
			}
			luaState.Remove(1);
			MethodCache methodCache = new MethodCache();
			MethodBase method = @delegate.Method;
			if (this.MatchParameters(luaState, method, methodCache, 0))
			{
				try
				{
					object o;
					if (method.IsStatic)
					{
						o = method.Invoke(null, methodCache.args);
					}
					else
					{
						o = method.Invoke(@delegate.Target, methodCache.args);
					}
					this._translator.Push(luaState, o);
					return 1;
				}
				catch (TargetInvocationException ex)
				{
					if (this._translator.interpreter.UseTraceback)
					{
						ex.GetBaseException().Data["Traceback"] = this._translator.interpreter.GetDebugTraceback();
					}
					return this._translator.Interpreter.SetPendingException(ex.GetBaseException());
				}
				catch (Exception pendingException)
				{
					return this._translator.Interpreter.SetPendingException(pendingException);
				}
			}
			this._translator.ThrowError(luaState, "Cannot invoke delegate (invalid arguments for  " + method.Name + ")");
			return 1;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00011CA0 File Offset: 0x0000FEA0
		[MonoPInvokeCallback(typeof(LuaFunction))]
		private static int CallConstructor(IntPtr state)
		{
			Lua lua = Lua.FromIntPtr(state);
			ObjectTranslator objectTranslator = ObjectTranslatorPool.Instance.Find(lua);
			int result = objectTranslator.MetaFunctionsInstance.CallConstructorInternal(lua);
			if (objectTranslator.GetObject(lua, -1) is LuaScriptException)
			{
				return lua.Error();
			}
			return result;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		private static ConstructorInfo[] ReorderConstructors(ConstructorInfo[] constructors)
		{
			if (constructors.Length < 2)
			{
				return constructors;
			}
			return (from c in constructors
			group c by c.GetParameters().Length).SelectMany((IGrouping<int, ConstructorInfo> g) => from ci in g
			orderby ci.ToString() descending
			select ci).ToArray<ConstructorInfo>();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00011D48 File Offset: 0x0000FF48
		private int CallConstructorInternal(Lua luaState)
		{
			ProxyType proxyType = this._translator.GetRawNetObject(luaState, 1) as ProxyType;
			if (proxyType == null)
			{
				this._translator.ThrowError(luaState, "Trying to call constructor on an invalid type reference");
				return 1;
			}
			MethodCache methodCache = new MethodCache();
			luaState.Remove(1);
			ConstructorInfo[] array = proxyType.UnderlyingSystemType.GetConstructors();
			array = MetaFunctions.ReorderConstructors(array);
			foreach (ConstructorInfo constructorInfo in array)
			{
				if (this.MatchParameters(luaState, constructorInfo, methodCache, 0))
				{
					try
					{
						this._translator.Push(luaState, constructorInfo.Invoke(methodCache.args));
					}
					catch (TargetInvocationException e)
					{
						this.ThrowError(luaState, e);
						return 1;
					}
					catch
					{
						luaState.PushNil();
					}
					return 1;
				}
			}
			if (proxyType.UnderlyingSystemType.IsValueType && luaState.GetTop() == 0)
			{
				this._translator.Push(luaState, Activator.CreateInstance(proxyType.UnderlyingSystemType));
				return 1;
			}
			string arg = (array.Length == 0) ? "unknown" : array[0].Name;
			this._translator.ThrowError(luaState, string.Format("{0} does not contain constructor({1}) argument match", proxyType.UnderlyingSystemType, arg));
			return 1;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00011E80 File Offset: 0x00010080
		private static bool IsInteger(double x)
		{
			return Math.Ceiling(x) == x;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00011E8C File Offset: 0x0001008C
		private static object GetTargetObject(Lua luaState, string operation, ObjectTranslator translator)
		{
			object rawNetObject = translator.GetRawNetObject(luaState, 1);
			if (rawNetObject != null && rawNetObject.GetType().HasMethod(operation))
			{
				return rawNetObject;
			}
			rawNetObject = translator.GetRawNetObject(luaState, 2);
			if (rawNetObject != null && rawNetObject.GetType().HasMethod(operation))
			{
				return rawNetObject;
			}
			return null;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00011ED4 File Offset: 0x000100D4
		private static int MatchOperator(Lua luaState, string operation, ObjectTranslator translator)
		{
			MethodCache methodCache = new MethodCache();
			object targetObject = MetaFunctions.GetTargetObject(luaState, operation, translator);
			if (targetObject == null)
			{
				translator.ThrowError(luaState, "Cannot call " + operation + " on a nil object");
				return 1;
			}
			Type type = targetObject.GetType();
			foreach (MethodInfo methodInfo in type.GetMethods(operation, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public))
			{
				if (translator.MatchParameters(luaState, methodInfo, methodCache, 0))
				{
					object o;
					if (methodInfo.IsStatic)
					{
						o = methodInfo.Invoke(null, methodCache.args);
					}
					else
					{
						o = methodInfo.Invoke(targetObject, methodCache.args);
					}
					translator.Push(luaState, o);
					return 1;
				}
			}
			translator.ThrowError(luaState, "Cannot call (" + operation + ") on object type " + type.Name);
			return 1;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00011F98 File Offset: 0x00010198
		internal Array TableToArray(Lua luaState, ExtractValue extractValue, Type paramArrayType, ref int startIndex, int count)
		{
			if (count == 0)
			{
				return Array.CreateInstance(paramArrayType, 0);
			}
			object obj = extractValue(luaState, startIndex);
			startIndex++;
			Array array;
			if (obj is LuaTable)
			{
				LuaTable luaTable = (LuaTable)obj;
				IDictionaryEnumerator enumerator = luaTable.GetEnumerator();
				enumerator.Reset();
				array = Array.CreateInstance(paramArrayType, luaTable.Values.Count);
				int num = 0;
				while (enumerator.MoveNext())
				{
					object obj2 = enumerator.Value;
					if (paramArrayType == typeof(object) && obj2 != null && obj2 is double && MetaFunctions.IsInteger((double)obj2))
					{
						obj2 = Convert.ToInt32((double)obj2);
					}
					array.SetValue(Convert.ChangeType(obj2, paramArrayType), num);
					num++;
				}
			}
			else
			{
				array = Array.CreateInstance(paramArrayType, count);
				array.SetValue(obj, 0);
				for (int i = 1; i < count; i++)
				{
					object value = extractValue(luaState, startIndex);
					array.SetValue(value, i);
					startIndex++;
				}
			}
			return array;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000120A4 File Offset: 0x000102A4
		internal bool MatchParameters(Lua luaState, MethodBase method, MethodCache methodCache, int skipParam)
		{
			ParameterInfo[] parameters = method.GetParameters();
			int num = 1;
			int num2 = luaState.GetTop() - skipParam;
			List<object> list = new List<object>();
			List<int> list2 = new List<int>();
			List<MethodArgs> list3 = new List<MethodArgs>();
			foreach (ParameterInfo parameterInfo in parameters)
			{
				ExtractValue extractValue;
				if (!parameterInfo.IsIn && parameterInfo.IsOut)
				{
					list.Add(null);
					list2.Add(list.Count - 1);
				}
				else if (this.IsParamsArray(luaState, num2, num, parameterInfo, out extractValue))
				{
					int count = num2 - num + 1;
					Type elementType = parameterInfo.ParameterType.GetElementType();
					Array item = this.TableToArray(luaState, extractValue, elementType, ref num, count);
					list.Add(item);
					int index = list.LastIndexOf(item);
					list3.Add(new MethodArgs
					{
						Index = index,
						ExtractValue = extractValue,
						IsParamsArray = true,
						ParameterType = elementType
					});
				}
				else if (num > num2)
				{
					if (!parameterInfo.IsOptional)
					{
						return false;
					}
					list.Add(parameterInfo.DefaultValue);
				}
				else if (this.IsTypeCorrect(luaState, num, parameterInfo, out extractValue))
				{
					object item2 = extractValue(luaState, num);
					list.Add(item2);
					int num3 = list.Count - 1;
					list3.Add(new MethodArgs
					{
						Index = num3,
						ExtractValue = extractValue,
						ParameterType = parameterInfo.ParameterType
					});
					if (parameterInfo.ParameterType.IsByRef)
					{
						list2.Add(num3);
					}
					num++;
				}
				else
				{
					if (!parameterInfo.IsOptional)
					{
						return false;
					}
					list.Add(parameterInfo.DefaultValue);
				}
			}
			if (num != num2 + 1)
			{
				return false;
			}
			methodCache.args = list.ToArray();
			methodCache.cachedMethod = method;
			methodCache.outList = list2.ToArray();
			methodCache.argTypes = list3.ToArray();
			return true;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001228A File Offset: 0x0001048A
		private bool IsTypeCorrect(Lua luaState, int currentLuaParam, ParameterInfo currentNetParam, out ExtractValue extractValue)
		{
			extractValue = this._translator.typeChecker.CheckLuaType(luaState, currentLuaParam, currentNetParam.ParameterType);
			return extractValue != null;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000122B0 File Offset: 0x000104B0
		private bool IsParamsArray(Lua luaState, int nLuaParams, int currentLuaParam, ParameterInfo currentNetParam, out ExtractValue extractValue)
		{
			extractValue = null;
			if (!currentNetParam.GetCustomAttributes(typeof(ParamArrayAttribute), false).Any<object>())
			{
				return false;
			}
			bool result = nLuaParams < currentLuaParam;
			if (luaState.Type(currentLuaParam) == LuaType.Table)
			{
				extractValue = this._translator.typeChecker.GetExtractor(typeof(LuaTable));
				if (extractValue != null)
				{
					return true;
				}
			}
			else
			{
				Type elementType = currentNetParam.ParameterType.GetElementType();
				extractValue = this._translator.typeChecker.CheckLuaType(luaState, currentLuaParam, elementType);
				if (extractValue != null)
				{
					return true;
				}
			}
			return result;
		}

		// Token: 0x04000219 RID: 537
		public static readonly LuaFunction GcFunction = new LuaFunction(MetaFunctions.CollectObject);

		// Token: 0x0400021A RID: 538
		public static readonly LuaFunction IndexFunction = new LuaFunction(MetaFunctions.GetMethod);

		// Token: 0x0400021B RID: 539
		public static readonly LuaFunction NewIndexFunction = new LuaFunction(MetaFunctions.SetFieldOrProperty);

		// Token: 0x0400021C RID: 540
		public static readonly LuaFunction BaseIndexFunction = new LuaFunction(MetaFunctions.GetBaseMethod);

		// Token: 0x0400021D RID: 541
		public static readonly LuaFunction ClassIndexFunction = new LuaFunction(MetaFunctions.GetClassMethod);

		// Token: 0x0400021E RID: 542
		public static readonly LuaFunction ClassNewIndexFunction = new LuaFunction(MetaFunctions.SetClassFieldOrProperty);

		// Token: 0x0400021F RID: 543
		public static readonly LuaFunction ExecuteDelegateFunction = new LuaFunction(MetaFunctions.RunFunctionDelegate);

		// Token: 0x04000220 RID: 544
		public static readonly LuaFunction CallConstructorFunction = new LuaFunction(MetaFunctions.CallConstructor);

		// Token: 0x04000221 RID: 545
		public static readonly LuaFunction ToStringFunction = new LuaFunction(MetaFunctions.ToStringLua);

		// Token: 0x04000222 RID: 546
		public static readonly LuaFunction CallDelegateFunction = new LuaFunction(MetaFunctions.CallDelegate);

		// Token: 0x04000223 RID: 547
		public static readonly LuaFunction AddFunction = new LuaFunction(MetaFunctions.AddLua);

		// Token: 0x04000224 RID: 548
		public static readonly LuaFunction SubtractFunction = new LuaFunction(MetaFunctions.SubtractLua);

		// Token: 0x04000225 RID: 549
		public static readonly LuaFunction MultiplyFunction = new LuaFunction(MetaFunctions.MultiplyLua);

		// Token: 0x04000226 RID: 550
		public static readonly LuaFunction DivisionFunction = new LuaFunction(MetaFunctions.DivideLua);

		// Token: 0x04000227 RID: 551
		public static readonly LuaFunction ModulosFunction = new LuaFunction(MetaFunctions.ModLua);

		// Token: 0x04000228 RID: 552
		public static readonly LuaFunction UnaryNegationFunction = new LuaFunction(MetaFunctions.UnaryNegationLua);

		// Token: 0x04000229 RID: 553
		public static readonly LuaFunction EqualFunction = new LuaFunction(MetaFunctions.EqualLua);

		// Token: 0x0400022A RID: 554
		public static readonly LuaFunction LessThanFunction = new LuaFunction(MetaFunctions.LessThanLua);

		// Token: 0x0400022B RID: 555
		public static readonly LuaFunction LessThanOrEqualFunction = new LuaFunction(MetaFunctions.LessThanOrEqualLua);

		// Token: 0x0400022C RID: 556
		private readonly Dictionary<object, Dictionary<object, object>> _memberCache = new Dictionary<object, Dictionary<object, object>>();

		// Token: 0x0400022D RID: 557
		private readonly ObjectTranslator _translator;

		// Token: 0x0400022E RID: 558
		public const string LuaIndexFunction = "local a={}local function b(c,d)local e=getmetatable(c)local f=e.cache[d]if f~=nil then if f==a then return nil end;return f else local g,h=get_object_member(c,d)if h then if g==nil then e.cache[d]=a else e.cache[d]=g end end;return g end end;return b";
	}
}

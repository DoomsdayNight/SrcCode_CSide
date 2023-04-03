﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KeraLua;
using NLua.Exceptions;
using NLua.Extensions;

namespace NLua.Method
{
	// Token: 0x02000074 RID: 116
	internal class LuaMethodWrapper
	{
		// Token: 0x06000423 RID: 1059 RVA: 0x00013EEC File Offset: 0x000120EC
		public LuaMethodWrapper(ObjectTranslator translator, object target, ProxyType targetType, MethodBase method)
		{
			this.InvokeFunction = new LuaFunction(this.Call);
			this._translator = translator;
			this._target = target;
			this._extractTarget = translator.typeChecker.GetExtractor(targetType);
			this._lastCalledMethod = new MethodCache();
			this._method = method;
			this._methodName = method.Name;
			this._isStatic = method.IsStatic;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00013F60 File Offset: 0x00012160
		public LuaMethodWrapper(ObjectTranslator translator, ProxyType targetType, string methodName, BindingFlags bindingType)
		{
			this.InvokeFunction = new LuaFunction(this.Call);
			this._translator = translator;
			this._methodName = methodName;
			this._extractTarget = translator.typeChecker.GetExtractor(targetType);
			this._lastCalledMethod = new MethodCache();
			this._isStatic = ((bindingType & BindingFlags.Static) == BindingFlags.Static);
			MethodInfo[] methodsRecursively = this.GetMethodsRecursively(targetType.UnderlyingSystemType, methodName, bindingType | BindingFlags.Public);
			this._members = LuaMethodWrapper.ReorderMethods(methodsRecursively);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00013FDC File Offset: 0x000121DC
		private static MethodInfo[] ReorderMethods(MethodInfo[] m)
		{
			if (m.Length < 2)
			{
				return m;
			}
			return (from c in m
			group c by c.GetParameters().Length).SelectMany((IGrouping<int, MethodInfo> g) => from ci in g
			orderby ci.ToString() descending
			select ci).ToArray<MethodInfo>();
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00014040 File Offset: 0x00012240
		private MethodInfo[] GetMethodsRecursively(Type type, string methodName, BindingFlags bindingType)
		{
			if (type == typeof(object))
			{
				return type.GetMethods(methodName, bindingType);
			}
			IEnumerable<MethodInfo> methods = type.GetMethods(methodName, bindingType);
			MethodInfo[] methodsRecursively = this.GetMethodsRecursively(type.BaseType, methodName, bindingType);
			return methods.Concat(methodsRecursively).ToArray<MethodInfo>();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001408A File Offset: 0x0001228A
		private int SetPendingException(Exception e)
		{
			return this._translator.interpreter.SetPendingException(e);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000140A0 File Offset: 0x000122A0
		private void FillMethodArguments(Lua luaState, int numStackToSkip)
		{
			object[] args = this._lastCalledMethod.args;
			for (int i = 0; i < this._lastCalledMethod.argTypes.Length; i++)
			{
				MethodArgs methodArgs = this._lastCalledMethod.argTypes[i];
				int num = i + 1 + numStackToSkip;
				if (this._lastCalledMethod.argTypes[i].IsParamsArray)
				{
					int count = this._lastCalledMethod.argTypes.Length - i;
					Array array = this._translator.TableToArray(luaState, methodArgs.ExtractValue, methodArgs.ParameterType, num, count);
					args[this._lastCalledMethod.argTypes[i].Index] = array;
				}
				else
				{
					args[methodArgs.Index] = methodArgs.ExtractValue(luaState, num);
				}
				if (this._lastCalledMethod.args[this._lastCalledMethod.argTypes[i].Index] == null && !luaState.IsNil(i + 1 + numStackToSkip))
				{
					throw new LuaException(string.Format("Argument number {0} is invalid", i + 1));
				}
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000141A0 File Offset: 0x000123A0
		private int PushReturnValue(Lua luaState)
		{
			int num = 0;
			for (int i = 0; i < this._lastCalledMethod.outList.Length; i++)
			{
				num++;
				this._translator.Push(luaState, this._lastCalledMethod.args[this._lastCalledMethod.outList[i]]);
			}
			if (!this._lastCalledMethod.IsReturnVoid && num > 0)
			{
				num++;
			}
			if (num >= 1)
			{
				return num;
			}
			return 1;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001420C File Offset: 0x0001240C
		private int CallInvoke(Lua luaState, MethodBase method, object targetObject)
		{
			if (!luaState.CheckStack(this._lastCalledMethod.outList.Length + 6))
			{
				throw new LuaException("Lua stack overflow");
			}
			try
			{
				object o;
				if (method.IsConstructor)
				{
					o = ((ConstructorInfo)method).Invoke(this._lastCalledMethod.args);
				}
				else
				{
					o = method.Invoke(targetObject, this._lastCalledMethod.args);
				}
				this._translator.Push(luaState, o);
			}
			catch (TargetInvocationException ex)
			{
				if (this._translator.interpreter.UseTraceback)
				{
					ex.GetBaseException().Data["Traceback"] = this._translator.interpreter.GetDebugTraceback();
				}
				return this.SetPendingException(ex.GetBaseException());
			}
			catch (Exception pendingException)
			{
				return this.SetPendingException(pendingException);
			}
			return this.PushReturnValue(luaState);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000142F8 File Offset: 0x000124F8
		private bool IsMethodCached(Lua luaState, int numArgsPassed, int skipParams)
		{
			return !(this._lastCalledMethod.cachedMethod == null) && numArgsPassed == this._lastCalledMethod.argTypes.Length && (this._members.Length == 1 || this._translator.MatchParameters(luaState, this._lastCalledMethod.cachedMethod, this._lastCalledMethod, skipParams));
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00014358 File Offset: 0x00012558
		private int CallMethodFromName(Lua luaState)
		{
			object obj = null;
			if (!this._isStatic)
			{
				obj = this._extractTarget(luaState, 1);
			}
			int num = this._isStatic ? 0 : 1;
			int numArgsPassed = luaState.GetTop() - num;
			if (this.IsMethodCached(luaState, numArgsPassed, num))
			{
				MethodBase cachedMethod = this._lastCalledMethod.cachedMethod;
				if (!luaState.CheckStack(this._lastCalledMethod.outList.Length + 6))
				{
					throw new LuaException("Lua stack overflow");
				}
				this.FillMethodArguments(luaState, num);
				return this.CallInvoke(luaState, cachedMethod, obj);
			}
			else
			{
				if (!this._isStatic)
				{
					if (obj == null)
					{
						this._translator.ThrowError(luaState, string.Format("instance method '{0}' requires a non null target object", this._methodName));
						return 1;
					}
					luaState.Remove(1);
				}
				bool flag = false;
				string text = null;
				foreach (MethodInfo methodInfo in this._members)
				{
					if (!(methodInfo.ReflectedType == null))
					{
						text = methodInfo.ReflectedType.Name + "." + methodInfo.Name;
						if (this._translator.MatchParameters(luaState, methodInfo, this._lastCalledMethod, 0))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					string e = (text == null) ? "Invalid arguments to method call" : ("Invalid arguments to method: " + text);
					this._translator.ThrowError(luaState, e);
					return 1;
				}
				if (this._lastCalledMethod.cachedMethod.ContainsGenericParameters)
				{
					return this.CallInvokeOnGenericMethod(luaState, (MethodInfo)this._lastCalledMethod.cachedMethod, obj);
				}
				return this.CallInvoke(luaState, this._lastCalledMethod.cachedMethod, obj);
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000144EC File Offset: 0x000126EC
		private int CallInvokeOnGenericMethod(Lua luaState, MethodInfo methodToCall, object targetObject)
		{
			List<Type> list = new List<Type>();
			ParameterInfo[] parameters = methodToCall.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i].ParameterType.IsGenericParameter)
				{
					list.Add(this._lastCalledMethod.args[i].GetType());
				}
			}
			MethodInfo methodInfo = methodToCall.MakeGenericMethod(list.ToArray());
			this._translator.Push(luaState, methodInfo.Invoke(targetObject, this._lastCalledMethod.args));
			return this.PushReturnValue(luaState);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00014570 File Offset: 0x00012770
		private int Call(IntPtr state)
		{
			Lua lua = Lua.FromIntPtr(state);
			MethodBase method = this._method;
			object obj = this._target;
			if (!lua.CheckStack(5))
			{
				throw new LuaException("Lua stack overflow");
			}
			this.SetPendingException(null);
			if (method == null)
			{
				return this.CallMethodFromName(lua);
			}
			if (!method.ContainsGenericParameters)
			{
				if (!method.IsStatic && !method.IsConstructor && obj == null)
				{
					obj = this._extractTarget(lua, 1);
					lua.Remove(1);
				}
				if (!this._translator.MatchParameters(lua, method, this._lastCalledMethod, 0))
				{
					this._translator.ThrowError(lua, "Invalid arguments to method call");
					return 1;
				}
				if (this._isStatic)
				{
					obj = null;
				}
				return this.CallInvoke(lua, this._lastCalledMethod.cachedMethod, obj);
			}
			else
			{
				if (!method.IsGenericMethodDefinition)
				{
					this._translator.ThrowError(lua, "Unable to invoke method on generic class as the current method is an open generic method");
					return 1;
				}
				this._translator.MatchParameters(lua, method, this._lastCalledMethod, 0);
				return this.CallInvokeOnGenericMethod(lua, (MethodInfo)method, obj);
			}
		}

		// Token: 0x04000248 RID: 584
		internal LuaFunction InvokeFunction;

		// Token: 0x04000249 RID: 585
		private readonly ObjectTranslator _translator;

		// Token: 0x0400024A RID: 586
		private readonly MethodBase _method;

		// Token: 0x0400024B RID: 587
		private readonly ExtractValue _extractTarget;

		// Token: 0x0400024C RID: 588
		private readonly object _target;

		// Token: 0x0400024D RID: 589
		private readonly bool _isStatic;

		// Token: 0x0400024E RID: 590
		private readonly string _methodName;

		// Token: 0x0400024F RID: 591
		private readonly MethodInfo[] _members;

		// Token: 0x04000250 RID: 592
		private MethodCache _lastCalledMethod;
	}
}

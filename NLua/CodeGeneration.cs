﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using NLua.Method;

namespace NLua
{
	// Token: 0x0200005E RID: 94
	internal class CodeGeneration
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x0000CE64 File Offset: 0x0000B064
		private CodeGeneration()
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = "NLua_generatedcode";
			this.newAssembly = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
			this.newModule = this.newAssembly.DefineDynamicModule("NLua_generatedcode");
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000CF08 File Offset: 0x0000B108
		public static CodeGeneration Instance { get; } = new CodeGeneration();

		// Token: 0x060002E9 RID: 745 RVA: 0x0000CF10 File Offset: 0x0000B110
		private Type GenerateEvent(Type eventHandlerType)
		{
			string name;
			lock (this)
			{
				name = "LuaGeneratedClass" + this.luaClassNumber.ToString();
				this.luaClassNumber++;
			}
			TypeBuilder typeBuilder = this.newModule.DefineType(name, TypeAttributes.Public, this.eventHandlerParent);
			Type[] parameterTypes = new Type[]
			{
				typeof(object),
				eventHandlerType
			};
			Type typeFromHandle = typeof(void);
			ILGenerator ilgenerator = typeBuilder.DefineMethod("HandleEvent", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig, typeFromHandle, parameterTypes).GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Ldarg_2);
			MethodInfo method = this.eventHandlerParent.GetMethod("HandleEvent");
			ilgenerator.Emit(OpCodes.Call, method);
			ilgenerator.Emit(OpCodes.Ret);
			return typeBuilder.CreateType();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000D008 File Offset: 0x0000B208
		private Type GenerateDelegate(Type delegateType)
		{
			string name;
			lock (this)
			{
				name = "LuaGeneratedClass" + this.luaClassNumber.ToString();
				this.luaClassNumber++;
			}
			TypeBuilder typeBuilder = this.newModule.DefineType(name, TypeAttributes.Public, this.delegateParent);
			MethodInfo method = delegateType.GetMethod("Invoke");
			ParameterInfo[] parameters = method.GetParameters();
			Type[] array = new Type[parameters.Length];
			Type returnType = method.ReturnType;
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
				if (!parameters[i].IsIn && parameters[i].IsOut)
				{
					num++;
				}
				if (array[i].IsByRef)
				{
					num2++;
				}
			}
			int[] array2 = new int[num2];
			ILGenerator ilgenerator = typeBuilder.DefineMethod("CallFunction", method.Attributes, returnType, array).GetILGenerator();
			ilgenerator.DeclareLocal(typeof(object[]));
			ilgenerator.DeclareLocal(typeof(object[]));
			ilgenerator.DeclareLocal(typeof(int[]));
			if (!(returnType == typeof(void)))
			{
				ilgenerator.DeclareLocal(returnType);
			}
			else
			{
				ilgenerator.DeclareLocal(typeof(object));
			}
			ilgenerator.Emit(OpCodes.Ldc_I4, array.Length);
			ilgenerator.Emit(OpCodes.Newarr, typeof(object));
			ilgenerator.Emit(OpCodes.Stloc_0);
			ilgenerator.Emit(OpCodes.Ldc_I4, array.Length - num);
			ilgenerator.Emit(OpCodes.Newarr, typeof(object));
			ilgenerator.Emit(OpCodes.Stloc_1);
			ilgenerator.Emit(OpCodes.Ldc_I4, num2);
			ilgenerator.Emit(OpCodes.Newarr, typeof(int));
			ilgenerator.Emit(OpCodes.Stloc_2);
			int j = 0;
			int num3 = 0;
			int num4 = 0;
			while (j < array.Length)
			{
				ilgenerator.Emit(OpCodes.Ldloc_0);
				ilgenerator.Emit(OpCodes.Ldc_I4, j);
				ilgenerator.Emit(OpCodes.Ldarg, j + 1);
				if (array[j].IsByRef)
				{
					if (array[j].GetElementType().IsValueType)
					{
						ilgenerator.Emit(OpCodes.Ldobj, array[j].GetElementType());
						ilgenerator.Emit(OpCodes.Box, array[j].GetElementType());
					}
					else
					{
						ilgenerator.Emit(OpCodes.Ldind_Ref);
					}
				}
				else if (array[j].IsValueType)
				{
					ilgenerator.Emit(OpCodes.Box, array[j]);
				}
				ilgenerator.Emit(OpCodes.Stelem_Ref);
				if (array[j].IsByRef)
				{
					ilgenerator.Emit(OpCodes.Ldloc_2);
					ilgenerator.Emit(OpCodes.Ldc_I4, num4);
					ilgenerator.Emit(OpCodes.Ldc_I4, j);
					ilgenerator.Emit(OpCodes.Stelem_I4);
					array2[num4] = j;
					num4++;
				}
				if (parameters[j].IsIn || !parameters[j].IsOut)
				{
					ilgenerator.Emit(OpCodes.Ldloc_1);
					ilgenerator.Emit(OpCodes.Ldc_I4, num3);
					ilgenerator.Emit(OpCodes.Ldarg, j + 1);
					if (array[j].IsByRef)
					{
						if (array[j].GetElementType().IsValueType)
						{
							ilgenerator.Emit(OpCodes.Ldobj, array[j].GetElementType());
							ilgenerator.Emit(OpCodes.Box, array[j].GetElementType());
						}
						else
						{
							ilgenerator.Emit(OpCodes.Ldind_Ref);
						}
					}
					else if (array[j].IsValueType)
					{
						ilgenerator.Emit(OpCodes.Box, array[j]);
					}
					ilgenerator.Emit(OpCodes.Stelem_Ref);
					num3++;
				}
				j++;
			}
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldloc_0);
			ilgenerator.Emit(OpCodes.Ldloc_1);
			ilgenerator.Emit(OpCodes.Ldloc_2);
			MethodInfo method2 = this.delegateParent.GetMethod("CallFunction");
			ilgenerator.Emit(OpCodes.Call, method2);
			if (returnType == typeof(void))
			{
				ilgenerator.Emit(OpCodes.Pop);
				ilgenerator.Emit(OpCodes.Ldnull);
			}
			else if (returnType.IsValueType)
			{
				ilgenerator.Emit(OpCodes.Unbox, returnType);
				ilgenerator.Emit(OpCodes.Ldobj, returnType);
			}
			else
			{
				ilgenerator.Emit(OpCodes.Castclass, returnType);
			}
			ilgenerator.Emit(OpCodes.Stloc_3);
			for (int k = 0; k < array2.Length; k++)
			{
				ilgenerator.Emit(OpCodes.Ldarg, array2[k] + 1);
				ilgenerator.Emit(OpCodes.Ldloc_0);
				ilgenerator.Emit(OpCodes.Ldc_I4, array2[k]);
				ilgenerator.Emit(OpCodes.Ldelem_Ref);
				if (array[array2[k]].GetElementType().IsValueType)
				{
					ilgenerator.Emit(OpCodes.Unbox, array[array2[k]].GetElementType());
					ilgenerator.Emit(OpCodes.Ldobj, array[array2[k]].GetElementType());
					ilgenerator.Emit(OpCodes.Stobj, array[array2[k]].GetElementType());
				}
				else
				{
					ilgenerator.Emit(OpCodes.Castclass, array[array2[k]].GetElementType());
					ilgenerator.Emit(OpCodes.Stind_Ref);
				}
			}
			if (!(returnType == typeof(void)))
			{
				ilgenerator.Emit(OpCodes.Ldloc_3);
			}
			ilgenerator.Emit(OpCodes.Ret);
			return typeBuilder.CreateType();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		private void GetReturnTypesFromClass(Type klass, out Type[][] returnTypes)
		{
			MethodInfo[] methods = klass.GetMethods();
			returnTypes = new Type[methods.Length][];
			int num = 0;
			foreach (MethodInfo methodInfo in methods)
			{
				if (klass.IsInterface)
				{
					this.GetReturnTypesFromMethod(methodInfo, out returnTypes[num]);
					num++;
				}
				else if (!methodInfo.IsPrivate && !methodInfo.IsFinal && methodInfo.IsVirtual)
				{
					this.GetReturnTypesFromMethod(methodInfo, out returnTypes[num]);
					num++;
				}
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000D648 File Offset: 0x0000B848
		public void GenerateClass(Type klass, out Type newType, out Type[][] returnTypes)
		{
			string name;
			lock (this)
			{
				name = "LuaGeneratedClass" + this.luaClassNumber.ToString();
				this.luaClassNumber++;
			}
			TypeBuilder typeBuilder;
			if (klass.IsInterface)
			{
				typeBuilder = this.newModule.DefineType(name, TypeAttributes.Public, typeof(object), new Type[]
				{
					klass,
					typeof(ILuaGeneratedType)
				});
			}
			else
			{
				typeBuilder = this.newModule.DefineType(name, TypeAttributes.Public, klass, new Type[]
				{
					typeof(ILuaGeneratedType)
				});
			}
			FieldBuilder fieldBuilder = typeBuilder.DefineField("__luaInterface_luaTable", typeof(LuaTable), FieldAttributes.Public);
			FieldBuilder fieldBuilder2 = typeBuilder.DefineField("__luaInterface_returnTypes", typeof(Type[][]), FieldAttributes.Public);
			ILGenerator ilgenerator = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new Type[]
			{
				typeof(LuaTable),
				typeof(Type[][])
			}).GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			if (klass.IsInterface)
			{
				ilgenerator.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));
			}
			else
			{
				ilgenerator.Emit(OpCodes.Call, klass.GetConstructor(Type.EmptyTypes));
			}
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_1);
			ilgenerator.Emit(OpCodes.Stfld, fieldBuilder);
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Ldarg_2);
			ilgenerator.Emit(OpCodes.Stfld, fieldBuilder2);
			ilgenerator.Emit(OpCodes.Ret);
			MethodInfo[] methods = klass.GetMethods();
			returnTypes = new Type[methods.Length][];
			int num = 0;
			foreach (MethodInfo methodInfo in methods)
			{
				if (klass.IsInterface)
				{
					this.GenerateMethod(typeBuilder, methodInfo, MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask, num, fieldBuilder, fieldBuilder2, false, out returnTypes[num]);
					num++;
				}
				else if (!methodInfo.IsPrivate && !methodInfo.IsFinal && methodInfo.IsVirtual)
				{
					this.GenerateMethod(typeBuilder, methodInfo, (methodInfo.Attributes | MethodAttributes.VtableLayoutMask) ^ MethodAttributes.VtableLayoutMask, num, fieldBuilder, fieldBuilder2, true, out returnTypes[num]);
					num++;
				}
			}
			MethodBuilder methodBuilder = typeBuilder.DefineMethod("LuaInterfaceGetLuaTable", MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual | MethodAttributes.HideBySig, typeof(LuaTable), new Type[0]);
			typeBuilder.DefineMethodOverride(methodBuilder, typeof(ILuaGeneratedType).GetMethod("LuaInterfaceGetLuaTable"));
			ilgenerator = methodBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldfld, fieldBuilder);
			ilgenerator.Emit(OpCodes.Ret);
			newType = typeBuilder.CreateType();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000D91C File Offset: 0x0000BB1C
		private void GetReturnTypesFromMethod(MethodInfo method, out Type[] returnTypes)
		{
			ParameterInfo[] parameters = method.GetParameters();
			Type[] array = new Type[parameters.Length];
			List<Type> list = new List<Type>();
			int num = 0;
			int num2 = 0;
			Type returnType = method.ReturnType;
			list.Add(returnType);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
				if (!parameters[i].IsIn && parameters[i].IsOut)
				{
					num++;
				}
				if (array[i].IsByRef)
				{
					list.Add(array[i].GetElementType());
					num2++;
				}
			}
			returnTypes = list.ToArray();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		private void GenerateMethod(TypeBuilder myType, MethodInfo method, MethodAttributes attributes, int methodIndex, FieldInfo luaTableField, FieldInfo returnTypesField, bool generateBase, out Type[] returnTypes)
		{
			ParameterInfo[] parameters = method.GetParameters();
			Type[] array = new Type[parameters.Length];
			List<Type> list = new List<Type>();
			int num = 0;
			int num2 = 0;
			Type returnType = method.ReturnType;
			list.Add(returnType);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
				if (!parameters[i].IsIn && parameters[i].IsOut)
				{
					num++;
				}
				if (array[i].IsByRef)
				{
					list.Add(array[i].GetElementType());
					num2++;
				}
			}
			int[] array2 = new int[num2];
			returnTypes = list.ToArray();
			if (generateBase)
			{
				ILGenerator ilgenerator = myType.DefineMethod("__luaInterface_base_" + method.Name, MethodAttributes.Private | MethodAttributes.HideBySig | MethodAttributes.VtableLayoutMask, returnType, array).GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				for (int j = 0; j < array.Length; j++)
				{
					ilgenerator.Emit(OpCodes.Ldarg, j + 1);
				}
				ilgenerator.Emit(OpCodes.Call, method);
				ilgenerator.Emit(OpCodes.Ret);
			}
			MethodBuilder methodBuilder = myType.DefineMethod(method.Name, attributes, returnType, array);
			if (myType.BaseType.Equals(typeof(object)))
			{
				myType.DefineMethodOverride(methodBuilder, method);
			}
			ILGenerator ilgenerator2 = methodBuilder.GetILGenerator();
			ilgenerator2.DeclareLocal(typeof(object[]));
			ilgenerator2.DeclareLocal(typeof(object[]));
			ilgenerator2.DeclareLocal(typeof(int[]));
			if (!(returnType == typeof(void)))
			{
				ilgenerator2.DeclareLocal(returnType);
			}
			else
			{
				ilgenerator2.DeclareLocal(typeof(object));
			}
			ilgenerator2.Emit(OpCodes.Ldc_I4, array.Length);
			ilgenerator2.Emit(OpCodes.Newarr, typeof(object));
			ilgenerator2.Emit(OpCodes.Stloc_0);
			ilgenerator2.Emit(OpCodes.Ldc_I4, array.Length - num + 1);
			ilgenerator2.Emit(OpCodes.Newarr, typeof(object));
			ilgenerator2.Emit(OpCodes.Stloc_1);
			ilgenerator2.Emit(OpCodes.Ldc_I4, num2);
			ilgenerator2.Emit(OpCodes.Newarr, typeof(int));
			ilgenerator2.Emit(OpCodes.Stloc_2);
			ilgenerator2.Emit(OpCodes.Ldloc_1);
			ilgenerator2.Emit(OpCodes.Ldc_I4_0);
			ilgenerator2.Emit(OpCodes.Ldarg_0);
			ilgenerator2.Emit(OpCodes.Ldfld, luaTableField);
			ilgenerator2.Emit(OpCodes.Stelem_Ref);
			int k = 0;
			int num3 = 1;
			int num4 = 0;
			while (k < array.Length)
			{
				ilgenerator2.Emit(OpCodes.Ldloc_0);
				ilgenerator2.Emit(OpCodes.Ldc_I4, k);
				ilgenerator2.Emit(OpCodes.Ldarg, k + 1);
				if (array[k].IsByRef)
				{
					if (array[k].GetElementType().IsValueType)
					{
						ilgenerator2.Emit(OpCodes.Ldobj, array[k].GetElementType());
						ilgenerator2.Emit(OpCodes.Box, array[k].GetElementType());
					}
					else
					{
						ilgenerator2.Emit(OpCodes.Ldind_Ref);
					}
				}
				else if (array[k].IsValueType)
				{
					ilgenerator2.Emit(OpCodes.Box, array[k]);
				}
				ilgenerator2.Emit(OpCodes.Stelem_Ref);
				if (array[k].IsByRef)
				{
					ilgenerator2.Emit(OpCodes.Ldloc_2);
					ilgenerator2.Emit(OpCodes.Ldc_I4, num4);
					ilgenerator2.Emit(OpCodes.Ldc_I4, k);
					ilgenerator2.Emit(OpCodes.Stelem_I4);
					array2[num4] = k;
					num4++;
				}
				if (parameters[k].IsIn || !parameters[k].IsOut)
				{
					ilgenerator2.Emit(OpCodes.Ldloc_1);
					ilgenerator2.Emit(OpCodes.Ldc_I4, num3);
					ilgenerator2.Emit(OpCodes.Ldarg, k + 1);
					if (array[k].IsByRef)
					{
						if (array[k].GetElementType().IsValueType)
						{
							ilgenerator2.Emit(OpCodes.Ldobj, array[k].GetElementType());
							ilgenerator2.Emit(OpCodes.Box, array[k].GetElementType());
						}
						else
						{
							ilgenerator2.Emit(OpCodes.Ldind_Ref);
						}
					}
					else if (array[k].IsValueType)
					{
						ilgenerator2.Emit(OpCodes.Box, array[k]);
					}
					ilgenerator2.Emit(OpCodes.Stelem_Ref);
					num3++;
				}
				k++;
			}
			ilgenerator2.Emit(OpCodes.Ldarg_0);
			ilgenerator2.Emit(OpCodes.Ldfld, luaTableField);
			ilgenerator2.Emit(OpCodes.Ldstr, method.Name);
			ilgenerator2.Emit(OpCodes.Call, this.classHelper.GetMethod("GetTableFunction"));
			Label label = ilgenerator2.DefineLabel();
			ilgenerator2.Emit(OpCodes.Dup);
			ilgenerator2.Emit(OpCodes.Brtrue_S, label);
			ilgenerator2.Emit(OpCodes.Pop);
			if (!method.IsAbstract)
			{
				ilgenerator2.Emit(OpCodes.Ldarg_0);
				for (int l = 0; l < array.Length; l++)
				{
					ilgenerator2.Emit(OpCodes.Ldarg, l + 1);
				}
				ilgenerator2.Emit(OpCodes.Call, method);
				ilgenerator2.Emit(OpCodes.Ret);
			}
			ilgenerator2.Emit(OpCodes.Ldnull);
			Label label2 = ilgenerator2.DefineLabel();
			ilgenerator2.Emit(OpCodes.Br_S, label2);
			ilgenerator2.MarkLabel(label);
			ilgenerator2.Emit(OpCodes.Ldloc_0);
			ilgenerator2.Emit(OpCodes.Ldarg_0);
			ilgenerator2.Emit(OpCodes.Ldfld, returnTypesField);
			ilgenerator2.Emit(OpCodes.Ldc_I4, methodIndex);
			ilgenerator2.Emit(OpCodes.Ldelem_Ref);
			ilgenerator2.Emit(OpCodes.Ldloc_1);
			ilgenerator2.Emit(OpCodes.Ldloc_2);
			ilgenerator2.Emit(OpCodes.Call, this.classHelper.GetMethod("CallFunction"));
			ilgenerator2.MarkLabel(label2);
			if (returnType == typeof(void))
			{
				ilgenerator2.Emit(OpCodes.Pop);
				ilgenerator2.Emit(OpCodes.Ldnull);
			}
			else if (returnType.IsValueType)
			{
				ilgenerator2.Emit(OpCodes.Unbox, returnType);
				ilgenerator2.Emit(OpCodes.Ldobj, returnType);
			}
			else
			{
				ilgenerator2.Emit(OpCodes.Castclass, returnType);
			}
			ilgenerator2.Emit(OpCodes.Stloc_3);
			for (int m = 0; m < array2.Length; m++)
			{
				ilgenerator2.Emit(OpCodes.Ldarg, array2[m] + 1);
				ilgenerator2.Emit(OpCodes.Ldloc_0);
				ilgenerator2.Emit(OpCodes.Ldc_I4, array2[m]);
				ilgenerator2.Emit(OpCodes.Ldelem_Ref);
				if (array[array2[m]].GetElementType().IsValueType)
				{
					ilgenerator2.Emit(OpCodes.Unbox, array[array2[m]].GetElementType());
					ilgenerator2.Emit(OpCodes.Ldobj, array[array2[m]].GetElementType());
					ilgenerator2.Emit(OpCodes.Stobj, array[array2[m]].GetElementType());
				}
				else
				{
					ilgenerator2.Emit(OpCodes.Castclass, array[array2[m]].GetElementType());
					ilgenerator2.Emit(OpCodes.Stind_Ref);
				}
			}
			if (!(returnType == typeof(void)))
			{
				ilgenerator2.Emit(OpCodes.Ldloc_3);
			}
			ilgenerator2.Emit(OpCodes.Ret);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000E0F4 File Offset: 0x0000C2F4
		public LuaEventHandler GetEvent(Type eventHandlerType, LuaFunction eventHandler)
		{
			Type type;
			if (this.eventHandlerCollection.ContainsKey(eventHandlerType))
			{
				type = this.eventHandlerCollection[eventHandlerType];
			}
			else
			{
				type = this.GenerateEvent(eventHandlerType);
				this.eventHandlerCollection[eventHandlerType] = type;
			}
			LuaEventHandler luaEventHandler = (LuaEventHandler)Activator.CreateInstance(type);
			luaEventHandler.Handler = eventHandler;
			return luaEventHandler;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000E145 File Offset: 0x0000C345
		public void RegisterLuaDelegateType(Type delegateType, Type luaDelegateType)
		{
			this._delegateCollection[delegateType] = luaDelegateType;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000E154 File Offset: 0x0000C354
		public void RegisterLuaClassType(Type klass, Type luaClass)
		{
			LuaClassType value = default(LuaClassType);
			value.klass = luaClass;
			this.GetReturnTypesFromClass(klass, out value.returnTypes);
			this._classCollection[klass] = value;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000E18C File Offset: 0x0000C38C
		public Delegate GetDelegate(Type delegateType, LuaFunction luaFunc)
		{
			List<Type> list = new List<Type>();
			Type type;
			if (this._delegateCollection.ContainsKey(delegateType))
			{
				type = this._delegateCollection[delegateType];
			}
			else
			{
				type = this.GenerateDelegate(delegateType);
				this._delegateCollection[delegateType] = type;
			}
			MethodInfo method = delegateType.GetMethod("Invoke");
			list.Add(method.ReturnType);
			foreach (ParameterInfo parameterInfo in method.GetParameters())
			{
				if (parameterInfo.ParameterType.IsByRef)
				{
					list.Add(parameterInfo.ParameterType);
				}
			}
			LuaDelegate luaDelegate = (LuaDelegate)Activator.CreateInstance(type);
			luaDelegate.Function = luaFunc;
			luaDelegate.ReturnTypes = list.ToArray();
			return Delegate.CreateDelegate(delegateType, luaDelegate, "CallFunction");
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000E254 File Offset: 0x0000C454
		public object GetClassInstance(Type klass, LuaTable luaTable)
		{
			LuaClassType luaClassType;
			if (this._classCollection.ContainsKey(klass))
			{
				luaClassType = this._classCollection[klass];
			}
			else
			{
				luaClassType = default(LuaClassType);
				this.GenerateClass(klass, out luaClassType.klass, out luaClassType.returnTypes);
				this._classCollection[klass] = luaClassType;
			}
			return Activator.CreateInstance(luaClassType.klass, new object[]
			{
				luaTable,
				luaClassType.returnTypes
			});
		}

		// Token: 0x040001F6 RID: 502
		private readonly Dictionary<Type, LuaClassType> _classCollection = new Dictionary<Type, LuaClassType>();

		// Token: 0x040001F7 RID: 503
		private readonly Dictionary<Type, Type> _delegateCollection = new Dictionary<Type, Type>();

		// Token: 0x040001F8 RID: 504
		private Dictionary<Type, Type> eventHandlerCollection = new Dictionary<Type, Type>();

		// Token: 0x040001F9 RID: 505
		private Type eventHandlerParent = typeof(LuaEventHandler);

		// Token: 0x040001FA RID: 506
		private Type delegateParent = typeof(LuaDelegate);

		// Token: 0x040001FB RID: 507
		private Type classHelper = typeof(LuaClassHelper);

		// Token: 0x040001FC RID: 508
		private AssemblyBuilder newAssembly;

		// Token: 0x040001FD RID: 509
		private ModuleBuilder newModule;

		// Token: 0x040001FE RID: 510
		private int luaClassNumber = 1;
	}
}

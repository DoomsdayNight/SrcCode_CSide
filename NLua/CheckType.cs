using System;
using System.Collections.Generic;
using KeraLua;
using NLua.Extensions;
using NLua.Method;

namespace NLua
{
	// Token: 0x0200005C RID: 92
	internal sealed class CheckType
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0000C300 File Offset: 0x0000A500
		public CheckType(ObjectTranslator translator)
		{
			this._translator = translator;
			this._extractValues.Add(typeof(object), new ExtractValue(this.GetAsObject));
			this._extractValues.Add(typeof(sbyte), new ExtractValue(this.GetAsSbyte));
			this._extractValues.Add(typeof(byte), new ExtractValue(this.GetAsByte));
			this._extractValues.Add(typeof(short), new ExtractValue(this.GetAsShort));
			this._extractValues.Add(typeof(ushort), new ExtractValue(this.GetAsUshort));
			this._extractValues.Add(typeof(int), new ExtractValue(this.GetAsInt));
			this._extractValues.Add(typeof(uint), new ExtractValue(this.GetAsUint));
			this._extractValues.Add(typeof(long), new ExtractValue(this.GetAsLong));
			this._extractValues.Add(typeof(ulong), new ExtractValue(this.GetAsUlong));
			this._extractValues.Add(typeof(double), new ExtractValue(this.GetAsDouble));
			this._extractValues.Add(typeof(char), new ExtractValue(this.GetAsChar));
			this._extractValues.Add(typeof(float), new ExtractValue(this.GetAsFloat));
			this._extractValues.Add(typeof(decimal), new ExtractValue(this.GetAsDecimal));
			this._extractValues.Add(typeof(bool), new ExtractValue(this.GetAsBoolean));
			this._extractValues.Add(typeof(string), new ExtractValue(this.GetAsString));
			this._extractValues.Add(typeof(char[]), new ExtractValue(this.GetAsCharArray));
			this._extractValues.Add(typeof(byte[]), new ExtractValue(this.GetAsByteArray));
			this._extractValues.Add(typeof(LuaFunction), new ExtractValue(this.GetAsFunction));
			this._extractValues.Add(typeof(LuaTable), new ExtractValue(this.GetAsTable));
			this._extractValues.Add(typeof(LuaThread), new ExtractValue(this.GetAsThread));
			this._extractValues.Add(typeof(LuaUserData), new ExtractValue(this.GetAsUserdata));
			this._extractNetObject = new ExtractValue(this.GetAsNetObject);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		internal ExtractValue GetExtractor(ProxyType paramType)
		{
			return this.GetExtractor(paramType.UnderlyingSystemType);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000C5FA File Offset: 0x0000A7FA
		internal ExtractValue GetExtractor(Type paramType)
		{
			if (paramType.IsByRef)
			{
				paramType = paramType.GetElementType();
			}
			if (!this._extractValues.ContainsKey(paramType))
			{
				return this._extractNetObject;
			}
			return this._extractValues[paramType];
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000C630 File Offset: 0x0000A830
		internal ExtractValue CheckLuaType(Lua luaState, int stackPos, Type paramType)
		{
			LuaType luaType = luaState.Type(stackPos);
			if (paramType.IsByRef)
			{
				paramType = paramType.GetElementType();
			}
			Type underlyingType = Nullable.GetUnderlyingType(paramType);
			if (underlyingType != null)
			{
				paramType = underlyingType;
			}
			bool flag = paramType == typeof(int) || paramType == typeof(uint) || paramType == typeof(long) || paramType == typeof(ulong) || paramType == typeof(short) || paramType == typeof(ushort) || paramType == typeof(float) || paramType == typeof(double) || paramType == typeof(decimal) || paramType == typeof(byte);
			if (underlyingType != null && luaType == LuaType.Nil)
			{
				if (flag || paramType == typeof(bool))
				{
					return this._extractValues[paramType];
				}
				return this._extractNetObject;
			}
			else
			{
				if (paramType == typeof(object))
				{
					return this._extractValues[paramType];
				}
				if (paramType.IsGenericParameter)
				{
					if (luaType == LuaType.Boolean)
					{
						return this._extractValues[typeof(bool)];
					}
					if (luaType == LuaType.String)
					{
						return this._extractValues[typeof(string)];
					}
					if (luaType == LuaType.Table)
					{
						return this._extractValues[typeof(LuaTable)];
					}
					if (luaType == LuaType.Thread)
					{
						return this._extractValues[typeof(LuaThread)];
					}
					if (luaType == LuaType.UserData)
					{
						return this._extractValues[typeof(object)];
					}
					if (luaType == LuaType.Function)
					{
						return this._extractValues[typeof(LuaFunction)];
					}
					if (luaType == LuaType.Number)
					{
						return this._extractValues[typeof(double)];
					}
				}
				bool flag2 = paramType == typeof(string) || paramType == typeof(char[]) || paramType == typeof(byte[]);
				if (flag)
				{
					if (luaState.IsNumericType(stackPos) && !flag2)
					{
						return this._extractValues[paramType];
					}
				}
				else if (paramType == typeof(bool))
				{
					if (luaState.IsBoolean(stackPos))
					{
						return this._extractValues[paramType];
					}
				}
				else if (flag2)
				{
					if (luaState.IsString(stackPos) || luaType == LuaType.Nil)
					{
						return this._extractValues[paramType];
					}
				}
				else if (paramType == typeof(LuaTable))
				{
					if (luaType == LuaType.Table || luaType == LuaType.Nil)
					{
						return this._extractValues[paramType];
					}
				}
				else if (paramType == typeof(LuaThread))
				{
					if (luaType == LuaType.Thread || luaType == LuaType.Nil)
					{
						return this._extractValues[paramType];
					}
				}
				else if (paramType == typeof(LuaUserData))
				{
					if (luaType == LuaType.UserData || luaType == LuaType.Nil)
					{
						return this._extractValues[paramType];
					}
				}
				else if (paramType == typeof(LuaFunction))
				{
					if (luaType == LuaType.Function || luaType == LuaType.Nil)
					{
						return this._extractValues[paramType];
					}
				}
				else
				{
					if (typeof(Delegate).IsAssignableFrom(paramType) && luaType == LuaType.Function && paramType.GetMethod("Invoke") != null)
					{
						return new ExtractValue(new DelegateGenerator(this._translator, paramType).ExtractGenerated);
					}
					if (paramType.IsInterface && luaType == LuaType.Table)
					{
						return new ExtractValue(new ClassGenerator(this._translator, paramType).ExtractGenerated);
					}
					if ((paramType.IsInterface || paramType.IsClass) && luaType == LuaType.Nil)
					{
						return this._extractNetObject;
					}
					if (luaState.Type(stackPos) == LuaType.Table)
					{
						if (luaState.GetMetaField(stackPos, "__index") == LuaType.Nil)
						{
							return null;
						}
						object netObject = this._translator.GetNetObject(luaState, -1);
						luaState.SetTop(-2);
						if (netObject != null && paramType.IsInstanceOfType(netObject))
						{
							return this._extractNetObject;
						}
					}
				}
				object netObject2 = this._translator.GetNetObject(luaState, stackPos);
				if (netObject2 != null && paramType.IsInstanceOfType(netObject2))
				{
					return this._extractNetObject;
				}
				return null;
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000CA6B File Offset: 0x0000AC6B
		private object GetAsSbyte(Lua luaState, int stackPos)
		{
			if (!luaState.IsNumericType(stackPos))
			{
				return null;
			}
			if (luaState.IsInteger(stackPos))
			{
				return (sbyte)luaState.ToInteger(stackPos);
			}
			return (sbyte)luaState.ToNumber(stackPos);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000CA9C File Offset: 0x0000AC9C
		private object GetAsByte(Lua luaState, int stackPos)
		{
			if (!luaState.IsNumericType(stackPos))
			{
				return null;
			}
			if (luaState.IsInteger(stackPos))
			{
				return (byte)luaState.ToInteger(stackPos);
			}
			return (byte)luaState.ToNumber(stackPos);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000CACD File Offset: 0x0000ACCD
		private object GetAsShort(Lua luaState, int stackPos)
		{
			if (!luaState.IsNumericType(stackPos))
			{
				return null;
			}
			if (luaState.IsInteger(stackPos))
			{
				return (short)luaState.ToInteger(stackPos);
			}
			return (short)luaState.ToNumber(stackPos);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000CAFE File Offset: 0x0000ACFE
		private object GetAsUshort(Lua luaState, int stackPos)
		{
			if (!luaState.IsNumericType(stackPos))
			{
				return null;
			}
			if (luaState.IsInteger(stackPos))
			{
				return (ushort)luaState.ToInteger(stackPos);
			}
			return (ushort)luaState.ToNumber(stackPos);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000CB2F File Offset: 0x0000AD2F
		private object GetAsInt(Lua luaState, int stackPos)
		{
			if (!luaState.IsNumericType(stackPos))
			{
				return null;
			}
			if (luaState.IsInteger(stackPos))
			{
				return (int)luaState.ToInteger(stackPos);
			}
			return (int)luaState.ToNumber(stackPos);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000CB60 File Offset: 0x0000AD60
		private object GetAsUint(Lua luaState, int stackPos)
		{
			if (!luaState.IsNumericType(stackPos))
			{
				return null;
			}
			if (luaState.IsInteger(stackPos))
			{
				return (uint)luaState.ToInteger(stackPos);
			}
			return (uint)luaState.ToNumber(stackPos);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000CB91 File Offset: 0x0000AD91
		private object GetAsLong(Lua luaState, int stackPos)
		{
			if (!luaState.IsNumericType(stackPos))
			{
				return null;
			}
			if (luaState.IsInteger(stackPos))
			{
				return luaState.ToInteger(stackPos);
			}
			return (long)luaState.ToNumber(stackPos);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000CBC1 File Offset: 0x0000ADC1
		private object GetAsUlong(Lua luaState, int stackPos)
		{
			if (!luaState.IsNumericType(stackPos))
			{
				return null;
			}
			if (luaState.IsInteger(stackPos))
			{
				return (ulong)luaState.ToInteger(stackPos);
			}
			return (ulong)luaState.ToNumber(stackPos);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000CBF1 File Offset: 0x0000ADF1
		private object GetAsDouble(Lua luaState, int stackPos)
		{
			if (!luaState.IsNumericType(stackPos))
			{
				return null;
			}
			if (luaState.IsInteger(stackPos))
			{
				return (double)luaState.ToInteger(stackPos);
			}
			return luaState.ToNumber(stackPos);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000CC21 File Offset: 0x0000AE21
		private object GetAsChar(Lua luaState, int stackPos)
		{
			if (!luaState.IsNumericType(stackPos))
			{
				return null;
			}
			if (luaState.IsInteger(stackPos))
			{
				return (char)luaState.ToInteger(stackPos);
			}
			return (char)luaState.ToNumber(stackPos);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000CC52 File Offset: 0x0000AE52
		private object GetAsFloat(Lua luaState, int stackPos)
		{
			if (!luaState.IsNumericType(stackPos))
			{
				return null;
			}
			if (luaState.IsInteger(stackPos))
			{
				return (float)luaState.ToInteger(stackPos);
			}
			return (float)luaState.ToNumber(stackPos);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000CC83 File Offset: 0x0000AE83
		private object GetAsDecimal(Lua luaState, int stackPos)
		{
			if (!luaState.IsNumericType(stackPos))
			{
				return null;
			}
			if (luaState.IsInteger(stackPos))
			{
				return luaState.ToInteger(stackPos);
			}
			return (decimal)luaState.ToNumber(stackPos);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		private object GetAsBoolean(Lua luaState, int stackPos)
		{
			return luaState.ToBoolean(stackPos);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000CCCA File Offset: 0x0000AECA
		private object GetAsCharArray(Lua luaState, int stackPos)
		{
			if (!luaState.IsString(stackPos))
			{
				return null;
			}
			return luaState.ToString(stackPos, false).ToCharArray();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000CCE4 File Offset: 0x0000AEE4
		private object GetAsByteArray(Lua luaState, int stackPos)
		{
			if (!luaState.IsString(stackPos))
			{
				return null;
			}
			return luaState.ToBuffer(stackPos, false);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000CCF9 File Offset: 0x0000AEF9
		private object GetAsString(Lua luaState, int stackPos)
		{
			if (!luaState.IsString(stackPos))
			{
				return null;
			}
			return luaState.ToString(stackPos, false);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000CD0E File Offset: 0x0000AF0E
		private object GetAsTable(Lua luaState, int stackPos)
		{
			return this._translator.GetTable(luaState, stackPos);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000CD1D File Offset: 0x0000AF1D
		private object GetAsThread(Lua luaState, int stackPos)
		{
			return this._translator.GetThread(luaState, stackPos);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000CD2C File Offset: 0x0000AF2C
		private object GetAsFunction(Lua luaState, int stackPos)
		{
			return this._translator.GetFunction(luaState, stackPos);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000CD3B File Offset: 0x0000AF3B
		private object GetAsUserdata(Lua luaState, int stackPos)
		{
			return this._translator.GetUserData(luaState, stackPos);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000CD4C File Offset: 0x0000AF4C
		public object GetAsObject(Lua luaState, int stackPos)
		{
			if (luaState.Type(stackPos) == LuaType.Table && luaState.GetMetaField(stackPos, "__index") != LuaType.Nil)
			{
				if (luaState.CheckMetaTable(-1, this._translator.Tag))
				{
					luaState.Insert(stackPos);
					luaState.Remove(stackPos + 1);
				}
				else
				{
					luaState.SetTop(-2);
				}
			}
			return this._translator.GetObject(luaState, stackPos);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		public object GetAsNetObject(Lua luaState, int stackPos)
		{
			object netObject = this._translator.GetNetObject(luaState, stackPos);
			if (netObject != null || luaState.Type(stackPos) != LuaType.Table)
			{
				return netObject;
			}
			if (luaState.GetMetaField(stackPos, "__index") == LuaType.Nil)
			{
				return null;
			}
			if (luaState.CheckMetaTable(-1, this._translator.Tag))
			{
				luaState.Insert(stackPos);
				luaState.Remove(stackPos + 1);
				netObject = this._translator.GetNetObject(luaState, stackPos);
			}
			else
			{
				luaState.SetTop(-2);
			}
			return netObject;
		}

		// Token: 0x040001F1 RID: 497
		private readonly Dictionary<Type, ExtractValue> _extractValues = new Dictionary<Type, ExtractValue>();

		// Token: 0x040001F2 RID: 498
		private readonly ExtractValue _extractNetObject;

		// Token: 0x040001F3 RID: 499
		private readonly ObjectTranslator _translator;
	}
}

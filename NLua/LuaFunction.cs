using System;
using KeraLua;

namespace NLua
{
	// Token: 0x02000064 RID: 100
	public class LuaFunction : LuaBase
	{
		// Token: 0x06000357 RID: 855 RVA: 0x0000FD1F File Offset: 0x0000DF1F
		public LuaFunction(int reference, Lua interpreter) : base(reference, interpreter)
		{
			this.function = null;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000FD30 File Offset: 0x0000DF30
		public LuaFunction(LuaFunction nativeFunction, Lua interpreter) : base(0, interpreter)
		{
			this.function = nativeFunction;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000FD44 File Offset: 0x0000DF44
		internal object[] Call(object[] args, Type[] returnTypes)
		{
			Lua lua;
			if (!base.TryGet(out lua))
			{
				return null;
			}
			return lua.CallFunction(this, args, returnTypes);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000FD68 File Offset: 0x0000DF68
		public object[] Call(params object[] args)
		{
			Lua lua;
			if (!base.TryGet(out lua))
			{
				return null;
			}
			return lua.CallFunction(this, args);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000FD8C File Offset: 0x0000DF8C
		internal void Push(Lua luaState)
		{
			Lua lua;
			if (!base.TryGet(out lua))
			{
				return;
			}
			if (this._Reference != 0)
			{
				luaState.RawGetInteger(LuaRegistry.Index, (long)this._Reference);
				return;
			}
			lua.PushCSFunction(this.function);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000FDCC File Offset: 0x0000DFCC
		public override string ToString()
		{
			return "function";
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000FDD4 File Offset: 0x0000DFD4
		public override bool Equals(object o)
		{
			LuaFunction luaFunction = o as LuaFunction;
			if (luaFunction == null)
			{
				return false;
			}
			Lua lua;
			if (!base.TryGet(out lua))
			{
				return false;
			}
			if (this._Reference != 0 && luaFunction._Reference != 0)
			{
				return lua.CompareRef(luaFunction._Reference, this._Reference);
			}
			return this.function == luaFunction.function;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000FE2D File Offset: 0x0000E02D
		public override int GetHashCode()
		{
			if (this._Reference == 0)
			{
				return this.function.GetHashCode();
			}
			return this._Reference;
		}

		// Token: 0x04000214 RID: 532
		internal readonly LuaFunction function;
	}
}

using System;
using KeraLua;
using NLua.Extensions;

namespace NLua
{
	// Token: 0x0200006A RID: 106
	public class LuaUserData : LuaBase
	{
		// Token: 0x0600038D RID: 909 RVA: 0x000104A0 File Offset: 0x0000E6A0
		public LuaUserData(int reference, Lua interpreter) : base(reference, interpreter)
		{
		}

		// Token: 0x17000099 RID: 153
		public object this[string field]
		{
			get
			{
				Lua lua;
				if (!base.TryGet(out lua))
				{
					return null;
				}
				return lua.GetObject(this._Reference, field);
			}
			set
			{
				Lua lua;
				if (!base.TryGet(out lua))
				{
					return;
				}
				lua.SetObject(this._Reference, field, value);
			}
		}

		// Token: 0x1700009A RID: 154
		public object this[object field]
		{
			get
			{
				Lua lua;
				if (!base.TryGet(out lua))
				{
					return null;
				}
				return lua.GetObject(this._Reference, field);
			}
			set
			{
				Lua lua;
				if (!base.TryGet(out lua))
				{
					return;
				}
				lua.SetObject(this._Reference, field, value);
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001054C File Offset: 0x0000E74C
		public object[] Call(params object[] args)
		{
			Lua lua;
			if (!base.TryGet(out lua))
			{
				return null;
			}
			return lua.CallFunction(this, args);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001056D File Offset: 0x0000E76D
		internal void Push(Lua luaState)
		{
			luaState.GetRef(this._Reference);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001057B File Offset: 0x0000E77B
		public override string ToString()
		{
			return "userdata";
		}
	}
}

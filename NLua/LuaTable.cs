using System;
using System.Collections;
using KeraLua;
using NLua.Extensions;

namespace NLua
{
	// Token: 0x02000068 RID: 104
	public class LuaTable : LuaBase
	{
		// Token: 0x06000368 RID: 872 RVA: 0x00010073 File Offset: 0x0000E273
		public LuaTable(int reference, Lua interpreter) : base(reference, interpreter)
		{
		}

		// Token: 0x17000093 RID: 147
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

		// Token: 0x17000094 RID: 148
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

		// Token: 0x0600036D RID: 877 RVA: 0x00010120 File Offset: 0x0000E320
		public IDictionaryEnumerator GetEnumerator()
		{
			Lua lua;
			if (!base.TryGet(out lua))
			{
				return null;
			}
			return lua.GetTableDict(this).GetEnumerator();
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0001014C File Offset: 0x0000E34C
		public ICollection Keys
		{
			get
			{
				Lua lua;
				if (!base.TryGet(out lua))
				{
					return null;
				}
				return lua.GetTableDict(this).Keys;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00010174 File Offset: 0x0000E374
		public ICollection Values
		{
			get
			{
				Lua lua;
				if (!base.TryGet(out lua))
				{
					return new object[0];
				}
				return lua.GetTableDict(this).Values;
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000101A0 File Offset: 0x0000E3A0
		internal object RawGet(string field)
		{
			Lua lua;
			if (!base.TryGet(out lua))
			{
				return null;
			}
			return lua.RawGetObject(this._Reference, field);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000101C6 File Offset: 0x0000E3C6
		internal void Push(Lua luaState)
		{
			luaState.GetRef(this._Reference);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000101D4 File Offset: 0x0000E3D4
		public override string ToString()
		{
			return "table";
		}
	}
}

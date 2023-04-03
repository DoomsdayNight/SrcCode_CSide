using System;
using System.Collections.Concurrent;
using KeraLua;

namespace NLua
{
	// Token: 0x0200006D RID: 109
	internal class ObjectTranslatorPool
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00013BCD File Offset: 0x00011DCD
		public static ObjectTranslatorPool Instance
		{
			get
			{
				return ObjectTranslatorPool._instance;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00013BD6 File Offset: 0x00011DD6
		public void Add(Lua luaState, ObjectTranslator translator)
		{
			if (!this.translators.TryAdd(luaState, translator))
			{
				throw new ArgumentException("An item with the same key has already been added. ", "luaState");
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00013BF8 File Offset: 0x00011DF8
		public ObjectTranslator Find(Lua luaState)
		{
			ObjectTranslator result;
			if (!this.translators.TryGetValue(luaState, out result))
			{
				Lua mainThread = luaState.MainThread;
				if (!this.translators.TryGetValue(mainThread, out result))
				{
					throw new Exception("Invalid luaState, couldn't find ObjectTranslator");
				}
			}
			return result;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00013C38 File Offset: 0x00011E38
		public void Remove(Lua luaState)
		{
			ObjectTranslator objectTranslator;
			this.translators.TryRemove(luaState, out objectTranslator);
		}

		// Token: 0x04000241 RID: 577
		private static volatile ObjectTranslatorPool _instance = new ObjectTranslatorPool();

		// Token: 0x04000242 RID: 578
		private ConcurrentDictionary<Lua, ObjectTranslator> translators = new ConcurrentDictionary<Lua, ObjectTranslator>();
	}
}

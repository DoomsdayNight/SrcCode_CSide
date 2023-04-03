using System;
using KeraLua;
using NLua.Extensions;

namespace NLua
{
	// Token: 0x02000069 RID: 105
	public class LuaThread : LuaBase, IEquatable<LuaThread>, IEquatable<Lua>, IEquatable<Lua>
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000373 RID: 883 RVA: 0x000101DB File Offset: 0x0000E3DB
		public Lua State
		{
			get
			{
				return this._luaState;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000374 RID: 884 RVA: 0x000101E4 File Offset: 0x0000E3E4
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

		// Token: 0x06000375 RID: 885 RVA: 0x00010224 File Offset: 0x0000E424
		public LuaThread(int reference, Lua interpreter) : base(reference, interpreter)
		{
			this._luaState = interpreter.GetThreadState(reference);
			this._translator = interpreter.Translator;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00010248 File Offset: 0x0000E448
		public int Reset()
		{
			int top = this._luaState.GetTop();
			int result = this._luaState.ResetThread();
			this._luaState.SetTop(top);
			return result;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00010278 File Offset: 0x0000E478
		public void XMove(Lua to, object val, int index = 1)
		{
			int top = this._luaState.GetTop();
			this._translator.Push(this._luaState, val);
			this._luaState.XMove(to, index);
			this._luaState.SetTop(top);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000102BC File Offset: 0x0000E4BC
		public void XMove(Lua to, object val, int index = 1)
		{
			int top = this._luaState.GetTop();
			this._translator.Push(this._luaState, val);
			this._luaState.XMove(to.State, index);
			this._luaState.SetTop(top);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00010308 File Offset: 0x0000E508
		public void XMove(LuaThread thread, object val, int index = 1)
		{
			int top = this._luaState.GetTop();
			this._translator.Push(this._luaState, val);
			this._luaState.XMove(thread.State, index);
			this._luaState.SetTop(top);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00010351 File Offset: 0x0000E551
		internal void Push(Lua luaState)
		{
			luaState.GetRef(this._Reference);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001035F File Offset: 0x0000E55F
		public override string ToString()
		{
			return "thread";
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00010368 File Offset: 0x0000E568
		public override bool Equals(object obj)
		{
			LuaThread luaThread = obj as LuaThread;
			if (luaThread != null)
			{
				return this.State == luaThread.State;
			}
			Lua lua = obj as Lua;
			if (lua != null)
			{
				return this.State == lua.State;
			}
			Lua lua2 = obj as Lua;
			if (lua2 != null)
			{
				return this.State == lua2;
			}
			return base.Equals(obj);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000103C2 File Offset: 0x0000E5C2
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000103CA File Offset: 0x0000E5CA
		public bool Equals(LuaThread other)
		{
			return this.State == other.State;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000103DA File Offset: 0x0000E5DA
		public bool Equals(Lua other)
		{
			return this.State == other;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000103E5 File Offset: 0x0000E5E5
		public bool Equals(Lua other)
		{
			return this.State == other.State;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000103F5 File Offset: 0x0000E5F5
		public static explicit operator Lua(LuaThread thread)
		{
			return thread.State;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000103FD File Offset: 0x0000E5FD
		public static explicit operator LuaThread(Lua interpreter)
		{
			return interpreter.Thread;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00010405 File Offset: 0x0000E605
		public static bool operator ==(LuaThread threadA, LuaThread threadB)
		{
			return threadA.State == threadB.State;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00010415 File Offset: 0x0000E615
		public static bool operator !=(LuaThread threadA, LuaThread threadB)
		{
			return threadA.State != threadB.State;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00010428 File Offset: 0x0000E628
		public static bool operator ==(LuaThread thread, Lua state)
		{
			return thread.State == state;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00010433 File Offset: 0x0000E633
		public static bool operator !=(LuaThread thread, Lua state)
		{
			return thread.State != state;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00010441 File Offset: 0x0000E641
		public static bool operator ==(Lua state, LuaThread thread)
		{
			return state == thread.State;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001044C File Offset: 0x0000E64C
		public static bool operator !=(Lua state, LuaThread thread)
		{
			return state != thread.State;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001045A File Offset: 0x0000E65A
		public static bool operator ==(LuaThread thread, Lua interpreter)
		{
			return thread.State == interpreter.State;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001046A File Offset: 0x0000E66A
		public static bool operator !=(LuaThread thread, Lua interpreter)
		{
			return thread.State != interpreter.State;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001047D File Offset: 0x0000E67D
		public static bool operator ==(Lua interpreter, LuaThread thread)
		{
			return interpreter.State == thread.State;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001048D File Offset: 0x0000E68D
		public static bool operator !=(Lua interpreter, LuaThread thread)
		{
			return interpreter.State != thread.State;
		}

		// Token: 0x04000217 RID: 535
		private Lua _luaState;

		// Token: 0x04000218 RID: 536
		private ObjectTranslator _translator;
	}
}

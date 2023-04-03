using System;

namespace NLua
{
	// Token: 0x02000063 RID: 99
	public abstract class LuaBase : IDisposable
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0000FC03 File Offset: 0x0000DE03
		protected bool TryGet(out Lua lua)
		{
			if (this._lua.State == null)
			{
				lua = null;
				return false;
			}
			lua = this._lua;
			return true;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000FC20 File Offset: 0x0000DE20
		protected LuaBase(int reference, Lua lua)
		{
			this._lua = lua;
			this._Reference = reference;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000FC38 File Offset: 0x0000DE38
		~LuaBase()
		{
			this.Dispose(false);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000FC68 File Offset: 0x0000DE68
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000FC78 File Offset: 0x0000DE78
		private void DisposeLuaReference(bool finalized)
		{
			if (this._lua == null)
			{
				return;
			}
			Lua lua;
			if (!this.TryGet(out lua))
			{
				return;
			}
			lua.DisposeInternal(this._Reference, finalized);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000FCA8 File Offset: 0x0000DEA8
		public virtual void Dispose(bool disposeManagedResources)
		{
			if (this._disposed)
			{
				return;
			}
			bool finalized = !disposeManagedResources;
			if (this._Reference != 0)
			{
				this.DisposeLuaReference(finalized);
			}
			this._lua = null;
			this._disposed = true;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		public override bool Equals(object o)
		{
			LuaBase luaBase = o as LuaBase;
			Lua lua;
			return luaBase != null && this.TryGet(out lua) && lua.CompareRef(luaBase._Reference, this._Reference);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000FD17 File Offset: 0x0000DF17
		public override int GetHashCode()
		{
			return this._Reference;
		}

		// Token: 0x04000211 RID: 529
		private bool _disposed;

		// Token: 0x04000212 RID: 530
		protected readonly int _Reference;

		// Token: 0x04000213 RID: 531
		private Lua _lua;
	}
}

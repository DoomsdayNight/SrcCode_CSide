using System;

namespace NLua.Exceptions
{
	// Token: 0x0200007C RID: 124
	[Serializable]
	public class LuaScriptException : LuaException
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00014C77 File Offset: 0x00012E77
		public bool IsNetException { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00014C7F File Offset: 0x00012E7F
		public override string Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00014C87 File Offset: 0x00012E87
		public LuaScriptException(string message, string source) : base(message)
		{
			this._source = source;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00014C97 File Offset: 0x00012E97
		public LuaScriptException(Exception innerException, string source) : base("A .NET exception occured in user-code", innerException)
		{
			this._source = source;
			this.IsNetException = 1;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00014CB3 File Offset: 0x00012EB3
		public override string ToString()
		{
			return base.GetType().FullName + ": " + this._source + this.Message;
		}

		// Token: 0x0400025E RID: 606
		private readonly string _source;
	}
}

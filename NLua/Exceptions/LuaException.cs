using System;

namespace NLua.Exceptions
{
	// Token: 0x0200007B RID: 123
	[Serializable]
	public class LuaException : Exception
	{
		// Token: 0x06000451 RID: 1105 RVA: 0x00014C64 File Offset: 0x00012E64
		public LuaException(string message) : base(message)
		{
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00014C6D File Offset: 0x00012E6D
		public LuaException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

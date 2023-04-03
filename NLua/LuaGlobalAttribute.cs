using System;

namespace NLua
{
	// Token: 0x02000065 RID: 101
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class LuaGlobalAttribute : Attribute
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000FE49 File Offset: 0x0000E049
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000FE51 File Offset: 0x0000E051
		public string Name { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000FE5A File Offset: 0x0000E05A
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000FE62 File Offset: 0x0000E062
		public string Description { get; set; }
	}
}

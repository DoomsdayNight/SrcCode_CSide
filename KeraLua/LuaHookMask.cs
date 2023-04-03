using System;

namespace KeraLua
{
	// Token: 0x0200008C RID: 140
	[Flags]
	public enum LuaHookMask
	{
		// Token: 0x0400028C RID: 652
		Disabled = 0,
		// Token: 0x0400028D RID: 653
		Call = 1,
		// Token: 0x0400028E RID: 654
		Return = 2,
		// Token: 0x0400028F RID: 655
		Line = 4,
		// Token: 0x04000290 RID: 656
		Count = 8
	}
}

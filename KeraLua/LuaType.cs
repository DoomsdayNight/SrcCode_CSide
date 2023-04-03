using System;

namespace KeraLua
{
	// Token: 0x02000092 RID: 146
	public enum LuaType
	{
		// Token: 0x040002AF RID: 687
		None = -1,
		// Token: 0x040002B0 RID: 688
		Nil,
		// Token: 0x040002B1 RID: 689
		Boolean,
		// Token: 0x040002B2 RID: 690
		LightUserData,
		// Token: 0x040002B3 RID: 691
		Number,
		// Token: 0x040002B4 RID: 692
		String,
		// Token: 0x040002B5 RID: 693
		Table,
		// Token: 0x040002B6 RID: 694
		Function,
		// Token: 0x040002B7 RID: 695
		UserData,
		// Token: 0x040002B8 RID: 696
		Thread
	}
}

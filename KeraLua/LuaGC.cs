using System;

namespace KeraLua
{
	// Token: 0x0200008A RID: 138
	public enum LuaGC
	{
		// Token: 0x0400027A RID: 634
		Stop,
		// Token: 0x0400027B RID: 635
		Restart,
		// Token: 0x0400027C RID: 636
		Collect,
		// Token: 0x0400027D RID: 637
		Count,
		// Token: 0x0400027E RID: 638
		Countb,
		// Token: 0x0400027F RID: 639
		Step,
		// Token: 0x04000280 RID: 640
		[Obsolete("Deprecatad since Lua 5.4, Use Incremental instead")]
		SetPause,
		// Token: 0x04000281 RID: 641
		[Obsolete("Deprecatad since Lua 5.4, Use Incremental instead")]
		SetStepMultiplier,
		// Token: 0x04000282 RID: 642
		IsRunning = 9,
		// Token: 0x04000283 RID: 643
		Generational,
		// Token: 0x04000284 RID: 644
		Incremental
	}
}

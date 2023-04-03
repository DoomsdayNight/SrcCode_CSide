using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004C0 RID: 1216
	public interface INKMUnitStateEvent : IEventConditionOwner
	{
		// Token: 0x0600222B RID: 8747
		bool LoadFromLUA(NKMLua cNKMLua);

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x0600222C RID: 8748
		bool bAnimTime { get; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x0600222D RID: 8749
		bool bStateEnd { get; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600222E RID: 8750
		float EventStartTime { get; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x0600222F RID: 8751
		EventRollbackType RollbackType { get; }
	}
}

using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004C1 RID: 1217
	public interface INKMUnitStateEventRollback : INKMUnitStateEvent, IEventConditionOwner
	{
		// Token: 0x06002230 RID: 8752
		void ProcessEventRollback(NKMGame cNKMGame, NKMUnit cNKMUnit, float rollbackTime);
	}
}

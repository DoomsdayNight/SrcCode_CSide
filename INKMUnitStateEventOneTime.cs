using System;
using NKM.Unit;

namespace NKM
{
	// Token: 0x020004C2 RID: 1218
	public interface INKMUnitStateEventOneTime : INKMUnitStateEvent, IEventConditionOwner
	{
		// Token: 0x06002231 RID: 8753
		void ProcessEvent(NKMGame cNKMGame, NKMUnit cNKMUnit);
	}
}

using System;

namespace UnityEngine.UI.Extensions
{
	// Token: 0x0200030E RID: 782
	internal interface IScrollSnap
	{
		// Token: 0x06001187 RID: 4487
		void ChangePage(int page);

		// Token: 0x06001188 RID: 4488
		void SetLerp(bool value);

		// Token: 0x06001189 RID: 4489
		int CurrentPage();

		// Token: 0x0600118A RID: 4490
		void StartScreenChange();
	}
}

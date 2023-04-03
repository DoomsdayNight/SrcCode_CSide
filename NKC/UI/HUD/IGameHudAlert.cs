using System;

namespace NKC.UI.HUD
{
	// Token: 0x02000C3B RID: 3131
	internal interface IGameHudAlert
	{
		// Token: 0x06009165 RID: 37221
		void SetActive(bool value);

		// Token: 0x06009166 RID: 37222
		void OnStart();

		// Token: 0x06009167 RID: 37223
		void OnUpdate();

		// Token: 0x06009168 RID: 37224
		void OnCleanup();

		// Token: 0x06009169 RID: 37225
		bool IsFinished();
	}
}

using System;

namespace UnityEngine.UI.Extensions.Tweens
{
	// Token: 0x02000360 RID: 864
	internal interface ITweenValue
	{
		// Token: 0x0600149C RID: 5276
		void TweenValue(float floatPercentage);

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600149D RID: 5277
		bool ignoreTimeScale { get; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x0600149E RID: 5278
		float duration { get; }

		// Token: 0x0600149F RID: 5279
		bool ValidTarget();

		// Token: 0x060014A0 RID: 5280
		void Finished();
	}
}

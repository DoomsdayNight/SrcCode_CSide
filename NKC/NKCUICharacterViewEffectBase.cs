using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x0200074F RID: 1871
	public abstract class NKCUICharacterViewEffectBase : MonoBehaviour
	{
		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06004AE2 RID: 19170 RVA: 0x00166FB9 File Offset: 0x001651B9
		public virtual Color EffectColor { get; } = Color.white;

		// Token: 0x06004AE3 RID: 19171
		public abstract void SetEffect(NKCASUIUnitIllust unitIllust, Transform trOriginalRoot);

		// Token: 0x06004AE4 RID: 19172
		public abstract void CleanUp(NKCASUIUnitIllust unitIllust, Transform trOriginalRoot);

		// Token: 0x06004AE5 RID: 19173
		public abstract void SetColor(Color color);

		// Token: 0x06004AE6 RID: 19174
		public abstract void SetColor(float fR = -1f, float fG = -1f, float fB = -1f, float fA = -1f);
	}
}

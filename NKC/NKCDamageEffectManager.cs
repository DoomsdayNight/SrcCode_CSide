using System;
using NKM;

namespace NKC
{
	// Token: 0x02000667 RID: 1639
	public class NKCDamageEffectManager : NKMDamageEffectManager
	{
		// Token: 0x06003392 RID: 13202 RVA: 0x00103C1F File Offset: 0x00101E1F
		protected override NKMDamageEffect CreateDamageEffect()
		{
			return (NKCDamageEffect)this.m_NKMGame.GetObjectPool().OpenObj(NKM_OBJECT_POOL_TYPE.NOPT_NKCDamageEffect, "", "", false);
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x00103C42 File Offset: 0x00101E42
		protected override void CloseDamageEffect(NKMDamageEffect cNKMDamageEffect)
		{
			this.m_NKMGame.GetObjectPool().CloseObj(cNKMDamageEffect);
		}
	}
}
